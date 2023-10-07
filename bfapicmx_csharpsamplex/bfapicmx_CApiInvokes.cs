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
using System.ComponentModel; 
using System.Linq;
using System.Reflection; 
using System.Text;
using System.IO;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Net;
using System.Data;
using System.Windows.Input;
using System.Configuration;

namespace Siemens.Automation.bfapicmx_csharpsamplex
{
    /// <summary>
    /// Initializes and invokes commands on the API.
    /// It is instantiated in the class ViewModelBase.
    /// </summary>
    public class bfapicmx_CApiInvocation
    {

        // Allows access to properties and controls
        MainWindow m_mainwindow;
        InitWindow m_initwindow;
        SubFoldWindow m_subFoldWindow;
        // Definitions of xml for 
        CFunctionParameters m_fcnParam;

        // COM Interface to API
        static object s_apiInstance;

        // User infos for Audit Trail
        //public UserData UserInfo = new UserData();
        List<UserData> users = new List<UserData>();
        //int inter = 0;

        // Types for ISB interface
        // Necessary when using API loader
        static Type s_apiISBType;
        static Type s_apiSISType;
        static Type s_apiCommanMgrType;

        // Connection point for events (OnNotify and OnSISNotify)
        CBFAPICMXConnectionPoint connect;

        // Debug text is used instead of message boxes when quiet=true
        StringBuilder Debug_Text;

        public bfapicmx_CApiInvocation(MainWindow _mainwindow)
        {
            m_mainwindow = _mainwindow;
            m_fcnParam = new CFunctionParameters(m_mainwindow);
        }

        #region P U B L I C  F U N C T I O N S

        public enum CommandType
        {
            Get,
            Set,
            Create,
            Copy,
            Delete,
            Execute
        }

        public object ApiInstance
        {
            get
            {
                return s_apiInstance;
            }
        }

        public Type ApiISBType
        {
            get
            {
                return s_apiISBType;
            }
        }

        public Type ApiSISType
        {
            get
            {
                return s_apiSISType;
            }
        }

        public Type ApiCommanMgrType
        {
            get
            {
                return s_apiCommanMgrType;
            }
        }

