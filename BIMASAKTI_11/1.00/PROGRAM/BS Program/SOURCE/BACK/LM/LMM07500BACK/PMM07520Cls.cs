using PMM07500COMMON;
using PMM07500COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using PMM07500COMMON.DTO_s.stamp_amount;

namespace PMM07500BACK
{
    public class PMM07520Cls : R_BusinessObject<PMM07520GridDTO>
    {
        //declare tenant class in order to bring rsp project dll while deployment
        RSP_PM_MAINTAIN_STAMP_RATEResources.Resources_Dummy_Class _rspMaintainSTamp = new();
        RSP_PM_SAVE_STAMP_RATEResources.Resources_Dummy_Class _rspSaveStampRate = new();
        RSP_PM_SAVE_STAMP_RATE_AMOUNTResources.Resources_Dummy_Class _rspSaveStampRateAMOUNT = new();
        RSP_PM_SAVE_STAMP_RATE_AMOUNTResources.Resources_Dummy_Class _rspSaveStampRateAmount = new();
        RSP_PM_DELETE_STAMP_RATEResources.Resources_Dummy_Class _rspDeleteStampRate = new();
        RSP_PM_DELETE_STAMP_RATE_AMOUNTResources.Resources_Dummy_Class _rspDeleteStampRateAMOUNT = new();
        RSP_PM_DELETE_STAMP_RATE_AMOUNTResources.Resources_Dummy_Class _rspDeleteStampRateAmount = new();

        private LoggerPMM07500 _logger;

        private readonly ActivitySource _activitySource;

        public PMM07520Cls()
        {
            _logger = LoggerPMM07500.R_GetInstanceLogger();
            _activitySource = PMM07500Activity.R_GetInstanceActivitySource();
        }

        public List<PMM07520GridDTO> GetStampRateAmountList(PMR07500ParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<PMM07520GridDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_STAMP_RATE_AMOUNT_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, int.MaxValue, poEntity.CPARENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CGRAND_PARENT_ID", DbType.String, int.MaxValue, poEntity.CGRAND_PARENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, int.MaxValue, poEntity.CLANGUAGE_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = new();
                loRtn = R_Utility.R_ConvertTo<PMM07520GridDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Deleting(PMM07520GridDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);


                lcQuery = "RSP_PM_DELETE_STAMP_RATE_AMOUNT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, poEntity.CREC_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    ShowLogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        protected override PMM07520GridDTO R_Display(PMM07520GridDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            PMM07520GridDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_STAMP_RATE_AMOUNT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, poEntity.CREC_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, int.MaxValue, poEntity.CLANGUAGE_ID);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loRtn = R_Utility.R_ConvertTo<PMM07520GridDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(PMM07520GridDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                R_ExternalException.R_SP_Init_Exception(loConn);

                string lcQuery = "RSP_PM_SAVE_STAMP_RATE_AMOUNT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_CODE", DbType.String, int.MaxValue, poNewEntity.CSTAMP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CGRAND_PARENT_ID", DbType.String, int.MaxValue, poNewEntity.CGRAND_PARENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, int.MaxValue, poNewEntity.CPARENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDATE", DbType.String, int.MaxValue, poNewEntity.CDATE);
                loDb.R_AddCommandParameter(loCmd, "@NMIN_AMOUNT", DbType.Decimal, int.MaxValue, poNewEntity.NMIN_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NSTAMP_AMOUNT", DbType.Decimal, int.MaxValue, poNewEntity.NSTAMP_AMOUNT);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    var loResult = loDb.SqlExecQuery(loConn, loCmd, false);
                    poNewEntity.CREC_ID = R_Utility.R_ConvertTo<PMM07520GridDTO>(loResult).FirstOrDefault().CREC_ID;

                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    ShowLogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
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

        #region logmethodhelper

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
    }
}
