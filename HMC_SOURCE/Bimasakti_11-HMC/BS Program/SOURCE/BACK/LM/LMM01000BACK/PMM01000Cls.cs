using PMM01000COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using PMM01000COMMON.Print;
using System.Diagnostics;

namespace PMM01000BACK
{
    public class PMM01000Cls : R_BusinessObject<PMM01000DTO>
    {
        RSP_PM_MAINTAIN_RATE_ECResources.Resources_Dummy_Class _loRSP_EC = new RSP_PM_MAINTAIN_RATE_ECResources.Resources_Dummy_Class();
        RSP_PM_MAINTAIN_RATE_OTResources.Resources_Dummy_Class _loRSP_OT = new RSP_PM_MAINTAIN_RATE_OTResources.Resources_Dummy_Class();
        RSP_PM_MAINTAIN_RATE_WGResources.Resources_Dummy_Class _loRSP_WG = new RSP_PM_MAINTAIN_RATE_WGResources.Resources_Dummy_Class();
        RSP_PM_MAINTAIN_UTILITY_CHARGESResources.Resources_Dummy_Class _loRSP = new RSP_PM_MAINTAIN_UTILITY_CHARGESResources.Resources_Dummy_Class();

        private LoggerPMM01000 _PMM01000logger;
        private LoggerPMM01000Print _PMM01000Printlogger;
        private readonly ActivitySource _activitySource;

