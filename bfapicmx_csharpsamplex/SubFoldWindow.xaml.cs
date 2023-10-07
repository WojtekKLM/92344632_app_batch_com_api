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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Siemens.Automation.bfapicmx_csharpsamplex
{
    /// <summary>
    /// Interaction logic for SubFoldWindow.xaml
    /// </summary>
    public partial class SubFoldWindow : Window
    {
        MainWindow m_mainwindow = null;
        public SubFoldWindow()
        {
            InitializeComponent();
        }

        public SubFoldWindow(Window window)
        {
            // Initialise the Subfoler window and build the connection to the main Window 
            m_mainwindow = window as MainWindow;
            bfapicmx_CApiInvocation invocation = new bfapicmx_CApiInvocation(m_mainwindow);
            object apiInstance = m_mainwindow.APIInvoke.ApiInstance;
            Type apiISBType = m_mainwindow.APIInvoke.ApiISBType;
            Type apiSISType = m_mainwindow.APIInvoke.ApiSISType;
            Type apiCommanMgrType = m_mainwindow.APIInvoke.ApiCommanMgrType;
            InitializeComponent();
        }

        private void UI_OKAY_CLICK(object sender, RoutedEventArgs e)
        {
            m_mainwindow.UI_CBSUBFOLDER4PCELL.SelectedIndex = UI_CBSUBFOLDERS4PCELL.SelectedIndex;
            this.DialogResult = true;
            this.Close(); 
        }

        private void UI_CANCELED_CLICK(object sender, RoutedEventArgs e)
        {
            UI_CANCELED.IsCancel = true;
        }

        private void UI_SUBFOLDER_CLICK(object sender, RoutedEventArgs e)
        {
            m_mainwindow.APIInvoke.InvokeCommand("GetAllSubfolders4PCell", false);
            object[] tmp = new object[m_mainwindow.UI_CBSUBFOLDER4PCELL.Items.Count];
            m_mainwindow.UI_CBSUBFOLDER4PCELL.Items.CopyTo(tmp, 0);
            foreach (string item in m_mainwindow.UI_CBSUBFOLDER4PCELL.Items) 
            {
                UI_CBSUBFOLDERS4PCELL.Items.Add(item);
            }
            UI_CBSUBFOLDERS4PCELL.SelectedIndex = 0;
        }
    }
}
