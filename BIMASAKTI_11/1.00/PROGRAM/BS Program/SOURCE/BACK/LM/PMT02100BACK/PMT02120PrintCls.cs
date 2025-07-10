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
using PMT02100COMMON.Loggers;
using PMT02100BACK.OpenTelemetry;
using PMT02100COMMON.DTOs.PMT02120Print;
using PMT02100COMMON.DTOs.PMT02100;
using System.Data.SqlClient;

namespace PMT02100BACK
{
    public class PMT02120PrintCls
    {
        //RSP_CB_GENERATE_CASH_FLOWResources.Resources_Dummy_Class loRsp = new RSP_CB_GENERATE_CASH_FLOWResources.Resources_Dummy_Class();

        private LoggerPMT02120Print _loggerPMT02120Print;
        private readonly ActivitySource _activitySource;
        public PMT02120PrintCls()
        {
            _loggerPMT02120Print = LoggerPMT02120Print.R_GetInstanceLogger();
            _activitySource = PMT02120PrintActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMT02120PrintReportDTO> GetPrintReportList(PMT02120PrintReportParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintReportList");
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<PMT02120PrintReportDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();

                lcQuery = "CREATE TABLE #FILTERED_HANDOVER " +
                    "(NO INT NOT NULL, " +
                    "CCOMPANY_ID VARCHAR(8) NOT NULL, " +
                    "CPROPERTY_ID VARCHAR(20) NOT NULL, " +
                    "CDEPT_CODE VARCHAR(20) NOT NULL, " +
                    "CTRANS_CODE VARCHAR(10) NOT NULL, " +
                    "CREF_NO VARCHAR(30) NOT NULL)";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<PMT02120FilteredHandoverDTO>((SqlConnection)loConn, "#FILTERED_HANDOVER", poParam.FilteredHandoverData);

                lcQuery = "EXEC RSP_PM_GET_HANDOVER_REPORT " +
                          "@LASSIGNMENT, " +
                          "@LCHECKLIST, " +
                          "@CLANG_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@LASSIGNMENT", DbType.String, 50, poParam.LASSIGNMENT);
                loDb.R_AddCommandParameter(loCmd, "@LCHECKLIST", DbType.String, 50, poParam.LCHECKLIST);
                loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 50, poParam.CLANG_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _loggerPMT02120Print.LogDebug("EXEC RSP_PM_GET_HANDOVER_REPORT {@Parameters} || GetPrintReportList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02120PrintReportDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerPMT02120Print.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public PMT02120PrintReportDTO GetBaseHeaderLogoCompany(PMT02120PrintReportParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            R_Exception loEx = new R_Exception();
            PMT02120PrintReportDTO loResult = null;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();


                lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as OLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerPMT02120Print.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as OLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMT02120PrintReportDTO>(loDataTable).FirstOrDefault();
                lcQuery = "EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _loggerPMT02120Print.LogDebug(string.Format("EXEC RSP_GS_GET_COMPANY_INFO '@CCOMPANY_ID'", loDbParam));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<PMT02120PrintReportDTO>(loDataTable).FirstOrDefault();

                loResult.CCOMPANY_NAME = loCompanyNameResult.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerPMT02120Print.LogError(loEx);
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
    }
}
