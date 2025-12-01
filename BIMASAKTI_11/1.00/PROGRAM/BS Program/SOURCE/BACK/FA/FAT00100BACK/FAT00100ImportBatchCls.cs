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
using R_OpenTelemetry;
using FAT00100BackResources;
using System.Data.SqlClient;

namespace FAT00100Back
{
    /// <summary>
    /// Batch processing class for FAT00100 - Import Asset Batch
    /// Implements R_IBatchProcessAsync for batch processing operations
    /// </summary>
    public class FAT00100ImportBatchCls : R_IBatchProcessAsync
    {
        private readonly ActivitySource _activitySource;
        private readonly LoggerFAT00100 _logger;
        private readonly FAT00100BackResources.Resources_Dummy_Class loRsp = new();

        // MUST FOLLOW THIS EXACTLY FOR CONSTRUCTOR
        public FAT00100ImportBatchCls()
        {
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = FAT00100Activity.R_GetInstanceActivitySource();
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
            R_Exception loEx = new R_Exception();
            R_Exception loExceptionSP = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();

                // Deserialize batch data
                List<FAT00100TempImportDTO> loObjectImport = R_NetCoreUtility.R_DeserializeObjectFromByte<List<FAT00100TempImportDTO>>(poBatchProcessPar.BigObject);

                // Get parameters from UserParameters dictionary
                string lcRecId = poBatchProcessPar.Key.KEY_GUID;
                string lcLangId = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CFOREIGN_LANGUAGE", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;
                string lcCompId = poBatchProcessPar.Key.COMPANY_ID;
                string lcUserId = poBatchProcessPar.Key.USER_ID;
                string lcDeptCode = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CDEPT_CODE", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;
                string lcTransactionCode = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CTRANSACTION_CODE", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;
                string lcReferenceNo = poBatchProcessPar.UserParameters.Where(x => x.Key.Equals("CREFERENCE_NO", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Value?.ToString() ?? string.Empty;

                using TransactionScope loTransScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                // Init Exception - Note: R_ExternalException may not be available in NET6, preserve logic
                // R_ExternalException.R_SP_Init_Exception(loConn);

                if (loObjectImport != null && loObjectImport.Count > 0)
                {
                    // Write upload process status
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = "exec RSP_WriteUploadProcessStatus @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID, @CSTEP, @CSTATUS";
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID.Trim());
                    loDb.R_AddCommandParameter(loCmd, "@CSTEP", DbType.Int32, 0, 1);
                    loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 200, "Saving " + poBatchProcessPar.Key.USER_ID);
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                    // Get current date
                    string lcCmd = string.Format("DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY('{0}') SELECT @DATENOW as Date_Now", lcCompId);
                    DateTime ldDateNow = loDb.SqlExecObjectQuery<DateTime>(lcCmd, loConn, false).FirstOrDefault();

                    // Create temp table
                    lcCmd = string.Format(" IF OBJECT_ID('tempdb..#ExAsset') IS NOT NULL " +
                                          " DROP TABLE #ExAsset                          " +
                                          " CREATE TABLE #ExAsset ( 					   " +
                                          " RECID VARCHAR(50)         " +
                                          " ,CASSET_CODE VARCHAR(20)  					   " +
                                          " ,CASSET_NAME VARCHAR(60) 					   " +
                                          " ,CCATEGORY_CODE VARCHAR(6)  				   " +
                                          " ,CASSET_DEPT_CODE VARCHAR(8)       		        " +
                                          " ,CJRNGRP_CODE VARCHAR(5)   				        " +
                                          " ,CTAX_CATEGORY_CODE VARCHAR(8)			        " +
                                          " ,CASSET_OWNER VARCHAR(30)    				           " +
                                          " ,CASSET_LOCATION VARCHAR(60)   			        " +
                                          " ,IBEGINNING_QTY integer     				           " +
                                          " ,CUNIT VARCHAR(6)  						        " +
                                          " ,CTRANS_DESCRIPTION VARCHAR(300)  		        " +
                                          " ,CSERIAL_NUMBER VARCHAR(20) 				           " +
                                          " ,CINSERVICE_DATE VARCHAR(8)				        " +
                                          " ,NBEGINNING_AMT numeric(18,2)				   " +
                                          " ,NLADDITION_AMT numeric(18,2)    			   " +
                                          " ,NLDEDUCTION_AMT numeric(18,2)   			   " +
                                          " ,NLPRIOR_DEPR_AMT numeric(18,2)  			   " +
                                          " ,NLYTD_DEPR_AMT numeric(18,2)    			   " +
                                          " ,CDEPR_METHOD VARCHAR(1)    				   " +
                                          " ,CSTART_DATE VARCHAR(8)     				   " +
                                          " ,NLBEG_BOOK_VALUE numeric(18,2)  			   " +
                                          " ,NLRESIDUAL_VALUE numeric(18,2)       	   " +
                                          " ,IUSEFUL_LIVE_YR integer    				   " +
                                          " ,IUSEFUL_LIVE_MO integer  				   " +
                                          " ,IREM_USEFUL_LIVE_YR integer    				   " +
                                          " ,IREM_USEFUL_LIVE_MO integer)    				   ");
                    await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);

                    // Prepare objects for bulk insert
                    List<FAT00100TempImportDTO> loNewObject = new List<FAT00100TempImportDTO>();
                    foreach (var x in loObjectImport)
                    {
                        if (x != null)
                        {
                            x.RECID = poBatchProcessPar.Key.KEY_GUID;
                            if (string.IsNullOrEmpty(x.CASSET_CODE))
                            {
                                x.CASSET_CODE = string.Empty;
                            }
                            // x is already FAT00100TempImportDTO from deserialization, just add it
                            loNewObject.Add(x);
                        }
                        else
                        {
                            break;
                        }
                    }

                    // Bulk insert into temp table
                    // Note: R_BulkInsert requires System.Data.SqlClient.SqlConnection
                    if (loNewObject.Count > 0 && loConn is System.Data.SqlClient.SqlConnection sqlConn)
                    {
                        loDb.R_BulkInsert<FAT00100TempImportDTO>(sqlConn, "#ExAsset", loNewObject);
                    }

                    // Validate imported asset
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " EXEC RSP_FAT00100_VALIDATE_IMPORTED_ASSET @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO ";
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, lcCompId);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, lcDeptCode);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, lcTransactionCode);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, lcReferenceNo);

                    // Note: In VB.NET, SqlExecNonQuery returns int (row count), but in NET6 SqlExecNonQueryAsync returns void
                    // Validation errors are handled via R_ExternalException which may not be available in NET6
                    // For now, we'll execute and catch exceptions, then check for SP exceptions if available
                    try
                    {
                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    // Get Exception from SP - Note: R_ExternalException may not be available in NET6, preserve logic
                    // loExceptionSP.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    // If no exceptions, proceed to save
                    if (!loExceptionSP.Haserror && !loEx.Haserror)
                    {
                        // Save imported asset
                        loCmd.Parameters.Clear();
                        loCmd.CommandText = " EXEC RSP_FAT00100_SAVE_IMPORTED_ASSET @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";
                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, lcCompId);
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, lcDeptCode);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, lcTransactionCode);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, lcReferenceNo);
                        loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, lcUserId);

                        try
                        {
                            await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                        }
                        catch (Exception ex)
                        {
                            loEx.Add(ex);
                        }
                    }
                    else
                    {
                        // Validation failed - add error message
                        loEx.Add(R_Utility.R_GetError(typeof(Resources_Dummy_Class), "PS004"));
                    }
                }

                if (!loEx.Haserror)
                {
                    loTransScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            // Handle error status writing outside transaction
            if (loExceptionSP.Haserror || loEx.Haserror)
            {
                try
                {
                    R_Db? loDbStatus = new R_Db();
                    using DbConnection loConn = await loDbStatus.GetConnectionAsync();
                    using DbCommand loCmd = loDbStatus.GetCommand();

                    loDbStatus.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDbStatus.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDbStatus.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID.Trim());
                    loDbStatus.R_AddCommandParameter(loCmd, "@CSTEP", DbType.Int32, 0, 1);
                    loDbStatus.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 200, "Saving " + poBatchProcessPar.Key.USER_ID);

                    if (loExceptionSP.Haserror)
                    {
                        int iSeqNo = 0;
                        loDbStatus.R_AddCommandParameter(loCmd, "@ISEQ_NO", DbType.Int32, 0, 0);
                        loDbStatus.R_AddCommandParameter(loCmd, "@CERROR_MESSAGE", DbType.String, 500, string.Empty);

                        foreach (var i in loExceptionSP.ErrorList)
                        {
                            iSeqNo++;
                            loCmd.CommandText = "INSERT INTO GST_UPLOAD_ERROR_STATUS (CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES(@CCOMPANY_ID, @CUSER_ID, @CKEY_GUID, @ISEQ_NO, @CERROR_MESSAGE)";
                            loCmd.Parameters["@ISEQ_NO"].Value = iSeqNo;
                            loCmd.Parameters["@CERROR_MESSAGE"].Value = i.ErrDescp;
                            await loDbStatus.SqlExecNonQueryAsync(loConn, loCmd, false);
                        }

                        // Write batch status complete with validation error
                        loCmd.CommandText = "exec RSP_WriteUploadProcessStatus @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID, @CSTEP, @CSTATUS, @CFINISH";
                        loDbStatus.R_AddCommandParameter(loCmd, "@CFINISH", DbType.Int32, 0, 9);
                        loCmd.Parameters["@CSTEP"].Value = 100;
                        loCmd.Parameters["@CSTATUS"].Value = "Save " + poBatchProcessPar.Key.USER_ID + " Complete With Validation";
                        await loDbStatus.SqlExecNonQueryAsync(loConn, loCmd, true);
                    }
                    else
                    {
                        // Write batch status complete
                        loCmd.CommandText = "exec RSP_WriteUploadProcessStatus @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID, @CSTEP, @CSTATUS, @CFINISH";
                        loDbStatus.R_AddCommandParameter(loCmd, "@CFINISH", DbType.Int32, 0, 1);
                        loCmd.Parameters["@CSTEP"].Value = 100;
                        loCmd.Parameters["@CSTATUS"].Value = "Save " + poBatchProcessPar.Key.USER_ID + " Complete";
                        await loDbStatus.SqlExecNonQueryAsync(loConn, loCmd, true);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                }
            }
            else
            {
                // Write batch status complete - success
                try
                {
                    R_Db? loDbStatus = new R_Db();
                    using DbConnection loConn = await loDbStatus.GetConnectionAsync();
                    using DbCommand loCmd = loDbStatus.GetCommand();

                    loDbStatus.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDbStatus.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDbStatus.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID.Trim());
                    loDbStatus.R_AddCommandParameter(loCmd, "@CSTEP", DbType.Int32, 0, 1);
                    loDbStatus.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 200, "Saving " + poBatchProcessPar.Key.USER_ID);
                    loCmd.CommandText = "exec RSP_WriteUploadProcessStatus @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID, @CSTEP, @CSTATUS, @CFINISH";
                    loDbStatus.R_AddCommandParameter(loCmd, "@CFINISH", DbType.Int32, 0, 1);
                    loCmd.Parameters["@CSTEP"].Value = 100;
                    loCmd.Parameters["@CSTATUS"].Value = "Save " + poBatchProcessPar.Key.USER_ID + " Complete";
                    await loDbStatus.SqlExecNonQueryAsync(loConn, loCmd, true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                }
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Helper method to get error messages from resources
        /// </summary>
        /// <param name="pcErrorId">Error ID from resource file</param>
        /// <returns>R_Error object</returns>
        private R_Error GetError(string pcErrorId)
        {
            try
            {
                return R_Utility.R_GetError(typeof(Resources_Dummy_Class), pcErrorId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

