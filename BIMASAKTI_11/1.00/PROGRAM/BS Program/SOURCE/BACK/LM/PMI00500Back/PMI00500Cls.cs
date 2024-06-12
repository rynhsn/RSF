using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMI00500Common;
using PMI00500Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMI00500Back;

public class PMI00500Cls
{
    private LoggerPMI00500 _logger;
    private readonly ActivitySource _activitySource;

    public PMI00500Cls()
    {
        _logger = LoggerPMI00500.R_GetInstanceLogger();
        _activitySource = PMI00500Activity.R_GetInstanceActivitySource();
    }

    public List<PMI00500PropertyDTO> GetPropertyList(PMI00500ParameterDb poParam)
    {
        
        using Activity loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<PMI00500PropertyDTO> loRtn = null;
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

            loRtn = R_Utility.R_ConvertTo<PMI00500PropertyDTO>(loDataTable).ToList();
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

    public List<PMI00500HeaderDTO> GetHeaderList(PMI00500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetHeaderList));
        R_Exception loEx = new();
        List<PMI00500HeaderDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_PM_GET_REMINDER_INQUIRY_HD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 8, poParam.CLANG_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMI00500HeaderDTO>(loDataTable).ToList();
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

    public List<PMI00500DTAgreementDTO> GetDTAgreementList(PMI00500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetDTAgreementList));
        R_Exception loEx = new();
        List<PMI00500DTAgreementDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_PM_GET_REMINDER_INQUIRY_DT_AGR";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poParam.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 8, poParam.CLANG_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTENANT_ID" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMI00500DTAgreementDTO>(loDataTable).ToList();
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

    public List<PMI00500DTReminderDTO> GetDTReminderList(PMI00500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetDTReminderList));
        R_Exception loEx = new();
        List<PMI00500DTReminderDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_PM_GET_REMINDER_INQUIRY_DT_REM";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poParam.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CAGREEMENT_NO", DbType.String, 20, poParam.CAGREEMENT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 8, poParam.CLANG_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTENANT_ID" or
                        "@CAGREEMENT_NO" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMI00500DTReminderDTO>(loDataTable).ToList();
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
    
    public List<PMI00500DTInvoiceDTO> GetDTInvoiceList(PMI00500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetDTInvoiceList));
        R_Exception loEx = new();
        List<PMI00500DTInvoiceDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_PM_GET_REMINDER_INQUIRY_DT_INV";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poParam.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CAGREEMENT_NO", DbType.String, 20, poParam.CAGREEMENT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CREMINDER_NO", DbType.String, 20, poParam.CREMINDER_NO);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 8, poParam.CLANG_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTENANT_ID" or
                        "@CAGREEMENT_NO" or
                        "@CREMINDER_NO" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMI00500DTInvoiceDTO>(loDataTable).ToList();
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
}