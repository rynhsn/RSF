using PMM00200COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using PMM00200COMMON.DTO_s;
using System.Diagnostics;
using System.Reflection;

namespace PMM00200BACK
{
    public class PMM00200Cls : R_BusinessObject<PMM00200DTO>
    {
        RSP_PM_MAINTAIN_USER_PARAMResources.Resources_Dummy_Class rspUserParam = new();

        private PMM00200Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMM00200Cls()
        {
            _logger = PMM00200Logger.R_GetInstanceLogger();
            _activitySource = PMM00200Activity.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(PMM00200DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override PMM00200DTO R_Display(PMM00200DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            PMM00200DTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_USER_PARAM_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CCODE", DbType.String, 50, poEntity.CCODE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMM00200DTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(PMM00200DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
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

                switch (poCRUDMode)
                {
                    //case eCRUDMode.AddMode:
                    //    lcAction = "ADD";
                    //    break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }

                lcQuery = "RSP_PM_MAINTAIN_USER_PARAM";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCODE", DbType.String, 8, poNewEntity.CCODE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 510, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LEVEL_OPERATOR_SIGN", DbType.String, 2, poNewEntity.CUSER_LEVEL_OPERATOR_SIGN);
                loDb.R_AddCommandParameter(loCmd, "@IUSER_LEVEL", DbType.Int32, 4, poNewEntity.IUSER_LEVEL);
                loDb.R_AddCommandParameter(loCmd, "@CVALUE", DbType.String, 100, poNewEntity.CVALUE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

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

        public List<PMM00200GridDTO> GetUserParamList(PMM00200DBParam poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMM00200GridDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_USER_PARAM_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loRtn = R_Utility.R_ConvertTo<PMM00200GridDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public void ActiveInactiveUserParam(ActiveInactiveParam poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                lcQuery = "RSP_PM_MAINTAIN_USER_PARAM";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCODE", DbType.String, 8, poEntity.CCODE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, "");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LEVEL_OPERATOR_SIGN", DbType.String, 2, "");
                loDb.R_AddCommandParameter(loCmd, "@IUSER_LEVEL", DbType.Int32, 8, 0);
                loDb.R_AddCommandParameter(loCmd, "@CVALUE", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
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
