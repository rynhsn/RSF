using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using GLM00500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00500Back;

public class GLM00500UploadCls : R_IBatchProcess
{

    // public GLM00500UploadErrorDTO GetErrorMsg(GLM00500ParameterUploadDb poParam)
    // {
    //     R_Exception loEx = new R_Exception();
    //     GLM00500UploadErrorDTO loRtn = null;
    //     R_Db loDb;
    //     DbConnection loConn;
    //     DbCommand loCmd;
    //     string lcQuery;
    //
    //     try
    //     {
    //         loDb = new R_Db();
    //         loConn = loDb.GetConnection();
    //         loCmd = loDb.GetCommand();
    //
    //         lcQuery = "RSP_GL_GET_DATA_UPLOAD_VALIDATION_LIST";
    //         loCmd.CommandType = CommandType.StoredProcedure;
    //         loCmd.CommandText = lcQuery;
    //
    //         loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
    //
    //         var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
    //         loRtn = R_Utility.R_ConvertTo<GLM00500UploadErrorDTO>(loDataTable).FirstOrDefault();
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //
    //     return loRtn;
    // }

    public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        var loEx = new R_Exception();
        var loDb = new R_Db();
        DbConnection loConn = null;
        DbCommand loCmd = null;

        try
        {
            var lcGuid = poBatchProcessPar.Key.KEY_GUID;
            var lcCompanyId = poBatchProcessPar.Key.COMPANY_ID;
            var lcUserId = poBatchProcessPar.Key.USER_ID;
            
            var loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<GLM00500UploadForSystemDTO>>(poBatchProcessPar
                    .BigObject);
            var loObject = R_Utility.R_ConvertCollectionToCollection<GLM00500UploadForSystemDTO, GLM00500UploadToSystemDTO>(loTempObject);
            // using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
            // {
                loConn = loDb.GetConnection();
                // var lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{lcCompanyId}', " + 
                //               $"'{lcUserId}', " +
                //               $"'{lcGuid}', " +
                //               $"{0}, 'Process Start', 0"; //0=Process, 1=Success, 9=Fail

                // loDb.SqlExecNonQuery(lcQuery, loConn, false);

                var lcQuery =   $"CREATE TABLE #GLM00500_BUDGET_UPLOAD( " +
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
                loDb.R_BulkInsert((SqlConnection)loConn, "#GLM00500_BUDGET_UPLOAD", loObject);

                lcQuery = $"EXEC RSP_GL_VALIDATE_BUDGET_UPLOAD  '{lcUserId}', '{lcCompanyId}', '{lcGuid}'";

                var loCheckError = loDb.SqlExecObjectQuery<GLM00500UploadCheckErrorDTO>(lcQuery, loConn, false)
                    .FirstOrDefault();

                // if (loCheckError.IERROR_COUNT > 0)
                // {
                    // lcQuery =
                    //     $"SELECT CPROCES_ID = '{loCheckError.CPROCES_ID}', IERROR_COUNT = {loCheckError.IERROR_COUNT} ";
                    // lcQuery += "INTO #RHC_TEMPORARY";
                    // loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    //
                    // lcQuery =
                    //     $"EXEC RSP_ConvertTableToXML '{lcCompanyId}', '{lcUserId}', '{lcGuid}', '#RHC_TEMPORARY', 1";
                    // loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    // lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{lcCompanyId}', " + //dari parameter batch
                    //           $"'{lcUserId}', " +
                    //           $"'{lcGuid}', " +
                    //           $"{30}, 'Process Fail', 9"; //0=Process, 1=Success, 9=Fail
                    // loDb.SqlExecNonQuery(lcQuery, loConn, false);
                // }
                
                if(loCheckError.IERROR_COUNT == 0)
                {
                    // R_ExternalException.R_SP_Init_Exception(loConn);

                    lcQuery = $"EXEC RSP_GL_SAVE_BUDGET_UPLOAD '{lcUserId}', '{lcCompanyId}', '{lcGuid}'";
                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    //
                    // try
                    // {
                    //     loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    //     // loDb.SqlExecNonQuery(loConn, loCmd, false);
                    // }
                    // catch (Exception ex)
                    // {
                    //     loEx.Add(ex);
                    // }
                    //
                    // loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
                    //
                    // lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{lcCompanyId}', " + //dari parameter batch
                    //           $"'{lcUserId}', " +
                    //           $"'{lcGuid}', " +
                    //           $"{30}, 'Process Success', 1"; //0=Process, 1=Success, 9=Fail
                    // loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    
                    
                }

                // lcQuery = "SELECT * FROM #RHC_TEMPORARY";
                // var loResult = loDb.SqlExecObjectQuery<GLM00500UploadCheckErrorDTO>(lcQuery, loConn, true);

                // TransScope.Complete();
            // }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
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
    
    // public GLM00500UploadErrorReturnDTO GetUploadList(string pcKeyGuid, string pcCompanyId, string pcUserId)
    // {
    //     R_Exception loEx = new();
    //     GLM00500UploadErrorReturnDTO loRtn = new();
    //     DbConnection loConn;
    //
    //     try
    //     {
    //         var loResultUploadList = new List<GLM00500UploadForSystemDTO>();
    //         var loResultErrorList = new List<GLM00500UploadErrorDTO>();
    //         var loDb = new R_Db();
    //         loConn = loDb.GetConnection();
    //         var lcCompanyId = pcCompanyId;
    //         var lcUserId = pcUserId;
    //
    //         var lcQuery = $"EXEC RSP_ConvertXMLToTable '{lcCompanyId}', '{lcUserId}', '{pcKeyGuid}'";
    //         var loProcess = loDb.SqlExecObjectQuery<GLM00500UploadCheckErrorDTO>(lcQuery, loConn, false).FirstOrDefault();
    //
    //         // if (loProcess == null)
    //         //     return default;
    //
    //         // lcQuery = $"EXEC RSP_GL_GET_BUDGET_UPLOAD_LIST '{loProcess.CPROCES_ID}'";
    //         loResultUploadList = loDb.SqlExecObjectQuery<GLM00500UploadForSystemDTO>(lcQuery, loConn, false);
    //
    //         // foreach (var error in loResultUploadList)
    //         // {
    //         //     lcQuery = $"EXEC RSP_GL_GET_DATA_UPLOAD_VALIDATION_LIST '{error.CREC_ID}'";
    //         //     var loValidation = loDb.SqlExecObjectQuery<GLM00500UploadErrorDTO>(lcQuery, loConn, false);
    //         //
    //         //     loResultErrorList.AddRange(loValidation);
    //         // }
    //
    //         loRtn.UploadList = loResultUploadList;
    //         loRtn.ErrorList = loResultErrorList;
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //
    //     return loRtn;
    // }
}