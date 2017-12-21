#region File Header
// ----------------------------------------------------------------------------
// File Name    : ExceptionLog.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : ExceptionLog
// Description  : 
//                
// Author       : DhineshKumar (dhinesh886@gmail.com)
// Created Date : Monday, December 07, 2014
// Updated By   : -
// Updated Date : -
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : 
// ----------------------------------------------------------------------------
#endregion File Header

namespace FBTS.Model.Common
{
    public static class Constants
    {
      
        // App directories
        public const string DefaultAppPagesDirectory = "AppPages/";
        public const string DataImportDirectory = "../../Resources/FileUploads/DataFiles/";
        public const string UserImagesDirectory = "../../Resources/FileUploads/UserImages/";
        public const string LogosDirectory = "../../Resources/FileUploads/Logos/";
        public const string LoginPageUrl = "Default.aspx";
        public const string SessionExpiryPageUrl = "../../SessionExpired.aspx";
        public const string Error404Url = "../../AppPages/Common/404.aspx";
        public const string Error500Url = "../../AppPages/Common/500.aspx";
        public const string WebPageRootHtml = "../../AppPages/";
        public const string DefaultHomeLink = "Common/Ezy_Welcome.aspx?cptitle=Welcome&mcode=MM00SM00PM00";// "Common/Ezy_Welcome.aspx?cptitle=Welcome&mcode=MM00SM00PM00";
        public const string DefaultDashboardLink = "../../Ezy_Welcome.aspx?cptitle=Welcome&mcode=MM00SM00PM00";
        public const string UserExcelFileDirectory = "../../Resources/FileUploads/ExcelFile/";
        
        //logOff page
        
        public const string LogoutIntermediatePageUrl = "../../LogoutIntermediate.aspx";
        //public const string LogoutPageUrl = "http:.//192.168.9.201/fbts/Logout.aspx";
        //public const string LogoutPageUrl = "http:.//stg-bts.ap.health.ge.com/bts/Logout.aspx";
        

        // Config Keys
        public const string SSOIDKey = "ssoIdKey";
        public const string BypassSSOAuthentication = "BypassSSOAuthentication";
        public const string ClientIdConfKey = "ClientId";
        public const string TrnsDbInfoKey = "TrnsDbInfo";
        public const string AppVersionConfKey = "AppVersion";
        public const string AppCopyRightConfKey = "CopyRight";
        public const string SSOLogOffStep1PageUrl = "SSOLogOffStep1PageUrl";
        public const string LoggingOffPageUrl = "LoggingOffPageUrl";
        public const string SSOLogOffStep2PageUrl = "SSOLogOffStep2PageUrl";
        public const string LoggedOffPageUrl = "LoggedOffPageUrl";

        //Session Keys
        public const string UserContextSessionKey = "UserContext";
        public const string DataViewTemplateSessionKey = "DataViewTemplate";
        public const string ImportDataSessionKey = "UploadFileObject";


        //QueryString Keys
        public const string DataViewTemplateIdKey = "dvid";
        public const string MenuCodeQsKey = "mcode";
        public const string ReportCodeQsKey = "rptid";
        public const string ActionQsKey = "act";
        public const string TdQsKey = "td";
        public const string StageQsKey = "stage";
        public const string TxnIdQsKey = "id";


        public const string BasicTxnEditUrlFormat =
            "{0}{1}&" + TxnIdQsKey + "={2}&" + TdQsKey + "={3}&" + StageQsKey + "={4}";
        // Messaging Constants
        public const string DefaultMessageHeader = "Message!";
        public const string DefaultSaveErrorMessage = "Unexpected Error Occured while saving the data.For more details please check the Log";
        public const string UploadFaildMessage = "File Upload Failed. Please check the log for further information.";
        public const string ImportFaildMessage = "Data Import Failed. Please check the log for further information.";
        public const string ImportSuccessMessage = "File uploaded and Data Transferred successfully.";
        public const string DataProcessSuccessMessage = "Data Processed successfully.";
        public const string DataProcessFailMessage = "There is no data to process. Please import data and tryagain.";
        // Alerts CSS
        public const string MessageSuccess = "alert alert-success";
        public const string MessageInformation = "alert alert-info";
        public const string MessageWarning = "alert";
        public const string MessageError = "alert alert-error";

