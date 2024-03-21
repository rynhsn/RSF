using System.Data;
using System.Data.Common;
using System.Diagnostics;
using LMT03500Common;
using LMT03500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMT03500Back;

public class LMT03500UpdateMeterCls
{
    private LoggerLMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public LMT03500UpdateMeterCls()
    {
        _logger = LoggerLMT03500.R_GetInstanceLogger();
        _activitySource = LMT03500Activity.R_GetInstanceActivitySource();
    }

    public List<LMT03500BuildingUnitDTO> GetBuildingUnitList(LMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetBuildingUnitList));
        R_Exception loEx = new();
        List<LMT03500BuildingUnitDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_BUILDING_UNIT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poParam.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParam.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CFLOOR_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMT03500BuildingUnitDTO>(loDataTable).ToList();
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

    public List<LMT03500UtilityMeterDTO> GetUtilityMeterList(LMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetUtilityMeterList));
        R_Exception loEx = new();
        List<LMT03500UtilityMeterDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_UTILITY_METER_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poParam.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CTENANT_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMT03500UtilityMeterDTO>(loDataTable).ToList();
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

    public LMT03500UtilityMeterDetailDTO GetUtilityMeterDetail(LMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetUtilityMeterDetail));
        R_Exception loEx = new();
        LMT03500UtilityMeterDetailDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_UTILITY_METER_DT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 20, poParam.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poParam.CCHARGES_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CUNIT_ID" or
                        "@CCHARGES_TYPE" or
                        "@CCHARGES_ID" or
                        "@CUSER_ID"
                ).Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMT03500UtilityMeterDetailDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public LMT03500AgreementUtilitiesDTO GetAgreementUtilities(LMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetAgreementUtilities));
        R_Exception loEx = new();
        LMT03500AgreementUtilitiesDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_AGREEMENT_UTILITIES";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CUNIT_ID" or
                        "@CUSER_ID"
                ).Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMT03500AgreementUtilitiesDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public void LMT03500UpdateMeterNo(LMT03500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(LMT03500UpdateMeterNo));
        R_Exception loEx = new R_Exception();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_UPDATE_UTILITY_METER_NO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParams.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 30, poParams.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 30, poParams.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 30, poParams.CUTILITY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CMETER_NO", DbType.String, 30, poParams.CMETER_NO);
            loDb.R_AddCommandParameter(loCmd, "@IMETER_START", DbType.Int32, 255, poParams.IMETER_START);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_INV_PRD", DbType.String, 30, poParams.CSTART_INV_PRD);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poParams.CSTART_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CREF_NO" or
                        "@CUNIT_ID" or
                        "@CTENANT_ID" or
                        "@CUTILITY_TYPE" or
                        "@CMETER_NO" or
                        "@IMETER_START" or
                        "@CSTART_INV_PRD" or
                        "@CSTART_DATE" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            loDb.SqlExecQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public void LMT03500ChangeMeterNo(LMT03500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(LMT03500ChangeMeterNo));
        R_Exception loEx = new R_Exception();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_CHANGE_UTILITY_METER_NO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParams.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 30, poParams.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 30, poParams.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 30, poParams.CUTILITY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@IFROM_METER_NO", DbType.Int32, 255, poParams.IFROM_METER_NO);
            loDb.R_AddCommandParameter(loCmd, "@IMETER_END", DbType.Int32, 255, poParams.IMETER_END);
            loDb.R_AddCommandParameter(loCmd, "@ITO_METER_NO", DbType.Int32, 255, poParams.ITO_METER_NO);
            loDb.R_AddCommandParameter(loCmd, "@IMETER_START", DbType.Int32, 255, poParams.IMETER_START);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_INV_PRD", DbType.String, 30, poParams.CSTART_INV_PRD);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poParams.CSTART_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CREF_NO" or
                        "@CUNIT_ID" or
                        "@CTENANT_ID" or
                        "@CUTILITY_TYPE" or
                        "@IFROM_METER_NO" or
                        "@IMETER_END" or
                        "@ITO_METER_NO" or
                        "@IMETER_START" or
                        "@CSTART_INV_PRD" or
                        "@CSTART_DATE" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            loDb.SqlExecQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
    }
}
    