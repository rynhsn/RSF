﻿using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_GS_MAINTAIN_TAX_PCTResources;

namespace GSM02000Back;

#region "Not Async Version"

// public class GSM02000TaxCls : R_BusinessObject<GSM02000TaxDTO>
// {
//     Resources_Dummy_Class _resources = new();
//     private readonly ActivitySource _activitySource;
//
//     private LoggerGSM02000 _logger;
//
//     public GSM02000TaxCls()
//     {
//         _logger = LoggerGSM02000.R_GetInstanceLogger();
//         _activitySource =GSM02000Activity.R_GetInstanceActivitySource();
//     }
//     
//     protected override GSM02000TaxDTO R_Display(GSM02000TaxDTO poEntity)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_Display));
//         R_Exception loEx = new();
//         GSM02000TaxDTO loRtn = null;
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
//             lcQuery = "RSP_GS_GET_TAX_PCT_DETAIL";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poEntity.CTAX_DATE);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
//
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTAX_ID" or 
//                         "@CTAX_DATE" or 
//                         "@CUSER_ID"
//                 )
//                 .Select(x => x.Value);
//
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//
//             loRtn = R_Utility.R_ConvertTo<GSM02000TaxDTO>(loDataTable).FirstOrDefault();
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
//     protected override void R_Saving(GSM02000TaxDTO poNewEntity, eCRUDMode poCRUDMode)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_Saving));
//         R_Exception loEx = new();
//         string lcQuery;
//         R_Db loDb;
//         DbCommand loCmd;
//         DbConnection loConn = null;
//         string lcAction = "";
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             R_ExternalException.R_SP_Init_Exception(loConn);
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
//             lcQuery = "RSP_GS_MAINTAIN_TAX_PCT";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.CTAX_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poNewEntity.CTAX_DATE);
//             loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 10, poNewEntity.NTAX_PERCENTAGE);
//             loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
//
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTAX_ID" or 
//                         "@CTAX_DATE" or 
//                         "@NTAX_PERCENTAGE" or 
//                         "@CACTION" or 
//                         "@CUSER_ID"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//
//             try
//             {
//                 loDb.SqlExecNonQuery(loConn, loCmd, false);
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         finally
//         {
//             if (loConn != null)
//             {
//                 if (loConn.State != ConnectionState.Closed)
//                 {
//                     loConn.Close();
//                 }
//
//                 loConn.Dispose();
//             }
//         }
//
//         EndBlock:
//         loEx.ThrowExceptionIfErrors();
//     }
//
//     protected override void R_Deleting(GSM02000TaxDTO poEntity)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_Deleting));
//         R_Exception loEx = new();
//         string lcQuery;
//         R_Db loDb;
//         DbCommand loCmd;
//         DbConnection loConn = null;
//         string lcAction = "DELETE";
//
//         try
//         {
//             loDb = new R_Db();
//             loConn = loDb.GetConnection();
//             loCmd = loDb.GetCommand();
//
//             R_ExternalException.R_SP_Init_Exception(loConn);
//
//             lcQuery = "RSP_GS_MAINTAIN_TAX_PCT";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poEntity.CTAX_DATE);
//             loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 10, poEntity.NTAX_PERCENTAGE);
//             loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
//
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTAX_ID" or 
//                         "@CTAX_DATE" or 
//                         "@NTAX_PERCENTAGE" or 
//                         "@CACTION" or 
//                         "@CUSER_ID"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//
//             try
//             {
//                 loDb.SqlExecNonQuery(loConn, loCmd, false);
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         finally
//         {
//             if (loConn != null)
//             {
//                 if (loConn.State != ConnectionState.Closed)
//                 {
//                     loConn.Close();
//                 }
//
//                 loConn.Dispose();
//             }
//         }
//
//         loEx.ThrowExceptionIfErrors();
//     }
//
//     public List<GSM02000TaxSalesDTO> SalesTaxListDb(GSM02000ParameterDb poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(SalesTaxListDb));
//         R_Exception loEx = new();
//         List<GSM02000TaxSalesDTO> loRtn = null;
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
//             lcQuery = "RSP_GS_GET_TAX_LIST";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
//
//
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CUSER_ID"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//             loRtn = R_Utility.R_ConvertTo<GSM02000TaxSalesDTO>(loDataTable).ToList();
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
//     public List<GSM02000TaxDTO> TaxListDb(GSM02000ParameterDb poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(TaxListDb));
//         R_Exception loEx = new();
//         List<GSM02000TaxDTO> loRtn = null;
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
//             lcQuery = "RSP_GS_GET_TAX_PCT_LIST";
//             loCmd.CommandType = CommandType.StoredProcedure;
//             loCmd.CommandText = lcQuery;
//
//             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poParameter.CTAX_ID);
//             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
//
//             var loDbParam = loCmd.Parameters.Cast<DbParameter>()
//                 .Where(x =>
//                     x.ParameterName is 
//                         "@CCOMPANY_ID" or 
//                         "@CTAX_ID" or 
//                         "@CUSER_ID"
//                 )
//                 .Select(x => x.Value);
//             
//             _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
//
//             var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
//             loRtn = R_Utility.R_ConvertTo<GSM02000TaxDTO>(loDataTable).ToList();
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

public class GSM02000TaxCls : R_BusinessObjectAsync<GSM02000TaxDTO>
{
    Resources_Dummy_Class _resources = new();
    private readonly ActivitySource _activitySource;

    private LoggerGSM02000 _logger;

    public GSM02000TaxCls()
    {
        _logger = LoggerGSM02000.R_GetInstanceLogger();
        _activitySource = GSM02000Activity.R_GetInstanceActivitySource();
    }

    public async Task<List<GSM02000TaxSalesDTO>> SalesTaxListDbAsync(GSM02000ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(SalesTaxListDbAsync));
        R_Exception loEx = new();
        List<GSM02000TaxSalesDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TAX_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000TaxSalesDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public async Task<List<GSM02000TaxDTO>> TaxListDbAsync(GSM02000ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(TaxListDbAsync));
        R_Exception loEx = new();
        List<GSM02000TaxDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TAX_PCT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poParameter.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTAX_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000TaxDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    protected override async Task<GSM02000TaxDTO> R_DisplayAsync(GSM02000TaxDTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_DisplayAsync));
        R_Exception loEx = new();
        GSM02000TaxDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TAX_PCT_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poEntity.CTAX_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTAX_ID" or
                        "@CTAX_DATE" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM02000TaxDTO>(loDataTable).FirstOrDefault();
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

    protected override async Task R_SavingAsync(GSM02000TaxDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_SavingAsync));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcAction = "ADD";
            }
            else if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcAction = "EDIT";
            }

            lcQuery = "RSP_GS_MAINTAIN_TAX_PCT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poNewEntity.CTAX_DATE);
            loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 10, poNewEntity.NTAX_PERCENTAGE);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTAX_ID" or
                        "@CTAX_DATE" or
                        "@NTAX_PERCENTAGE" or
                        "@CACTION" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
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

    protected override async Task R_DeletingAsync(GSM02000TaxDTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_DeletingAsync));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "DELETE";

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GS_MAINTAIN_TAX_PCT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poEntity.CTAX_DATE);
            loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 10, poEntity.NTAX_PERCENTAGE);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTAX_ID" or
                        "@CTAX_DATE" or
                        "@NTAX_PERCENTAGE" or
                        "@CACTION" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
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

        loEx.ThrowExceptionIfErrors();
    }
}

#endregion