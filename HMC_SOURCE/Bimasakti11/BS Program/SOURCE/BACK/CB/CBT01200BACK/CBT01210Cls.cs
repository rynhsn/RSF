using CBT01200Back;
using CBT01200Common.Loggers;
using CBT01200Common.DTOs;
using CBT01200Common;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using CBT01200Common.DTOs.CBT01210;
using System.Transactions;
using R_CommonFrontBackAPI;

namespace CBT01200BACK
{
    public class CBT01210Cls :   R_BusinessObject<CBT01210ParamDTO>
    {
        private RSP_CB_DELETE_TRANS_JRNResources.Resources_Dummy_Class loDeleteCBTransJRNRes = new();
        private RSP_CB_SAVE_TRANS_JRNResources.Resources_Dummy_Class loSaveCBTransJRNRes = new();
        private RSP_CB_SAVE_CA_WT_JOURNALResources.Resources_Dummy_Class loSaveCAWTJRNRes = new();
        private RSP_CB_SAVE_SYSTEM_PARAMResources.Resources_Dummy_Class loSaveSystemCBTransJRNRes = new();
        private RSP_CB_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class loUpdateCBTransHDStatusRes
            = new();
        private LoggerCBT01200 _logger;

        private readonly ActivitySource _activitySource;
        public CBT01210Cls()
        {
            _logger = LoggerCBT01200.R_GetInstanceLogger();
            _activitySource = CBT01200Activity.R_GetInstanceActivitySource();
        }
        
        #region log activity
        
        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }
        #endregion
        
        public List<CBT01201DTO> GetJournalDetailList(CBT01210ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<CBT01201DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_CB_GET_TRANS_JRN_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, int.MaxValue, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBT01201DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override CBT01210ParamDTO R_Display(CBT01210ParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            CBT01210ParamDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_CB_GET_TRANS_JRN";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 100, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBT01210ParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(CBT01210ParamDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");

            R_Exception loEx = new R_Exception();
            string lcQuery = null;
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
                    lcAction = "NEW";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                 
                lcQuery = @"RSP_CB_SAVE_TRANS_JRN";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, int.MaxValue, poNewEntity.CPARENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, int.MaxValue, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, int.MaxValue, poNewEntity.CREF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CINPUT_TYPE", DbType.String, int.MaxValue, "M");
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, int.MaxValue, poNewEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, int.MaxValue, poNewEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, int.MaxValue, poNewEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, int.MaxValue, poNewEntity.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDBCR", DbType.String, int.MaxValue, poNewEntity.CDBCR);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, int.MaxValue, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Decimal, int.MaxValue, poNewEntity.NTRANS_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CDETAIL_DESC", DbType.String, int.MaxValue, poNewEntity.CDETAIL_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_NO", DbType.String, int.MaxValue, poNewEntity.CDOCUMENT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_DATE", DbType.String, int.MaxValue, poNewEntity.CDOCUMENT_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LSUSPENSE_ACCOUNT", DbType.Boolean, int.MaxValue, false);
                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    var loTempResult = R_Utility.R_ConvertTo<CBT01200JournalHDParam>(loDataTable).FirstOrDefault();
                    poNewEntity.CREC_ID = loTempResult.CREC_ID;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    _logger.LogError(ex, "An error occurred while executing the stored procedure.");
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred in the outer catch block.");
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

        protected override void R_Deleting(CBT01210ParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            CBT01210ParamDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_CB_DELETE_TRANS_JRN";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 100, poEntity.CREC_ID);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBT01210ParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

    internal class ConvertRecID
    {
        public string CJRN_ID { get; set; }
        public string CPARENT_ID { get; set; }
    }
}