        // DDL
        public const string Ddl = "DDL";
        public const string key = "key";
        public const string filter1 = "filter1";
        public const string filter2 = "filter2";
        public const string filter3 = "filter3";
        public const string filter4 = "filter4";
        public const string DdldefaultText = "Select Default Text";
        public const string masterType = "Master Type";

        //// Default Actions
        public const string ProcessAction = "P";
        public const string ViewAction = "V";
        public const string InsertAction = "I";
        public const string UpdateAction = "U";
        public const string DeleteAction = "D";
        public const string BanAction = "B";
        public const string UnBanAction = "UB";
        public const string CancelAction = "C";
        public const string ApproveAction = "A";
        public const string ExtendAction = "E";
        public const string SchedululeAction = "S";
       

        //// Dataview Related Constants
        public const string AdminDesignationId = "SA";
        public const string DvStages = "ST";
        //public const string DvCriteria = "CR";
        //public const string DvActions = "AN";
        //public const string DvFields = "FL";
        public const string DvMode = "MD";
        public const string DvCType = "CT";
        public const string DvTeam = "TM";
        //public const string DvCreateNewAction = "NW";
        //public const string DvConfigBranch = "BRANCH_FROM";
        //public const string DvConfigBu = "BU";
        public const string DvConfigWh = "WH";
        //public const string DvConfigDateFrom = "DATE_FROM";
        //public const string DvConfigDateTo = "DATE_TO";
        //public const string DvConfigTd = "TD";
        //public const string DvConfigStage = "STAGE";
        //public const string DvConfigSort = "SORTFLD";
        //public const string DvConfigSortType = "SORTTYPE";
        //public const string DvConfigCType = "CTYPE";
        //public const string DvConfigTrnType = "TYPE";
        //public const string DvConfigMode = "MODE";
        //public const string DvConfigUserId = "USERID";
        //public const string DvConfigDvId = "DVID";
        //public const string DvConfigSp = "SP";
        public const string DvConfigXmlRoot = "<CONFIGURATIONS>{0}</CONFIGURATIONS>";
        public const string DvConfigXmlNode = "<CONFIGURATION><CONFIGTYPE>{0}</CONFIGTYPE><CONFIGVALUE>{1}</CONFIGVALUE></CONFIGURATION>"; 

        public const string RptParaUserId = "USERID";
        public const string RptParaRptId = "RPTID";
        public const string RptParaXmlRoot = "<PARAMETERS>{0}</PARAMETERS>";
        public const string RptParaXmlNode = "<PARAMETER><PARAMETERCODE>{0}</PARAMETERCODE><PARAMETERVALUE>{1}</PARAMETERVALUE></PARAMETER>";
        public const string RptStoredProcedure = "Usp_Reports_Wrapper";

        ////Menu Related
        public const string ActiveMenuUlCss = "open";
        public const string ActiveMenuAnchorCss = "active";
        public const string SelectedSubMenuCss = "active";
        public const string HrefJs = "javascript:;";
        public const string SubMenuPlaceHolder = "SubMenuMarkup";

        //// Record Types
        public const string BranchCType = "A";
        public const string SysDesignation = "SYSDESIG";
        public const string SysDesignationCType = "C";
        public const string DeptCType = "A";
        public const string WHCType = "W";

        public const string DeviationType = "DV";
        public const string OrderingType = "OR";

        //public const string PartType = "PT";
        public const string ReferencesType = "REF";
        public const string LocalSaleOrder = "LSO";
        public const string ForcastingRequest = "FCR";
        public const string LocalType = "LT";
        public const string DefaultCurrencyCode = "IND";
        public const decimal DefaultCurrencyRate = 1;
        public const string DefaultInvPost = "N";


