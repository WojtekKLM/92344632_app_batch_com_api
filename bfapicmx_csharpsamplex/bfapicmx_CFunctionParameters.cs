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
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace  Siemens.Automation.bfapicmx_csharpsamplex
{
    
    /// <summary>
    /// Defines how the xml for a particular command is to be filled which is given to the API.
    /// It is instantiated in the class ApiInvokes.
    /// </summary>
    public class CFunctionParameters
    {
        int a = 0;
        /// <summary>
        /// The enumeration defines all possible values that can be used to fill the xml.
        /// The elements are matched with the respective names and values in GetValues4Enum().
        /// </summary>
        enum XML_PARAM_NAME
        {
            // Init and general parameters
            BF_API_XML_APPVERSION,
            BF_API_XML_SESSION,
            BF_API_XML_PROJECTHDL,
            BF_API_XML_PCELLHDL,
            BF_API_XML_OUTPUTBAR,
            BF_API_XML_SYNCTIME,
            BF_API_XML_HIERARCHY,
            BF_API_XML_WILDCARD_NAME,
            BF_API_XML_PCELLHDL_ASHDL,
            BF_API_XML_UNITCLHDL,
            BF_API_XML_UNITHDL,

            BF_API_XML_NAME,
            BF_API_XML_DESC,
            BF_API_XML_SIZE,
            BF_API_XML_VERSION,
            BF_API_XML_CODE,
            BF_API_XML_OPTLOCK,

            // SetCurrentUser
            BF_API_XML_USERNAME,
            BF_API_XML_USERNAMELONG,
            BF_API_XML_PASSWORD,
            BF_API_XML_DOMAIN,
            BF_API_XML_COMPUTER,
            BF_API_XML_APPLICATIONID,
            BF_API_XML_AUDITTRAIL_HOSTNAME,
            BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP,

            // Recipe Formula
            BF_API_XML_MRHDL,
            BF_API_XML_BATCHML,
            BF_API_XML_FORMULACATHDL,
            BF_API_XML_FORMULACATHDL4PCELL,
            BF_API_XML_FORMULAHDL,
            BF_API_XML_CHILDHDL,
            BF_API_XML_WITH_BATCHML,
            BF_API_XML_PRODUCTHDL,
            BF_API_XML_RECFORMNAME,
            BF_API_XML_RECFORMDESC,
            BF_API_XML_RECFORMVERSION,
            BF_API_XML_RECFORMOPTLOCK,
            // Order Batch
            BF_API_XML_ORDERCATHDL,
            BF_API_XML_ORDERHDL,
            BF_API_XML_ORDER_EARLIEST,
            BF_API_XML_ORDER_LATEST,
            BF_API_XML_BATCHHDL,
            BF_API_XML_FORMORMR_HDL,
            BF_API_XML_STATE,
            BF_API_XML_EXSTATE,
            BF_API_XML_BATCH_STARTMODE,
            BF_API_XML_BATCH_PLANSTART,
            BF_API_XML_BATCH_PLANEND,
            BF_API_XML_BATCH_DATETIMEFORMATPLANSTART,
            BF_API_XML_BATCH_DATETIMEFORMATPLANEND,
            BF_API_XML_BATCH_PREDECESSORHDL,
            BF_API_XML_BATCH_SUCCESSORHDL,
            BF_API_XML_BATCH_CHAIN_ON_START,
            BF_API_XML_BATCH_GAPTIMEINSEC,
            BF_API_XML_ORDERBATCHNAME,
            BF_API_XML_ORDERBATCHDESC,
            BF_API_XML_ORDERBATCHSIZE,
            BF_API_XML_ORDERBATCHOPTLOCK,
            BF_API_XML_STAYINCONTINUE,
            BF_API_XML_CONTINUE,

            // Material Quality
            BF_API_XML_MATNAME,
            BF_API_XML_MATDESC,
            BF_API_XML_MATCODE,
            BF_API_XML_MATOPTLOCK,
            BF_API_XML_MATERIALHDL,
            BF_API_XML_MAT_USAGE,
            BF_API_XML_MATERIAL_XML,
            BF_API_XML_QUALITYHDL,
            BF_API_XML_QUALITYDATA,

            // Allocation Parameter
            BF_API_XML_ALLOCATIONHDL,
            BF_API_XML_ALLOCATION_NEW_HDL,
            BF_API_XML_SCALING_ID,
            BF_API_XML_PARAMETERID,
            BF_API_XML_STRATEGYTYPE_ID,
            BF_API_XML_ALLOCONSTART,
            BF_API_XML_TRPID,
            BF_API_XML_CONTID,
            BF_API_XML_TERMID,
            BF_API_XML_PARAMETERTYPE,
            BF_API_XML_IGNOREDEFERING,
            BF_API_XML_WITHACTVALUE,
            BF_API_XML_WITHACTVALUEOBJ,
            BF_API_XML_SET_TEXT,
            BF_API_XML_SET_TEXT2,
            BF_API_XML_PARAMETER_USAGE,
            BF_API_XML_PARAMETER_USAGE2,
            BF_API_XML_DB_ID,
            BF_API_XML_DB_ID2,
            BF_API_XML_DATATYPEID,
            BF_API_XML_DATATYPEID2,
            BF_API_XML_MATHDL4PARAM,
            BF_API_XML_MATHDL4PARAM2,
            BF_API_XML_AMOUNT,
            BF_API_XML_AMOUNT2,
            BF_API_XML_PHYSICALUNITID,
            BF_API_XML_PHYSICALUNITID2,
            BF_API_XML_OBJ,
            BF_API_XML_MANYPARAMETER_XML,
            BF_API_XML_WITH_PARAMETER,

            // Subfolder
            BF_API_XML_SUBFOLDERHDL,
            BF_API_XML_SUBFOLDERHDLEMPTY,
            BF_API_XML_SUBOJECTHDL,
            BF_API_XML_SUBNAME,

            
            // Common Functions
            BF_API_XML_OBJECTHDL,// only for common functions!
            BF_API_XML_TARGETTYPE,
            BF_API_XML_ATTRIBUTE_TYPE,
            BF_API_XML_ATTRIBUTE_VALUE,
            BF_API_XML_OBJECT_STATE,
            BF_API_XML_CHECK_TEXT,
            BF_API_XML_CHECK_ERROR,
            BF_API_XML_CHECK_ARCHIVED,
            BF_API_XML_CHECK_ING,
            BF_API_XML_HDL1,
            BF_API_XML_HDL2,
            BF_API_XML_ERROR,
            BF_API_XML_DATAFLAG,
            BF_API_XML_ARCHIV,
            BF_API_XML_EXTDATA,
            BF_API_XML_WITHOUTTAG,
            BF_API_XML_WITHPCELL,
            BF_API_XML_ARCHIVTAG,
            BF_API_XML_EXTTRANSFORM,

            // LogBook | Print
            BF_API_XML_ACT_RENAMED,
            BF_API_XML_ACT_CHAINED,
            BF_API_XML_ACT_UNCHAINED,
            BF_API_XML_ACT_REL_TEST,
            BF_API_XML_ACT_REL_PROD,
            BF_API_XML_ACT_ACHIVED,
            BF_API_XML_ACT_IMPORTED,
            BF_API_XML_ACT_NEW_PROD,
            BF_API_XML_ACT_DELETED,
            BF_API_XML_OBJ_PCELL,
            BF_API_XML_OBJ_LIB,
            BF_API_XML_OBJ_RECIPE,
            BF_API_XML_OBJ_FORMCAT,
            BF_API_XML_OBJ_FORMULA,
            BF_API_XML_OBJ_ORDERCAT,
            BF_API_XML_OBJ_ORDER,
            BF_API_XML_OBJ_BATCH,
            BF_API_XML_OBJ_MATERIAL,
            BF_API_XML_LOGBUSERNAME,
            BF_API_XML_DTPSTARTTIME,
            BF_API_XML_DTPENDTIME,
            BF_API_XML_LBOBJECTHDL,
            BF_API_XML_OUTPUTFILEANDPATH,
            BF_API_XML_TEMPLATEPATH,
            BF_API_XML_PDFPATH,
            BF_API_XML_LCID,
            BF_API_XML_TIMEZONEKEYNAME,
            BF_API_XML_ADDPARAMETER_NAME1,
            BF_API_XML_ADDPARAMETER_VALUE1,
            BF_API_XML_ADDPARAMETER_NAME2,
            BF_API_XML_ADDPARAMETER_VALUE2,

            // API | SIS Events
            BF_API_XML_EVENTHDL,
            BF_API_XML_SIS_POSITION4EVENTID,
            BF_API_XML_SIS_EVENTID,
            BF_API_XML_SIS_ADVISEOPTIONS,
            BF_API_XML_OBJECT_TYPE,
            BF_API_XML_NOTIFY_TYPE,
            BF_API_XML_EVTID4ADVISE,
            BF_API_XML_AUTOREADVISE,

            // Command Manager
            BF_API_XML_GETERRORTEXT_ERROR,
            BF_API_XML_GETERRORTEXT_LANGUAGE,
            BF_API_XML_ACK_BATCH_HDL,
            BF_API_XML_ACK_CONTID,
            BF_API_XML_ACK_TERMID,
            BF_API_XML_ACK_COOKIE,
            BF_API_XML_PCELLHDL_,
            BF_API_XML_GETDATAPCELLPARTID,
            BF_API_XML_ARCHIVETYPE,


            // Historical SIS Events
            BF_API_XML_HSIS_OBJECTTYPE,
            BF_API_XML_HSIS_STARTTIME,
            BF_API_XML_HSIS_ENDTIME,
            BF_API_XML_HSIS_STARTEVENTID,
            BF_API_XML_HSIS_ENDEVENTID,
            BF_API_XML_HSIS_OBJECTHDL
        }

        // Enables the connection to the GUI through properties
        MainWindow m_mainwindow;

        public CFunctionParameters(MainWindow _mainwindow)
        {
            m_mainwindow = _mainwindow; 
        }

        /// <summary>
        /// Fills the collections with the names and values for a particular 'Get' command
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="TagParams">Collection of a string array that is to be filled with xml tags.</param>
        /// <param name="AttributeParams">Collection of a string array that is to be filled with xml attributes.</param>
        /// <param name="ValueParams">Collection of a string array that is to be filled with xml values.</param>
        public void Param4GetCommand(string Command, Collection<string[]> TagParams, Collection<string[]> AttributeParams, Collection<string[]> ValueParams)
        {
            List<XML_PARAM_NAME[]> attributeParamName = new List<XML_PARAM_NAME[]>();
            if (TagParams == null)
            {
                throw new ArgumentNullException("TagParams");
            }
            switch (Command)
            {
                case "GetAllProjects":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "GetAllPCells4Project":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;

                case "GetAllChainRoots4PCell":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "GetAllMR4PCell":
                case "GetAllFormulaCats4PCell":
                case "GetAllOrderCats4PCell":
                case "GetAllLIB4PCell":
                case "GetAllMaterials4PCell":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;
                case "GetAllFormulaCats4MR":
                case "GetAllFormulas4MR":
                case "GetFormulaCat4MR":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MRHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MRHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;

                case "GetAllUnitClasses4PCell":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR, 
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME, 
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                     XML_PARAM_NAME.BF_API_XML_PCELLHDL_ASHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR, 
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;
                case "GetAllDataTypes4PCell":
                case "GetAllPhysicalUnits4PCell":
                case "GetAllEventDefinitions":
                case "GetAllSubscriptions":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});
                    break;
                case "GetAllActiveXFERPhase4PCell":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HSIS_STARTTIME,
                                    XML_PARAM_NAME.BF_API_XML_HSIS_ENDTIME});

                    break;

                case "GetAllUnits4UnitClass":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_UNITCLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    break;
                case "GetAllFormulas4FormulaCat":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULACATHDL4PCELL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULACATHDL4PCELL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;

                case "GetMR4Formula":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULAHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    break;
                case "GetProductData4Formula":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULAHDL});
                    break;
                case "GetProductData4MR":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MRHDL});
                    break;
                case "GetAllOrders4OrderCat":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERCATHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERCATHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;
                case "GetAllBatches4Order":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;
                case "GetBatchSize":
                case "GetBatchStartData":
                case "GetBatchState":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL});
                    break;
                case "GetAllChainedSuccessors4Batch":
                case "GetAllChainedPredecessors4Batch":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "GetAllFormulasOrMR4CreateBatch":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;

                case "GetMaterialData":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MATERIALHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "GetAllQualities4Material":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,  
                                    XML_PARAM_NAME.BF_API_XML_MATERIALHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else // without time stamp (synctime)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,  
                                    XML_PARAM_NAME.BF_API_XML_MATERIALHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;

                case "GetAllocations":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,  
                                    XML_PARAM_NAME.BF_API_XML_ALLOCATIONHDL,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    break;

                case "GetChildren":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    if (m_mainwindow.WithTimeStamp)
                    {
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,  
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    { // without time stamp (synctime)
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,  
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    attributeParamName.Add(new XML_PARAM_NAME[] { XML_PARAM_NAME.BF_API_XML_TARGETTYPE });
                    //attributeParamName.Add(new XML_PARAM_NAME[] { XML_PARAM_NAME.BF_API_XML_SUBTARGETTYPE }); -- Just 4 Test with Subfolder
                    break;
                case "GetParent":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;

                case "GetAllUnits":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL_ASHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_SYNCTIME,
                                    XML_PARAM_NAME.BF_API_XML_HIERARCHY,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    attributeParamName.Add(new XML_PARAM_NAME[] { XML_PARAM_NAME.BF_API_XML_HSIS_OBJECTTYPE });
                    break;
                case "GetChangedBatches":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    //XML_PARAM_NAME.BF_API_XML_PCELLHDL_ASHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    if (m_mainwindow.UI_CHKHISEVENTSTARTTIME.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_HSIS_STARTTIME});
                    }
                    if (m_mainwindow.UI_CHKHISEVENTENDTIME.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HSIS_ENDTIME});
                    }
                    break;
                case "GetAttribute4Object":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "GetObjectData":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    if (m_mainwindow.CheckExtTransform == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_EXTTRANSFORM});
                    }
                    string strArchiveTag = m_mainwindow.ArchiveTag;
                    switch (strArchiveTag)
                    {
                        case "0"://"6.0.4 (Archive V1)":
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_EXTDATA,
                                    XML_PARAM_NAME.BF_API_XML_ARCHIV});
                            break;
                        case "1"://"6.1.3 (Archive V2)":
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_WITHPCELL,
                                    XML_PARAM_NAME.BF_API_XML_ARCHIV});
                            break;
                        case "2"://"7.0.1 (Archive V3)":
                            if (m_mainwindow.UI_CHCKDEFAULT.IsChecked == true)
                            {
                                TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                                attributeParamName.Add(new XML_PARAM_NAME[] { 
                                        XML_PARAM_NAME.BF_API_XML_DATAFLAG,
                                        XML_PARAM_NAME.BF_API_XML_ARCHIV});
                            }
                            else
                            {
                                TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                                attributeParamName.Add(new XML_PARAM_NAME[] { 
                                        XML_PARAM_NAME.BF_API_XML_WITHPCELL,
                                        XML_PARAM_NAME.BF_API_XML_ARCHIV});
                            }
                            break;
                        case "3"://7.0.7 (Archive V4)":
                        case "4"://"7.1.2 (Archive V5)":
                        case "5"://"8.0.0 (Archive V6)":
                        case "6"://"8.0.1 (Archive V7)":
                        case "7"://"8.1.0 (Archive V8)":
                        case "8"://"8.2.0 (Archive V9)":
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_DATAFLAG,
                                    XML_PARAM_NAME.BF_API_XML_ARCHIV});

                            break;
                        case null:
                            break;
                        default:
                            throw new NotImplementedException("Archive Type " + strArchiveTag + " doesn't exits!");
                    }
                    break;
                case "GetQualityData":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_QUALITYHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "GetParameter":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_ALLOCATIONHDL,
                                    XML_PARAM_NAME.BF_API_XML_WITHACTVALUE,
                                    XML_PARAM_NAME.BF_API_XML_IGNOREDEFERING,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_RPE });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_CONTID,
                                    XML_PARAM_NAME.BF_API_XML_TERMID});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_PARAMETERTYPE});
                    break;
                case "GetObjectHeader":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_WITHACTVALUEOBJ});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    attributeParamName.Add(new XML_PARAM_NAME[] { XML_PARAM_NAME.BF_API_XML_WITH_PARAMETER});
                    break;
                case "GetObjectState":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL});
                    break;
                case "GetErrorText":
                    break;
                case "GetLogBookObjects":
                    int index = 0;
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});

                    if (m_mainwindow.UI_CHKRENAMED.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACT_RENAMED});
                    }
                    if (m_mainwindow.UI_CHKCHAINED.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACT_CHAINED});
                    }
                    if (m_mainwindow.UI_CHKUNCHAINED.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACT_UNCHAINED});
                    }
                    if (m_mainwindow.UI_CHKREL_TEST.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACT_REL_TEST});
                    }
                    if (m_mainwindow.UI_CHKREL_PROD.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACT_REL_PROD});
                    }
                    if (m_mainwindow.UI_CHKARCHIVED.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_ACT_ACHIVED});
                    }
                    if (m_mainwindow.UI_CHKIMPORTED.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {  
                                    XML_PARAM_NAME.BF_API_XML_ACT_IMPORTED});
                    }
                    if (m_mainwindow.UI_CHKNEW_PROD.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACT_NEW_PROD});
                    }

                    if (m_mainwindow.UI_CHKDELETED.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {   
                                    XML_PARAM_NAME.BF_API_XML_ACT_DELETED});
                    }
                    if (m_mainwindow.UI_CHKPCELL.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_PCELL});
                    }
                    if (m_mainwindow.UI_CHKLIBRARY.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_LIB});
                    }
                    if (m_mainwindow.UI_CHKRECIPE.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_RECIPE});
                    }
                    if (m_mainwindow.UI_CHKFORMCAT.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_FORMCAT});
                    }
                    if (m_mainwindow.UI_CHKFORMULA.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_FORMULA});
                    }
                    if (m_mainwindow.UI_CHKORDERCAT.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_ORDERCAT});
                    }
                    if (m_mainwindow.UI_CHKORDER.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_ORDER});
                    }
                    if (m_mainwindow.UI_CHKBATCH.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_BATCH});
                    }
                    if (m_mainwindow.UI_CHKMATERIAL.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJ_MATERIAL});
                    }
                    while (m_mainwindow.UI_TXTLOGBUSERNAME.Items.Count > index)
                    {

                        m_mainwindow.UI_TXTLOGBUSERNAME.SelectedIndex = (int)index;
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_LOGBUSERNAME});
                        index = index + 1;
                    }
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                     attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_DTPSTARTTIME,
                                    XML_PARAM_NAME.BF_API_XML_DTPENDTIME});
                    break;
                case "GetAllPrintTemplates4PCell":
                    if (m_mainwindow.UI_CHCKWITHOBJHDL.IsChecked.Value == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_LBOBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    else
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                        attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    }
                    break;
                case "GetAllSubfolders4PCell":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR,
                                    XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME});
                    break;
                case "GetHistoricalSISEventsByTime":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                     attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});
                     if (m_mainwindow.UI_CHKHISEVENTSTARTTIME.IsChecked.Value == true)
                     {
                         TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                         attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HSIS_STARTTIME});
                     }
                     if (m_mainwindow.UI_CHKHISEVENTENDTIME.IsChecked.Value == true)
                     {
                         TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                         attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HSIS_ENDTIME});
                     }
                     if (m_mainwindow.UI_CBHSISCHANGED.Items.Count != 0)
                     {
                         for (int i = 0; i < m_mainwindow.UI_CBHSISCHANGED.Items.Count; ++i )
                         {
                             TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                             attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HSIS_OBJECTHDL});
                         }
                     }
                    break;
                case"GetHistoricalSISEventsByID":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                     attributeParamName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL});
                     if (m_mainwindow.CheckHisEndEvent == true)
                     {
                         TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                         attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HSIS_STARTEVENTID,
                                    XML_PARAM_NAME.BF_API_XML_HSIS_ENDEVENTID});
                     }
                     else 
                     {
                         TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                         attributeParamName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HSIS_STARTEVENTID});
                     }
                    break;
                case"GetHistoricalSISEventsByFile":
                    break;
                default:
                    throw new NotImplementedException("Command " + Command + " has not been implemented in FunctionParameters.cs!");
            }

            GetValues4Enum(attributeParamName, AttributeParams, ValueParams);
        }

        /// <summary>
        /// Fills the collections with the names and values for a particular 'Set' command
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="TagParams">Collection of a string array that is to be filled with xml tags.</param>
        /// <param name="AttributeParams">Collection of a string array that is to be filled with xml attributes.</param>
        /// <param name="ValueParams">Collection of a string array that is to be filled with xml values.</param>
        public void Param4SetCommand(string Command, Collection<string[]> TagParams, Collection<string[]> AttributeParams, Collection<string[]> ValueParams)
        {
            List<XML_PARAM_NAME[]> AttributeParamsName = new List<XML_PARAM_NAME[]>();
            if (TagParams == null)
            {
                throw new ArgumentNullException("TagParams");
            }
            switch (Command)
            {
                case "SetCurrentUser":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME,
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG,
                                    XML_PARAM_NAME.BF_API_XML_PASSWORD,
                                    XML_PARAM_NAME.BF_API_XML_DOMAIN,
                                    XML_PARAM_NAME.BF_API_XML_APPLICATIONID});
                    break;
                case "SetBatchState":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME,
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    if (m_mainwindow.StayingContinueChck && m_mainwindow.ContinueChck)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                                XML_PARAM_NAME.BF_API_XML_STATE,
                                                XML_PARAM_NAME.BF_API_XML_EXSTATE,
                                                XML_PARAM_NAME.BF_API_XML_CONTINUE,
                                                XML_PARAM_NAME.BF_API_XML_STAYINCONTINUE});
                    }
                    else if (m_mainwindow.StayingContinueChck) 
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                                XML_PARAM_NAME.BF_API_XML_STATE,
                                                XML_PARAM_NAME.BF_API_XML_EXSTATE,
                                                XML_PARAM_NAME.BF_API_XML_STAYINCONTINUE});
                    }
                    else if (m_mainwindow.ContinueChck) 
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                                XML_PARAM_NAME.BF_API_XML_STATE,
                                                XML_PARAM_NAME.BF_API_XML_EXSTATE,
                                                XML_PARAM_NAME.BF_API_XML_CONTINUE});
                    }
                    else
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                                XML_PARAM_NAME.BF_API_XML_STATE,
                                                XML_PARAM_NAME.BF_API_XML_EXSTATE});
                    }
                    break;
                case "SetBatchStartData":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME,
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }

                    if (m_mainwindow.BatchDateTimeFormatChck)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_BATCH_STARTMODE,
                                    XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANSTART,
                                    XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANEND});
                        break;
                    }
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_BATCH_STARTMODE,
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PLANSTART,
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PLANEND});
                    break;
                case "SetBatchSize":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME,
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_SIZE});
                    break;
                case "SetMR4Formula":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULAHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_MRHDL});
                    break;
                case "SetFormulaCat4MR":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MRHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_FORMULACATHDL});
                    break;
                case "SetMaterialData":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MATERIALHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_MATNAME, 
                                    XML_PARAM_NAME.BF_API_XML_MATDESC, 
                                    XML_PARAM_NAME.BF_API_XML_MATCODE,
                                    XML_PARAM_NAME.BF_API_XML_MAT_USAGE});
                    break;

                case "SetAllocations":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_ALLOCATIONHDL});

                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME,
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME,
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    if (!string.IsNullOrEmpty(m_mainwindow.StrategyTypeId)
                        && string.IsNullOrEmpty(m_mainwindow.ParameterId)
                        && string.IsNullOrEmpty(m_mainwindow.AllocOnStart))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_TRPID, 
                                    XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL,
                                    XML_PARAM_NAME.BF_API_XML_STRATEGYTYPE_ID});
                    }
                    if (!string.IsNullOrEmpty(m_mainwindow.StrategyTypeId)
                        && !string.IsNullOrEmpty(m_mainwindow.ParameterId)
                        && string.IsNullOrEmpty(m_mainwindow.AllocOnStart))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_TRPID, 
                                    XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL,
                                    XML_PARAM_NAME.BF_API_XML_STRATEGYTYPE_ID,
                                    XML_PARAM_NAME.BF_API_XML_PARAMETERID});
                    }
                    if (!string.IsNullOrEmpty(m_mainwindow.StrategyTypeId)
                    && !string.IsNullOrEmpty(m_mainwindow.AllocOnStart)
                    && string.IsNullOrEmpty(m_mainwindow.ParameterId))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                XML_PARAM_NAME.BF_API_XML_TRPID, 
                                XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL,
                                XML_PARAM_NAME.BF_API_XML_STRATEGYTYPE_ID,
                                XML_PARAM_NAME.BF_API_XML_ALLOCONSTART});
                    }
                    if (!string.IsNullOrEmpty(m_mainwindow.AllocOnStart)
                       && string.IsNullOrEmpty(m_mainwindow.ParameterId)
                       && string.IsNullOrEmpty(m_mainwindow.StrategyTypeId))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                XML_PARAM_NAME.BF_API_XML_TRPID, 
                                XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL,
                                XML_PARAM_NAME.BF_API_XML_ALLOCONSTART});
                    }
                    if (!string.IsNullOrEmpty(m_mainwindow.AllocOnStart)
                       && !string.IsNullOrEmpty(m_mainwindow.ParameterId)
                       && string.IsNullOrEmpty(m_mainwindow.StrategyTypeId))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                XML_PARAM_NAME.BF_API_XML_TRPID, 
                                XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL,
                                XML_PARAM_NAME.BF_API_XML_ALLOCONSTART,
                                XML_PARAM_NAME.BF_API_XML_PARAMETERID});
                    }
                    if (!string.IsNullOrEmpty(m_mainwindow.ParameterId)
                        && string.IsNullOrEmpty(m_mainwindow.StrategyTypeId)
                        && string.IsNullOrEmpty(m_mainwindow.AllocOnStart))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                XML_PARAM_NAME.BF_API_XML_TRPID, 
                                XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL,
                                XML_PARAM_NAME.BF_API_XML_PARAMETERID});
                    }
                    if (!string.IsNullOrEmpty(m_mainwindow.StrategyTypeId)
                       && !string.IsNullOrEmpty(m_mainwindow.ParameterId)
                       && !string.IsNullOrEmpty(m_mainwindow.AllocOnStart))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                XML_PARAM_NAME.BF_API_XML_TRPID, 
                                XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL,
                                XML_PARAM_NAME.BF_API_XML_STRATEGYTYPE_ID,
                                XML_PARAM_NAME.BF_API_XML_PARAMETERID,
                                XML_PARAM_NAME.BF_API_XML_ALLOCONSTART});
                    }
                    if (string.IsNullOrEmpty(m_mainwindow.StrategyTypeId)
                        && string.IsNullOrEmpty(m_mainwindow.ParameterId)
                        && string.IsNullOrEmpty(m_mainwindow.AllocOnStart))
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                XML_PARAM_NAME.BF_API_XML_TRPID, 
                                XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL});
                    }
                    break;
                case "SetAttribute4Object":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ATTRIBUTE_TYPE, 
                                    XML_PARAM_NAME.BF_API_XML_ATTRIBUTE_VALUE});
                    break;
                case "SetProductData4Formula":
                    TagParams.Add(new String[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[]{
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_FORMULAHDL,
                                    XML_PARAM_NAME.BF_API_XML_PRODUCTHDL});
                    break;
                case "SetQualityData":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[]{
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_QUALITYHDL});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[]{ 
                                    XML_PARAM_NAME.BF_API_XML_MATNAME,
                                    XML_PARAM_NAME.BF_API_XML_MATDESC,
                                    XML_PARAM_NAME.BF_API_XML_MATCODE});
                    break;
                case "SetParameter":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_WITHACTVALUE,
                                    XML_PARAM_NAME.BF_API_XML_IGNOREDEFERING,
                                    XML_PARAM_NAME.BF_API_XML_ALLOCATIONHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_RPE });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_CONTID,
                                    XML_PARAM_NAME.BF_API_XML_TERMID});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_PARAMETER_USAGE,
                                    XML_PARAM_NAME.BF_API_XML_DB_ID,
                                    XML_PARAM_NAME.BF_API_XML_DATATYPEID,
                                    XML_PARAM_NAME.BF_API_XML_AMOUNT,
                                    XML_PARAM_NAME.BF_API_XML_PHYSICALUNITID,
                                    XML_PARAM_NAME.BF_API_XML_MATHDL4PARAM});

                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_USERNAME,
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG,
                                    XML_PARAM_NAME.BF_API_XML_COMPUTER});
                    }

                    if (m_mainwindow.TxtUsg2 != "")
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_PARAMETER_USAGE2,
                                    XML_PARAM_NAME.BF_API_XML_DB_ID2,
                                    XML_PARAM_NAME.BF_API_XML_DATATYPEID2,
                                    XML_PARAM_NAME.BF_API_XML_AMOUNT2,
                                    XML_PARAM_NAME.BF_API_XML_PHYSICALUNITID2,
                                    XML_PARAM_NAME.BF_API_XML_MATHDL4PARAM2});
                    }

                    break;
                case "SetManyParameters":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                     XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                     XML_PARAM_NAME.BF_API_XML_SESSION,
                                     XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                     XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                     XML_PARAM_NAME.BF_API_XML_ALLOCATIONHDL});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_MANYPARAMETER_XML});
                    break;
                case "SetObjectState":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                     XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                     XML_PARAM_NAME.BF_API_XML_SESSION,
                                     XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                     XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                     XML_PARAM_NAME.BF_API_XML_OBJECTHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJECT_STATE,
                                    XML_PARAM_NAME.BF_API_XML_CHECK_TEXT,
                                    XML_PARAM_NAME.BF_API_XML_CHECK_ERROR,
                                    XML_PARAM_NAME.BF_API_XML_CHECK_ARCHIVED,
                                    XML_PARAM_NAME.BF_API_XML_CHECK_ING});
                    break;
                case"SetAdviseFilter":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                     XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                     XML_PARAM_NAME.BF_API_XML_SESSION,
                                     XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                     XML_PARAM_NAME.BF_API_XML_PCELLHDL});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_NOTIFY_TYPE});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_OBJECT_TYPE});
                                    
                    break;
                default:
                    throw new NotImplementedException("Command " + Command + " has not been implemented in FunctionParameters.cs!");
            }
            GetValues4Enum(AttributeParamsName, AttributeParams, ValueParams);
        }

        /// <summary>
        /// Fills the collections with the names and values for a particular 'Create' command
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="TagParams">Collection of a string array that is to be filled with xml tags.</param>
        /// <param name="AttributeParams">Collection of a string array that is to be filled with xml attributes.</param>
        /// <param name="ValueParams">Collection of a string array that is to be filled with xml values.</param>
        public void Param4CreateCommand(string Command, Collection<string[]> TagParams, Collection<string[]> AttributeParams, Collection<string[]> ValueParams)
        {
            List<XML_PARAM_NAME[]> AttributeParamsName = new List<XML_PARAM_NAME[]>();
            if (TagParams == null)
            {
                throw new ArgumentNullException("TagParams");
            }
            switch (Command)
            {
                case "CreateBatch":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERHDL});

                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME, 
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    if (m_mainwindow.BatchStartModeChck)
                    {
                        if (m_mainwindow.BatchDateTimeFormatChck)
                        {
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_FORMORMR_HDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHSIZE, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_STARTMODE,
                                    XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANSTART, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANEND, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK});
                        }
                        else
                        {
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_FORMORMR_HDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHSIZE, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_STARTMODE,
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PLANSTART, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PLANEND, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK});
                        }
                    }
                    else
                    {
                        if (m_mainwindow.BatchDateTimeFormatChck)
                        {
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_FORMORMR_HDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHSIZE, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANSTART, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANEND, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK});
                        }
                        else
                        {
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_FORMORMR_HDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHSIZE, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PLANSTART, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PLANEND, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK});
                        }
                    }
                    break;
                case "CreateRecipe":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MRHDL});
                    if (m_mainwindow.RecFormSubFolderChck == true)
                    {
                        var element = m_mainwindow.FindElementByName<System.Windows.Controls.ComboBox>(m_mainwindow.SUBF, "UI_CBSUBFOLDER4PCELL");
                        string temp = element.Text;
                        //Subfolder checked but not filled
                        if (temp == "")
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK, 
                                    XML_PARAM_NAME.BF_API_XML_WITH_BATCHML,
                                    XML_PARAM_NAME.BF_API_XML_BATCHML,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDLEMPTY});
                        }
                        //Subfolder checked and filled
                        else
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK, 
                                    XML_PARAM_NAME.BF_API_XML_WITH_BATCHML,
                                    XML_PARAM_NAME.BF_API_XML_BATCHML,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDL});
                        }
                    }
                    else 
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                   XML_PARAM_NAME.BF_API_XML_RECFORMNAME, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK, 
                                    XML_PARAM_NAME.BF_API_XML_WITH_BATCHML,
                                    XML_PARAM_NAME.BF_API_XML_BATCHML});
                    }
                    break;
                case "CreateOrder":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERCATHDL});

                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME, 
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHSIZE, 
                                    XML_PARAM_NAME.BF_API_XML_ORDER_EARLIEST,
                                    XML_PARAM_NAME.BF_API_XML_ORDER_LATEST,
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK});
                    break;
                case "CreateFormulaCat":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL_ASHDL});
                    if (m_mainwindow.RecFormSubFolderChck == true)
                    {
                        var element = m_mainwindow.FindElementByName<System.Windows.Controls.ComboBox>(m_mainwindow.SUBF, "UI_CBSUBFOLDER4PCELL");
                        string temp = element.Text;
                        //Subfolder checked but not filled
                        if (temp == "")
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDLEMPTY});
                        }
                        //Subfolder checked and filled
                        else
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDL});
                        }
                    }
                    else 
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK});
                    }
                    break;
                case "CreateFormula":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULACATHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK});
                    break;
                case "CreateOrderCat":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL_ASHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME, 
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    if (m_mainwindow.OrderBatchSubFolderChck == true)
                    {
                        var element = m_mainwindow.FindElementByName<System.Windows.Controls.ComboBox>(m_mainwindow.SUBF, "UI_CBSUBFOLDER4PCELL");
                        string temp = element.Text;
                        //Subfolder checked but not filled
                        if (temp == "")
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                         AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDLEMPTY});
                            //AttributeParamsName.ElementAt(1).SetValue(XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDL,3);
                        }
                        //Subfolder checked and filled
                        else
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDL});
                        }
                    }
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                      AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK});
                    break;
                case "CreateMaterial":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL_ASHDL});
                        if (m_mainwindow.MatSubFolderChck == true) 
                        {
                            var element = m_mainwindow.FindElementByName<System.Windows.Controls.ComboBox>(m_mainwindow.SUBF, "UI_CBSUBFOLDER4PCELL");
                            string temp = element.Text;
                            //Subfolder checked but not filled
                            if (temp == "")
                            {
                                TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                                AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_MATNAME, 
                                    XML_PARAM_NAME.BF_API_XML_MATDESC, 
                                    XML_PARAM_NAME.BF_API_XML_MATCODE,
                                    XML_PARAM_NAME.BF_API_XML_MAT_USAGE, 
                                    XML_PARAM_NAME.BF_API_XML_MATOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDLEMPTY});
                            }
                            //Subfolder checked and filled
                            else
                            {
                                TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                                AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_MATNAME, 
                                    XML_PARAM_NAME.BF_API_XML_MATDESC, 
                                    XML_PARAM_NAME.BF_API_XML_MATCODE,
                                    XML_PARAM_NAME.BF_API_XML_MAT_USAGE, 
                                    XML_PARAM_NAME.BF_API_XML_MATOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDL});
                            }
                        }
                        else 
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_MATNAME, 
                                    XML_PARAM_NAME.BF_API_XML_MATDESC, 
                                    XML_PARAM_NAME.BF_API_XML_MATCODE,
                                    XML_PARAM_NAME.BF_API_XML_MAT_USAGE, 
                                    XML_PARAM_NAME.BF_API_XML_MATOPTLOCK});
                        }
                   
                    break;
                case "CreateManyMaterials":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_MATERIAL_XML});
                    break;
                case "CreateQuality":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_MATERIALHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[]{
                                    XML_PARAM_NAME.BF_API_XML_MATNAME,
                                    XML_PARAM_NAME.BF_API_XML_MATCODE,
                                    XML_PARAM_NAME.BF_API_XML_MATDESC,
                                    XML_PARAM_NAME.BF_API_XML_MATOPTLOCK});
                    break;
                case"CreateSubfolder":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_SUBOJECTHDL});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_SUBNAME});
                    break;
                default:
                    throw new NotImplementedException("Command " + Command + " has not been implemented in FunctionParameters.cs!");
            }
            GetValues4Enum(AttributeParamsName, AttributeParams, ValueParams);
        }

        /// <summary>
        /// Fills the collections with the names and values for a particular 'Copy' command
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="TagParams">Collection of a string array that is to be filled with xml tags.</param>
        /// <param name="AttributeParams">Collection of a string array that is to be filled with xml attributes.</param>
        /// <param name="ValueParams">Collection of a string array that is to be filled with xml values.</param>
        public void Param4CopyCommand(string Command, Collection<string[]> TagParams, Collection<string[]> AttributeParams, Collection<string[]> ValueParams)
        {
            List<XML_PARAM_NAME[]> AttributeParamsName = new List<XML_PARAM_NAME[]>();

            if (TagParams == null)
            {
                throw new ArgumentNullException("TagParams");
            }
            switch (Command)
            {
                case "CopyRecipe":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MRHDL});
                    if (m_mainwindow.RecFormSubFolderChck == true) 
                    {
                        var element = m_mainwindow.FindElementByName<System.Windows.Controls.ComboBox>(m_mainwindow.SUBF, "UI_CBSUBFOLDER4PCELL");
                        string temp = element.Text;
                        //Subfolder checked but not filled
                        if (temp == "")
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDLEMPTY});
                        }
                        //Subfolder checked and filled
                        else
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                   XML_PARAM_NAME.BF_API_XML_RECFORMNAME,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK,
                                    XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDL});
                        }
                    }
                    else 
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK});
                    }
                    break;
                case "CopyFormula":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULAHDL});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_RECFORMNAME,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMDESC,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMVERSION,
                                    XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK});
                    break;
                case "CopyOrder":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERHDL});
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME,
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC,
                                    XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK});
                    break;
                case "CopyBatch":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL});
                    break;
                case "CopyParameter":
                        m_mainwindow.UI_txtSet2.Text = m_mainwindow.UI_txtSet.Text;
                        m_mainwindow.UI_txtUsg2.Text = m_mainwindow.UI_txtUsg.Text;
                        m_mainwindow.UI_txtID2.Text = m_mainwindow.UI_txtID.Text;
                        m_mainwindow.UI_txtDtId2.Text = m_mainwindow.UI_txtDtId.Text;
                        m_mainwindow.UI_txtValue2.Text = m_mainwindow.UI_txtValue.Text;
                        m_mainwindow.UI_txtPhUnit2.Text = m_mainwindow.UI_txtPhUnit.Text;
                        m_mainwindow.UI_txtMatHdl2.Text = m_mainwindow.UI_txtMatHdl.Text;
                    break;
                default:
                    throw new NotImplementedException("Command " + Command + " has not been implemented in FunctionParameters.cs!");
            }
            GetValues4Enum(AttributeParamsName, AttributeParams, ValueParams);
        }

        /// <summary>
        /// Fills the collections with the names and values for a particular 'Delete' command
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="TagParams">Collection of a string array that is to be filled with xml tags.</param>
        /// <param name="AttributeParams">Collection of a string array that is to be filled with xml attributes.</param>
        /// <param name="ValueParams">Collection of a string array that is to be filled with xml values.</param>
        public void Param4DeleteCommand(string Command, Collection<string[]> TagParams, Collection<string[]> AttributeParams, Collection<string[]> ValueParams)
        {
            List<XML_PARAM_NAME[]> AttributeParamsName = new List<XML_PARAM_NAME[]>();
            if (TagParams == null)
            {
                throw new ArgumentNullException("TagParams");
            }
            switch (Command)
            {
                case "DeleteRecipe":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MRHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "DeleteFormula":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULAHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;
                case "DeleteFormulaCat":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_FORMULACATHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR});
                    break;

                case "DeleteOrderCat":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERCATHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME, 
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    break;
                case "DeleteOrder":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_ORDERHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME, 
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    break;
                case "DeleteBatch":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCHHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME, 
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    break;
                case "DeleteMaterial":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_MATERIALHDL});
                    if (m_mainwindow.WithAuditTrail)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_AUDITTRAIL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_USERNAME, 
                                    XML_PARAM_NAME.BF_API_XML_USERNAMELONG, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME, 
                                    XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP});
                    }
                    break;
                case "DeleteQuality":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                        XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                        XML_PARAM_NAME.BF_API_XML_SESSION,
                                        XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                        XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                        XML_PARAM_NAME.BF_API_XML_QUALITYHDL});
                   
                    break;
                case "DelParameter":
                        m_mainwindow.UI_txtSet2.Text = "";
                        m_mainwindow.UI_txtUsg2.Text = "";
                        m_mainwindow.UI_txtID2.Text = "";
                        m_mainwindow.UI_txtDtId2.Text = "";
                        m_mainwindow.UI_txtValue2.Text = "";
                        m_mainwindow.UI_txtPhUnit2.Text = "";
                        m_mainwindow.UI_txtMatHdl2.Text = "";
                    break;
                case"DeleteSubfolder":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_SUBOJECTHDL});
                    break;
                default:
                    throw new NotImplementedException("Command " + Command + " has not been implemented in FunctionParameters.cs!");
            }
            GetValues4Enum(AttributeParamsName, AttributeParams, ValueParams);
        }

        /// <summary>
        /// Fills the collections with the names and values for a particular 'Execute' command
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="TagParams">Collection of a string array that is to be filled with xml tags.</param>
        /// <param name="AttributeParams">Collection of a string array that is to be filled with xml attributes.</param>
        /// <param name="ValueParams">Collection of a string array that is to be filled with xml values.</param>
        public void Param4ExecuteCommand(string Command, Collection<string[]> TagParams, Collection<string[]> AttributeParams, Collection<string[]> ValueParams)
        {
            List<XML_PARAM_NAME[]> AttributeParamsName = new List<XML_PARAM_NAME[]>();
            if (TagParams == null)
            {
                throw new ArgumentNullException("TagParams");
            }
            switch (Command)
            {
                case "Init":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION});
                    break;
                case "Exit":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] { 
                                    XML_PARAM_NAME.BF_API_XML_SESSION });
                    break;
                case "UnAdvise":
                case "Connect":
                case "DisConnect":
                case "Resume":
                case "Suspend":
                case "SubscribeAllBasicEvents":
                case "UnAdvise4SIS":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL });
                    break;
                case "Advise":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL });
                    if (m_mainwindow.CheckEventId == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                         AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_EVTID4ADVISE});
                    }
                    if (m_mainwindow.CheckAutoAdvise == true)
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_AUTOREADVISE});
                    }
                    break;
                case "ChainBatch2Batch":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PREDECESSORHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_BATCH_SUCCESSORHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_CHAIN_ON_START,
                                    XML_PARAM_NAME.BF_API_XML_BATCH_GAPTIMEINSEC});
                    break;
                case "UnChainBatchFromBatch":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_BATCH_PREDECESSORHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_BATCH_SUCCESSORHDL});
                    break;
                case "SubscribeEvent":
                case "UnSubscribeEvent":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL, 
                                    XML_PARAM_NAME.BF_API_XML_EVENTHDL});
                    break;
                case "Advise4SIS":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_SIS_POSITION4EVENTID,
                                    XML_PARAM_NAME.BF_API_XML_SIS_EVENTID,
                                    XML_PARAM_NAME.BF_API_XML_SIS_ADVISEOPTIONS});
                    break;
                case "IsMaterialReferenced":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_MATERIALHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTBAR });
                    break;
                case "ValidateObject":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL});
                    break;
                case "IsEqualHDL":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_HDL1,
                                    XML_PARAM_NAME.BF_API_XML_HDL2});
                    break;
                case "LockObject":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL});
                    break;
                case "UnlockObject":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OBJECTHDL});
                    break;
                case "TransferData2PH":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                     AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_LBOBJECTHDL});
                    break;
                case "BackUp":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_OUTPUTFILEANDPATH});
                    break;
                case "PrintObject":
                    for (int i = 0; i < m_mainwindow.Counter; ++i )
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_OBJECT });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION, 
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL, 
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL,
                                    XML_PARAM_NAME.BF_API_XML_LBOBJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_TEMPLATEPATH,
                                    XML_PARAM_NAME.BF_API_XML_PDFPATH,
                                    XML_PARAM_NAME.BF_API_XML_LCID,
                                    XML_PARAM_NAME.BF_API_XML_TIMEZONEKEYNAME});

                        if (m_mainwindow.UI_CHCKADDPARAMETER.IsChecked.Value == true)
                        {
                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                            AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ADDPARAMETER_NAME1,
                                    XML_PARAM_NAME .BF_API_XML_ADDPARAMETER_VALUE1});

                            TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                             AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ADDPARAMETER_NAME2,
                                    XML_PARAM_NAME .BF_API_XML_ADDPARAMETER_VALUE2});
                           
                        }
                        else 
                        {
                            m_mainwindow.UI_TXTPARANAME1.Clear();
                            m_mainwindow.UI_TXTPARAVALUE1.Clear();
                            m_mainwindow.UI_TXTPARANAME2.Clear();
                            m_mainwindow.UI_TXTPARAVALUE2.Clear();
                        }
                    }
                    break;
                case "ExecuteCommand_GetErrorText":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_GETERRORTEXT });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION, 
                                    XML_PARAM_NAME.BF_API_XML_SESSION});

                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_DATA });
                                AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_GETERRORTEXT_ERROR,
                                    XML_PARAM_NAME .BF_API_XML_GETERRORTEXT_LANGUAGE});
                    break;
                case "ExecuteCommand_Acknowledge":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_ACKNOWLEDGE });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});
                    if (m_mainwindow.AckTermID != "")
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_BATCH });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACK_BATCH_HDL,
                                    XML_PARAM_NAME.BF_API_XML_ACK_CONTID,
                                    XML_PARAM_NAME.BF_API_XML_ACK_TERMID,
                                    XML_PARAM_NAME.BF_API_XML_ACK_COOKIE});
                    }
                    else 
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_BATCH });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_ACK_BATCH_HDL,
                                    XML_PARAM_NAME.BF_API_XML_ACK_CONTID,
                                    XML_PARAM_NAME.BF_API_XML_ACK_COOKIE});
                    }
                    break;
                case "ExecuteCommand_GetAllActOperAllocs":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_GETALLACTIVEOPERATORALLOCATION });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});
                    break;
                case "ExecuteCommand_GetAllActObj":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_GETALLACTIVEOBJECTS });
                    AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});
                    break;
                case "AddOps":
                    if (m_mainwindow.AckBatchHdl2 != "")
                    {
                        m_mainwindow.UI_CBBATCHHDL_1.Items.Add(m_mainwindow.AckBatchHdl2);
                        if (m_mainwindow.UI_CBBATCHHDL_1.Items.Count == 1)
                        {
                            m_mainwindow.UI_CBBATCHHDL_1.Text = m_mainwindow.AckBatchHdl2;
                        }
                        else
                        {
                            m_mainwindow.UI_CBBATCHHDL_1.SelectedIndex = m_mainwindow.UI_CBBATCHHDL_1.Items.Count;
                        }
                        m_mainwindow.AckBatchHdl2 = "";
                    }
                    break;
                case "ClearOps":
                    m_mainwindow.UI_CBBATCHHDL_1.Items.Clear();
                    break;
                case "AddObj":
                    if (m_mainwindow.AckBatchHdl3 != "")
                    {
                        m_mainwindow.UI_CBBATCHHDL_2.Items.Add(m_mainwindow.AckBatchHdl3);
                        if (m_mainwindow.UI_CBBATCHHDL_2.Items.Count == 1)
                        {
                            m_mainwindow.UI_CBBATCHHDL_2.Text = m_mainwindow.AckBatchHdl3;
                        }
                        else
                        {
                            m_mainwindow.UI_CBBATCHHDL_2.SelectedIndex = m_mainwindow.UI_CBBATCHHDL_2.Items.Count;
                        }
                        m_mainwindow.AckBatchHdl3 = "";
                    }
                    break;
                case "ClearObj":
                    m_mainwindow.UI_CBBATCHHDL_2.Items.Clear();
                    break;
                case "ExecuteCommand_GetData4PCell":
                    if (m_mainwindow.PCellPartID != "")
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_GETDATA4PCELL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL});
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_PCELL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL_,
                                    XML_PARAM_NAME.BF_API_XML_GETDATAPCELLPARTID,
                                    XML_PARAM_NAME.BF_API_XML_ARCHIVETYPE});
                    }
                    else
                    {
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_GETDATA4PCELL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL,
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL});
                        TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_PCELL });
                        AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_PCELLHDL_,
                                    XML_PARAM_NAME.BF_API_XML_ARCHIVETYPE});
                    }
                    break;
                  case "ExecuteCommand_GetProjectSettings":
                    TagParams.Add(new string[] { XmlTag.BF_API_XMLTAG_GETPROJECTSETTINGS });
                     AttributeParamsName.Add(new XML_PARAM_NAME[] {
                                    XML_PARAM_NAME.BF_API_XML_APPVERSION,
                                    XML_PARAM_NAME.BF_API_XML_SESSION,
                                    XML_PARAM_NAME.BF_API_XML_PROJECTHDL});
                    break;
                default:
                    throw new NotImplementedException("Command " + Command + " has not been implemented in FunctionParameters.cs!");
            }
            GetValues4Enum(AttributeParamsName, AttributeParams, ValueParams);
        }

        /// <summary>
        /// Fills the collections with the names and values depending on the 
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="TagParams">Collection of a string array that is to be filled with xml tags.</param>
        /// <param name="AttributeParams">Collection of a string array that is to be filled with xml attributes.</param>
        /// <param name="ValueParams">Collection of a string array that is to be filled with xml values.</param>
        private void GetValues4Enum(List<XML_PARAM_NAME[]> AttributeParamNames, Collection<string[]> AttributeParams, Collection<string[]> ValueParams)
        {
            foreach (XML_PARAM_NAME[] attribArr in AttributeParamNames)
            {
                string[] names = new string[attribArr.Length];
                string[] values = new string[attribArr.Length];
                for (int i = 0; i < attribArr.Length; i++)
                {
                    XML_PARAM_NAME attrib = attribArr[i];
                    string new_name = null;
                    string new_value = null;
                    string strExsate = null;
                    int lExState; 

                    switch (attrib)
                    {
                        #region INIT and GENERAL
                        case XML_PARAM_NAME.BF_API_XML_SESSION:
                            new_name = XmlParam.BF_API_XML_SESSION;
                            new_value = m_mainwindow.Session;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OUTPUTBAR:
                            new_name = XmlParam.BF_API_XML_OUTPUTBAR;
                            new_value = m_mainwindow.OutputBar.ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_APPVERSION:
                            new_name = XmlParam.BF_API_XML_APPVERSION;
                            new_value = XmlParam.BF_API_XML_APIVERSION;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PROJECTHDL:
                            new_name = XmlParam.BF_API_XML_PROJECTHDL;
                            new_value = m_mainwindow.ProjectHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PCELLHDL:
                            new_name = XmlParam.BF_API_XML_PCELLHDL;
                            new_value = m_mainwindow.PCellHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_UNITCLHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.UnitClassHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_UNITHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.UnitHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HIERARCHY:
                            new_name = XmlParam.BF_API_XML_HIERARCHY;
                            new_value = m_mainwindow.WithHierarchy ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_WILDCARD_NAME:
                            new_name = XmlParam.BF_API_XML_WILDCARD_NAME;
                            new_value = m_mainwindow.WildCard;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SYNCTIME:
                            new_name = XmlParam.BF_API_XML_SYNCTIME;
                            new_value = m_mainwindow.TimeStampString;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_HOSTNAME:
                            new_name = XmlParam.BF_API_XML_AUDITTRAIL_HOSTNAME;
                            new_value = m_mainwindow.ComputerName;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP:
                            new_name = XmlParam.BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP;
                            new_value = DateTime.UtcNow.ToString("{0:yyyy-M-dTH:m:s.FFF}");
                            break;

                        case XML_PARAM_NAME.BF_API_XML_PCELLHDL_ASHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.PCellHdl;
                            break;
                        #endregion

                        #region SetCurrentUser
                        case XML_PARAM_NAME.BF_API_XML_USERNAME:
                            new_name = XmlParam.BF_API_XML_USERNAME;
                            new_value = m_mainwindow.UserName;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_USERNAMELONG:
                            new_name = XmlParam.BF_API_XML_USERNAMELONG;
                            new_value = m_mainwindow.UserNameLong;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PASSWORD:
                            new_name = XmlParam.BF_API_XML_PASSWORD;
                            new_value = m_mainwindow.Password;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_DOMAIN:
                            new_name = XmlParam.BF_API_XML_DOMAIN;
                            new_value = m_mainwindow.Domain;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_APPLICATIONID:
                            new_name = XmlParam.BF_API_XML_APPLICATIONID;
                            new_value = m_mainwindow.Application;
                            break;
                        #endregion

                        #region RECIPE FORMULA
                        case XML_PARAM_NAME.BF_API_XML_MRHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.MRHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_FORMULACATHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.FormulaCatHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_FORMULACATHDL4PCELL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.FormulaCatHdl4Pcell;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_FORMULAHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.FormulaHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_RECFORMNAME:
                            new_name = XmlParam.BF_API_XML_NAME;
                            new_value = m_mainwindow.UIRecFormName;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_RECFORMVERSION:
                            new_name = XmlParam.BF_API_XML_VERSION;
                            new_value = m_mainwindow.UIRecFormVersion;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_RECFORMDESC:
                            new_name = XmlParam.BF_API_XML_DESCRIPTION;
                            new_value = m_mainwindow.UIRecFormDesc;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_RECFORMOPTLOCK:
                            new_name = XmlParam.BF_API_XML_OPTLOCK;
                            new_value = m_mainwindow.UIRecFormOptLock ? "1" : "0";
                            break;
                        #endregion

                        #region ORDER BATCH

                        case XML_PARAM_NAME.BF_API_XML_SIZE:
                            new_name = XmlParam.BF_API_XML_SIZE;
                            new_value = m_mainwindow.UISize;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDERCATHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.OrderCatHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDERHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.OrderHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDER_EARLIEST:
                            new_name = XmlParam.BF_API_XML_EARLIEST;
                            new_value = m_mainwindow.OrderEarlierW3C;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDER_LATEST:
                            new_name = XmlParam.BF_API_XML_LATEST;
                            new_value = m_mainwindow.OrderLaterW3C;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCHHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.BatchHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_FORMORMR_HDL:
                            new_name = XmlParam.BF_API_XML_FORMORMR_HDL;
                            new_value = m_mainwindow.FormOrMRHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_STATE:
                            new_name = XmlParam.BF_API_XML_STATE;
                            new_value = m_mainwindow.BatchState.ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_EXSTATE:
                            new_name = XmlParam.BF_API_XML_EXSTATE;
                            new_value = m_mainwindow.BatchExState.ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_STARTMODE:
                            new_name = XmlParam.BF_API_XML_STARTMODE;
                            new_value = m_mainwindow.BatchStartModeChck ? m_mainwindow.BatchStartMode : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_PLANSTART:
                            new_name = XmlParam.BF_API_XML_PLANSTART;
                            new_value = m_mainwindow.BatchPlanStartW3C;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_PLANEND:
                            new_name = XmlParam.BF_API_XML_PLANEND;
                            new_value = m_mainwindow.BatchPlanEndW3C;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANSTART:
                            new_name = XmlParam.BF_API_XML_PLANSTART;
                            new_value = m_mainwindow.BatchDateTimeFormatPlanStart;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_DATETIMEFORMATPLANEND:
                            new_name = XmlParam.BF_API_XML_PLANEND;
                            new_value = m_mainwindow.BatchDateTimeFormatPlanEnd;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCHML:
                            new_name = XmlParam.BF_API_XML_BATCHML_FILENAMEANDPATH;
                            new_value = m_mainwindow.BatchML;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_WITH_BATCHML:
                            new_name = XmlParam.BF_API_XML_WITH_BATCHML;
                            new_value = m_mainwindow.BatchWithML ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_PREDECESSORHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.BatchHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_SUCCESSORHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.BatchChain;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_CHAIN_ON_START:
                            new_name = XmlParam.BF_API_XML_CHAIN_ON_START;
                            new_value = m_mainwindow.BatchChainOnStart.ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_BATCH_GAPTIMEINSEC:
                            new_name = XmlParam.BF_API_XML_GAPTIMEINSEC;
                            new_value = m_mainwindow.BatchGaptime;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PRODUCTHDL:
                            new_name = XmlParam.BF_API_XML_PRODUCTHDL;
                            new_value = m_mainwindow.ProductHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDERBATCHNAME:
                            new_name = XmlParam.BF_API_XML_NAME;
                            new_value = m_mainwindow.UIOrderBatchName;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDERBATCHSIZE:
                            new_name = XmlParam.BF_API_XML_SIZE;
                            new_value = m_mainwindow.UIOrderBatchSize;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDERBATCHDESC:
                            new_name = XmlParam.BF_API_XML_DESCRIPTION;
                            new_value = m_mainwindow.UIOrderBatchDesc;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ORDERBATCHOPTLOCK:
                            new_name = XmlParam.BF_API_XML_OPTLOCK;
                            new_value = m_mainwindow.UIOrderBatchOptLock ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_STAYINCONTINUE:
                            new_name = XmlParam.BF_API_XML_STAYINCONTIMODE;
                            new_value = m_mainwindow.StayingContinueChck ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_CONTINUE:
                            new_name = XmlParam.BF_API_XML_CONTINUE;
                            new_value = m_mainwindow.ContinueChck ? "1" : "0";
                            break;
                        #endregion

                        #region MATERIAL QUALITY
                        case XML_PARAM_NAME.BF_API_XML_MATERIALHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.Material;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MAT_USAGE:
                            new_name = XmlParam.BF_API_XML_USAGE;
                            new_value = m_mainwindow.MaterialUsage.ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MATERIAL_XML:
                            new_name = XmlParam.BF_API_XML_MATERIAL_XML;
                            new_value = m_mainwindow.MaterialsPath;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_QUALITYHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.Quality4Material;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_QUALITYDATA:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.QualityData;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MATNAME:
                            new_name = XmlParam.BF_API_XML_NAME;
                            new_value = m_mainwindow.UIMatName;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MATCODE:
                            new_name = XmlParam.BF_API_XML_CODE;
                            new_value = m_mainwindow.UIMatCode;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MATDESC:
                            new_name = XmlParam.BF_API_XML_DESCRIPTION;
                            new_value = m_mainwindow.UIMatDesc;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MATOPTLOCK:
                            new_name = XmlParam.BF_API_XML_OPTLOCK;
                            new_value = m_mainwindow.UIMatOptLock ? "1" : "0";
                            break;
                        #endregion

                        #region ALLOCATION PARAMETER
                        case XML_PARAM_NAME.BF_API_XML_ALLOCATIONHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.AllocationHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PARAMETERID:
                            new_name = XmlParam.BF_API_XML_PARAMETER_ID;
                            new_value = m_mainwindow.ParameterId;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_STRATEGYTYPE_ID:
                            new_name = XmlParam.BF_API_XML_STRATEGYTYPE_ID;
                            new_value = m_mainwindow.StrategyTypeId;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ALLOCONSTART:
                            new_name = XmlParam.BF_API_XML_ALLOCONSTART;
                            new_value = m_mainwindow.AllocOnStart;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_TRPID:
                            new_name = XmlParam.BF_API_XML_DB_ID;
                            new_value = m_mainwindow.TRPID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ALLOCATION_NEW_HDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.AllocationNewHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_CONTID:
                            new_name = XmlParam.BF_API_XML_CONT_ID;
                            new_value = m_mainwindow.CONTID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_TERMID:
                            new_name = XmlParam.BF_API_XML_TERM_ID;
                            new_value = m_mainwindow.TERMID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PARAMETERTYPE:
                            new_name = XmlParam.BF_API_XML_PARAMETER_USAGE;
                            new_value = m_mainwindow.ParameterType;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SET_TEXT:
                            new_name = XmlParam.BF_API_XML_PARAMETER_USAGE;
                            new_value = m_mainwindow.TxtSet;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SET_TEXT2:
                            new_name = XmlParam.BF_API_XML_PARAMETER_USAGE;
                            new_value = m_mainwindow.TxtSet2;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PARAMETER_USAGE:
                            new_name = XmlParam.BF_API_XML_PARAMETER_USAGE;
                            new_value = m_mainwindow.TxtUsg;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PARAMETER_USAGE2:
                            new_name = XmlParam.BF_API_XML_PARAMETER_USAGE;
                            new_value = m_mainwindow.TxtUsg2;
                            break;
                         case XML_PARAM_NAME.BF_API_XML_DB_ID:
                            new_name = XmlParam.BF_API_XML_DB_ID;
                            new_value = m_mainwindow.DbID;
                            break;
                         case XML_PARAM_NAME.BF_API_XML_DB_ID2:
                            new_name = XmlParam.BF_API_XML_DB_ID;
                            new_value = m_mainwindow.DbID2;
                            break;   
                        case XML_PARAM_NAME.BF_API_XML_DATATYPEID:
                            new_name = XmlParam.BF_API_XML_DATATYPE_ID;
                            new_value = m_mainwindow.DataTypeID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_DATATYPEID2:
                            new_name = XmlParam.BF_API_XML_DATATYPE_ID;
                            new_value = m_mainwindow.DataTypeID2;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MATHDL4PARAM:
                            new_name = XmlParam.BF_API_XML_MATERIAL_HDL;
                            new_value = m_mainwindow.MaterialHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MATHDL4PARAM2:
                            new_name = XmlParam.BF_API_XML_MATERIAL_HDL;
                            new_value = m_mainwindow.MaterialHdl2;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PHYSICALUNITID:
                            new_name = XmlParam.BF_API_XML_PHYSICALUNIT_ID;
                            new_value = m_mainwindow.PhysicalUnitID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PHYSICALUNITID2:
                            new_name = XmlParam.BF_API_XML_PHYSICALUNIT_ID;
                            new_value = m_mainwindow.PhysicalUnitID2;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_AMOUNT:
                            new_name = XmlParam.BF_API_XML_AMOUNT;
                            new_value = m_mainwindow.Amount;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_AMOUNT2:
                            new_name = XmlParam.BF_API_XML_AMOUNT;
                            new_value = m_mainwindow.Amount2;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_IGNOREDEFERING:
                            new_name = XmlParam.BF_API_XML_IGNOREDEFERING;
                            new_value = m_mainwindow.IgnoreDefering ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_WITHACTVALUE:
                            new_name = XmlParam.BF_API_XML_WITHACTVALUE;
                            new_value = m_mainwindow.WithActValue ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_MANYPARAMETER_XML:
                            new_name = XmlParam.BF_API_XML_MANYPARAMETER_XML;
                            new_value = m_mainwindow.ParametersPath;
                            break;
                       
                        #endregion

                        #region COMMON FUNCTIONS
                        case XML_PARAM_NAME.BF_API_XML_TARGETTYPE:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.TargetType;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJECTHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.ObjectHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_CHILDHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.ChildHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ATTRIBUTE_TYPE:
                            new_name = XmlParam.BF_API_XML_ATTRIBUTE_TYPE;
                            new_value = m_mainwindow.AttributeName;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ATTRIBUTE_VALUE:
                            new_name = XmlParam.BF_API_XML_VALUE;
                            new_value = m_mainwindow.AttributeValue;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_WITH_PARAMETER:
                            new_name = XmlParam.BF_API_XML_WITH_PARAMETER;
                            new_value = m_mainwindow.WithParameter ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJECT_STATE:
                            new_name = XmlParam.BF_API_XML_STATE;
                            new_value = m_mainwindow.ObjectState.ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_CHECK_TEXT:
                            lExState = int.Parse(m_mainwindow.UI_CHCKTEST.Tag.ToString());
                            strExsate = lExState.ToString();
                            new_name = XmlParam.BF_API_XML_EXSTATE;
                            new_value = m_mainwindow.CheckText ? strExsate : " ";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_CHECK_ERROR:
                            lExState = int.Parse(m_mainwindow.UI_CHCKERROR.Tag.ToString());
                            strExsate = lExState.ToString();
                            new_name = XmlParam.BF_API_XML_EXSTATE;
                            new_value = m_mainwindow.CheckError ? strExsate : " ";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_CHECK_ARCHIVED:
                            lExState = int.Parse(m_mainwindow.UI_CHCKARCHIVED.Tag.ToString());
                            strExsate = lExState.ToString();
                            new_name = XmlParam.BF_API_XML_EXSTATE;
                            new_value = m_mainwindow.CheckArchived ? strExsate : " ";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_CHECK_ING:
                            lExState = int.Parse(m_mainwindow.UI_CHCKING.Tag.ToString());
                            strExsate = lExState.ToString();
                            new_name = XmlParam.BF_API_XML_EXSTATE;
                            new_value = m_mainwindow.CheckIng ? strExsate : " ";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HDL1:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.Hdl1;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HDL2:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.Hdl2;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_DATAFLAG:
                            new_name = XmlParam.BF_API_XML_DATAFLAGS;
                            new_value = m_mainwindow.GetXmlFlags().ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ARCHIV:
                            new_name = XmlParam.BF_API_XML_ARCHIVETYPE;
                            new_value = m_mainwindow.ArchiveTyp;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_EXTDATA:
                            new_name = XmlParam.BF_API_XML_EXTDATA;
                            new_value = m_mainwindow.CheckExtData ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_WITHOUTTAG:
                            new_name = XmlParam.BF_API_XML_NOTAG;
                            new_value = m_mainwindow.CheckWithoutTag ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_WITHPCELL:
                            new_name = XmlParam.BF_API_XML_PCELL;
                            new_value = m_mainwindow.CheckWithPCell ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ARCHIVTAG:
                            new_name = XmlParam.BF_API_XML_ARCHIVETYPE;
                            new_value = m_mainwindow.ArchiveTag;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_EVTID4ADVISE:
                            new_name = XmlParam.BF_API_XML_SIS_STARTEVENTID;
                            new_value = m_mainwindow.EventId4Advise;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_WITHACTVALUEOBJ:
                            new_name = XmlParam.BF_API_XML_WITHACTVALUE;
                            new_value = m_mainwindow.WithActValueObj ? "1" : "0";
                            break;   
                        #endregion
                        #region SUBFOLDER
                        case XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDL:
                            new_name = XmlParam.BF_API_XML_SUBFOLDER_HDL;
                            new_value = m_mainwindow.SubFolderHDL;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SUBFOLDERHDLEMPTY:
                            new_name = XmlParam.BF_API_XML_SUBFOLDER_HDL;
                            new_value = m_mainwindow.SubFolderHDLEmpty;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SUBOJECTHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.SubFolderHDL;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SUBNAME:
                            new_name = XmlParam.BF_API_XML_NAME;
                            new_value = m_mainwindow.SubName;
                            break;
                        #endregion

                        #region API | SIS EVENTS
                        case XML_PARAM_NAME.BF_API_XML_EVENTHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.EventHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SIS_POSITION4EVENTID:
                            new_name = XmlParam.BF_API_XML_SIS_POSITION4EVENTID;
                            new_value = m_mainwindow.SIS_Position4EventID.ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_SIS_EVENTID:
                            new_name = XmlParam.BF_API_XML_SIS_EVENTID;
                            new_value = m_mainwindow.SIS_EventID;
                            break;
                        #endregion
                        #region NOTIY FILTER
                        case XML_PARAM_NAME.BF_API_XML_SIS_ADVISEOPTIONS:
                            new_name = XmlParam.BF_API_XML_SIS_ADVISEOPTIONS;
                            new_value = m_mainwindow.SIS_AdviseOptions ? "1" : "0";
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJECT_TYPE:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjectType;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_NOTIFY_TYPE:
                            new_name = XmlParam.BF_API_XML_NOTIFYTYPE;
                            new_value = m_mainwindow.NotifyType;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_EXTTRANSFORM:
                            new_name = XmlParam.BF_API_XML_EXTTRANSFORM;
                            new_value = m_mainwindow.Extransform;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_AUTOREADVISE:
                            new_name = XmlParam.BF_API_XML_SIS_AUTOREADVISE;
                            new_value = m_mainwindow.AutoReAdvise;
                            break;
                            
                        #endregion

                        #region LOGBOOKPRINT
                        case XML_PARAM_NAME.BF_API_XML_ACT_RENAMED:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActRenamed;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_CHAINED:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActChained;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_UNCHAINED:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActUnchained;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_REL_TEST:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActRelTest ;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_REL_PROD:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActRelProd;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_ACHIVED:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActArchived;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_IMPORTED:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActImported;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_NEW_PROD:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActNewProd;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACT_DELETED:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ActDeleted;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_PCELL:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjPcell;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_LIB:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjLibrary;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_RECIPE:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjRecipe;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_FORMCAT:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjFormulaCat;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_FORMULA:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjFormula;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_ORDERCAT:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjOrderCat;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_ORDER:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjOrder;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_BATCH:
                            new_name = XmlParam.BF_API_XML_ACTION_TYPE;
                            new_value = m_mainwindow.ObjBatch;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OBJ_MATERIAL:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = m_mainwindow.ObjMaterial;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_LOGBUSERNAME:
                            new_name = XmlParam.BF_API_XML_USERNAME;
                            new_value = m_mainwindow.LogbUserName;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_DTPSTARTTIME:
                            new_name = XmlParam.BF_API_XML_STARTTIME;
                            new_value = m_mainwindow.LogbStartTimeW3C;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_DTPENDTIME:
                            new_name = XmlParam.BF_API_XML_ENDTIME;
                            new_value = m_mainwindow.LogbEndTimeW3C;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_LBOBJECTHDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.LbObjectHdl;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_OUTPUTFILEANDPATH:
                            new_name = XmlParam.BF_API_XML_OUTPUTFILEANDPATH;
                            new_value = m_mainwindow.OutPutFileAndPath;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_TEMPLATEPATH:
                            new_name = XmlParam.BF_API_XML_TEMPLATEFILEANDPATH;
                            new_value = m_mainwindow.TemplatePath;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PDFPATH:
                            new_name = XmlParam.BF_API_XML_OUTPUTFILEANDPATH;
                            new_value = m_mainwindow.PDFPath;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_LCID:
                            new_name = XmlParam.BF_API_XML_LCID;
                            new_value = m_mainwindow.LcId;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_TIMEZONEKEYNAME:
                            new_name = XmlParam.BF_API_XML_TIMEZONEKEYNAME;
                            new_value = m_mainwindow.TimeZonekeyName;
                            break;;
                        case XML_PARAM_NAME.BF_API_XML_ADDPARAMETER_NAME1:
                            new_name = XmlParam.BF_API_XML_NAME;
                            new_value = m_mainwindow.ParameterName1;
                            break;;
                        case XML_PARAM_NAME.BF_API_XML_ADDPARAMETER_VALUE1:
                            new_name = XmlParam.BF_API_XML_VALUE;
                            new_value = m_mainwindow.ParameterValue1;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ADDPARAMETER_NAME2:
                            new_name = XmlParam.BF_API_XML_NAME;
                            new_value = m_mainwindow.ParameterName2;
                            break; ;
                        case XML_PARAM_NAME.BF_API_XML_ADDPARAMETER_VALUE2:
                            new_name = XmlParam.BF_API_XML_VALUE;
                            new_value = m_mainwindow.ParameterValue2;
                            break;
                            
                        #endregion

                        #region COMMAND MANAGER
                        case XML_PARAM_NAME.BF_API_XML_GETERRORTEXT_ERROR:
                            new_name = XmlParam.BF_API_XML_GETERRORTEXT_ERROR;
                            new_value = m_mainwindow.ErrorCode1;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_GETERRORTEXT_LANGUAGE:
                            new_name = XmlParam.BF_API_XML_GETERRORTEXT_LANGUAGEID;
                            new_value = m_mainwindow.LanguageID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACK_BATCH_HDL:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.AckBatchHdl1;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACK_CONTID:
                            new_name = XmlParam.BF_API_XML_CONT_ID;
                            new_value = m_mainwindow.AckContID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACK_TERMID:
                            new_name = XmlParam.BF_API_XML_TERM_ID;
                            new_value = m_mainwindow.AckTermID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ACK_COOKIE:
                            new_name = XmlParam.BF_API_XML_ACKNOWLEDGE_COOKIE;
                            new_value = m_mainwindow.Cookie;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_PCELLHDL_:
                            new_name = XmlParam.BF_API_XML_HDL;
                            new_value = m_mainwindow.PCellHdl_;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_GETDATAPCELLPARTID:
                            new_name = XmlParam.BF_API_XML_GETDATA4PCELL_PARTID;
                            new_value = m_mainwindow.PCellPartID;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_ARCHIVETYPE:
                            new_name = XmlParam.BF_API_XML_ARCHIVETYPE;
                            new_value = m_mainwindow.ArchiveType;
                            break;
                            
                        #endregion

                        #region HISTORICAL SIS EVENTS
                        case XML_PARAM_NAME.BF_API_XML_HSIS_OBJECTTYPE:
                            new_name = XmlParam.BF_API_XML_OBJECTTYPE;
                            new_value = ((int)IPCS7_SBAPI_XLib.BF_API_OBJECT_TYPE.BF_API_OBJECT_UNIT).ToString();
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HSIS_STARTTIME:
                            new_name = XmlParam.BF_API_XML_STARTTIME;
                            new_value = m_mainwindow.HSIS_StartTime;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HSIS_ENDTIME:
                            new_name = XmlParam.BF_API_XML_ENDTIME;
                            new_value = m_mainwindow.HSIS_EndTime;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HSIS_STARTEVENTID:
                            new_name = XmlParam.BF_API_XML_SIS_STARTEVENTID;
                            new_value = m_mainwindow.HSIS_StartEventId;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HSIS_ENDEVENTID:
                            new_name = XmlParam.BF_API_XML_SIS_ENDEVENTID;
                            new_value = m_mainwindow.HSIS_EndEventId;
                            break;
                        case XML_PARAM_NAME.BF_API_XML_HSIS_OBJECTHDL:
                        new_name = XmlParam.BF_API_XML_HDL;
                        new_value = m_mainwindow.UI_CBHSISCHANGED.Items.GetItemAt(a).ToString();
                        ++a;
                        break;
                        #endregion
                        default:
                            throw new NotImplementedException("Attribute " + attrib + " has not been implemented in FunctionParameters.cs!");
                    }
                    if (new_value == null)
                        throw new ArgumentNullException("Parameter " + attrib + " is not set!");
                    names[i] = new_name;
                    values[i] = new_value;
                }
                AttributeParams.Add(names);
                ValueParams.Add(values);
            }
            a = 0;
        }
    }
}
