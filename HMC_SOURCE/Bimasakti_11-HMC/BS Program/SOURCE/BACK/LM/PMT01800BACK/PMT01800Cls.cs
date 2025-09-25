using System.Data.Common;
using System.Diagnostics;
using PMT01800BACK.Activity;
using PMT01800COMMON;
using PMT01800COMMON.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT01800BACK;

public class PMT01800Cls : R_BusinessObject<PMT01800DTO>
{
    private LogPMT01800Common _logger;
    private readonly ActivitySource _activitySource;

    public PMT01800Cls()
    {
        _logger = LogPMT01800Common.R_GetInstanceLogger();
        _activitySource = PMT01800Activity.R_GetInstanceActivitySource();
    }
    protected override PMT01800DTO R_Display(PMT01800DTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(PMT01800DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
    }

    protected override void R_Deleting(PMT01800DTO poEntity)
    {
        throw new NotImplementedException();
    }
    
    public PMT01800InitialProcessDTO InitSpinnerYear(PMT01800DBParameter poParameter)
    {
        using var Activity = _activitySource.StartActivity(nameof(InitSpinnerYear));
        R_Exception loException = new R_Exception();
        PMT01800InitialProcessDTO loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @$"SELECT YEAR(dbo.RFN_GET_DB_TODAY ('{poParameter.CCOMPANY_ID}')) as 'CYEAR_DEFAULT'";
            loCmd.CommandType = System.Data.CommandType.Text;
            loCmd.CommandText = lcQuery;

            
            _logger.LogDebug("{Query} || AssignLooList(Cls) ", lcQuery, poParameter);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
            loReturn = R_Utility.R_ConvertTo<PMT01800InitialProcessDTO>(loReturnTemp).FirstOrDefault();
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
    
    public List<PMT01800PropertyDTO> GetAllPropertyList(PMT01800DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetAllPropertyList));
        R_Exception loException = new R_Exception();
        List<PMT01800PropertyDTO> loReturn = null;
        R_Db loDb;
        DbCommand loCommand;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
            loCommand.CommandType = System.Data.CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poParameter.CUSER_ID);
            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                x.ParameterName == "CCOMPANY_ID" ||
                x.ParameterName == "CUSER_ID").Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || AssignLooList(Cls) ", lcQuery, poParameter);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<PMT01800PropertyDTO>(loReturnTemp).ToList();
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
    
    public List<PMT01800DTO> GetAllLooList(PMT01800DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetAllLooList));
        R_Exception loException = new R_Exception();
        List<PMT01800DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCommand;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_PM_GET_ASSIGN_LOO_LIST";
            loCommand.CommandType = System.Data.CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CLOO_TRANS_CODE", System.Data.DbType.String, 50, poParameter.CLOO_TRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CLOC_TRANS_CODE", System.Data.DbType.String, 50, poParameter.CLOC_TRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CFROM_DATE", System.Data.DbType.String, 50, poParameter.CFROM_DATE);
            loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", System.Data.DbType.String, 50, poParameter.CLANG_ID);
            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                x.ParameterName == "CCOMPANY_ID" ||
                x.ParameterName == "CPROPERTY_ID" ||
                x.ParameterName == "CLOO_TRANS_CODE" ||
                x.ParameterName == "CLOC_TRANS_CODE" ||
                x.ParameterName == "CFROM_DATE" ||
                x.ParameterName == "CLANG_ID").Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || AssignLooList(Cls) ", lcQuery, poParameter);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<PMT01800DTO>(loReturnTemp).ToList();
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
    
    public List<PMT01800DTO> GetLooListDetail(PMT01800DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetLooListDetail));
        R_Exception loException = new R_Exception();
        List<PMT01800DTO> loReturn = null;
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

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", System.Data.DbType.String, 50, poParameter.CTRANS_CODE);
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
            loReturn = R_Utility.R_ConvertTo<PMT01800DTO>(loReturnTemp).ToList();
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