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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System.Xml;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.Text.RegularExpressions;

namespace Siemens.Automation.bfapicmx_csharpsamplex
{

    /// <summary>
    /// Connection point to API events
    /// Implements 3 interfaces for different versions of API
    /// </summary>
    public class CBFAPICMXConnectionPoint : IPCS7_SBAPI_XLib._ISB_APIDispatchEvents, IPCS7_SBAPI_XLib._ISB_API_707_DispatchEvents, IPCS7_SBAPI_XLib._ISB_API_712_DispatchEvents, IDisposable
    {
        TreeView m_notificationControl;

        IConnectionPoint m_icp;
        IConnectionPointContainer m_icpc;

        MainWindow m_mainwindow;

        // The cookie is returned when executing Advise and is needed when executing Unadvise
        int cookie;

        public CBFAPICMXConnectionPoint(TreeView notificationControl, object apiInstance, Guid eventType, MainWindow mainwindow)
        {
            m_notificationControl = notificationControl;
            //this.invokes = _invokes;

            m_icpc = (IConnectionPointContainer)apiInstance;

            m_icpc.FindConnectionPoint(ref eventType, out m_icp);

            m_icp.Advise(this, out cookie);

            this.m_mainwindow = mainwindow;
        }

        /// <summary>
        /// Needs a destructor to remove connection to API correctly
        /// </summary>
        ~CBFAPICMXConnectionPoint()
        {
            Dispose();
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_icp != null)
                {
                    try
                    {
                        m_icp.Unadvise(cookie);
                        m_icp = null;
                    }
                    catch (ObjectDisposedException e)
                    {
                        System.Windows.MessageBox.Show("Exception occured in Dispose():\n" + e.Message, "Exception in ConnectionPoint");
                    }
                }
            }
        }

        /* Example for an event:
        SessionNotifyCounter: 1
        EventId: 
        Timestamp: 2013-09-02T08:45:19.582Z
        OT: PCELL
        NT: SERVER_UP
        Project: 1/0/SB8_2-10-12724223
        PCell: 
        Parent: 
        Object: 
        <?xml 
         */

        /// <summary>
        /// Invoked by event handlers
        /// </summary>
        /// <param name="values">A list containing all arguments (see example above)</param>
        void OnNotify(List<KeyValuePair<string, string>> values)
        {
            m_notificationControl.Dispatcher.BeginInvoke(new FillTreeViewDelegate(FillTreeView), values);
        }

        void OnSISNotify(List<KeyValuePair<string, string>> values)
        {
            m_notificationControl.Dispatcher.BeginInvoke(new FillTreeViewDelegate(FillTreeViewSiS), values);
        }
        /// <summary>
        /// Invoked on window thread to be able to change treeview control
        /// </summary>
        /// <param name="values">A list containing all arguments (see example above)</param>
        delegate void FillTreeViewDelegate(List<KeyValuePair<string, string>> values);

        void FillTreeView(List<KeyValuePair<string, string>> values)
        {
            try
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = "Unnamed Event";
                string time = "";
                string obj = "";
                string type = "";
                string timeStamp = "";
                string timeStampOnly = "";
                string objType = "";
                string notType = "";
                string project = "";
                string preFix = "";
                string temp = "";
                string outPut4LogFile = "";
                string evtIdValue = "";
                string tmpMsgText = "";
                string tmpMsgText2 = "";
                string tmpMsgText3 = "";
                string msgText = "";
                foreach (KeyValuePair<string, string> value in values)
                {
                    if (value.Value != null && value.Value != "")
                    {
                        TreeViewItem sub_item = new TreeViewItem();
                        sub_item.Header = value.Key + ": " + value.Value;
                        preFix = value.Key + ": ";
                        switch (value.Key)
                        {
                            case "TimeStamp":
                                time = new System.Text.RegularExpressions.Regex(".*?T([^.]+)\\..*").Replace(value.Value, "$1");
                                item.Header = value.Value;
                                timeStampOnly = value.Value;
                                timeStamp = preFix + value.Value ;
                                break;
                            case "BF_API_OBJECT_TYPE":
                                if (value.Value.StartsWith("BF_API_OBJECT_"))
                                    obj = value.Value.Substring("BF_API_OBJECT_".Length);
                                    objType = preFix + value.Value;
                                break;
                            case "BF_API_NOTIFY_TYPE":
                                if (value.Value.StartsWith("BF_API_NOTIFY_"))
                                    type = value.Value.Substring("BF_API_NOTIFY_".Length);
                                    notType = preFix + value.Value;

                                if (value.Value == "BF_API_NOTIFY_SERVER_DOWN")
                                    sub_item.Foreground = new SolidColorBrush(Colors.Red);
                                else if (value.Value == "BF_API_NOTIFY_SERVER_UP")
                                    sub_item.Foreground = new SolidColorBrush(Colors.Green);
                                break;
                            case "Project":
                                project = preFix + value.Value;
                                break;
                            case "Change":
                                try
                                {
                                    XmlDocument doc = new XmlDocument();
                                    doc.LoadXml(value.Value);
                                    XmlHandler.Xml2TreeView(doc, sub_item, false);

                                    sub_item.Header = "Change:";
                                    if (true == value.Value.Contains( evtIdValue ))
                                    {
                                        if (!string.IsNullOrEmpty(evtIdValue))
                                        {
                                            int end = value.Value.LastIndexOf("-->");
                                            int start = evtIdValue.IndexOf("!--");
                                            if (end >= 0)
                                            {
                                                tmpMsgText = value.Value.Substring(start, end + 3);
                                                int begin = tmpMsgText.IndexOf("?");
                                                int final = tmpMsgText.IndexOf("<", begin + 1 );
                                                tmpMsgText2 = tmpMsgText.Remove(begin, final - begin);
                                                int first = tmpMsgText2.IndexOf("<");
                                                int last = tmpMsgText2.IndexOf("<", first + 1);
                                                tmpMsgText3 = tmpMsgText2.Remove(first, last - first);
                                                int debut = tmpMsgText3.IndexOf("<");
                                                int fin = tmpMsgText3.IndexOf("<", debut + 1);
                                                msgText = tmpMsgText3.Remove(debut, fin - debut);
                                                //msgText = tmpMsgText2.Remove(first, last - first);
                                            }
                                        }
                                    }
                                }
                                catch (FormatException e) 
                                { 
                                }
                                break;
                        }
                        item.Items.Add(sub_item);
                        temp = value.Value;
                        if (value.Key == "EventId") 
                        {
                            evtIdValue = "<!--"+" "+ temp;
                        }
                    }
                }

                item.Header = timeStampOnly +" " + msgText;
                outPut4LogFile = time + Environment.NewLine + 
                                 "OBJECT:" + obj + Environment.NewLine +
                                 "NOTIFY:" + type + Environment.NewLine +
                                 timeStamp + Environment.NewLine + 
                                 objType + Environment.NewLine + 
                                 notType + Environment.NewLine +
                                 project + Environment.NewLine +
                                 temp + Environment.NewLine + Environment.NewLine;
                if (m_mainwindow.UI_CHCKDUMP2FILES.IsChecked == true) 
                {
                    WriteLogFile(outPut4LogFile);
                }
                
                m_notificationControl.Items.Add(item);
            }
            catch (NotSupportedException e)
            {

            }
        }

        public void WriteLogFile(string output)
        {
            
            string dumpFile = m_mainwindow.UI_TXTDUMP2FILES.Text;
                //Create a Directory if it doesn't exist
                if (File.Exists(dumpFile) == false)
                {
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(dumpFile);
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message + ":\n";

                        MessageBox.Show(msg, "Please enter a correct Path", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    m_mainwindow.Cursor = Cursors.Arrow;
                }
                if ((!string.IsNullOrEmpty(output)))
                {
                    string filePathOut = dumpFile + "api_notification_0" + ".log";
                    File.AppendAllText(filePathOut, output, Encoding.Unicode);
                }
        }

        void FillTreeViewSiS(List<KeyValuePair<string, string>> values)
        {
            try
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = "Unnamed Event";
                string time = "";
                string obj = "";
                string type = "";
                string timeStamp = "";
                string timeStampOnly = "";
                string sessionnotifycounter = "";
                string eventid = "";
                string evtIdValue = "";
                string batchlocalcounter = "";
                string clientid = "";
                string pcell = "";
                string root = "";
                string parent = "";
                string objType = "";
                string notType = "";
                string project = "";
                string objekt = "";
                string executetype = "";
                string preFix = "";
                string temp = "";
                string tempsisxml = "";
                string outPut4LogFileSiS = "";
                string tmpMsgText = "";
                string tmpMsgText2 = "";
                string tmpMsgText3 = "";
                string msgText = "";
                foreach (KeyValuePair<string, string> value in values)
                {
                    if (value.Value != null && value.Value != "")
                    {
                        TreeViewItem sub_item = new TreeViewItem();
                        sub_item.Header = value.Key + ": " + value.Value;
                        preFix = value.Key + ": ";
                        switch (value.Key)
                        {
                            case "TimeStamp":
                                time = new System.Text.RegularExpressions.Regex(".*?T([^.]+)\\..*").Replace(value.Value, "$1");
                                item.Header = value.Value;
                                timeStampOnly = value.Value;
                                timeStamp = preFix + value.Value;
                                break;
                            case "SessionNotifyCounter":
                                item.Header = value.Value;
                                sessionnotifycounter = preFix + value.Value;
                                break;
                            case "EventId":
                                item.Header = value.Value;
                                evtIdValue = value.Value;
                                eventid = preFix + value.Value;
                                break;
                            case "BatchLocalCounter":
                                item.Header = value.Value;
                                batchlocalcounter = preFix + value.Value;
                                break;
                            case "ClientId":
                                item.Header = value.Value;
                                clientid = preFix + value.Value;
                                break;
                            case "PCell":
                                item.Header = value.Value;
                                pcell = preFix + value.Value;
                                break;
                            case "Root":
                                item.Header = value.Value;
                                root = preFix + value.Value;
                                break;
                            case "Parent":
                                item.Header = value.Value;
                                parent = preFix + value.Value;
                                break;
                            case "Object":
                                item.Header = value.Value;
                                objekt = preFix + value.Value;
                                break;
                            case "ExecuteType":
                                item.Header = value.Value;
                                executetype = preFix + value.Value;
                                break;
                            case "BF_API_OBJECT_TYPE":
                                if (value.Value.StartsWith("BF_API_OBJECT_"))
                                    obj = value.Value.Substring("BF_API_OBJECT_".Length);
                                objType = preFix + value.Value;
                                break;
                            case "BF_API_NOTIFY_TYPE":
                                if (value.Value.StartsWith("BF_API_NOTIFY_"))
                                    type = value.Value.Substring("BF_API_NOTIFY_".Length);
                                notType = preFix + value.Value;

                                if (value.Value == "BF_API_NOTIFY_SERVER_DOWN")
                                    sub_item.Foreground = new SolidColorBrush(Colors.Red);
                                else if (value.Value == "BF_API_NOTIFY_SERVER_UP")
                                    sub_item.Foreground = new SolidColorBrush(Colors.Green);
                                break;
                            case "Project":
                                project = preFix + value.Value;
                                break;
                            case "Change":
                                try
                                {
                                    XmlDocument doc = new XmlDocument();
                                    doc.LoadXml(value.Value);
                                    XmlHandler.Xml2TreeView(doc, sub_item, false);

                                    sub_item.Header = "Change:";
                                    string evtIdInChange = "<!--" + " " + evtIdValue;
                                    if (value.Value.Contains( evtIdInChange ) )
                                    {
                                        if (!string.IsNullOrEmpty(evtIdValue))
                                        {
                                            int end = value.Value.LastIndexOf("-->");
                                            int start = evtIdInChange.IndexOf("!--");
                                            if (end >= 0)
                                            {
                                                tmpMsgText = value.Value.Substring(start, end + 3);
                                                int begin = tmpMsgText.IndexOf("?");
                                                int final = tmpMsgText.IndexOf("<", begin + 1);
                                                tmpMsgText2 = tmpMsgText.Remove(begin, final - begin);
                                                int first = tmpMsgText2.IndexOf("<");
                                                int last = tmpMsgText2.IndexOf("<", first + 1);
                                                tmpMsgText3 = tmpMsgText2.Remove(first, last - first);
                                                int debut = tmpMsgText3.IndexOf("<");
                                                int fin = tmpMsgText3.IndexOf("<", debut + 1);
                                                msgText = tmpMsgText3.Remove(debut, fin - debut);
                                                //msgText = tmpMsgText2.Remove(first, last - first);
                                            }
                                        }
                                    }
                                }
                                catch (FormatException e)
                                {
                                }
                                break;
                        }
                        item.Items.Add(sub_item);
                        temp = value.Value;
                        if (temp.StartsWith("<")) 
                        {
                            tempsisxml = temp;
                        }
                    }
                }
                item.Header = timeStampOnly +" " + msgText ;
                outPut4LogFileSiS = StringFormatInFile(sessionnotifycounter) +
                                    StringFormatInFile(eventid) +
                                    StringFormatInFile(batchlocalcounter) +
                                    StringFormatInFile(clientid) +
                                    StringFormatInFile(timeStamp) +
                                    StringFormatInFile(objType) +
                                    StringFormatInFile(notType) +
                                    StringFormatInFile(project) +
                                    StringFormatInFile(pcell) +
                                    StringFormatInFile(root) +
                                    StringFormatInFile(parent) +
                                    StringFormatInFile(objekt) +
                                    StringFormatInFile(executetype) +
                                    StringFormatInFile(tempsisxml) + Environment.NewLine;
                if (m_mainwindow.UI_CHCKDUMP2FILES.IsChecked == true)
                {
                    WriteLogFileSiS(outPut4LogFileSiS);
                }
                m_notificationControl.Items.Add(item);
            }
            catch (NotSupportedException e)
            {

            }
        }

        public void WriteLogFileSiS(string output)
        {
            string dumpFile = m_mainwindow.UI_TXTDUMP2FILES.Text;
            //Create a Directory if it doesn't exist
            if (Directory.Exists(dumpFile) == false)
            {
                DirectoryInfo di = Directory.CreateDirectory(dumpFile);
            }
            if ((!string.IsNullOrEmpty(output)))
            {
                string filePathOut = dumpFile + "sis_notification_0" + ".log";
                File.AppendAllText(filePathOut, output, Encoding.Unicode);
            }
            
        }

        public string StringFormatInFile(String test) 
        {
            if (!(string.IsNullOrEmpty(test)))
            {
                test = test + Environment.NewLine;
            }
            else
            { ;}
            return test;
        }
        // All events need to be implemented but call the same operation
        public void OnNotify(
            IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE p_eOT,
            IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE p_eNT,
            string p_hdlProject,
            string p_hdlPCell,
            string p_hdlParent,
            string p_hdlObject,
            string p_bstrChange)
        {
            OnNotify(new KeyValuePair<string, string>[] {
                new KeyValuePair<string,string>("BF_API_OBJECT_TYPE", p_eOT.ToString()),
                new KeyValuePair<string,string>("BF_API_NOTIFY_TYPE", p_eNT.ToString()),
                new KeyValuePair<string,string>("Project", p_hdlProject),
                new KeyValuePair<string,string>("PCell", p_hdlPCell),
                new KeyValuePair<string,string>("Parent", p_hdlParent),
                new KeyValuePair<string,string>("Object", p_hdlObject),
                new KeyValuePair<string,string>("Change", p_bstrChange)
            }.ToList());
        }

        public void OnSISNotify(
            int p_dwSessionNotifyCounter,
            IPCS7_SBAPI_XLib.BF_API_SIS_NOTIFY_TYPE p_eNT,
            string p_hdlProject,
            string p_hdlPCell,
            string p_bstrChange,
            int p_dwBlockSize,
            int p_bEventLossDetected)
        {
            OnNotify(new KeyValuePair<string, string>[] {
                new KeyValuePair<string,string>("SessionNotifyCounter", p_dwSessionNotifyCounter.ToString()),
                new KeyValuePair<string,string>("BF_API_SIS_NOTIFY_TYPE", p_eNT.ToString()),
                new KeyValuePair<string,string>("Project", p_hdlProject),
                new KeyValuePair<string,string>("PCell", p_hdlPCell),
                new KeyValuePair<string,string>("Change", p_bstrChange),
                new KeyValuePair<string,string>("BlockSize", p_dwBlockSize.ToString()),
                new KeyValuePair<string,string>("EventLossDetected", p_bEventLossDetected.ToString())
            }.ToList());
        }

        // 707
        public void OnNotify(
            string p_bstrEventId,
            string p_bstrTimeStamp,
            IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE p_eOT,
            IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE p_eNT,
            string p_hdlProject,
            string p_hdlPCell,
            string p_hdlParent,
            string p_hdlObject,
            string p_bstrChange)
        {
            OnNotify(new KeyValuePair<string, string>[] {
                new KeyValuePair<string,string>("EventId", p_bstrEventId),
                new KeyValuePair<string,string>("TimeStamp", p_bstrTimeStamp),
                new KeyValuePair<string,string>("BF_API_OBJECT_TYPE", (int)p_eOT + " " + "-" + " " + p_eOT.ToString() ),
                new KeyValuePair<string,string>("BF_API_NOTIFY_TYPE", (int)p_eNT + " " + "-" + " " + p_eNT.ToString() ),
                new KeyValuePair<string,string>("Project", p_hdlProject),
                new KeyValuePair<string,string>("PCell", p_hdlPCell),
                new KeyValuePair<string,string>("Parent", p_hdlParent),
                new KeyValuePair<string,string>("Object", p_hdlObject),
                new KeyValuePair<string,string>("Change", p_bstrChange)
            }.ToList());
        }

        public void OnSISNotify(
            int p_dwSessionNotifyCounter,
            string p_bstrEventId,
            string p_bstrTimeStamp,
            IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE p_eOT,
            IPCS7_SBAPI_XLib.BF_API_SIS_NOTIFY_TYPE p_eNT,
            string p_hdlProject,
            string p_hdlPCell,
            string p_bstrChange,
            int p_bEventLossDetected)
        {
            OnNotify(new KeyValuePair<string, string>[] {
                new KeyValuePair<string,string>("SessionNotifyCounter", p_dwSessionNotifyCounter.ToString()),
                new KeyValuePair<string,string>("EventId", p_bstrEventId),
                new KeyValuePair<string,string>("TimeStamp", p_bstrTimeStamp),
                new KeyValuePair<string,string>("BF_API_OBJECT_TYPE", p_eOT.ToString()),
                new KeyValuePair<string,string>("BF_API_SIS_NOTIFY_TYPE", p_eNT.ToString()),
                new KeyValuePair<string,string>("Project", p_hdlProject),
                new KeyValuePair<string,string>("PCell", p_hdlPCell),
                new KeyValuePair<string,string>("Change", p_bstrChange),
                new KeyValuePair<string,string>("EventLossDetected", p_bEventLossDetected.ToString())
            }.ToList());
        }

        public void OnSISNotify4Refresh(
            int p_dwSessionNotifyCounter,
            string p_bstrEventId,
            string p_bstrTimeStamp,
            IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE p_eOT,
            IPCS7_SBAPI_XLib.BF_API_SIS_NOTIFY_TYPE p_eNT,
            string p_hdlProject,
            string p_hdlPCell,
            string p_bstrChange,
            int p_bLastEvent)
        {
            OnNotify(new KeyValuePair<string, string>[] {
                new KeyValuePair<string,string>("SessionNotifyCounter", p_dwSessionNotifyCounter.ToString()),
                new KeyValuePair<string,string>("EventId", p_bstrEventId),
                new KeyValuePair<string,string>("TimeStamp", p_bstrTimeStamp),
                new KeyValuePair<string,string>("BF_API_OBJECT_TYPE", p_eOT.ToString()),
                new KeyValuePair<string,string>("BF_API_SIS_NOTIFY_TYPE", p_eNT.ToString()),
                new KeyValuePair<string,string>("Project", p_hdlProject),
                new KeyValuePair<string,string>("PCell", p_hdlPCell),
                new KeyValuePair<string,string>("Change", p_bstrChange),
                new KeyValuePair<string,string>("LastEvent", p_bLastEvent.ToString())
            }.ToList());
        }

        // 712
        public void OnSISNotify(
            int p_dwSessionNotifyCounter,
            string p_bstrEventId,
            int p_bstrBatchLocalCounter,
            string p_bstrClientId,
            string p_bstrTimeStamp,
            IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE p_eOT,
            int p_dwSisNT712,
            string p_hdlProject,
            string p_hdlPCell,
            string p_hdlRoot,
            string p_hdlParent,
            string p_hdlObject,
            string p_bstrChange,
            IPCS7_SBAPI_XLib.BF_API_EXECUTE_TYPE p_eExecuteType)
        {
            OnSISNotify(new KeyValuePair<string, string>[] {
                new KeyValuePair<string,string>("SessionNotifyCounter", p_dwSessionNotifyCounter.ToString()),
                new KeyValuePair<string,string>("EventId", p_bstrEventId),
                new KeyValuePair<string,string>("TimeStamp", p_bstrTimeStamp),
                new KeyValuePair<string,string>("BF_API_OBJECT_TYPE", (int)p_eOT + " " + "-" + " " + p_eOT.ToString() ),
                new KeyValuePair<string,string>("Project", p_hdlProject),
                new KeyValuePair<string,string>("PCell", p_hdlPCell),
                new KeyValuePair<string,string>("Root", p_hdlRoot),
                new KeyValuePair<string,string>("Parent", p_hdlParent),
                new KeyValuePair<string,string>("Object", p_hdlObject),
                new KeyValuePair<string,string>("Change", p_bstrChange),
                new KeyValuePair<string,string>("ExecuteType", p_eExecuteType.ToString())
            }.ToList());
        }

        public void OnSISNotify4Refresh(
            int p_dwSessionNotifyCounter,
            string p_bstrEventId,
            int p_bstrBatchLocalCounter,
            string p_bstrClientId,
            string p_bstrTimeStamp,
            IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE p_eOT,
            int p_dwSisNT712,
            string p_hdlProject,
            string p_hdlPCell,
            string p_hdlRoot,
            string p_hdlParent,
            string p_hdlObject,
            string p_bstrChange,
            IPCS7_SBAPI_XLib.BF_API_EXECUTE_TYPE p_eExecuteType,
            int p_bLastEvent)
        {
            OnNotify(new KeyValuePair<string, string>[] {
                new KeyValuePair<string,string>("SessionNotifyCounter", p_dwSessionNotifyCounter.ToString()),
                new KeyValuePair<string,string>("EventId", p_bstrEventId),
                new KeyValuePair<string,string>("BatchLocalCounter", p_bstrBatchLocalCounter.ToString()),
                new KeyValuePair<string,string>("ClientId", p_bstrClientId),
                new KeyValuePair<string,string>("TimeStamp", p_bstrTimeStamp),
                new KeyValuePair<string,string>("BF_API_OBJECT_TYPE", p_eOT.ToString()),
                new KeyValuePair<string,string>("SisNT712", p_dwSisNT712.ToString()),
                new KeyValuePair<string,string>("Project", p_hdlProject),
                new KeyValuePair<string,string>("PCell", p_hdlPCell),
                new KeyValuePair<string,string>("Root", p_hdlRoot),
                new KeyValuePair<string,string>("Parent", p_hdlParent),
                new KeyValuePair<string,string>("Object", p_hdlObject),
                new KeyValuePair<string,string>("Change", p_bstrChange),
                new KeyValuePair<string,string>("ExecuteType", p_eExecuteType.ToString()),
                new KeyValuePair<string,string>("LastEvent", p_bLastEvent.ToString())
            }.ToList());
        }

    }
}
