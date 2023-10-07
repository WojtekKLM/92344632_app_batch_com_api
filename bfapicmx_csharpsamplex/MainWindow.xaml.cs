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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; 
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace  Siemens.Automation.bfapicmx_csharpsamplex
{
     
    /// <summary>
    /// The C# class for the main window of this tool.
    /// </summary>
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        public Dictionary<string, string> CommandObjectCltn = new Dictionary<string, string>();
        bfapicmx_CApiInvocation invocation;
        
        public bfapicmx_CApiInvocation APIInvoke
        {
            get
            {
                if (invocation == null)
                {
                    invocation = new bfapicmx_CApiInvocation(this);
                }
                return invocation;
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelBase(this);
        }

        // Properties for API invokes
        // Values are accessed in the class FunctionParameters

        #region GENERAL CONTROLS

        public TreeView Notification
        {
            get
            {
                return UI_TXTNOTIFICATIONS;
            }
        }

        public int OutputBar
        {
            get
            {
                int outputbar = 0;

                foreach (ToggleButton button in UI_OUTPUTBAR_1.Children)
                    outputbar += button.IsChecked.Value ? int.Parse(button.Tag.ToString()) : 0;
                foreach (ToggleButton button in UI_OUTPUTBAR_2.Children)
                    outputbar += button.IsChecked.Value ? int.Parse(button.Tag.ToString()) : 0;

                return outputbar;
            }
        }

        public int OutputBar4Command(string command)
        {
            int outputbar = OutputBar;
            if (command == "GetBatchState")
            {
                outputbar |= 128; // State
                outputbar |= 256; // ExState
                outputbar |= 512; // ChainInfo
            }
            return outputbar;
        }

        public int Counter
        {
            get
            {
                int counter;
                int.TryParse(UI_TXTCOUNTER.Text, out counter);
                if (counter > 0 && counter <= 10000)
                    return counter;
                else
                    return 1;
            }
            set
            {
                UI_TXTCOUNTER.Text = value.ToString();
                HighlightControlEffect(UI_TXTCOUNTER);
            }
        }

        public string WildCard
        {
            get
            {
                string wildcard = "";
                if (UI_CHCKWILDCARD.IsChecked.Value)
                {
                    wildcard = UI_TXTWILDCARD.Text;
                }
                return wildcard;
            }
        }

        public bool WithTimeStamp
        {
            get
            {
                return UI_CHCKTIMESTAMP.IsChecked.Value;
            }
        }

        public bool WithAuditTrail
        {
            get
            {
                return UI_CHCKAUDITTRAIL.IsChecked.Value;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                DateTime date = UI_DTPDATE.Value;
                DateTime time = UI_DTPTIME.Value;
                DateTime result = date.Date + time.TimeOfDay;
                return result;
            }
        }

        public string TimeStampString
        {
            get
            {
                DateTime stamp = TimeStamp;
                string formatted = stamp.ToString("d.M.yyyy H:m:s");
                return formatted;
            }
        }

        public bool WithHierarchy
        {
            get
            {
                return (bool)UI_CHCKHIERARCHY.IsChecked;
            }
        }

        #endregion

        #region INIT

        public string Session
        {
            get
            {
                return UI_SessionID.Text;
            }
            set
            {
                UI_SessionID.Text = value;
            }
        }

        public string ProjectHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLPROEJCTS.Text);
            }
        }

        public string PCellHdl
        {
            get
            {
                return Extract_Hdl(UI_CBPCELLS.Text);
            }
        }

        public string UnitClassHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLUNITCLASSES4PCELL.Text);
            }
        }

        public string UnitHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLUNITS4UNITCLASS.Text);
            }
        }

        public string UserName
        {
            get
            {
                return UI_TXTUSERNAME.Text;
            }
            set
            {
                UI_TXTUSERNAME.Text = value;
            }
        }

        public string UserNameLong
        {
            get
            {
                return UI_TXTLONGNAME.Text;
            }
            set
            {
                UI_TXTLONGNAME.Text = value;
            }
        }

        public string Domain
        {
            get
            {
                return UI_TXTDOMAIN.Text;
            }
            set
            {
                UI_TXTDOMAIN.Text = value;
            }
        }

        public string ComputerName
        {
            get
            {
                return UI_TXTCOMPUTER.Text;
            }
            set
            {
               UI_TXTCOMPUTER.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return UI_TXTPASSWORD.Password;
            }
            set 
            {
                UI_TXTPASSWORD.Password = value;
            }
        }

        public string Application
        {
            get
            {
                return UI_TXTAPPLICATION.Text;
            }
            set
            {
                UI_TXTAPPLICATION.Text = value;
            }
        }

        public bool ChooseAPIVersionChck
        {
            get
            {
                return UI_CHCKCHOOSEAPIVERSION.IsChecked.Value;
            }
        }

        public string DumpFile
        {
            get
            {
                return UI_TXTDUMP2FILES.Text;
            }
            set
            {
                UI_TXTDUMP2FILES.Text = value;
            }
        }

        public string DumpCheck
        { 
            get
            {
                return UI_CHCKDUMP2FILES.IsChecked.Value.ToString();
            }
            set 
            {
                UI_CHCKDUMP2FILES.IsChecked.Equals(value).ToString();
            }
        }
        #endregion

        #region RECIPE FORMULA

        public string MRHdl
        {
            get
            {
                return Extract_Hdl(UI_CBMRs2.Text);
            }
        }

        public string FormulaCatHdl
        {
            get
            {
                return Extract_Hdl(UI_CBFORMULACATS.Text);
            }
        }

        public string FormulaCatHdl4Pcell
        {
            get
            {
                return Extract_Hdl(UI_CBFORMULACATS4PCELL.Text);
            }
        }

        public string FormulaHdl
        {
            get
            {
                return Extract_Hdl(UI_CBFORMULAS.Text);
            }
        }

        public string ProductHdl
        {
            get
            {
                return Extract_Value("producthdl", UI_CBPRODUCTDATA4OBJECT.Text);
            }
        }

        public bool RecFormSubFolderChck
        {
            get
            {
                return UI_CHCKRECFORMWITHSUBFOLDER.IsChecked.Value;
            }
        }

        public string UIRecFormName
        {
            get
            {
                return UI_TXTRECFORMNAME.Text;
            }
        }

        public string UIRecFormVersion
        {
            get
            {
                return UI_TXTRECFORMVERSION.Text;
            }
        }

        public string UIRecFormDesc
        {
            get
            {
                return UI_TXTRECFORMDESC.Text;
            }
        }

        public bool UIRecFormOptLock
        {
            get
            {
                return UI_CHCKRECFORMOPTLOCK.IsChecked.Value;
            }
        }


        #endregion

        #region ORDER BATCH

        public string UISize
        {
            get
            {
                return UI_TXTBATCHSIZE.Text;
            }
        }

        public string OrderCatHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLORDERCATS4PCELL.Text);
            }
        }

        public string OrderHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLORDERS4ORDERCAT.Text);
            }
        }

        public string BatchHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLBATCHES4ORDER.Text);
            }
        }

        public string OrderEarlier
        {
            get
            {
                string dateTimeEarlier = UI_DTPEARLIER.Value.ToString("MM.dd.yyyy hh:mm:ss");
                return dateTimeEarlier;
            }
        }

        public string OrderEarlierW3C
        {
            get
            {
                string dateTimeEarlier = UI_DTPEARLIER.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return dateTimeEarlier;
            }
        }

        public string OrderLater
        {
            get
            {
                string dateTimeLater = UI_DTPLATER.Value.ToString("MM.dd.yyyy hh:mm:ss");
                return dateTimeLater;
            }
        }

        public string OrderLaterW3C
        {
            get
            {
                string dateTimeLater = UI_DTPLATER.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return dateTimeLater;
            }
        }
        public string FormOrMRHdl
        {
            get
            {
                return Extract_Hdl(UI_CBFORMORMR4CREATEBATCH.Text);
            }
        }

        public bool BatchWithML
        {
            get
            {
                return UI_CHKBatchML.IsChecked.Value;
            }
        }

        public string BatchML
        {
            get
            {
                return UI_TXTBATCHML.Text;
            }
            set 
            {
                UI_TXTBATCHML.Text = value;
            }
        }

        public string BatchStartMode
        {
            get
            {
                ComboBoxItem item = (ComboBoxItem)UI_CBSTARTMODE.SelectedItem;
                return (string)item.Tag;
            }
        }

        public bool BatchStartModeChck
        {
            get
            {
                return UI_CHCKSTARTMODE.IsChecked.Value;
            }
        }

        public bool BatchDateTimeFormatChck
        {
            get
            {
                return UI_CHCKDATETIMEFORMAT.IsChecked.Value;
            }
        }

        public string BatchDateTimeFormatPlanStart
        {
            get
            {
                return UI_TXTDATETIMEFORMATPLANSTART.Text;
            }
        }

        public string BatchDateTimeFormatPlanEnd
        {
            get
            {
                return UI_TXTDATETIMEFORMATPLANSEND.Text;
            }
        }

        public string BatchPlanStart
        {
            get
            {
                string dateTimePlanStart = UI_DTPPLANSTARTDATE.Value.ToString("MM.dd.yyyy H:mm:ss");
                return dateTimePlanStart;
            }
        }

        public string BatchPlanStartW3C
        {
            get
            {
                string dateTimePlanStart = UI_DTPPLANSTARTDATE.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return dateTimePlanStart;
            }
        }
        public string BatchPlanEnd
        {
            get
            {
                string dateTimePlanEnd = UI_DTPPLANENDDATE.Value.ToString("MM.dd.yyyy H:mm:ss");
                return dateTimePlanEnd;
            }
        }

        public string BatchPlanEndW3C
        {
            get
            {
                string dateTimePlanEnd = UI_DTPPLANENDDATE.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return dateTimePlanEnd;
            }
        }
        public int BatchState
        {
            get
            {
                return UI_CBBATCHSTATE.SelectedIndex;
            }
            set
            {
                UI_CBBATCHSTATE.SelectedIndex = value;
                HighlightControlEffect(UI_CBBATCHSTATE);
            }
        }

        public int BatchExState
        {
            get
            {
                int exstate = 0;
                foreach (CheckBox box in UI_BATCHSTATECLOSED.Children)
                {
                    if ((bool)box.IsChecked) { exstate += int.Parse((string)box.Tag); }
                }
                foreach (CheckBox box in UI_BATCHSTATEARCHIVED.Children)
                {
                    if ((bool)box.IsChecked) { exstate += int.Parse((string)box.Tag); }
                }
                return exstate;
            }
            set
            {
                //int counter = 0;
                foreach (CheckBox box in UI_BATCHSTATECLOSED.Children)
                {
                    int tag = int.Parse((string)box.Tag);
                    int and = value & tag;
                    if (and > 0) { box.IsChecked = true; }
                    else { box.IsChecked = false; }
                    //HighlightControlEffect(box, counter += 80);
                }
                //counter = 0;
                foreach (CheckBox box in UI_BATCHSTATEARCHIVED.Children)
                {
                    int tag = int.Parse((string)box.Tag);
                    int and = value & tag;
                    if (and > 0) { box.IsChecked = true; }
                    else { box.IsChecked = false; }
                    //HighlightControlEffect(box, counter += 80);
                }
            }
        }

        public string BatchChain
        {
            get
            {
                return Extract_Hdl(UI_CBCHAINEDBATCHES.Text);
            }
        }

        public int BatchChainOnStart
        {
            get
            {
                int val = 0;
                if (UI_RBCHAINONSTART.IsChecked.Value)
                    val = 1;
                return val;
            }
        }

        public string BatchGaptime
        {
            get
            {
                return UI_TXTGAPTIME.Text;
            }
        }

        public bool OrderBatchSubFolderChck
        {
            get
            {
                return UI_CHCKORDERBATCHWITHSUBFOLDER.IsChecked.Value;
            }
        }

        public string UIOrderBatchName
        {
            get
            {
                return UI_TXTORDERBATCHNAME.Text;
            }
        }

        public string UIOrderBatchSize
        {
            get
            {
                return UI_TXTORDERBATCHSIZE.Text;
            }
        }

        public string UIOrderBatchDesc
        {
            get
            {
                return UI_TXTORDERBATCHDESC.Text;
            }
        }

        public bool UIOrderBatchOptLock
        {
            get
            {
                return UI_CHCKORDERBATCHOPTLOCK.IsChecked.Value;
            }
        }

        public bool StayingContinueChck
        {
            get
            {
                return UI_CHCKSTAYINGCONTINUE.IsChecked.Value;
            }
        }

        public bool ContinueChck
        {
            get
            {
                return UI_CHCKCONTINUE.IsChecked.Value;
            }
        }
        #endregion

        #region MATERIAL QUALITY

        public string Material
        {
            get
            {
                return Extract_Hdl(UI_CBMATERIALS.Text);
            }
        }

        public int MaterialUsage
        {
            get
            {
                return UI_CBMATERIAL_USAGE.SelectedIndex;
            }
            set
            {
                UI_CBMATERIAL_USAGE.SelectedIndex = value;
                HighlightControlEffect(UI_CBMATERIAL_USAGE);
            }
        }

        public string MaterialsPath
        {
            get
            {
                return UI_TXTMANYMATERIALSPATH.Text;
            }
            set 
            {
                UI_TXTMANYMATERIALSPATH.Text = value;
            }
        }

        public string Quality4Material
        {
            get
            {
                return Extract_Hdl(UI_CBALLQUALITIES4MATERIAL.Text);
            }
        }

        public string QualityData
        {
            get
            {
                return Extract_Hdl(UI_CBQUALITYDATA.Text);
            }
        }

        public bool MatSubFolderChck
        {
            get
            {
                return UI_CHCKMATWITHSUBFOLDER.IsChecked.Value;
            }
        }

        public string UIMatName
        {
            get
            {
                return UI_TXTMATNAME.Text;
            }
        }

        public string UIMatCode
        {
            get
            {
                return UI_TXTMATCODE.Text;
            }
        }

        public string UIMatDesc
        {
            get
            {
                return UI_TXTMATDESC.Text;
            }
        }

        public bool UIMatOptLock
        {
            get
            {
                return UI_CHCKMATOPTLOCK.IsChecked.Value;
            }
        }
        #endregion

        #region ALLOCATION PARAMETER
        public string AllocationHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLOCATIONS.Text);
            }
        }

        public string StrategyTypeId
        {
           get
           {
               string StrategyTypeId = UI_STRATEGYTYPE.Text;
              //string strScalingId = Extract_Value("scalingid", text);
               return StrategyTypeId;
           }
        }

        public string ParameterId
        {
           get
           {
              string strParameterId = UI_PROCESS_PARAM_ID.Text;
              //string strParameterId = Extract_Value("parameterid", text);
              return strParameterId;
           }
        }

        public string AllocOnStart
        {
            get
            {
                string strAllocOnStart = UI_ALLOSONSTART_ID.Text;
                //string strParameterId = Extract_Value("parameterid", text);
                return strAllocOnStart;
            }
        }
        public string TRPID
        {
            get
            {
                string text = UI_CBTRP.Text;
                string hdl = Extract_Value("valueid", text);
                return hdl;
            }
        }

        public string AllocationNewHdl
        {
            get
            {
                string text = UI_CBNEW_ALLOCATION.Text;
                string hdl = Extract_Value("hdl", text);
                return hdl;
            }
        }

        public string CONTID
        {
            get
            {
                return (string)UI_CONTID.Text;
                
            }
        }

        public string TERMID
        {
            get
            {
               return (string)UI_TERMID.Text;
                
            }
        }

        public string ParameterType
        {
            get
            {
                ComboBoxItem item = (ComboBoxItem)UI_CBPARAMETERTYPE.SelectedItem;
                string value = (string)item.Tag;
                return value;
            }
        }

        public string TxtSet
        {
            get
            {
                return UI_txtSet.Text;
            }
        }

        public string TxtSet2
        {
            get
            {
                return UI_txtSet2.Text;
            }
        }
            
        public string TxtUsg
        {
            get
            {
                return UI_txtUsg.Text;
            }
        }

        public string TxtUsg2
        {
            get
            {
                return UI_txtUsg2.Text;
            }
        }

        public string DbID
        {
            get
            {
                return UI_txtID.Text;
            }
        }

        public string DbID2
        {
            get
            {
                return UI_txtID2.Text;
            }
        }

        public string DataTypeID
        {
            get
            {
                return UI_txtDtId.Text;
            }
        }
        public string DataTypeID2
        {
            get
            {
                return UI_txtDtId2.Text;
            }
        }

        public string Amount
        {
            get
            {
                return UI_txtValue.Text;
            }
        }

        public string Amount2
        {
            get
            {
                return UI_txtValue2.Text;
            }
        }

        public string PhysicalUnitID
        {
            get
            {
                return UI_txtPhUnit.Text;
            }
        }

        public string PhysicalUnitID2
        {
            get
            {
                return UI_txtPhUnit2.Text;
            }
        }

        public string MaterialHdl
        {
            get
            {
                return UI_txtMatHdl.Text;
            }
        }

        public string MaterialHdl2
        {
            get
            {
                return UI_txtMatHdl2.Text;
            }
        }

        public bool IgnoreDefering
        {
            get
            {
                return UI_CHCKIGNOREDEFERING.IsChecked.Value;
            }
        }

        public bool WithActValue
        {
            get
            {
                return UI_CHCKWITHACTVALUE.IsChecked.Value;
            }
        }

        public string ParametersPath
        {
            get
            {
                return UI_TXTMANYPARAMETERPATH.Text;
            }
        }
        
        #endregion
        #region SUBFOLDER
        
        public string SubFolderHDL
        {
            get
            {
                int count = UI_CBSUBFOLDER4PCELL.Items.Count;
                string temp = UI_CBSUBFOLDER4PCELL.Text;
                if (count == 0 || string.IsNullOrEmpty(temp)) 
                {
                    return null;
                }
                temp = UI_CBSUBFOLDER4PCELL.SelectedItem.ToString();
                return Extract_Hdl (temp);
            }
        }

        public string SubFolderHDLEmpty
        {
            get
            {
                string temp = "";
                return temp;
            }

        }

        public string SubName
        {
            get
            {
                return UI_TXTSUBFOLDERNAME.Text;
            }
        }
       
        /* Helpmethod to for CreateMaterial with Subfolder
           For the communication between Material Quality and Subfolder
        */
        public T FindElementByName<T>(FrameworkElement element, string sChildName) where T : FrameworkElement
        {
            T childElement = null;
            var nChildCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < nChildCount; i++)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;

                if (child == null)
                    continue;

                if (child is T && child.Name.Equals(sChildName))
                {
                    childElement = (T)child;
                    break;
                }

                childElement = FindElementByName<T>(child, sChildName);

                if (childElement != null)
                    break;
            }
            return childElement;
        }
        #endregion

        #region COMMON FUNCTIONS

        public string TargetType
        {
            get
            {
                ComboBoxItem item = (ComboBoxItem)UI_CBTARGETTYPE.SelectedItem;
                string value = (string)item.Tag;
                return value;
            }
        }

        public string ObjectHdl
        {
            get
            {
                return Extract_Hdl(UI_TXTHDLINPUT.Text);
            }
        }
        public string ChildHdl
        {
            get
            {
                return Extract_Hdl(UI_CBCHILDREN.Text);
            }
        }

        public string AttributeName
        {
            get
            {
                string text = UI_CBATTRIBUTE_NAMES.Text;
                int key = -1;
                if (bfapicmx_CAttributes.Attributes.ContainsValue(text))
                    key = bfapicmx_CAttributes.Attributes.FirstOrDefault(x => x.Value == text).Key;

                if(key == -1)
                    throw new NotImplementedException("Attribute-Value " + text + " has not been implemented in Attributes.cs!");
                return key.ToString();
            }
        }

        public string AttributeValue
        {
            get
            {
                return UI_CBATTRIBUTE_VALUES.Text;
            }
        }

        public bool WithParameter
        {
            get
            {
                return UI_CHCKWITHPARAMETER.IsChecked.Value;
            }
        }

        public int ObjectState
        {
            get
            {
                return UI_CBOJECTSTATE.SelectedIndex;
            }
            set
            {
                UI_CBOJECTSTATE.SelectedIndex = value;
                HighlightControlEffect(UI_CBOJECTSTATE);
            }
        }

        public bool CheckText
        {
            get
            {
                return (bool)UI_CHCKTEST.IsChecked;
            }
        }

        public bool CheckError
        {
            get
            {
                return (bool)UI_CHCKERROR.IsChecked;
            }
        }

        public bool CheckArchived
        {
            get
            {
                return (bool)UI_CHCKARCHIVED.IsChecked;
            }
        }
        public bool CheckIng
        {
            get
            {
                return (bool)UI_CHCKING.IsChecked;
            }
        }

        public string Hdl1
        {
            get
            {
                return Extract_Hdl(UI_HDL1.Text);
            }
        }
        public string Hdl2
        {
            get
            {
                return Extract_Hdl(UI_HDL2.Text);
            }
        }

        public string ErrorCode
        {
            get
            {
                return UI_TXTERRORCODE.Text;
            }
        }
        public string ErrorTest
        {
            get
            {
                return UI_TXTERRORTEXT.Text;
            }
            set
            {
                UI_TXTERRORTEXT.Text = value;
            }
        }

        public int GetXmlFlags()
        {
            int xmlflag = 0;
            if (UI_CHCKDEFAULT.IsChecked == false)
            {
                if (UI_CHCKPCELL.IsChecked == true && UI_CHCKPCELL.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKPCELL.Tag.ToString());
                }
                if (UI_CHCKCHARTS.IsChecked == true && UI_CHCKCHARTS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKCHARTS.Tag.ToString());
                }
                if (UI_CHCKSFCSTRUCTURE.IsChecked == true && UI_CHCKSFCSTRUCTURE.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKSFCSTRUCTURE.Tag.ToString());
                }
                if (UI_CHCKDELTAMATERIALS.IsChecked == true && UI_CHCKDELTAMATERIALS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKDELTAMATERIALS.Tag.ToString());
                }
                if (UI_CHCKMODIFS.IsChecked == true && UI_CHCKMODIFS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKMODIFS.Tag.ToString());
                }
                if (UI_CHCKBATCHTAGS.IsChecked == true && UI_CHCKBATCHTAGS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKBATCHTAGS.Tag.ToString());
                }
                if (UI_CHCKBATCHEMESSAGES.IsChecked == true && UI_CHCKBATCHEMESSAGES.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKBATCHEMESSAGES.Tag.ToString());
                }
                if (UI_CHCKSNAPSHOTS.IsChecked == true && UI_CHCKSNAPSHOTS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKSNAPSHOTS.Tag.ToString());
                }
                if (UI_CHCKCOMMENTS.IsChecked == true && UI_CHCKCOMMENTS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKCOMMENTS.Tag.ToString());
                }
                if (UI_CHCKESIG.IsChecked == true && UI_CHCKESIG.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKESIG.Tag.ToString());
                }
                if (UI_CHCKACTION.IsChecked == true && UI_CHCKACTION.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKACTION.Tag.ToString());
                }
                if (UI_CHCKRPEDESC.IsChecked == true && UI_CHCKRPEDESC.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKRPEDESC.Tag.ToString());
                }
                if (UI_CHCKRPEVALUE.IsChecked == true && UI_CHCKRPEVALUE.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKRPEVALUE.Tag.ToString());
                }
                if (UI_CHCKRPEDATA.IsChecked == true && UI_CHCKRPEDATA.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKRPEDATA.Tag.ToString());
                }
                if (UI_CHCKEVENTS.IsChecked == true && UI_CHCKEVENTS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKEVENTS.Tag.ToString());
                }
                if (UI_CHCKALLOCATIONS.IsChecked == true && UI_CHCKALLOCATIONS.IsVisible == true)
                {
                    xmlflag += int.Parse(UI_CHCKALLOCATIONS.Tag.ToString());
                }
                return xmlflag;
            }
            else
                return -1;
        }

        public string ArchiveTyp
        {
            get
            { 
                ComboBoxItem item = (ComboBoxItem)UI_CBARCHIVE.SelectedItem;
                string value = (string)item.Tag;
                return value;
                //return int.Parse(UI_CBARCHIVE.SelectedIndex.ToString());
            }
        }

        public string ArchiveTag
        {
            get
            {
                string value;
                ComboBoxItem item = (ComboBoxItem)UI_CBARCHIVE.SelectedItem;
                if (item == null)
                {
                    value = "";
                }
                else
                {
                    value = (string)item.Tag;
                }
                return value;
            }
        }

        public bool CheckExtData
        {
            get
            {
                return (bool)UI_CHCKEXTENDEDDATA.IsChecked;
            }
        }
        public bool CheckWithoutTag
        {
            get
            {
                return (bool)UI_CHCKBATCHWITHOUTTAGS.IsChecked;
            }
        }

        public bool CheckWithPCell
        {
            get
            {
                return (bool)UI_CHCKWITHPCELL.IsChecked;
            }
        }
       
        public void UnchekVisible()
        {
            UI_CHCKPCELL.Visibility = System.Windows.Visibility.Visible;
            if (UI_CHCKPCELL.IsChecked == true)
            {
                UI_CHCKPCELL.IsChecked = false;
            }
            UI_CHCKMODIFS.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKCOMMENTS.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKRPEDESC.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKCHARTS.Visibility = System.Windows.Visibility.Visible;
            if (UI_CHCKCHARTS.IsChecked == true)
            {
                UI_CHCKCHARTS.IsChecked = false;
            }
            UI_CHCKBATCHTAGS.Visibility = System.Windows.Visibility.Visible;
            if (UI_CHCKBATCHTAGS.IsChecked == true)
            {
                UI_CHCKBATCHTAGS.IsChecked = false;
            }
            UI_CHCKESIG.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKRPEVALUE.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Visible;
            if (UI_CHCKSFCSTRUCTURE.IsChecked == true)
            {
                UI_CHCKSFCSTRUCTURE.IsChecked = false;
            }
            UI_CHCKBATCHEMESSAGES.Visibility = System.Windows.Visibility.Visible;
            if (UI_CHCKBATCHEMESSAGES.IsChecked == true)
            {
                UI_CHCKBATCHEMESSAGES.IsChecked = false;
            }
            UI_CHCKACTION.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKRPEDATA.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKDELTAMATERIALS.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKSNAPSHOTS.Visibility = System.Windows.Visibility.Visible;
            if (UI_CHCKSNAPSHOTS.IsChecked == true)
            {
                UI_CHCKSNAPSHOTS.IsChecked = false;
            }
            UI_CHCKEVENTS.Visibility = System.Windows.Visibility.Visible;
            if (UI_CHCKEVENTS.IsChecked == true)
            {
                UI_CHCKEVENTS.IsChecked = false;
            }
            UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Visible;
            UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Visible;
        }
        
        public void DefaultCheckInvisible()
        {
            UI_CHCKPCELL.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKMODIFS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKCOMMENTS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKRPEDESC.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKCHARTS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKBATCHTAGS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKESIG.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKRPEVALUE.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKBATCHEMESSAGES.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKACTION.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKRPEDATA.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKDELTAMATERIALS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKSNAPSHOTS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKEVENTS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Hidden;
            UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Hidden;
        }

        public bool CheckExtTransform
        {
            get
            {
                return UI_CHKEXTTRANSFORM.IsChecked.Value;
            }
        }

        public string Extransform
        {
            get
            {
                string tmp = "";
                if (CheckExtTransform == true)
                {
                    tmp = UI_TXTEXTTRANS.Text;
                }
                return tmp;
            }
        }

        public bool WithActValueObj
        {
            get
            {
                return UI_CHCKWITHACTVALUEOBJ.IsChecked.Value;
            }
        }

        public string ObjectDataHeaderPath
        {
            get
            {
                return UI_TXTINTO.Text;
            }
            set 
            {
                UI_TXTINTO.Text = value;
            }
        }
        
        #endregion

        #region API|SIS EVENTS
        public string EventHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLEVENTDEFINITIONS.Text);
            }
        }

        public int SIS_Position4EventID
        {
            get
            {
                return UI_CBEVENTID_POSITION.SelectedIndex;
            }
        }

        public string SIS_EventID
        {
            get
            {
                return Extract_Hdl(UI_CBEVENTID.Text);
            }
        }

        public string Mode4SisEvent
        {
            get
            {
                string value;
                ComboBoxItem item = (ComboBoxItem)UI_CBEVENTID_POSITION.SelectedItem;
                if (item == null)
                {
                    value = "";
                }
                else
                {
                    value = (string)item.Content;
                }
                return value;
            }
        }

       
        #endregion
        #region NOTIFY FILTER

        public bool SIS_AdviseOptions
        {
            get
            {
                return UI_CHKENSURE_CONSISTENT.IsChecked.Value;
            }
        }

        public bool CheckAutoAdvise
        {
            get
            {
                return UI_CHKAUTOADVISE.IsChecked.Value;
            }
        }

        public bool CheckEventId
        {
            get
            {
                return UI_CHKEVENTID.IsChecked.Value;
            }
        }

        public string EventId4Advise
        {
            get
            {
                string tmp = "";
                if (CheckEventId == true)
                {
                    tmp = UI_TXTEVTTID.Text;
                }
                return tmp;
            }
        }

        public string AutoReAdvise
        {
            get
            {
                string tmp = "";
                if (CheckAutoAdvise == true)
                {
                    tmp = UI_TXTAUTOREADVISE.Text;
                }
                return tmp;
            }
        }

        public string NotifyType
        {
            get
            {
                string Notifytext = "";
                Notifytext += UI_CHKFILTCREATED.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE.BF_API_NOTIFY_CREATED)).ToString() + "\n" : "";
                Notifytext += UI_CHKFILTDELETE.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE.BF_API_NOTIFY_DELETED)).ToString() + "\n" : "";
                Notifytext += UI_CHKFILTRENAME.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE.BF_API_NOTIFY_RENAME)).ToString() + "\n" : "";
                Notifytext += UI_CHKFILTSAVED.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE.BF_API_NOTIFY_SAVED)).ToString() + "\n" : "";
                Notifytext += UI_CHKFILTLOCKED.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE.BF_API_NOTIFY_LOCK)).ToString() + "\n" : "";
                Notifytext += UI_CHKFILTUNLOCKED.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE.BF_API_NOTIFY_UNLOCK)).ToString() + "\n" : "";
                Notifytext += UI_CHKFILTSTATECHANGED.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_NOTIFY_TYPE.BF_API_NOTIFY_STATE_CHANGED)).ToString() + "\n" : "";

                return Notifytext;
            }
        }

        public string ObjectType
        {
            get
            {
                string Objecttext = "";
                Objecttext += UI_CHKFILTPCELL.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_PCELL)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTLIBRARY.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_LIB)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTRECIPIE.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_MASTERRECIPE)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTFORMCAT.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_FORMULACAT)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTFORM.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_FORMULA)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTORDERCAT.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_ORDERCAT)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTORDER.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_ORDER)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTBATCH.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_BATCH)).ToString() + "\n" : "";
                Objecttext += UI_CHKFFILTMATERIAL.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_MATERIAL)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTQUALITY.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_QUALITY)).ToString() + "\n" : "";
                Objecttext += UI_CHKFILTUSERGRP.IsChecked.Value ? ((int)(IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_USERGROUP)).ToString() + "\n" : "";
                return Objecttext;
            }
        }
       
        #endregion

        #region HISTORICAL SIS EVENTS
        public bool HSIS_BatchUnit
        {
            get
            {
                return UI_RBBATCH.IsChecked.Value;
            }
        }

        public string HSIS_UnitHdl
        {
            get
            {
                return Extract_Hdl(UI_CBALLHSISUNITS.Text);
            }
        }

        public string HSIS_StartTime
        {
            get
            {
                string StartdateTimeHis = UI_DTPHSIS_STARTDATE.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return StartdateTimeHis;
            }
        }

        public string HSIS_EndTime
        {
            get
            {
                string EndDdateTimeHis = UI_DTPHSIS_ENDDATE.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return EndDdateTimeHis;
            }
        }

        public bool CheckHisEndEvent
        {
            get
            {
                return (bool)UI_CHKHISEVENTEND.IsChecked;
            }
        }

        public string HSIS_StartEventId
        {
            get
            {
                return UI_TXTSTARTEVENT.Text;
            }
        }

        public string HSIS_EndEventId
        {
            get
            {
                return UI_TXTENDEVENT.Text;
            }
        }

        public string Input4HisEventByFile
        {
            get
            {
                string HisFilePath = UI_TXTHISFILEPATH.Text;
                string result = "";
                if(File.Exists(HisFilePath))
                {
                    StreamReader sr = new StreamReader(HisFilePath);
                    {
                        string line;
                        string nextLine;
                        while ((line = sr.ReadLine()) != null)
                        {
                            nextLine = line;
                            result = result + nextLine + (char)13 + (char)10;
                        }
                    }
                }
                return result;
            }
        }

        public void AddAndSelectItemsHDLInCombo(ComboBox cb2Copy, ComboBox cbCopy)
        {
            string item = cb2Copy.Text;
            string itemHDL = Extract_Hdl(item);
            if (!string.IsNullOrEmpty(itemHDL))
            {
                int itemsNr = cbCopy.Items.Count;
                if (itemsNr == 0)
                {
                    cbCopy.SelectedIndex = 0;
                }
                else
                {
                    cbCopy.SelectedIndex = itemsNr++;
                }
                if (cbCopy.Items.Contains(itemHDL))
                {
                    ;
                }
                else
                {
                    cbCopy.Items.Add(itemHDL);
                }
            }
        }
        public string HSIS_ObjectHdl
        {
            get
            {
                //string Hdl = null; ;
                //if (UI_CBHSISCHANGED.Items.Count != 0)
                //{
                //    foreach (ComboBoxItem items in UI_CBHSISCHANGED.Items)
                //    {
                int i = 0; 
                       return Extract_Hdl(UI_CBHSISCHANGED.Items.GetItemAt(i).ToString());
                //    }
                //}
                //return Hdl;
           } 
           
        }
        #endregion

        #region LOGBOOKPRINT
        public string ActRenamed
        {
            get
            {
                 return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_RENAMED).ToString();
            }
        }

        public string ActChained
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_CHAINED).ToString();
                
            }
        }

        public string ActUnchained
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_UNCHAINED).ToString();
            }
        }

        public string ActRelTest
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_REL_TEST).ToString();
                
            }
        }

        public string ActRelProd
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_REL_PROD).ToString();
               
            }
        }

        public string ActArchived
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_ARCHIVED).ToString();
            }
        }

        public string ActImported
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_IMPORTED).ToString();
            }
        }

        public string ActNewProd
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_NEW_PCELL_DATA).ToString();
            }
        }

        public string ActDeleted
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_ACTION_TYPE.BF_API_ACTION_DELETED).ToString();
            }
        }
        public string ObjPcell
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_PCELL).ToString();
            }
        }

        public string ObjLibrary
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_LIB).ToString();
            }
        }

        public string ObjRecipe
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_MASTERRECIPE).ToString();
            }
                
        }

        public string ObjFormulaCat
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_FORMULACAT).ToString();
            }
        }

        public string ObjFormula
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_FORMULA).ToString();
            }
        }

        public string ObjOrderCat
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_ORDERCAT).ToString();
            }
        }

        public string ObjOrder
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_ORDER).ToString();
            }    
        }

        public string ObjBatch
        {
            get
            {
                 return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_BATCH).ToString();
            }
        }

        public string ObjMaterial
        {
            get
            {
                return ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_MATERIAL).ToString();
            }
        }

        public string LogbUserName
        {
            get
            {
                return UI_TXTLOGBUSERNAME.SelectedItem.ToString();
            }
        }

        public string LogbStartTime
        {
            get
            {
                string StartdateTimeLogb = UI_DTPSTARTDATE.Value.ToString("MM.dd.yyyy H:mm:ss");
                return StartdateTimeLogb;
            }
        }

        public string LogbStartTimeW3C
        {
            get
            {
                string StartdateTimeLogb = UI_DTPSTARTDATE.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return StartdateTimeLogb;
            }
        }

        public string LogbEndTime
        {
            get
            {
                string EnddateTimeLogb = UI_DTPENDDATE.Value.ToString("MM.dd.yyyy H:mm:ss");
                return EnddateTimeLogb;
            }
        }

        public string LogbEndTimeW3C
        {
            get
            {
                string EnddateTimeLogb = UI_DTPENDDATE.Value.ToString("yyyy-MM-ddT HH:mm:ss.sssZ");
                return EnddateTimeLogb;
            }
        }
        public string LbObjectHdl
        {
            get
            {
                return Extract_Hdl(UI_TXTOBJECTHDL.Text);
            }
        }

        public string OutPutFileAndPath
        {
            get
            {
                return UI_TXTOUTPUTPATHANDFILE.Text + ".sbb";
            }
            set 
            {
                UI_TXTOUTPUTPATHANDFILE.Text = value;
            }
        }

        public string TemplatePath
        {
            get
            {
                return Extract_Hdl(UI_CBTEMPLATEPATHANDFILE.Text);
            }
        }

        public string PDFPath
        {
            get
            {
                string temp;
                if (Counter > 0) 
                {
                    temp = UI_TXTOUTPUTPATHANDFILE.Text + "_"  + Counter.ToString() + "_" + BatchID +"_" + LcId + "_" + TimeZonekeyName + ".pdf";
                }
                else
                {
                    temp = UI_TXTOUTPUTPATHANDFILE.Text + "_" + BatchID + "_" + LcId + "_" + TimeZonekeyName + ".pdf";
                }
                return temp;
            }
        }

        public string LcId
        {
            get
            {
                ComboBoxItem item = (ComboBoxItem)UI_CBLCID.SelectedItem;
                string value = (string)item.Tag;
                return value;
            }
        }

        public string TimeZonekeyName
        {
            get
            {
                return UI_CBTIMEZONE.Text; 
            }
            
        }

        public string BatchID
        {
            get
            {
                int index = LbObjectHdl.LastIndexOf('/');
                return LbObjectHdl.Substring(index + 1);
            }
        }

        public string ParameterName1
        {
            get
            {
                return UI_TXTPARANAME1.Text;
            }
        }

        public string ParameterValue1
        {
            get
            {
                return UI_TXTPARAVALUE1.Text;
            }
        }

        public string ParameterName2
        {
            get
            {
                 return UI_TXTPARANAME2.Text;
            }
        }

         public string ParameterValue2
         {
             get
             {
                 return UI_TXTPARAVALUE2.Text;
             }
         }
        
        #endregion

         #region COMMAND MANAGER

         public string ErrorCode1
         {
             get
             {
                 return UI_TXTERRORCODE1.Text;
             }
         }

         public string LanguageID
         {
             get
             {
                 return UI_TXTLANGUAGEID.Text;
             }
         }

         public string AckBatchHdl1
         {
             get
             {
                 return Extract_Hdl(UI_TXTBATCHHDL_1.Text);
             }
             set
             {
                 if (value == null) 
                 {
                     throw new ArgumentNullException("value");
                 }
                 UI_TXTBATCHHDL_1.Text = value.ToString();
             }
         }

         public string AckBatchHdl2
         {
             get
             {
                 return Extract_Hdl(UI_TXTBATCHHDL_2.Text);
             }
             set
             {
                 if (value == null)
                 {
                     throw new ArgumentNullException("value");
                 }
                 UI_TXTBATCHHDL_2.Text = value.ToString();
             }
         }

         public string AckBatchHdl3
         {
             get
             {
                 return Extract_Hdl(UI_TXTBATCHHDL_3.Text);
             }
             set
             {
                 if (value == null)
                 {
                     throw new ArgumentNullException("value");
                 }
                 UI_TXTBATCHHDL_3.Text = value.ToString();
             }
         }

         public string AckContID
         {
             get
             {
                 return UI_TXTCONTID.Text;
             }
         }

         public string AckTermID
         {
             get
             {
                 return UI_TXTTERMID.Text;
             }
         }

         public string Cookie
         {
             get
             {
                 return UI_TXTCOOKIE.Text;
             }
         }

         public string PCellHdl_
         {
             get
             {
                 return Extract_Hdl(UI_TXTPCELLHDL.Text);
             }
         }

         public string ArchiveType
         {
             get
             {
                 ComboBoxItem item = (ComboBoxItem)UI_CBARCHIVETYPE.SelectedItem;
                 string value = (string)item.Tag;
                 return value;
             }
         }

         public string PCellPartID
         {
             get
             {
                 return UI_TXTPCELLPARTID.Text;
             }
         }
         #endregion
         #region AUDIT TRAIL

         public int UserInfoCount
         {
             get
             {
                 return UI_LSTUSERINFO.Items.Count;
             }
         }
         #endregion
        
         #region After command filling

         // Fill Controls on GUI with Values from response
        // Argument Hdls and Values is filled in XmlHandler.ReadXMLDocumentRecursive()
        public void FillGUIWithValues(string Command, List<string> Hdls, List<Dictionary<string, string>> Values)
        {
            if (Hdls.Count > 0 || Values.Count > 0)
            {
                if (Values.Count > 0)
                {
                    // Special Controls get filled with attributes stored in Values
                    // Only the first Value (list of attributes) is used

                    Dictionary<string, string> Attributes = Values[0];

                    if (Attributes.ContainsKey("size"))
                    {
                        UI_TXTBATCHSIZE.Text = Attributes["size"];
                    }
                   
                    int temp;
                    if (Attributes.ContainsKey("usage"))
                        if(int.TryParse(Attributes["usage"], out temp))
                         MaterialUsage= temp;
                    
                    if (Attributes.ContainsKey("state"))
                        if(int.TryParse(Attributes["state"], out temp))
                         BatchState = temp;
                    
                    if (Attributes.ContainsKey("exstate"))
                        if(int.TryParse(Attributes["exstate"], out temp))
                         BatchExState = temp;
                    
                    if (Attributes.ContainsKey("chaininfo"))
                        UI_CHCKCONTINUE.IsChecked = Attributes["chaininfo"] == "1" ? true : false;

                    if (Attributes.ContainsKey("session"))
                    {
                        UI_SessionID.Text = Attributes["session"];
                        HighlightControlEffect(UI_SessionID);
                    }

                    if (Command == "GetBatchStartData")
                    {
                         if (Attributes.ContainsKey("startmode"))
                             foreach (ComboBoxItem item in UI_CBSTARTMODE.Items)
                             {
                                 if (((string)item.Tag) == Attributes["startmode"]) item.IsSelected = true;
                                 break;
                             }

                        DateTime temp_date;
                        if (Attributes.ContainsKey("planstart"))
                            if(DateTime.TryParse(Attributes["planstart"], out temp_date))
                                UI_DTPPLANSTARTDATE.Value = temp_date;

                        if (Attributes.ContainsKey("planend"))
                            if(DateTime.TryParse(Attributes["planend"], out temp_date))
                                UI_DTPPLANENDDATE.Value = temp_date;

                        if (Attributes.ContainsKey("actstart"))
                            if(DateTime.TryParse(Attributes["actstart"], out temp_date))
                                UI_DTPACTSTARTDATE.Value = temp_date;

                        if (Attributes.ContainsKey("actend"))
                            if(DateTime.TryParse(Attributes["actend"], out temp_date))
                                UI_DTPACTENDDATE.Value = temp_date;
                    }
                    else if (Command == "GetAllocations" && Values.Count >= 2)
                    {
                        Dictionary<string, string> values = Values[1];
                        UI_TXTCUR_ALLOCATION.Text = values.ImplodeDic();

                        UI_CBTRP.Items.Clear();
                        UI_CBNEW_ALLOCATION.Items.Clear();

                        foreach (Dictionary<string, string> lists in Values)
                        {
                            if (lists.ContainsKey("valueid"))
                                UI_CBTRP.Items.Add(lists.ImplodeDic());

                            else if (lists.ContainsKey("hdl"))
                                UI_CBNEW_ALLOCATION.Items.Add(lists.ImplodeDic());
                        }

                        UI_CBTRP.SelectedIndex = UI_CBNEW_ALLOCATION.SelectedIndex = 0;
                    }
                    else if (Command == "GetAttribute4Object")
                    {
                        UI_CBATTRIBUTE_NAMES.Items.Clear();
                        UI_CBATTRIBUTE_VALUES.Items.Clear();
                        foreach (KeyValuePair<string, string> attrib in Attributes)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = attrib.Key;
                            item.Tag = attrib.Value;
                            UI_CBATTRIBUTE_NAMES.Items.Add(item);
                        }

                        UI_CBATTRIBUTE_NAMES.SelectedIndex = 0;
                    }
                }
            }

            try
            {
                List<UIElement> controls = GetControls4Command(Command);
                FillGUIWithValues(controls, Hdls, Values);
            }
            catch (Exception e)
            {
                if(Hdls.Count > 0 || Values.Count > 0)
                    MessageBox.Show("Exception occured in Operation FillGUIWithValues with Command " + Command + ":\n" + e.Message);
            }
        }

        /// <summary>
        /// Fills all combobox and textbox controls in a list with Hdls or Values.
        /// </summary>
        /// <param name="controls">List of combobox and textbox controls.</param>
        /// <param name="Hdls">A list of handles in string format.</param>
        /// <param name="Values">A list of a dictionary containg the values in name=value format.</param>
        void FillGUIWithValues(List<UIElement> controls, List<string> Hdls, List<Dictionary<string, string>> Values)
        {
            List<ComboBox> comboboxes = controls.Where(control => control.GetType() == typeof(ComboBox)).Cast<ComboBox>().ToList();
            comboboxes.ForEach(control => ((ComboBox)control).Items.Clear());
            if (Hdls.Count > 0)
            {
                foreach (UIElement control in controls)
                {
                    if (control.GetType() == typeof(ComboBox))
                    {
                        ComboBox combobox = (ComboBox)control;
                        Values.Zip(Hdls, (v, k) => new { v, k }).ToList().ForEach(item =>
                        {
                            string text = item.v.ImplodeDic();
                            combobox.Items.Add(text);
                        });
                    }
                    else if (control.GetType() == typeof(TextBox))
                    {
                        TextBox box = (TextBox)control;
                        if (Values.Count > 0)
                            box.Text = Values.ElementAt(0).ImplodeDic();
                        else
                            box.Text = Hdls[0];
                    }
                    else
                    {
                        throw new NotImplementedException("Control of type " + control.GetType().ToString() + " is not implemented in MainWindow.xaml.cs!");
                    }
                }
            }
            else
            {
                Values.ForEach(value =>
                {
                    //comboBoxItem cbitem = new ComboBoxItem();
                    //cbitem.Content = value;
                    if (Values.IndexOf(value) == 0)
                        controls.Where(control => control.GetType() == typeof(TextBox)).ToList().ForEach(box => ((TextBox)box).Text = value.ImplodeDic());
                    comboboxes.ForEach(control => control.Items.Add(value.ImplodeDic()));
                });
            }
            if (comboboxes.Count > 0)
                comboboxes[0].SelectedIndex = 0;

            foreach (UIElement control in controls)
            {
                HighlightControlEffect(control);
            }
        }

        /// <summary>
        /// Defines the combobox and textbox controls to fill after a particular command was invoked.
        /// </summary>
        /// <param name="Command">The command that had been executed.</param>
        /// <returns>The list of controls.</returns>
        List<UIElement> GetControls4Command(string Command)
        {
            List<UIElement> controls = new List<UIElement>();
            switch (Command)
            {
                #region INIT
                case "GetAllProjects":
                    //controls.Add(UI_CBPROJECTS);
                    controls.Add(UI_CBALLPROEJCTS);
                    break;
                case "GetAllPCells4Project":
                    controls.Add(UI_CBPCELLS);
                    controls.Add(UI_CBALLPCELLS4PROJECT);
                    break;
                case "GetAllUnitClasses4PCell":
                    controls.Add(UI_CBALLUNITCLASSES4PCELL);
                    break;
                case "GetAllUnits4UnitClass":
                    controls.Add(UI_CBALLUNITS4UNITCLASS);
                    break;
                case "GetAllDataTypes4PCell":
                    controls.Add(UI_CBALLDATATYPES4PCELL);
                    break;
                case "GetAllPhysicalUnits4PCell":
                    controls.Add(UI_CBALLPHYSICALUNITS4PCELL);
                    break;
                #endregion

                #region RECIPE_FORMULA
                case "CopyRecipe":
                case "GetAllMR4PCell":
                    controls.Add(UI_CBMRs2);
                    break;
                case "GetMR4Formula":
                    controls.Add(UI_CBMRs3);
                    break;
                case "GetFormulaCat4MR":
                case "CreateFormulaCat":
                    controls.Add(UI_CBFORMULACATS);
                    break;
                case "GetAllFormulaCats4PCell":
                    controls.Add(UI_CBFORMULACATS4PCELL);
                    break;
                case "GetAllFormulas4FormulaCat":
                    controls.Add(UI_CBFORMULAS);
                    break;
                case "GetAllFormulas4MR":
                    controls.Add(UI_CBFORMULAS4MR);
                    break;
                case "GetProductData4Object":
                    controls.Add(UI_CBPRODUCTDATA4OBJECT);
                    break;
                case "CreateFormula":
                case "CopyFormula":
                    controls.Add(UI_CBFORMULAS);
                    controls.Add(UI_CBFORMULAS);
                    break;
                #endregion

                #region ORDER_BATCH
                case "GetAllOrderCats4PCell":
                    controls.Add(UI_CBALLORDERCATS4PCELL);
                    break;
                case "GetAllOrders4OrderCat":
                    controls.Add(UI_CBALLORDERS4ORDERCAT);
                    break;
                case "GetAllBatches4Order":
                case "CreateBatch":
                case "CopyBatch":
                    controls.Add(UI_CBALLBATCHES4ORDER);
                    break;
                case "GetAllFormulasOrMR4CreateBatch":
                    controls.Add(UI_CBFORMORMR4CREATEBATCH);
                    break;
                case "CreateOrderCat":
                    controls.Add(UI_CBALLORDERCATS4PCELL);
                    break;
                case "CreateOrder":
                case "CopyOrder":
                    controls.Add(UI_CBALLORDERS4ORDERCAT);
                    break;
                case "GetAllChainRoots4PCell":
                case "GetAllChainedPredecessors4Batch":
                case "GetAllChainedSuccessors4Batch":
                    controls.Add(UI_CBCHAINEDBATCHES);
                    break;
                #endregion

                #region MATERIAL_QUALITY
                case "GetAllMaterials4PCell":
                case "CreateMaterial":
                case "CreateManyMaterials":
                    controls.Add(UI_CBMATERIALS);
                    break;
                case "IsMaterialReferenced":
                    controls.Add(UI_CBMATERIALREFERENCED);
                    break;
                case "GetMaterialData":
                    controls.Add(UI_CBMATERIALDATA);
                    break;
                case "GetAllQualities4Material":
                case "CreateQuality":
                    controls.Add(UI_CBALLQUALITIES4MATERIAL);
                    break;
                case "GetQualityData":
                    controls.Add(UI_CBQUALITYDATA);
                    break;
                #endregion

                #region ALLOCATION PARAMETER
                case "GetParameter":
                    controls.Add(UI_CBPARAMETER);
                    break;
                #endregion
                #region SUBFOLDER
                case "GetAllSubfolders4PCell":
                    controls.Add(UI_CBSUBFOLDER4PCELL);
                    break;
                case "GetAllLIB4PCell":
                    controls.Add(UI_CBGETALLLIBFORPCEEL);
                    break;
                case "CreateSubfolder":
                    break;
                #endregion

                #region COMMON FUNCTIONS
                case "GetParent":
                    controls.Add(UI_TXTHDLINPUT);
                    break;
                case "GetAttribute4Object":
                    controls.Add(UI_CBATTRIBUTE4OBJECT);
                    break;
                #endregion

                #region API|SIS EVENTS
                case "GetAllEventDefinitions":
                    controls.Add(UI_CBALLEVENTDEFINITIONS);
                    break;
                case "GetAllSubscriptions":
                    controls.Add(UI_CBALLSUBSCRIPTIONS);
                    break;
                case "GetAllActiveXFERPhase4PCell":
                    break;
                #endregion
                #region LOGBOOKPRINT
                case "GetLogBookObjects":
                    controls.Add(UI_CBRESULT);
                    break;
                case "GetAllPrintTemplates4PCell":
                    controls.Add(UI_CBTEMPLATEPATHANDFILE);
                    break;
                #endregion

                #region HISTORICAL SIS EVENTS
                case "GetChangedBatches":
                    controls.Add(UI_CBHSISCHANGEDBATCHES);
                    break;
                #endregion

                case "GetChildren":
                    controls.Add(UI_CBCHILDREN);
                    controls.Add(UI_CBALLHSISUNITS);
                    break;
                case "Init":
                case "GetBatchState":
                case "GetBatchStartData":
                case "GetBatchSize":
                case "GetAllocations":
                case "SetParameter":
                case "GetObjectHeader":
                case "GetObjectState":
                    // No control needed
                    break;
                default:
                    throw new NotImplementedException("Command " + Command + " has not been implemented in MainWindow.xaml.cs!");
            }
            return controls;
        }

        #endregion


        #region Effects

        void HighlightControlEffect(UIElement control)
        {
            HighlightControlEffect(control, 0);
        }
        void HighlightControlEffect(UIElement control, int wait_time)
        {
            Storyboard sb = this.FindResource("UI_CBStoryB") as Storyboard;
            ColorAnimationUsingKeyFrames animation = (ColorAnimationUsingKeyFrames)sb.Children[0];
            Color color = Colors.White;
            string control_style = "";
            if (control.GetType() == typeof(ComboBox))
                control_style = "COMBOBOX";
            else if (control.GetType() == typeof(CheckBox))
                control_style = "CHECKBOX";
            else if (control.GetType() == typeof(TextBox))
                control_style = "TEXTBOX";
            try
            {
                Style test = (Style)FindResource(control_style);
                var res = test.Setters.First(setter => ((Setter)setter).Property.Name == "Background");
                if (res != null)
                    color = (Color)ColorConverter.ConvertFromString(((Setter)res).Value.ToString());
            }
            catch { }
            animation.KeyFrames[animation.KeyFrames.Count - 1].Value = color;
            Storyboard.SetTarget(sb, control);
            sb.BeginTime = new TimeSpan(0, 0, 0, 0, wait_time);
            sb.Begin(this, true);
        }

        #endregion

        public bool FindAndKillProcess(string proName) 
        {
            foreach( Process pro in Process.GetProcesses() )
            {
                if( pro.ProcessName.StartsWith(proName))
                {
                    try
                    {
                        pro.Kill();
                    }
                    catch 
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        #region R O U T E D  E V E N T S

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_INIT, UI_INIT_TAB);
            ViewModelBase context = ((ViewModelBase)DataContext);
            context.APIInvokes.ReadAllSettings();
            UI_TXTDOMAIN.Text = Domain;
            UI_TXTCOMPUTER.Text = ComputerName;
            // Only used for debug, executes main functions on start up

           /*ViewModelBase context = ((ViewModelBase)DataContext);
           context.APIInvokes.InitAPI();
            context.APIInvokes.InvokeCommand("GetAllProjects",true, bfapicmx_CApiInvocation.CommandType.Get);
            context.APIInvokes.InvokeCommand("GetAllPCells4Project", true, bfapicmx_CApiInvocation.CommandType.Get);
            context.APIInvokes.InvokeCommand("GetAllMR4PCell", true, bfapicmx_CApiInvocation.CommandType.Get);
            UI_TXTPASSWORD.Password = "superuser";
            context.APIInvokes.InvokeCommand("SetCurrentUser",true, bfapicmx_CApiInvocation.CommandType.Set);*/
        }

        // Logic for showing different tabs (e.g. Init, Recipe_Formula)

        private void UI_INIT_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_INIT, UI_INIT_TAB);
        }

        private void UI_RECIPE_FORMULA_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_RECIPEFORMULA, UI_RECIPE_FORMULA_TAB);
        }

        private void UI_ORDER_BATCH_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_ORDERBATCH, UI_ORDER_BATCH_TAB);
        }

        private void UI_MATERIAL_QUALITY_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_MATERIALQUALITY, UI_MATERIAL_QUALITY_TAB);
        }

        private void UI_PARAMETER_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_ALLOCATIONPARAMETER, UI_PARAMETER_TAB);
        }

        private void UI_COMMON_FUNCTIONS_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_COMMONFUNCTIONS, UI_COMMON_FUNCTIONS_TAB);
        }

        private void UI_API_SIS_EVENTS_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_APISISEVENTS, UI_API_SIS_EVENTS_TAB);
        }

        private void UI_LOGBOOK_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_LOGBOOK, UI_LOGBOOK_TAB);
        }

        private void UI_PRINT_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_PRINT, UI_PRINT_TAB);
        }

        private void UI_COMMAND_MANAGER_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_COMMANDMANAGER, UI_COMMAND_MANAGER_TAB);
        }

        private void UI_AUDIT_TRAIL_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_AUDITTRAIL, UI_AUDIT_TRAIL_TAB);
        }

        private void UI_HISTORICAL_SIS_EVENTS_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_HISTORICALSISEVENTS, UI_HISTORICAL_SIS_EVENTS_TAB);
        }

        private void UI_NOTIFY_FILTER_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_NOTIFYFILTER, UI_NOTIFY_FILTER_TAB);
        }

        private void UI_SUBFOLDER_TAB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityProperty(UI_GRID_SUBFOLDER, UI_SUBFOLDER_TAB);
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        private void UI_XML_TO_SQL_TAB_Checked(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Checks or unchecks all ToggleButtons in Outputbar
        /// </summary>
        private void UI_CHECHKBOXALL_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ToggleButton chckbutn in UI_OUTPUTBAR_1.Children)
            {
                chckbutn.IsChecked = UI_CHECHKBOXALL.IsChecked;
            }

            foreach (ToggleButton chckbutn in UI_OUTPUTBAR_2.Children)
            {
                chckbutn.IsChecked = UI_CHECHKBOXALL.IsChecked;
            }
        }

        private void UI_CHECHKBOXALL_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (ToggleButton chckbutn in UI_OUTPUTBAR_1.Children)
            {
                chckbutn.IsChecked = UI_CHECHKBOXALL.IsChecked;
            }

            foreach (ToggleButton chckbutn in UI_OUTPUTBAR_2.Children)
            {
                chckbutn.IsChecked = UI_CHECHKBOXALL.IsChecked;
            }
        }

        /// <summary>
        /// Toggles word wrap in the browser controls used for Input and Output xml
        /// </summary>
        private void UI_OUTPUTWORDWRAP_Clicked(object sender, RoutedEventArgs e)
        {
            if (UI_WEBOUTPUT.IsLoaded)
            {
                try
                {
                    UI_WEBOUTPUT.InvokeScript("setoverflow");
                }
                catch (Exception e2)
                {
                    System.Windows.MessageBox.Show("Could not change word wrap:\n" + e2.Message);
                }
            }
        }

        private void UI_WEBINPUT_WORDWRAP_Click(object sender, RoutedEventArgs e)
        {
            if (UI_WEBINPUT.IsLoaded)
            {
                try
                {
                    UI_WEBINPUT.InvokeScript("setoverflow");
                }
                catch (Exception e2)
                {
                    System.Windows.MessageBox.Show("Could not change word wrap:\n" + e2.Message);
                }
            }
        }

        /// <summary>
        /// Toggles visibility of the xml viewer and treeview used for Input and Output xml
        /// </summary>
        private void UI_WEBOUTPUT_TREETOGGLE_Click(object sender, RoutedEventArgs e)
        {
            if (UI_WEBOUTPUT.Visibility == System.Windows.Visibility.Visible)
            {
                UI_WEBOUTPUT.Visibility = Visibility.Hidden;
                UI_WEBOUTPUTTREE.Visibility = Visibility.Visible;
            }
            else
            {
                UI_WEBOUTPUT.Visibility = Visibility.Visible;
                UI_WEBOUTPUTTREE.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Enables or disables the time pickers for filtering by time.
        /// Not possible using binding because of the use of WindowsForms controls.
        /// </summary>
        private void UI_CHCKTIMESTAMP_Checked(object sender, RoutedEventArgs e)
        {
            UI_DTPDATE.Enabled = UI_DTPTIME.Enabled = ((CheckBox)e.Source).IsChecked.Value;
        }

        //private void UI_CHCKADDPARAMETER_Checked(object sender, RoutedEventArgs e)
        //{
        //    //UI_DTPDATE.Enabled = UI_DTPTIME.Enabled = ((CheckBox)e.Source).IsChecked.Value;
        //}
        

        /// <summary>
        /// Highlights the controls (e.g. Name, Desc) which are necessary for
        /// a particular command.
        /// </summary>
        private void UI_MOUSEOVER4CREATEFORM(object sender, MouseEventArgs e)
        {
            Brush txt_brush;
            Brush chk_brush;
            if (e.RoutedEvent.Name == "MouseEnter")
            {
                txt_brush = chk_brush = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                txt_brush = new SolidColorBrush(Colors.White);
                chk_brush = null;
            }
            FrameworkElement elem = (FrameworkElement)e.Source;
            switch (elem.Name)
            {
                case "UI_BCreateRecipe":
                case "UI_BCopyRecipe":
                    UI_TXTRECFORMVERSION.Background = txt_brush;
                    UI_TXTRECFORMNAME.Background = UI_TXTRECFORMDESC.Background = txt_brush;
                    UI_CHCKRECFORMWITHSUBFOLDER.Background = chk_brush;
                    UI_CHCKRECFORMOPTLOCK.Background = chk_brush;
                    UI_CHKBatchML.Background = chk_brush;
                    break;
                case "UI_BCreateFormula":
                case "UI_BCopyFormula":
                    UI_TXTRECFORMNAME.Background = UI_TXTRECFORMDESC.Background = txt_brush;
                    UI_TXTRECFORMVERSION.Background = txt_brush;
                    UI_CHCKRECFORMOPTLOCK.Background = chk_brush;
                    break;
                case "UI_BCreateOrder":
                    UI_TXTORDERBATCHNAME.Background = UI_TXTORDERBATCHDESC.Background = txt_brush;
                    UI_CHCKORDERBATCHOPTLOCK.Background = chk_brush;
                    UI_TXTORDERBATCHSIZE.Background = txt_brush;
                    break;
                case "UI_BCopyOrder":
                    UI_TXTORDERBATCHNAME.Background = UI_TXTORDERBATCHDESC.Background = txt_brush;
                    UI_CHCKORDERBATCHOPTLOCK.Background = chk_brush;
                    UI_TXTORDERBATCHSIZE.Background = txt_brush;
                    break;
                case "UI_BCreateOrderCat":
                    UI_CHCKORDERBATCHWITHSUBFOLDER.Background = chk_brush;
                    UI_TXTORDERBATCHNAME.Background = UI_TXTORDERBATCHDESC.Background = txt_brush;
                    UI_CHCKORDERBATCHOPTLOCK.Background = chk_brush ;
                    break;
                case "UI_BCreateBatch":
                    UI_TXTORDERBATCHNAME.Background = UI_TXTORDERBATCHDESC.Background = txt_brush;
                    UI_CHCKORDERBATCHOPTLOCK.Background = chk_brush;
                    UI_TXTORDERBATCHSIZE.Background = txt_brush;
                    UI_CBFORMORMR4CREATEBATCH.Background = txt_brush;
                    break;
                case "UI_BCopyBatch":
                     UI_TXTORDERBATCHNAME.Background = UI_TXTORDERBATCHDESC.Background = txt_brush;
                    UI_CHCKORDERBATCHOPTLOCK.Background = chk_brush ;
                    break;
                case "UI_BCreateFormulaCat":
                    UI_CHCKRECFORMWITHSUBFOLDER.Background = chk_brush;
                    UI_TXTRECFORMNAME.Background = UI_TXTRECFORMDESC.Background = txt_brush;
                    UI_CHCKRECFORMOPTLOCK.Background = chk_brush;
                    break;
                case "UI_BGetallocations":
                    UI_CBALLOCATIONS.Background = txt_brush;
                    break;
                case "UI_BGetchildren":
                    UI_TXTHDLINPUT.Background = txt_brush;
                    break;
                case "UI_BCreateSubfolder":
                    UI_TXTSUBFOLDERNAME.Background = txt_brush;
                    UI_CBSUBFOLDER4PCELL.Background = txt_brush;
                    break;
                case "UI_BPrintObject":
                    UI_TXTOBJECTHDL.Background = txt_brush;
                    UI_CHCKADDPARAMETER.Background = chk_brush;
                    break;
                case "UI_BGetObjectHeader":
                    UI_TXTHDLINPUT.Background = txt_brush;
                    UI_CHCKWITHPARAMETER.Background = chk_brush;
                    UI_TXTINTO.Background = txt_brush;
                    break;
                case "UI_BGetObjectData":
                    UI_TXTHDLINPUT.Background = txt_brush;
                    UI_CBARCHIVE.Background = txt_brush;
                    UI_TXTINTO.Background = txt_brush;
                    break;
                case "UI_BCreateMaterial":
                case "UI_BSetMaterialData":
                    UI_TXTMATCODE.Background = txt_brush;
                    UI_CHCKMATWITHSUBFOLDER.Background = chk_brush;
                    UI_CBMATERIAL_USAGE.Background = txt_brush;
                    UI_TXTMATNAME.Background = UI_TXTMATDESC.Background = txt_brush;
                    UI_CHCKMATOPTLOCK.Background = chk_brush;
                    break;
                case "UI_BSetQualityData":
                    UI_TXTMATCODE.Background = txt_brush;
                    UI_CBMATERIAL_USAGE.Background = txt_brush;
                    UI_TXTMATNAME.Background = UI_TXTMATDESC.Background = txt_brush;
                    UI_CHCKMATOPTLOCK.Background = chk_brush;
                    break;
                case "UI_BCreateQuality":
                    UI_TXTMATNAME.Background = UI_TXTMATDESC.Background = txt_brush;
                    UI_TXTMATCODE.Background = txt_brush;
                    UI_CHCKMATOPTLOCK.Background = chk_brush;
                    break;
                default:
                    break;
            }
        }

        // Dialogs are not available in WPF, so using the Win32 wrapper.
        // Used for the function CreateManyMaterials.
        Microsoft.Win32.OpenFileDialog dlg = null;
        private void UI_BMaterialsBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (dlg == null)
            {
                dlg = new Microsoft.Win32.OpenFileDialog();
            }
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
            dlg.DefaultExt = "*.xml";
            dlg.Filter = "Xml documents (.xml)|*.xml";
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog().Value)
            {
                string filename = dlg.FileName;
                UI_TXTMANYMATERIALSPATH.Text = filename;
            }
        }

        // Dialogs are not available in WPF, so using the Win32 wrapper.
        // Used for the function SetManyParameter.
        private void UI_BParameterBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (dlg == null)
            {
                dlg = new Microsoft.Win32.OpenFileDialog();
            }
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
            dlg.DefaultExt = "*.xml";
            dlg.Filter = "Xml documents (.xml)|*.xml";
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog().Value)
            {
                string filename = dlg.FileName;
                UI_TXTMANYPARAMETERPATH.Text = filename;
            }
        }
        
        // Workaround for a bug:
        // When the grid splitters are dragged further than the MinHeight of a 
        // row, the grid will increase in size to keep the height of the row.
        // The bug is solved by scaling the heighest row to the right height.
        private void UI_GridIO_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double target_grid_height = this.ActualHeight - 24; // 24: (target distance between grid and window)

            double total_height = 0; 
            RowDefinition heighest_row = null;
            foreach (RowDefinition row in UI_GridIO.RowDefinitions)
            {
                if (heighest_row == null || row.ActualHeight > heighest_row.ActualHeight)
                    heighest_row = row; // find heighest row
                total_height += row.ActualHeight; // sum-up row heights
                if (total_height > target_grid_height)
                { 
                    // resize to calculated target height
                    double target_row_height = target_grid_height - (total_height - heighest_row.ActualHeight);
                    heighest_row.Height = new GridLength(target_row_height);
                }
            }
        }

        /// <summary>
        /// When closing save some values that have been changed on the GUI.
        /// </summary>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            bfapicmx_csharpsamplex.Properties.Settings.Default.Save();

            try
            {
                ViewModelBase context = ((ViewModelBase)DataContext);
                context.APIInvokes.ExitAPI();
                Dispatcher.InvokeShutdown();
            }
            catch (Exception f)
            {
                MessageBox.Show("Could not exit the API:\n" + f.Message);
            }
        }

        #endregion


        #region P R I V A T E  F U N C T I O N S

        /// <summary>
        /// Implements a Tab-Feature by changing the center container when clicking the buttons on the left.
        /// </summary>
        private void ChangeVisibilityProperty(ContentControl _UIGrid, ToggleButton _UIToggle)
        {
            foreach (UIElement _UIElement in UI_COMMONCANVAS.Children)
            {
                if (_UIElement.GetType() == typeof(ContentControl))
                {
                    if (((ContentControl)_UIElement).Tag == _UIGrid.Tag)
                    {
                        _UIElement.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        _UIElement.Visibility = Visibility.Collapsed;
                    }
                }
            }

            foreach (UIElement _uielement in UI_TABCANVAS.Children)
            {
                if (_uielement.GetType() == typeof(ToggleButton))
                {
                    if (((ToggleButton)_uielement).Tag == _UIToggle.Tag)
                    {
                        ((ToggleButton)_uielement).IsChecked = true;
                    }
                    else
                    {
                        ((ToggleButton)_uielement).IsChecked = false;
                    }
                }
            }
            _UIToggle.IsChecked = true;
        }

        /// <summary>
        /// Extracts the value of the 'hdl' attribute from a text (e.g. hdl=xyz)
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>The handle.</returns>
        string Extract_Hdl(string text)
        {
            string hdl = "";
            hdl = Extract_Value("hdl", text);
            if (hdl == null)
            {
                string[] parts = text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    hdl = parts[0];
                    if (hdl.StartsWith("'") && hdl.EndsWith("'")) 
                    {
                        foreach (char char2Trim in hdl)
                        {
                            if (char2Trim == hdl[0] || char2Trim == hdl[hdl.Length - 1])
                            {
                                hdl = hdl.TrimStart(hdl[0]);
                                hdl = hdl.TrimEnd(hdl[hdl.Length - 1]);
                                return hdl;
                            }
                        }
                    }
                }
                else
                {
                    parts = text.Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                        hdl = parts[0];
                    else
                        hdl = text;
                }
            }
            return hdl;
        }

        /// <summary>
        /// Extracts the value of a specified attribute from a text (e.g. state=23, name=xyz)
        /// </summary>
        /// <param name="prefix">The name of the attribute to search (e.g. state, name)</param>
        /// <param name="text">The text to parse.</param>
        /// <returns>The handle.</returns>
        string Extract_Value(string prefix, string text)
        {
            Regex regex = new Regex(prefix + "='?([^',]+)");
            Match match = regex.Match(text);
            if (match.Success)
            {
                string new_value = match.Result("$1");
                return new_value;
            }
            else
                return null;
        }

        /// <summary>
        /// Adds a user in the listbox of Logbook-Print.
        /// </summary>
        private void UI_BLOGBADDUSER_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UI_TXTLOGBUSERNAME.Text))
                //UI_LBLOGBUSERNAMES.Items.Add(new ListBoxItem { Content = UI_TXTLOGBUSERNAME.Text });
                UI_TXTLOGBUSERNAME.Items.Add(new ComboBoxItem { Content = UI_TXTLOGBUSERNAME.Text });
            UI_TXTLOGBUSERNAME.Text = "";
            UI_TXTLOGBUSERNAME.Focus();
        }

        /// <summary>
        /// Removes a user in the listbox of Logbook-Print.
        /// </summary>
        private void UI_BLOGBREMOVEUSER_Click(object sender, RoutedEventArgs e)
        {
            //int index = UI_LBLOGBUSERNAMES.SelectedIndex;
            int index = UI_TXTLOGBUSERNAME.SelectedIndex;
            if (index != -1)
            {
                //UI_LBLOGBUSERNAMES.Items.RemoveAt(index);
                //UI_LBLOGBUSERNAMES.SelectedIndex = Math.Min(UI_LBLOGBUSERNAMES.Items.Count - 1, index);
                UI_TXTLOGBUSERNAME.Items.RemoveAt(index);
                UI_TXTLOGBUSERNAME.SelectedIndex = Math.Min(UI_TXTLOGBUSERNAME.Items.Count - 1, index);
            }
        }

        /// <summary>
        /// Executes the Auto function.
        /// </summary>
        private void UI_BAuto_Click(object sender, RoutedEventArgs e)
        {
            ViewModelBase context = ((ViewModelBase)DataContext);
            context.APIInvokes.Auto();
        }

        #endregion

        // clean the Notification Window
        private void UI_TXTNOTIFICATIONS_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Notification.Items.Clear();
        }

        // For GetObjectdata
        private void UI_CHCKDEFAULT_Unchecked(object sender, RoutedEventArgs e)
        {
            string value = ArchiveTag;
            switch (value)
            {
                case "0"://"6.0.4 (Archive V1)":
                    DefaultCheckInvisible();
                    break;
                case "1"://"6.1.3 (Archive V2)":
                    DefaultCheckInvisible();
                    break;
                case "2"://"7.0.1 (Archive V3)":
                    UnchekVisible();
                    UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Hidden;
                    UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Hidden;
                    UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Hidden;
                    UI_CHCKESIG.IsChecked = false;
                    UI_CHCKACTION.IsChecked = false;
                    break;
                case "3"://"7.0.7 (Archive V4)":
                    UnchekVisible();
                    UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Visible;
                    UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Hidden;
                    UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Hidden;
                    UI_CHCKESIG.IsChecked = true;
                    UI_CHCKACTION.IsChecked = true;
                    UI_CHCKSFCSTRUCTURE.IsChecked = true;
                    break;
                case "4"://"7.1.2 (Archive V5)":
                    UnchekVisible();
                    UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Visible;
                    UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Visible;
                    UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Hidden;
                    UI_CHCKALLOCATIONS.IsChecked = true;
                    break;
                case "5"://"8.0.0 (Archive V6)":
                case "6"://"8.0.1 (Archive V7)":
                case "7"://"8.1.0 (Archive V8)":
                case "8"://"8.2.0 (Archive V9)":
                    UnchekVisible();
                    UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Visible;
                    UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Visible;
                    UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Visible;
                    UI_CHCKESIG.IsChecked = true;
                    UI_CHCKACTION.IsChecked = true;
                    UI_CHCKALLOCATIONS.IsChecked = true;
                    UI_CHCKEXTINFO.IsChecked = true;
                    break;
                case "":
                    DefaultCheckInvisible();
                    break;
            }
        }

        private void UI_CHCKDEFAULT_Checked(object sender, RoutedEventArgs e)
        {
            string value = ArchiveTag;
            switch (value)
            {
                case "0"://"6.0.4 (Archive V1)":
                case "1"://"6.1.3 (Archive V2)":
                case "2"://"7.0.1 (Archive V3)":
                case "3"://"7.0.7 (Archive V4)":
                case "4"://"7.1.2 (Archive V5)":
                case "5"://"8.0.0 (Archive V6)":
                case "6"://"8.0.1 (Archive V7)":
                case "7"://"8.1.0 (Archive V8)":
                case "8"://"8.2.0 (Archive V9)":
                case "":
                    DefaultCheckInvisible();
                    break;
            }
        }
        
        // For GetObjectdata paramter choice
        private void UI_CBARCHIVE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = ArchiveTag;
            switch (value)
            {
                case "0"://"6.0.4 (Archive V1)":
                    DefaultCheckInvisible();
                    if (UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Hidden
                        && UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Hidden)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Visible;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Visible;
                    }
                    else { ;}
                    if (UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    break;
                case "1"://"6.1.3 (Archive V2)":
                    DefaultCheckInvisible();
                    if (UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Hidden)
                    {
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    { ; }
                    if (UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Visible
                        && UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    break;
                case "2"://"7.0.1 (Archive V3)":
                    if (UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    if (UI_CHCKDEFAULT.IsChecked == false)
                    {
                        UnchekVisible();
                        UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKESIG.IsChecked = false;
                        UI_CHCKACTION.IsChecked = false;
                    }
                    else 
                    {
                        DefaultCheckInvisible();
                    }
                    break;
                case "3"://"7.0.7 (Archive V4)":
                    if (    UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    if (UI_CHCKDEFAULT.IsChecked == false)
                    {
                        UnchekVisible();
                        UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Visible;
                        UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKESIG.IsChecked = true;
                        UI_CHCKACTION.IsChecked = true;
                        UI_CHCKSFCSTRUCTURE.IsChecked = true;
                    }
                    break;
                case "4"://"7.1.2 (Archive V5)":
                    if (UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    if (UI_CHCKDEFAULT.IsChecked == false)
                    {
                        UnchekVisible();
                        UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Visible;
                        UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Visible;
                        UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKALLOCATIONS.IsChecked = true;
                    }
                    break;
                case "5"://"8.0.0 (Archive V6)":
                case "6"://"8.0.1 (Archive V7)":
                    if (UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Visible
                        || UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    if (UI_CHCKDEFAULT.IsChecked == false
                        && (UI_CHCKSFCSTRUCTURE.IsVisible == false
                             || UI_CHCKALLOCATIONS.IsVisible == false
                             || UI_CHCKEXTINFO.IsVisible == false))
                    {
                        UnchekVisible();
                        UI_CHCKSFCSTRUCTURE.Visibility = System.Windows.Visibility.Visible;
                        UI_CHCKALLOCATIONS.Visibility = System.Windows.Visibility.Visible;
                        UI_CHCKEXTINFO.Visibility = System.Windows.Visibility.Visible;

                    }
                    UI_CHCKESIG.IsChecked = true;
                    UI_CHCKACTION.IsChecked = true;
                    UI_CHCKALLOCATIONS.IsChecked = true;
                    UI_CHCKEXTINFO.IsChecked = true;
                    break;
                case "7"://"8.1.0 (Archive V8)":
                case "8"://"8.2.0 (Archive V9)":
                    break;
                case "":
                    DefaultCheckInvisible();
                    if (UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Visible
                        && UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    if (UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    if (UI_CHCKWITHPCELL.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKWITHPCELL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    { ; }
                    if (UI_CHCKBATCHWITHOUTTAGS.Visibility == System.Windows.Visibility.Visible
                        && UI_CHCKEXTENDEDDATA.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHCKBATCHWITHOUTTAGS.Visibility = System.Windows.Visibility.Hidden;
                        UI_CHCKEXTENDEDDATA.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    break;
            }
        }

        private void UI_CBEVENTID_POSITION_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = Mode4SisEvent;
            switch (value)
            {
                case "VALUE":
                    if (UI_CHKENSURE_CONSISTENT.Visibility == System.Windows.Visibility.Hidden)
                    {
                        UI_CHKENSURE_CONSISTENT.Visibility = System.Windows.Visibility.Visible;
                    }
                    else { ;}
                    break;
                case "START":
                    if (UI_CHKENSURE_CONSISTENT.Visibility == System.Windows.Visibility.Visible)
                    {
                        UI_CHKENSURE_CONSISTENT.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else { ;}
                    break;
                case "ACTUAL":
                    // After Start
                    if (e.RemovedItems.Count > 0) 
                    {
                        var tmp = e.RemovedItems[0];
                        ComboBoxItem unselected_item = (ComboBoxItem)tmp;
                        ComboBoxItem selected_item = (ComboBoxItem)UI_CBEVENTID_POSITION.SelectedValue;
                        if (unselected_item != selected_item)
                        {
                            if (UI_CHKENSURE_CONSISTENT.Visibility == System.Windows.Visibility.Visible)
                            {
                                UI_CHKENSURE_CONSISTENT.Visibility = System.Windows.Visibility.Hidden;
                            }
                            else { ;}
                        }
                    }//
                    break;
                default:
                    break;
            }
        }

        private void AddUserInfo_Click(object sender, RoutedEventArgs e)
        {
            ViewModelBase context = ((ViewModelBase)DataContext);
            context.APIInvokes.AddUserInfo();
            
        }

        private void cb_Checked_Click(object sender, RoutedEventArgs e)
        {
            //UserData us = new UserData();
            //if (UI_LSTUSERINFO.Items.Count > 1)
            //{
            //    for (int j = 0; j < UI_LSTUSERINFO.Items.Count; j++)
            //    {
            //        us.MyCheck.IsChecked = true;
            //    }
            //    //APIInvoke.UserInfo.check.IsChecked = true;
            //}
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UI_DTPHSIS_ENDDATE.Enabled = ((CheckBox)e.Source).IsChecked.Value;
        }

        private void UI_CHKAUTOADVISE_Checked(object sender, RoutedEventArgs e)
        {
            UI_TXTAUTOREADVISE.Visibility = System.Windows.Visibility.Visible;
        }

        private void UI_CHKAUTOADVISE_Unchecked(object sender, RoutedEventArgs e)
        {
            if (UI_TXTAUTOREADVISE.Visibility == System.Windows.Visibility.Visible)
            {
                UI_TXTAUTOREADVISE.Visibility = System.Windows.Visibility.Hidden;
            }
            else { ;}
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            UI_TXTENDEVENT.IsEnabled = ((CheckBox)e.Source).IsChecked.Value;
        }

        // Used for the function GetHistoricalSISEventsByFile.
        private void UI_BROWSE_Click(object sender, RoutedEventArgs e)
        {
            if (dlg == null)
            {
                dlg = new Microsoft.Win32.OpenFileDialog();
            }
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
            dlg.DefaultExt = "*.xml";
            dlg.Filter = "Xml documents (.xml)|*.xml";
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog().Value)
            {
                string filename = dlg.FileName;
                UI_TXTHISFILEPATH.Text = filename;
            }
        }

        private void UI_CHKEVENTID_Checked(object sender, RoutedEventArgs e)
        {
            UI_TXTEVTTID.Visibility = System.Windows.Visibility.Visible;
        }

        private void UI_CHKEVENTID_Unchecked(object sender, RoutedEventArgs e)
        {
            if (UI_TXTEVTTID.Visibility == System.Windows.Visibility.Visible)
            {
                UI_TXTEVTTID.Visibility = System.Windows.Visibility.Hidden;
            }
            else { ;}
        }

        private void UI_COMMANDEXECUTE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)UI_COMMANDEXECUTE.SelectedItem;
            string value = (string)item.Content;      
            switch(value)
            {
                case "Acknowledge":
                        UI_BDACKNOWLEDGE.Visibility = System.Windows.Visibility.Visible;
                        UI_BDALLACTIVEOPSALLOC.Visibility = System.Windows.Visibility.Hidden;
                        UI_BDALLACTIVESOPS.Visibility = System.Windows.Visibility.Hidden;
                        UI_BDALLDATA4PCELL.Visibility = System.Windows.Visibility.Hidden;
                        UI_BDERRORTEXT.Visibility = System.Windows.Visibility.Hidden;
                        UI_BDPROJSETTINGS.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "GetAllActive OperatorAllocations":
                    UI_BDALLACTIVEOPSALLOC.Visibility = System.Windows.Visibility.Visible;
                    UI_BDACKNOWLEDGE.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVESOPS.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLDATA4PCELL.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDERRORTEXT.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDPROJSETTINGS.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "GetAllActiveObjects":
                    UI_BDALLACTIVESOPS.Visibility = System.Windows.Visibility.Visible;
                    UI_BDACKNOWLEDGE.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVEOPSALLOC.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLDATA4PCELL.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDERRORTEXT.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDPROJSETTINGS.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "GetData4PCell":
                    UI_BDALLDATA4PCELL.Visibility = System.Windows.Visibility.Visible;
                    UI_BDACKNOWLEDGE.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVESOPS.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVEOPSALLOC.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDERRORTEXT.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDPROJSETTINGS.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "GetErrorText":
                    UI_BDERRORTEXT.Visibility = System.Windows.Visibility.Visible;
                    UI_BDACKNOWLEDGE.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVESOPS.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLDATA4PCELL.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVEOPSALLOC.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDPROJSETTINGS.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "GetProjectSettings":
                    UI_BDPROJSETTINGS.Visibility = System.Windows.Visibility.Visible;
                    UI_BDACKNOWLEDGE.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVESOPS.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLDATA4PCELL.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDERRORTEXT.Visibility = System.Windows.Visibility.Hidden;
                    UI_BDALLACTIVEOPSALLOC.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
        }

        private void UI_CHCKWITHSUBFOLDER_Checked(object sender, RoutedEventArgs e)
        {
            var element = FindElementByName<ComboBox>(SUBF, "UI_CBSUBFOLDER4PCELL");
            string temp = element.Text;
            if (temp == "")
            {
                MessageBox.Show("Make sure you get a SUBFOLDER before creating this object!", "Empty Subfolder",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else { ; }
        }

        // Reset the GUI after Exit API
        internal void Reset()
        {
            if (this.Content is Panel)
            {
                this.ResetPanel((Panel)this.Content);
            }
        }

        internal void ResetPanel(Panel p_Panel)
        {
            foreach (UIElement currChild in p_Panel.Children)
            {
                ResetUIElement(currChild);
            }
        }

        internal void ResetUIElement(UIElement p_UIElement)
        {
            if (null != p_UIElement)
            {
                if (p_UIElement is Panel)
                {
                    this.ResetPanel((Panel)p_UIElement);
                }
                else if (p_UIElement is Decorator)
                {
                    this.ResetUIElement(((Decorator)p_UIElement).Child);
                }
                else if (p_UIElement is ComboBox)
                {
                    ComboBox cb = (ComboBox)p_UIElement;
                    String cbName = cb.Name;
                    switch (cbName)
                    {
                        case "UI_CBTARGETTYPE":
                        case "UI_CBARCHIVE":
                        case "UI_CBOJECTSTATE":
                        case "UI_CBEVENTID_POSITION":
                        case "UI_CBLCID":
                        case "UI_CBTIMEZONE":
                        case "UI_COMMANDEXECUTE":
                        case "UI_CBSTARTMODE":
                        case "UI_CBMATERIAL_USAGE":
                        case "UI_CBPARAMETERTYPE":
                        case "UI_CBHSISCHANGED":
                            break;
                        default:
                            clearCombobox(cb);
                            break;
                    }

                }
                else if (p_UIElement is TextBox)
                {
                    TextBox tb = (TextBox)p_UIElement;
                    String tbName = tb.Name;
                    switch (tbName)
                    {
                        case "UI_TXTDUMP2FILES":
                        case "UI_TXTINTO":
                        case "UI_TXTBATCHML":
                        case "UI_TXTOUTPUTPATHANDFILE":
                        case "UI_TXTHISFILEPATH":
                        case "UI_TXTUSERNAME":
                        case "UI_TXTLONGNAME":
                        case "UI_TXTDOMAIN":
                        case "UI_TXTCOMPUTER":
                            break;
                        default:
                            tb.Clear();
                            break;
                    }
                }
                else if (p_UIElement is ContentControl)
                {
                    object oContent = ((ContentControl)p_UIElement).Content;

                    if (oContent is UIElement)
                    {
                        this.ResetUIElement((UIElement)oContent);
                    }
                }
                else if (p_UIElement is ListView)
                {
                    ListView lv = (ListView)p_UIElement;
                    lv.Items.Clear();
                }
            }
        }

        public void clearCombobox(ComboBox combo) 
        {
            combo.Items.Clear();
            combo.SelectedIndex = -1;
        }

        private void UI_BAddUnit_Click(object sender, RoutedEventArgs e)
        {
            AddAndSelectItemsHDLInCombo(UI_CBALLHSISUNITS, UI_CBHSISCHANGED);
        }

        private void UI_BAddBatch_Click(object sender, RoutedEventArgs e)
        {

            AddAndSelectItemsHDLInCombo(UI_CBHSISCHANGEDBATCHES, UI_CBHSISCHANGED);
        }

        private void UI_BClearHdl_Click(object sender, RoutedEventArgs e)
        {
            clearCombobox(UI_CBHSISCHANGED);
        }

        private void UI_CHCKSHOWEXAMPLE_Checked(object sender, RoutedEventArgs e)
        {
            //string filename ="D:/VM_206/bfapicmx_csharpsamplex/example/SBManyParameter.xml";
            //PathInfo ReturnInfo = new PathInfo();
            //ExtractInfo(filename, ReturnInfo);
            //string strFilePath = ReturnInfo.FileDrive + ReturnInfo.FilePath + ReturnInfo.FileNamewExt;
            //if (ReturnInfo.PathIsValid == true)
            //{
            //    string text = File.ReadAllText(filename, Encoding.Unicode);
            //    System.Diagnostics.Process p = new System.Diagnostics.Process();
            //    p.StartInfo.FileName = text;
            //    p.Start();
            //}
            //ViewModelBase context = ((ViewModelBase)DataContext);
            //context.APIInvokes.fill   (filename);
            
            //if (dlg == null)
            //{
            //    dlg = new Microsoft.Win32.OpenFileDialog();
            //}
            //dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
            //dlg.DefaultExt = "*.xml";
            //dlg.Filter = "Xml documents (.xml)|*.xml";
            //dlg.FilterIndex = 2;
            //dlg.CheckFileExists = true;
            //dlg.RestoreDirectory = true;
            //if (dlg.ShowDialog().Value)
            //{
                
            //    string filename = dlg.FileName;
            //    UI_TXTMANYMATERIALSPATH.Text = filename;
            //}
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            UI_DTPHSIS_STARTDATE.Enabled = ((CheckBox)e.Source).IsChecked.Value;
        }

        private void UI_CHKHISEVENTSTART_Checked(object sender, RoutedEventArgs e)
        {
            UI_TXTSTARTEVENT.IsEnabled = ((CheckBox)e.Source).IsChecked.Value;
        }

        private void UI_DTPHSIS_STARTDATE_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPHSIS_STARTDATE, HSIS_StartTime);
        }

        private void UI_DTPHSIS_ENDDATE_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPHSIS_ENDDATE, HSIS_EndTime);
        }

        private void UI_DTPEARLIER_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPEARLIER, OrderEarlierW3C);
        }

        private void UI_DTPLATER_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPLATER, OrderLaterW3C);
            
        }

        private void UI_DTPPLANSTARTDATE_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPPLANSTARTDATE, BatchPlanStartW3C);
            
        }

        private void UI_DTPPLANENDDATE_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPPLANENDDATE, BatchPlanEndW3C);
        }

        private void UI_DTPSTARTDATE_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPSTARTDATE, LogbStartTimeW3C);
        }

        private void UI_DTPENDDATE_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(UI_DTPENDDATE, LogbEndTimeW3C);
        }

        private void CLOSED_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_CLOSED";
            this.UI_CHCKBFAIEXSTATECLOSED.ToolTip = (tooltip);
        }

        private void ARCHIVED_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_ARCHIVED";
            this.UI_CHCKBFAIEXSTATEARCHIVED.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATEERROR_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_ERROR";
            this.UI_CHCKBFAIEXSTATEERROR.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATEING_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_ING";
            this.UI_CHCKBFAIEXSTATEING.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATETESTONLY_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_TEST_ONLY";
            this.UI_CHCKBFAIEXSTATETESTONLY.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATECHILDBLOCKING_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_CHILD_BLOCKING";
            this.UI_CHCKBFAIEXSTATECHILDBLOCKING.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATECHILDWAITING_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_CHILD_WAITING";
            this.UI_CHCKBFAIEXSTATECHILDWAITING.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATECHILDOPERATING_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_CHILDOPERATING";
            this.UI_CHCKBFAIEXSTATECHILDOPERATING.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATEREADYTOSTART_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_READY_TO_START";
            this.UI_CHCKBFAIEXSTATEREADYTOSTART.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATESTARTCMDRECEIVED_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_START_CMD_RECEIVED";
            this.UI_CHCKBFAIEXSTATESTARTCMDRECEIVED.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATESIGNATURE_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_SIGNATURE";
            this.UI_CHCKBFAIEXSTATESIGNATURE.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATETIMEEXPIRED_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_TIME_EXPIRED";
            this.UI_CHCKBFAIEXSTATETIMEEXPIRED.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATEWAITONSTRATALLOC_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_WAIT_ONSTARTALLOC";
            this.UI_CHCKBFAIEXSTATEWAITONSTRATALLOC.ToolTip = (tooltip);
        }

        private void UI_CHCKBFAIEXSTATEEDIT_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.ToolTip tooltip = new System.Windows.Controls.ToolTip();
            tooltip.Content = "BF_API_EXSTATE_EDIT";
            this.UI_CHCKBFAIEXSTATEEDIT.ToolTip = (tooltip);
        }

        private void UI_CHKEXTTRANSFORM_Checked(object sender, RoutedEventArgs e)
        {
            UI_TXTEXTTRANS.Visibility = System.Windows.Visibility.Visible;
        }

        private void UI_CHKEXTTRANSFORM_Unchecked(object sender, RoutedEventArgs e)
        {
            if (UI_TXTEXTTRANS.Visibility == System.Windows.Visibility.Visible)
            {
                UI_TXTEXTTRANS.Visibility = System.Windows.Visibility.Hidden;
            }
            else { ;}
        }

        private void UI_CBTRP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModelBase context = ((ViewModelBase)DataContext);
            context.APIInvokes.FillPPandScalID();
            context = null;
        }

       /* private void UI_CHCKDUMP2FILES_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog objDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult objResult = objDialog.ShowDialog();
            if (objResult == System.Windows.Forms.DialogResult.OK)
            {
                string folderName = objDialog.SelectedPath;
                UI_TXTDUMP2FILES.Text = folderName;
            }
        }*/
    }
    
}
