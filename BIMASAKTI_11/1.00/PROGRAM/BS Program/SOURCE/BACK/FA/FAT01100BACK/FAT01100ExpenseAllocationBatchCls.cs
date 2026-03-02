using FAT01100Back.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_OpenTelemetry;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAT01100Common.DTOs;
using System.Data.SqlClient;

namespace FAT01100Back
{
    public class FAT01100ExpenseAllocationBatchCls : R_IBatchProcessAsync
    {
        RSP_FAT01100_SAVE_TRANS_EXP_ALLOCResources.Resources_Dummy_Class loRsp2 = new();
        private readonly ActivitySource _activitySource;
        private readonly LoggerFAT01100 _logger;
        public FAT01100ExpenseAllocationBatchCls()
        {
            _logger = LoggerFAT01100.R_GetInstanceLogger();
            _activitySource = R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public async Task R_BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity? loActivity = _activitySource.StartActivity("R_BatchProcessAsync");
            R_Exception loException = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                _logger.LogInfo("Test Connection");
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }
                _logger.LogInfo("Start Batch");
                _ = _BatchProcessAsync(poBatchProcessPar); // Fire and forget
                _logger.LogInfo("End Batch");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loDb != null)
                {
                    loDb = null;
                }
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            await Task.CompletedTask; // Satisfy async requirement for fire-and-forget pattern
        }

        private async Task _BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            using var Activity = _activitySource.StartActivity(nameof(_BatchProcessAsync));
            _logger.LogInfo(string.Format("START process method {0} on Cls", nameof(_BatchProcessAsync)));
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loCommand = loDb.GetCommand();
                loConn = await loDb.GetConnectionAsync();
                R_ExternalException.R_SP_Init_Exception(loConn);
                //Get data from poBatchPRocessParam
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<FAT01100ExpenseAllocationBatchListDisplayDTO>>(poBatchProcessPar.BigObject);

                var loCDEPT_CODE = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(FAT01100BatchContextConstant.CDEPT_CODE)).FirstOrDefault().Value;
                var lcCDEPT_CODE = ((System.Text.Json.JsonElement)loCDEPT_CODE).GetString();

                var loCTRANSACTION_CODE = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(FAT01100BatchContextConstant.CTRANSACTION_CODE)).FirstOrDefault().Value;
                var lcCTRANSACTION_CODE = ((System.Text.Json.JsonElement)loCTRANSACTION_CODE).GetString();

                var loCREF_NO = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(FAT01100BatchContextConstant.CREF_NO)).FirstOrDefault().Value;
                var lcCREF_NO = ((System.Text.Json.JsonElement)loCREF_NO).GetString();

                var loCASSET_CODE = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(FAT01100BatchContextConstant.CASSET_CODE)).FirstOrDefault().Value;
                var lcCASSET_CODE = ((System.Text.Json.JsonElement)loCASSET_CODE).GetString();

                var loCTRANS_SEQ_NO = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(FAT01100BatchContextConstant.CTRANS_SEQ_NO)).FirstOrDefault().Value;
                var lcCTRANS_SEQ_NO = ((System.Text.Json.JsonElement)loCTRANS_SEQ_NO).GetString();

                var loCPARENT_ID = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(FAT01100BatchContextConstant.CPARENT_ID)).FirstOrDefault().Value;
                var lcCPARENT_ID = ((System.Text.Json.JsonElement)loCPARENT_ID).GetString();

                lcQuery = "CREATE TABLE #FAT01100_EXP_ALLOC_LIST(" +
                   "CEXPENSE_DEPT_CODE   VARCHAR(20)" +
                   ",CEXPENSE_DEPT_NAME   VARCHAR(100)" +
                   ",NEXPENSE_PCT         NUMERIC(5,2)" +
                   ") ";


                _logger.LogDebug("{@ObjectQuery} ", lcQuery);

                await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

                for (var i = 0; i < loObject.Count; i++)
                {
                    _logger.LogDebug($"INSERT INTO #FAT01100_EXP_ALLOC_LIST " +
                                     $"VALUES (" +
                                     $"'{loObject[i].CEXPENSE_DEPT_CODE}', " +
                                     $"'{loObject[i].CEXPENSE_DEPT_NAME}', " +
                                     $"{loObject[i].NEXPENSE_PCT}, " +
                                     $")");
                }

                await loDb.R_BulkInsertAsync((SqlConnection)loConn, "#FAT01100_EXP_ALLOC_LIST", loObject);
                lcQuery = "RSP_FAT01100_SAVE_TRANS_EXP_ALLOC "; 
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, lcCDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 20, lcCREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CASSET_CODE", DbType.String, 50, lcCASSET_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_SEQ_NO", DbType.String, 8, lcCTRANS_SEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CPARENT_ID", DbType.String, 50, lcCPARENT_ID);
                //loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);
                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCommand.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                    throw;
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            if (loException.Haserror)
            {
                string lcMessageError = loException.ErrorList[0].ErrDescp.Replace("'", "`");
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, lcMessageError);
                await loDb.SqlExecNonQueryAsync(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", lcMessageError);

                await loDb.SqlExecNonQueryAsync(lcQuery);
            }
            _logger.LogInfo(string.Format("End process method on Cls", nameof(_BatchProcessAsync)));
            loException.ThrowExceptionIfErrors();
        }
    }
}
