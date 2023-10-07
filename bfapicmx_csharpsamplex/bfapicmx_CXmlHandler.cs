/*This sample code is made available at no cost.
 * Any liability for defects as to quality or title of the information,
 * software and documentation especially in relation to the correctness
 * or absence of defects or the absence of claims or third party rights 
 * or in relation to completeness and/or fitness for purpose are excluded except
 * for cases involving willful misconduct or fraud.
 * Any further liability of Siemens is excluded unless required by law,
 * e.g. under the Act on Product Liability or in cases of willful misconduct,
 * gross negligence, personal injury or death, failure to meet guaranteed characteristics,
 * fraudulent concealment of a defect or in case of breach of fundamental contractual obligations.
 * The damages in case of breach of fundamental contractual obligations is limited to the contract-typical,
 * foreseeable damage if there is no willful misconduct or gross negligence.
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;  

namespace Siemens.Automation.bfapicmx_csharpsamplex
{
    /// <summary>
    /// Generate XML as input for the API and parse the reponse
    /// </summary>
    public static class XmlHandler
    {
        // The header contains a script, which allows to toggle word-wrap, after the page has loaded.
        const string HtmlHeader = "<html><head><script>function setoverflow() { "
                                + "if(document.body.style.whiteSpace == 'normal') { document.body.style.overflowX = 'auto'; document.body.style.whiteSpace = 'nowrap'; } "
                                + "else { document.body.style.overflowX = 'hidden';  document.body.style.whiteSpace = 'normal'; document.body.style.wordWrap = 'break-word'; } } </script></head>\n"
                                + "<body style=\"font-size:10.5pt; font-family:Courier New; overflow-y:auto; word-wrap: break-word; white-space: normal;\">\n";

        // These colors are used for the browser and treeview representation
        static Color s_XmlColor_Element = Colors.Blue;
        static Color s_XmlColor_XmlDecl = Colors.Blue;
        static Color s_XmlColor_Comment = Colors.Green;
        static Color s_XmlColor_Attrib_Name = Colors.Red;
        static Color s_XmlColor_Attrib_Value = Colors.Purple;
        static MainWindow m_mainwindow = new MainWindow();

        /// <summary>
        /// Special handling of some commands
        /// Should be used exceptionally
        /// </summary>
        /// <param name="command">The command to be executed (can be changed)</param>
        /// <param name="tagParamCltn">Collection of tags</param>
        /// <param name="attributeParamCltn">Collection of attributes</param>
        /// <param name="valueParamCltn">Collection of values</param>
        public static void HandleSpecialCommands(   ref string command, Collection<string[]> tagParamCltn,
                                                    Collection<string[]> attributeParamCltn,
                                                    Collection<string[]> valueParamCltn,
                                                    ref string tmpCommand/*Just 4 GetHistoricalSISEventsByFile*/,
                                                    MainWindow mainwindow /*Just to differ the BATCH from the UNIT*/)
        {
            if (tagParamCltn == null || attributeParamCltn == null || valueParamCltn == null)
            {
                throw new ArgumentNullException("tagParamCltn");
                throw new ArgumentNullException("attributeParamCltn");
                throw new ArgumentNullException("valueParamCltn");
            }
            switch (command)
            {
                case "GetProductData4Formula":
                case "GetProductData4MR":
                    command = "GetProductData4Object";
                    break;
                case "GetHistoricalSISEventsByTime":
                case "GetHistoricalSISEventsByID":
                case "GetHistoricalSISEventsByFile":
                    tmpCommand = command;
                    m_mainwindow = mainwindow;
                    if (m_mainwindow.UI_RBUNIT.IsChecked.Value == true)
                    {
                        command = "GetHistoricalSISEvents4Unit";
                    }
                    else 
                    {
                        command = "GetHistoricalSISEvents4Batch";
                    }
                    break;
                case "SetProductData4Formula":
                    command = "SetProductData4Object";
                    break;
                case "ExecuteCommand_GetErrorText":
                case "ExecuteCommand_Acknowledge":
                case "ExecuteCommand_GetAllActOperAllocs":
                case "ExecuteCommand_GetAllActObj":
                case "ExecuteCommand_GetData4PCell":
                case "ExecuteCommand_GetProjectSettings":
                    command = "ExecuteCommand";
                    break;
                case "GetAllUnits":
                    command = "GetChildren";
                    break;
                case "CreateManyMaterials":
                    bool handled = false;
                    for (int i = 0; i < attributeParamCltn.Count && !handled; i++)
                        for (int j = 0; j < attributeParamCltn[i].Length && !handled; j++)
                        {
                            string attrib = attributeParamCltn[i][j];
                            if (attrib == XmlParam.BF_API_XML_MATERIAL_XML)
                            {
                                string path = valueParamCltn[i][j];

                                tagParamCltn.RemoveAt(i);
                                attributeParamCltn.RemoveAt(i);
                                valueParamCltn.RemoveAt(i);

                                XmlDocument doc = new XmlDocument();
                                string xml = File.ReadAllText(path);
                                doc.LoadXml(xml);
                                List<string> new_tags = new List<string>();

                                for (int k = 0; k < doc.DocumentElement.ChildNodes.Count; k++ )
                                {
                                    XmlNode node = doc.DocumentElement.ChildNodes[k];
                                    if (node.LocalName == XmlTag.BF_API_XMLTAG_DATA)
                                    {
                                        string[] new_attributes = new string[node.Attributes.Count];
                                        string[] new_values = new string[node.Attributes.Count];
                                        for (int l = 0; l < node.Attributes.Count; l++)
                                        {
                                            XmlAttribute xmlAttrib = node.Attributes[l];
                                            new_attributes[l] = xmlAttrib.LocalName;
                                            new_values[l] = xmlAttrib.Value;
                                        }
                                        new_tags.Add(XmlTag.BF_API_XMLTAG_DATA);
                                        attributeParamCltn.Add(new_attributes);
                                        valueParamCltn.Add(new_values);
                                    }
                                }
                                tagParamCltn.Add(new_tags.ToArray());

                                handled = true;

                                break;
                            }
                        }            
                    break;
                case "SetManyParameters":
                    command = "SetParameter";
                    bool handle = false;
                    for (int i = 0; i < attributeParamCltn.Count && !handle; i++)
                        for (int j = 0; j < attributeParamCltn[i].Length && !handle; j++)
                        {
                            string attrib = attributeParamCltn[i][j];
                            if (attrib == XmlParam.BF_API_XML_MANYPARAMETER_XML)
                            {
                                string path = valueParamCltn[i][j];

                                tagParamCltn.RemoveAt(i);
                                attributeParamCltn.RemoveAt(i);
                                valueParamCltn.RemoveAt(i);

                                XmlDocument doc = new XmlDocument();
                                string xml = File.ReadAllText(path);
                                bool isXml = xml.StartsWith("<");
                                if (!isXml)
                                {
                                    throw new ArgumentException("WRONG XML FORMAT");
                                }
                                doc.LoadXml(xml);
                                List<string> new_tags = new List<string>();

                                for (int k = 0; k < doc.DocumentElement.ChildNodes.Count; k++ )
                                {
                                    XmlNode node = doc.DocumentElement.ChildNodes[k];
                                    if (node.LocalName == XmlTag.BF_API_XMLTAG_DATA)
                                    {
                                        string[] new_attributes = new string[node.Attributes.Count];
                                        string[] new_values = new string[node.Attributes.Count];
                                        for (int l = 0; l < node.Attributes.Count; l++)
                                        {
                                            XmlAttribute xmlAttrib = node.Attributes[l];
                                            new_attributes[l] = xmlAttrib.LocalName;
                                            new_values[l] = xmlAttrib.Value;
                                        }
                                        new_tags.Add(XmlTag.BF_API_XMLTAG_DATA);
                                        attributeParamCltn.Add(new_attributes);
                                        valueParamCltn.Add(new_values);
                                    }
                                }
                                tagParamCltn.Add(new_tags.ToArray());

                                handled = true;
                                break;
                            }
                        }            
                    break;
            }
        }

        /// <summary>
        /// Generates xml document to be sent to the API
        /// </summary>
        /// <param name="tagParamCltn">Collection of tags</param>
        /// <param name="attributeParamCltn">Collection of attributes</param>
        /// <param name="valueParamCltn">Collection of values</param>
        /// <returns></returns>
        public static string BuildXMLDocument(Collection<string[]> tagParamCltn, Collection<string[]> attributeParamCltn, Collection<string[]> valueParamCltn)
        {
            XmlDocument XMLDocument = new XmlDocument();
            string xml;
            XMLDocument.AppendChild(XMLDocument.CreateXmlDeclaration("1.0", "UTF-16", null));
            XMLDocument.AppendChild(XMLDocument.CreateElement("Request"));
            if (tagParamCltn == null || attributeParamCltn == null || valueParamCltn == null)
            {
                throw new ArgumentNullException("tagParamCltn");
                throw new ArgumentNullException("attributeParamCltn");
                throw new ArgumentNullException("valueParamCltn");
            }
            if (tagParamCltn.Count > 0)
            {
                XmlElement xmlelement = null;
                XmlElement last_element = null;
                XmlElement vor_last_element = null;
                XmlElement root_element = null;
                for (int i = 0; i < tagParamCltn.Count; i++)
                {
                    if (tagParamCltn[i] != null)
                    {
                        for (int j = 0; j < tagParamCltn[i].Count(); j++)
                        {
                           xmlelement = XMLDocument.CreateElement(tagParamCltn[i].ElementAt(j));
                           for (int k = 0; k < attributeParamCltn[i].Count(); k++)
                                {
                                    XmlAttribute xmlattribute = XMLDocument.CreateAttribute(attributeParamCltn[i].ElementAt(k));
                                    xmlattribute.Value = valueParamCltn[i].ElementAt(k);

                                    xmlelement.SetAttributeNode(xmlattribute);
                                }
                           if (last_element != null)

                               if (last_element.Name == xmlelement.Name)
                               {
                                   //vor_last_element.AppendChild(xmlelement);
                                   root_element.AppendChild(xmlelement);
                               }
                               else
                               {
                                   last_element.AppendChild(xmlelement);
                                   //root_element.AppendChild(xmlelement);
                               }

                           else
                           {
                               XMLDocument.DocumentElement.AppendChild(xmlelement);
                               root_element = xmlelement;
                           }
                        }
                        vor_last_element = last_element;
                        last_element = xmlelement;
                    }
                    else
                    {
                        if (attributeParamCltn.Count > 0)
                        {
                            XmlElement XMLElement = XMLDocument.CreateElement(XmlTag.BF_API_XMLTAG_OBJECT);
                            XMLDocument.DocumentElement.AppendChild(XMLElement);

                            for (int j = 0; j < attributeParamCltn[i].Count(); j++)
                            {
                                XmlAttribute _xmlattribute = XMLDocument.CreateAttribute(attributeParamCltn[i].ElementAt(j));
                                _xmlattribute.Value = valueParamCltn[i].ElementAt(j);

                                XMLElement.SetAttributeNode(_xmlattribute);

                            }
                        }
                        else // No Attribute Collection!
                        {
                            throw new ArgumentNullException("attributeParamCltn", "Function BuildXMLDocument got no Attributes!");
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("attributeParamCltn","Function BuildXMLDocument got no Parameters! Specially for Copy/Delparameter ");
            }

             xml = XMLDocument.InnerXml;

            return xml;
        }

        /// <summary>
        /// Reads the Xml document recursively
        /// </summary>
        /// <param name="Output">Xml output by API</param>
        /// <param name="Command">Command executed on API</param>
        /// <param name="HDLs">Collection of handles to be filled</param>
        /// <param name="Values">Collection of values to be filled</param>
        public static void ReadXMLDocument(string Output, List<string> HDLs, List<Dictionary<string, string>> Values)
        {
            if (!String.IsNullOrEmpty(Output))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new StringReader(Output));
                ReadXMLDocument(doc, HDLs, Values);
            }
            else // Error handling
            {

            }
        }

        /// <summary>
        /// Reads all handles and values in xml document and saves in collections
        /// </summary>
        /// <param name="Node">Current xml node</param>
        /// <param name="Command"></param>
        /// <param name="HDLs">Collection of handles to be filled</param>
        /// <param name="Values">Collection of values to be filled</param>
        static void ReadXMLDocument(XmlNode Node, List<string> HDLs, List<Dictionary<string, string>> Values)
        {
            Dictionary<string, string> attributeDictionary = null;

            if (Node.Name == XmlTag.BF_API_XMLTAG_DATA || Node.Name == XmlTag.BF_API_XMLTAG_OBJECT || Node.Name == XmlTag.BF_API_XMLTAG_VALUE)
            {
                if (Node.Attributes[XmlParam.BF_API_XML_SESSION] != null)
                {
                    HDLs.Add(Node.Attributes[XmlParam.BF_API_XML_SESSION].Value);
                }
                attributeDictionary = GetAttributes(Node);
            }

            if (attributeDictionary != null && attributeDictionary.Count > 0)
                Values.Add(attributeDictionary);

            foreach (XmlNode childnode in Node.ChildNodes)
            {
                ReadXMLDocument(childnode, HDLs, Values);
            }
        }

        /// <summary>
        /// Read all attributes of a node and return in a Dictionary<string, string>
        /// </summary>
        /// <param name="Node">Current xml node</param>
        static Dictionary<string, string> GetAttributes(XmlNode Node)
        {
            Dictionary<string, string> attributeDictionary = new Dictionary<string, string>();
            foreach (XmlAttribute attrib in Node.Attributes)
            {
                string name = attrib.LocalName;
                string value = attrib.Value;
                if (!string.IsNullOrWhiteSpace(value) && value != "ERROR" && value != "<E>")
                    attributeDictionary.Add(name, value);
            }
            return attributeDictionary;
        }


        /// <summary>
        /// Try to read xml, convert to colored html format and show in a browser control
        /// </summary>
        /// <param name="Browser">The browser control to use</param>
        /// <param name="Input">The input that will be sent to the API</param>
        public static void ShowInput(System.Windows.Controls.WebBrowser Browser, string Input)
        {
            StringBuilder html = new StringBuilder(HtmlHeader);
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Input);
                Xml2Html(doc, ref html, 0);
            }
            catch (FormatException e)
            {
               string strException = e.Message;
                html.AppendLine(System.Net.WebUtility.HtmlEncode(Input));
            }

            html.Append("</body></html>");
            if (Browser == null) 
            {
                throw new ArgumentNullException("Browser");
            }
            Browser.NavigateToString(html.ToString());
        }

        /// <summary>
        /// Try to read xml, convert to colored html format and show in a browser control
        /// Also show a message if no xml provided or error code not 0
        /// </summary>
        /// <param name="Browser">The browser control to use</param>
        /// <param name="Command">The command that was executed</param>
        /// <param name="Output">The response from API, can be xml or plain text</param>
        /// <param name="ErrorCode">The error code returned from API</param>
        public static void ShowOutput(WebBrowser Browser, TreeView Treeview, string Command, string Output, int ErrorCode)
        {
            // In case ErrorCode is not 0 or the response is not in xml format, Message is shown on the output browser
            string Message = "<b>" + Command + "</b><br /> ";
            if (ErrorCode != int.MaxValue)
                Message += "Exited with Code: <b>" + ErrorCode;
            else
                Message += "failed.";

            Message += "</b>";

            if (Browser == null || Treeview == null)
            {
                throw new ArgumentNullException("Browser");
                throw new ArgumentNullException("Treeview");
            }
            // Add the html header (<html><head>...)
            StringBuilder Html_Output = new StringBuilder(HtmlHeader);

            // Only stays false, if xml is successfully parsed and 
            // displayed on browser and treeview control
            bool Xml_Failed = false;
            if (string.IsNullOrWhiteSpace(Output))
                Xml_Failed = true;
            try
            {
                if (!Xml_Failed)
                {
                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.Load(new StringReader(Output));

                    // Some commands return an xml even if the command was unsuccessful
                    if (ErrorCode != 0)
                        Html_Output.Append(Message + "<br /><br /><b>Response:</b><br />");

                    // Convert xml to html in a human readable format
                    Xml2Html(XMLDoc, ref Html_Output, 0);

                    // If the command had been executed before, search the former entry
                    var items = new List<TreeViewItem>();
                    foreach (TreeViewItem item in Treeview.Items)
                    {
                        item.IsExpanded = false;
                        if ((string)item.Header == Command)
                            items.Add(item);
                    }

                    // Remove the entry
                    foreach (TreeViewItem item in items)
                    {
                        Treeview.Items.Remove(item);
                    }

                    // Create new item
                    TreeViewItem New_Item = new TreeViewItem();
                    New_Item.Header = Command;
                    New_Item.IsExpanded = true;
                    Treeview.Items.Add(New_Item);

                    // Fill new item
                    Xml2TreeView(XMLDoc, New_Item, true);
                }
            }
            catch (XmlException) // These two exception types are common when the response is not in xml format
            {
                Xml_Failed = true;
            }
            catch (ArgumentNullException)
            {
                Xml_Failed = true;
            }

            if (Xml_Failed) // Show error code and plain response instead of xml representation
            {
                Html_Output.AppendLine(Message);
                if (Output != null && Output != "")
                {
                    string resp = Output;
                    if (ErrorCode != 0)
                        resp = System.Net.WebUtility.HtmlEncode(Output);
                    Html_Output.AppendLine("<br /><br /><b>Response:</b><br />" + resp);
                }

                // Show Browser instead of Treeview (can be toggled with a button)
                if (Treeview == null)
                {
                    throw new ArgumentNullException("Treeview");
                }
                Treeview.Visibility = System.Windows.Visibility.Hidden;
                if (Browser == null) 
                {
                    throw new ArgumentNullException("Browser"); 
                }
                Browser.Visibility = System.Windows.Visibility.Visible;
            }

            Html_Output.Append("</body></html>");
            Browser.NavigateToString(Html_Output.ToString());
        }

        /// <summary>
        /// Recursively converts an xml document to a colored html format
        /// </summary>
        /// <param name="Node">The current xml node</param>
        /// <param name="Html">The reference to a StringBuilder object containing the html</param>
        /// <param name="Indent">The indention level depending on the nesting of the xml document</param>
        static void Xml2Html(XmlNode Node, ref StringBuilder Html, int Indent)
        {
            // Get colors defined at the top of the class in hex format (e.g. #0000FF)
            Func<Color, string> GetColorString = str => string.Format("#{0:X2}{1:X2}{2:X2}", str.R, str.G, str.B);
            string col_elem = GetColorString(s_XmlColor_Element);
            string col_xmlDecl = GetColorString(s_XmlColor_XmlDecl);
            string col_comm = GetColorString(s_XmlColor_Comment);
            string col_attName = GetColorString(s_XmlColor_Attrib_Name);
            string col_attVal = GetColorString(s_XmlColor_Attrib_Value);
            switch (Node.NodeType)
            {
                case XmlNodeType.Document:
                    foreach (XmlNode childNode in Node.ChildNodes)
                    {
                        Xml2Html(childNode, ref Html, Indent);
                    }
                    break;
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    Html.AppendFormat("<div style=\"color:{0};\">&lt;?{1} {2}?&gt;</div>\n", col_xmlDecl, Node.Name, Node.Value.Replace("\"", "'"));
                    break;
                case XmlNodeType.Element:
                    Html.AppendFormat("<div style=\"color:{0}; margin-left:{1}em;\">\n&lt;{2}", col_elem, Indent, Node.Name); // e.g. "<Object"
                    if (Node.Attributes != null)
                    {
                        foreach (XmlAttribute attrib in Node.Attributes)
                        {
                            // e.g. "name=xyz "
                            string value = System.Net.WebUtility.HtmlEncode(attrib.Value).Replace(" ", "&nbsp;");
                            Html.AppendFormat(" <span style=\"color:{0}\">{1}</span><span style=\"color:black\">=</span><span style=\"color:{2}; font-weight:bold;\">'{3}'</span>", col_attName, attrib.Name, col_attVal, value);
                        }
                    }
                    if (Node.ChildNodes.Count > 0)
                    {
                        Html.Append("&gt;"); // " >"
                        foreach (XmlNode childNode in Node.ChildNodes)
                        {
                            Xml2Html(childNode, ref Html, Indent + 1);
                        }
                        Html.Append("&lt;/" + Node.Name + "&gt;</div>"); // e.g. </Request>"
                    }
                    else
                    {
                        Html.Append(" /&gt;</div>"); // " />"
                    }
                    break;
                case XmlNodeType.Comment: // e.g. "<!-- GetAllProjects -->"
                    Html.AppendFormat("<div style=\"color:{0};\"> &lt;!-- {1} --&gt;</div>" + Environment.NewLine, col_comm, Node.Value.Trim());
                    break;
                case XmlNodeType.Text: // not needed yet
                case XmlNodeType.CDATA:
                    Html.AppendLine(Node.Value);
                    break;
            }
        }

        /// <summary>
        /// Fills a tree view with an xml document
        /// </summary>
        /// <param name="Node">The current xml node or xml document</param>
        /// <param name="TreeView">The current TreeViewItem or TreeView control to be filled</param>
        /// <param name="expanded">If set to true, this will expand all items.</param>
        public static void Xml2TreeView(XmlNode Node, TreeViewItem TreeView, bool expanded)
        {
            // The colors are defined at the top of the class
            SolidColorBrush col_elem = new SolidColorBrush(s_XmlColor_Element);
            SolidColorBrush col_comm = new SolidColorBrush(s_XmlColor_Comment);
            SolidColorBrush col_attName = new SolidColorBrush(s_XmlColor_Attrib_Name);
            SolidColorBrush col_attVal = new SolidColorBrush(s_XmlColor_Attrib_Value);
            if (Node == null || TreeView == null)
            {
                throw new ArgumentNullException("Node");
            }
            foreach (XmlNode childNode in Node.ChildNodes)
            {
                XmlNodeType childType = childNode.NodeType;
                if (childType == XmlNodeType.Element || childType == XmlNodeType.Comment) // Only show elements and comments
                {
                    TreeViewItem childItem = new TreeViewItem();
                    childItem.IsExpanded = expanded;

                    // Allows to use multiple colors in one item
                    TextBlock block = new TextBlock();
                    List<System.Windows.Documents.Run> runs = new List<System.Windows.Documents.Run>();

                    switch (childType)
                    {
                        case XmlNodeType.Comment:
                            runs.Add(new Run(childNode.OuterXml) { Foreground = col_comm });
                            break;
                        case XmlNodeType.Element:
                            runs.Add(new Run("<" + childNode.Name) { Foreground = col_elem });

                            foreach (XmlAttribute attribute in childNode.Attributes)
                            { // Standard colors for xml (taken from Notepad++)
                                runs.Add(new Run(" " + attribute.LocalName) { Foreground = col_attName });
                                runs.Add(new Run("=") { Foreground = Brushes.Black });
                                runs.Add(new Run("'" + attribute.Value + "'") { Foreground = col_attVal });
                            }
                            runs.Add(new Run(">") { Foreground = col_elem });
                            break;
                        default:
                            runs.Add(new System.Windows.Documents.Run(childNode.Name));
                            break;
                    }
                    block.Inlines.AddRange(runs);

                    childItem.Header = block;

                    Xml2TreeView(childNode, childItem, expanded); // Recursively

                    TreeView.Items.Add(childItem);
                }
            }
        }
    }
}
