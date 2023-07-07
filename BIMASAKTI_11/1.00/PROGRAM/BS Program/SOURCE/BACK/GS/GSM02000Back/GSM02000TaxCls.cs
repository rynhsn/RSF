using System.Data;
using System.Data.Common;
using GSM02000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02000Back;

public class GSM02000TaxCls : R_BusinessObject<GSM02000TaxDTO>
{
    protected override GSM02000TaxDTO R_Display(GSM02000TaxDTO poEntity)
    {
        R_Exception loEx = new R_Exception();
        GSM02000TaxDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_SALES_TAX_PCT_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poEntity.CTAX_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM02000TaxDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    protected override void R_Saving(GSM02000TaxDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loEx = new R_Exception();
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
            
            lcQuery = "RSP_GS_MAINTAIN_SALES_TAX_PCT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poNewEntity.CTAX_DATE);
            loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 10, poNewEntity.NTAX_PERCENTAGE);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

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

    protected override void R_Deleting(GSM02000TaxDTO poEntity)
    {
        R_Exception loEx = new R_Exception();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "DELETE";
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GS_MAINTAIN_SALES_TAX_PCT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 50, poEntity.CTAX_DATE);
            loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.Decimal, 10, poEntity.NTAX_PERCENTAGE);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

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

        loEx.ThrowExceptionIfErrors();
    }

    public List<GSM02000TaxSalesDTO> SalesTaxListDb(GSM02000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        List<GSM02000TaxSalesDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_SALES_TAX_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000TaxSalesDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<GSM02000TaxDTO> TaxListDb(GSM02000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        List<GSM02000TaxDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_SALES_TAX_PCT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poParameter.CTAX_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM02000TaxDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
}