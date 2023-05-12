using System.Data;
using System.Data.Common;
using GSM02000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02000Back;

public class GSM02000Cls : R_BusinessObject<GSM02000DTO>
{
    protected override GSM02000DTO R_Display(GSM02000DTO poEntity)
    {
        R_Exception loEx = new R_Exception();
        GSM02000DTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            // loConn = loDb.GetConnection("BimasaktiConnectionString");
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_SALES_TAX_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM02000DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    protected override void R_Saving(GSM02000DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loEx = new R_Exception();
        string lcQuery = null;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";
        
        try
        {
            loDb = new R_Db();
            // loConn = loDb.GetConnection("BimasaktiConnectionString");
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
            
            lcQuery = "RSP_GS_MAINTAIN_SALES_TAX";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 10, poNewEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 10, poNewEntity.CTAX_NAME);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poNewEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 18, poNewEntity.NTAX_PERCENTAGE);
            loDb.R_AddCommandParameter(loCmd, "@CROUNDING_MODE", DbType.String, 10, poNewEntity.CROUNDING_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IROUNDING", DbType.Int32, 10, poNewEntity.IROUNDING);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poNewEntity.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);

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
        R_Exception loEx = new R_Exception();
        string lcQuery = null;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";
        
        try
        {
            loDb = new R_Db();
            // loConn = loDb.GetConnection("BimasaktiConnectionString");
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_MAINTAIN_SALES_TAX";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 10, poEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 10, poEntity.CTAX_NAME);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 18, poEntity.NTAX_PERCENTAGE);
            loDb.R_AddCommandParameter(loCmd, "@CROUNDING_MODE", DbType.String, 10, poEntity.CROUNDING_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IROUNDING", DbType.Int32, 10, poEntity.IROUNDING);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poEntity.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);

            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public List<GSM02000GridDTO> SalesTaxListDb(GSM02000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        List<GSM02000GridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd;
        string lcQuery = null;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection("BimasaktiConnectionString");
            loCmd = loDb.GetCommand();

            // lcQuery = "EXEC RSP_GS_GET_SALES_TAX_LIST @CCOMPANY_ID, @CUSER_ID";
            lcQuery = "RSP_GS_GET_SALES_TAX_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000GridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<GSM02000RoundingDTO> RoundingListDb(GSM02000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        List<GSM02000RoundingDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd;
        string lcQuery = null;

        try
        {
            loDb = new R_Db();
            // loConn = loDb.GetConnection("BimasaktiConnectionString");
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery =
                $"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{poParameter.CCOMPANY_ID}' , '_ROUNDING_MODE', '', '{poParameter.CCULTURE}')";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000RoundingDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}