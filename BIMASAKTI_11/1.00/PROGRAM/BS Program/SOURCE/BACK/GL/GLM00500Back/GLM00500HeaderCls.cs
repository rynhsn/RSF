using System.Data;
using System.Data.Common;
using GLM00500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00500Back;

public class GLM00500HeaderCls : R_BusinessObject<GLM00500BudgetHDDTO>
{
    protected override GLM00500BudgetHDDTO R_Display(GLM00500BudgetHDDTO poEntity)
    {
        var loEx = new R_Exception();
        GLM00500BudgetHDDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_HD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 255, poEntity.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500BudgetHDDTO>(DataTable).FirstOrDefault();
            loReturn.CREC_ID = poEntity.CREC_ID;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Deleting(GLM00500BudgetHDDTO poEntity)
    {
        R_Exception loEx = new R_Exception();
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcQuery;
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GL_DELETE_BUDGET";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 255, poEntity.CREC_ID);

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

    protected override void R_Saving(GLM00500BudgetHDDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loEx = new R_Exception();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcRecId = "";
        string lcAction = "";
        bool llFinal = false; 
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcRecId = "";
                lcAction = "NEW";
            }
            else if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcRecId = poNewEntity.CREC_ID;
                lcAction = "EDIT";
                llFinal = poNewEntity.LFINAL;
            }
            
            lcQuery = "RSP_GL_SAVE_BUDGET_HD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, lcRecId);
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NO", DbType.String, 255, poNewEntity.CBUDGET_NO);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 255, poNewEntity.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NAME", DbType.String, 255, poNewEntity.CBUDGET_NAME);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 255, poNewEntity.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@LFINAL", DbType.Boolean, 1, llFinal);


            try
            {
                // loDb.SqlExecNonQuery(loConn, loCmd, false);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loResult = R_Utility.R_ConvertTo<GLM00500BudgetHDDTO>(loDataTable).FirstOrDefault();
                poNewEntity.CREC_ID = loResult.CREC_ID;
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
    
    public void GLM00500FinalizeBudgetDb(GLM00500ParameterDb poParams)
    {
        R_Exception loEx = new R_Exception();
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcQuery;
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GL_FINALIZE_BUDGET";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 255, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 255, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 255, poParams.CREC_ID);

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

    public List<GLM00500BudgetHDDTO> GLM00500GetBudgetHDListDb(GLM00500ParameterDb poParams)
    {
        R_Exception loEx = new R_Exception();
        List<GLM00500BudgetHDDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_HD_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poParams.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParams.CLANGUAGE_ID);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500BudgetHDDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLM00500GSMPeriodDTO GLM00500GetPeriodsDb(GLM00500ParameterDb poParams)
    {
        R_Exception loEx = new R_Exception();
        GLM00500GSMPeriodDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"SELECT IMIN_YEAR=CAST(MIN(CYEAR) AS INT), IMAX_YEAR=CAST(MAX(CYEAR) AS INT)
                        FROM GSM_PERIOD (NOLOCK)
                        WHERE CCOMPANY_ID = '{poParams.CCOMPANY_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;


            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500GSMPeriodDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLM00500GLSystemParamDTO GLM00500GetSystemParamDb(GLM00500ParameterDb poParams)
    {
        R_Exception loEx = new R_Exception();
        GLM00500GLSystemParamDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParams.CLANGUAGE_ID);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500GLSystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLM00500FunctionDTO> GLM00500GetCurrencyTypeListDb(GLM00500ParameterDb poParams)
    {
        R_Exception loEx = new R_Exception();
        List<GLM00500FunctionDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParams.CCOMPANY_ID}', '_CURRENCY_TYPE', '', '{poParams.CLANGUAGE_ID}')";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500FunctionDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
}