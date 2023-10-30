using System.Data;
using System.Data.Common;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02000Back;

public class GSM02000Cls : R_BusinessObject<GSM02000DTO>
{
    private LoggerGSM02000 _logger;
    
    public GSM02000Cls()
    {
        _logger = LoggerGSM02000.R_GetInstanceLogger();
    }
    
    protected override GSM02000DTO R_Display(GSM02000DTO poEntity)
    {
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
                .Where(x => x.ParameterName == "@"+ poEntity.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
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

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.CTAX_ID);
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
                .Where(x => x.ParameterName == "@"+ poNewEntity.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
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
            loDb.R_AddCommandParameter(loCmd, "@CTAXIN_GL_ACCOUNT_NO", DbType.String, 20, poEntity.CTAXIN_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTAXOUT_GL_ACCOUNT_NO", DbType.String, 60, poEntity.CTAXOUT_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@"+ poEntity.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
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
                .Where(x => x.ParameterName == "@"+ poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
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
                .Where(x => x.ParameterName == "@"+ poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
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