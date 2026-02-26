using FAM00100Common.DTOs.FAM00100;
using FAM00100Common.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Transactions;

namespace FAM00100Back
{
    public class FAM00100Cls
    {
        RSP_FA_SAVE_SYSTEM_PARAM.Resources_Dummy_Class _loRsp = new RSP_FA_SAVE_SYSTEM_PARAM.Resources_Dummy_Class();

        private LoggerFAM00100 _Logger;
        private readonly ActivitySource _activitySource;

        public FAM00100Cls()
        {
            _Logger = LoggerFAM00100.R_GetInstanceLogger();
            _activitySource = FAM00100ActivityInitSourceBase.R_GetInstanceActivitySource();
        }

        public async Task<FAM00100ValidateInitDTO> GetInitValidate()
        {
            using Activity activity = _activitySource.StartActivity("GetTodayDateDB");
            var loEx = new R_Exception();
            FAM00100ValidateInitDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT TOP 1 1 AS CRESULT FROM FAM_SYSTEM_PARAM (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //testing empty company id
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                string loCompanyIdLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT TOP 1 1 AS CRESULT FROM CBM_SYSTEM_PARAM (NOLOCK) WHERE CCOMPANY_ID = {0}", loCompanyIdLog);
                _Logger.LogDebug(loDebugLogResult);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<FAM00100ValidateInitDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<FAM00100DTO> GetSystemParamCB()
        {
            using Activity activity = _activitySource.StartActivity("GetSystemParamCB");
            var loEx = new R_Exception();
            FAM00100DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_FA_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_FA_GET_SYSTEM_PARAM {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<FAM00100DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<FAM00100GSPeriodYearRangeDTO> GetPeriodYearRangeRecord()
        {
            using Activity activity = _activitySource.StartActivity("GetPeriodYearRangeRecord");
            var loEx = new R_Exception();
            FAM00100GSPeriodYearRangeDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 50, "");

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<FAM00100GSPeriodYearRangeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<FAM00100DTO> SaveSystemParamCB(FAM00100DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveSystemParamCB");
            var loEx = new R_Exception();
            FAM00100DTO loResult = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    await SPSaveSystemParamCB(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

                loResult = await GetSystemParamCB();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        private async Task SPSaveSystemParamCB(FAM00100DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";

                }

                lcQuery = "RSP_FA_SAVE_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 10, poNewEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DEPT_CODE", DbType.String, 10, poNewEntity.CTRANS_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 10, poNewEntity.CASSET_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_JOURNAL_TYPE", DbType.String, 1, poNewEntity.CASSET_JOURNAL_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CAUTO_DEPR_TYPE", DbType.String, 1, poNewEntity.CAUTO_DEPR_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@LINCREMENT_FLAG", DbType.Boolean, 1, poNewEntity.LINCREMENT_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@IJRNGRP_LENGTH", DbType.Int64, 4, poNewEntity.IJRNGRP_LENGTH);
                loDb.R_AddCommandParameter(loCmd, "@LBY_DEPT", DbType.Boolean, 1, poNewEntity.LBY_DEPT);
                loDb.R_AddCommandParameter(loCmd, "@IBY_DEPT_LENGTH", DbType.Int64, 4, poNewEntity.IBY_DEPT_LENGTH);
                loDb.R_AddCommandParameter(loCmd, "@LICLINK", DbType.Boolean, 1, poNewEntity.LICLINK);
                loDb.R_AddCommandParameter(loCmd, "@CICLINK_DATE", DbType.String, 8, poNewEntity.CICLINK_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LPJLINK", DbType.Boolean, 1, poNewEntity.LPJLINK);
                loDb.R_AddCommandParameter(loCmd, "@CPJLINK_DATE", DbType.String, 8, poNewEntity.CPJLINK_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 1, poNewEntity.LGLLINK);
                loDb.R_AddCommandParameter(loCmd, "@CGLLINK_DATE", DbType.String, 8, poNewEntity.CGLLINK_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PERIOD", DbType.String, 8, poNewEntity.CCURRENT_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSOFT_PERIOD", DbType.String, 8, poNewEntity.CSOFT_PERIOD);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_FA_SAVE_SYSTEM_PARAM {@poParameter}", loDbParam);

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