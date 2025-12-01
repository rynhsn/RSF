using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FAT00100Back.DTOs;
using FAT00100Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Common;
using R_OpenTelemetry;

namespace FAT00100Back
{
    /// <summary>
    /// Batch processing class for FAT0010002 - Expense Allocation Batch
    /// Implements R_IBatchProcessAsync for batch processing operations
    /// </summary>
    public class FAT0010002BatchCls : R_IBatchProcessAsync
    {
        private readonly ActivitySource _activitySource;
        private readonly LoggerFAT00100 _logger;

        // MUST FOLLOW THIS EXACTLY FOR CONSTRUCTOR
        public FAT0010002BatchCls()
        {
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = R_LibraryActivity.R_GetInstanceActivitySource();
        }

        // MUST FOLLOW THIS EXACTLY FOR R_BATCHPROCESSASYNC
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
            string lcMethod = nameof(_BatchProcessAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            R_Exception loException = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                string lcCompanyId = poBatchProcessPar.Key.COMPANY_ID;

                // Get parameters from UserParameters dictionary
                string lcDeptCode = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CDEPT_CODE", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;
                string lcTransactionCode = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CTRANSACTION_CODE", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;
                string lcReferenceNo = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CREFERENCE_NO", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;
                string lcAssetCode = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CASSET_CODE", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;
                string lcAssetTransSeqNo = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CASSET_TRANS_SEQNO", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;

                using TransactionScope loTransScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                // Deserialize batch data
                List<FAT0010002CommonDTO> loObjectImport = R_NetCoreUtility.R_DeserializeObjectFromByte<List<FAT0010002CommonDTO>>(poBatchProcessPar.BigObject);

                // DELETE existing records
                loCmd.Parameters.Clear();
                string lcCmdDelete = " DELETE FAT_TRANS_EXP_ALLOC " +
                                          " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                          " AND CDEPT_CODE = @CDEPT_CODE " +
                                          " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                          " AND CREFERENCE_NO = @CREFERENCE_NO " +
                                          " AND CASSET_CODE = @CASSET_CODE " +
                                          " AND CASSET_TRANS_SEQNO = @CASSET_TRANS_SEQNO ";

                loCmd.CommandText = lcCmdDelete;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, lcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, lcDeptCode);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, lcTransactionCode);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, lcReferenceNo);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, lcAssetCode);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, lcAssetTransSeqNo);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                if (loObjectImport != null && loObjectImport.Count > 0)
                {
                    string lcCmdInsert = " DECLARE @DATENOW AS DATETIME = DBO.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                            " INSERT INTO FAT_TRANS_EXP_ALLOC " +
                            " (CCOMPANY_ID, CDEPT_CODE, CTRANSACTION_CODE, CREFERENCE_NO, CASSET_CODE, " +
                            " CASSET_TRANS_SEQNO, CEXPENSE_DEPT_CODE, NEXPENSE_PCT, COLD_FLAG, " +
                            " CCREATE_BY, DCREATE_DATE, CUPDATE_BY, DUPDATE_DATE) " +
                            " VALUES ";

                    loCmd.Parameters.Clear();
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, lcCompanyId);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, lcDeptCode);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, lcTransactionCode);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, lcReferenceNo);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, lcAssetCode);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, lcAssetTransSeqNo);
                    loDb.R_AddCommandParameter(loCmd, "@COLD_FLAG", DbType.String, 50, "0");
                    loDb.R_AddCommandParameter(loCmd, "@CCREATE_BY", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poBatchProcessPar.Key.USER_ID);

                    int countLoop = 1;
                    foreach (var saveParam in loObjectImport)
                    {
                        countLoop++;
                        // Note: Preserving original behavior - embeds values directly in SQL string
                        lcCmdInsert += string.Format("(@CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CASSET_CODE, " +
                                                      " @CASSET_TRANS_SEQNO, '{0}', '{1}', @COLD_FLAG, " +
                                                      " @CCREATE_BY, @DATENOW, @CUPDATE_BY, @DATENOW),",
                                                      saveParam.CEXPENSE_DEPT_CODE, saveParam.NEXPENSE_PCT);
                    }

                    if (!string.IsNullOrEmpty(lcCmdInsert))
                    {
                        // Remove trailing comma
                        lcCmdInsert = lcCmdInsert.Substring(0, lcCmdInsert.Length - 1);
                        loCmd.CommandText = lcCmdInsert;
                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    }
                }

                // Write batch status
                if (!loException.Haserror)
                {
                    string lcCmdStatus = string.Format("exec RSP_WriteUploadProcessStatus '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                        poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID, poBatchProcessPar.Key.KEY_GUID.Trim(), 1, "Save Complete", 1);
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = lcCmdStatus;
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, true);
                }
                else
                {
                    string lcCmdStatus = string.Format("exec RSP_WriteUploadProcessStatus N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}'",
                        poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID, poBatchProcessPar.Key.KEY_GUID.Trim(), 1, "Save Complete With Validation", 9);
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = lcCmdStatus;
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                }

                loTransScope.Complete();
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

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }
    }
}

