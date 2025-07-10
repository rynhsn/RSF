using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Lookup_ICCOMMON.DTOs.ICL00100;
using Lookup_ICCOMMON.DTOs.ICL00200;
using Lookup_ICCOMMON.DTOs.ICL00300;
using Lookup_ICCOMMON.Loggers;
using R_BackEnd;
using R_Common;

namespace Lookup_ICBACK;

public class PublicICLookupCls
{
    
    private LoggerICPublicLookup _loggerIC;
    private readonly ActivitySource _activitySource;

    public PublicICLookupCls()
    {
        _loggerIC = LoggerICPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_ICBackActivity.R_GetInstanceActivitySource();
    }
    
    public List<ICL00100DTO> RequestLookup(ICL00100ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(RequestLookup));
        R_Exception loException = new R_Exception();
        List<ICL00100DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_IC_GET_REQUEST_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd,"@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd,"@CDEPT_CODE", DbType.String, 90, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd,"@CUSER_ID", DbType.String, 90, poParameter.CUSER_ID);

            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerIC.LogDebug("EXEC RSP_IC_GET_REQUEST_LIST  {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<ICL00100DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
    
    public List<ICL00200DTO> RequestNoLookup(ICL00200ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(RequestNoLookup));
        R_Exception loException = new R_Exception();
        List<ICL00200DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_IC_GET_INVENTORY_REQ_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd,"@CUSER_ID", DbType.String, 90, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd,"@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd,"@CDEPT_CODE", DbType.String, 90, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd,"@CALLOC_ID", DbType.String, 90, poParameter.CALLOC_ID);

            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerIC.LogDebug("EXEC RSP_IC_GET_INVENTORY_REQ_LIST  {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<ICL00200DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    

    public ICL00300PeriodDTO InitialProcessICL00300(ICL00300ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(InitialProcessICL00300));
        R_Exception loException = new R_Exception();
        ICL00300PeriodDTO loReturn = null;
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
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 10, poParameter.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, poParameter.CMODE);


            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
            _loggerIC.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<ICL00300PeriodDTO>(loReturnTemp).FirstOrDefault();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
    public List<ICL00300DTO> TransactionLookup(ICL00300ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(RequestNoLookup));
        R_Exception loException = new R_Exception();
        List<ICL00300DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_IC_LOOKUP_TRX_REF_NO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd,"@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);
            loDb.R_AddCommandParameter(loCmd,"@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd,"@CTRANS_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd,"@CPERIOD", DbType.String, 6, poParameter.CPERIOD);
            loDb.R_AddCommandParameter(loCmd,"@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd,"@CWAREHOUSE_ID", DbType.String, 20, poParameter.CWAREHOUSE_ID);
            loDb.R_AddCommandParameter(loCmd,"@CALLOC_ID", DbType.String, 20, poParameter.CALLOC_ID);
            loDb.R_AddCommandParameter(loCmd,"@CTRANS_STATUS", DbType.String, Int32.MaxValue, poParameter.CTRANS_STATUS);

            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerIC.LogDebug("EXEC RSP_IC_LOOKUP_TRX_REF_NO   {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<ICL00300DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
}