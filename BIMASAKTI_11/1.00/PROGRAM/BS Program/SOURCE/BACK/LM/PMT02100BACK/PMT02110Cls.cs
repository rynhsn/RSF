using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02100BACK.OpenTelemetry;
using PMT02100COMMON.Loggers;
using PMT02100COMMON.DTOs.PMT02110;
using System.Reflection.Metadata;

namespace PMT02100BACK
{
    public class PMT02110Cls
    {
        RSP_PM_HANDOVER_CONFIRM_SCHEDULEResources.Resources_Dummy_Class loRsp = new RSP_PM_HANDOVER_CONFIRM_SCHEDULEResources.Resources_Dummy_Class();
        
        private LoggerPMT02110 _logger;
        private readonly ActivitySource _activitySource;

        public PMT02110Cls()
        {
            _logger = LoggerPMT02110.R_GetInstanceLogger();
            _activitySource = PMT02110ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public void ConfirmScheduleProcess(PMT02110ConfirmParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("ConfirmScheduleProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_HANDOVER_CONFIRM_SCHEDULE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSCHEDULED_HO_DATE", DbType.String, 50, poParameter.CSCHEDULED_HO_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CSCHEDULED_HO_TIME", DbType.String, 50, poParameter.CSCHEDULED_HO_TIME);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                //var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                //.Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                //_logger.LogDebug("EXEC RSP_PM_HANDOVER_PROCESS {@poParameter}", loDbParam);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_HANDOVER_CONFIRM_SCHEDULE {@Parameters} || ConfirmScheduleProcess(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }

        public PMT02110TenantDTO GetTenantDetail(PMT02110TenantParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetTenantDetail");
            R_Exception loException = new R_Exception();
            PMT02110TenantDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_GET_TENANT_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 50, poParameter.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                //var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                //.Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                //_logger.LogDebug("EXEC RSP_PM_HANDOVER_PROCESS {@poParameter}", loDbParam);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_TENANT_DETAIL {@Parameters} || GetTenantDetail(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02110TenantDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

    }
}
