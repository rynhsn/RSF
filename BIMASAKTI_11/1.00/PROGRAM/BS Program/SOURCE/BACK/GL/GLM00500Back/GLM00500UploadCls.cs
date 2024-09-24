using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Transactions;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00500Back;

public class GLM00500UploadCls : R_IBatchProcess
{
    
    RSP_GL_SAVE_BUDGET_DTResources.Resources_Dummy_Class _rscSaveDT = new();
    RSP_GL_DELETE_BUDGET_DTResources.Resources_Dummy_Class _rscDeleteDT = new();
    RSP_GL_GENERATE_ACCOUNT_BUDGETResources.Resources_Dummy_Class _rscGenerateAccount = new();
    
    
    private readonly ActivitySource _activitySource;
    private readonly LoggerGLM00500 _logger;

    public GLM00500UploadCls()
    {
        _logger = LoggerGLM00500.R_GetInstanceLogger();
        _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
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
            var lcGuid = poBatchProcessPar.Key.KEY_GUID;
            var lcCompanyId = poBatchProcessPar.Key.COMPANY_ID;
            var lcUserId = poBatchProcessPar.Key.USER_ID;

            //log info
            _logger.LogInfo("Get big object from batch process parameter");
            var loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<GLM00500UploadForSystemDTO>>(poBatchProcessPar
                    .BigObject);
            var loObject =
                R_Utility.R_ConvertCollectionToCollection<GLM00500UploadForSystemDTO, GLM00500UploadToSystemDTO>(
                    loTempObject);

            loConn = loDb.GetConnection();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            _logger.LogInfo("Create Temp Table for Budget Upload");
            var lcQuery = $"CREATE TABLE #GLM00500_BUDGET_UPLOAD( " +
                          $"SEQ_NO INT, " +
                          $"BUDGET_YEAR VARCHAR(10), " +
                          $"BUDGET_NO VARCHAR(20), " +
                          $"BUDGET_NAME NVARCHAR(100), " +
                          $"CURRENCY_TYPE VARCHAR(10), " +
                          $"ACCOUNT_TYPE VARCHAR(10), " +
                          $"ACCOUNT_NO VARCHAR(20), " +
                          $"CENTER VARCHAR(10), " +
                          $"PERIOD_1 NUMERIC(19, 2), " +
                          $"PERIOD_2 NUMERIC(19, 2), " +
                          $"PERIOD_3 NUMERIC(19, 2), " +
                          $"PERIOD_4 NUMERIC(19, 2), " +
                          $"PERIOD_5 NUMERIC(19, 2), " +
                          $"PERIOD_6 NUMERIC(19, 2), " +
                          $"PERIOD_7 NUMERIC(19, 2), " +
                          $"PERIOD_8 NUMERIC(19, 2), " +
                          $"PERIOD_9 NUMERIC(19, 2), " +
                          $"PERIOD_10 NUMERIC(19, 2), " +
                          $"PERIOD_11 NUMERIC(19, 2), " +
                          $"PERIOD_12 NUMERIC(19, 2), " +
                          $"PERIOD_13 NUMERIC(19, 2), " +
                          $"PERIOD_14 NUMERIC(19, 2), " +
                          $"PERIOD_15 NUMERIC(19, 2), " +
                          $"VALID VARCHAR(10), " +
                          $"NOTES NVARCHAR(2000), " +
                          $"CREC_ID VARCHAR(50), " +
                          $")";
            loDb.SqlExecNonQuery(lcQuery, loConn, false);
            
            _logger.LogInfo("Bulk Insert to Temp Table for Budget Upload");
            loDb.R_BulkInsert((SqlConnection)loConn, "#GLM00500_BUDGET_UPLOAD", loObject);

            lcQuery = $"EXEC RSP_GL_VALIDATE_BUDGET_UPLOAD '{lcUserId}', '{lcCompanyId}', '{lcGuid}'";

            _logger.LogInfo("Execute RSP_GL_VALIDATE_BUDGET_UPLOAD");
            var loCheckError = loDb.SqlExecObjectQuery<GLM00500UploadCheckErrorDTO>(lcQuery, loConn, false)
                .FirstOrDefault();

            _logger.LogInfo("Check Error");
            if (loCheckError.IERROR_COUNT == 0)
            {
                _logger.LogInfo("Has no error, continue to save data");
                
                lcQuery = $"EXEC RSP_GL_SAVE_BUDGET_UPLOAD '{lcUserId}', '{lcCompanyId}', '{lcGuid}'";
                // loDb.SqlExecNonQuery(lcQuery, loConn, false);
                try
                {
                    _logger.LogInfo("Execute RSP_GL_SAVE_BUDGET_UPLOAD");
                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                _logger.LogInfo("Finish RSP_GL_SAVE_BUDGET_UPLOAD");
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