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

// public class GSM05000Cls : R_BusinessObject<GSM05000DTO>
// {
//     private LoggerGSM05000 _logger;
//     private readonly ActivitySource _activitySource;
//
//     public GSM05000Cls()
//     {
//         _logger = LoggerGSM05000.R_GetInstanceLogger();
//         _activitySource =GSM05000Activity.R_GetInstanceActivitySource();
//     }
//
//     protected override GSM05000DTO R_Display(GSM05000DTO poEntity)
//     {
//         using Activity loActivity = _activitySource.StartActivity(nameof(R_Display));
//         R_Exception loEx = new();
//         GSM05000DTO loRtn = null;
//         R_Db loDb;
//         DbConnection loConn;
//         DbCommand loCmd;
//         string lcQuery;
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             lcQuery = @"RSP_GS_GET_TRANS_CODE_INFO";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 30, poEntity.CTRANS_CODE);
//
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTRANS_CODE"
//                 )
//                 .Select(x => x.Value);
//
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//             loRtn = R_Utility.R_ConvertTo<GSM05000DTO>(loDataTable).FirstOrDefault();
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         EndBlock:
//         loEx.ThrowExceptionIfErrors();
//
//         return loRtn;
//     }
//
//     protected override void R_Deleting(GSM05000DTO poEntity)
//     {
//         throw new NotImplementedException();
//     }
//
//     protected override void R_Saving(GSM05000DTO poNewEntity, eCRUDMode poCRUDMode)
//     {
//         using Activity loActivity = _activitySource.StartActivity(nameof(R_Saving));
//         R_Exception loEx = new();
//         string lcQuery;
//         R_Db loDb;
//         DbCommand loCmd;
//         DbConnection loConn;
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             if (poCRUDMode == eCRUDMode.EditMode)
//             {
//                 lcQuery = @"UPDATE GSM_TRANSACTION_CODE SET 
//                         CTRANS_SHORT_NAME = @CTRANS_SHORT_NAME 
//                       , LINCREMENT_FLAG = @LINCREMENT_FLAG 
//                       , LDEPT_MODE = @LDEPT_MODE 
//                       , CDEPT_DELIMITER = @CDEPT_DELIMITER 
//                       , LTRANSACTION_MODE = @LTRANSACTION_MODE 
//                       , CTRANSACTION_DELIMITER = @CTRANSACTION_DELIMITER 
//                       , CPERIOD_MODE = @CPERIOD_MODE 
//                       , CYEAR_FORMAT = @CYEAR_FORMAT 
//                       , CPERIOD_DELIMITER = @CPERIOD_DELIMITER 
//                       , INUMBER_LENGTH = @INUMBER_LENGTH 
//                       , CNUMBER_DELIMITER = @CNUMBER_DELIMITER 
//                       , CPREFIX = @CPREFIX 
//                       , CPREFIX_DELIMITER = @CPREFIX_DELIMITER 
//                       , CSUFFIX = @CSUFFIX 
//                       , CSEQUENCE01 = @CSEQUENCE01 
//                       , CSEQUENCE02 = @CSEQUENCE02 
//                       , CSEQUENCE03 = @CSEQUENCE03 
//                       , CSEQUENCE04 = @CSEQUENCE04 
//                       , LAPPROVAL_FLAG = @LAPPROVAL_FLAG 
//                       , LUSE_THIRD_PARTY = @LUSE_THIRD_PARTY 
//                       , CAPPROVAL_MODE = @CAPPROVAL_MODE 
//                       , LAPPROVAL_DEPT = @LAPPROVAL_DEPT 
//                       , CUPDATE_BY = @CUPDATE_BY 
//                       , DUPDATE_DATE = @DUPDATE_DATE
//                         WHERE CCOMPANY_ID = @CCOMPANY_ID AND CTRANS_CODE = @CTRANS_CODE";
//
//                 loCmd.CommandType = CommandType.Text;
//                 loCmd.CommandText = lcQuery;
//
//                 loDb.R_AddCommandParameter(loCmd, "@CTRANS_SHORT_NAME", DbType.String, 20, poNewEntity.CTRANS_SHORT_NAME);
//                 loDb.R_AddCommandParameter(loCmd, "@LINCREMENT_FLAG", DbType.Boolean, 1, poNewEntity.LINCREMENT_FLAG);
//                 loDb.R_AddCommandParameter(loCmd, "@LDEPT_MODE", DbType.Boolean, 1, poNewEntity.LDEPT_MODE);
//                 loDb.R_AddCommandParameter(loCmd, "@CDEPT_DELIMITER", DbType.String, 1, poNewEntity.CDEPT_DELIMITER);
//                 loDb.R_AddCommandParameter(loCmd, "@LTRANSACTION_MODE", DbType.Boolean, 1,
//                     poNewEntity.LTRANSACTION_MODE);
//                 loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DELIMITER", DbType.String, 1,
//                     poNewEntity.CTRANSACTION_DELIMITER);
//                 loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MODE", DbType.String, 1, poNewEntity.CPERIOD_MODE);
//                 loDb.R_AddCommandParameter(loCmd, "@CYEAR_FORMAT", DbType.String, 4, poNewEntity.CYEAR_FORMAT);
//                 loDb.R_AddCommandParameter(loCmd, "@CPERIOD_DELIMITER", DbType.String, 1,
//                     poNewEntity.CPERIOD_DELIMITER);
//                 loDb.R_AddCommandParameter(loCmd, "@INUMBER_LENGTH", DbType.Int32, 4, poNewEntity.INUMBER_LENGTH);
//                 loDb.R_AddCommandParameter(loCmd, "@CNUMBER_DELIMITER", DbType.String, 1,
//                     poNewEntity.CNUMBER_DELIMITER);
//                 loDb.R_AddCommandParameter(loCmd, "@CPREFIX", DbType.String, 10, poNewEntity.CPREFIX);
//                 loDb.R_AddCommandParameter(loCmd, "@CPREFIX_DELIMITER", DbType.String, 1,
//                     poNewEntity.CPREFIX_DELIMITER);
//                 loDb.R_AddCommandParameter(loCmd, "@CSUFFIX", DbType.String, 10, poNewEntity.CSUFFIX);
//                 loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE01", DbType.String, 10, poNewEntity.CSEQUENCE01);
//                 loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE02", DbType.String, 10, poNewEntity.CSEQUENCE02);
//                 loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE03", DbType.String, 10, poNewEntity.CSEQUENCE03);
//                 loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE04", DbType.String, 10, poNewEntity.CSEQUENCE04);
//                 loDb.R_AddCommandParameter(loCmd, "@LAPPROVAL_FLAG", DbType.Boolean, 1, poNewEntity.LAPPROVAL_FLAG);
//                 loDb.R_AddCommandParameter(loCmd, "@LUSE_THIRD_PARTY", DbType.Boolean, 1, poNewEntity.LUSE_THIRD_PARTY);
//                 loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_MODE", DbType.String, 1, poNewEntity.CAPPROVAL_MODE);
//                 loDb.R_AddCommandParameter(loCmd, "@LAPPROVAL_DEPT", DbType.Boolean, 1, poNewEntity.LAPPROVAL_DEPT);
//                 loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 10, poNewEntity.CUPDATE_BY);
//                 loDb.R_AddCommandParameter(loCmd, "@DUPDATE_DATE", DbType.DateTime, 8, poNewEntity.DUPDATE_DATE);
//                 loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
//                 loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10,
//                     poNewEntity.CTRANS_CODE);
//
//
//                 var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                     .Where(x =>
//                         x.ParameterName is
//                             "@CTRANS_SHORT_NAME" or
//                             "@LINCREMENT_FLAG" or
//                             "@LDEPT_MODE" or
//                             "@CDEPT_DELIMITER" or
//                             "@LTRANSACTION_MODE" or
//                             "@CTRANSACTION_DELIMITER" or
//                             "@CPERIOD_MODE" or
//                             "@CYEAR_FORMAT" or
//                             "@CPERIOD_DELIMITER" or
//                             "@INUMBER_LENGTH" or
//                             "@CNUMBER_DELIMITER" or
//                             "@CPREFIX" or
//                             "@CPREFIX_DELIMITER" or
//                             "@CSUFFIX" or
//                             "@CSEQUENCE01" or
//                             "@CSEQUENCE02" or
//                             "@CSEQUENCE03" or
//                             "@CSEQUENCE04" or
//                             "@LAPPROVAL_FLAG" or
//                             "@LUSE_THIRD_PARTY" or
//                             "@CAPPROVAL_MODE" or
//                             "@LAPPROVAL_DEPT" or
//                             "@CUPDATE_BY" or
//                             "@DUPDATE_DATE" or
//                             "@CCOMPANY_ID" or
//                             "@CTRANS_CODE"
//                     )
//                     .Select(x => x.Value);
//
//                 _logger.LogDebug("{pcQuery} {@poParam}", lcQuery, loDbParam);
//
//                 loDb.SqlExecNonQuery(loConn, loCmd, true);
//             }
//             else
//             {
//                 loEx.Add("403", "Mode is not allowed");
//             }
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
//     //method get semua data
//     public List<GSM05000GridDTO> GetTransactionCodeListDb(GSM05000ParameterDb poParameterDb)
//     {
//         using Activity loActivity = _activitySource.StartActivity(nameof(GetTransactionCodeListDb));
//         R_Exception loEx = new();
//         List<GSM05000GridDTO> loRtn = null;
//         R_Db loDb;
//         DbConnection loConn;
//         DbCommand loCmd;
//         string lcQuery;
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             lcQuery = $"EXEC RSP_GS_GET_TRANS_CODE_LIST '{poParameterDb.CCOMPANY_ID}', '{poParameterDb.CUSER_ID}'";
//             loCmd.CommandType = CommandType.Text;
//             loCmd.CommandText = lcQuery;
//             
//             _logger.LogDebug("{pcQuery}", lcQuery);
//             
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//
//             loRtn = R_Utility.R_ConvertTo<GSM05000GridDTO>(loDataTable).ToList();
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         EndBlock:
//         loEx.ThrowExceptionIfErrors();
//
//         return loRtn;
//     }
//
//
//     public GSM05000ExistDTO GetValidateUpdateDb(GSM05000ParameterDb poEntity)
//     {
//         using Activity loActivity = _activitySource.StartActivity(nameof(GetValidateUpdateDb));
//         R_Exception loEx = new();
//         GSM05000ExistDTO loRtn = null;
//         R_Db loDb;
//         DbConnection loConn;
//         DbCommand loCmd;
//         string lcQuery = null;
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             if (poEntity.ETAB_NAME == GSM05000eTabName.Numbering)
//             {
//                 lcQuery = @"select top 1 1 as EXIST 
//                           from GSM_TRANSACTION_NUMBER (nolock) 
//                          where CCOMPANY_ID = @CCOMPANY_ID 
//                            and CTRANS_CODE = @CTRANS_CODE";
//             }
//             else if (poEntity.ETAB_NAME == GSM05000eTabName.Approval)
//             {
//                 lcQuery = @"select top 1 1 as EXIST 
//                           from GSM_TRANSACTION_APPROVAL (nolock) 
//                          where CCOMPANY_ID = @CCOMPANY_ID 
//                            and CTRANS_CODE = @CTRANS_CODE";
//             }
//
//             loCmd.CommandType = CommandType.Text;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 30, poEntity.CTRANS_CODE);
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
//
//             loRtn = R_Utility.R_ConvertTo<GSM05000ExistDTO>(loDataTable).FirstOrDefault();
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         EndBlock:
//         loEx.ThrowExceptionIfErrors();
//
//         return loRtn;
//     }
//
//     public List<GSM05000DelimiterDTO> GetDelimiterListDb(GSM05000ParameterDb poParameterDb)
//     {
//         using Activity loActivity = _activitySource.StartActivity(nameof(GetDelimiterListDb));
//         R_Exception loEx = new();
//         List<GSM05000DelimiterDTO> loRtn = null;
//         R_Db loDb;
//         DbConnection loConn;
//         DbCommand loCmd;
//         string lcQuery;
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             lcQuery =
//                 @$"SELECT * FROM RFT_GET_GSB_CODE_INFO ('SIAPP', '{poParameterDb.CCOMPANY_ID}', '_GS_REFNO_DELIMITER', '', '{poParameterDb.CLANGUAGE_ID}')";
//             loCmd.CommandType = CommandType.Text;
//             loCmd.CommandText = lcQuery;
//
//             _logger.LogDebug("{pcQuery}", lcQuery);
//
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//
//             loRtn = R_Utility.R_ConvertTo<GSM05000DelimiterDTO>(loDataTable).ToList();
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         EndBlock:
//         loEx.ThrowExceptionIfErrors();
//
//         return loRtn;
//     }
// }

