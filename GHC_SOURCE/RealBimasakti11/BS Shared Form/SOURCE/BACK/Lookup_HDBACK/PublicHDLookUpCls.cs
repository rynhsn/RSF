using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using Lookup_HDCOMMON.DTOs.HDL00300;
using Lookup_HDCOMMON.DTOs.HDL00400;
using Lookup_HDCOMMON.DTOs.HDL00500;
using Lookup_HDCOMMON.Loggers;
using R_BackEnd;
using R_Common;

namespace Lookup_HDBACK;

public class PublicHDLookUpCls
{
    private LoggerHDPublicLookup _loggerAp;
    private readonly ActivitySource _activitySource;

    public PublicHDLookUpCls()
    {
        _loggerAp = LoggerHDPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_HDBackActivity.R_GetInstanceActivitySource();
    }

    public List<HDL00100DTO> PriceListLookupHeader(HDL00100ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(PriceListLookupHeader));
        R_Exception loException = new R_Exception();
        List<HDL00100DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_HD_GET_PRICELIST_HEADER_LIST ";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAXABLE", DbType.String, 90, poParameter.CTAXABLE);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 90, poParameter.CLANG_ID);

            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_HD_GET_PRICELIST_HEADER_LIST {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<HDL00100DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<HDL00100DTO> PriceListLookupDetail(HDL00100ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(PriceListLookupDetail));
        R_Exception loException = new R_Exception();
        List<HDL00100DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_HD_GET_PRICELIST_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 90, poParameter.CSTATUS);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 90, poParameter.CLANG_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPRICELIST_ID", DbType.String, 90, poParameter.CPRICELIST_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLOOKUP_DATE", DbType.String, 90, poParameter.CLOOKUP_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CSLA_CALL_TYPE_ID", DbType.String, 90, poParameter.CSLA_CALL_TYPE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAXABLE", DbType.String, 90, poParameter.CTAXABLE);
            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_HD_GET_PRICELIST_LIST {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<HDL00100DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<HDL00200DTO> PriceListItemLookup(HDL00200ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(PriceListLookupDetail));
        R_Exception loException = new R_Exception();
        List<HDL00200DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_HD_GET_PRICELIST_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 90, poParameter.CSTATUS);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 90, poParameter.CLANG_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPRICELIST_ID", DbType.String, 90, poParameter.CPRICELIST_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLOOKUP_DATE", DbType.String, 90, poParameter.CLOOKUP_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CSLA_CALL_TYPE_ID", DbType.String, 90, poParameter.CSLA_CALL_TYPE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAXABLE", DbType.String, 90, poParameter.CTAXABLE);
            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_HD_GET_PRICELIST_LIST {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<HDL00200DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<HDL00300DTO> PublicLocationLookup(HDL00300ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(PublicLocationLookup));
        R_Exception loException = new R_Exception();
        List<HDL00300DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_HD_GET_PUBLIC_LOC_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 90, poParameter.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 90, poParameter.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 90, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poParameter.LACTIVE);
 
            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_HD_GET_PUBLIC_LOC_LIST {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<HDL00300DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
    
    public List<HDL00400DTO> AssetLookup(HDL00400ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(AssetLookup));
        R_Exception loException = new R_Exception();
        List<HDL00400DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_HD_GET_ASSET_LOOKUP_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 90, poParameter.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 90, poParameter.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 90, poParameter.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLOCATION_ID", DbType.String, 90, poParameter.CLOCATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CAREA_TYPE", DbType.String, 90, poParameter.CAREA_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 90, poParameter.CUSER_ID);
            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_HD_GET_ASSET_LOOKUP_LIST {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<HDL00400DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
    public List<HDL00500DTO> TaskLookup(HDL00500ParameterDTO poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(TaskLookup));
        R_Exception loException = new R_Exception();
        List<HDL00500DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_HD_GET_TASK_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 90, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 90, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 90, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTASK_CTG_CODE", DbType.String, 90, "");
            //Debug Logs
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName ==
                            "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                .Select(x => x.Value);
            _loggerAp.LogDebug("EXEC RSP_HD_GET_TASK_DT_LIST {@poParameter}", loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<HDL00500DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)

        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
}