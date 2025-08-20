using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Transactions;
using ICB00100Common;
using ICB00100Common.DTOs;
using R_BackEnd;
using R_Common;

namespace ICB00100Back;

public class ICB00100Cls
{
    RSP_IC_SOFT_CLOSE_PERIODResources.Resources_Dummy_Class _resources = new();
    RSP_IC_VALIDATE_SOFTCLOSE_PRDResources.Resources_Dummy_Class _resourcesValidation = new();

    private LoggerICB00100 _logger;
    private readonly ActivitySource _activitySource;

    public ICB00100Cls()
    {
        _logger = LoggerICB00100.R_GetInstanceLogger();
        _activitySource = ICB00100Activity.R_GetInstanceActivitySource();
    }

    #region update spec

    public List<ICB00100PropertyDTO> GetPropertyList(ICB00100ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<ICB00100PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParam.CCOMPANY_ID}', '{poParam.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<ICB00100PropertyDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public ICB00100SystemParamDTO GetSystemParam(ICB00100ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
        R_Exception loEx = new();
        ICB00100SystemParamDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_IC_GET_SYSTEM_PARAM";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 8, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParam.CLANGUAGE_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<ICB00100SystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public ICB00100PeriodYearRangeDTO GetPeriod(ICB00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPeriod));
        R_Exception loEx = new();
        ICB00100PeriodYearRangeDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE '{poParams.CCOMPANY_ID}', '', ''";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<ICB00100PeriodYearRangeDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public void UpdateSoftClosePeriod(ICB00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(UpdateSoftClosePeriod));
        R_Exception loEx = new();
        // ICB00100PeriodYearRangeDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"RSP_IC_MAINTAIN_SOFT_CLOSE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YEAR", DbType.String, 4, poParams.CPERIOD_YEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MONTH", DbType.String, 2, poParams.CPERIOD_MONTH);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "EDIT");
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPERIOD_YEAR" or
                        "@CPERIOD_MONTH" or
                        "@CACTION" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            loDb.SqlExecNonQuery(loConn, loCmd, true);
            // var DataTable = loDb.SqlExecNonQuery(loConn, loCmd, true);

            // loReturn = R_Utility.R_ConvertTo<ICB00100PeriodYearRangeDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public List<ICB00100ValidateSoftCloseDTO> ValidateSoftClosePeriod(ICB00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ValidateSoftClosePeriod));
        R_Exception loEx = new();
        List<ICB00100ValidateSoftCloseDTO> loReturn = new();
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd = null;
        string lcQuery;

        using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                _logger.LogInfo("Start transcope");
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = $"RSP_IC_VALIDATE_SOFTCLOSE_PRD";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YEAR", DbType.String, 4, poParams.CPERIOD_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MONTH", DbType.String, 2, poParams.CPERIOD_MONTH);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CPROPERTY_ID" or
                            "@CPERIOD_YEAR" or
                            "@CPERIOD_MONTH" or
                            "@CUSER_ID"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                try
                {
                    var DataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loReturn = R_Utility.R_ConvertTo<ICB00100ValidateSoftCloseDTO>(DataTable).ToList();
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.LogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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

                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public ICB00100SoftClosePeriodDTO ProcessSoftClosePeriod(ICB00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ProcessSoftClosePeriod));
        R_Exception loEx = new();
        ICB00100SoftClosePeriodDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd = null;
        string lcQuery;

        using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                _logger.LogInfo("Start transcope");
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = $"RSP_IC_SOFT_CLOSE_PERIOD";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParams.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 30, poParams.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YEAR", DbType.String, 4, poParams.CPERIOD_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MONTH", DbType.String, 2, poParams.CPERIOD_MONTH);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poParams.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CPROPERTY_ID" or
                            "@CPERIOD_YEAR" or
                            "@CPERIOD_MONTH" or
                            "@CUSER_LOGIN_ID"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                try
                {
                    var DataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loReturn = R_Utility.R_ConvertTo<ICB00100SoftClosePeriodDTO>(DataTable).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.LogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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

                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    #endregion
}