        /// <summary>
        /// Invokes a command. The CommandType defines which function in the class FunctionParameters is called.
        /// </summary>
        /// <param name="command">The command to be invoked.</param>
        /// <param name="quiet">If true, there is no output to the GUI and messages are instead written to Debug_Text.</param>
        /// <param name="type"></param>
        public void InvokeCommand(string command, bool quiet, CommandType type)
        {
            bool success = true;

            if (command == "Init")
                success = InitAPI();
            if (command == "CreateMaterial")
            {
                if (m_mainwindow.MatSubFolderChck == true)
                {
                    success = SubFolder();
                }
                else { }
            }
            if (command == "CreateFormulaCat")
            {
                if (m_mainwindow.RecFormSubFolderChck == true)
                {
                    success = SubFolder();
                }
                else { }
            }
            if (command == "CreateOrderCat")
            {
                if (m_mainwindow.OrderBatchSubFolderChck == true)
                {
                    success = SubFolder();
                }
                else { }
            }
            if (command == "CreateRecipe")
            {
                if (m_mainwindow.RecFormSubFolderChck == true)
                {
                    success = SubFolder();
                }
                else { }
            }
            if (command == "CopyRecipe")
            {
                if (m_mainwindow.RecFormSubFolderChck == true)
                {
                    success = SubFolder();
                }
                else { }
            }                 
            Collection<string[]> TagParamCollection = new Collection<string[]>();
            Collection<string[]> AttributeParamCollection = new Collection<string[]>();
            Collection<string[]> ValueParamCollection = new Collection<string[]>();
            string tmpCommand = "";
            List<string> HdlCltnJust4GetParam = new List<string>();
            List<Dictionary<string, string>> ValueCltnJust4GetParam = new List<Dictionary<string, string>>();

            if (success)
            {
                try
                {
                    // In FunctionParameters the commands are split depending on the command type
                    switch (type)
                    {
                        case CommandType.Get:
                            m_fcnParam.Param4GetCommand(command, TagParamCollection, AttributeParamCollection, ValueParamCollection);
                            break;
                        case CommandType.Set:
                            m_fcnParam.Param4SetCommand(command, TagParamCollection, AttributeParamCollection, ValueParamCollection);
                            break;
                        case CommandType.Create:
                            m_fcnParam.Param4CreateCommand(command, TagParamCollection, AttributeParamCollection, ValueParamCollection);
                            break;
                        case CommandType.Copy:
                            m_fcnParam.Param4CopyCommand(command, TagParamCollection, AttributeParamCollection, ValueParamCollection);
                            break;
                        case CommandType.Delete:
                            m_fcnParam.Param4DeleteCommand(command, TagParamCollection, AttributeParamCollection, ValueParamCollection);
                            break;
                        case CommandType.Execute:
                            m_fcnParam.Param4ExecuteCommand(command, TagParamCollection, AttributeParamCollection, ValueParamCollection);
                            break;
                    }
                }
                catch (ArgumentNullException e)
                {
                    if (command == "GetObjectData")
                    {
                        string msg = "Please choose an Archive Type";
                        if (!quiet)
                            MessageBox.Show(msg, "Exception in CApiInvokes.InvokeCommand", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                            Debug_Text.AppendLine("<b>" + command + "</b>: " + msg + "<br />");
                        success = false;
                    }
                    else
                    {
                        string msg = "An exception occurred when trying to get parameters for the command " + command + "\nMessage: " + e.Message;
                        if (!quiet)
                            MessageBox.Show(msg, "Error in CApiInvokes.InvokeCommand", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            Debug_Text.AppendLine("<b>" + command + "</b>: " + msg + "<br />");
                        success = false;
                    }
                }
            }

            if (success)
            {
                string Input = null;
                // Some commands need special handling (e.g. changing command name, because of ambiguous use)
                XmlHandler.HandleSpecialCommands(ref command, TagParamCollection, AttributeParamCollection, ValueParamCollection, ref tmpCommand, m_mainwindow);

                // Special Case
                switch(command)
                {
                    case "GetErrorText":
                        Input = m_mainwindow.ErrorCode;
                        break;
                    case "GetHistoricalSISEvents4Batch":
                        if (tmpCommand == "GetHistoricalSISEventsByFile")
                        {
                            Input = m_mainwindow.Input4HisEventByFile;
                        }
                        else 
                        {
                            // Generate xml
                            Input = XmlHandler.BuildXMLDocument(TagParamCollection, AttributeParamCollection, ValueParamCollection);
                        }
                        break;
                    default:
                            // Generate xml
                            Input = XmlHandler.BuildXMLDocument(TagParamCollection, AttributeParamCollection, ValueParamCollection);
                        break;
                }
                if (s_apiInstance != null) // API initialized?
                {
                    // Show xml on GUI
                    if (command != "GetErrorText") 
                    {
                        XmlHandler.ShowInput(m_mainwindow.UI_WEBINPUT, Input);
                    }

                    object[] parameters = null;
                    string Output = "";
                    try
                    {
                        parameters = InvokeMethod(command, Input);
                    }
                    catch (TargetException e)
                    {
                        Output = e.Message;
                        string msg = "Exception occured while trying to Invoke " + command + ":\n " + e.Message; ;
                        if (!quiet)
                            MessageBox.Show(msg, "Error in CApiInvokes.InvokeCommand", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            Debug_Text.AppendLine("<b>" + command + "</b>: " + msg + "<br />");
                    }
                    if (parameters == null)
                    {
                        // Command was not invoked
                        string msg = "Could not find command " + command + " on interface of IPCS7_SBAPI_XLib.ISB_API or IPCS7_SBAPI_XLib.ISB_SIS!"; ;
                        if (!quiet)
                            MessageBox.Show(msg, "Error in CApiInvokes.InvokeCommand", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            Debug_Text.AppendLine("<b>" + command + "</b>: " + msg + "<br />");
                    }
                    // Special Case
                    if (command == "IsEqualHDL")
                    {
                        int res = (int) parameters[2];
                        switch (res)
                        {
                            case 0:
                                Output = "HDL1 == HDL2";
                                break;
                            case -1:
                                Output = "HDL1 < HDL2";
                                break;
                            case 1:
                                Output = "HDL1 > HDL2";
                                break;
                        }
                    }
                    //Special Case
                    if (command == "GetErrorText") 
                    {
                        Output = (string)parameters[1];
                        m_mainwindow.ErrorTest = Output;
                    }
                    // If error_code stays int.Maxvalue it means
                    int error_code = int.MaxValue;
                    if (command == "ValidateObject")
                    {
                        error_code = (int)parameters[2];
                        Output = (string)parameters[1];
                    }
                    if (parameters != null && parameters.Length == 3)
                    {
                        error_code = (int)parameters[2];
                        Output = parameters[1] as string;
                    }
                    if (error_code == 0)
                    {
                        if (quiet)
                            Debug_Text.AppendLine("<b>" + command + "</b>: succeeded<br />");

                        List<string> HdlCltn = new List<string>();
                        List<Dictionary<string, string>> ValueCltn = new List<Dictionary<string, string>>();

                        if (Output != null && Output != "")
                        {
                            try
                            {
                                // Special Case
                                if (command == "GetErrorText" || command == "ExecuteCommand" || command == "ValidateObject")
                                {
                                    ;
                                }
                                // Fill lists with handles and values from the xml document
                                else
                                {
                                    XmlHandler.ReadXMLDocument(Output, HdlCltn, ValueCltn);
                                }
                            }
                            catch (FileFormatException e)
                            {
                                string msg = "Exception occurred in ApiInvokes.InvokeCommand() while trying to parse response XML:\n" + e.Message; ;
                                if (!quiet)
                                    MessageBox.Show(msg, "Error in CApiInvokes.InvokeCommand", MessageBoxButton.OK, MessageBoxImage.Error);
                                else
                                    Debug_Text.AppendLine("<b>" + command + "</b>: " + msg + "<br />");

                                Output = System.Net.WebUtility.HtmlEncode(Output);
                            }
                        }

                        // Fill controls on GUI with these handles and values
                        // If lists are empty, the controls will be emptied
                        m_mainwindow.FillGUIWithValues(command, HdlCltn, ValueCltn);
                        HdlCltnJust4GetParam = HdlCltn;
                        ValueCltnJust4GetParam = ValueCltn;

                        if (command == "Exit")
                            ExitAPI();
                        
                    }

                    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    if (!quiet)
                    {
                        // Special case
                        if ((command == "GetObjectHeader" || command == "GetObjectData") && (m_mainwindow.ObjectDataHeaderPath != null))
                        {
                            
                            if (Output.StartsWith("<"))
                            {
                                PathInfo ReturnInfo = new PathInfo();
                                ExtractInfo(m_mainwindow.ObjectDataHeaderPath, ReturnInfo);
                                string strFilePath = ReturnInfo.FileDrive + ReturnInfo.FilePath + ReturnInfo.FileNamewExt;
                                if (ReturnInfo.PathIsValid == true)
                                {
                                    WriteToFile(strFilePath, Output, Encoding.Unicode);
                                    if (command == "GetObjectHeader" || command == "GetObjectData")
                                    {
                                        ShowXmlFile(strFilePath);
                                    }
                                    string pathSuc = "Successful written to file <a href='" + strFilePath + "' Target='_blank' >" + strFilePath + "</a>";
                                    XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathSuc, error_code);
                                }
                                else
                                {
                                    string pathFail = " Invalid Path. Writing to file Failed <a href='" + strFilePath + "' Target='_blank' >" + strFilePath + "</a>";
                                    XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathFail, error_code);
                                }
                            }
                            else 
                            {
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                            }
                        }

                        if (command == "GetLogBookObjects")
                        {
                            PathInfo ReturnInfo = new PathInfo();
                            string path = m_mainwindow.UI_TXTDUMP2FILES.Text + "GetLogBookObjects.xml";
                            ExtractInfo(path, ReturnInfo);
                            path = ReturnInfo.FileDrive + ReturnInfo.FilePath + ReturnInfo.FileNamewExt;
                            WriteToFile(path, Output, Encoding.Unicode);
                            if (Output.StartsWith("<"))
                            {
                                if (m_mainwindow.UI_CHCKONLYFILE.IsChecked.Value == false)
                                {
                                    ShowXmlFile(path);
                                }
                                string pathSuc1 = "Successful written to file <a href='" + path + "' Target='_blank' >" + path + "</a>";
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathSuc1, error_code);
                            }
                            else
                            {
                                string pathFail1 = "Writing to file Failed <a href='" + path + "' Target='_blank' >" + path + "</a>";
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathFail1, error_code);
                            }
                        }

                        if (command == "GetHistoricalSISEvents4Batch") 
                        {
                            string HisPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + "\\BFAPICMX_GetHistoricalSISEvents4Batch_Out.xml";
                                if (Output == "")
                                {
                                    XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                                }
                                else
                                {
                                    if (Output.StartsWith("<"))
                                    {
                                        WriteToFile(HisPath, Output, Encoding.Unicode);
                                        ShowXmlFile(HisPath);
                                        string pathSuc2 = "Successful written to file <a href='" + HisPath + "' Target='_blank' >" + HisPath + "</a>";
                                        XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathSuc2, error_code);
                                    }
                                    else
                                    {
                                        if (tmpCommand == "GetHistoricalSISEventsByFile")
                                        {
                                            XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                                        }
                                        else
                                        {
                                            XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                                        }
                                    }
                                }
                        }

                        if (command == "GetHistoricalSISEvents4Unit")
                        {
                            string HisPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + "\\BFAPICMX_GetHistoricalSISEvents4Unit_Out.xml";
                            if (Output == "")
                            {
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                            }
                            else
                            {
                                if (Output.StartsWith("<"))
                                {
                                    WriteToFile(HisPath, Output, Encoding.Unicode);
                                    ShowXmlFile(HisPath);
                                    string pathSuc2 = "Successful written to file <a href='" + HisPath + "' Target='_blank' >" + HisPath + "</a>";
                                    XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathSuc2, error_code);
                                }
                                else
                                {
                                    if (tmpCommand == "GetHistoricalSISEventsByFile")
                                    {
                                        XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                                    }
                                    else
                                    {
                                        XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                                    }
                                }
                            }
                        }

                        if (command == "BackUp")
                        {
                            if ((m_mainwindow.UI_CHCKDUMP2FILES.IsChecked == true) && (m_mainwindow.OutPutFileAndPath != ""))
                            {
                                string filePath = m_mainwindow.UI_TXTDUMP2FILES.Text + "BFAPICMX_" + "BackUp" + ".xml";
                                WriteToFile(filePath, "---." + m_mainwindow.OutPutFileAndPath, Encoding.Unicode);
                                string pathSuc2 = "Successful written to file <a href='" + m_mainwindow.OutPutFileAndPath + "' Target='_blank' >" + m_mainwindow.OutPutFileAndPath + "</a>";
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathSuc2, error_code);
                            }
                            else
                            {
                                string pathSuc2 = "Successful written to file <a href='" + m_mainwindow.OutPutFileAndPath + "' Target='_blank' >" + m_mainwindow.OutPutFileAndPath + "</a>";
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathSuc2, error_code);
                            }
                        }
                        if (command == "PrintObject") 
                        {
                            if (error_code == 0)
                            {
                                string filePath = m_mainwindow.PDFPath;
                                string pathSuc3 = "Successful written to file <a href='" + filePath + "' Target='_blank' >" + filePath + "</a>";
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, pathSuc3, error_code);
                            }
                            else 
                            {
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                            }
                        }
                        if(command == "GetParameter")
                        {
                            if (ValueCltnJust4GetParam.Count == 0 || HdlCltnJust4GetParam.Count == 0 ) 
                            {
                                XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                            }
                            else
                            {
                                FillParams4SetParams();
                            }
                        }
                        if (command == "GetMaterialData")
                        {
                            FillMaterials4SetMaterial();
                        }
                        if (command == "GetAllocations") 
                        {
                            FillPPandScalID();
                        }
                        if (command == "IsEqualHDL" || command == "GetErrorText")
                        {
                            error_code = 0;
                        }

