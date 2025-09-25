using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLM00300Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00300Back
{
    public class GLM00300Cls :R_BusinessObject<GLM00300DTO>
    {
        protected override GLM00300DTO R_Display(GLM00300DTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00300DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "EXEC RSP_GL_GET_BUDGET_WEIGHTING @CCOMPANY_ID,@CREC_ID, @CLANGUAGE_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 100, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poEntity.CLANGUAGE_ID);
 

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLM00300DTO>(loDataTable).FirstOrDefault();
                loResult.CREC_ID = poEntity.CREC_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GLM00300DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            var loDb = new R_Db();
            DbCommand loCmd;
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            string lcAction = "";
            string lcQuery = "";

            try
            {
                loCmd = loDb.GetCommand();
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "NEW";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                lcQuery = "RSP_GL_SAVE_BUDGET_WEIGHTING";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "CUSER_ID", DbType.String, 20, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "CACTION", DbType.String, 20, lcAction);
                loDb.R_AddCommandParameter(loCmd, "CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "CCOMPANY_ID", DbType.String, 20, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "CBW_CODE", DbType.String, 20, poNewEntity.CBW_CODE);
                loDb.R_AddCommandParameter(loCmd, "CBW_NAME", DbType.String, 50, poNewEntity.CBW_NAME);

                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT01", DbType.Decimal, 20, poNewEntity.NL_AMOUNT01);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT02", DbType.Decimal, 20, poNewEntity.NL_AMOUNT02);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT03", DbType.Decimal, 20, poNewEntity.NL_AMOUNT03);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT04", DbType.Decimal, 20, poNewEntity.NL_AMOUNT04);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT05", DbType.Decimal, 20, poNewEntity.NL_AMOUNT05);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT06", DbType.Decimal, 20, poNewEntity.NL_AMOUNT06);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT07", DbType.Decimal, 20, poNewEntity.NL_AMOUNT07);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT08", DbType.Decimal, 20, poNewEntity.NL_AMOUNT08);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT09", DbType.Decimal, 20, poNewEntity.NL_AMOUNT09);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT10", DbType.Decimal, 20, poNewEntity.NL_AMOUNT10);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT11", DbType.Decimal, 20, poNewEntity.NL_AMOUNT11);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT12", DbType.Decimal, 20, poNewEntity.NL_AMOUNT12);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT13", DbType.Decimal, 20, poNewEntity.NL_AMOUNT13);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT14", DbType.Decimal, 20, poNewEntity.NL_AMOUNT14);
                loDb.R_AddCommandParameter(loCmd, "NL_AMOUNT15", DbType.Decimal, 20, poNewEntity.NL_AMOUNT15);

                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT01", DbType.Decimal, 20, poNewEntity.NB_AMOUNT01);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT02", DbType.Decimal, 20, poNewEntity.NB_AMOUNT02);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT03", DbType.Decimal, 20, poNewEntity.NB_AMOUNT03);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT04", DbType.Decimal, 20, poNewEntity.NB_AMOUNT04);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT05", DbType.Decimal, 20, poNewEntity.NB_AMOUNT05);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT06", DbType.Decimal, 20, poNewEntity.NB_AMOUNT06);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT07", DbType.Decimal, 20, poNewEntity.NB_AMOUNT07);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT08", DbType.Decimal, 20, poNewEntity.NB_AMOUNT08);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT09", DbType.Decimal, 20, poNewEntity.NB_AMOUNT09);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT10", DbType.Decimal, 20, poNewEntity.NB_AMOUNT10);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT11", DbType.Decimal, 20, poNewEntity.NB_AMOUNT11);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT12", DbType.Decimal, 20, poNewEntity.NB_AMOUNT12);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT13", DbType.Decimal, 20, poNewEntity.NB_AMOUNT13);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT14", DbType.Decimal, 20, poNewEntity.NB_AMOUNT14);
                loDb.R_AddCommandParameter(loCmd, "NB_AMOUNT15", DbType.Decimal, 20, poNewEntity.NB_AMOUNT15);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //loDb.SqlExecNonQuery(loConn, loCmd, false);
                    
                   var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    var loResult = R_Utility.R_ConvertTo<GLM00300DTO>(loDataTable).FirstOrDefault();
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
            loEx.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(GLM00300DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = "DELETE";

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"RSP_GL_DELETE_BUDGET_WEIGHTING '{poEntity.CREC_ID}'";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public List<GLM00300DTO> GetBudgetWeightingList(GLM00300ParameterDB poParameter)
        {
            var loEx = new R_Exception();
            List<GLM00300DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                loCmd = loDb.GetCommand();
                var lcQuery = "RSP_GL_GET_BUDGET_WEIGHTING_LIST @CCOMPANY_ID, @CLANGUAGE_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 20, poParameter.CLANGUAGE_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLM00300DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<CurrencyCodeDTO> GetCurrencyList(GLM00300ParameterDB poParameter)
        {
            var loEx = new R_Exception();
            List<CurrencyCodeDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CBASE_CURRENCY_CODE,CLOCAL_CURRENCY_CODE FROM GSM_COMPANY (NOLOCK) WHERE CCOMPANY_ID = '{poParameter.CCOMPANY_ID}'";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CurrencyCodeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
