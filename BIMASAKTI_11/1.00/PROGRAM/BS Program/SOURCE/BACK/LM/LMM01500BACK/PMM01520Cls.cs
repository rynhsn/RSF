using PMM01500COMMON;
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
using System.Diagnostics;
using System.Transactions;

namespace PMM01500BACK
{
    public class PMM01520Cls 
    {
        private LoggerPMM01520 _Logger;
        private readonly ActivitySource _activitySource;

        public PMM01520Cls()
        {
            _Logger = LoggerPMM01520.R_GetInstanceLogger();
            _activitySource = PMM01520ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMM01520DTO> GetAllPinaltyDate(PMM01520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllPinaltyDate");
            var loEx = new R_Exception();
            List<PMM01520DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_INVGRP_PENALTY_DATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_INVGRP_PENALTY_DATE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01520DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMM01520DTO GetPinaltyDateSP(PMM01520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPinaltyDateSP");
            var loEx = new R_Exception();
            PMM01520DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_INVGRP_PENALTY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_DATE", DbType.String, 50, poEntity.CPENALTY_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_INVGRP_PENALTY {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01520DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMM01520DTO GetPinaltySP(PMM01520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPinaltySP");
            var loEx = new R_Exception();
            PMM01520DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_INVOICE_GROUP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_INVOICE_GROUP {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01520DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMM01520DTO SaveDeletePinalty(PMM01520DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SavePinalty");
            var loEx = new R_Exception();
            PMM01520DTO loRtn = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    SavingDeletePinaltySP(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

                if (poCRUDMode == eCRUDMode.AddMode || poCRUDMode == eCRUDMode.EditMode)
                {
                    loRtn = GetPinaltyDateSP(poNewEntity);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public void SavingDeletePinaltySP(PMM01520DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SavingDeletePinaltySP");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_MAINTAIN_INVGRP_PENALTY";
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
                else if (poCRUDMode == eCRUDMode.DeleteMode)
                {
                    poNewEntity.CACTION = "DELETE";
                }

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 20, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_DATE", DbType.String, 8, poNewEntity.CPENALTY_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LPENALTY", DbType.Boolean, 50, poNewEntity.LPENALTY);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_CHARGES_ID", DbType.String, 20, poNewEntity.CPENALTY_UNIT_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_ADD_ID", DbType.String, 20, poNewEntity.CPENALTY_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_TYPE", DbType.String, 2, poNewEntity.CPENALTY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NPENALTY_TYPE_VALUE", DbType.Decimal, 50, poNewEntity.NPENALTY_TYPE_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_TYPE_CALC_BASEON", DbType.String, 2, poNewEntity.CPENALTY_TYPE_CALC_BASEON);
                loDb.R_AddCommandParameter(loCmd, "@CROUNDING_MODE", DbType.String, 50, poNewEntity.CROUNDING_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IROUNDED", DbType.Int32, 50, poNewEntity.IROUNDED);
                loDb.R_AddCommandParameter(loCmd, "@CCUTOFDATE_BY", DbType.String, 50, poNewEntity.CCUTOFDATE_BY);
                loDb.R_AddCommandParameter(loCmd, "@IGRACE_PERIOD", DbType.Int32, 50, poNewEntity.IGRACE_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_FEE_START_FROM", DbType.String, 50, poNewEntity.CPENALTY_FEE_START_FROM);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_HOLIDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_SATURDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_SUNDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@LMIN_PENALTY_AMOUNT", DbType.Boolean, 50, poNewEntity.LMIN_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NMIN_PENALTY_AMOUNT", DbType.Decimal, 50, poNewEntity.NMIN_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@LMAX_PENALTY_AMOUNT", DbType.Boolean, 50, poNewEntity.LMAX_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NMAX_PENALTY_AMOUNT", DbType.Decimal, 50, poNewEntity.NMAX_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_INVGRP_PENALTY {@poParameter}", loDbParam);

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
