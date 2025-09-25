using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Back;

#region "Not Async Version"
// public class GSM05000NumberingCls : R_BusinessObject<GSM05000NumberingGridDTO>
// {
//     private LoggerGSM05000 _logger;
//     private readonly ActivitySource _activitySource;
//
//     public GSM05000NumberingCls()
//     {
//         _logger = LoggerGSM05000.R_GetInstanceLogger();
//         _activitySource =GSM05000Activity.R_GetInstanceActivitySource();
//     }
//     
//     protected override GSM05000NumberingGridDTO R_Display(GSM05000NumberingGridDTO poEntity)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_Display));
//         R_Exception loEx = new();
//         GSM05000NumberingGridDTO loRtn = null;
//         R_Db loDb;
//         DbConnection loConn;
//         DbCommand loCmd;
//         string lcQuery;
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             lcQuery = "RSP_GS_GET_TRANS_CODE_NUMBER_DETAIL";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
//             loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);
//             loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_ID);
//             
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTRANSACTION_CODE" or 
//                         "@CPERIOD_NO" or
//                         "@CDEPT_CODE" or
//                         "@CUSER_LOGIN_ID"
//                 )
//                 .Select(x => x.Value);
//
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//             
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//             loRtn = R_Utility.R_ConvertTo<GSM05000NumberingGridDTO>(loDataTable).FirstOrDefault();
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//         return loRtn;
//     }
//
//     protected override void R_Saving(GSM05000NumberingGridDTO poNewEntity, eCRUDMode poCRUDMode)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_Saving));
//         R_Exception loEx = new();
//         string lcQuery;
//         R_Db loDb;
//         DbCommand loCmd;
//         DbConnection loConn;
//         var lcAction = "";
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//             
//             if (poCRUDMode == eCRUDMode.AddMode)
//             {
//                 lcAction = "ADD";
//             }
//             else if (poCRUDMode == eCRUDMode.EditMode)
//             {
//                 lcAction = "EDIT";
//             }
//
//             lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_NUMBERING";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
//             loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 4, poNewEntity.CCYEAR);
//             loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poNewEntity.CPERIOD_NO);
//             loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
//             loDb.R_AddCommandParameter(loCmd, "@ISTART_NUMBER", DbType.Int32, 8, poNewEntity.ISTART_NUMBER);
//             loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
//             
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTRANSACTION_CODE" or 
//                         "@CCYEAR" or
//                         "@CPERIOD_NO" or
//                         "@CDEPT_CODE" or
//                         "@ISTART_NUMBER" or
//                         "@CACTION" or
//                         "@CUSER_ID"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//             
//             loDb.SqlExecNonQuery(loConn, loCmd, true);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         EndBlock:
//         loEx.ThrowExceptionIfErrors();
//     }
//
//     protected override void R_Deleting(GSM05000NumberingGridDTO poEntity)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_Deleting));
//         R_Exception loEx = new();
//         string lcQuery;
//         R_Db loDb;
//         DbCommand loCmd;
//         DbConnection loConn;
//         var lcAction = "DELETE";
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//             
//             lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_NUMBERING";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
//             loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 50, poEntity.CCYEAR);
//             loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);
//             loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
//             loDb.R_AddCommandParameter(loCmd, "@ISTART_NUMBER", DbType.Int32, 8, poEntity.ISTART_NUMBER);
//             loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
//             
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTRANSACTION_CODE" or 
//                         "@CCYEAR" or
//                         "@CPERIOD_NO" or
//                         "@CDEPT_CODE" or
//                         "@ISTART_NUMBER" or
//                         "@CACTION" or
//                         "@CUSER_ID"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//             
//             loDb.SqlExecNonQuery(loConn, loCmd, true);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         EndBlock:
//         loEx.ThrowExceptionIfErrors();
//     }
//
//     public List<GSM05000NumberingGridDTO> GetNumberingListDb(GSM05000ParameterDb poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(GetNumberingListDb));
//         R_Exception loEx = new();
//         List<GSM05000NumberingGridDTO> loRtn = null;
//         R_Db loDb;
//         DbConnection loConn;
//         DbCommand loCmd;
//         string lcQuery;
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             lcQuery = "RSP_GS_GET_TRANS_CODE_NUMBER_LIST";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poParameter.CUSER_LOGIN_ID);
//             
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTRANSACTION_CODE" or 
//                         "@CUSER_LOGIN_ID"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//             
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//             loRtn = R_Utility.R_ConvertTo<GSM05000NumberingGridDTO>(loDataTable).ToList();
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//
//         return loRtn;
//     }
//
//     public GSM05000NumberingHeaderDTO GetNumberingHeaderDb(GSM05000ParameterDb poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(GetNumberingHeaderDb));
//         R_Exception loEx = new();
//         GSM05000NumberingHeaderDTO loRtn = null;
//         R_Db loDb;
//         DbConnection loConn;
//         DbCommand loCmd;
//         string lcQuery;
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             lcQuery = @"SELECT CTRANS_CODE, 
//                         CTRANSACTION_NAME, 
//                         LDEPT_MODE, 
//                         CPERIOD_MODE, 
//                         (CASE WHEN CPERIOD_MODE = 'N' THEN 'None'
//                               WHEN CPERIOD_MODE = 'P' THEN 'Periodically'
//                               WHEN CPERIOD_MODE = 'Y' THEN 'Yearly' END) AS CPERIOD_MODE_DESCR
//                         FROM GSM_TRANSACTION_CODE (NOLOCK)
//                         WHERE CCOMPANY_ID = @CCOMPANY_ID
//                         AND CTRANS_CODE = @CTRANS_CODE";
//             loCmd.CommandType = CommandType.Text;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
//             
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTRANS_CODE"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("{pcQuery} {@poParam}", lcQuery, loDbParam);
//             
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//             loRtn = R_Utility.R_ConvertTo<GSM05000NumberingHeaderDTO>(loDataTable).FirstOrDefault();
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//
//         return loRtn;
//     } 
// }
#endregion

