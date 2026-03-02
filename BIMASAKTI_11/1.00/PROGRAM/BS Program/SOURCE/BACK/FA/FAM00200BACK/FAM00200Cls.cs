using FAM00200Back.OpenTelemetry;
using FAM00200Common.DTOs;
using FAM00200Common.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Transactions;

namespace FAM00200Back
{
    public class FAM00200Cls
    {
        private readonly RSP_FA_SAVE_TAX_TYPEResources.Resources_Dummy_Class _oRsp = new RSP_FA_SAVE_TAX_TYPEResources.Resources_Dummy_Class();

        private readonly LoggerFAM00200 _Logger;
        private readonly ActivitySource _activitySource;

        public FAM00200Cls()
        {
            _Logger = LoggerFAM00200.R_GetInstanceLogger();
            _activitySource = FAM00200ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public async Task<List<FAM00200DTO>> GetListTaxType()
        {
            using Activity activity = _activitySource.StartActivity("GetListTaxType");
            var loEx = new R_Exception();
            List<FAM00200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_FA_GET_TAX_TYPE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_FA_GET_TAX_TYPE_LIST {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<FAM00200DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<FAM00200DTO> GetTaxType(FAM00200DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetTaxType");
            var loEx = new R_Exception();
            FAM00200DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_FA_GET_TAX_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE_ID", DbType.String, 50, poEntity.CTAX_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_FA_GET_TAX_TYPE {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<FAM00200DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<FAM00200DTO> SaveTaxType(FAM00200DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveTaxType");
            var loEx = new R_Exception();
            FAM00200DTO loResult = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    await SPSaveTaxType(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

                loResult = await GetTaxType(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        private async Task SPSaveTaxType(FAM00200DTO poNewEntity, eCRUDMode poCRUDMode)
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
                R_ExternalException.R_SP_Init_Exception(loConn);

                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";
                    poNewEntity.CREC_ID = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";

                }

                lcQuery = "RSP_FA_SAVE_TAX_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE_ID", DbType.String, 20, poNewEntity.CTAX_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE_NAME", DbType.String, 200, poNewEntity.CTAX_TYPE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE_DESC", DbType.String, 400, poNewEntity.CTAX_TYPE_DESC);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIFE", DbType.Int32, 4, poNewEntity.IUSEFUL_LIFE);
                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_FA_SAVE_TAX_TYPE {@poParameter}", loDbParam);
                try
                {
                    var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);

                    if (poCRUDMode == eCRUDMode.AddMode)
                    {
                        var loTempResult = R_Utility.R_ConvertTo<FAM00200DTO>(loDataTable).FirstOrDefault();
                        poNewEntity.CREC_ID = loTempResult.CREC_ID;
                    }
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
