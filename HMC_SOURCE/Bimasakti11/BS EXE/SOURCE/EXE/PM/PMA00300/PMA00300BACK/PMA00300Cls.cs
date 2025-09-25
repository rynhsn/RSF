using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMA00300COMMON;
using PMA00300COMMON.Param_DTO;
using PMA00300COMMON.VA_DTO;
using PMA00300BackResources;
using System.Globalization;
using log4net;
using System.Reflection.Metadata;
using R_ReportServerClient;
using R_ReportServerCommon;
using System.Text.Json;
using System.Reflection.Emit;
using R_Storage;
using R_StorageCommon;
using System.Windows.Input;
using System.Linq;
using System.Transactions;

namespace PMA00300BACK
{
    public class PMA00300Cls
    {

        private static readonly ILog _logger = LogManager.GetLogger(typeof(PMA00300Cls));
        public async Task ProcessReport(PMA00300ReportClientParameterDTO ParameterReportClient, PMA00300ParamReportDTO DbParameter)
        {
            R_Exception loEx = new R_Exception();
            string? lcMethodName = nameof(ProcessReport);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_ReportServerRule loReportRule;
            string lcReportFileName = "";
            R_FileType leReportOutputType;
            var loDb = new R_Db();
            //  DbConnection loConn = null;
            R_ConnectionAttribute _connectionAttribute;
            //Set Report Rule and Name
            loReportRule = new R_ReportServerRule(ParameterReportClient.CTENANT_CUSTOMER_ID.ToLower(), ParameterReportClient.CCOMPANY_ID.ToLower());
            _logger.Info("assign loReportRule");

            lcReportFileName = "PMA00300.frx";
            _logger.Info("Get file Frx");

            //Prepare Parameter
            leReportOutputType = R_FileType.PDF;
            string lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;
            _logger.Info("Create object Report Parameter");

            CultureInfo loCultureInfo = new CultureInfo(ParameterReportClient.CREPORT_CULTURE);

            _logger!.Info("Get Data Billing");
            List<PMA00300DataReportDTO> listDataBilling = GetDataBilling(DbParameter);

            _logger!.Info("Assign parameter for detail unit");
            DbParameter.CREPORT_TYPE = "D";
            DbParameter.CSERVICE_TYPE = "UN";
            _logger!.Info("Get Data Detail Unit");
            List<PMA00300UtilityDetail> listDataDetailUnit = GeDataDetailUnitList(DbParameter);

            _logger!.Info("Assign parameter for detail utility");
            DbParameter.CSERVICE_TYPE = "UT";
            _logger!.Info("Get Data Detail Utility");
            List<PMA00300UtilityChargesDetail> listDataDetailUtility = GeDataDetailUtilityList(DbParameter);

            _logger.Info("Set Label Report");
            var loLabel = AssignValuesWithMessages(typeof(Resources_PMA00300), loCultureInfo, new PMA00300LabelDTO());

            _logger!.Info("Get Data Logo");
            var oLogo = GetLogoCompany(DbParameter);

            R_ReportFormatDTO loReportFormat = new R_ReportFormatDTO()
            {
                DecimalSeparator = ParameterReportClient.CNUMBER_FORMAT,
                GroupSeparator = ParameterReportClient.CNUMBER_FORMAT == "." ? "," : ".",
                DecimalPlaces = ParameterReportClient.IDECIMAL_PLACES,
                ShortDate = ParameterReportClient.CDATE_SHORT_FORMAT,
                ShortTime = ParameterReportClient.CTIME_SHORT_FORMAT,
            };

            _connectionAttribute = loDb.GetConnectionAttribute();
            _logger!.Info("Get Connection Attribute");

            if (listDataBilling!=  null)
            {
                foreach (var item in listDataBilling)
                {
                    //if (item.CTENANT_ID == "TNT72" )//|| item.CTENANT_ID == "TNT71")
                    //{

                    //TenantPROPERTY fromDB
                    _logger!.Info("Assign Tenant id for param");
                    DbParameter.CTENANT_ID = item.CTENANT_ID;
                    _logger!.Info("Get data VA");
                    var dataVirtualAccount = GetVAList(DbParameter);

                    _logger!.Info("Get data Unit");
                    List<PMA00300UtilityDetail> FilteredUnitList = listDataDetailUnit.Where(x => x.CREF_NO == item.CREF_NO).ToList();

                    _logger!.Info("Get data Utility");
                    var filteredGroups = listDataDetailUtility
                 .Where(x => x.CREF_NO == item.CREF_NO)
                 .GroupBy(x => x.CCHARGES_TYPE)
                 .ToDictionary(g => g.Key, g => g.ToList());

                    _logger!.Info("Seprated with different ChargeType");
                    List<PMA00300UtilityChargesDetail> FilteredUtilityList01 = filteredGroups.GetValueOrDefault("01", new List<PMA00300UtilityChargesDetail>());
                    List<PMA00300UtilityChargesDetail> FilteredUtilityList02 = filteredGroups.GetValueOrDefault("02", new List<PMA00300UtilityChargesDetail>());
                    List<PMA00300UtilityChargesDetail> FilteredUtilityList03 = filteredGroups.GetValueOrDefault("03", new List<PMA00300UtilityChargesDetail>());
                    List<PMA00300UtilityChargesDetail> FilteredUtilityList04 = filteredGroups.GetValueOrDefault("04", new List<PMA00300UtilityChargesDetail>());


                    var Header = new PMA00300BaseHeaderDTO()
                    {
                        PROPERTY_NAME = item.CPROPERTY_NAME,
                        CLOGO = oLogo.CLOGO
                    };

                    PMA00300ResultDataDTO resultData = new PMA00300ResultDataDTO()
                    {
                        Header = Header,
                        Label = (PMA00300LabelDTO)loLabel,
                        Data = item,
                        VirtualAccountData = dataVirtualAccount,
                        DataUnitList = FilteredUnitList,
                        DataUtility1 = FilteredUtilityList01,
                        DataUtility2 = FilteredUtilityList02,
                        DataUtility3 = FilteredUtilityList03,
                        DataUtility4 = FilteredUtilityList04,
                    };

                    R_GenerateReportParameter loParameter = new R_GenerateReportParameter()
                    {
                        ReportRule = loReportRule,
                        ReportFileName = lcReportFileName,
                        ReportData = JsonSerializer.Serialize(resultData),
                        ReportDataSourceName = "ResponseDataModel",
                        ReportFormat = loReportFormat,
                        ReportDataType = typeof(PMA00300ResultDataDTO).ToString(),
                        ReportOutputType = leReportOutputType,
                        ReportAssemblyName = "PMA00300COMMON.dll",
                        ReportParameter = null
                    };
                    _logger!.Info("Success get Parameter");
                    //byte[] loRtnInByte = null;

                    //_logger!.Info("Call method [R_GenerateReportByte] to Generate Report");
                    //try
                    //{
                    //var abc = R_ReportServerClientService.R_GetHttpClient();
                    var loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(R_ReportServerClientService.R_GetHttpClient(),
                         "api/ReportServer/GetReport", loParameter);
                    //menghubungkan ke API dari engine yang sudah ada
                    //}
                    //catch (Exception ex)
                    //{
                    //    loEx.Add(ex);
                    //}

                    string fileExtension = leReportOutputType.ToString();

                    _logger!.Info("Declare param to save storage");
                    var ParamSaveStorage = new ParamSaveStorageDTO()
                    {
                        CCOMPANY_ID = item.CCOMPANY_ID,
                        CUSER_ID = "SYSTEM",
                        CPROPERTY_ID = item.CPROPERTY_ID,
                        CTENANT_ID = item.CTENANT_ID,
                        CREF_NO = item.CREF_NO,
                        CLOI_AGRMT_REC_ID = item.CLOI_AGRMT_REC_ID,
                        CREF_PRD = DbParameter.CPERIOD,
                        CSTORAGE_ID = item.CSTORAGE_ID,
                        FileExtension = fileExtension,
                        REPORT = loRtnInByte
                    };

                    //Start Transaction BLOCK
                    _logger!.Info("Start Transaction Block");
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            _logger.Info("Start transcope");
                            var loConn = loDb.GetConnection();
                            _logger.Info("Get connection in transcope [Connectiin and COnecction attribute]");

                            if (loRtnInByte != null)
                            {
                                string lcStorageId = SaveReportToAzure(ParamSaveStorage, loConn, _connectionAttribute);
                                var ParamSaveBillingStatement = new ParamSaveBillingStatement()
                                {
                                    CCOMPANY_ID = item.CCOMPANY_ID,
                                    CPROPERTY_ID = item.CPROPERTY_ID,
                                    CTENANT_ID = item.CTENANT_ID,
                                    CLOI_AGRMT_REC_ID = item.CLOI_AGRMT_REC_ID,
                                    CREF_PRD = DbParameter.CPERIOD,
                                    CREF_DATE = item.CSTATEMENT_DATE,
                                    CDUE_DATE = item.CDUE_DATE,
                                    CSTORAGE_ID = lcStorageId,
                                    CUSER_ID = DbParameter.CUSER_ID,
                                };

                                SaveBillingStatementList(ParamSaveBillingStatement, loConn);
                                scope.Complete();
                            }
                        }
                        catch (Exception ex)
                        {
                            loEx.Add(ex);
                            _logger!.Error(string.Format("Log Error {0} ", ex));
                        }
                    }
                }
            }
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            //_logger.Debug(string.Format("END process method {0} on Cls", lcMethodName));
        }
        private string SaveReportToAzure(ParamSaveStorageDTO poParameter, DbConnection poConnection, R_ConnectionAttribute poConnAttribute)
        {
            R_Exception loEx = new R_Exception();

            string lcMethodName = nameof(SaveReportToAzure);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            string loResult = null;
            DbConnection loConn = null;
            R_SaveResult loSaveResult = null;
            try
            {
                var loDb = new R_Db();
                loConn = poConnection;
                var _connectionAttribute = poConnAttribute;
                var loCommand = loDb.GetCommand();

                _logger.Info("Get storage type");
                StorageType loGetStorageType = GetStorageType(poParameter, poConnection);

                R_EStorageType loStorageType = loGetStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;
                R_EProviderForCloudStorage loProvider = loGetStorageType.CSTORAGE_PROVIDER_ID!.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;


                // Add and create Storage ID
                if (string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {

                    _logger.Info("Add  storage id to save cause storage id not exist:");
                    R_AddParameter loAddParameter = new R_AddParameter()
                    {
                        StorageType = loStorageType,
                        ProviderCloudStorage = loProvider,
                        FileName = $"Report_{poParameter.CREF_NO}",
                        FileExtension = poParameter.FileExtension,
                        UploadData = poParameter.REPORT,
                        UserId = poParameter.CUSER_ID,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = poParameter.CCOMPANY_ID,
                            CDATA_TYPE = "PMT_BILLING_STATEMENT",
                            CKEY01 = poParameter.CCOMPANY_ID,
                            CKEY02 = poParameter.CPROPERTY_ID,
                            CKEY03 = poParameter.CTENANT_ID,
                            CKEY04 = poParameter.CLOI_AGRMT_REC_ID,
                            CKEY05 = poParameter.CREF_PRD,
                        }
                    };
                    _logger.Info("Call R_StorageUtility.AddFile to storage table");
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn);
                }
                else if (!string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {
                    _logger.Info("update with storage id to save cause storage id already exist:");
                    R_UpdateParameter loUpdateParameter;

                    loUpdateParameter = new R_UpdateParameter()
                    {
                        StorageId = poParameter.CSTORAGE_ID,
                        UploadData = poParameter.REPORT,
                        UserId = poParameter.CUSER_ID,
                        OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                        {
                            FileExtension = poParameter.FileExtension,
                            FileName = $"Report_{poParameter.CREF_NO}.{poParameter.FileExtension}"
                        }
                    };
                    _logger.Info("Call R_StorageUtility.UpdateFile to storage table");
                    loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, _connectionAttribute.Provider);
                }
                if (loSaveResult != null)
                {
                    loResult = loSaveResult.StorageId;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            _logger!.Info(string.Format("END process method {0} on Cls", lcMethodName));
            loEx.ThrowExceptionIfErrors();
            return loResult!;
        }
        public void SaveBillingStatementList(ParamSaveBillingStatement poParameter, DbConnection poConnection)
        {
            string? lcMethodName = nameof(SaveBillingStatementList);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection loConn = poConnection;
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_SAVE_BILLING_STATEMENT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID ", DbType.String, 20, poParameter.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLOI_AGRMT_REC_ID", DbType.String, 50, poParameter.CLOI_AGRMT_REC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CREF_PRD", DbType.String, 6, poParameter.CREF_PRD);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, poParameter.CREF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CDUE_DATE ", DbType.String, 8, poParameter.CDUE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSTORAGE_ID", DbType.String, 40, poParameter.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loDataTable = loDb.SqlExecNonQuery(loConn, loCommand, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
        }
        public List<PMA00300DataReportDTO> GetDataBilling(PMA00300ParamReportDTO poParameter)
        {
            string lcMethodName = nameof(GetDataBilling);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00300DataReportDTO>? loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = loDb.GetConnection();
                //var loConn2 = loDb.GetName();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PM_PMA00300";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD ", DbType.String, 8, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@"))
                 .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<PMA00300DataReportDTO>(loReturnTemp).ToList();
                _logger!.Info(string.Format("Convert string to datetime format on method", lcMethodName));
                if (loResult.Count > 0)
                {
                    foreach (var item in loResult)
                    {
                        item.DDUE_DATE = ConvertStringToDateTimeFormat(item.CDUE_DATE);
                        item.DSTATEMENT_DATE = ConvertStringToDateTimeFormat(item.CSTATEMENT_DATE);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
        public List<PMA00300VADTO> GetVAList(PMA00300ParamReportDTO poParameter)
        {
            string? lcMethodName = nameof(GetVAList);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new();
            List<PMA00300VADTO>? loReturn = new();
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_TENANT_VA_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID ", DbType.String, 20, poParameter.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMA00300VADTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;
        }
        public PMA00300BaseHeaderDTO GetLogoCompany(PMA00300ParamReportDTO poParameter)
        {
            var loEx = new R_Exception();
            string? lcMethodName = nameof(GetLogoCompany);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            PMA00300BaseHeaderDTO loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();
                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poParameter.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMA00300BaseHeaderDTO>(loDataTable).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            loEx.ThrowExceptionIfErrors();
            return loResult!;
        }
        public PMA00300ReportClientParameterDTO GetClientReportFormat(string poParam)
        {
            string lcMethodName = nameof(GetClientReportFormat);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            PMA00300ReportClientParameterDTO? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();

                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = $"select * from SAM_COMPANIES where CCOMPANY_ID ='{poParam}'";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<PMA00300ReportClientParameterDTO>(loReturnTemp).ToList().Any() ?
                           R_Utility.R_ConvertTo<PMA00300ReportClientParameterDTO>(loReturnTemp).FirstOrDefault()! : new PMA00300ReportClientParameterDTO();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
        private StorageType GetStorageType(ParamSaveStorageDTO poParameter, DbConnection poConnection)
        {
            string lcMethodName = nameof(GetStorageType);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            var loEx = new R_Exception();
            StorageType loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                _logger!.Info("before exec RSP_GS_GET_STORAGE_TYPE");
                loConn = poConnection;
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poParameter.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<StorageType>(loDataTable).FirstOrDefault()!;
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger!.Error(string.Format("Log Error {0} ", ex));
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            loEx.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;
        }
        public List<PMA00300UtilityDetail> GeDataDetailUnitList(PMA00300ParamReportDTO poParameter)
        {
            string? lcMethodName = nameof(GeDataDetailUnitList);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new();
            List<PMA00300UtilityDetail>? loReturn = new();
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_PMA00300";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD", DbType.String, 8, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCommand, "@CREPORT_TYPE", DbType.String, 1, poParameter.CREPORT_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CSERVICE_TYPE ", DbType.String, 2, poParameter.CSERVICE_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMA00300UtilityDetail>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;
        }
        public List<PMA00300UtilityChargesDetail> GeDataDetailUtilityList(PMA00300ParamReportDTO poParameter)
        {
            string? lcMethodName = nameof(GeDataDetailUnitList);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new();
            List<PMA00300UtilityChargesDetail>? loReturn = new();
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_PMA00300";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD", DbType.String, 8, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCommand, "@CREPORT_TYPE", DbType.String, 1, poParameter.CREPORT_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CSERVICE_TYPE ", DbType.String, 2, poParameter.CSERVICE_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMA00300UtilityChargesDetail>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;
        }
        private static DateTime ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return DateTime.Now;
            }
            else
            {
                DateTime result;
                if (pcEntity.Length == 6)
                {
                    pcEntity += "01";
                }

                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }
        private object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
        {
            object loObj = Activator.CreateInstance(poObject.GetType())!;
            var loGetPropertyObject = poObject.GetType().GetProperties();

            foreach (var property in loGetPropertyObject)
            {
                string propertyName = property.Name;
                string message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
                property.SetValue(loObj, message);
            }

            return loObj;
        }

    }
}