#region "Async Version"
public class GSM05000NumberingCls : R_BusinessObjectAsync<GSM05000NumberingGridDTO>
{
    private LoggerGSM05000 _logger;
    private readonly ActivitySource _activitySource;

    public GSM05000NumberingCls()
    {
        _logger = LoggerGSM05000.R_GetInstanceLogger();
        _activitySource =GSM05000Activity.R_GetInstanceActivitySource();
    }
    
    protected override async Task<GSM05000NumberingGridDTO> R_DisplayAsync(GSM05000NumberingGridDTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_DisplayAsync));
        R_Exception loEx = new();
        GSM05000NumberingGridDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_NUMBER_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or 
                        "@CPERIOD_NO" or
                        "@CDEPT_CODE" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000NumberingGridDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    protected override async Task R_SavingAsync(GSM05000NumberingGridDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_SavingAsync));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;
        var lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();
            
            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcAction = "ADD";
            }
            else if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcAction = "EDIT";
            }

            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_NUMBERING";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 4, poNewEntity.CCYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poNewEntity.CPERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@ISTART_NUMBER", DbType.Int32, 8, poNewEntity.ISTART_NUMBER);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or 
                        "@CCYEAR" or
                        "@CPERIOD_NO" or
                        "@CDEPT_CODE" or
                        "@ISTART_NUMBER" or
                        "@CACTION" or
                        "@CUSER_ID"
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

    protected override async Task R_DeletingAsync(GSM05000NumberingGridDTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_DeletingAsync));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;
        var lcAction = "DELETE";

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();
            
            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_NUMBERING";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 50, poEntity.CCYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@ISTART_NUMBER", DbType.Int32, 8, poEntity.ISTART_NUMBER);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or 
                        "@CCYEAR" or
                        "@CPERIOD_NO" or
                        "@CDEPT_CODE" or
                        "@ISTART_NUMBER" or
                        "@CACTION" or
                        "@CUSER_ID"
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

    public async Task<List<GSM05000NumberingGridDTO>> GetNumberingListDbAsync(GSM05000ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetNumberingListDbAsync));
        R_Exception loEx = new();
        List<GSM05000NumberingGridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_NUMBER_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poParameter.CUSER_LOGIN_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANSACTION_CODE" or 
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000NumberingGridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public async Task<GSM05000NumberingHeaderDTO> GetNumberingHeaderDbAsync(GSM05000ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetNumberingHeaderDbAsync));
        R_Exception loEx = new();
        GSM05000NumberingHeaderDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = @"SELECT CTRANS_CODE, 
                        CTRANSACTION_NAME, 
                        LDEPT_MODE, 
                        CPERIOD_MODE, 
                        (CASE WHEN CPERIOD_MODE = 'N' THEN 'None'
                              WHEN CPERIOD_MODE = 'P' THEN 'Periodically'
                              WHEN CPERIOD_MODE = 'Y' THEN 'Yearly' END) AS CPERIOD_MODE_DESCR
                        FROM GSM_TRANSACTION_CODE (NOLOCK)
                        WHERE CCOMPANY_ID = @CCOMPANY_ID
                        AND CTRANS_CODE = @CTRANS_CODE";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTRANS_CODE"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("{pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000NumberingHeaderDTO>(loDataTable).FirstOrDefault();
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
#endregion