using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT01800BACK.Activity;
using PMT01800COMMON;
using PMT01800COMMON.DTO;
using PMT01800COMMON.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT01800BACK;

public class PMT01810Cls : R_BusinessObject<PMT01810DTO>
{
    private LogPMT01800Common _logger;
    private readonly ActivitySource _activitySource;

    public PMT01810Cls()
    {
        _logger = LogPMT01800Common.R_GetInstanceLogger();
        _activitySource = PMT01800Activity.R_GetInstanceActivitySource();
    }

    protected override PMT01810DTO R_Display(PMT01810DTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(PMT01810DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
    }

    protected override void R_Deleting(PMT01810DTO poEntity)
    {
        using var activity = _activitySource.StartActivity(nameof(R_Deleting));
        R_Exception loException = new R_Exception();
        string lcQuery = null;
        R_Db loDb;
        DbCommand loCommand;
        DbConnection loConn = null;
        string lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", System.Data.DbType.String, 50, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", System.Data.DbType.String, 50, poEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", System.Data.DbType.String, 50, poEntity.CREF_NO);
            loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@IDAYS", System.Data.DbType.Int32, 50, 0);
            loDb.R_AddCommandParameter(loCommand, "@IMONTHS", System.Data.DbType.Int32, 50, 0);
            loDb.R_AddCommandParameter(loCommand, "@IYEARS", System.Data.DbType.Int32, 50, 0);
            loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", System.Data.DbType.String, 50,
                "");
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", System.Data.DbType.String, 50,
                "");
            loDb.R_AddCommandParameter(loCommand, "@CNOTES", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", System.Data.DbType.String, 50,
                "");
            loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", System.Data.DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", System.Data.DbType.String, 50,
                "");
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poEntity.CUSER_ID);
            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                x.ParameterName == "CCOMPANY_ID" ||
                x.ParameterName == "CPROPERTY_ID" ||
                x.ParameterName == "CTRANS_CODE" ||
                x.ParameterName == "CREF_NO" ||
                x.ParameterName == "CREF_DATE" ||
                x.ParameterName == "CBUILDING_ID" ||
                x.ParameterName == "CDOC_NO" ||
                x.ParameterName == "CDOC_DATE" ||
                x.ParameterName == "CSTART_DATE" ||
                x.ParameterName == "CEND_DATE" ||
                x.ParameterName == "IDAYS" ||
                x.ParameterName == "IMONTHS" ||
                x.ParameterName == "IYEARS" ||
                x.ParameterName == "CSALESMAN_ID" ||
                x.ParameterName == "CTENANT_ID" ||
                x.ParameterName == "CUNIT_DESCRIPTION" ||
                x.ParameterName == "CNOTES" ||
                x.ParameterName == "CCURRENCY_CODE" ||
                x.ParameterName == "CLEASE_MODE" ||
                x.ParameterName == "CCHARGE_MODE" ||
                x.ParameterName == "CDEPT_CODE" ||
                x.ParameterName == "CACTION" ||
                x.ParameterName == "CUSER_ID").Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || AssignLooList(Cls) ", lcQuery, poEntity);
            //loDb.SqlExecNonQuery(loConn, loCommand, true);
            try
            {
                loDb.SqlExecNonQuery(loConn, loCommand, false);
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
            _logger.LogError(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    public List<PMT01810DTO> GetAllLocList(PMT01800DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetAllLocList));
        R_Exception loException = new R_Exception();
        List<PMT01810DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCommand;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_PM_GET_AGREEMENT_LIST";
            loCommand.CommandType = System.Data.CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50,
                poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50,
                poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", System.Data.DbType.String, 50,
                poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50,
                poParameter.CUSER_ID);
            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                x.ParameterName == "CCOMPANY_ID" ||
                x.ParameterName == "CPROPERTY_ID" ||
                x.ParameterName == "CTRANS_CODE" ||
                x.ParameterName == "CUSER_ID").Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || AssignLooList(Cls) ", lcQuery, poParameter);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<PMT01810DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }


    public List<PMT01810DTO> GetLocListDetail(PMT01800DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetLocListDetail));
        R_Exception loException = new R_Exception();
        List<PMT01810DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCommand;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_PM_GET_ASSIGN_LOO_UNIT_LIST";
            loCommand.CommandType = System.Data.CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50,
                poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50,
                poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", System.Data.DbType.String, 50,
                poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", System.Data.DbType.String, 50, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", System.Data.DbType.String, 50, poParameter.CREF_NO);
            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                x.ParameterName == "CCOMPANY_ID" ||
                x.ParameterName == "CPROPERTY_ID" ||
                x.ParameterName == "CTRANS_CODE" ||
                x.ParameterName == "CDEPT_CODE" ||
                x.ParameterName == "CREF_NO").Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || AssignLooList(Cls) ", lcQuery, poParameter);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<PMT01810DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }
}