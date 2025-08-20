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
using R_CommonFrontBackAPI;
using R_Processor;
using R_ProcessorJob;
using R_ReportServerClient;
using R_ReportServerClient.DTO;
using R_ReportServerCommon;
using R_Storage;
using R_StorageCommon;

namespace PMR03000Back;

public class PMR03000PrintCls
{
    private LoggerPMR03000Print _logger;
    private readonly ActivitySource _activitySource;

    RSP_PM_PMA00300Resources.Resources_Dummy_Class _resourcesDummyClass = new();

    public PMR03000PrintCls()
    {
        _logger = LoggerPMR03000Print.R_GetInstanceLogger();
        _activitySource = PMR03000Activity.R_GetInstanceActivitySource();
    }

    public async Task<List<PMR03000DataReportDTO>> GetDataBilling(PMR03000ReportParamDTO poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDataBilling));
        const string lcMethodName = nameof(GetDataBilling);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new();
        List<PMR03000DataReportDTO> loReturn = null;
        DbCommand loCommand = null;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new R_Db();

            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);

            R_ExternalException.R_SP_Init_Exception(loConn);

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
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, false);

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

            loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }

            if (loCommand != null)
            {
                loCommand.Dispose();
                loCommand = null;
            }
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        _logger.LogInfo(string.Format("End Method {0}", lcMethodName));
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public async Task<List<PMR03000DetailUnitDTO>> GetDataDetailUnitList(
        PMR03000ReportParamDTO poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDataDetailUnitList));
        const string lcMethodName = nameof(GetDataDetailUnitList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        R_Exception loException = new();
        List<PMR03000DetailUnitDTO> loReturn = new();
        string lcQuery;
        DbCommand loCommand = null;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new();
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);

            R_ExternalException.R_SP_Init_Exception(loConn);

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
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, false);

                _logger.LogInfo(string.Format(
                    "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                    lcMethodName));
                loReturn = R_Utility.R_ConvertTo<PMR03000DetailUnitDTO>(loDataTable).ToList()!;
                _logger.LogDebug("{@ObjectReturn}", loReturn);

                if (loReturn.Count > 0)
                {
                    foreach (var item in loReturn)
                    {
                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }

            if (loCommand != null)
            {
                loCommand.Dispose();
                loCommand = null;
            }
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        _logger.LogInfo(string.Format("End Method {0}", lcMethodName));
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public async Task<List<PMR03000DetailUtilityDTO>> GetDataDetailUtilityList(
        PMR03000ReportParamDTO poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDataDetailUtilityList));
        const string lcMethodName = nameof(GetDataDetailUtilityList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        R_Exception loException = new();
        List<PMR03000DetailUtilityDTO>? loReturn = new();
        string lcQuery;
        DbCommand loCommand = null;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new();
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);

            R_ExternalException.R_SP_Init_Exception(loConn);

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
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);

                _logger.LogInfo(string.Format(
                    "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                    lcMethodName));
                loReturn = R_Utility.R_ConvertTo<PMR03000DetailUtilityDTO>(loDataTable).ToList()!;
                _logger.LogDebug("{@ObjectReturn}", loReturn);

                if (loReturn.Count > 0)
                {
                    foreach (var item in loReturn)
                    {
                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }

            if (loCommand != null)
            {
                loCommand.Dispose();
                loCommand = null;
            }
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        _logger.LogInfo(string.Format("End Method {0}", lcMethodName));
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }


    public async Task<List<PMR03000RateWGListDTO>> GetRateWGList(PMR03000ReportParamDTO poParam)
    {
        R_Exception loEx = new();
        List<PMR03000RateWGListDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_UTILITY_INFO_RATE_WG";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.RateParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.RateParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 20,
                poParam.RateParameter.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poParam.RateParameter.CCHARGES_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.RateParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 8, poParam.RateParameter.CSTART_DATE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CCHARGE_TYPE_ID" or
                        "@CCHARGES_ID" or
                        "@CUSER_ID" or
                        "@CCHARGES_DATE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMR03000RateWGListDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public static DateTime ConvertStringToDateTimeFormat(string? pcEntity)
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

    public async Task<PMR03000BaseHeaderDTO> GetLogoProperty(PMR03000ReportParamDTO poParameter)
    {
        var loEx = new R_Exception();
        string? lcMethodName = nameof(GetLogoProperty);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        PMR03000BaseHeaderDTO loResult = null;
        DbConnection loConn = null;
        DbCommand loCommand = null;
        try
        {
            var loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCommand = loDb.GetCommand();

            var lcQuery = "RSP_GS_GET_PROPERTY_DETAIL";
            loCommand = loDb.GetCommand();
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                .Select(x => x.Value);
            _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_DETAIL {@Parameters}", loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
            loResult = R_Utility.R_ConvertTo<PMR03000BaseHeaderDTO>(loDataTable).FirstOrDefault();

            if (string.IsNullOrEmpty(loResult.CSTORAGE_ID) == false)
            {
                var loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CSTORAGE_ID
                };

                var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                loResult.CLOGO = loReadResult.Data;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }

            if (loCommand != null)
            {
                loCommand.Dispose();
                loCommand = null;
            }
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
        DbCommand loCommand = null;
        R_Db loDb = null;
        DbConnection loConn = null;
        try
        {
            loDb = new();
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
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
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
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

            if (loCommand != null)
            {
                loCommand.Dispose();
                loCommand = null;
            }
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        return loReturn;
    }


    public async Task<PMR03000BillingStatementDTO> GetBillingStatementList(PMR03000ReportParamDTO poParameter,
        DbConnection poConn)
    {
        string lcMethodName = nameof(GetBillingStatementList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        var loEx = new R_Exception();
        PMR03000BillingStatementDTO loResult = null;
        var loDb = new R_Db();
        DbConnection loConn = null;
        DbCommand loCmd = null;

        try
        {
            _logger!.LogInfo("before exec RSP_PM_GET_BILLING_STATEMENT");
            loConn = poConn;

            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_PM_GET_BILLING_STATEMENT";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_TENANT_ID", DbType.String, 20, poParameter.CFROM_TENANT);
            loDb.R_AddCommandParameter(loCmd, "@CTO_TENANT_ID", DbType.String, 20, poParameter.CTO_TENANT);
            loDb.R_AddCommandParameter(loCmd, "@CREF_PRD", DbType.String, 6, poParameter.CPERIOD);

            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMR03000BillingStatementDTO>(loDataTable).FirstOrDefault();
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
        finally
        {
            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
        }


        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        return loResult!;
    }

    private async Task<PMR03000StorageType> GetStorageType(PMR03000ParamSaveStorageDTO poParameter, DbConnection poConn)
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
            loConn = poConn;
            loCmd = loDb.GetCommand();
            ;
            _logger!.LogInfo("before exec RSP_GS_GET_STORAGE_TYPE");

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
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
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
        finally
        {
            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        return loResult!;
    }

    public async Task<string> SaveReportToAzure(PMR03000ParamSaveStorageDTO poParameter, DbConnection poConn,
        R_ConnectionAttribute poConnAttribute)
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
            var loGetPmr03000StorageType = await GetStorageType(poParameter, loConn);

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
                    FileName = poParameter.CFILE_NAME,
                    FileExtension = poParameter.FileExtension,
                    UploadData = poParameter.REPORT,
                    UserId = poParameter.CUSER_ID,
                    BusinessKeyParameter = new R_BusinessKeyParameter()
                    {
                        CCOMPANY_ID = poParameter.CCOMPANY_ID,
                        CDATA_TYPE = "PMT_BILLING_STATEMENT",
                        CKEY01 = poParameter.CPROPERTY_ID,
                        CKEY02 = poParameter.CTENANT_ID,
                        CKEY03 = poParameter.CLOI_AGRMT_REC_ID,
                        CKEY04 = poParameter.CREF_PRD,
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
                        FileName = poParameter.CFILE_NAME
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

    public async Task SaveBillingStatementList(PMR03000ParamSaveBillingStatement poParameter, DbConnection poConn)
    {
        string? lcMethodName = nameof(SaveBillingStatementList);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new();
        string lcQuery;
        DbCommand loCommand = null;
        DbConnection loConn = null;
        R_Db loDb;
        try
        {
            loDb = new();
            loConn = poConn;

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
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            var loDataTable = await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }
        finally
        {
            if (loCommand != null)
            {
                loCommand.Dispose();
                loCommand = null;
            }
        }

        if (loException.Haserror)
        {
            loException.ThrowExceptionIfErrors();
        }

        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
    }

    public async Task<PMR03000ResultDataDTO> GenerateReport(PMR03000ReportParamDTO poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GenerateReport));

        var loEx = new R_Exception();
        var loReturn = new PMR03000ResultDataDTO();
        var loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);

        try
        {
            _logger.LogDebug("Generated parameters for data print", poParam);

            _logger.LogInfo("Get Summary Data Billing for Report");
            poParam.CREPORT_TYPE = "S";
            poParam.CSERVICE_TYPE = "";
            var listDataBilling = await this.GetDataBilling(poParameter: poParam);
            _logger.LogInfo("Data Billing retrieved successfully for Report");

            _logger.LogInfo("Get Data Detail Unit");
            poParam.CREPORT_TYPE = "D";
            poParam.CSERVICE_TYPE = "UN";
            var listDataDetailUnit = await this.GetDataDetailUnitList(poParameter: poParam);
            _logger.LogInfo("Data Detail Unit Retrieved successfully for Report");

            _logger.LogInfo("Get Data Detail Utility");
            poParam.CREPORT_TYPE = "D";
            poParam.CSERVICE_TYPE = "UT";
            var listDataDetailUtility = await this.GetDataDetailUtilityList(poParameter: poParam);
            _logger.LogInfo("Data Detail Utility Retrieved successfully for Report");

            _logger.LogInfo("Get Data Logo");
            var oLogo = await this.GetLogoProperty(poParam);
            _logger.LogInfo("Company Logo Retrieved successfully for Report");

            _logger.LogInfo("Get Label");
            var loLabel = AssignValuesWithMessages(typeof(PMR03000BackResources.Resources_Dummy_Class),
                loCultureInfo, new PMR03000ReportLabelDTO());

            _logger.LogInfo("Report data formatted for print");

            var loData = new PMR03000ResultDataDTO();

            var Header = new PMR03000BaseHeaderDTO()
            {
                PROPERTY_NAME = poParam.CPROPERTY_NAME,
                CLOGO = oLogo.CLOGO
            };

            var resultData = new PMR03000ResultDataDTO()
            {
                Header = Header,
                Label = (PMR03000ReportLabelDTO)loLabel,
                Datas = new List<PMR03000DataReportDTO>(),
            };

            if (listDataBilling != null)
            {
                // var itemIndexes = 0;
                foreach (var item in listDataBilling)
                {
                    _logger.LogInfo("Set Param for VA");
                    var loVAParam = new PMR03000ParameterDb()
                    {
                        CCOMPANY_ID = item.CCOMPANY_ID,
                        CPROPERTY_ID = item.CPROPERTY_ID,
                        CTENANT_ID = item.CTENANT_ID,
                        CLANG_ID = R_BackGlobalVar.REPORT_CULTURE
                    };

                    _logger.LogInfo("Get data VA");
                    var dataVirtualAccount = await GetVAList(loVAParam);

                    _logger.LogInfo("Get data Unit");
                    var FilteredUnitList = (listDataDetailUnit.Any())
                        ? listDataDetailUnit.Where(x => x.CCUSTOMER_ID == item.CTENANT_ID).ToList()
                        : new List<PMR03000DetailUnitDTO>();

                    _logger.LogInfo("Get data Utility");
                    var filteredGroups = listDataDetailUtility
                        .Where(x => x.CCUSTOMER_ID == item.CTENANT_ID)
                        .GroupBy(x => x.CCHARGES_TYPE);

                    _logger.LogInfo("Seprated with different ChargeType");
                    var FilteredUtilityList01 = filteredGroups
                        .Where(g => g.Key == "01")
                        .SelectMany(g => g)
                        .ToList();
                    var FilteredUtilityList02 = filteredGroups
                        .Where(g => g.Key == "02")
                        .SelectMany(g => g)
                        .ToList();
                    var FilteredUtilityList03 = filteredGroups
                        .Where(g => g.Key == "03")
                        .SelectMany(g => g)
                        .ToList();
                    var FilteredUtilityList04 = filteredGroups
                        .Where(g => g.Key == "04")
                        .SelectMany(g => g)
                        .ToList();

                    if (FilteredUtilityList03.Any())
                    {
                        foreach (var utility in FilteredUtilityList03)
                        {
                            var utilityRate = new PMR03000ReportParamDTO()
                            {
                                RateParameter = new PMR03000ReportParamRateDb()
                                {
                                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                                    CPROPERTY_ID = utility.CPROPERTY_ID,
                                    CCHARGES_TYPE = utility.CCHARGES_TYPE,
                                    CCHARGES_ID = utility.CCHARGES_ID,
                                    CUSER_ID = R_BackGlobalVar.USER_ID,
                                    CSTART_DATE = utility.CSTART_DATE
                                }
                            };
                            utility.RateWGList = await GetRateWGList(utilityRate);
                        }
                    }

                    if (FilteredUtilityList04.Any())
                    {
                        foreach (var utility in FilteredUtilityList04)
                        {
                            var utilityRate = new PMR03000ReportParamDTO()
                            {
                                RateParameter = new PMR03000ReportParamRateDb()
                                {
                                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                                    CPROPERTY_ID = utility.CPROPERTY_ID,
                                    CCHARGES_TYPE = utility.CCHARGES_TYPE,
                                    CCHARGES_ID = utility.CCHARGES_ID,
                                    CUSER_ID = R_BackGlobalVar.USER_ID,
                                    CSTART_DATE = utility.CSTART_DATE
                                }
                            };
                            utility.RateWGList = await GetRateWGList(utilityRate);
                        }
                    }

                    resultData.Datas.Add(new PMR03000DataReportDTO
                    {
                        CCOMPANY_ID = item.CCOMPANY_ID,
                        CPROPERTY_ID = item.CPROPERTY_ID,
                        CPROPERTY_NAME = item.CPROPERTY_NAME,
                        CSTATEMENT_DATE = item.CSTATEMENT_DATE,
                        DSTATEMENT_DATE = item.DSTATEMENT_DATE,
                        CDUE_DATE = item.CDUE_DATE,
                        DDUE_DATE = item.DDUE_DATE,
                        CLOI_AGRMT_REC_ID = item.CLOI_AGRMT_REC_ID,
                        CSTORAGE_ID = item.CSTORAGE_ID,
                        CUSER_ID = item.CUSER_ID,
                        CTENANT_ID = item.CTENANT_ID,
                        CTENANT_NAME = item.CTENANT_NAME,
                        CBILLING_ADDRESS = item.CBILLING_ADDRESS,
                        CREF_NO = item.CREF_NO,
                        CBUILDING_ID = item.CBUILDING_ID,
                        CBUILDING_NAME = item.CBUILDING_NAME,
                        CUNIT_ID_LIST = item.CUNIT_ID_LIST,
                        CUNIT_DESCRIPTION = item.CUNIT_DESCRIPTION,
                        CCURRENCY = item.CCURRENCY,
                        CCURRENCY_CODE = item.CCURRENCY_CODE,
                        NPREVIOUS_BALANCE = item.NPREVIOUS_BALANCE,
                        NPREVIOUS_PAYMENT = item.NPREVIOUS_PAYMENT,
                        NCURRENT_PENALTY = item.NCURRENT_PENALTY,
                        NNEW_BILLING = item.NNEW_BILLING,
                        NNEW_BALANCE = item.NNEW_BALANCE,
                        NSALES = item.NSALES,
                        NRENT = item.NRENT,
                        NDEPOSIT = item.NDEPOSIT,
                        NREVENUE_SHARING = item.NREVENUE_SHARING,
                        NSERVICE_CHARGE = item.NSERVICE_CHARGE,
                        NSINKING_FUND = item.NSINKING_FUND,
                        NPROMO_LEVY = item.NPROMO_LEVY,
                        NGENERAL_CHARGE = item.NGENERAL_CHARGE,
                        NELECTRICITY = item.NELECTRICITY,
                        NCHILLER = item.NCHILLER,
                        NWATER = item.NWATER,
                        NGAS = item.NGAS,
                        NPARKING = item.NPARKING,
                        NOVERTIME = item.NOVERTIME,
                        NGENERAL_UTILITY = item.NGENERAL_UTILITY,
                        TMESSAGE_DESCR_RTF = poParam.TMESSAGE_DESCR_RTF,
                        TADDITIONAL_DESCR_RTF = poParam.TADDITIONAL_DESCR_RTF,

                        VirtualAccountData = dataVirtualAccount,
                        DataUnitList = FilteredUnitList,
                        DataUnitListIsEmpty = !FilteredUnitList.Any(),
                        DataUtility1 = FilteredUtilityList01,
                        DataUtility1IsEmpty = !FilteredUtilityList01.Any(),
                        DataUtility2 = FilteredUtilityList02,
                        DataUtility2IsEmpty = !FilteredUtilityList02.Any(),
                        DataUtility3 = FilteredUtilityList03,
                        DataUtility3IsEmpty = !FilteredUtilityList03.Any(),
                        DataUtility4 = FilteredUtilityList04,
                        DataUtility4IsEmpty = !FilteredUtilityList04.Any(),
                    });

                    // itemIndexes++;
                    // loDataPrint.Data.Add(resultData);
                }
            }

            loReturn = resultData;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger!.LogError(string.Format("Log Error {0} ", ex));
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
}

public class PMR03000DistributeReportCls : R_IBatchProcessAsync, R_IProcessJobAsync<PMR03000DataReportDTO, R_Exception>
{
    private LoggerPMR03000Print _logger;
    RSP_GS_GET_STORAGE_TYPEResources.Resources_Dummy_Class _RSPStorage = new();
    private readonly ActivitySource _activitySource;

    private readonly R_ConnectionAttribute _connectionAttribute;

    private R_UploadAndProcessKey? _oKey;
    private int _nMaxData;
    private int _nCountProcess;
    private int _nCountError;

    private string _cTenantId = "";
    private string _cReportCulture = "";

    private int _CountReportMade = 0;
    private int _DecimalPlaces;
    private string? _DecimalSeparator = "";
    private string? _GroupSeparator = "";
    private string? _ShortDate = "";
    private string? _ShortTime = "";
    private string? _LongDate = "";
    private string? _LongTime = "";

    private R_UploadAndProcessKey? _olock = new R_UploadAndProcessKey(); // For locking multithread
    private R_ProcessorClass<PMR03000DataReportDTO, R_Exception>? _oProcessor;

    private PMR03000ReportParamDTO _poParamReport = new();

    private PMR03000ResultDataDTO _oResultData = new PMR03000ResultDataDTO();

    public PMR03000DistributeReportCls()
    {
        R_Db? loDb = new R_Db();
        _logger = LoggerPMR03000Print.R_GetInstanceLogger();
        _activitySource = PMR03000Activity.R_GetInstanceActivitySource();

        _connectionAttribute = loDb.GetConnectionAttribute(R_Db.eDbConnectionStringType.ReportConnectionString);

        _cTenantId = R_BackGlobalVar.TENANT_ID;
        _cReportCulture = R_BackGlobalVar.REPORT_CULTURE;

        _DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
        _DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
        _GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
        _ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
        _ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        _LongDate = R_BackGlobalVar.REPORT_FORMAT_LONG_DATE;
        _LongTime = R_BackGlobalVar.REPORT_FORMAT_LONG_TIME;

        loDb = null;
        _logger.LogInfo("koneksi string di Constructor ={0}", _connectionAttribute.ConnectionString);
    }

    public async Task R_BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
    {
        using Activity activity = _activitySource.StartActivity("R_BatchProcessAsync");
        R_Exception loException = new R_Exception();
        var loDb = new R_Db();

        try
        {
            _logger.LogInfo("Test Connection");
            if (loDb.R_TestConnection() == false)
            {
                loException.Add("01", "Database Connection Failed");
                goto EndBlock;
            }

            _logger.LogInfo("Start Batch");
            _R_BatchProcessAsync(poBatchProcessPar);
            _logger.LogInfo("End Batch");
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        finally
        {
            if (loDb != null)
            {
                loDb = null;
            }
        }

        EndBlock:

        loException.ThrowExceptionIfErrors();
    }

    private async Task _R_BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
    {
        _logger.LogInfo("start R_BatchProcessAsync ");
        string lcMethodName = nameof(R_BatchProcessAsync);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        R_Exception loException = new R_Exception();

        _logger.LogInfo("before R_Db() R_BatchProcessAsync ");
        R_Db loDb = new R_Db();
        _logger.LogInfo("after R_Db() R_BatchProcessAsync ");
        DbConnection? loConnection = null;
        string lcGuidId;
        PMR03000ReportParamDTO loParamBigObject = new();
        R_ReportServerClientConfiguration loReportConfiguration;

        using TransactionScope scope = new(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            if (loDb.R_TestConnection() == false)
            {
                loException.Add("", "Error where Connection to database");
                _logger.LogError(loException);
                goto EndBlock;
            }

            loParamBigObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<PMR03000ReportParamDTO>(
                    poBatchProcessPar.BigObject);
            _logger.LogInfo("get param", lcMethodName);

            #region GetParameter

            _oKey = poBatchProcessPar.Key;
            var loCls = new PMR03000PrintCls();

            _poParamReport = loParamBigObject;
            _poParamReport.CCOMPANY_ID = _oKey.COMPANY_ID;
            _poParamReport.CUSER_ID = _oKey.USER_ID;
            _poParamReport.LPRINT = true;

            var loResultFromDB = await loCls.GenerateReport(_poParamReport);
            _oResultData = loResultFromDB;

            //_nMaxData = loTempListForProcess.DataReport.Count; // Setel nilai _nMaxData berdasarkan jumlah total data
            _nCountProcess = 0; // Inisialisasi _nCountProcess
            loReportConfiguration = R_ReportServerClientService.R_GetReportServerConfiguration();
            _oProcessor = new R_ProcessorClass<PMR03000DataReportDTO, R_Exception>(this);
            _logger.LogInfo("before call method R_ProcessAsync", lcMethodName);
            await _oProcessor.R_ProcessAsync("key1");

            #endregion

            scope.Complete();
        }
        catch (Exception ex)
        {
            loConnection = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loException.Add(ex);
            _logger.LogError(loException);
            await HandleFinalStatus(loConnection, loDb, 9, loException);
        }
        finally
        {
            if (loConnection != null)
            {
                if (loConnection.State != ConnectionState.Closed)
                    loConnection.Close();
                loConnection.Dispose();
                loConnection = null;
            }
        }


        EndBlock:
        _logger.LogInfo("end R_BatchProcessAsync ");
        loException.ThrowExceptionIfErrors();
    }

    public async Task R_SingleProcessAsync(string poKey, PMR03000DataReportDTO poParameter)
    {
        R_Exception loException = new R_Exception();
        string lcMethodName = nameof(R_SingleProcessAsync);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        _logger.LogInfo("Get data from Report from DB");

        R_Db? loDb = null;
        DbConnection? loConn = null;
        R_GenerateReportParameter loParameter;
        R_ReportServerRule loReportRule;
        R_FileType leReportOutputType;

        int lnCountProcess;
        string lcCmd;
        string lcReportFileName = "";
        try
        {
            lock (_olock)
            {
                lnCountProcess = _nCountProcess;
                _nCountProcess += 1;
            }

            // Initialize database connection
            loDb = new R_Db();
            _logger.LogInfo("Connecting to database: {0}", _connectionAttribute.ConnectionString);
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            _logger.LogInfo("Connection established: {0}", loConn.ConnectionString);

            lcCmd =
                $"exec RSP_WriteUploadProcessStatus '{_oKey.COMPANY_ID.Trim()}','{_oKey.USER_ID.Trim()}','{_oKey.KEY_GUID.Trim()}',{_GetPropCount(lnCountProcess)},'Processing CREF_NO {poParameter.CREF_NO}'";
            await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);
            loReportRule = new R_ReportServerRule(_cTenantId.ToLower(), _oKey.COMPANY_ID.ToLower());
            _logger.LogInfo("Assigning report rule", loReportRule);

            _logger.LogInfo("Starting Get Report Server Data");
            var loGetReportByte = await GetReportData(poParameter);
            _logger.LogInfo("End Get Report Server Data");

            leReportOutputType = R_ReportServerCommon.R_FileType.PDF;
            var lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;
            var loCls = new PMR03000PrintCls();
            PMR03000ParamSaveStorageDTO loParamStorage = new PMR03000ParamSaveStorageDTO
            {
                CCOMPANY_ID = _oKey.COMPANY_ID,
                CUSER_ID = _oKey.USER_ID,
                CFILE_NAME = _poParamReport.CFILE_NAME,
                CLANG_ID = _cReportCulture,
                CTENANT_ID = poParameter.CTENANT_ID,
                CPROPERTY_ID = _poParamReport.CPROPERTY_ID,
                CLOI_AGRMT_REC_ID = poParameter.CLOI_AGRMT_REC_ID,
                FileExtension = lcExtension,
                CREF_PRD = _poParamReport.CPERIOD,
                REPORT = loGetReportByte
            };
            _logger.LogInfo("Starting Save and Get Storage ID");
            string lcStorageId = await loCls.SaveReportToAzure(loParamStorage, loConn, _connectionAttribute);
            _logger.LogInfo("End Save and Get Storage ID");

            _logger.LogInfo("Report data saved to Billing Statement");
            var loSaveBillingParam = new PMR03000ParamSaveBillingStatement()
            {
                CCOMPANY_ID = _oKey.COMPANY_ID,
                CPROPERTY_ID = _poParamReport.CPROPERTY_ID,
                CTENANT_ID = poParameter.CTENANT_ID,
                CLOI_AGRMT_REC_ID = poParameter.CLOI_AGRMT_REC_ID,
                CREF_PRD = _poParamReport.CPERIOD,
                CREF_DATE = poParameter.CSTATEMENT_DATE,
                CDUE_DATE = poParameter.CDUE_DATE,
                CSTORAGE_ID = lcStorageId,
                CUSER_ID = _oKey.USER_ID,
            };
            await loCls.SaveBillingStatementList(loSaveBillingParam, loConn);

            var loBillingStatement = await loCls.GetBillingStatementList(_poParamReport, loConn);

            // Transaction block to save 
            _logger.LogInfo("Starting transaction");

            List<PMR03000DistributeReportDataDTO> _DataSendEmailReport = new();
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromSeconds(30)))
            {
                try
                {
                    _CountReportMade++;
                    var loParamReportData = new PMR03000DistributeReportDataDTO
                    {
                        CSTORAGE_ID = lcStorageId,
                        CFILE_NAME = _poParamReport.CFILE_NAME,
                        CFILE_ID = poParameter.CTENANT_ID,
                        OFILE_DATA_REPORT = loGetReportByte,
                        LDATA_READY = true
                    };

                    lock (_DataSendEmailReport)
                    {
                        _DataSendEmailReport.Add(loParamReportData);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
            }

            var emailFiles = _DataSendEmailReport.Select(reportData => new R_EmailEngineBackObject
            {
                FileName = reportData.CFILE_NAME,
                FileExtension = ".pdf",
                FileId = reportData.CFILE_ID,
                FileData = reportData.OFILE_DATA_REPORT
            }).ToList();

            await SendEmail(emailFiles, loBillingStatement, loConn);
            EndBlock:
            _logger.LogInfo("End of R_SingleProcessAsync");
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(loException);
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
    }

    private async Task<byte[]> GetReportData(PMR03000DataReportDTO poParameter)
    {
        byte[] loResult = null;
        R_Exception loException = new R_Exception();
        string lcMethodName = nameof(GetReportData);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        _logger.LogInfo("Get Report Server");
        R_ReportServerRule loReportRule;
        R_FileType leReportOutputType;
        string lcReportFileName = "";
        string lcStorageId = "";
        string lcExtension;
        try
        {
            //Set Parameter Report Server
            lcReportFileName = _poParamReport.CFILE_NAME.EndsWith(".frx")
                ? _poParamReport.CFILE_NAME
                : $"{_poParamReport.CFILE_NAME}.frx";
            _logger.LogDebug("Selected report template file: {0}", lcReportFileName);

            leReportOutputType = R_ReportServerCommon.R_FileType.PDF;
            lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;
            loReportRule =
                new R_ReportServerRule(R_BackGlobalVar.TENANT_ID.ToLower(), R_BackGlobalVar.COMPANY_ID.ToLower());
            _logger.LogDebug("Report Server Rule initialized", loReportRule);

            var loData = new PMR03000ResultDataDTO()
            {
                Header = _oResultData.Header,
                Label = _oResultData.Label,
                Datas = new List<PMR03000DataReportDTO>()
            };
            loData.Datas.Add(poParameter);

            var loParamGetPrintReport = new R_GenerateReportParameter()
            {
                ReportRule = loReportRule,
                ReportFileName = lcReportFileName,
                ReportData = JsonSerializer.Serialize(loData),
                ReportDataSourceName = "ResponseDataModel",
                ReportFormat = new R_ReportFormatDTO()
                {
                    DecimalSeparator = _DecimalSeparator,
                    DecimalPlaces = _DecimalPlaces,
                    GroupSeparator = _GroupSeparator,
                    ShortDate = _ShortDate,
                    ShortTime = _ShortTime,
                    LongDate = _LongDate,
                    LongTime = _LongTime
                },
                ReportDataType = typeof(PMR03000ResultDataDTO).ToString(),
                ReportOutputType = leReportOutputType,
                ReportAssemblyName = "PMR03000Common.dll",
                ReportParameter = null
            };

            _logger.LogInfo("Report parameters prepared successfully");

            // Generate Report as Byte Array
            _logger.LogInfo("Starting report generation as byte array...");
            loResult = await R_ReportServerUtility.R_GenerateReportByte(
                R_ReportServerClientService.R_GetHttpClient(),
                "api/ReportServer/GetReport",
                loParamGetPrintReport
            );
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        return loResult;
    }

    public async Task<List<PMR03000DataReportDTO>> R_InitDataAsync(string poKey)
    {
        string lcMethodName = nameof(R_InitDataAsync);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        _logger!.LogInfo(string.Format("START method {0}", lcMethodName));
        var loDb = new R_Db();
        var loException = new R_Exception();
        List<PMR03000DataReportDTO>? loReturn = null;

        try
        {
            _logger.LogInfo("Assign Data for Report");
            loReturn = _oResultData.Datas;

            _logger.LogInfo($"End {lcMethodName}");
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger!.LogInfo(string.Format("END method {0}", lcMethodName));
        return loReturn!;
    }

    public async Task R_InitDataErrorStatusAsync(string poKey, R_Exception poException)
    {
        R_Exception loException = new R_Exception();
        string lcMethodName = nameof(R_InitDataErrorStatusAsync);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        DbConnection? loConn = null;
        R_Db loDb = new R_Db();
        try
        {
            _logger.LogInfo("start R_InitDataErrorStatus ", lcMethodName);
            //string? lcCmd;
            string lcErrMsg;
            int lnCountError;

            lock (_olock)
                lnCountError = _nCountError;
            lcErrMsg = $"ERROR run method {lcMethodName}";

            if (poException != null)
            {
                if (poException.ErrorList.Count > 0)
                {
                    lcErrMsg = string.Format("{0} with message {1}", lcErrMsg,
                        poException.ErrorList.FirstOrDefault()!.ErrDescp);
                    lcErrMsg = lcErrMsg.Replace("'", "").Replace(((char)34).ToString(), "");
                }
            }

            _logger.LogInfo("koneksi di R_InitDataErrorStatus {0}", _connectionAttribute.ConnectionString);
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            _logger.LogInfo("koneksi loConn di R_InitDataErrorStatus {0}", loConn.ConnectionString);

            var lcQuery =
                "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                string.Format("('{0}', '{1}', ", _oKey.COMPANY_ID, _oKey.USER_ID) +
                string.Format("'{0}', -1, '{1}')", _oKey.KEY_GUID, loException.ErrorList[0].ErrDescp);

            _logger.LogInfo("koneksi loConn di R_InitDataErrorStatus {0}", loConn.ConnectionString);
            await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

            lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", _oKey.COMPANY_ID) +
                      string.Format("'{0}', ", _oKey.USER_ID) +
                      string.Format("'{0}', ", _oKey.KEY_GUID) +
                      string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

            await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(loException);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != System.Data.ConnectionState.Closed)
                    loConn.Close();

                loConn.Dispose();
            }
        }

        _logger.LogInfo("end R_InitDataErrorStatus ");

        loException.ThrowExceptionIfErrors();
    }

    public async Task R_StatusAsync(string poKey, string pcMessage)
    {
        _logger.LogInfo("start R_Status ");
        string lcMethodName = nameof(R_StatusAsync);
        R_Exception loException = new R_Exception();
        DbConnection? loConn = null;
        R_Db loDb = new R_Db();
        string lcCmd;
        int lnCountProcess;
        try
        {
            _logger.LogInfo("koneksi di R_Status {0}", _connectionAttribute.ConnectionString);
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            _logger.LogInfo("koneksi loConn di R_Status {0}", loConn.ConnectionString);

            _logger.LogInfo("before exec RSP_WriteUploadProcessStatus", lcMethodName);

            lock (_olock)
                lnCountProcess = _nCountProcess;

            lcCmd = string.Format("exec RSP_WriteUploadProcessStatus '{0}','{1}','{2}',{3},'{4}'",
                _oKey.COMPANY_ID.Trim(), _oKey.USER_ID.Trim(), _oKey.KEY_GUID.Trim(), _GetPropCount(lnCountProcess),
                pcMessage.Trim());
            _logger.LogInfo("koneksi sebelum SqlExecNonQuery di R_Status {0}", loConn.ConnectionString);
            await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);
            _logger.LogInfo("after exec RSP_WriteUploadProcessStatus", lcMethodName);
            _logger.LogInfo("end R_Status ");
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(loException);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != System.Data.ConnectionState.Closed)
                    loConn.Close();

                loConn.Dispose();
            }
        }

        loException.ThrowExceptionIfErrors();
    }

    public async Task R_SingleSuccessStatusAsync(string poKey, PMR03000DataReportDTO poParameter)
    {
        _logger.LogInfo("start R_SingleSuccessStatus ");
        R_Exception loException = new R_Exception();
        string lcMethodName = nameof(R_SingleSuccessStatusAsync);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        R_Db loDb = new R_Db();
        DbConnection? loConn = null;
        string lcCmd;
        int lnCountProcess;
        try
        {
            _logger.LogInfo("koneksi di R_SingleSuccessStatus {0}", _connectionAttribute.ConnectionString);
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            _logger.LogInfo("koneksi loConn di R_SingleSuccessStatus {0}", loConn.ConnectionString);

            lock (_olock)
                lnCountProcess = _nCountProcess;
            _logger.LogInfo("before exec RSP_WriteUploadProcessStatus", lcMethodName);

            lcCmd = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}','{1}','{2}',{3},'{4}'",
                _oKey.COMPANY_ID.Trim(), _oKey.USER_ID.Trim(), _oKey.KEY_GUID.Trim(), _GetPropCount(lnCountProcess),
                string.Format("Process data CREF {0}", poParameter));

            _logger.LogInfo("koneksi loConn di R_SingleSuccessStatus {0}", loConn.ConnectionString);
            await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);
            _logger.LogInfo("after exec RSP_WriteUploadProcessStatus", lcMethodName);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(loException);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != System.Data.ConnectionState.Closed)
                    loConn.Close();

                loConn.Dispose();
            }
        }

        _logger.LogInfo("end R_SingleSuccessStatus ");

        loException.ThrowExceptionIfErrors();
    }

    public async Task R_SingleErrorStatusAsync(string poKey, PMR03000DataReportDTO poParameter, R_Exception poException)
    {
        R_Exception loException = new R_Exception();
        string lcMethodName = nameof(R_SingleErrorStatusAsync);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        DbCommand loCommand;
        DbConnection? loConn = null;
        //string? lcCmd;
        R_Db loDb = new R_Db();
        int lnCountError;
        string lcError = "";
        try
        {
            _logger.LogInfo("koneksi di R_SingleErrorStatus {0}", _connectionAttribute.ConnectionString);
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            _logger.LogInfo("koneksi loConn di R_SingleErrorStatus {0}", loConn.ConnectionString);
            _logger.LogInfo("before INSERT INTO GST_UPLOAD_ERROR_STATUS", lcMethodName);

            lock (_olock)
            {
                lnCountError = _nCountError;
                _nCountError += 1;
            }

            if (poException.Haserror)
            {
                lcError = poException.ErrorList.FirstOrDefault().ErrDescp;
                lcError.Replace("\"", " ").Replace("'", " ");
            }

            loCommand = loDb.GetCommand();
            var lcQuery =
                $"INSERT INTO  GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID, CUSER_ID, CKEY_GUID, ISEQ_NO, CERROR_MESSAGE) " +
                $"VALUES ( @COMPANY_ID, @USER_ID, @KEY_GUID, @CountError, @lcErrMsg ); ";

            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.Text;

            loDb.R_AddCommandParameter(loCommand, "@COMPANY_ID", DbType.String, 10, _oKey.COMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@USER_ID", DbType.String, 20, _oKey.USER_ID);
            loDb.R_AddCommandParameter(loCommand, "@KEY_GUID", DbType.String, 256, _oKey.KEY_GUID);
            loDb.R_AddCommandParameter(loCommand, "@CountError", DbType.Int32, 256, lnCountError);
            loDb.R_AddCommandParameter(loCommand, "@lcErrMsg", DbType.String, 256, lcError);

            _logger.LogInfo("koneksi loConn di R_SingleErrorStatus {0}", loConn.ConnectionString);
            var loReturnTempVal = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
            _logger.LogInfo("after INSERT INTO GST_UPLOAD_ERROR_STATUS", lcMethodName);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(loException);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != System.Data.ConnectionState.Closed)
                    loConn.Close();

                loConn.Dispose();
            }
        }

        _logger.LogInfo("end R_SingleErrorStatus ");

        loException.ThrowExceptionIfErrors();
    }

    public async Task R_ProcessCompleteStatusAsync(string poKey, List<R_Exception> poExceptions)
    {
        _logger.LogInfo("Start R_ProcessCompleteStatus");
        R_Exception loException = new();
        string lcMethodName = nameof(R_ProcessCompleteStatusAsync);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        DbConnection? loConn = null;
        R_Db? loDb = new();
        int lnFlag = poExceptions.Count > 0 ? 9 : 1;

        try
        {
            loConn = await loDb.GetConnectionAsync(R_Db.eDbConnectionStringType.ReportConnectionString);
            _logger.LogInfo("Database connection: {0}", loConn.ConnectionString);

            await HandleFinalStatus(loConn, loDb, lnFlag, loException);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger!.LogError(loException);
        }
        finally
        {
            loConn?.Close();
            loDb = null;
        }

        _logger.LogInfo("End R_ProcessCompleteStatus");
        loException.ThrowExceptionIfErrors();
    }

    #region Utilities

    private int _GetPropCount(int pnCount)
    {
        if (_nMaxData == 0)
            return 0;
        return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(100) * pnCount / _nMaxData));
    }

    #endregion

    private async Task HandleFinalStatus(DbConnection loConn, R_Db loDb, int lnFlag, R_Exception loException)
    {
        if (lnFlag == 9 && loException.Haserror)
        {
            string lcErrMsg = $"ERROR run method {nameof(R_ProcessCompleteStatusAsync)}";
            if (loException.ErrorList.Count > 0)
            {
                lcErrMsg = $"{lcErrMsg} with message {loException.ErrorList.First().ErrDescp}".Replace("'", "")
                    .Replace("\"", "");
            }

            string lcQuery =
                $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID, CUSER_ID, CKEY_GUID, ISEQ_NO, CERROR_MESSAGE) " +
                $"VALUES('{_oKey.COMPANY_ID}', '{_oKey.USER_ID}', '{_oKey.KEY_GUID}', -1, '{lcErrMsg}')";
            await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);
        }

        string lcCmd =
            $"EXEC RSP_WriteUploadProcessStatus '{_oKey.COMPANY_ID}', '{_oKey.USER_ID}', '{_oKey.KEY_GUID}', {_GetPropCount(_nMaxData)}, 'Finish Process', {lnFlag}";
        await loDb.SqlExecNonQueryAsync(lcCmd, loConn,false);
    }

    public async Task<string> SendEmail(List<R_EmailEngineBackObject> poDataAttachment,
        PMR03000BillingStatementDTO poSendToData, DbConnection poConnection)
    {
        var lcMethodName = nameof(SendEmail);
        using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        var loException = new R_Exception();
        R_EmailEngineBackCommandPar loEmailPar;
        List<R_EmailEngineBackObject> loEmailFiles;
        DbConnection? loConn = null;
        var lcRtn = "";
        try
        {
            var loEmailTemplate = await GetEmailTemplate(poConnection);
            loEmailFiles = poDataAttachment;

            poSendToData.DDUE_DATE = PMR03000PrintCls.ConvertStringToDateTimeFormat(poSendToData.CDUE_DATE);
            poSendToData.CDUE_DATE_DISPLAY = poSendToData.DDUE_DATE.ToString(_LongDate);
            poSendToData.CTOTAL_AMT_DISPLAY = poSendToData.NTOTAL_AMT.ToString(_DecimalSeparator);
            
            loEmailPar = new R_EmailEngineBackCommandPar()
            {
                COMPANY_ID = _oKey.COMPANY_ID,
                USER_ID = _oKey.USER_ID,
                PROGRAM_ID = "PMR03000",
                Message = new R_EmailEngineMessage()
                {
                    EMAIL_FROM = loEmailTemplate.CGENERAL_EMAIL_ADDRESS!,
                    EMAIL_BODY = string.Format(loEmailTemplate.CTEMPLATE_BODY, poSendToData.CTENANT_NAME, poSendToData.CUNIT_NAME,
                        poSendToData.CPERIOD, poSendToData.CCURRENCY_CODE, poSendToData.CTOTAL_AMT_DISPLAY,
                        poSendToData.CDUE_DATE_DISPLAY),
                    EMAIL_SUBJECT = string.Format(R_Utility.R_GetMessage(typeof(PMR03000BackResources.Resources_Dummy_Class), "EmailSubject", new CultureInfo(_cReportCulture)), poSendToData.CPROPERTY_NAME, poSendToData.CTENANT_ID, poSendToData.CTENANT_NAME),
                    EMAIL_TO = poSendToData.CBILLING_EMAIL!,
                    EMAIL_CC = "", //"ericsonwen123@gmail.com",//"hafizmursiddd@gmail.com", // hafiz.codeid@realta.net
                    EMAIL_BCC = "",
                    FLAG_HTML = true
                }
            };
            _logger.LogInfo("Get data to send to email engine");
            _logger.LogDebug("Data loEmailPar to send to email engine: {@Parameter}", loEmailPar);
            loConn = poConnection;
            loEmailPar.Attachments = loEmailFiles;
            lcRtn = await R_EmailEngineBack.R_EmailEngineSaveFromBackAsync(loEmailPar, loConn);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        _logger.LogInfo("end SaveEmailFromBack ");
        loException.ThrowExceptionIfErrors();
        _logger!.LogInfo(string.Format("End process method {0} on Cls", lcMethodName));
        return lcRtn;
    }

    private async Task<PMR03000GetEmailTemplateDTO> GetEmailTemplate(DbConnection poConn)
    {
        string lcMethodName = nameof(GetEmailTemplate);
        _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        var loEx = new R_Exception();
        PMR03000GetEmailTemplateDTO loResult = null;
        var loDb = new R_Db();
        DbConnection loConn = null;
        DbCommand loCmd = null;

        try
        {
            loConn = poConn;
            loCmd = loDb.GetCommand();
            ;
            _logger!.LogInfo("before exec RSP_GS_GET_EMAIL_TEMPLATE");

            var lcQuery = "RSP_GS_GET_EMAIL_TEMPLATE";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, _oKey.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTEMPLATE_ID", DbType.String, 50, "BILLING_STATEMENT_DISTRIBUTE");

            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

            try
            {
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMR03000GetEmailTemplateDTO>(loDataTable).FirstOrDefault()!;
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
        finally
        {
            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        return loResult!;
    }
}