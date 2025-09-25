using PMT50600COMMON.DTOs.PMT50620;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT50600COMMON.Loggers;
using PMT50600BACK.OpenTelemetry;
using System.Diagnostics;

namespace PMT50600BACK
{
    public class PMT50620Cls
    {
        private LoggerPMT50620 _logger;
        private readonly ActivitySource _activitySource;
        public PMT50620Cls()
        {
            _logger = LoggerPMT50620.R_GetInstanceLogger();
            _activitySource = PMT50620ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public void CloseFormProcess(OnCloseProcessParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("CloseFormProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GENERATE_WH_TAX_DEDUCTION " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "CLOGIN_USER_ID, " +
                    "@CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParam.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GENERATE_WH_TAX_DEDUCTION {@Parameters} || CloseFormProcess(Cls) ", loDbParam);

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
    }
}