        //public const string DefaultAnalysisType = "A";
        public const string UnitTypes = "UN";
        //public const string UomModule = "PART";
        public const string CompanyCType = "R";
        public const string BuCType = "B";
       
        public const string UserType = "U";
        //public const string SpCheckRelationType = "MSTR"; 
        //public const string ComponentType = "CDTL";
        public const string MatGroupClass = "MCLS";
        public const string MatGroupType = "MTYP";
        public const string MatGroupGroup = "MGRP";
        //public const string FinancialAttribute = "FA";
        public const string Vendors = "C";
        public const string Customers = "D";
        
        //public const string AccountAddress = "ACAD";
        //public const string Addresses = "ADSS";
        //public const string Address = "ADDS";
        //public const string Contact = "CONT";
        //public const string LedgerList = "ADTL";
        public const string DefaultAddressCode = "BB00";
        //public const string DefaultContactCode = "CC00";
        //public const string DefaultBankCode = "BK00";
        //public const string EquipmentType = "E";
        //public const string ResourceType = "S";
        //public const string ProcessStageType = "X";
        //public const string MaintenanceType = "M";
        //public const string MaintenanceCategory = "MC";
        public const string Location = "PL";
        //public const string Grade = "PG";
        //public const string EmployeeDesignation = "PD";
        //public const string HRPayroll = "HP";
        //public const string OperationalParameter = "OP";
        //public const string Dept = "PS";
        //public const string Bank = "B";
        //public const string FinancialGp = "F";
        public const string CountryType = "G";
        //public const string Td = "TD";
        
        //public const string Party = "PTY";
        //public const string Parts = "PRTS";
        public const string TxnOrderType = "OR";
        public const string TxnBackOrderType = "BO";
        public const string TxnStockTransferType = "ST";
        public const string TxnDebriefingType = "DB";
        public const string TxnTrackingType = "TK";
        public const string GSPOType = "GP";
        public const string C09Type = "C0";
        //public const string TxnSchType = "SCH";
        //public const string BillWise = "BW";
        //public const string VendorType = "LE";
        //public const string CustomerType = "L";
        public const string PurchaseRequestTdType = "PR";
        public const string SaleRequestTdType = "SR";
        //public const string PurchaseOrderTdType = "PO";
        //public const string PurchaseInvoiceTdType = "PI";
        public const string SalesOrderTdType = "SO";
        //public const string CashBankJournalListType = "CBJL";
        //public const string BankChequeViewType = "V";
        //public const string BankChequeReadUsedType = "RU";
        //public const string BankChequeReadAvailableType = "RA";
        //public const string BankChequeReadCancelledType = "RC";
        //public const string BankChequeUpdateCancel = "C";
        //public const string BankChequeUpdateUnCancel = "UC";
        //public const string RecurringJournalListType = "RJL";
        //public const string PDCBankJournalListType = "PDBJ";
        //public const string PDCListType = "PDC";
        //public const string PDCBillListType = "PDBL";
        //public const string AccountMappingListType = "ACCM";
        //public const string AccountRelatedBranchsType = "ACRB";
        //public const string VouchersType = "VN";
        //public const string VendorSetupType = "VNS";
        //public const string AccountCurrenyListType = "ACUR";

        //// Lebels
        public const string LabelVendor = "Vendor";
        public const string LabelCustomer = "Customer";
        public const string LabelDealer = "Dealer";
        public const string LabelWarehouse = "Warehouse";
        //public const string LabelAddressCode = "Address Code";
        //public const string LabelAddress = "Address";
        //public const string LabelTxnNumber = "Txn Number";
        //public const string LabelTxnDate = "Txn Date";


        //// Sp Types
        public const string RetriveList = "L";
        public const string RetriveForm = "F";
        public const string ProcessType = "P";
        public const string RetriveTempType = "T";
        public const string RetriveBranchesType = "B";
        public const string RetriveOfficersType = "O";
        public const string RetriveAlternativeView = "ALTV";

