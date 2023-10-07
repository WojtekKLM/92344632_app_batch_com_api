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
    /// The C# class for the initialization window.
    /// It is opened by ApiInvokes.InitAPI().
    /// </summary>
    public partial class InitWindow : Window
    {
       
        public InitWindow()
        {
            InitializeComponent();

            Version = ((ComboBoxItem)UI_VERSIONBOX.SelectedItem).Tag.ToString();
            Loader = UI_LOADERCHECK.IsChecked.Value;
            Remote = UI_REMOTECHECK.IsChecked.Value;
            string tmApiVersion = ((ComboBoxItem)UI_VERSIONBOX.SelectedItem).Content.ToString();
            ApiVersion = tmApiVersion.Substring( tmApiVersion.LastIndexOf('('), 7);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Version = ((ComboBoxItem)UI_VERSIONBOX.SelectedItem).Tag.ToString();
            string tmApiVersion = ((ComboBoxItem)UI_VERSIONBOX.SelectedItem).Content.ToString();
            ApiVersion = tmApiVersion.Substring(tmApiVersion.LastIndexOf('('), 7);
        }

        private void UI_OK_Click(object sender, RoutedEventArgs e)
        {
            Loader = UI_LOADERCHECK.IsChecked.Value;
            Remote = UI_REMOTECHECK.IsChecked.Value;
            this.DialogResult = true;
            this.Close(); 
        }

        private void UI_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            UI_CANCEL.IsCancel = true;
        }

        private string _version;
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }

        private bool _loader;
        public bool Loader
        {
            get
            {
                return _loader;
            }
            set
            {
                _loader = value;
            }
        }
        private bool _Remote;
        public bool Remote
        {
            get
            {
                return _Remote;
            }
            set
            {
                _Remote = value;
            }
        }
        private string _apiVersion;
        public string ApiVersion
        {
            get
            {
                return _apiVersion;
            }
            set
            {
                _apiVersion = value;
            }
        }
    }
}
