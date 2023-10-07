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
using System.Xml;

namespace Siemens.Automation.bfapicmx_csharpsamplex
{
    /// <summary>
    /// Provides a list of attributes using the enumeration IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.
    /// </summary>
    static class bfapicmx_CAttributes
    {
        static Dictionary<int, string> s_attributes = null;
        public static Dictionary<int, string> Attributes
        {
            get
            {
                if (s_attributes == null)
                {
                    s_attributes = new Dictionary<int, string>();
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_NAME, "name");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_VERSION, "version");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_PARENT, "parent");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_DBID, "dbid");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_DESC, "desc");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_CODE, "code");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_PRODUCT, "product");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_USAGE, "usage");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_STATE, "state");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_EXSTATE, "exstate");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_CHAININFO, "chaininfo");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_OBJECTTYPE, "objecttype");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_SIZE, "size");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_LOVALUE, "lovalue");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_HIVALUE, "hivalue");
                    s_attributes.Add((int)IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.BF_API_ATTRIBUTE_OBJECTGUID, "objectguid");
                }
                return s_attributes;
            }
        }

        /// <summary>
        /// An extension for IDictionary that concatenates all entries to a string.
        /// The keys and values are each combined with an equality sign and then connected by seperating comma.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string ImplodeDic<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return string.Join(", ", dictionary.Select(kv => kv.Key.ToString() + "=" + kv.Value.ToString()));
        }
    }
    
    /// <summary>
    /// Provides a list of attribute names which are used in FunctionParameters to generate the xml which is sent to the API.
    /// </summary>
    public struct XmlParam
    {
        public const string BF_API_XML_XML = "xml";
        public const string BF_API_XML_VERSION_ENCODING = "version=\"1.0\" encoding=\"UTF-16\"";
        public const string BF_API_XML_APIVERSION = "600";
        public const string BF_API_XML_APPVERSION = "appversion";
        public const string BF_API_XML_SESSION = "session";
        public const string BF_API_XML_PROJECTHDL = "projecthdl";
        public const string BF_API_XML_PCELLHDL = "pcellhdl";
        public const string BF_API_XML_HDL = "hdl";
        public const string BF_API_XML_OBJECTTYPE = "objecttype";
        public const string BF_API_XML_NOTIFYTYPE = "notifytype";
        public const string BF_API_XML_NO_HDL_EXIST = "no HDL exist";
        public const string BF_API_XML_OUTPUTBAR = "outputbar";
        public const string BF_API_XML_SYNCTIME = "synctime";
        public const string BF_API_XML_HIERARCHY = "hierarchy";
        public const string BF_API_XML_AUDITTRAIL = "audittrail";
        public const string BF_API_XML_PARENT = "parent";
        public const string BF_API_XML_DB_ID = "id";
        public const string BF_API_XML_NAME = "name";
        public const string BF_API_XML_VERSION = "version";
        public const string BF_API_XML_CODE = "code";
        public const string BF_API_XML_USAGE = "usage";
        public const string BF_API_XML_STATE = "state";
        public const string BF_API_XML_EXSTATE = "exstate";
        public const string BF_API_XML_CHAININFO = "chaininfo";
        public const string BF_API_XML_CONTINUE = "continue";
        public const string BF_API_XML_STAYINCONTIMODE = "stayincontimode";
        public const string BF_API_XML_DESCRIPTION = "desc";
        public const string BF_API_XML_DEFERRED = "deferred";
        public const string BF_API_XML_DATATYPE_NAME = "datatypename";
        public const string BF_API_XML_LOVALUE = "lovalue";
        public const string BF_API_XML_VALUE = "value";
        public const string BF_API_XML_HIVALUE = "hivalue";
        public const string BF_API_XML_MATERIAL_NAME = "material";
        public const string BF_API_XML_PHYSICALUNIT_ID = "physicalunitid";
        public const string BF_API_XML_SCALING_NAME = "scalingname";
        public const string BF_API_XML_AMOUNT = "amount";
        public const string BF_API_XML_NOTSET = "";
        public const string BF_API_XML_DATATYPE_ID = "datatypeid";
        public const string BF_API_XML_USERNAME = "username";
        public const string BF_API_XML_USERNAMELONG = "usernamelong";
        public const string BF_API_XML_PASSWORD = "password";
        public const string BF_API_XML_TICKET = "ticket";
        public const string BF_API_XML_DOMAIN = "domain";
        public const string BF_API_XML_COMPUTER = "computer";
        public const string BF_API_XML_APPLICATIONID = "applicationid";
        public const string BF_API_XML_PHYSICALUNIT_NAME = "physicalunitname";
        public const string BF_API_XML_OPTLOCK = "optlock";
        public const string BF_API_XML_STARTMODE = "startmode";
        public const string BF_API_XML_PLANSTART = "planstart";
        public const string BF_API_XML_PLANEND = "planend";
        public const string BF_API_XML_ACTSTART = "actstart";
        public const string BF_API_XML_ACTEND = "actend";
        public const string BF_API_XML_SIZE = "size";
        public const string BF_API_XML_EARLIEST = "earliest";
        public const string BF_API_XML_LATEST = "latest";
        public const string BF_API_XML_CLASS = "class";
        public const string BF_API_XML_LOVALUE_ID = "lovalueid";
        public const string BF_API_XML_VALUE_ID = "valueid";
        public const string BF_API_XML_HIVALUE_ID = "hivalueid";
        public const string BF_API_XML_SCALING_ID = "scalingid";
        public const string BF_API_XML_PARAMETER_ID = "parameterid";
        public const string BF_API_XML_STRATEGYTYPE_ID = "strategytype";
        public const string BF_API_XML_ALLOCONSTART = "alloconstart";
        public const string BF_API_XML_MATERIAL_HDL = "materialhdl";
        public const string BF_API_XML_MATERIAL_XML = "materialsxml";
        public const string BF_API_XML_MANYPARAMETER_XML = "parametersxml";
        public const string BF_API_XML_ALLOCATIONS = "allocations";
        public const string BF_API_XML_FORMORMR_HDL = "formormrhdl";
        public const string BF_API_XML_ERRORCODE = "errorcode";
        public const string BF_API_XML_FILTER = "filter";
        public const string BF_API_XML_NOTAG = "notag";     // only V1 - archive
        public const string BF_API_XML_EXTDATA = "extdata";  // only V1 - archive
        public const string BF_API_XML_DATAFLAGS = "dataflags";
        public const string BF_API_XML_PCELL = "pcell";
        public const string BF_API_XML_ARCHIVETYPE = "archivetype";
        public const string BF_API_XML_ATTRIBUTE_TYPE = "attributetype";
        public const string BF_API_XML_PARAMETERTYPE = "parametertype";
        public const string BF_API_XML_PARAMETER_USAGE = "usage";
        public const string BF_API_XML_TIMEOUT = "timeout";
        public const string BF_API_XML_WITH_PARAMETER = "withparameter";
        public const string BF_API_XML_STARTTIME = "starttime";
        public const string BF_API_XML_ENDTIME = "endtime";
        public const string BF_API_XML_ACTION_TYPE = "actiontype";
        public const string BF_API_XML_CONT_ID = "contid";
        public const string BF_API_XML_TERM_ID = "termid";
        public const string BF_API_XML_CHAIN_ON_START = "chainonstart";
        public const string BF_API_XML_LOCATION_ID = "locationid";
        public const string BF_API_XML_WITH_BATCHML = "withbatchml";
        public const string BF_API_XML_BATCHML_FILENAMEANDPATH = "batchmlfilenameandpath";
        public const string BF_API_XML_PRODUCTHDL = "producthdl";
        public const string BF_API_XML_PRODUCTOBJECT = "productobject";
        public const string BF_API_XML_PRODUCT = "product";
        public const string BF_API_XML_OBJECTGUID = "objectguid";
        public const string BF_API_XML_TIMESTAMP = "timestamp";
        public const string BF_API_XML_LCID = "lcid";
        public const string BF_API_XML_TIMEZONEKEYNAME = "timezonekeyname";
        public const string BF_API_XML_SUBFOLDER_HDL = "subfolderhdl";
        public const string BF_API_XML_EXTTRANSFORM = "exttransform";

        public const string BF_API_XML_DBVERSION_ENCODING = "version=\"1.0\" encoding=\"ISO-8859-1\"";
        public const string BF_API_XML_DBNAME = "dbnamme";
        public const string BF_API_XML_WITHCOPMARERSULT = "withcompareresult";
        public const string BF_API_XML_WITHDBCOMPARE = "withdbcompare";
        public const string BF_API_XML_WORKERPROCESS = "workerprocess";
        public const string BF_API_XML_ERRDIR = "errdir";
        public const string BF_API_XML_PDFDIR = "pdfdir";
        public const string BF_API_XML_RECURSIV = "recursiv";
        public const string BF_API_XML_WATCH = "watch";


        public const string BF_API_XML_SIS_POSITION4EVENTID = "position4eventid";
        public const string BF_API_XML_SIS_EVENTID = "eventid";
        public const string BF_API_XML_SIS_STARTEVENTID = "starteventid";
        public const string BF_API_XML_SIS_ENDEVENTID = "endeventid";
        public const string BF_API_XML_SIS_ADVISEOPTIONS = "adviseoptions";
        public const string BF_API_XML_SIS_AUTOREADVISE = "autoreadvise";

        public const string BF_API_XML_GETERRORTEXT_ERROR = "error";
        public const string BF_API_XML_GETERRORTEXT_LANGUAGEID = "languageid";
        public const string BF_API_XML_ACKNOWLEDGE_COOKIE = "cookie";
        public const string BF_API_XML_GETDATA4PCELL_PARTID = "partid";
        public const string BF_API_XML_AUDITTRAIL_HOSTNAME = "host";
        public const string BF_API_XML_AUDITTRAIL_CLIENTTIMESTAMP = "timestamp";

        public const string BF_API_XML_WILDCARD_NAME = "wildcardfilter4name";
        public const string BF_API_XML_TEMPLATEFILEANDPATH = "templatefileandpath";
        public const string BF_API_XML_OUTPUTFILEANDPATH = "outputfileandpath";
        public const string BF_API_XML_IGNOREDEFERING = "ignoredeferring";
        public const string BF_API_XML_WITHACTVALUE = "withactvalue";
        public const string BF_API_XML_GAPTIMEINSEC = "gaptimeinsec";

        public const string BF_API_STARTED = "XMLParse started";
        public const string BF_API_COMPLETED = "XMLParse completed";
        public const string BF_API_ERROR = "XMLParse error";
        public const string BF_API_CREATED = "XML created";
        public const string BF_API_DELETED = "XML deleted";

    }

    /// <summary>
    /// Provides a list of tags using the enumeration IPCS7_SBAPI_XLib.BF_API_ATTRIBUTE_TYPE.
    /// The attributes are used to generate the xml which is sent to the API.
    /// </summary>
    public struct XmlTag
    {
        public const string BF_API_XMLTAG_REQUEST = "Request";
        public const string BF_API_XMLTAG_DATA = "Data";
        public const string BF_API_XMLTAG_OBJECT = "Object";
        public const string BF_API_XMLTAG_SERVER = "Server";
        public const string BF_API_XMLTAG_COLLECTION = "Collection";
        public const string BF_API_XMLTAG_VALUE = "Value";
        public const string BF_API_XMLTAG_RPE = "Rpe";
        public const string BF_API_XMLTAG_GETERRORTEXT = "Geterrortext";
        public const string BF_API_XMLTAG_GETALLACTIVEOPERATORALLOCATION = "Getallactiveoperatorallocation";
        public const string BF_API_XMLTAG_GETALLACTIVEOBJECTS = "Getallactiveobjects";
        public const string BF_API_XMLTAG_BATCH = "Batch";
        public const string BF_API_XMLTAG_RESULT = "Result";
        public const string BF_API_XMLTAG_AUDITTRAIL = "Audittrail";
        public const string BF_API_XMLTAG_ACKNOWLEDGE = "Acknowledge";
        public const string BF_API_XMLTAG_GETDATA4PCELL = "Getdata4pcell";
        public const string BF_API_XMLTAG_GETPROJECTSETTINGS = "Getprojectsettings";
        public const string BF_API_XMLTAG_PCELL = "Pcell";

        public const string BF_API_XMLTAG_FILE = "File";
        public const string BF_API_XMLTAG_TIME = "Time";
        public const string BF_API_XMLTAG_ORIGNIAL = "original";
        public const string BF_API_XMLTAG_NEW = "new";
        public const string BF_API_XMLTAG_DIR = "Dir";

        public const string BF_API_XMLTAG_PARAMETER_PROCESS = "PP";
        public const string BF_API_XMLTAG_PARAMETER_INPUT = "PI";
        public const string BF_API_XMLTAG_PARAMETER_OUTPUT = "PO";
    }
}