        public const string CategoryType = "CAT";

        //// DDL Filters
        public const string DdlFilterCountry = "country";
        //public const string DdlFilterAccountBalance = "Account Balance";
        public const string DdlFilterDLevel = "DesignationLevel";
        public const string DdlFilterUser = "USER";
        //public const string DdlFilterMode = "mode";
        //public const string DdlFilterCType = "ctype";
        //public const string DdlFilterTrnType = "trntype";
        //public const string DdlFilterModule = "module";
        //public const string DdlFilterAnalysis = "analysisCode";
        //public const string DdlFilterAnalysisDesp = "analysisDescription";
        public const string DdlFilterClass = "CL";
        public const string DdlFilterTypes = "TYP";
        public const string DdlFilterGroups = "GRP";
        //public const string DdlFilterUomTypes = "uomtypes";
        //public const string DdlFilterUomModule = "uommodule";
        //public const string DdlFilterAccounts = "accounts";
        //public const string DdlFilterLType = "ltype";
        //public const string DdlFilterLMode = "lmode";
        //public const string DdlFilterLSub = "lsub"; 
        //public const string DdlFilterCategory = "categories";
        //public const string DdlFilterTrnTd = "td";
        //public const string DdlFilterJournalAccount = "al";
        //public const string TypeofLedger = "modul";
        //public const string JournalType = "TR";
        //public const string PayrollClass = "HC";
        //public const string IntermediateTotal = "XT";
        //public const string NetAmount = "NT";
        //public const string Allowance = "HA";
        //public const string BasicPay = "HV";
        //public const string Earning = "HE";
        //public const string ESIPF = "HR";
        //public const string Deduction = "HD";
        //public const string TransactionType = "TR";
        //public const string DdlFilterMenuLevel = "MenuLeve";
        //public const string BOMParameters = "BP";
        //public const string OperationParameters = "OP";


        //// DDL Types
        public const string DdlCountry = "CTRY";
        public const string DdlBranch = "BRH";
        public const string DdlDesg = "DESS";
        public const string DdlDepartment = "DEPT";
        public const string DdlWh = "WH";
        public const string DdlState = "STE";
        public const string DdlCatHeaderData = "MCTD";
        public const string DdlMATCategories = "MCAT";
        public const string DdlRegion = "REGN";
       


        //public const string DdlAccountBalance = "ALO";
        //public const string DdlAccountType = "LAL";
        //public const string DdlGrade = "GRA";
        
        //public const string DdlParameter = "PARA";
        //public const string DdlEmployeeId = "EMP";
        //public const string DdlExpression = "EXP";
        //public const string DdlParaCode = "PCOD";
        //public const string DdlAccType = "ACTY";
        //public const string DdlBOMParameters = "BOMP";

        //public const string DdlAccPara = "OPAC";
        //public const string DdlJournalLedger = "JACC";
        //public const string DdlCurrency = "TCUR";
        
        
        public const string DdlRManager = "RMGR";
        
        //public const string DdlBank = "BANK";
        //public const string DdlOperationalParameter = "OPTY";
        //public const string DdlDesignation = "DESG";
        //public const string DdlBu = "BU";
        
