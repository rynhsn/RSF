using PMT01500Common.Context;
using PMT01500Common.DTO._1._AgreementList.Upload;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.Json;

namespace PMT01500Back
{
    public class PMT01500UploadProcessCls : R_IBatchProcess
    {
        //readonly ResourceDum _oRsp = new RSP_GS_UPLOAD_CASH_BANKResources.Resources_Dummy_Class();
        private readonly ActivitySource _activitySource;

        public PMT01500UploadProcessCls()
        {
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(nameof(R_BatchProcess));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();
            Task loTask;

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                loTask = Task.Run(() =>
                {
                    _ = _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(nameof(_BatchProcess));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection? loConn = null;
            DbCommand? loCommand = null;

            List<PMT01500UploadErrorValidateDTO>? loTempObject;
            List<PMT01500TemporaryUploadToDbDTO>? loObject;

            /*
            object lcTempBankType;
            string? lcParBankType;
            */

            object lcTempPropertyId;
            string? lcParPropertyId;


            try
            {
                await Task.Delay(100);

                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                //Get data from poBatchPRocessParam
                loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMT01500UploadErrorValidateDTO>>(poBatchProcessPar.BigObject);
                //CONVERT DATA, SO TO BE READY INSERT TO TEMPORARY TABLE 
                loObject = (List<PMT01500TemporaryUploadToDbDTO>?)R_Utility
                    .R_ConvertCollectionToCollection<PMT01500UploadErrorValidateDTO, PMT01500TemporaryUploadToDbDTO>(loTempObject);


                #region GetParameterProperty
                //get parameter
                /*
                lcTempBankType = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CBANK_TYPE)).FirstOrDefault().Value;
                lcParBankType = ((JsonElement)lcTempBankType).GetString();
                */

                lcTempPropertyId = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID)).FirstOrDefault().Value;
                lcParPropertyId = ((JsonElement)lcTempPropertyId).GetString();
                #endregion

                lcQuery = $"CREATE TABLE #LEASE_AGREEMENT(NO INT " +
                    $",CCOMPANY_ID	VARCHAR(8) " +
                    $",CPROPERTY_ID VARCHAR(20)		" +
                    $",CDEPT_CODE VARCHAR(20)		" +
                    $",CREF_NO VARCHAR(30)		" +
                    $",CREF_DATE CHAR(8)			" +
                    $",CBUILDING_ID VARCHAR(20)		" +
                    $",CDOC_NO VARCHAR(30)		" +
                    $",CDOC_DATE CHAR(8)			" +
                    $",CSTART_DATE VARCHAR(8)		" +
                    $",CEND_DATE VARCHAR(8)		" +
                    $",CMONTH VARCHAR(2)		" +
                    $",CYEAR VARCHAR(4)		" +
                    $",CDAY VARCHAR(4)		" +
                    $",CSALESMAN_ID VARCHAR(8)		" +
                    $",CTENANT_ID VARCHAR(20)	" +
                    $",CUNIT_DESCRIPTION NVARCHAR(255)	" +
                    $",CNOTES NVARCHAR(MAX) " +
                    $",CCURRENCY_CODE VARCHAR(3) " +
                    $",CLEASE_MODE CHAR(2)			" +
                    $",CCHARGE_MODE CHAR(2) " +
                    $" )";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<PMT01500TemporaryUploadToDbDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT", loObject);

                lcQuery = "RSP_PM_UPLOAD_LEASE_AGREEMENT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcParPropertyId);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 20, "802030");
                loDb.SqlExecNonQuery(loConn, loCommand, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
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

            if (loException.Haserror)
            {
                lcQuery = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                 $"VALUES " +
                 $"( '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}', {100}, '{loException.ErrorList[0].ErrDescp}' );";

                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(loConn, loCommand, false);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                          $"'{poBatchProcessPar.Key.USER_ID}', " +
                          $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                          $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }

    }
}
