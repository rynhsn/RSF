using PMT00500COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using System.Data.SqlClient;
using System.Globalization;
using System.Transactions;

namespace PMT00500BACK
{
    public class PMT00502Cls : R_IBatchProcess
    {
        private LoggerPMT00500 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT00502Cls()
        {
            _Logger = LoggerPMT00500.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        #region Lease Bacth
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("R_BatchProcess");
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                _Logger.LogInfo("Test Connection");
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                _Logger.LogInfo("Start Batch");
                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });
                _Logger.LogInfo("End Batch");

                //while (!loTask.IsCompleted)
                //{
                //    Thread.Sleep(100);
                //}

                //if (loTask.IsFaulted)
                //{
                //    loException.Add(loTask.Exception.InnerException != null ?
                //        loTask.Exception.InnerException :
                //        loTask.Exception);

                //    goto EndBlock;
                //}
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(ex);
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
        }
        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("_BatchProcess");
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.CULTURE);
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            string lcPropertyId;
            DataTable loDataTable;

            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                   
                    loConn = loDb.GetConnection();
                    loCmd = loDb.GetCommand();
                    PMT00500LeaseDTO loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<PMT00500LeaseDTO>(poBatchProcessPar.BigObject);

                    R_ExternalException.R_SP_Init_Exception(loConn);

                    _Logger.LogInfo("Start Bulk Insert");
                    #region Unit Bulk Insert
                    lcQuery = "CREATE TABLE #GRID_UNIT (" +
                                @"CUNIT_ID VARCHAR(20) NOT NULL DEFAULT ''
						    ,CFLOOR_ID VARCHAR(20) NOT NULL DEFAULT '')";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    loDb.R_BulkInsert<PMT00510MappingLeaseDTO>((SqlConnection)loConn, "#GRID_UNIT", loObject.UnitList);
                    #endregion

                    #region Utility Bulk Insert
                    lcQuery = "CREATE TABLE #GRID_UTILITY(" +
                        "CBUILDING_ID VARCHAR(20) NOT NULL DEFAULT '' " +
                        ",CFLOOR_ID VARCHAR(20) NOT NULL DEFAULT '' " +
                        ",CUNIT_ID VARCHAR(20) NOT NULL DEFAULT '' " +
                        ",CCHARGES_TYPE VARCHAR(2) NOT NULL " +
                        ",CCHARGES_ID VARCHAR(20) NOT NULL" +
                        ",CSEQ_NO VARCHAR(3) NOT NULL " +
                        ",CMETER_NO VARCHAR(50) NOT NULL)";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    loDb.R_BulkInsert<PMT00520MappingLeaseDTO>((SqlConnection)loConn, "#GRID_UTILITY", loObject.UtilityList);
                    #endregion

                    #region Charges Bulk Insert
                    lcQuery = "CREATE TABLE #GRID_CHARGES " +
                                @"(CSEQ_NO VARCHAR(3) NOT NULL, 
                            CUNIT_ID VARCHAR(20) NOT NULL DEFAULT '', 
                            CFLOOR_ID VARCHAR(20) NOT NULL DEFAULT '', 
							CBUILDING_ID VARCHAR(20) NOT NULL DEFAULT '', 
                            CCHARGES_TYPE VARCHAR(2) NOT NULL,  
							CCHARGES_ID VARCHAR(20) NOT NULL, 
							CSTART_DATE VARCHAR(8) NOT NULL, 
							CEND_DATE VARCHAR(8) NOT NULL)";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    loDb.R_BulkInsert<PMT00530MappingLeaseDTO>((SqlConnection)loConn, "#GRID_CHARGES", loObject.ChargesList);
                    #endregion
                    _Logger.LogInfo("End Bulk Insert");

                    lcQuery = "RSP_PM_MANTAIN_LEASE_OWNERSHIP";
                    loCmd.CommandType = CommandType.StoredProcedure;
                    loCmd.CommandText = lcQuery;

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, loObject.CPROPERTY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, loObject.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE_LOI);

                    loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, loObject.CREF_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, loObject.CREF_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, loObject.CBUILDING_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, loObject.CTENANT_ID);

                    loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, loObject.CSTART_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CEND_DATE", DbType.String, 8, loObject.CEND_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@IYEARS", DbType.Int32, 50, loObject.IYEARS);
                    loDb.R_AddCommandParameter(loCmd, "@IMONTHS", DbType.Int32, 50, loObject.IMONTHS);
                    loDb.R_AddCommandParameter(loCmd, "@IDAYS", DbType.Int32, 50, loObject.IDAYS);

                    loDb.R_AddCommandParameter(loCmd, "@CLEASE_MODE", DbType.String, 2, loObject.CLEASE_MODE);
                    loDb.R_AddCommandParameter(loCmd, "@CUNIT_DESCRIPTION", DbType.String, 255, loObject.CUNIT_DESCRIPTION);
                    loDb.R_AddCommandParameter(loCmd, "@CNOTES", DbType.String, int.MaxValue, loObject.CNOTES);

                    loDb.R_AddCommandParameter(loCmd, "@CLINK_TRANS_CODE", DbType.String, 30, ContextConstant.VAR_TRANS_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CLINK_REF_NO", DbType.String, 30, loObject.CLINK_REF_NO);

                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "ADD");

                    try
                    {
                        //Debug Logs
                        var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                        _Logger.LogDebug("EXEC RSP_PM_MANTAIN_LEASE_OWNERSHIP {@poParameter}", loDbParam);
                        //Console.WriteLine("This Guid : " + poBatchProcessPar.Key.KEY_GUID);

                        loDb.SqlExecNonQuery(loConn, loCmd, false);
                    }
                    catch (Exception ex)
                    {
                        loException.Add(ex);
                    }
                    loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
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

            string lcMessageError = "";
            if (loException.Haserror)
            {
                for (int i = 0; i < loException.ErrorList.Count; i++)
                {
                    lcMessageError = loException.ErrorList[i].ErrDescp.Replace("'", "`");
                    lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                        string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                        string.Format("'{0}', {1}, '{2}')", poBatchProcessPar.Key.KEY_GUID, i+1,lcMessageError);
                    loDb.SqlExecNonQuery(lcQuery);
                }

                lcMessageError = R_Utility.R_GetMessage(typeof(RSP_PM_MANTAIN_LEASE_OWNERSHIPResources.Resources_Dummy_Class), "1501", loCultureInfo);
                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", lcMessageError);
                loDb.SqlExecNonQuery(lcQuery);
            }
            else
            {
                lcMessageError = R_Utility.R_GetMessage(typeof(RSP_PM_MANTAIN_LEASE_OWNERSHIPResources.Resources_Dummy_Class), "_SuccessBatch", loCultureInfo);
                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 1", lcMessageError);
                loDb.SqlExecNonQuery(lcQuery);
            }
        }
        #endregion
    }
}