        //public const string DdlTd = "TD";
        //public const string DdlTxnTd = "TXNTD";
        //public const string DdlSchTd = "SCHTD";
        //public const string DdlModules = "MM";
        //public const string DdlSubModules = "SMM";
        //public const string DdlMenuIcons = "ICN";
        //public const string DdlMenu = "MENU";
        public const string DdlWFComponents = "WFC";
        //public const string DdlFCategories = "FCC";
        //public const string DdlLedger = "LG";
        //public const string DdlFGroup = "FG";
        //public const string DdlAGroup = "AG";
        //public const string DdlFType = "OPTN";
        //public const string DdlLType = "LTY";
        //public const string DdlMaterialClass = "MCLS";
        //public const string DdlMaterialType = "MTYP";
        //public const string DdlMaterialGroup = "MGRP";
        //public const string DdlOptions = "OPTN";
        //public const string DdlParties = "PTY";
        //public const string DdlPartyAddress = "PADR";
        //public const string DdlParts = "PRTS";
        //public const string DdlParties1 = "APFG";
        //public const string DdlPurchaseOrderTD = "POL";
        //public const string DdlSalesOrderTD = "SOL";
        //public const string DdlPurchaseRequestTD = "PRL";
        //public const string DdlJournal = "JRN";
        //public const string DdlJRN = "JRT";
        //public const string DdlCurrencyType = "CUR";
        //public const string DdlJournalType = "J";
        //public const string DdlCashBankJournalType = "CBJ";
        //public const string DdlSalesPurchaseJournalType = "SPJ";
        //public const string DdlJournalHeaderType = "JHD";
        //public const string DdlJournalDetalType = "JDTL";
        //public const string DdlCheckNoType = "CHKL"; 
        //public const string DdlAnalysisType = "ANA";
        //public const string DdlTable = "TBL";
        //public const string DdlTableColumn = "TBLC";

        //// DDL Default Text
        public const string DdlDefaultTextCountry = "Select Country";
        public const string DdlDefaultTextBranch = "Select Branch";
        public const string DdlDefaultTextDesig = "Select Designation";
        public const string DdlDefaultTextDept = "Select Department";
        public const string DdlDefaultTextWh = "Select Wh";
        public const string DdlDefaultTextState = "Select State";
        public const string DdlDefaultTextModality = "Select Modality";
        public const string DdlDefaultTextLocation= "Select Location";
        public const string DdlDefaultTextTrnCurrency = "Select Currency";

        //public const string DdlDefaultTextAccountBalance = "Select Account Balance";
        //public const string DdlDefaultTextAccountType = "Select Account Type";
        //public const string DdlDefaultTextGrade = "Select Employee Grade";
        //public const string DdlDefaultTextDesignation = "Select Employee Designation";
        //public const string DdlDefaultTextParameter = "Select Parameter";
        //public const string DdlDefaultTextExpression = "Select Expression";
        //public const string DdlDefaultTextParaCode = "Select Parameter Code";
        //public const string DdlDefaultTextEmployeeId = "Select Employee ID";
        //public const string DdlDefaultTextAccountParameter = "Select Account";

        
        
        
        //public const string DdlDefaultTextUsers = "Select User";
        
        //public const string DdlDefaultTextBank = "Select Bank";
        //public const string DdlDefaultTextOperationParameter = "Select Parameter";
        //
        //public const string DdlDefaultTextBu = "Select Bu";
        public const string DdlDefaultTextRManager = "Select Reporting Manager";
        //public const string DdlDefaultTextModule = "Select Module";
        //public const string DdlDefaultTextSubModule = "Select Sub Module";
        //public const string DdlDefaultTextMenuIcons = "Select Icons";
        //public const string DdlDefaultTextComponents = "Select Components";
        public const string DdlDefaultTextCategories = "Select Categories";
        //public const string DdlDefaultTextLedger = "Select Ledger Account";
        //public const string DdlDefaultTextFGroup = "Select Groups";
        //public const string DdlDefaultTextAGroup = "Select Account Groups";
        //public const string DdlDefaultTextFType = "Select Types";
        public const string DdlDefaultTextMaterialClass = "Select Class";
        public const string DdlDefaultTextMaterialType = "Select Type";
        public const string DdlDefaultTextRegion = "ALL";
        public const string DdlDefaultTextMaterialGroup = "Select Group";
        //public const string DdlDefaultTextVendor = "Select Vendor";
        public const string DdlDefaultTextCustomer = "Select Customer/Dealer";
        //public const string DdlDefaultTextPartyAddress = "Select Address";
        public const string DdlDefaultTextUnit = "Select Unit";
        //public const string DdlDefaultTextPOL = "Select Purchase Order";
        //public const string DdlDefaultTextSOL = "Select Sales Order";
        //public const string DdlDefaultTextPRL = "Select Purchase Request";
        //public const string DdlDefaultTextJRN = "Select Journal";
        //public const string DdlDefaultTextJRNTYPE = "Select Journal Type";
        //public const string DdlDefaultTextTd = "Select Transaction Definition";
        //public const string DdlDefaultTextJournal = "Select Journal";
        //public const string DdlDefaultTextBankJournal = "Select Bank Journal";
        //public const string DdlDefaultTextCheckNo = "Select Check No";
        //public const string DdlDefaultTextMode = "Select Mode";
        //public const string DdlDefaultTextSP = "Select SP";
        //public const string DdlDefaultTextMenu = "Select Menu";
        //public const string DdlDefaultTextSubMenu = "Select Sub Menu";
        //public const string DdlDefaultTextAnalysis = "Select Analysis";
        //public const string DdlDefaultTextStage = "Select Stage";
        //public const string DdlDefaultTextAction = "Select Action";
        public const string DdlDefaultTextPartNo = "Select Part";
        public const string DdlDefaultTextFCNo = "Select FC #";
        public const string DdlDefaultTextTeam = "Select Team";

