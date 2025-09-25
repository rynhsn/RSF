using PMM10000COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM10000COMMON.UtilityDTO;
using System.Data.SqlClient;
using PMM10000COMMON.Upload;

namespace PMM10000BACK
{
    public class PMM10000UploadCls : R_IBatchProcess
    {
        private LoggerPMM10000 _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_UPLOAD_SLA_PROCESSResources.Resources_Dummy_Class _objectRSP = new();

        public PMM10000UploadCls()
        {
            //Initial and Get Logger
            _logger = LoggerPMM10000.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(R_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("000", "Database Connection Failed");
                    _logger.LogError(loException);
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {

            string lcMethodName = nameof(_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand? loCommand = null;
            try
            {
                await Task.Delay(100);
      
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                #region GetParameter
                //get parameter
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();
                #endregion
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<CallTypeErrorDTO>>(poBatchProcessPar.BigObject);

                List<CallTypeUploadDTO> loObjectUpload = loTempObject
                    .Select(x => new CallTypeUploadDTO
                    {
                        No = x.No,
                        CCOMPANY_ID = poBatchProcessPar.Key.COMPANY_ID,
                        CPROPERTY_ID = lcPropertyId,
                        CCALL_TYPE_ID = x.CallTypeId,
                        CCALL_TYPE_NAME = x.CallTypeName,
                        CCATEGORY_ID = x.Category,
                        IDAYS = x.Days,
                        IHOURS = x.Hours,
                        IMINUTES = x.Minutes,
                        LPRIOIRTY = x.LinkToPriorityApps
                    })
                    .ToList(); 

                lcQuery = $"CREATE TABLE #UPLOAD_SLA " +
                          $"(No INT, " +
                          $"CCOMPANY_ID VARCHAR(8), " +
                          $"CPROPERTY_ID   VARCHAR(20), " +
                          $"CCALL_TYPE_ID VARCHAR(20), " +
                          $"CCALL_TYPE_NAME VARCHAR(200), " +
                          $"CCATEGORY_ID VARCHAR(20), " +
                          $"IDAYS INT, " +
                          $"IHOURS INT, " +
                          $"IMINUTES INT, " +
                          $"LPRIOIRTY BIT )";

                _logger.LogDebug("{@ObjectQuery} ", lcQuery);

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert((SqlConnection)loConn, "#UPLOAD_SLA", loObjectUpload);

                lcQuery = "RSP_PM_UPLOAD_SLA_PROCESS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcPropertyId);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogInfo("Execute query : ");
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var abc = loDb.SqlExecNonQuery(loConn, loCommand, false);

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

                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            if (loException.Haserror)
            {
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery);
                _logger.LogInfo(string.Format("Exec query to inform framework from outer exception on cls"));
                _logger.LogDebug("{@ObjectQuery}", lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                _logger.LogDebug("{@ObjectQuery}", lcQuery);
                _logger.LogInfo("Exec query to inform framework that process upload is finished");
                loDb.SqlExecNonQuery(lcQuery);
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

        }
    }
}
