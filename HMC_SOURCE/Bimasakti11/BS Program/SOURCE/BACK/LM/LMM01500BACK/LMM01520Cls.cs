using LMM01500COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Input;

namespace LMM01500BACK
{
    public class LMM01520Cls : R_BusinessObject<LMM01520DTO>
    {
        private LoggerLMM01520 _Logger;

        public LMM01520Cls()
        {
            _Logger = LoggerLMM01520.R_GetInstanceLogger();
        }

        public List<LMM01522DTO> GetAllAdditionalId(LMM01522DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01522DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT CCHARGES_ID, CCHARGES_NAME FROM GSM_OTHER_CHARGES (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    "AND CPROPERTY_ID =  @CPROPERTY_ID " +
                    "AND CCHARGES_TYPE = 'A' " +
                    "AND CSTATUS = '80'";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01522DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(LMM01520DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override LMM01520DTO R_Display(LMM01520DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01520DTO loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_INVOICE_GROUP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_INVOICE_GROUP {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<LMM01520DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM01520DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_MAINTAIN_INVGRP_PENALTY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;


                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LPENALTY", DbType.Boolean, 50, poNewEntity.LPENALTY);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_ADD_ID", DbType.String, 50, poNewEntity.CPENALTY_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_TYPE", DbType.String, 50, poNewEntity.CPENALTY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NPENALTY_TYPE_VALUE", DbType.Int32, 50, poNewEntity.NPENALTY_TYPE_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_TYPE_CALC_BASEON", DbType.String, 50, poNewEntity.CPENALTY_TYPE_CALC_BASEON);
                loDb.R_AddCommandParameter(loCmd, "@IROUNDED", DbType.Int32, 50, poNewEntity.IROUNDED);
                loDb.R_AddCommandParameter(loCmd, "@CCUTOFDATE_BY", DbType.String, 50, poNewEntity.CCUTOFDATE_BY);
                loDb.R_AddCommandParameter(loCmd, "@IGRACE_PERIOD", DbType.Int32, 50, poNewEntity.IGRACE_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_FEE_START_FROM", DbType.String, 50, poNewEntity.CPENALTY_FEE_START_FROM);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_HOLIDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_SATURDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_SUNDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@NMIN_PENALTY_AMOUNT", DbType.String, 50, poNewEntity.NMIN_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NMAX_PENALTY_AMOUNT", DbType.String, 50, poNewEntity.NMAX_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_INVGRP_PENALTY {@poParameter}", loDbParam);

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
                _Logger.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
