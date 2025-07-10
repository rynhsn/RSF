using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Transactions;
using PMR03000Common;
using PMR03000Common.DTOs.Print;
using R_BackEnd;
using R_Common;
using R_ReportServerClient;
using R_ReportServerCommon;
using R_Storage;
using R_StorageCommon;

namespace PMR03000Back;

public class PMR03000PrintCls
{
    private LoggerPMR03000Print _logger;
    private readonly ActivitySource _activitySource;

    public PMR03000PrintCls()
    {
        _logger = LoggerPMR03000Print.R_GetInstanceLogger();
        _activitySource = PMR03000Activity.R_GetInstanceActivitySource();
    }

    // public async Task<List<PMR03000ResultDataDTO>> SendToEmail(PMR03000ReportParamDTO poParam)
    // {
    //     using var loActivity = _activitySource.StartActivity(nameof(SendToEmail));
    //     const string lcMethodName = nameof(SendToEmail);
    //     _logger.LogInfo(string.Format("START method {0}", lcMethodName));
    //
    //     var loEx = new R_Exception();
    //     List<PMR03000ResultDataDTO>? loReturn = null;
    //     DbConnection? loConn = null;
    //     DbCommand? loCommand;
    //     R_Db? loDb = new();
    //
    //     try
    //     {
    //         _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
    //
    //         R_ReportServerRule loReportRule;
    //         string lcReportFileName = "";
    //         R_FileType leReportOutputType;
    //         R_ConnectionAttribute _connectionAttribute;
    //
    //         var loParameterReportClient = await GetClientReportFormat(poParam.CCOMPANY_ID);
    //         //Set Report Rule and Name
    //         loReportRule = new R_ReportServerRule(loParameterReportClient.CTENANT_CUSTOMER_ID.ToLower(),
    //             loParameterReportClient.CCOMPANY_ID.ToLower());
    //         _logger.LogInfo("assign loReportRule");
    //
    //         lcReportFileName = poParam.CFILE_NAME.EndsWith(".frx")
    //             ? poParam.CFILE_NAME
    //             : $"{poParam.CFILE_NAME}.frx";
    //         _logger.LogInfo("Get file Frx");
    //
    //         //Prepare Parameter
    //         leReportOutputType = R_FileType.PDF;
    //         var lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;
    //         _logger.LogInfo("Create object Report Parameter");
    //
    //         var loCultureInfo = new CultureInfo(loParameterReportClient.CREPORT_CULTURE);
    //
    //         _logger.LogDebug("Generated parameters for data print", poParam);
    //         _logger.LogInfo("Get Summary Data Billing for Report");
    //         poParam.CREPORT_TYPE = "S";
    //         poParam.CSERVICE_TYPE = "";
    //         var listDataBilling = await GetDataBilling(poParameter: poParam);
    //         _logger.LogInfo("Data Billing retrieved successfully for Report");
    //
    //         _logger.LogInfo("Get Data Detail Unit");
    //         poParam.CREPORT_TYPE = "D";
    //         poParam.CSERVICE_TYPE = "UN";
    //         var listDataDetailUnit = await GeDataDetailUnitList(poParameter: poParam);
    //         _logger.LogInfo("Data Detail Unit Retrieved successfully for Report");
    //
    //         _logger.LogInfo("Get Data Detail Utility");
    //         poParam.CREPORT_TYPE = "D";
    //         poParam.CSERVICE_TYPE = "UT";
    //         var listDataDetailUtility = await GeDataDetailUtilityList(poParameter: poParam);
    //         _logger.LogInfo("Data Detail Utility Retrieved successfully for Report");
    //
    //         _logger.LogInfo("Get Data Logo");
    //         var oLogo = await GetLogoCompany(poParam);
    //         _logger.LogInfo("Company Logo Retrieved successfully for Report");
    //
    //         _logger.LogInfo("Set Label Report");
    //         var loLabel = AssignValuesWithMessages(typeof(PMR03000BackResources.Resources_Dummy_Class), loCultureInfo,
    //             new PMR03000ReportLabelDTO());
    //
    //         var Header = new PMR03000BaseHeaderDTO()
    //         {
    //             PROPERTY_NAME = poParam.CPROPERTY_NAME,
    //             CLOGO = oLogo.CLOGO
    //         };
    //
    //         var loReportFormat = new R_ReportFormatDTO()
    //         {
    //             DecimalSeparator = loParameterReportClient.CNUMBER_FORMAT,
    //             GroupSeparator = loParameterReportClient.CNUMBER_FORMAT == "." ? "," : ".",
    //             DecimalPlaces = loParameterReportClient.IDECIMAL_PLACES,
    //             ShortDate = loParameterReportClient.CDATE_SHORT_FORMAT,
    //             ShortTime = loParameterReportClient.CTIME_SHORT_FORMAT,
    //         };
    //
    //         _connectionAttribute = loDb.GetConnectionAttribute();
    //         _logger!.LogInfo("Get Connection Attribute");
    //
    //         if (listDataBilling != null)
    //         {
    //             foreach (var item in listDataBilling)
    //             {
    //                 _logger.LogInfo("Set Param for VA");
    //                 var loVAParam = new PMR03000ParameterDb()
    //                 {
    //                     CCOMPANY_ID = item.CCOMPANY_ID,
    //                     CPROPERTY_ID = item.CPROPERTY_ID,
    //                     CTENANT_ID = item.CTENANT_ID,
    //                     CLANG_ID = R_BackGlobalVar.REPORT_CULTURE
    //                 };
    //
    //                 _logger.LogInfo("Get data VA");
    //                 var dataVirtualAccount = await GetVAList(loVAParam);
    //
    //                 _logger.LogInfo("Get data Unit");
    //                 var FilteredUnitList = listDataDetailUnit.Where(x => x.CREF_NO == item.CREF_NO).ToList();
    //
    //                 _logger.LogInfo("Get data Utility");
    //                 var filteredGroups = listDataDetailUtility
    //                     .Where(x => x.CREF_NO == item.CREF_NO)
    //                     .GroupBy(x => x.CCHARGES_TYPE)
    //                     .ToDictionary(g => g.Key, g => g.ToList());
    //
    //                 _logger.LogInfo("Seprated with different ChargeType");
    //                 var FilteredUtilityList01 =
    //                     filteredGroups.GetValueOrDefault("01", new List<PMR03000DetailUtilityDTO>());
    //                 var FilteredUtilityList02 =
    //                     filteredGroups.GetValueOrDefault("02", new List<PMR03000DetailUtilityDTO>());
    //                 var FilteredUtilityList03 =
    //                     filteredGroups.GetValueOrDefault("03", new List<PMR03000DetailUtilityDTO>());
    //                 var FilteredUtilityList04 =
    //                     filteredGroups.GetValueOrDefault("04", new List<PMR03000DetailUtilityDTO>());
    //
    //                 var resultData = new PMR03000ResultDataDTO()
    //                 {
    //                     Header = Header,
    //                     Label = (PMR03000ReportLabelDTO)loLabel,
    //                     Datas = item,
    //                     VirtualAccountData = dataVirtualAccount,
    //                     DataUnitList = FilteredUnitList,
    //                     DataUtility1 = FilteredUtilityList01,
    //                     DataUtility2 = FilteredUtilityList02,
    //                     DataUtility3 = FilteredUtilityList03,
    //                     DataUtility4 = FilteredUtilityList04,
    //                 };
    //
    //                 var loParameter = new R_GenerateReportParameter()
    //                 {
    //                     ReportRule = loReportRule,
    //                     ReportFileName = lcReportFileName,
    //                     ReportData = JsonSerializer.Serialize(resultData),
    //                     ReportDataSourceName = "ResponseDataModel",
    //                     ReportFormat = loReportFormat,
    //                     ReportDataType = typeof(PMR03000ResultDataDTO).ToString(),
    //                     ReportOutputType = leReportOutputType,
    //                     ReportAssemblyName = "PMR03000Common.dll",
    //                     ReportParameter = null
    //                 };
    //                 
    //                 _logger!.LogInfo("Success get Parameter");
    //                 var loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(
    //                     R_ReportServerClientService.R_GetHttpClient(),
    //                     "api/ReportServer/GetReport", loParameter);
    //
    //                 var fileExtension = leReportOutputType.ToString();
    //
    //                 _logger!.LogInfo("Declare param to save storage");
    //                 var ParamSaveStorage = new PMR03000ParamSaveStorageDTO()
    //                 {
    //                     CCOMPANY_ID = item.CCOMPANY_ID,
    //                     CUSER_ID = "SYSTEM",
    //                     CPROPERTY_ID = item.CPROPERTY_ID,
    //                     CTENANT_ID = item.CTENANT_ID,
    //                     CREF_NO = item.CREF_NO,
    //                     CLOI_AGRMT_REC_ID = item.CLOI_AGRMT_REC_ID,
    //                     CREF_PRD = poParam.CPERIOD,
    //                     CSTORAGE_ID = item.CSTORAGE_ID,
    //                     FileExtension = fileExtension,
    //                     REPORT = loRtnInByte
    //                 };
    //
    //                 //Start Transaction BLOCK
    //                 _logger!.LogInfo("Start Transaction Block");
    //                 using var scope = new TransactionScope(TransactionScopeOption.Required, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled);
    //                 try
    //                 {
    //                     _logger.LogInfo("Start transcope");
    //                     loConn = await loDb.GetConnectionAsync();
    //                     _logger.LogInfo("Get connection in transcope [Connectiin and COnecction attribute]");
    //
    //                     if (loRtnInByte != null)
    //                     {
    //                         var lcStorageId = await SaveReportToAzure(ParamSaveStorage, loConn, _connectionAttribute);
    //                         var ParamSaveBillingStatement = new PMR03000ParamSaveBillingStatement()
    //                         {
    //                             CCOMPANY_ID = item.CCOMPANY_ID,
    //                             CPROPERTY_ID = item.CPROPERTY_ID,
    //                             CTENANT_ID = item.CTENANT_ID,
    //                             CLOI_AGRMT_REC_ID = item.CLOI_AGRMT_REC_ID,
    //                             CREF_PRD = poParam.CPERIOD,
    //                             CREF_DATE = item.CSTATEMENT_DATE,
    //                             CDUE_DATE = item.CDUE_DATE,
    //                             CSTORAGE_ID = lcStorageId,
    //                             CUSER_ID = poParam.CUSER_ID,
    //                         };
    //
    //                         await SaveBillingStatementList(ParamSaveBillingStatement, loConn);
    //                         scope.Complete();
    //                     }
    //                 }
    //                 catch (Exception ex)
    //                 {
    //                     loEx.Add(ex);
    //                     _logger!.LogError(string.Format("Log Error {0} ", ex));
    //                 }
    //             }
    //         }
    //
    //         _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
    //         //_logger.Debug(string.Format("END process method {0} on Cls", lcMethodName));
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //         _logger!.LogError(string.Format("Log Error {0} ", ex));
    //     }
    //     finally
    //     {
    //         if (loConn != null)
    //         {
    //             if ((loConn.State == ConnectionState.Closed) == false)
    //             {
    //                 loConn.Close();
    //                 loConn.Dispose();
    //             }
    //
    //             loConn = null;
    //         }
    //
    //         if (loDb != null)
    //         {
    //             loDb = null;
    //         }
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //     _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
    //
    //     return loReturn!;
    // }