#endregion

#region "Async Version"

public class GSM05000Cls : R_BusinessObjectAsync<GSM05000DTO>
{
    private LoggerGSM05000 _logger;
    private readonly ActivitySource _activitySource;

    public GSM05000Cls()
    {
        _logger = LoggerGSM05000.R_GetInstanceLogger();
        _activitySource = GSM05000Activity.R_GetInstanceActivitySource();
    }

    //method get semua data
    public async Task<List<GSM05000GridDTO>> GetTransactionCodeListDbAsync(GSM05000ParameterDb poParameterDb)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetTransactionCodeListDbAsync));
        R_Exception loEx = new();
        List<GSM05000GridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_TRANS_CODE_LIST '{poParameterDb.CCOMPANY_ID}', '{poParameterDb.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000GridDTO>(loDataTable).ToList();
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


    public async Task<GSM05000ExistDTO> GetValidateUpdateDbAsync(GSM05000ParameterDb poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetValidateUpdateDbAsync));
        R_Exception loEx = new();
        GSM05000ExistDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery = null;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            if (poEntity.ETAB_NAME == GSM05000eTabName.Numbering)
            {
                lcQuery = @"select top 1 1 as EXIST 
                          from GSM_TRANSACTION_NUMBER (nolock) 
                         where CCOMPANY_ID = @CCOMPANY_ID 
                           and CTRANS_CODE = @CTRANS_CODE";
            }
            else if (poEntity.ETAB_NAME == GSM05000eTabName.Approval)
            {
                lcQuery = @"select top 1 1 as EXIST 
                          from GSM_TRANSACTION_APPROVAL (nolock) 
                         where CCOMPANY_ID = @CCOMPANY_ID 
                           and CTRANS_CODE = @CTRANS_CODE";
            }

            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 30, poEntity.CTRANS_CODE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTRANS_CODE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("{pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000ExistDTO>(loDataTable).FirstOrDefault();
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

    public async Task<List<GSM05000DelimiterDTO>> GetDelimiterListDbAsync(GSM05000ParameterDb poParameterDb)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDelimiterListDbAsync));
        R_Exception loEx = new();
        List<GSM05000DelimiterDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery =
                @$"SELECT * FROM RFT_GET_GSB_CODE_INFO ('SIAPP', '{poParameterDb.CCOMPANY_ID}', '_GS_REFNO_DELIMITER', '', '{poParameterDb.CLANGUAGE_ID}')";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000DelimiterDTO>(loDataTable).ToList();
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

    protected override async Task<GSM05000DTO> R_DisplayAsync(GSM05000DTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_DisplayAsync));
        R_Exception loEx = new();
        GSM05000DTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_GS_GET_TRANS_CODE_INFO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 30, poEntity.CTRANS_CODE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTRANS_CODE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000DTO>(loDataTable).FirstOrDefault();
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

    protected override async Task R_SavingAsync(GSM05000DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_SavingAsync));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcQuery = @"UPDATE GSM_TRANSACTION_CODE SET 
                        CTRANS_SHORT_NAME = @CTRANS_SHORT_NAME 
                      , LINCREMENT_FLAG = @LINCREMENT_FLAG 
                      , LDEPT_MODE = @LDEPT_MODE 
                      , CDEPT_DELIMITER = @CDEPT_DELIMITER 
                      , LTRANSACTION_MODE = @LTRANSACTION_MODE 
                      , CTRANSACTION_DELIMITER = @CTRANSACTION_DELIMITER 
                      , CPERIOD_MODE = @CPERIOD_MODE 
                      , CYEAR_FORMAT = @CYEAR_FORMAT 
                      , CPERIOD_DELIMITER = @CPERIOD_DELIMITER 
                      , INUMBER_LENGTH = @INUMBER_LENGTH 
                      , CNUMBER_DELIMITER = @CNUMBER_DELIMITER 
                      , CPREFIX = @CPREFIX 
                      , CPREFIX_DELIMITER = @CPREFIX_DELIMITER 
                      , CSUFFIX = @CSUFFIX 
                      , CSEQUENCE01 = @CSEQUENCE01 
                      , CSEQUENCE02 = @CSEQUENCE02 
                      , CSEQUENCE03 = @CSEQUENCE03 
                      , CSEQUENCE04 = @CSEQUENCE04 
                      , LAPPROVAL_FLAG = @LAPPROVAL_FLAG 
                      , LUSE_THIRD_PARTY = @LUSE_THIRD_PARTY 
                      , CAPPROVAL_MODE = @CAPPROVAL_MODE 
                      , LAPPROVAL_DEPT = @LAPPROVAL_DEPT 
                      , CUPDATE_BY = @CUPDATE_BY 
                      , DUPDATE_DATE = @DUPDATE_DATE
                        WHERE CCOMPANY_ID = @CCOMPANY_ID AND CTRANS_CODE = @CTRANS_CODE";

                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CTRANS_SHORT_NAME", DbType.String, 20,
                    poNewEntity.CTRANS_SHORT_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LINCREMENT_FLAG", DbType.Boolean, 1, poNewEntity.LINCREMENT_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@LDEPT_MODE", DbType.Boolean, 1, poNewEntity.LDEPT_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_DELIMITER", DbType.String, 1, poNewEntity.CDEPT_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@LTRANSACTION_MODE", DbType.Boolean, 1,
                    poNewEntity.LTRANSACTION_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DELIMITER", DbType.String, 1,
                    poNewEntity.CTRANSACTION_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MODE", DbType.String, 1, poNewEntity.CPERIOD_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR_FORMAT", DbType.String, 4, poNewEntity.CYEAR_FORMAT);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_DELIMITER", DbType.String, 1,
                    poNewEntity.CPERIOD_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@INUMBER_LENGTH", DbType.Int32, 4, poNewEntity.INUMBER_LENGTH);
                loDb.R_AddCommandParameter(loCmd, "@CNUMBER_DELIMITER", DbType.String, 1,
                    poNewEntity.CNUMBER_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@CPREFIX", DbType.String, 10, poNewEntity.CPREFIX);
                loDb.R_AddCommandParameter(loCmd, "@CPREFIX_DELIMITER", DbType.String, 1,
                    poNewEntity.CPREFIX_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@CSUFFIX", DbType.String, 10, poNewEntity.CSUFFIX);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE01", DbType.String, 10, poNewEntity.CSEQUENCE01);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE02", DbType.String, 10, poNewEntity.CSEQUENCE02);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE03", DbType.String, 10, poNewEntity.CSEQUENCE03);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE04", DbType.String, 10, poNewEntity.CSEQUENCE04);
                loDb.R_AddCommandParameter(loCmd, "@LAPPROVAL_FLAG", DbType.Boolean, 1, poNewEntity.LAPPROVAL_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_THIRD_PARTY", DbType.Boolean, 1, poNewEntity.LUSE_THIRD_PARTY);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_MODE", DbType.String, 1, poNewEntity.CAPPROVAL_MODE);
                loDb.R_AddCommandParameter(loCmd, "@LAPPROVAL_DEPT", DbType.Boolean, 1, poNewEntity.LAPPROVAL_DEPT);
                loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 10, poNewEntity.CUPDATE_BY);
                loDb.R_AddCommandParameter(loCmd, "@DUPDATE_DATE", DbType.DateTime, 8, poNewEntity.DUPDATE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10,
                    poNewEntity.CTRANS_CODE);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CTRANS_SHORT_NAME" or
                            "@LINCREMENT_FLAG" or
                            "@LDEPT_MODE" or
                            "@CDEPT_DELIMITER" or
                            "@LTRANSACTION_MODE" or
                            "@CTRANSACTION_DELIMITER" or
                            "@CPERIOD_MODE" or
                            "@CYEAR_FORMAT" or
                            "@CPERIOD_DELIMITER" or
                            "@INUMBER_LENGTH" or
                            "@CNUMBER_DELIMITER" or
                            "@CPREFIX" or
                            "@CPREFIX_DELIMITER" or
                            "@CSUFFIX" or
                            "@CSEQUENCE01" or
                            "@CSEQUENCE02" or
                            "@CSEQUENCE03" or
                            "@CSEQUENCE04" or
                            "@LAPPROVAL_FLAG" or
                            "@LUSE_THIRD_PARTY" or
                            "@CAPPROVAL_MODE" or
                            "@LAPPROVAL_DEPT" or
                            "@CUPDATE_BY" or
                            "@DUPDATE_DATE" or
                            "@CCOMPANY_ID" or
                            "@CTRANS_CODE"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("{pcQuery} {@poParam}", lcQuery, loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            else
            {
                loEx.Add("403", "Mode is not allowed");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    protected override async Task R_DeletingAsync(GSM05000DTO poEntity)
    {
        throw new NotImplementedException();
    }
}

#endregion