        //// Txn References
        //public const string TxnRefTypeNumber = "NO";
        public const string TxnRefTypeDate = "DATE";        
        public const string TxnRefTypeRemarks = "REMARKS"; 
        //public const string TxnStaticRefNo1 = "StaticRefNo1";
        //public const string TxnStaticRefDate1 = "StaticRefDate1";
        //public const string TxnStaticRefNo2 = "StaticRefNo2";
        //public const string TxnStaticRefDate2 = "StaticRefDate2";
        //public const string TxnStaticRefNo3 = "StaticRefNo3";
        //public const string TxnStaticRefDate3 = "StaticRefDate3";

        public const int DefaultAmendmentNo = 0;
        //// Transactions
        //public const string TxnBudgetAccount = "INTERNAL";
        
        ////Form Type
        public const string TableWFComponents = "WFCOP";
        public const string TableMCatDetls = "MCATD";
        public const string TableMCatHeader = "MCATH";
        public const string TableMaterials= "MAT";
        public const string TableAccounts = "ACCS";
        public const string TableOrderDetail = "ORDDTL";
        public const string TableFolloup = "FLUP";
        public const string TableFolloupApprove = "FLAPRO";
        public const string TableInvoice = "INV";
        public const string FieldRequestReference = "FRREF";
        public const string OrderHeaderReference = "ORHREF";
        public const string FCNumberAndQuantity = "FCQTY";
        public const string TableDashBoard = "DSH";
        public const string FlolloupPendingOrder = "FLPO";
        public const string FlolloupPendingFollowup = "FLDTPO";
        public const string FlolloupChangeStatusOrAlternativepart = "FLCSAN";
        public const string BilledPart = "BILLED";
        public const string AuditLog = "AUDIT";
        public const string AlternativePart = "ALTND";

        public const string ForcastingHeader = "Forcasting #";
        public const string FRHeader = "FR #";
        public const string SRHeader = "SR #";
        public const string StatusHeader = "Status";
        public const string ReqLocHeader = "Reqested Location";
        public const string FRSRHeader = "FR/SR #";
        public const string MainSRHeader = "Main SR #";
        public const string RelatedSRHeader = "Related SR #";
        public const string PrcessingDateHeader = "Processing Date";
        public const string OrderingNuberHeader = "Order #";
        public const string InvoiceNuberHeader = "Invoice #";
        public const string LocationLablel = "Location";
        public const string RegionLable = "Region";
        public const string ModalityLabel = "Modality";
        public const string StringOk = "Ok";
        public const string StringNotOk = "Not Ok";