    public async Task<List<PMR03000DataReportDTO>> GetDataBilling(PMR03000ReportParamDTO poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDataBilling));
        const string lcMethodName = nameof(GetDataBilling);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new();
        List<PMR03000DataReportDTO> loReturn = null;
        DbCommand loCommand;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new R_Db();

            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            ;
            //var loConn2 = loDb.GetName();
            loCommand = loDb.GetCommand();
            var lcQuery = "RSP_PM_PMA00300";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20,
                poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20,
                poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPERIOD ", DbType.String, 8, poParameter.CPERIOD);
            loDb.R_AddCommandParameter(loCommand, "@CFROM_TENANT", DbType.String, 30,
                poParameter.CFROM_TENANT);
            loDb.R_AddCommandParameter(loCommand, "@CTO_TENANT", DbType.String, 30,
                poParameter.CTO_TENANT);
            loDb.R_AddCommandParameter(loCommand, "@CREPORT_TYPE", DbType.String, 1,
                poParameter.CREPORT_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CSERVICE_TYPE ", DbType.String, 2,
                poParameter.CSERVICE_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@LPRINT ", DbType.Boolean, 1, poParameter.LPRINT);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
            
            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CPERIOD" or
                        "@CFROM_TENANT" or
                        "@CTO_TENANT" or
                        "@CREPORT_TYPE" or
                        "@CSERVICE_TYPE" or
                        "@LPRINT" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            try
            {
                _logger.LogInfo(string.Format(
                    "Execute the SQL query and store the result in loDataTable in Method {0}",
                    lcMethodName));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);

                _logger.LogInfo(string.Format(
                    "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                    lcMethodName));
                loReturn = R_Utility.R_ConvertTo<PMR03000DataReportDTO>(loDataTable).ToList()!;
                _logger.LogDebug("{@ObjectReturn}", loReturn);
                if (loReturn.Count > 0)
                {
                    foreach (var item in loReturn)
                    {
                        item.DDUE_DATE = ConvertStringToDateTimeFormat(item.CDUE_DATE);
                        item.DSTATEMENT_DATE = ConvertStringToDateTimeFormat(item.CSTATEMENT_DATE);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        _logger.LogInfo(string.Format("End Method {0}", lcMethodName));
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public async Task<List<PMR03000DetailUnitDTO>> GeDataDetailUnitList(
        PMR03000ReportParamDTO poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeDataDetailUnitList));
        const string lcMethodName = nameof(GeDataDetailUnitList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        R_Exception loException = new();
        List<PMR03000DetailUnitDTO> loReturn = new();
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new();
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            ;
            loCommand = loDb.GetCommand();
            lcQuery = "RSP_PM_PMA00300";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20,
                poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20,
                poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPERIOD", DbType.String, 8, poParameter.CPERIOD);
            loDb.R_AddCommandParameter(loCommand, "@CFROM_TENANT", DbType.String, 30,
                poParameter.CFROM_TENANT);
            loDb.R_AddCommandParameter(loCommand, "@CTO_TENANT", DbType.String, 30,
                poParameter.CTO_TENANT);
            loDb.R_AddCommandParameter(loCommand, "@CREPORT_TYPE", DbType.String, 1,
                poParameter.CREPORT_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CSERVICE_TYPE ", DbType.String, 2,
                poParameter.CSERVICE_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@LPRINT ", DbType.Boolean, 1, poParameter.LPRINT);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
            
            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CPERIOD" or
                        "@CFROM_TENANT" or
                        "@CTO_TENANT" or
                        "@CREPORT_TYPE" or
                        "@CSERVICE_TYPE" or
                        "@LPRINT" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                _logger.LogInfo(string.Format(
                    "Execute the SQL query and store the result in loDataTable in Method {0}",
                    lcMethodName));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);

                _logger.LogInfo(string.Format(
                    "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                    lcMethodName));
                loReturn = R_Utility.R_ConvertTo<PMR03000DetailUnitDTO>(loDataTable).ToList()!;
                _logger.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        _logger.LogInfo(string.Format("End Method {0}", lcMethodName));
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public async Task<List<PMR03000DetailUtilityDTO>> GeDataDetailUtilityList(
        PMR03000ReportParamDTO poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeDataDetailUnitList));
        const string lcMethodName = nameof(GeDataDetailUnitList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        R_Exception loException = new();
        List<PMR03000DetailUtilityDTO>? loReturn = new();
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new();
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            ;
            loCommand = loDb.GetCommand();
            lcQuery = "RSP_PM_PMA00300";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20,
                poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20,
                poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPERIOD", DbType.String, 8, poParameter.CPERIOD);
            loDb.R_AddCommandParameter(loCommand, "@CFROM_TENANT", DbType.String, 30,
                poParameter.CFROM_TENANT);
            loDb.R_AddCommandParameter(loCommand, "@CTO_TENANT", DbType.String, 30,
                poParameter.CTO_TENANT);
            loDb.R_AddCommandParameter(loCommand, "@CREPORT_TYPE", DbType.String, 1,
                poParameter.CREPORT_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CSERVICE_TYPE ", DbType.String, 2,
                poParameter.CSERVICE_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@LPRINT ", DbType.Boolean, 1, poParameter.LPRINT);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
            
            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CPERIOD" or
                        "@CFROM_TENANT" or
                        "@CTO_TENANT" or
                        "@CREPORT_TYPE" or
                        "@CSERVICE_TYPE" or
                        "@LPRINT" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                _logger.LogInfo(string.Format(
                    "Execute the SQL query and store the result in loDataTable in Method {0}",
                    lcMethodName));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _logger.LogInfo(string.Format(
                    "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                    lcMethodName));
                loReturn = R_Utility.R_ConvertTo<PMR03000DetailUtilityDTO>(loDataTable).ToList()!;
                _logger.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        _logger.LogInfo(string.Format("End Method {0}", lcMethodName));
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    private static DateTime ConvertStringToDateTimeFormat(string? pcEntity)
    {
        if (string.IsNullOrWhiteSpace(pcEntity))
        {
            return DateTime.Now;
        }

        DateTime result;
        if (pcEntity.Length == 6)
        {
            pcEntity += "01";
        }

        return DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out result)
            ? result
            : DateTime.Now;
    }

    public object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo,
        object poObject)
    {
        var loObj = Activator.CreateInstance(poObject.GetType())!;
        var loGetPropertyObject = poObject.GetType().GetProperties();

        foreach (var property in loGetPropertyObject)
        {
            string propertyName = property.Name;
            string message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
            property.SetValue(loObj, message);
        }

        return loObj;
    }

    public async Task<PMR03000BaseHeaderDTO> GetLogoCompany(PMR03000ReportParamDTO poParameter)
    {
        var loEx = new R_Exception();
        string? lcMethodName = nameof(GetLogoCompany);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        PMR03000BaseHeaderDTO loResult = null;
        try
        {
            var loDb = new R_Db();
            var loConn = await loDb.GetConnectionAsync();
            var loCmd = loDb.GetCommand();
            var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15,
                poParameter.CCOMPANY_ID);

            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
            _logger!.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
            loResult = R_Utility.R_ConvertTo<PMR03000BaseHeaderDTO>(loDataTable).FirstOrDefault()!;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        loEx.ThrowExceptionIfErrors();
        return loResult!;
    }

    public async Task<List<PMR03000VADTO>> GetVAList(PMR03000ParameterDb poParameter)
    {
        string? lcMethodName = nameof(GetVAList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        R_Exception loException = new();
        List<PMR03000VADTO>? loReturn = new();
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new();
            loConn = await loDb.GetConnectionAsync();
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
            _logger!.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<PMR03000VADTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
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
        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        return loReturn;
    }

    private async Task<string> SaveReportToAzure(PMR03000ParamSaveStorageDTO poParameter, DbConnection poConn, R_ConnectionAttribute poConnAttribute)
    {
        var loEx = new R_Exception();

        var lcMethodName = nameof(SaveReportToAzure);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        string loResult = null;
        DbConnection loConn = null;
        R_SaveResult loSaveResult = null;
        R_ConnectionAttribute loConnAttribute;
        _logger!.LogInfo("Get Connection Attribute");
        try
        {
            var loDb = new R_Db();
            loConn = poConn;
            loConnAttribute = poConnAttribute;
            var loCommand = loDb.GetCommand();

            _logger.LogInfo("Get storage type");
            var loGetPmr03000StorageType = await GetStorageType(poParameter);

            R_EStorageType loStorageType = loGetPmr03000StorageType.CSTORAGE_TYPE != "1"
                ? R_EStorageType.OnPremise
                : R_EStorageType.Cloud;
            R_EProviderForCloudStorage loProvider = loGetPmr03000StorageType.CSTORAGE_PROVIDER_ID!.ToLower() != "azure"
                ? R_EProviderForCloudStorage.google
                : R_EProviderForCloudStorage.azure;


            // Add and create Storage ID
            if (string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
            {
                _logger.LogInfo("Add  storage id to save cause storage id not exist:");
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
                _logger.LogInfo("Call R_StorageUtility.AddFile to storage table");
                loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn);
            }
            else if (!string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
            {
                _logger.LogInfo("update with storage id to save cause storage id already exist:");
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
                _logger.LogInfo("Call R_StorageUtility.UpdateFile to storage table");
                loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttribute.Provider);
            }

            if (loSaveResult != null)
            {
                loResult = loSaveResult.StorageId;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }
        _logger!.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        loEx.ThrowExceptionIfErrors();
        return loResult!;
    }

    private async Task<PMR03000StorageType> GetStorageType(PMR03000ParamSaveStorageDTO poParameter)
    {
        string lcMethodName = nameof(GetStorageType);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        var loEx = new R_Exception();
        PMR03000StorageType loResult = null;
        var loDb = new R_Db();
        DbConnection loConn = null;
        DbCommand loCmd = null;

        try
        {
            _logger!.LogInfo("before exec RSP_GS_GET_STORAGE_TYPE");
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            ;
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
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMR03000StorageType>(loDataTable).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.LogError(string.Format("Log Error {0} ", ex));
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        return loResult!;
    }

    public async Task SaveBillingStatementList(PMR03000ParamSaveBillingStatement poParameter, DbConnection poConn)
    {
        string? lcMethodName = nameof(SaveBillingStatementList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new();
        string lcQuery;
        DbCommand loCommand;
        DbConnection loConn = null;
        R_Db loDb;
        try
        {
            loDb = new();
            loConn = poConn;
            ;
            loCommand = loDb.GetCommand();
            lcQuery = "RSP_PM_SAVE_BILLING_STATEMENT";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID ", DbType.String, 20, poParameter.CTENANT_ID);
            loDb.R_AddCommandParameter(loCommand, "@CLOI_AGRMT_REC_ID", DbType.String, 50,
                poParameter.CLOI_AGRMT_REC_ID);
            loDb.R_AddCommandParameter(loCommand, "@CREF_PRD", DbType.String, 6, poParameter.CREF_PRD);
            loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, poParameter.CREF_DATE);
            loDb.R_AddCommandParameter(loCommand, "@CDUE_DATE ", DbType.String, 8, poParameter.CDUE_DATE);
            loDb.R_AddCommandParameter(loCommand, "@CSTORAGE_ID", DbType.String, 40, poParameter.CSTORAGE_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _logger!.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
            var loDataTable = await loDb.SqlExecNonQueryAsync(loConn, loCommand, true);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        if (loException.Haserror)
        {
            loException.ThrowExceptionIfErrors();
        }

        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
    }


    public async Task<PMR03000ReportClientParameterDTO> GetClientReportFormat(string poParam)
    {
        string lcMethodName = nameof(GetClientReportFormat);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new();
        PMR03000ReportClientParameterDTO? loResult = null;
        R_Db loDb;
        DbCommand loCommand;
        try
        {
            loDb = new R_Db();

            var loConn = await loDb.GetConnectionAsync();
            loCommand = loDb.GetCommand();
            var lcQuery = $"select * from SAM_COMPANIES where CCOMPANY_ID ='{poParam}'";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.Text;
            _logger!.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
            var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
            loResult = R_Utility.R_ConvertTo<PMR03000ReportClientParameterDTO>(loReturnTemp).ToList().Any()
                ? R_Utility.R_ConvertTo<PMR03000ReportClientParameterDTO>(loReturnTemp).FirstOrDefault()!
                : new PMR03000ReportClientParameterDTO();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

        return loResult!;
    }
}