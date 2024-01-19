using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Transactions;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_GS_MAINTAIN_TRANS_CODE_APPROVERResources;

namespace GSM05000Back;

public class GSM05000ApprovalUserCls: R_BusinessObject<GSM05000ApprovalUserDTO>
{
    
    Resources_Dummy_Class _resources = new();
    
    private LoggerGSM05000 _logger;
    private readonly ActivitySource _activitySource;

    public GSM05000ApprovalUserCls()
    {
        _logger = LoggerGSM05000.R_GetInstanceLogger();
        _activitySource = GSM05000Activity.R_GetInstanceActivitySource();
    }
    
    protected override GSM05000ApprovalUserDTO R_Display(GSM05000ApprovalUserDTO poEntity)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_Display));
        R_Exception loEx = new();
        GSM05000ApprovalUserDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_APPROVER_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or
                        "@CUSER_ID" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalUserDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    protected override void R_Saving(GSM05000ApprovalUserDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_Saving));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn=null;
        var lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);
            
            lcAction = poCRUDMode switch
            {
                eCRUDMode.AddMode => "ADD",
                eCRUDMode.EditMode => "EDIT",
                _ => lcAction
            };

            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_APPROVER";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poNewEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 255, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@ISEQUENCE", DbType.String, 10, poNewEntity.ISEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LREPLACEMENT", DbType.Boolean, 1, poNewEntity.LREPLACEMENT);
            loDb.R_AddCommandParameter(loCmd, "@NLIMIT_AMOUNT", DbType.Decimal, 20, poNewEntity.NLIMIT_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 10, poNewEntity.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE" or
                        "@CDEPT_CODE" or
                        "@CUSER_ID" or
                        "@ISEQUENCE" or
                        "@LREPLACEMENT" or
                        "@NLIMIT_AMOUNT" or
                        "@CACTION" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            try
            {
                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
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
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(GSM05000ApprovalUserDTO poEntity)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_Deleting));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn=null;
        var lcAction = "DELETE";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);
            
            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_APPROVER";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 255, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@ISEQUENCE", DbType.String, 10, poEntity.ISEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LREPLACEMENT", DbType.Boolean, 1, poEntity.LREPLACEMENT);
            loDb.R_AddCommandParameter(loCmd, "@NLIMIT_AMOUNT", DbType.Decimal, 20, poEntity.NLIMIT_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 10, poEntity.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE" or
                        "@CDEPT_CODE" or
                        "@CUSER_ID" or
                        "@ISEQUENCE" or
                        "@LREPLACEMENT" or
                        "@NLIMIT_AMOUNT" or
                        "@CACTION" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            try
            {
                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
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
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
    
    public List<GSM05000ApprovalUserDTO> GSM05000GetApprovalUser(GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000GetApprovalUser));
        R_Exception loEx = new();
        List<GSM05000ApprovalUserDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_APPROVER_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poParameter.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalUserDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader (GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000GetApprovalHeader));
        R_Exception loEx = new();
        GSM05000ApprovalHeaderDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalHeaderDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public string GSM05000ValidationForAction (GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000ValidationForAction));
        R_Exception loEx = new();
        string lcRtn = "";
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"SELECT TOP 1 1 FROM GSM_TRANS_APV_REPLACEMENT (NOLOCK)
			                WHERE CCOMPANY_ID = @CCOMPANY_ID
			                AND CTRANS_CODE = @CTRANS_CODE
			                AND CDEPT_CODE = @CDEPT_CODE
			                AND CUSER_ID = @CUSER_ID";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("{pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            lcRtn = R_Utility.R_ConvertTo<string>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return lcRtn;
    }

    public List<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartment(GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000GetApprovalDepartment));
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"EXEC RSP_GS_GET_DEPT_LOOKUP_LIST '{poParameter.CCOMPANY_ID}', '{poParameter.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalDepartmentDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public List<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartment(GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000LookupApprovalDepartment));
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_DEPT_LOOKUP_LIST '{poParameter.CCOMPANY_ID}', '{poParameter.CUSER_ID}', 'GSM05000'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalDepartmentDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public void GSM05000ApprovalCopyTo(GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000ApprovalCopyTo));
        R_Exception loEx = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_COPY_TO_USER_APPR";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE_TO", DbType.String, 8, poParameter.CDEPT_CODE_TO);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParameter.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or
                        "@CDEPT_CODE" or
                        "@CDEPT_CODE_TO" or
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

        loEx.ThrowExceptionIfErrors();
    }
    
    public void GSM05000ApprovalCopyFrom(GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000ApprovalCopyFrom));
        R_Exception loEx = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_COPY_FROM_USER_APPR";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE_FROM", DbType.String, 8, poParameter.CDEPT_CODE_FROM);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParameter.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or
                        "@CDEPT_CODE" or
                        "@CDEPT_CODE_FROM" or
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

        loEx.ThrowExceptionIfErrors();
    }
    
    public List<GSM05000ApprovalDepartmentDTO> GSM05000DepartmentChangeSequence(GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000DepartmentChangeSequence));
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            lcQuery = "RSP_GS_GET_TRANS_CODE_DEPT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalDepartmentDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public List<GSM05000ApprovalUserDTO> GSM05000GetUserSequenceData(GSM05000ParameterDb poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000GetUserSequenceData));
        R_Exception loEx = new();
        List<GSM05000ApprovalUserDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_APPROVER_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poParameter.CUSER_LOGIN_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or
                        "@CUSER_LOGIN_ID" or
                        "@CDEPT_CODE"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalUserDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex); 
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }   

    public void GSM05000UpdateSequence(List<GSM05000ApprovalUserDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM05000UpdateSequence));
        R_Exception loEx = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                foreach (var poEntity in poParameter)
                {
                    R_Saving(poEntity, eCRUDMode.EditMode);
                }

                transactionScope.Complete();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        // return loRtn;
    }
}