        public const string ForecastingType = "FR";
        public const string BillTrackingType = "BT";
        public const string TaxType = "Inclusive";
        public const string FreshOrder = "FO";
        public const string FreshDeviationOrder = "FD";
        public const string PendingOrder = "PO";
        public const string PendingDeviationOrder = "PDO";
        public const string AlternativePartNeeded = "AN";
        public const string DeviationOrder = "DO";
        public const string Billed = "BL";
        public const string BackToOrder = "TO";
        public const string ChangeStatus = "CS";
        public const string PartReserved = "PR";
        public const string SystemIssue="SI";
        public const string SRValidationType = "SRVD";
        public const string ProfileValidationType = "NUVD";
        public const string SendBackValidation = "SBFD";
        //public const string AccGroupType="AG";
        //public const string LedgerAccType = "LA";
        //public const string SubLedgerAccType = "SLA";
        //public const string AccGroupSub = "0";
        public const string LedgerSub = "1";
        //public const string SubLedgerSub = "2";
        //public const string ContactType = "C";
        //public const string BankType = "B";
        //public const string BankLType = "K";
        //public const string CashLType = "S";
        //public const string BankJournalType = "BB";
        //public const string CashJournalType = "CB";
        //public const string JournalVoucherType = "JB";
        //public const string CashBankSLNO = "001";
        //public const string BankChequeType = "BCHK";
        public const string DebitType = "D";
        public const string CreditType = "C";
        public const string SalesInvoice="SI";
        //public const string PurchaseInvoice="PI";
        //public const string SalesDebitNote = "SD";
        //public const string SalesCreditNote = "SC";
        //public const string PurchaseDebitNote = "PD";
        //public const string PurchaseCreditNote = "PC";
        public const string AllType = "ALL";
        //public const string SalesType = "1";
        //public const string PurchaseType = "2";
        //public const string PartyAccount = "PARTY ACCOUNT";
        public const string ASSET = "A";
        public const string EXPENSE = "E";
        public const string INCOME = "I";
        //public const string LIABILITIES = "L";
        //public const string RecurringJournalTableName = "M_JOURNAL_TMPL";
        public const string PartType = "P";
        //public const string ReceiptType = "R";
        //public const string TRNPDCStatus = "PD";
        public const string TRNLogedOFF = "LU";
        public const string TRNInProcessOFF = "IP";
        public const string TRNCompletedOFF = "FI";
        public const string TRNPendingOFF = "PN";
        public const string TRNCancelOFF = "CN";
        public const string VerificationDeviation = "VD";//Report
        public const string GPRSC09 = "GC";
        //public const string HEADERTABLETYPE = "H";
        //public const string DETAILTABLETYPE = "D";
        //public const string PERSONALINFO = "P";
        //public const string HRINFO = "HR";
        //public const string RecurringExpressionCode = "R";
        //public const string MainMenu = "0";
        //public const string SubMenu = "1";

        //public const string ReferanceNo = "REFNO00";
        //public const string ReferanceDate = "REFDT00";
        //public const string OnAccount = "OA";
        //public const string Regular = "RG";
        //public const string MultipleCurrencyControlType = "MULTCURRENCY";
        //public const string TDSControlAccType = "TDS";
        //public const string TCSControlAccType = "TCS";
        //public const string DefaultChequeNo = "Cheque No";
        //public const string DefaultChequeDate = "Cheque Date";
        
        // Date Related
        /// <summary>
        /// Default Date as String "1900-01-01"
        /// </summary>
        public const string DefaultDate = "1900-01-01"; 
        // DateFormats
        /// <summary>
        ///     Output Format[YYYY-MM-DD]
        /// </summary>
        public const string Format01 = "yyyy-MM-dd";

        /// <summary>
        ///     Output Format[DD/MM/YYYY]
        /// </summary>
        public const string Format02 = "dd/MM/yyyy";

        /// <summary>
        ///     Output Format[DD-MM-YYYY]
        /// </summary>
        public const string Format03 = "dd-MM-yyyy";

