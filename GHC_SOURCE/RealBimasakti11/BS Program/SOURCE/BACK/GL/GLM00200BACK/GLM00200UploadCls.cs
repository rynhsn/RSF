using GLM00200COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using GLM00200COMMON.DTO_s;

namespace GLM00200BACK
{
    public class GLM00200UploadCls : R_IBatchProcess
    {
        //var
        private RSP_GL_RECURRING_UPLOADResources.Resources_Dummy_Class _rsp = new();
        private LoggerGLM00200 _logger;
        private readonly ActivitySource _activitySource;

        //method
        public GLM00200UploadCls()
        {
            _logger = LoggerGLM00200.R_GetInstanceLogger();
            _activitySource = GLM00200Activity.R_GetInstanceActivitySource();
        }
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using var Activity = _activitySource.StartActivity(nameof(R_BatchProcess));
            var loEx = new R_Exception();
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                _logger.LogInfo("START process method R_BatchProcess on Cls");

                _logger.LogInfo("Get big object from batch process parameter");
                var loObject =
                    R_NetCoreUtility.R_DeserializeObjectFromByte<List<RecurringUploadDTO>>(poBatchProcessPar
                        .BigObject);


                loConn = loDb.GetConnection();

                R_ExternalException.R_SP_Init_Exception(loConn);

                _logger.LogInfo("Create Temp Table for Recurring Upload");
                var lcQuery = $"CREATE TABLE #GLM00200_RECURRING_UPLOAD( " +
                    "SEQ_NO INT" +
                    ",Department VARCHAR(20)" +
                    ",Recurring_No VARCHAR(30)" +
                    ",Recurring_Name NVARCHAR(200)" +
                    ",Document_No VARCHAR(30)" +
                    ",Currency_Code VARCHAR(3)" +
                    ",Fixed_Rate INT" +
                    ",Local_Currency_Base Rate NUMERIC(19,2)" +
                    ",Local_Currency_Rate NUMERIC(19,2)" +
                    ",Base_Currency_Base_Rate NUMERIC(19,2)" +
                    ",Base_Currency_Rate NUMERIC(19,2)" +
                    ",Frequency INT" +
                    ",Interval INT" +
                    ",Start_Date VARCHAR(8)" +
                    ",Account_No VARCHAR(20)" +
                    ",Center_Code VARCHAR(10)" +
                    ",Debit_Amount NUMERIC(19,2)" +
                    ",Credit_Amount NUMERIC(19,2)" +
                    ",DBCR VARCHAR(1)" +
                    ",Description NVARCHAR(200)" +
                    ")";
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                _logger.LogInfo("Bulk Insert to Temp Table for Recurring Upload");
                loDb.R_BulkInsert((SqlConnection)loConn, "#GLM00200_RECURRING_UPLOAD", loObject);

                lcQuery = $"EXEC RSP_GL_VALIDATE_RECURRING_UPLOAD '{poBatchProcessPar.Key.USER_ID}', '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.KEY_GUID}'";

                _logger.LogInfo("Execute RSP_GL_VALIDATE_Recurring_UPLOAD");
                var loCheckError = loDb.SqlExecObjectQuery<RecurringCheckUploadDTO>(lcQuery, loConn, false)
                    .FirstOrDefault();

                _logger.LogInfo("Check Error");
                if (loCheckError.IERROR_COUNT == 0 || loCheckError!=null)
                {
                    _logger.LogInfo("Has no error, continue to save data");

                    lcQuery = $"EXEC RSP_GL_SAVE_RECURRING_UPLOAD '{poBatchProcessPar.Key.USER_ID}', '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.KEY_GUID}'";
                    // loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    try
                    {
                        _logger.LogInfo("Execute RSP_GL_SAVE_RECURRING_UPLOAD");
                        loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    _logger.LogInfo("Finish RSP_GL_SAVE_RECURRING_UPLOAD");
                    loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
