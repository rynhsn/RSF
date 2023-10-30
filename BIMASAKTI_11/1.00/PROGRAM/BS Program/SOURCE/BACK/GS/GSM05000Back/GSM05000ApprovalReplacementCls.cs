using System.Data;
using System.Data.Common;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Back;

public class GSM05000ApprovalReplacementCls : R_BusinessObject<GSM05000ApprovalReplacementDTO>
{
    
    private LoggerGSM05000 _logger;

    public GSM05000ApprovalReplacementCls()
    {
        _logger = LoggerGSM05000.R_GetInstanceLogger();
    }
    protected override GSM05000ApprovalReplacementDTO R_Display(GSM05000ApprovalReplacementDTO poEntity)
    {
        R_Exception loEx = new();
        GSM05000ApprovalReplacementDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_REPLACEMENT_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 255, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_REPLACEMENT", DbType.String, 50, poEntity.CUSER_REPLACEMENT);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or
                        "@CDEPT_CODE" or
                        "@CUSER_ID" or
                        "@CUSER_REPLACEMENT" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalReplacementDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    protected override void R_Saving(GSM05000ApprovalReplacementDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;
        var lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcAction = "ADD";
            }
            else if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcAction = "EDIT";
            }

            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_APPR_REPLACE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 255, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_REPLACEMENT", DbType.String, 50, poNewEntity.CUSER_REPLACEMENT);
            loDb.R_AddCommandParameter(loCmd, "@CVALID_FROM", DbType.String, 50, poNewEntity.CVALID_FROM);
            loDb.R_AddCommandParameter(loCmd, "@CVALID_TO", DbType.String, 20, poNewEntity.CVALID_TO);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poNewEntity.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE" or
                        "@CDEPT_CODE" or
                        "@CUSER_ID" or
                        "@CUSER_REPLACEMENT" or
                        "@CVALID_FROM" or
                        "@CVALID_TO" or
                        "@CACTION" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(GSM05000ApprovalReplacementDTO poEntity)
    {
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;
        var lcAction = "DELETE";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_APPR_REPLACE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 255, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_REPLACEMENT", DbType.String, 50, poEntity.CUSER_REPLACEMENT);
            loDb.R_AddCommandParameter(loCmd, "@CVALID_FROM", DbType.String, 50, poEntity.CVALID_FROM);
            loDb.R_AddCommandParameter(loCmd, "@CVALID_TO", DbType.String, 20, poEntity.CVALID_TO);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE" or
                        "@CDEPT_CODE" or
                        "@CUSER_ID" or
                        "@CUSER_REPLACEMENT" or
                        "@CVALID_FROM" or
                        "@CVALID_TO" or
                        "@CACTION" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
    
    public List<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacement(GSM05000ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<GSM05000ApprovalReplacementDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_REPLACEMENT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poParameter.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or
                        "@CDEPT_CODE" or
                        "@CUSER_ID" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalReplacementDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
}