using PMM03700COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using PMM03700COMMON.DTO_s;
using RSP_PM_MAINTAIN_TENANT_CLASSResources;
using RSP_PM_MAINTAIN_TENANT_CLASS_GRPResources;
using System.Diagnostics;
using System.Reflection;

namespace PMM03700BACK
{
    public class PMM03700Cls : R_BusinessObject<TenantClassificationGroupDTO>
    {
        //declare tenant class in order to bring rsp project dll while deployment
        RSP_PM_MAINTAIN_TENANT_CLASSResources.Resources_Dummy_Class _rspTenantClass = new();

        RSP_PM_MAINTAIN_TENANT_CLASS_GRPResources.Resources_Dummy_Class _rspTenantClassGRP = new();

        private LoggerPMM03700 _logger;

        private readonly ActivitySource _activitySource;

        public PMM03700Cls()
        {
            _logger = LoggerPMM03700.R_GetInstanceLogger();
            _activitySource = PMM03700Activity.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(TenantClassificationGroupDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            TenantClassificationGroupDTO loRtn = null;
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


                lcQuery = "RSP_PM_MAINTAIN_TENANT_CLASS_GRP";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 50, poEntity.CTENANT_CLASSIFICATION_GROUP_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_NAME", DbType.String, 50, poEntity.CTENANT_CLASSIFICATION_GROUP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "DELETE");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
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

        protected override TenantClassificationGroupDTO R_Display(TenantClassificationGroupDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            TenantClassificationGroupDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_TENANT_CLASS_GRP_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 50, poEntity.CTENANT_CLASSIFICATION_GROUP_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loRtn = R_Utility.R_ConvertTo<TenantClassificationGroupDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(TenantClassificationGroupDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
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
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }

                string lcQuery = "RSP_PM_MAINTAIN_TENANT_CLASS_GRP";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 20, poNewEntity.CTENANT_CLASSIFICATION_GROUP_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_NAME", DbType.String, 100, poNewEntity.CTENANT_CLASSIFICATION_GROUP_NAME);
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

        public List<TenantClassificationGroupDTO> GetTenantClassGrpList(TenantClassificationGroupDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<TenantClassificationGroupDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_TENANT_CLASS_GRP_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = new();
                loRtn = R_Utility.R_ConvertTo<TenantClassificationGroupDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PropertyDTO> GetPropertyList(PropertyDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PropertyDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
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