                        if (command != "GetObjectHeader" 
                            && command != "GetObjectData" 
                            && command != "GetLogBookObjects" 
                            && command != "BackUp" 
                            && command != "PrintObject"
                            && command != "GetHistoricalSISEvents4Batch"
                            && command != "GetHistoricalSISEvents4Unit")
                        {
                            XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, command, Output, error_code);
                        }
                        if ((m_mainwindow.UI_CHCKDUMP2FILES.IsChecked == true))
                        {
                            WriteInOutXmlToDump(command, Input, Output);
                        }
                        watch.Stop();
                    }
                }
                else // API not initialized
                {
                    string msg = "API not initialized!"; ;
                    if (!quiet)
                        MessageBox.Show(msg, "Error in CApiInvokes.InvokeCommand", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        Debug_Text.AppendLine("<b>" + command + "</b>: " + msg + "<br />");
                }
            }
        }
        public void WriteInOutXmlToDump(string command, string input, string output)
        {
            bool bDriveExist = true;
            string dumpFile = m_mainwindow.UI_TXTDUMP2FILES.Text;
            if (!dumpFile.EndsWith("\\"))
            {
                dumpFile = dumpFile.Insert(dumpFile.Length, "\\");
            }
            //Create a Directory if it doesn't exist
            if (Directory.Exists(dumpFile) == false)
            {
                try
                {
                        DirectoryInfo di = Directory.CreateDirectory(dumpFile);
                }
                catch (Exception e)
                {
                    bDriveExist = false;
                    string msg = e.Message + ":\n";
                    MessageBox.Show(msg, "Unable to create a Directory", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if ( (!string.IsNullOrEmpty(input)) && ((input.StartsWith("<"))) && bDriveExist )
            {
                   
                string filePathIn = dumpFile + "BFAPICMX_" + command + "_in.xml";
                try
                {
                        WriteToFile(filePathIn, input, Encoding.Unicode);
                }
                catch (Exception e)
                {
                    string msg = e.Message + ":\n";
                    MessageBox.Show(msg, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if ( (!string.IsNullOrEmpty(output)) && (output.StartsWith("<")) && bDriveExist )
            {
                string filePathOut = dumpFile + "BFAPICMX_" + command + "_out.xml";
                try
                {
                        WriteToFile(filePathOut, output, Encoding.Unicode);
                }
                catch (Exception e)
                {
                    string msg = e.Message + ":\n";
                    MessageBox.Show(msg, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            m_mainwindow.Cursor = Cursors.Arrow;
        }
       /* public void WriteInOutXmlToDump(string command, string input, string output)
        {
            bool bDirCreate = false;
            bool bDriveExist = false;
            bool bPath4DumpfileExists = false;
            string temp;
            if ((m_mainwindow.UI_CHCKDUMP2FILES.IsChecked == true))
            {
                string dumpFile = m_mainwindow.UI_TXTDUMP2FILES.Text;
                if (dumpFile.Length >= 3)
                {
                    temp = dumpFile.Substring(0, 3);
                    bDriveExist = this.isDriveExists(temp);
                }
                else
                {
                    bDriveExist = this.isDriveExists(dumpFile);
                }

                bPath4DumpfileExists = Directory.Exists(dumpFile);
                // This drive exist
                if (bDriveExist)
                {
                    // Path does'nt exist
                    if (!bPath4DumpfileExists)
                    {
                        try
                        {
                            //Create a Directory with this path
                            string filePathIn = dumpFile + "BFAPICMX_" + command + "_in.xml";
                            string fileName = "BFAPICMX_" + command + "_in.xml";
                            PathInfo ReturnInfo = new PathInfo();
                            ExtractInfo(filePathIn, ReturnInfo);
                            if (ReturnInfo.PathIsValid || ReturnInfo.FileNamewExt == fileName)
                            {
                                DirectoryInfo di = Directory.CreateDirectory(dumpFile);
                                bDirCreate = di.Exists;
                            }
                        }
                        catch (Exception e)
                        {
                            string msg = e.Message + ":\n";

                            MessageBox.Show(msg, "Unable to create a Directory", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        m_mainwindow.Cursor = Cursors.Arrow;
                    }
                    if ((!string.IsNullOrEmpty(input)) && ((input.StartsWith("<"))))
                    {
                        string filePathIn = dumpFile + "BFAPICMX_" + command + "_in.xml";
                        string tempFilePathIn = filePathIn;
                        PathInfo ReturnInfo = new PathInfo();
                        ExtractInfo(tempFilePathIn, ReturnInfo);
                        if (bDriveExist)
                        {
                            if (ReturnInfo.PathIsValid)
                            {
                                try
                                {
                                    WriteToFile(filePathIn, input, Encoding.Unicode);
                                }
                                catch (Exception e)
                                {
                                    string msg = e.Message + ":\n";
                                    MessageBox.Show(msg, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                m_mainwindow.Cursor = Cursors.Arrow;
                            }
                            else
                            {
                                string fileName = "BFAPICMX_" + command + "_in.xml";
                                if ((ReturnInfo.FilePath == "") && (ReturnInfo.FileNamewExt != fileName))
                                {
                                    string msg = "The File " + fileName + " will be created on " + ReturnInfo.FileDrive + ":\n";
                                    MessageBox.Show(msg, " Wrong Path Format", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    try
                                    {
                                        string test = dumpFile.Substring(0, 3);
                                        string inputFileName = test + fileName;
                                        WriteToFile(inputFileName, input, Encoding.Unicode);
                                    }
                                    catch (Exception e)
                                    {
                                        string msgs = e.Message + ":\n";
                                        MessageBox.Show(msgs, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        WriteToFile(filePathIn, input, Encoding.Unicode);
                                    }
                                    catch (Exception e)
                                    {
                                        string msg = e.Message + ":\n";
                                        MessageBox.Show(msg, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                    m_mainwindow.Cursor = Cursors.Arrow;
                                }
                            }

                        }
                    }
                    if ((!string.IsNullOrEmpty(output)) && (output.StartsWith("<")))
                    {
                        string filePathOut = dumpFile + "BFAPICMX_" + command + "_out.xml";
                        string tempFilePathOut = filePathOut;
                        PathInfo ReturnInfo = new PathInfo();
                        ExtractInfo(tempFilePathOut, ReturnInfo);
                        if (bDriveExist)
                        {
                            if (ReturnInfo.PathIsValid)
                            {
                                try
                                {
                                    WriteToFile(filePathOut, output, Encoding.Unicode);
                                }
                                catch (Exception e)
                                {
                                    string msg = e.Message + ":\n";
                                    MessageBox.Show(msg, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                m_mainwindow.Cursor = Cursors.Arrow;
                            }
                            else
                            {
                                string fileName = "BFAPICMX_" + command + "_out.xml";
                                if ((ReturnInfo.FilePath == "") && (ReturnInfo.FileNamewExt != fileName))
                                {
                                    string msg = "The File " + fileName + " will be created on " + ReturnInfo.FileDrive + ":\n";
                                    MessageBox.Show(msg, " Wrong Path Format", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    try
                                    {
                                        string test = dumpFile.Substring(0, 3);
                                        string inputFileName = test + fileName;
                                        WriteToFile(inputFileName, input, Encoding.Unicode);
                                    }
                                    catch (Exception e)
                                    {
                                        string msgs = e.Message + ":\n";
                                        MessageBox.Show(msgs, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        WriteToFile(filePathOut, input, Encoding.Unicode);
                                    }
                                    catch (Exception e)
                                    {
                                        string msg = e.Message + ":\n";
                                        MessageBox.Show(msg, "Unable to create and write to File", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                    m_mainwindow.Cursor = Cursors.Arrow;
                                }

                            }
                        }
                    }
                }
                else
                {
                    string msg = "Unable to write the dump File" + ":\n" + "Please give a correct drive name ";
                    string.Concat("");
                    MessageBox.Show(msg, "Incorrect drive name", MessageBoxButton.OK, MessageBoxImage.Warning);
                    m_mainwindow.Cursor = Cursors.Arrow;
                }

            }
        }
        */
        /// <summary>
        /// Invokes a command for the Auto command. 
        /// If possible use instead InvokeCommand(string command, bool quiet, CommandType type).
        /// The CommandType is taken from the name.
        /// </summary>
        /// <param name="command">The command to be invoked.</param>
        /// <param name="quiet">If true, there is no output to the GUI and messages are instead written to Debug_Text.</param>
        public void InvokeCommand(string command, bool quiet)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (command.StartsWith("Get"))
                InvokeCommand(command, quiet, CommandType.Get);
            else if (command.StartsWith("Set"))
                InvokeCommand(command, quiet, CommandType.Set);
            else if (command.StartsWith("Init") || command.StartsWith("Advise") || command.StartsWith("UnAdvise"))
                InvokeCommand(command, quiet, CommandType.Execute);
        }

        /// <summary>
        /// Invokes many common functions consecutively.
        /// </summary>
        public void Auto()
        {
            Debug_Text = new StringBuilder();

            m_mainwindow.Cursor = System.Windows.Input.Cursors.Wait;
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(new Action(() => { }), System.Windows.Threading.DispatcherPriority.Background, null);
            try
            {
                InvokeCommand("Init", true);
                InvokeCommand("GetAllProjects", true);
                InvokeCommand("GetAllPCells4Project", true);
                InvokeCommand("SetCurrentUser", true);
                InvokeCommand("Advise", true);
                InvokeCommand("UnAdvise", true);
                InvokeCommand("GetAllUnitClasses4PCell", true);
                InvokeCommand("GetAllUnits4UnitClass", true);
                InvokeCommand("GetAllDataTypes4PCell", true);
                InvokeCommand("GetAllPhysicalUnits4PCell", true);
                InvokeCommand("GetAllPhysicalUnits4PCell", true);
                InvokeCommand("GetAllLIB4PCell", true);
                InvokeCommand("GetAllMR4PCell", true);
                InvokeCommand("GetFormulaCat4MR", true);
                InvokeCommand("GetAllFormulas4FormulaCat", true);
                InvokeCommand("GetMR4Formula", true);
                InvokeCommand("GetProductData4Formula", true);
                InvokeCommand("GetAllOrderCats4PCell", true);
                InvokeCommand("GetAllOrders4OrderCat", true);
                InvokeCommand("GetAllBatches4Order", true);
                InvokeCommand("GetAllFormulasOrMR4CreateBatch", true);
                InvokeCommand("GetBatchState", true);
                InvokeCommand("GetBatchStartData", true);
                InvokeCommand("GetAllMaterials4PCell", true);
                InvokeCommand("GetAllOrders4OrderCat", true);
                InvokeCommand("GetAllQualities4Material", true);
                InvokeCommand("GetMaterialData", true);
                InvokeCommand("GetQualityData", true);
                InvokeCommand("GetAllSubfolders4PCell", true);
            }
            catch (NotImplementedException e)
            {
                MessageBox.Show("APIInvokes.Auto() failed: " + e.Message);
            }
            m_mainwindow.Cursor = System.Windows.Input.Cursors.Arrow;

            XmlHandler.ShowOutput(m_mainwindow.UI_WEBOUTPUT, m_mainwindow.UI_WEBOUTPUTTREE, "Auto", Debug_Text.ToString(), 0);
        }

        #endregion

        #region P R I V A T E  F U N C T I O N S
        
        private bool SubFolder()
        {
            bool Continue = false;
            m_subFoldWindow = new SubFoldWindow(m_mainwindow);
            Continue = (bool)m_subFoldWindow.ShowDialog();
            return Continue;
        }

        /// <summary>
        /// Invokes a function on the API using the parameter command as method name.
        /// </summary>
        /// <param name="command">The name of the function to invoke.</param>
        /// <param name="input">The xml to be passed to the API.</param>
        /// <returns>Returns an array containing three elements: string(input), string(output), int(error code).</returns>
        private object[] InvokeMethod(string command, string input)
        {
            object[] Parameters = null;

            // Search for method using command as name
            MethodInfo methodinfo = s_apiInstance.GetType().GetMethod(command);

            // When using API loader 
            if (methodinfo == null)
            {
                methodinfo = s_apiISBType.GetMethod(command);
            }

            if (methodinfo == null)
            {
                methodinfo = s_apiSISType.GetMethod(command);
            }

            if (methodinfo == null)
            {
                methodinfo = s_apiCommanMgrType.GetMethod(command);
            }

            if (methodinfo != null)
            {
                // Get signature of method
                ParameterInfo[] parameterinfo = methodinfo.GetParameters();
                
                if (parameterinfo.Length == 0) // No parameters needed
                {
                    methodinfo.Invoke(s_apiInstance, null);
                }
                else // Standard signature: string, out string, out int
                {
                    if (command == "IsEqualHDL")
                    {
                        Parameters = new object[] { m_mainwindow.Hdl1, m_mainwindow.Hdl2, 0, 0 };
                    }
                    if (command == "GetErrorText")
                    {
                        int InputErrorCode = int.Parse(m_mainwindow.ErrorCode);
                        Parameters = new object[] { InputErrorCode, "", 0 };
                        
                    }
                    if (command == "ValidateObject")
                    {
                        int error = 0;
                        int warn = 0;
                        Parameters = new object[] { input, "", 0, error, warn};
                    }
                    if (command != "IsEqualHDL" && command != "GetErrorText" && command != "ValidateObject")
                    {
                        Parameters = new object[] { input, "", 0 };
                    }
                    // Invoke function of API with correct parameters
                    methodinfo.Invoke(s_apiInstance, Parameters);
                }
            }
            return Parameters;
        }

        /// <summary>
        /// Invokes a method multiple times.
        /// </summary>
        /// <param name="command">The command to be executed.</param>
        /// <param name="input">The input xml to be sent to the API.</param>
        private void InvokeMethodMultiple(string command, string input, int counter)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //int counter = m_mainwindow.Counter;
            for (int i = 0; i < counter; i++)
            {
                InvokeMethod(command, input);
                if (watch.ElapsedMilliseconds > 2000)
                {
                    m_mainwindow.Counter = counter - i;
                    Application.Current.Dispatcher.Invoke(new Action(delegate { }), System.Windows.Threading.DispatcherPriority.Background, null);

                    watch.Restart();
                }
            }
            if (m_mainwindow.Counter != counter) // For the animation
                m_mainwindow.Counter = counter;
        }

        /// <summary>
        /// Initialize the API.
        /// </summary>
        /// <returns>Returns true if successful.</returns>
        private bool InitAPI()
        {
            Guid eventID = typeof(IPCS7_SBAPI_XLib._ISB_APIDispatchEvents).GUID;
            // API was already initialised.. just call the command
            if ( s_apiInstance != null)
            {
                return true;
            } 
            bool Continue = false;
            m_initwindow = new InitWindow();
            if (true == m_mainwindow.ChooseAPIVersionChck)
            {
                // Show init window
                Continue = (bool)m_initwindow.ShowDialog();
            }
            else 
            {
                Continue = true;
            }

            if (Continue)
            {
                string version = m_initwindow.Version;
                bool loader = m_initwindow.Loader;
                Type type = null;
                bool remoter = m_initwindow.Remote;
 //               Guid eventID = typeof(IPCS7_SBAPI_XLib._ISB_APIDispatchEvents).GUID; ;
                try
                {
                    // Get the types of the API interfaces to initialize it depending on the version to use
                    int version_num;
                    string strProgId = null;
                    Guid clID = Guid.NewGuid();
                    bool isParse = int.TryParse(version, out version_num);
                    if (isParse)
                    {
                        switch (version_num)
                        {
                            case 613:
                                strProgId = loader ? "BFApiLoader.BFApiLdr.1" : "BFAPICMX.SB_API.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_APIDispatchEvents).GUID;
                                break;
                            case 701:
                                strProgId = loader ? "BFApiLoader.BFApiLdr701HF14.1" : "BFAPICMX.SB_API_701HF14.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_701HF14);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_APIDispatchEvents).GUID;
                                break;
                            case 707:
                                strProgId = loader ? "BFApiLoader.BFApiLdr707.1" : "BFAPICMX.SB_API_707.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_707);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS_707);
                                s_apiCommanMgrType = typeof(IPCS7_SBAPI_XLib.ISB_COMMAND_707);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_API_707_DispatchEvents).GUID;
                                break;
                            case 712:
                                strProgId = loader ? "BFApiLoader.BFApiLdr712.1" : "BFAPICMX.SB_API_712.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_712);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS_712);
                                s_apiCommanMgrType = typeof(IPCS7_SBAPI_XLib.ISB_COMMAND_712);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_API_712_DispatchEvents).GUID;
                                break;
                            case 800:
                                strProgId = loader ? "BFApiLoader.BFApiLdr800.1" : "BFAPICMX.SB_API_800.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_800);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS_712);
                                s_apiCommanMgrType = typeof(IPCS7_SBAPI_XLib.ISB_COMMAND_712);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_API_712_DispatchEvents).GUID;
                                break;
                            case 801:
                                strProgId = loader ? "BFApiLoader.BFApiLdr801.1" : "BFAPICMX.SB_API_801.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_800);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS_712);
                                s_apiCommanMgrType = typeof(IPCS7_SBAPI_XLib.ISB_COMMAND_712);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_API_712_DispatchEvents).GUID;
                                break;
                            case 810:
                                strProgId = loader ? "BFApiLoader.BFApiLdr810.1" : "BFAPICMX.SB_API_810.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_800);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS_712);
                                s_apiCommanMgrType = typeof(IPCS7_SBAPI_XLib.ISB_COMMAND_712);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_API_712_DispatchEvents).GUID;
                                break;
                            case 811:
                                strProgId = loader ? "BFApiLoader.BFApiLdr811.1" : "BFAPICMX.SB_API_811.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_800);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS_712);
                                s_apiCommanMgrType = typeof(IPCS7_SBAPI_XLib.ISB_COMMAND_712);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_API_712_DispatchEvents).GUID;
                                break;
                            case 820:
                                strProgId = loader ? "BFApiLoader.BFApiLdr820.1" : "BFAPICMX.SB_API_820.1";
                                s_apiISBType = typeof(IPCS7_SBAPI_XLib.ISB_API_800);
                                s_apiSISType = typeof(IPCS7_SBAPI_XLib.ISB_SIS_712);
                                s_apiCommanMgrType = typeof(IPCS7_SBAPI_XLib.ISB_COMMAND_712);
                                eventID = typeof(IPCS7_SBAPI_XLib._ISB_API_712_DispatchEvents).GUID;
                                break;
                            default:
                                throw new NotImplementedException("Version " + version + " is not exists in SBAPI!");
                        }
                        if (string.IsNullOrEmpty( m_mainwindow.ComputerName))
                        {
                            type = System.Type.GetTypeFromProgID(strProgId);
                        }
                        else
                        {
                            type = System.Type.GetTypeFromProgID(strProgId, m_mainwindow.ComputerName, true);
                        }
                        try
                        {
                            s_apiInstance = Activator.CreateInstance(type); 
                            // Connect to API events
                            connect = new CBFAPICMXConnectionPoint(m_mainwindow.Notification, s_apiInstance, eventID, m_mainwindow);
                            m_mainwindow.UI_APIVERSION.Text = m_initwindow.ApiVersion;
                        }
                        catch ( COMException e)
                        {
                            string msg = "Please enter a valid Computername " + "\nMessage: " + e.Message;
                            MessageBox.Show(msg, "Error in CApiInvokes.InvokeCommand", MessageBoxButton.OK, MessageBoxImage.Error);
                            Continue = false;
                        }
                    }
                    else 
                    {
                        throw new ArgumentException("Tryparse of version failed");
                    }
                }
                catch(EntryPointNotFoundException e)
                {
                    MessageBox.Show("Couldn't access the API!", e.Message);
                    Continue = false;
                }
            }

            return Continue;
        }

        /// <summary>
        /// Exits the API and disposes the COM objects.
        /// </summary>
        public void ExitAPI()
        {
            if (s_apiInstance != null)
            {
                AddUpdateAppSettings("Username", m_mainwindow.UI_TXTUSERNAME.Text);
                AddUpdateAppSettings("Password", m_mainwindow.Password);
                AddUpdateAppSettings("Domain", m_mainwindow.UI_TXTDOMAIN.Text);
                AddUpdateAppSettings("Application", m_mainwindow.UI_TXTAPPLICATION.Text);
                AddUpdateAppSettings("Computername", m_mainwindow.UI_TXTCOMPUTER.Text);
                AddUpdateAppSettings("Dumpfile", m_mainwindow.DumpFile);
                AddUpdateAppSettings("Batchml", m_mainwindow.BatchML);
                AddUpdateAppSettings("Manymaterials", m_mainwindow.MaterialsPath);
                AddUpdateAppSettings("ObjectdataHeader", m_mainwindow.ObjectDataHeaderPath);
                AddUpdateAppSettings("Backupfile", m_mainwindow.UI_TXTOUTPUTPATHANDFILE.Text);
                AddUpdateAppSettings("Checkdump", ( m_mainwindow.UI_CHCKDUMP2FILES.IsChecked).ToString() );
                AddUpdateAppSettings("Checkbatchml", (m_mainwindow.UI_CHKBatchML.IsChecked).ToString());
                AddUpdateAppSettings("Checkformatoutput", (m_mainwindow.UI_CHCKFORMATOUTPUT.IsChecked).ToString());
                AddUpdateAppSettings("Checklognotication", (m_mainwindow.UI_CHCKLOGNOTIFICATION.IsChecked).ToString());
                AddUpdateAppSettings("Checkmsgnotification", (m_mainwindow.UI_CHCKMSGNOTIFICATION.IsChecked).ToString());
                AddUpdateAppSettings("Checktreenotification", (m_mainwindow.UI_CHCKTREENOTIFICATION.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonall", (m_mainwindow.UI_CHECHKBOXALL.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonname", (m_mainwindow.UI_CHECHKNAME.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonversion", (m_mainwindow.UI_CHECHKVERSION.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonparent", (m_mainwindow.UI_CHECHKPARENT.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttondbid", (m_mainwindow.UI_CHECHKDBID.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttondesc", (m_mainwindow.UI_CHECHKDESC.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttoncode", (m_mainwindow.UI_CHECHKCODE.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonproduct", (m_mainwindow.UI_CHECHKPRODUCT.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonsubfhdl", (m_mainwindow.UI_CHECHSUBFHDL.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonusage", (m_mainwindow.UI_CHECHUSAGE.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonstate", (m_mainwindow.UI_CHECHSTATE.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonexstate", (m_mainwindow.UI_CHECHEXSTATE.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttontype", (m_mainwindow.UI_CHECHTYPE.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonsize", (m_mainwindow.UI_CHECHSIZE.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonlo", (m_mainwindow.UI_CHECHLO.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonhi", (m_mainwindow.UI_CHECHHI.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonguid", (m_mainwindow.UI_CHECHGUID.IsChecked).ToString());
                AddUpdateAppSettings("Togglebuttonuom", (m_mainwindow.UI_CHECHUOM.IsChecked).ToString());

                m_mainwindow.Reset();
                connect.Dispose();
                Marshal.FinalReleaseComObject(s_apiInstance);
                connect = null;
                s_apiInstance = null;
            }
        }
        
        //Helpmethods 4 GetObjectData and GetObjectHeader
        public void WriteToFile(string filepath, string text, Encoding encoding)
        {
                File.WriteAllText(filepath, text, encoding);
        }

        public static void ExtractInfo(string input, PathInfo ReturnInfo)
        {
            int intCurrentPos;
            int intFileNamePos;
            string strSource;

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            else 
            {
                strSource = input;
            }
            if (ReturnInfo == null)
            {
                throw new ArgumentNullException("ReturnInfo");
            }
            ReturnInfo.FileNamewExt = "";
            ReturnInfo.FileNamewoExt = "";
            ReturnInfo.FilePath = "";
            ReturnInfo.FileDrive = "";
            ReturnInfo.FileExt = "";
            ReturnInfo.FileIsValid = false;
            ReturnInfo.PathIsValid = false;
            ReturnInfo.DriveIsValid = false;

            if (strSource != "")
            {
                strSource.Replace("/", "\\");
                intCurrentPos = strSource.IndexOf(":", 0, strSource.Length, StringComparison.CurrentCulture) + 1;
                if (intCurrentPos > 1)
                {
                    ReturnInfo.FileDrive = strSource.Substring(0, intCurrentPos);
                    strSource = strSource.Substring(intCurrentPos);
                }

                intFileNamePos = strSource.LastIndexOf("\\", strSource.Length);

                if (strSource.Length - intFileNamePos > 0)
                {
                    int length = strSource.Length - intFileNamePos + 1;
                    ReturnInfo.FileNamewExt = strSource.Substring(intFileNamePos + 1);

                    intCurrentPos = ReturnInfo.FileNamewExt.IndexOf(".", 0, ReturnInfo.FileNamewExt.Length, StringComparison.CurrentCulture) + 1;
                    if (intCurrentPos != 1)
                    {
                        ReturnInfo.FileNamewoExt = ReturnInfo.FileNamewExt.Substring(0, intCurrentPos - 1);
                        ReturnInfo.FileExt = ReturnInfo.FileNamewExt.Substring(intCurrentPos);
                    }

                    strSource = strSource.Substring(0, intFileNamePos);
                }

                if (strSource.Length > 0)
                {
                    ReturnInfo.FilePath = strSource;

                    intCurrentPos = ReturnInfo.FilePath.IndexOf("\\", 0, ReturnInfo.FilePath.Length, StringComparison.CurrentCulture);
                    if (intCurrentPos != 0)
                    {
                        ReturnInfo.FilePath = "\\" + ReturnInfo.FilePath;
                    }
                    intCurrentPos = ReturnInfo.FilePath.LastIndexOf("\\", ReturnInfo.FilePath.Length, StringComparison.CurrentCulture);
                    if (ReturnInfo.FilePath.Length - intCurrentPos > 0)
                    {
                        ReturnInfo.FilePath = ReturnInfo.FilePath + "\\";
                    }

                    if (Directory.Exists(ReturnInfo.FileDrive))
                        ReturnInfo.DriveIsValid = true;
                    else
                        ReturnInfo.DriveIsValid = false;

                    if (Directory.Exists(ReturnInfo.FileDrive + ReturnInfo.FilePath))
                        ReturnInfo.PathIsValid = true;
                    else
                        ReturnInfo.PathIsValid = false;
                }

                if (File.Exists(ReturnInfo.FileDrive + ReturnInfo.FilePath + ReturnInfo.FileNamewExt))
                {
                    FileAttributes fa = File.GetAttributes(ReturnInfo.FileDrive + ReturnInfo.FilePath + ReturnInfo.FileNamewExt);
                    if ((fa & FileAttributes.Archive & FileAttributes.Hidden & FileAttributes.Normal & FileAttributes.ReadOnly & FileAttributes.System) != FileAttributes.Directory)
                        ReturnInfo.FileIsValid = true;
                    else
                        ReturnInfo.FileIsValid = false;
                }
            }

        }

        // Helpmethods to Open a Dialog ans show file
        public void ShowXmlFile(string strFileName)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = strFileName;
            p.Start();
        }

        // Helpmethods To Fill Params 4 SetParameter
        void FillParams4SetParams()
        {
            string strGetParameter;
            string strTemp1;

            strGetParameter = m_mainwindow.UI_CBPARAMETER.SelectedItem.ToString();
            if (strGetParameter != "")
            {
                m_mainwindow.UI_txtSet.Text = GetValueFromNamedItem(strGetParameter, XmlParam.BF_API_XML_PARAMETERTYPE);
                m_mainwindow.UI_txtUsg.Text = GetValueFromNamedItem(strGetParameter, XmlParam.BF_API_XML_PARAMETER_USAGE);
                m_mainwindow.UI_txtID.Text = GetValueFromNamedItem(strGetParameter, XmlParam.BF_API_XML_DB_ID);
                m_mainwindow.UI_txtDtId.Text = GetValueFromNamedItem(strGetParameter, XmlParam.BF_API_XML_DATATYPE_ID);

                strTemp1 = XmlParam.BF_API_XML_VALUE;
                if (m_mainwindow.UI_txtDtId.Text == "8" || m_mainwindow.UI_txtDtId.Text == "9" || m_mainwindow.UI_txtDtId.Text == "10")
                {
                    strTemp1 = XmlParam.BF_API_XML_LOCATION_ID;
                }

                m_mainwindow.UI_txtValue.Text = GetValueFromNamedItem(strGetParameter, strTemp1);
                m_mainwindow.UI_txtPhUnit.Text = GetValueFromNamedItem(strGetParameter, XmlParam.BF_API_XML_PHYSICALUNIT_ID);
                m_mainwindow.UI_txtMatHdl.Text = GetValueFromNamedItem(strGetParameter, XmlParam.BF_API_XML_HDL);
            }
            m_mainwindow.UI_CBPARAMETER.ToolTip = m_mainwindow.UI_CBPARAMETER.Text;

        }

        // Helpmethods To Fill Materials 4 SetMaterialData
        void FillMaterials4SetMaterial()
        {
            string strGetMaterials;

            strGetMaterials = m_mainwindow.UI_CBMATERIALDATA.Text;
            if (strGetMaterials != "")
            {
                m_mainwindow.UI_TXTMATNAME.Text = GetValueFromNamedItem(strGetMaterials, XmlParam.BF_API_XML_NAME);
                m_mainwindow.UI_TXTMATDESC.Text = GetValueFromNamedItem(strGetMaterials, XmlParam.BF_API_XML_DESCRIPTION);
                m_mainwindow.UI_TXTMATCODE.Text = GetValueFromNamedItem(strGetMaterials, XmlParam.BF_API_XML_CODE);
                m_mainwindow.UI_txtDtId.Text = GetValueFromNamedItem(strGetMaterials, XmlParam.BF_API_XML_DATATYPE_ID);
            }
        }
        public void FillPPandScalID()
        {
            string strGetAllocation;

            strGetAllocation = m_mainwindow.UI_CBTRP.Text;
            if (strGetAllocation != "")
            {
                m_mainwindow.UI_PROCESS_PARAM_ID.Text = GetValueFromNamedItem(strGetAllocation, XmlParam.BF_API_XML_PARAMETER_ID);
                m_mainwindow.UI_STRATEGYTYPE.Text = GetValueFromNamedItem(strGetAllocation, XmlParam.BF_API_XML_STRATEGYTYPE_ID);
            }
        }
   
        public string GetValueFromNamedItem(string sExpression, string sNamedItem)
        {
            int iIndex, iIndex2;
            string strItem, strChar;

            strItem = sNamedItem + "=";
           
            if (sExpression == null) 
            {
                throw new ArgumentNullException("sExpression");
            }
            
            iIndex = sExpression.IndexOf(strItem, 0);

            if (iIndex == -1)
                return "";

            iIndex = sExpression.IndexOf("=", iIndex);
            iIndex = iIndex + 1;
            strChar = sExpression.Substring(0, iIndex);
            iIndex2 = sExpression.IndexOf(",", iIndex);

            return sExpression.Substring(iIndex, iIndex2 - iIndex);
        }

        public bool CheckUserInfo()
        {
            Boolean bChecked;
            bChecked = true;
            int nIndex;
            int ItemCount = m_mainwindow.UI_LSTUSERINFO.Items.Count;
            for (nIndex = 0; nIndex < ItemCount; nIndex++)
            {
                if (m_mainwindow.UI_TXTAUDITUSERNAME.Text != null)
                {
                    if (ItemCount != 0)
                    {
                        if (users[nIndex].UserName == m_mainwindow.UI_TXTAUDITUSERNAME.Text)
                        {
                            if (users[nIndex].LongName == m_mainwindow.UI_TXTAUDITLONGNAME.Text)
                            {
                                if (m_mainwindow.UI_TXTAUDITCOMPUTERNAME.Text != "")
                                {
                                    if (users[nIndex].Computer == m_mainwindow.UI_TXTAUDITCOMPUTERNAME.Text)
                                    {
                                        bChecked = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return bChecked;
        }

        //Add new Users
        public void AddUserInfo()
        {
            m_mainwindow.InitializeComponent();

            if (CheckUserInfo() == true)
            {
                int numberOfUsers = m_mainwindow.UI_LSTUSERINFO.Items.Count;
                string audit_username = m_mainwindow.UI_TXTAUDITUSERNAME.Text;
                string audit_longname = m_mainwindow.UI_TXTAUDITLONGNAME.Text;
                string audit_computer = m_mainwindow.UI_TXTAUDITCOMPUTERNAME.Text;
                for (int index = 0; index <= numberOfUsers; ++index)
                {
                    if (index == numberOfUsers)
                    {
                        CheckBox chk = new CheckBox();
                        UserData user = new UserData()
                        {
                            UserName = audit_username,
                            LongName = audit_longname,
                            Computer = audit_computer,
                            MyCheck = chk
                        };
                        // Add user
                        users.Insert(index, user);
                        m_mainwindow.UI_LSTUSERINFO.Items.Add(user);
                    }
                }
                numberOfUsers = users.Count;
                m_mainwindow.UI_TXTAUDITUSERNAME.Text = "";
                m_mainwindow.UI_TXTAUDITLONGNAME.Text = "";
                m_mainwindow.UI_TXTAUDITCOMPUTERNAME.Text = "";
                    // show the Users
                m_mainwindow.UI_LSTUSERINFO.ItemsSource = users;
            }
            else 
            {
                MessageBox.Show("This User was already added", "Please add another User", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool isDriveExists(string path) 
        {
            return DriveInfo.GetDrives().Any(x =>x.Name == path);
        }

       // Read all Settings from config File bevor loading the main Window
       public void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        switch (key)
                        {
                            case "Username":
                                m_mainwindow.UserName = appSettings[key];
                                break;
                            case "Password":
                                m_mainwindow.Password = appSettings[key];
                                break;
                            case "Domain":
                                m_mainwindow.Domain = appSettings[key];
                                break;
                            case "Application":
                                m_mainwindow.Application = appSettings[key];
                                break;
                            case "Computername":
                                m_mainwindow.ComputerName = appSettings[key];
                                break;
                            case "Dumpfile":
                                m_mainwindow.DumpFile = appSettings[key];
                                break;
                            case "Batchml":
                                m_mainwindow.BatchML = appSettings[key];
                                break;
                            case "Manymaterials":
                                m_mainwindow.MaterialsPath = appSettings[key];
                                break;
                            case "ObjectdataHeader":
                                m_mainwindow.ObjectDataHeaderPath = appSettings[key];
                                break;
                            case "Backupfile":
                                m_mainwindow.UI_TXTOUTPUTPATHANDFILE.Text = appSettings[key];
                                break;
                            case "Checkdump":
                                m_mainwindow.UI_CHCKDUMP2FILES.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Checkbatchml":
                                m_mainwindow.UI_CHKBatchML.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Checkformatoutput":
                                m_mainwindow.UI_CHCKFORMATOUTPUT.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Checklognotication":
                                m_mainwindow.UI_CHCKLOGNOTIFICATION.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Checkmsgnotification":
                                m_mainwindow.UI_CHCKMSGNOTIFICATION.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Checktreenotification":
                                m_mainwindow.UI_CHCKTREENOTIFICATION.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonall":
                                m_mainwindow.UI_CHECHKBOXALL.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonname":
                                m_mainwindow.UI_CHECHKNAME.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonversion":
                                m_mainwindow.UI_CHECHKVERSION.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonparent":
                                m_mainwindow.UI_CHECHKPARENT.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttondbid":
                                m_mainwindow.UI_CHECHKDBID.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttondesc":
                                m_mainwindow.UI_CHECHKBOXALL.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttoncode":
                                m_mainwindow.UI_CHECHKDESC.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonproduct":
                                m_mainwindow.UI_CHECHKPRODUCT.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonsubfhdl":
                                m_mainwindow.UI_CHECHSUBFHDL.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonusage":
                                m_mainwindow.UI_CHECHUSAGE.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonstate":
                                m_mainwindow.UI_CHECHSTATE.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonexstate":
                                m_mainwindow.UI_CHECHEXSTATE.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttontype":
                                m_mainwindow.UI_CHECHTYPE.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonsize":
                                m_mainwindow.UI_CHECHSIZE.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonlo":
                                m_mainwindow.UI_CHECHLO.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonhi":
                                m_mainwindow.UI_CHECHHI.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonguid":
                                m_mainwindow.UI_CHECHGUID.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                            case "Togglebuttonuom":
                                m_mainwindow.UI_CHECHUOM.IsChecked = System.Convert.ToBoolean(appSettings[key]);
                                break;
                        }
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        //Add and update the config File with new settings bevor exit the application
       public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
      
        #endregion
    }

   

    // Just for GetObjectData and GetObjectHeader
    public class PathInfo
    {
        public string FileDrive { get; set; }
        public string FilePath { get; set; }
        public string FileNamewExt { get; set; }
        public string FileNamewoExt { get; set; }
        public string FileExt { get; set; }
        public bool FileIsValid { get; set; }
        public bool PathIsValid { get; set; }
        public bool DriveIsValid { get; set; }
    }

    // Hilfmethods for Audit Trail
    public class UserData : ObservableCollection<string>
    {
        public string UserName { get; set; }
        public string LongName { get; set; }
        public string Computer { get; set; }
        public CheckBox MyCheck{ get; set; }
    }
}

