using PMT00500COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;

namespace PMT00500BACK
{
    public class PMT00520Cls : R_BusinessObject<PMT00520DTO>
    {
        private LoggerPMT00520 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT00520Cls()
        {
            _Logger = LoggerPMT00520.R_GetInstanceLogger();
            _activitySource = PMT00520ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMT00520DTO> GetAllLOIUtilities(PMT00520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllLOIUnit");
            var loEx = new R_Exception();
            List<PMT00520DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_UTILITIES_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_UTILITIES_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT00520DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override PMT00520DTO R_Display(PMT00520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMT00520DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_UTILITIES_DT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 100, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 50, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_UTILITIES_DT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT00520DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Saving(PMT00520DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                    poNewEntity.CSEQ_NO = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_UTILITIES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);

                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poNewEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poNewEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 20, poNewEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CMETER_NO", DbType.String, 50, poNewEntity.CMETER_NO);
                loDb.R_AddCommandParameter(loCmd, "@IMETER_START", DbType.Int32, 50, 0);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_INV_PRD", DbType.String, 6, "");

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_UTILITIES {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<PMT00520DTO>(loDataTable).FirstOrDefault();

                    if (loTempResult != null)
                        poNewEntity.CSEQ_NO = loTempResult.CSEQ_NO;
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
        protected override void R_Deleting(PMT00520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_UTILITIES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);

                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 20, poEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CMETER_NO", DbType.String, 50, poEntity.CMETER_NO);
                loDb.R_AddCommandParameter(loCmd, "@IMETER_START", DbType.Int32, 50, poEntity.IMETER_START);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_INV_PRD", DbType.String, 6, poEntity.CSTART_INV_PRD);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_UTILITIES {@poParameter}", loDbParam);

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
        public void ChangeStatusLOIUtility(PMT00520ActiveInactiveDTO poNewEntity)
        {
            using Activity activity = _activitySource.StartActivity("ChangeStatusLOIUtility");
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_ACTIVE_INACTIVE_AGRMT_UTILITY";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 30, poNewEntity.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 30, poNewEntity.CFLOOR_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 30, poNewEntity.CUNIT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 30, poNewEntity.CCHARGES_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO);
                loDB.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_ACTIVE_INACTIVE_AGRMT_UTILITY {@poParameter}", loDbParam);

                    loDB.SqlExecNonQuery(loConn, loCmd, false);
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
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}