        public PMM01000Cls()
        {
            _PMM01000logger = LoggerPMM01000.R_GetInstanceLogger();
            _activitySource = PMM01000ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public PMM01000Cls(LoggerPMM01000Print poParam)
        {
            _PMM01000Printlogger = LoggerPMM01000Print.R_GetInstanceLogger();
            _activitySource = PMM01000PrintActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMM01002DTO> GetAllChargesUtility(PMM01002DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllChargesUtility");
            var loEx = new R_Exception();
            List<PMM01002DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CPROPERTY_ID" ||
                    x.ParameterName == "@CCHARGE_TYPE_ID" ||
                    x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _PMM01000logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_LIST {@poParameter}", loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01002DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Deleting(PMM01000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection();
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "RSP_PM_MAINTAIN_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                poEntity.CACTION = "DELETE";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_NAME", DbType.String, 50, poEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.String, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@LACCRUAL", DbType.Boolean, 50, poEntity.LACCRUAL);
                loDb.R_AddCommandParameter(loCmd, "@LTAXABLE", DbType.Boolean, 50, poEntity.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "@LTAX_EXEMPTION", DbType.Boolean, 50, poEntity.LTAX_EXEMPTION);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EXEMPTION_CODE", DbType.String, 50, poEntity.CTAX_EXEMPTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_EXEMPTION_PCT", DbType.Decimal, 50, poEntity.NTAX_EXEMPTION_PCT);
                loDb.R_AddCommandParameter(loCmd, "@LOTHER_TAX", DbType.Boolean, 50, poEntity.LOTHER_TAX);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poEntity.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LWITHHOLDING_TAX", DbType.Boolean, 50, poEntity.LWITHHOLDING_TAX);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_TYPE", DbType.String, 50, poEntity.CWITHHOLDING_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_ID", DbType.String, 50, poEntity.CWITHHOLDING_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUTILITY_JRNGRP_CODE", DbType.String, 50, poEntity.CUTILITY_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACCRUAL_METHOD", DbType.String, 50, poEntity.CACCRUAL_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CPROPERTY_ID" ||
                        x.ParameterName == "@CCHARGES_TYPE" ||
                        x.ParameterName == "@CCHARGES_ID" ||
                        x.ParameterName == "@CCHARGES_NAME" ||
                        x.ParameterName == "@CDESCRIPTION" ||
                        x.ParameterName == "@LACTIVE" ||
                        x.ParameterName == "@LACCRUAL" ||
                        x.ParameterName == "@LTAXABLE" ||
                        x.ParameterName == "@LTAX_EXEMPTION" ||
                        x.ParameterName == "@CTAX_EXEMPTION_CODE" ||
                        x.ParameterName == "@ITAX_EXEMPTION_PCT" ||
                        x.ParameterName == "@LOTHER_TAX" ||
                        x.ParameterName == "@COTHER_TAX_ID" ||
                        x.ParameterName == "@LWITHHOLDING_TAX" ||
                        x.ParameterName == "@CWITHHOLDING_TAX_TYPE" ||
                        x.ParameterName == "@CWITHHOLDING_TAX_ID" ||
                        x.ParameterName == "@CUTILITY_JRNGRP_CODE" ||
                        x.ParameterName == "@CACCRUAL_METHOD" ||
                        x.ParameterName == "@CACTION" ||
                        x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                    _PMM01000logger.LogDebug("EXEC RSP_PM_MAINTAIN_UTILITY_CHARGES {@poParameter}", loDbParam);

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
                _PMM01000logger.LogError(loEx);
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
        protected override PMM01000DTO R_Display(PMM01000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMM01000DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CPROPERTY_ID" ||
                    x.ParameterName == "@CCHARGE_TYPE_ID" ||
                    x.ParameterName == "@CCHARGES_ID" ||
                    x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _PMM01000logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_DETAIL {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01000DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Saving(PMM01000DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection();
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "RSP_PM_MAINTAIN_UTILITY_CHARGES";
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

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_NAME", DbType.String, 50, poNewEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.String, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@LACCRUAL", DbType.Boolean, 50, poNewEntity.LACCRUAL);
                loDb.R_AddCommandParameter(loCmd, "@LTAXABLE", DbType.Boolean, 50, poNewEntity.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "@LTAX_EXEMPTION", DbType.Boolean, 50, poNewEntity.LTAX_EXEMPTION);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EXEMPTION_CODE", DbType.String, 50, poNewEntity.CTAX_EXEMPTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_EXEMPTION_PCT", DbType.Decimal, 50, poNewEntity.NTAX_EXEMPTION_PCT);
                loDb.R_AddCommandParameter(loCmd, "@LOTHER_TAX", DbType.Boolean, 50, poNewEntity.LOTHER_TAX);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poNewEntity.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LWITHHOLDING_TAX", DbType.Boolean, 50, poNewEntity.LWITHHOLDING_TAX);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_TYPE", DbType.String, 50, poNewEntity.CWITHHOLDING_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_ID", DbType.String, 50, poNewEntity.CWITHHOLDING_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUTILITY_JRNGRP_CODE", DbType.String, 50, poNewEntity.CUTILITY_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACCRUAL_METHOD", DbType.String, 50, poNewEntity.CACCRUAL_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CPROPERTY_ID" ||
                        x.ParameterName == "@CCHARGES_TYPE" ||
                        x.ParameterName == "@CCHARGES_ID" ||
                        x.ParameterName == "@CCHARGES_NAME" ||
                        x.ParameterName == "@CDESCRIPTION" ||
                        x.ParameterName == "@LACTIVE" ||
                        x.ParameterName == "@LACCRUAL" ||
                        x.ParameterName == "@LTAXABLE" ||
                        x.ParameterName == "@LTAX_EXEMPTION" ||
                        x.ParameterName == "@CTAX_EXEMPTION_CODE" ||
                        x.ParameterName == "@ITAX_EXEMPTION_PCT" ||
                        x.ParameterName == "@LOTHER_TAX" ||
                        x.ParameterName == "@COTHER_TAX_ID" ||
                        x.ParameterName == "@LWITHHOLDING_TAX" ||
                        x.ParameterName == "@CWITHHOLDING_TAX_TYPE" ||
                        x.ParameterName == "@CWITHHOLDING_TAX_ID" ||
                        x.ParameterName == "@CUTILITY_JRNGRP_CODE" ||
                        x.ParameterName == "@CACCRUAL_METHOD" ||
                        x.ParameterName == "@CACTION" ||
                        x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                    _PMM01000logger.LogDebug("EXEC RSP_PM_MAINTAIN_UTILITY_CHARGES {@poParameter}", loDbParam);

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
                _PMM01000logger.LogError(loEx);
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
        public void PMM01000ChangeStatusSP(PMM01000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("PMM01000ChangeStatusSP");
            R_Exception loException = new R_Exception();

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_ACTIVE_INACTIVE_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.String, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CPROPERTY_ID" ||
                    x.ParameterName == "@CCHARGES_TYPE" ||
                    x.ParameterName == "@CCHARGES_ID" ||
                    x.ParameterName == "@LACTIVE" ||
                    x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _PMM01000logger.LogDebug("EXEC RSP_PM_ACTIVE_INACTIVE_UTILITY_CHARGES {@poParameter}", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _PMM01000logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        public void PMM01000CopySource(PMM01003DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("PMM01000CopySource");
            R_Exception loException = new R_Exception();

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_COPY_NEW_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_CHARGES_ID", DbType.String, 50, poEntity.CCURRENT_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_CHARGES_ID", DbType.String, 50, poEntity.CNEW_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_CHARGES_NAME", DbType.String, 50, poEntity.CNEW_CHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CPROPERTY_ID" ||
                    x.ParameterName == "@CCHARGES_TYPE" ||
                    x.ParameterName == "@CCURRENT_CHARGES_ID" ||
                    x.ParameterName == "@CNEW_CHARGES_ID" ||
                    x.ParameterName == "@CNEW_CHARGES_NAME" ||
                    x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _PMM01000logger.LogDebug("EXEC RSP_PM_COPY_NEW_UTILITY_CHARGES {@poParameter}", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _PMM01000logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        public PMM01000BeforeDeleteDTO ValidateBeforeDelete(PMM01000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("ValidateBeforeDelete");
            var loEx = new R_Exception();
            PMM01000BeforeDeleteDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();


                var lcQuery = "RSP_PM_VALIDATION_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01000logger.LogDebug("EXEC RSP_PM_VALIDATION_UTILITY_CHARGES {@Data}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01000BeforeDeleteDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Report SP
        public PMM01000PrintDTO GetBaseHeaderLogoCompany(PMM01000PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PMM01000PrintDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01000Printlogger.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01000PrintDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01000PrintDTO> GetListDataPrint(PMM01000PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetListDataPrint");
            var loEx = new R_Exception();
            List<PMM01000PrintDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_PRINT_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_FROM", DbType.String, 50, poEntity.CCHARGE_TYPE_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_TO", DbType.String, 50, poEntity.CCHARGE_TYPE_TO);
                loDb.R_AddCommandParameter(loCmd, "@CSHORT_BY", DbType.String, 50, poEntity.CSHORT_BY);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_INACTIVE", DbType.Boolean, 50, poEntity.LPRINT_INACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_DETAIL_ACC", DbType.Boolean, 50, poEntity.LPRINT_DETAIL_ACC);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CPROPERTY_ID" ||
                    x.ParameterName == "@CCHARGE_TYPE_FROM" ||
                    x.ParameterName == "@CCHARGE_TYPE_TO" ||
                    x.ParameterName == "@CSHORT_BY" ||
                    x.ParameterName == "@LPRINT_INACTIVE" ||
                    x.ParameterName == "@LPRINT_DETAIL_ACC" ||
                    x.ParameterName == "@CUSER_LOGIN_ID").Select(x => x.Value);
                _PMM01000Printlogger.LogDebug("EXEC RSP_PM_PRINT_UTILITY_CHARGES {@poParameters}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01000PrintDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
