using PMT00500COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using System.Data.SqlClient;

namespace PMT00500BACK
{
    public class PMT00501Cls : R_IBatchProcess
    {
        private LoggerPMT00500 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT00501Cls()
        {
            _Logger = LoggerPMT00500.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        #region Upload Bacth
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
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            string lcPropertyId;

            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                PMT00500UploadBigObjectParameterDTO loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<PMT00500UploadBigObjectParameterDTO>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString()!;

                _Logger.LogInfo("Start Bulk Insert");
                #region Agreement Bulk Insert
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT " +
                          @"( NO INT, 
                              CCOMPANY_ID VARCHAR(8), 
							  CPROPERTY_ID VARCHAR(20), 
							  CDEPT_CODE VARCHAR(20), 
							  CREF_NO VARCHAR(30), 
							  CREF_DATE CHAR(8), 
							  CBUILDING_ID VARCHAR(20), 
							  CDOC_NO VARCHAR(30), 
							  CDOC_DATE CHAR(8), 
							  CSTART_DATE VARCHAR(8), 
							  CEND_DATE VARCHAR(8), 
							  CMONTH VARCHAR(2), 
							  CYEAR VARCHAR(4), 
							  CDAY VARCHAR(4), 
							  CSALESMAN_ID VARCHAR(8), 
							  CTENANT_ID VARCHAR(20), 
							  CUNIT_DESCRIPTION NVARCHAR(255), 
							  CNOTES NVARCHAR(MAX), 
							  CCURRENCY_CODE VARCHAR(3), 
							  CLEASE_MODE CHAR(2), 
							  CCHARGE_MODE CHAR(2), 
							  ISEQ_NO_ERROR INT,
                              LERROR BIT,
                              CHO_PLAN_DATE VARCHAR(8),
                              CBILLING_RULE_CODE VARCHAR(20),
                              NBOOKING_FEE NUMERIC(18,2),
                              CTC_CODE VARCHAR(20), 
                              IHOURS VARCHAR(4) DEFAULT ''
                              )";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<PMT00500UploadAgreementDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT", loObject.AgreementList);
                #endregion

                #region Unit Bulk Insert
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_UNIT (" +
                            @"NO INT 
						    ,CDOC_NO VARCHAR(30) 
						    ,CUNIT_ID VARCHAR(20) 
						    ,CFLOOR_ID VARCHAR(20) 
						    ,CBUILDING_ID VARCHAR(20) 
						    ,ISEQ_NO_ERROR INT
                            ,LERROR BIT )";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<PMT00500UploadUnitDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_UNIT", loObject.UnitList);
                #endregion

                #region Utility Bulk Insert
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_UTILITY " +
                            @"(NO INT, 
                            CDOC_NO VARCHAR(30), 
							CUTILITY_TYPE VARCHAR(10), 
							CUNIT_ID VARCHAR(20), 
							CMETER_NO VARCHAR(30), 
							CCHARGES_ID VARCHAR(30), 
							CTAX_ID VARCHAR(30), 
							ISEQ_NO_ERROR INT,
                            LERROR BIT )";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<PMT00500UploadUtilitiesDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_UTILITY", loObject.UtilityList);
                #endregion

                #region Charges Bulk Insert
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_CHARGES " +
                            @"(NO INT, 
                            CDOC_NO VARCHAR(30), 
                            CUNIT_ID VARCHAR(30), 
                            CFLOOR_ID VARCHAR(30), 
							CCHARGES_ID VARCHAR(30), 
							CTAX_ID VARCHAR(30), 
							IYEARS INT,
                            IMONTHS INT, 
							IDAYS INT,
                            LBASED_OPEN_DATE BIT, 
							CSTART_DATE VARCHAR(8), 
							CEND_DATE VARCHAR(8), 
							CBILLING_MODE VARCHAR(30), 
							CCURENCY_CODE VARCHAR(30), 
							CFEE_METHOD VARCHAR(30), 
							NFEE_AMT NUMERIC(18,2), 
							CPERIOD_MODE VARCHAR(30), 
							LPRORATE BIT,
                            CDESCRIPTION NVARCHAR(MAX), 
							ISEQ_NO_ERROR INT,
                            LERROR BIT )";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<PMT00500UploadChargesDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_CHARGES", loObject.ChargesList);
                #endregion

                #region Deposit Bulk Insert
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_DEPOSIT " +
                        "(NO INT, " +
                        "CDOC_NO VARCHAR(30), " +
                        "LCONTRACTOR BIT, " +
                        "CCONTRACTOR_ID VARCHAR(30), " +
                        "CDEPOSIT_ID VARCHAR(30), " +
                        "CDEPOSIT_DATE VARCHAR(30), " +
                        "CCURRENCY_CODE VARCHAR(3), " +
                        "NDEPOSIT_AMT NUMERIC(18,2), " +
                        "LPAID BIT, " +
                        "CDESCRIPTION VARCHAR(30), " +
                        "ISEQ_NO_ERROR INT," +
                        "LERROR BIT )";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<PMT00500UploadDepositDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_DEPOSIT", loObject.DepositList);
                #endregion
                _Logger.LogInfo("End Bulk Insert");

                lcQuery = "EXEC RSP_PM_UPLOAD_LEASE_AGREEMENT " +
                    "@CCOMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CTRANS_CODE, " +
                    "@CUSER_ID, " +
                    "@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, lcPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_UPLOAD_LEASE_AGREEMENT {@poParameter}", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, false);
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

            if (loException.Haserror)
            {
                string lcMessageError = loException.ErrorList[0].ErrDescp.Replace("'", "`");
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, lcMessageError);
                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", lcMessageError);

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
        #endregion
    }
}