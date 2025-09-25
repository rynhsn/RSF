using GSM04000Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using RSP_GS_MAINTAIN_DEPARTMENTResources;
using RSP_GS_UPLOAD_DEPARTMENTResources;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

namespace GSM04000Back
{
    public class GSM04000UploadCls : R_IBatchProcess
    {
        private RSP_GS_UPLOAD_DEPARTMENTResources.Resources_Dummy_Class _rspUploadDept = new();

        private LoggerGSM04000 _logger;

        private readonly ActivitySource _activitySource;

        public GSM04000UploadCls()
        {
            _logger = LoggerGSM04000.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }


        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();
            _logger.LogInfo($"Start process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcessAsync(poBatchProcessPar);
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo($"End process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
        }

        public async Task _BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            _logger.LogInfo($"Start process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                // creation temptable
                lcQuery += $"CREATE TABLE #DEPARTMENT " +
                              $"( No INT, " +
                              $"DepartmentCode VARCHAR(254), " +
                              $"DepartmentName VARCHAR(254), " +
                              $"CenterCode VARCHAR(254)," +
                              $"ManagerName VARCHAR(254)," +
                              $"BranchCode VARCHAR(100)," +
                              $"Everyone BIT," +
                              $"Active BIT," +
                              $"NonActiveDate VARCHAR(8)," +
                              $"Email1 VARCHAR(100)," +
                              $"Email2 VARCHAR(100))"
                              ;

                //exec
                _logger.LogInfo(lcQuery);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                //deserialize object list
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM04000ExcelBatchDTO>>(poBatchProcessPar.BigObject).ToList();

                //logger for data bulkinsert
                foreach (var item in loObject)
                {
                    var logMessage = $"INSERT INTO #DEPARTMENT : {string.Join(", ", item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.GetValue(item)?.ToString() ?? ""))}";
                    _logger.LogInfo(logMessage);
                }

                //exec bulkinsert
                loDb.R_BulkInsert<GSM04000ExcelBatchDTO>((SqlConnection)loConn, "#DEPARTMENT", loObject);

                //query exec rsp upload
                lcQuery = "RSP_GS_UPLOAD_DEPARTMENT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, int.MaxValue, poBatchProcessPar.Key.KEY_GUID);

                _logger.LogDebug("Exec " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'")));
                //exec rsp uploads
                loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                    if (!(loConn.State == ConnectionState.Closed))
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

            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            int liFlag = 1;
            string lcMessageFinish = "Succes";
            if (loException.Haserror)
            {
                _logger.LogInfo("loException.Haserror (catch parent)");
                liFlag = 9;
                lcMessageFinish = "Has Error";
                lcQuery = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES ( '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}', 100,'{loException.ErrorList[0].ErrDescp}')";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                _logger.LogInfo("before INSERT INTO GST_UPLOAD_ERROR_STATUS");
                loDb.SqlExecNonQuery(loDb.GetConnection(), loCmd, true);
                _logger.LogInfo("after INSERT INTO GST_UPLOAD_ERROR_STATUS");
            }
            lcQuery = string.Format("EXEC RSP_WRITEUPLOADPROCESSSTATUS '{0}', '{1}', '{2}', 100, '{3}', {4}", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID, poBatchProcessPar.Key.KEY_GUID, lcMessageFinish, liFlag);
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            _logger.LogInfo("before SqlExecNonQuery RSP_WRITEUPLOADPROCESSSTATUS");
            loDb.SqlExecNonQuery(loDb.GetConnection(), loCmd, true);
            _logger.LogInfo("after SqlExecNonQuery RSP_WRITEUPLOADPROCESSSTATUS");

            _logger.LogInfo($"End process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
        }

    }
}
