using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT03500Back;

public class PMT03500UpdateMeterCls
{
    private LoggerPMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT03500UpdateMeterCls()
    {
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = PMT03500Activity.R_GetInstanceActivitySource();
    }

    public List<PMT03500BuildingUnitDTO> GetBuildingUnitList(PMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetBuildingUnitList));
        R_Exception loEx = new();
        List<PMT03500BuildingUnitDTO> loRtn = null;
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

            loRtn = R_Utility.R_ConvertTo<PMT03500BuildingUnitDTO>(loDataTable).ToList();
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

    public List<PMT03500UtilityMeterDTO> GetUtilityMeterList(PMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetUtilityMeterList));
        R_Exception loEx = new();
        List<PMT03500UtilityMeterDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_AGREEMENT_UTILITIES_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            // loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poParam.CTENANT_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);

            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParam.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poParam.CFLOOR_ID);

            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTRANS_CODE" or
                        "@CREF_NO" or
                        "@CUNIT_ID" or
                        "@CFLOOR_ID" or
                        "@CBUILDING_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500UtilityMeterDTO>(loDataTable).ToList();
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

    public List<PMT03500MeterNoDTO> GetMeterNoList(PMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetUtilityMeterList));
        R_Exception loEx = new();
        List<PMT03500MeterNoDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_BUILDING_UTILITIES_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poParam.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 20, poParam.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CFLOOR_ID" or
                        "@CUNIT_ID" or
                        "@CUTILITY_TYPE" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500MeterNoDTO>(loDataTable).ToList();
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

    public PMT03500UtilityMeterDetailDTO GetUtilityMeterDetail(PMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetUtilityMeterDetail));
        R_Exception loEx = new();
        PMT03500UtilityMeterDetailDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            // lcQuery = "RSP_PM_GET_UTILITY_METER_DT";
            lcQuery = "RSP_PM_GET_AGREEMENT_UTILITIES_DT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            // loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 20, poParam.CCHARGES_TYPE);
            // loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poParam.CCHARGES_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParam.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poParam.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 20, poParam.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poParam.CCHARGES_ID);
            loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 3, poParam.CSEQ_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTRANS_CODE" or
                        "@CREF_NO" or
                        "@CUNIT_ID" or
                        "@CFLOOR_ID" or
                        "@CBUILDING_ID" or
                        "@CCHARGES_TYPE" or
                        "@CCHARGES_ID" or
                        "@CSEQ_NO" or
                        "@CUSER_ID"
                ).Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500UtilityMeterDetailDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public PMT03500AgreementUtilitiesDTO GetAgreementUtilities(PMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetAgreementUtilities));
        R_Exception loEx = new();
        PMT03500AgreementUtilitiesDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            // lcQuery = "RSP_PM_GET_AGREEMENT_UTILITIES";
            lcQuery = "RSP_PM_GET_AGREEMENT_BY_UNIT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poParam.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@LOTHER_UNIT", DbType.Boolean, 1, poParam.LOTHER_UNIT);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CUNIT_ID" or
                        "@CFLOOR_ID" or
                        "@LOTHER_UNIT" or
                        "@CUSER_ID"
                ).Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500AgreementUtilitiesDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public List<PMT03500PeriodRangeDTO> GetPeriodRangeList(PMT03500ParameterDb poParam)
    {
        using var loScope = _activitySource.StartActivity(nameof(GetPeriodRangeList));
        R_Exception loEx = new();
        List<PMT03500PeriodRangeDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PERIOD_DT_RANGE_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParam.CFROM_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParam.CTO_PERIOD);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CFROM_PERIOD" or
                        "@TO_PERIOD"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500PeriodRangeDTO>(loDataTable).ToList();
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

    public void PMT03500UpdateMeterNo(PMT03500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(PMT03500UpdateMeterNo));
        R_Exception loEx = new R_Exception();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            // var loUtilityType = poParams.EUTYLITY_TYPE.ToString();
            // lcQuery = $"RSP_PM_UPDATE_UTILITY_METER_NO_{loUtilityType}";
            
            lcQuery = $"RSP_PM_UPDATE_UTILITY_METER_NO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParams.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 30, poParams.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 30, poParams.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 30, poParams.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_INV_PRD", DbType.String, 30, poParams.CSTART_INV_PRD);
            loDb.R_AddCommandParameter(loCmd, "@CMETER_NO", DbType.String, 30, poParams.CMETER_NO);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poParams.CSTART_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParams.CUSER_ID);

            loDb.R_AddCommandParameter(loCmd, "@NMETER_START", DbType.Decimal, 255, poParams.NMETER_START);
            loDb.R_AddCommandParameter(loCmd, "@NBLOCK1_START", DbType.Decimal, 255, poParams.NBLOCK1_START);
            loDb.R_AddCommandParameter(loCmd, "@NBLOCK2_START", DbType.Decimal, 255, poParams.NBLOCK2_START);
            
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
                        "@NBLOCK1_START" or
                        "@NBLOCK2_START" or
                        "@NMETER_START" or
                        "@CSTART_INV_PRD" or
                        "@CSTART_DATE" or
                        "@CUSER_LOGIN_ID"
                ).Select(x => x.Value);
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

    public void PMT03500ChangeMeterNo(PMT03500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(PMT03500ChangeMeterNo));
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
            
            lcQuery = $"RSP_PM_CHANGE_UTILITY_METER_NO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParams.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParams.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParams.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParams.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poParams.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParams.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 30, poParams.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, poParams.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poParams.CCHARGES_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_METER_NO", DbType.String, 30, poParams.CFROM_METER_NO);
            loDb.R_AddCommandParameter(loCmd, "@NMETER_END", DbType.Decimal, 255, poParams.NMETER_END);
            loDb.R_AddCommandParameter(loCmd, "@NBLOCK1_END", DbType.Decimal, 255, poParams.NBLOCK1_END);
            loDb.R_AddCommandParameter(loCmd, "@NBLOCK2_END", DbType.Decimal, 255, poParams.NBLOCK2_END);
            loDb.R_AddCommandParameter(loCmd, "@CTO_METER_NO", DbType.String, 30, poParams.CTO_METER_NO);
            loDb.R_AddCommandParameter(loCmd, "@NMETER_START", DbType.String, 255, poParams.NMETER_START);
            loDb.R_AddCommandParameter(loCmd, "@NBLOCK1_START", DbType.String, 255, poParams.NBLOCK1_START);
            loDb.R_AddCommandParameter(loCmd, "@NBLOCK2_START", DbType.String, 255, poParams.NBLOCK2_START);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_INV_PRD", DbType.String, 20, poParams.CSTART_INV_PRD);
            loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 20, poParams.CSTART_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTRANS_CODE" or
                        "@CREF_NO" or
                        "@CUNIT_ID" or
                        "@CFLOOR_ID" or
                        "@CBUILDING_ID" or
                        "@CTENANT_ID" or
                        "@CCHARGES_TYPE" or
                        "@CCHARGES_ID" or
                        "@CFROM_METER_NO" or
                        "@NMETER_END" or
                        "@NBLOCK1_END" or
                        "@NBLOCK2_END" or
                        "@CTO_METER_NO" or
                        "@NMETER_START" or
                        "@NBLOCK1_START" or
                        "@NBLOCK2_START" or
                        "@CSTART_INV_PRD" or
                        "@CSTART_DATE" or
                        "@CUSER_ID"
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