using GSM12000COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using System.Transactions;

namespace GSM12000BACK
{
    public class GSM12000Cls : R_BusinessObjectAsync<GSM12000DTO>
    {
        RSP_GS_MAINTAIN_MESSAGEResources.Resources_Dummy_Class _loRsp =
            new RSP_GS_MAINTAIN_MESSAGEResources.Resources_Dummy_Class();

        private LoggerGSM12000 _Logger;
        private readonly ActivitySource _activitySource;

        public GSM12000Cls()
        {
            _Logger = LoggerGSM12000.R_GetInstanceLogger();
            _activitySource = GSM12000ActivityInitSourceBase.R_GetInstanceActivitySource();
        }

        public async Task<List<GSM12000GSBCodeDTO>> GetListGSBCodeAsync()
        {
            using Activity activity = _activitySource.StartActivity("GetListGSBCodeAsync");
            var loEx = new R_Exception();
            List<GSM12000GSBCodeDTO> loResult = null;
            R_Db loDb;
            DbCommand loCmd = null;
            DbConnection loConn = null;
            string lcAction = "";

            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                string lcQuery = "";
                lcQuery =
                    "SELECT TOP 1 CPROPERTY_ID as CCODE FROM GSM_PROPERTY (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID AND LACTIVE = 1";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@") && x.ParameterName == "@CCOMPANY_ID")
                    .Select(x => x.Value);
                _Logger.LogDebug(
                    "SELECT TOP 1 CPROPERTY_ID as CCODE FROM GSM_PROPERTY (NOLOCK) WHERE CCOMPANY_ID = '{@poParameter}' AND LACTIVE = 1",
                    loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loTempResult = R_Utility.R_ConvertTo<GSM12000GSBCodeDTO>(loDataTable).FirstOrDefault();

                lcQuery =
                    "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO (@CAPPLICATION, @CPROPERTY_ID, @CCLASS_ID, @CREC_ID_LIST, @CLANGUAGE_ID)";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 50, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, loTempResult.CCODE);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, "_BS_MESSAGE_TYPE");
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID_LIST", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO ({@poParameter})", loDbParam);

                loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<GSM12000GSBCodeDTO>(loDataTable).ToList();
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
                    if (loConn.State != ConnectionState.Closed)
                    {
                        await loConn.CloseAsync();
                    }

                    await loConn.DisposeAsync();
                }

                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<GSM12000DTO>> GetListMessageAsync(string pcMessageType)
        {
            using Activity activity = _activitySource.StartActivity("GetListMessageAsync");
            var loEx = new R_Exception();
            List<GSM12000DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_MESSAGE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_TYPE", DbType.String, 50, pcMessageType);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_MESSAGE_LIST {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GSM12000DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override async Task<GSM12000DTO> R_DisplayAsync(GSM12000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_DisplayAsync");
            var loEx = new R_Exception();
            GSM12000DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_MESSAGE_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_TYPE", DbType.String, 50, poEntity.CMESSAGE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_NO", DbType.String, 50, poEntity.CMESSAGE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_MESSAGE_LIST {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GSM12000DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override async Task R_SavingAsync(GSM12000DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_SavingAsync");
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                await RSP_GS_MAINTAIN_MESSAGEClsAsync(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        protected override async Task R_DeletingAsync(GSM12000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_DeletingAsync");
            var loEx = new R_Exception();

            try
            {
                poEntity.CACTION = "DELETE";
                await RSP_GS_MAINTAIN_MESSAGEClsAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RSP_GS_MAINTAIN_MESSAGEClsAsync(GSM12000DTO poNewEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_MESSAGEClsAsync");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_MAINTAIN_MESSAGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_TYPE", DbType.String, 20, poNewEntity.CMESSAGE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_NO", DbType.String, 20, poNewEntity.CMESSAGE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_NAME", DbType.String, 20, poNewEntity.CMESSAGE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@TMESSAGE_DESCRIPTION", DbType.String, Int32.MaxValue, poNewEntity.TMESSAGE_DESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CADDITIONAL_INFO", DbType.String, Int32.MaxValue, poNewEntity.CADDITIONAL_INFO);
                loDb.R_AddCommandParameter(loCmd, "@TMESSAGE_DESCR_RTF", DbType.String, Int32.MaxValue, poNewEntity.TMESSAGE_DESCR_RTF);
                loDb.R_AddCommandParameter(loCmd, "@TADDITIONAL_DESCR_RTF", DbType.String, Int32.MaxValue, poNewEntity.TADDITIONAL_DESCR_RTF);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@"))
                        .ToDictionary(x => x.ParameterName, x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GS_MAINTAIN_MESSAGE {@poParameter}", loDbParam);

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

        public async Task ChangeStatusActiveClsAsync(GSM12000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("ChangeStatusActiveClsAsync");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_ACTIVE_INACTIVE_MESSAGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_TYPE", DbType.String, 20, poEntity.CMESSAGE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_NO", DbType.String, 20, poEntity.CMESSAGE_NO);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_MESSAGE {@poParameter}", loDbParam);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
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

        #region Report SP

        public GSM12000BaseHeaderResultDTO GetBaseHeaderLogoCompany()
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            GSM12000BaseHeaderResultDTO loResult = null;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<GSM12000BaseHeaderResultDTO>(loDataTable).FirstOrDefault();

                lcQuery = "EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _Logger.LogDebug(string.Format("EXEC RSP_GS_GET_COMPANY_INFO '@CCOMPANY_ID'", loDbParam));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult =
                    R_Utility.R_ConvertTo<GSM12000BaseHeaderResultDTO>(loDataTable).FirstOrDefault();

                loResult.CCOMPANY_NAME = loCompanyNameResult.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
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
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM12000ResultPrintSPDTO> GetPrintDataResult(GSM12000PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintDataResult");
            var loEx = new R_Exception();
            List<GSM12000ResultPrintSPDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_PRINT_MESSAGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_TYPE", DbType.String, 50, poEntity.CMESSAGE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_NO_FROM", DbType.String, 50, poEntity.CMESSAGE_NO_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_NO_TO", DbType.String, 50, poEntity.CMESSAGE_NO_TO);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_INACTIVE", DbType.Boolean, 50, poEntity.LPRINT_INACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_PRINT_MESSAGE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM12000ResultPrintSPDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion
    }
}