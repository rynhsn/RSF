using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using Lookup_CBCOMMON.DTOs.Loggers;
using R_BackEnd;
using R_Common;

namespace Lookup_CBBACK;

public class PublicCBLookupCls
{
    private LoggerCBPublicLookup _loggerAp;
    private readonly ActivitySource _activitySource;

    public PublicCBLookupCls()
    {
        _loggerAp = LoggerCBPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_CBBACKActivity.R_GetInstanceActivitySource();
    }


    public CBL00100DTO InitialProcessCBL00100(CBL00100ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(InitialProcessCBL00100));
        R_Exception loException = new R_Exception();
        CBL00100DTO loReturn = null;
        R_Db loDb;
        DbCommand loCmd;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_PERIOD_YEAR_RANGE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 10, "");
            loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, "");
            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<CBL00100DTO>(loReturnTemp).FirstOrDefault();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public CBL00100DTO InitialProcessCBL00100Month(CBL00100ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(InitialProcessCBL00100Month));
        R_Exception loException = new R_Exception();
        CBL00100DTO loReturn = null;
        R_Db loDb;
        DbCommand loCmd;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_PERIOD_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 10, poParameter.CYEAR);
            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_GS_GET_PERIOD_DT_LIST {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<CBL00100DTO>(loReturnTemp).FirstOrDefault();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<CBL00100DTO> ReceiptFromCustomerLookup(CBL00100ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(ReceiptFromCustomerLookup));
        R_Exception loException = new R_Exception();
        List<CBL00100DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_CB_LOOKUP_RECEIPT_FROM_CUSTOMER";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 10, poParameter.CPERIOD);


            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_CB_LOOKUP_RECEIPT_FROM_CUSTOMER {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<CBL00100DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
    
    
    public List<CBL00200DTO> CBJournalLookup(CBL00200ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(CBJournalLookup));
        R_Exception loException = new R_Exception();
        List<CBL00200DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_CB_LOOKUP_JOURNAL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 10, poParameter.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);


            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_CB_LOOKUP_JOURNAL {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<CBL00200DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
}