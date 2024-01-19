using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_GS_MAINTAIN_SALES_TAXResources;

namespace GSM02000Back;

public class GSM02000Cls : R_BusinessObject<GSM02000DTO>
{
    Resources_Dummy_Class _resources = new();
    
    private LoggerGSM02000 _logger;
    private readonly ActivitySource _activitySource;
    
    public GSM02000Cls()
    {
        _logger = LoggerGSM02000.R_GetInstanceLogger();
        _activitySource =GSM02000Activity.R_GetInstanceActivitySource();
    }
    
    protected override GSM02000DTO R_Display(GSM02000DTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_Display));
        R_Exception loEx = new();
        GSM02000DTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TAX_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CTAX_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM02000DTO>(loDataTable).FirstOrDefault();
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

    protected override void R_Saving(GSM02000DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_Saving));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
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
            
            lcQuery = "RSP_GS_MAINTAIN_TAX";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 20, poNewEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 100, poNewEntity.CTAX_NAME);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, poNewEntity.CDESCRIPTION);
            // loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 18, poNewEntity.NTAX_PERCENTAGE);
            loDb.R_AddCommandParameter(loCmd, "@CROUNDING_MODE", DbType.String, 10, poNewEntity.CROUNDING_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IROUNDING", DbType.Int32, 10, poNewEntity.IROUNDING);
            loDb.R_AddCommandParameter(loCmd, "@CTAXIN_GLACCOUNT_NO", DbType.String, 20, poNewEntity.CTAXIN_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTAXOUT_GLACCOUNT_NO", DbType.String, 60, poNewEntity.CTAXOUT_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poNewEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
            
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTAX_ID" or 
                        "@CTAX_NAME" or 
                        "@CDESCRIPTION" or 
                        "@CROUNDING_MODE" or 
                        "@IROUNDING" or 
                        "@CTAXIN_GLACCOUNT_NO" or 
                        "@CTAXOUT_GLACCOUNT_NO" or 
                        "@LACTIVE" or 
                        "@CACTION" or 
                        "@CUSER_ID"
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
                _logger.LogError(loEx);
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

    protected override void R_Deleting(GSM02000DTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_Deleting));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GS_MAINTAIN_TAX";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 100, poEntity.CTAX_NAME);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, poEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@CROUNDING_MODE", DbType.String, 10, poEntity.CROUNDING_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IROUNDING", DbType.Int32, 10, poEntity.IROUNDING);
            loDb.R_AddCommandParameter(loCmd, "@CTAXIN_GLACCOUNT_NO", DbType.String, 20, poEntity.CTAXIN_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTAXOUT_GLACCOUNT_NO", DbType.String, 60, poEntity.CTAXOUT_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CACTION" or 
                        "@CCOMPANY_ID" or 
                        "@CTAX_ID" or 
                        "@CTAX_NAME" or 
                        "@LACTIVE" or 
                        "@CDESCRIPTION" or 
                        "@CROUNDING_MODE" or 
                        "@IROUNDING" or 
                        "@CTAXIN_GLACCOUNT_NO" or 
                        "@CTAXOUT_GLACCOUNT_NO" or 
                        "@CUSER_ID"
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
                _logger.LogError(loEx);
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

    public List<GSM02000GridDTO> SalesTaxListDb(GSM02000ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(SalesTaxListDb));
        R_Exception loEx = new();
        List<GSM02000GridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
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
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000GridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);            
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<GSM02000RoundingDTO> RoundingListDb(GSM02000ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(RoundingListDb));
        R_Exception loEx = new();
        List<GSM02000RoundingDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery =
                $"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{poParameter.CCOMPANY_ID}' , '_ROUNDING_MODE', '', '{poParameter.CUSER_LANGUAGE}')";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{poQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000RoundingDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);            
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public void SetActiveInactiveDb(GSM02000ActiveInactiveDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(SetActiveInactiveDb));
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

            lcQuery = "RSP_GS_ACTIVE_INACTIVE_TAX";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 20, poParameter.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1 , poParameter.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CTAX_ID" or 
                        "@LACTIVE" or 
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

        loEx.ThrowExceptionIfErrors();
    }
}