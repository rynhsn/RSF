using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using RSP_PM_MAINTAIN_TENANT_UNIT_FACILITYResources;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace PMT03000BACK
{
    public class PMT03001Cls : R_BusinessObject<TenantUnitFacilityDTO>
    {
        Resources_Dummy_Class _rsp = new();
        private LoggerPMT03000 _logger;
        private readonly ActivitySource _activitySource;
        public PMT03001Cls()
        {
            _logger = LoggerPMT03000.R_GetInstanceLogger();
            _activitySource = PMT03000Activity.R_GetInstanceActivitySource();
        }

        //methods
        public List<UnitTypeCtgFacilityDTO> Getlist_UnitTypeCtgFacilityDTO(UnitTypeCtgFacilityDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<UnitTypeCtgFacilityDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_UNIT_TYPE_CTG_FACILITY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 20, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<UnitTypeCtgFacilityDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public List<TenantUnitFacilityDTO> Getlist_TenantUnitFacilityDTO(TenantUnitFacilityDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<TenantUnitFacilityDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_TENANT_UNIT_FACILITY";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poEntity.CTENANT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poEntity.CUNIT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String, 20, poEntity.CFACILITY_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 20, "");
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<TenantUnitFacilityDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        protected override void R_Deleting(TenantUnitFacilityDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd = null;
            DbConnection loConn = null;

            try
            {
                loDb = new R_Db();
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_PM_MAINTAIN_TENANT_UNIT_FACILITY";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, int.MaxValue, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, int.MaxValue, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, int.MaxValue, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, int.MaxValue, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String, int.MaxValue, poEntity.CFACILITY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, int.MaxValue, poEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CREGIST_NO", DbType.String, int.MaxValue, poEntity.CREGIST_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, int.MaxValue, poEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEND_DATE", DbType.String, int.MaxValue, poEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, "DELETE");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
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
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }
        protected override TenantUnitFacilityDTO R_Display(TenantUnitFacilityDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            TenantUnitFacilityDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                string lcQuery = "RSP_PM_GET_TENANT_UNIT_FACILITY";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String, 20, poEntity.CFACILITY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 20, poEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<TenantUnitFacilityDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Saving(TenantUnitFacilityDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd = null;
            DbConnection loConn = null;

            try
            {
                loDb = new R_Db();
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                poNewEntity.CACTION = poCRUDMode switch
                {
                    eCRUDMode.AddMode => "ADD",
                    eCRUDMode.EditMode => "EDIT",
                    _ => "",
                };
                lcQuery = "RSP_PM_MAINTAIN_TENANT_UNIT_FACILITY";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String,8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String,20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String,20, poNewEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String,20, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String,20, poNewEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String,20, poNewEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String,2, poNewEntity.CFACILITY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String,3, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CREGIST_NO", DbType.String,50, poNewEntity.CREGIST_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String,8, poNewEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEND_DATE", DbType.String,8, poNewEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean,1, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String,10, poNewEntity.CACTION);
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

            loEx.ThrowExceptionIfErrors();
        }
        public void ActiveInactive_TenantUnitFacility(TenantUnitFacilityDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            R_Db loDB;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                string lcQuery = "RSP_PM_ACTIVE_INACTIVE_TENANT_UNIT_FACILITY";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, int.MaxValue, poParam.CTENANT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, int.MaxValue, poParam.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, int.MaxValue, poParam.CFLOOR_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, int.MaxValue, poParam.CUNIT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String, int.MaxValue, poParam.CFACILITY_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, int.MaxValue, poParam.CSEQUENCE);
                loDB.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poParam.LACTIVE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loDB.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public FacilityQtyDTO GetRecord_FacilityQty(TenantUnitFacilityDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            FacilityQtyDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                string lcQuery = "RSP_PM_GET_FACILITY_QTY_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String, 20, poEntity.CFACILITY_TYPE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<FacilityQtyDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        //helper
        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }
        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }
    }
}