        /// <summary>
        ///     Output Format[MM/DD/YYYY]
        /// </summary>
        public const string Format04 = "MM/dd/yyyy";

        /// <summary>
        ///     Output Format[YYYY-MM-DD]
        /// </summary>
        public const string Format05 = "MM-dd-yyyy";

        /// <summary>
        ///     Output Format[MM/DD/YYYY 12:00:00 AM]
        /// </summary>
        public const string Format06 = "MM/dd/yyyy hh:mm:ss tt";

        /// <summary>
        ///     Output Format[DD/MM/YYYY 12:00:00 AM]
        /// </summary>
        public const string Format07 = "dd/MM/yyyy hh:mm:ss tt";

        // Current Date Types
        /// <summary>
        ///     Date Only
        /// </summary>
        public const string CdFormat01 = "DO";

        /// <summary>
        ///     Date and Time
        /// </summary>
        public const string CdFormat02 = "DT";

        /// <summary>
        ///     Time Only
        /// </summary>
        public const string CdFormat03 = "TO";       

        //Decimal Formate 
        public const string DecimalFormate = "0.00";

        public const string UserImageFormats = ".png|.jpg";

        // Number Generation
        public const string NewAutoNumberSequence = "AUTO_NUMBER||ISEQUENCE||{0}||{1}";
        //public const string NewNumberSequenceTxns = "AUTO_NUMBER||NUMBER";
        //public const string ParentMenuSequence = "NEW_PM";
        //public const string ReccuringJournalSequence = "New_RJ";
        //public const string PDCSequence = "New_PDC_BB";
        //public const string VoucherSequence = "New_Voucher";
        public const string FolloupNoPR = "FOLLOWUP_NOPR";
        public const string FolloupNoSR = "FOLLOWUP_NOSR";
        public const string FolloupNoSO = "FOLLOWUP_NODB_SO";
        public const string FolloupDtl = "FOLLOWUPDTL";
        public const string FieldRequestDefaultNumber = "FR0000000";
        public const string FieldRequestGenrateNumberType = "New_PR";
        public const string VerificationGenrateNumberType = "New_SR";
        public const string InvoiceGenrateNumberType = "MAX_INV";
        public const string TeamGenrateNumberType = "MAX_TM";
        public const string StageGenrateNumberType = "MAX_ST";
        public const string ForcastingSpacialist = "FC0000000";
        public const string Team = "000000000";
        public const string VarificationDefaultNumber = "SR0000000";
        public const string SalesOrderDefaultNumber = "SO0000000";
        public const string SaleInvoiceDefaultNumber = "SI0000000";
        public const string MaxSlnoFromFollowupNumberType = "MAX_SLNO_FL";

        //// Delimeters
        public const string DelimeterSinglePipe = "|";
        public const string DelimeterDoublePipe = "||";

        //// Special Characters
        public const string SpaceFromHTML = "&nbsp;";
        public const string DoubleQuetsHTML = "&quot;";
        public const string DoubleQuets = @"""";
        public const string SpecialCharQuestionMark = "?";
        public const string SpecialCharAmbrasent = "&";
        public const string SpecialCharEqualTo = "=";
        public const string SpecialCharComma = ", ";
        public const string SpecialCharHifen = "-";
        public const string SpecialCharApprox = "~";
        public const string SpecialCharSinglequote = "'";

        //Delete Query Type
        public const string DeleteText = "Delete";
        public const string UnBanText = "Activate";
        public const string BanText = "Deactivate";
        public const string DeleteAllTxn = "DLALL";

        public static KeyValuePairItems ConfirmMessages = new KeyValuePairItems
         {
            new KeyValuePairItem (DeleteAllTxn,"Are you sure want to {0} the All Transaction?")
         };

        // default Labels
        public const string LabelSREnter = "SR Number";

        //default Tooltip
        public const string ToolTipSrNumberSearch = "Enter SR Number To Serch";
    }
}