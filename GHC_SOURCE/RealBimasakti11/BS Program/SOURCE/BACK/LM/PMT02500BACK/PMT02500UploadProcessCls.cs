using PMT02500Common.DTO._1._AgreementList.Upload;
using PMT02500Common.DTO._1._AgreementList.Upload.Agreement;
using PMT02500Common.DTO._1._AgreementList.Upload.Charges;
using PMT02500Common.DTO._1._AgreementList.Upload.Deposit;
using PMT02500Common.DTO._1._AgreementList.Upload.Unit;
using PMT02500Common.DTO._1._AgreementList.Upload.Utility;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PMT02500Back
{
    public class PMT02500UploadProcessCls : R_IBatchProcess
    {
        //RSP_PM_UPLOAD_TENANTResources.Resources_Dummy_Class _loRsp = new RSP_PM_UPLOAD_TENANTResources.Resources_Dummy_Class();
        private readonly ActivitySource _activitySource;

        public PMT02500UploadProcessCls()
        {
            _activitySource = PMT02500Activity.R_GetInstanceActivitySource();
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("R_BatchProcess")!;
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    _BatchProcess(poBatchProcessPar);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                });

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
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            using Activity activity = _activitySource.StartActivity("_BatchProcess")!;
            R_Db loDb = new R_Db();
            string lcQuery;
            R_Exception loException = new R_Exception();
            //int liFinishFlag = 0;
            string PropertyId;

            DbConnection? loConn = null;
            DbCommand? loCmd = null;
            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                //liFinishFlag = 1; //0=Process, 1=Success, 9=Fail
                UploadBigObjectParameterDTO loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<UploadBigObjectParameterDTO>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02500_UPLOAD_CONTEXT)).FirstOrDefault().Value;
                PropertyId = ((System.Text.Json.JsonElement)loVar).GetString()!;


                //List<UploadTenantSaveDTO> loParam = new List<UploadTenantSaveDTO>();

                //loParam = loObject.Select(item => new UploadTenantSaveDTO()
                //{
                //    NO = item.NO,
                //    TenantId = item.TenantId,
                //    TenantName = item.TenantName,
                //    Address = item.Address,
                //    City = item.City,
                //    Province = item.Province,
                //    Country = item.Country,
                //    ZipCode = item.ZipCode,
                //    Email = item.Email,
                //    PhoneNo1 = item.PhoneNo1,
                //    PhoneNo2 = item.PhoneNo2,
                //    TenantGroup = item.TenantGroup,
                //    TenantCategory = item.TenantCategory,
                //    TenantType = item.TenantType,
                //    JournalGroup = item.JournalGroup,
                //    PaymentTerm = item.PaymentTerm,
                //    Currency = item.Currency,
                //    Salesman = item.Salesman,
                //    LineOfBusiness = item.LineOfBusiness,
                //    FamilyCard = item.FamilyCard
                //}).ToList();

                lcQuery = "CREATE TABLE #LEASE_AGREEMENT " +
                        "(NO INT, " +
                        "CCOMPANY_ID VARCHAR(8), " +
                        "CPROPERTY_ID VARCHAR(20), " +
                        "CDEPT_CODE VARCHAR(20), " +
                        "CREF_NO VARCHAR(30), " +
                        "CREF_DATE CHAR(8), " +
                        "CBUILDING_ID VARCHAR(20), " +
                        "CDOC_NO VARCHAR(30), " +
                        "CDOC_DATE CHAR(8), " +
                        "CSTART_DATE VARCHAR(8), " +
                        "CEND_DATE VARCHAR(8), " +
                        "CMONTH VARCHAR(2), " +
                        "CYEAR VARCHAR(4), " +
                        "CDAY VARCHAR(4), " +
                        "CSALESMAN_ID VARCHAR(8), " +
                        "CTENANT_ID VARCHAR(20), " +
                        "CUNIT_DESCRIPTION NVARCHAR(255), " +
                        "CNOTES NVARCHAR(MAX), " +
                        "CCURRENCY_CODE VARCHAR(3), " +
                        "CLEASE_MODE CHAR(2), " +
                        "CCHARGE_MODE CHAR(2))";



                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadAgreementSaveDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT", loObject.AgreementList);

                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_UNIT(" +
                    "NO INT " +
                    ",CDOC_NO VARCHAR(30) " +
                    ",CUNIT_ID VARCHAR(20) " +
                    ",CFLOOR_ID VARCHAR(20) " +
                    ",CBUILDING_ID VARCHAR(20))";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadUnitSaveDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_UNIT", loObject.UnitList);

                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_UTILITY " +
                        "(NO INT, " +
                        //"CCOMPANY_ID VARCHAR(8), " +
                        //"CPROPERTY_ID VARCHAR(20), " +
                        //"CBUILDING_ID VARCHAR(20), " +
                        //"CDEPT_CODE VARCHAR(20), " +
                        //"CREF_NO VARCHAR(30), " +
                        "CDOC_NO VARCHAR(30), " +
                        "CUTILITY_TYPE VARCHAR(10), " +
                        "CUNIT_ID VARCHAR(20), " +
                        "CMETER_NO VARCHAR(30), " +
                        "CCHARGES_ID VARCHAR(30), " +
                        "CTAX_ID VARCHAR(30))";




                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadUtilitySaveDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_UTILITY", loObject.UtilityList);

                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_CHARGES " +
                        "(NO INT, " +
                        //"CCOMPANY_ID VARCHAR(8), " +
                        //"CPROPERTY_ID VARCHAR(20), " +
                        //"CBUILDING_ID VARCHAR(20), " +
                        //"CDEPT_CODE VARCHAR(20), " +
                        //"CREF_NO VARCHAR(30), " +
                        "CDOC_NO VARCHAR(30), " +
                        "CCHARGES_ID VARCHAR(30), " +
                        "CTAX_ID VARCHAR(30), " +
                        "IYEARS INT, " +
                        "IMONTHS INT, " +
                        "IDAYS INT, " +
                        "LBASED_OPEN_DATE BIT, " +
                        "CSTART_DATE VARCHAR(8), " +
                        "CEND_DATE VARCHAR(8), " +
                        "CBILLING_MODE VARCHAR(30), " +
                        "CCURENCY_CODE VARCHAR(30), " +
                        "CFEE_METHOD VARCHAR(30), " +
                        "NFEE_AMT NUMERIC(18,2), " +
                        "CPERIOD_MODE VARCHAR(30), " +
                        "LPRORATE BIT, " +
                        "CDESCRIPTION NVARCHAR(MAX))";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadChargesSaveDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_CHARGES", loObject.ChargesList);

                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_DEPOSIT " +
                        "(NO INT, " +
                        //"CCOMPANY_ID VARCHAR(8), " +
                        //"CPROPERTY_ID VARCHAR(20), " +
                        //"CBUILDING_ID VARCHAR(20), " +
                        //"CDEPT_CODE VARCHAR(20), " +
                        //"CREF_NO VARCHAR(30), " +
                        "CDOC_NO VARCHAR(30), " +
                        "LCONTRACTOR BIT, " +
                        "CCONTRACTOR_ID VARCHAR(30), " +
                        "CDEPOSIT_ID VARCHAR(30), " +
                        "CDEPOSIT_DATE VARCHAR(30), " +
                        "CCURRENCY_CODE VARCHAR(3), " +
                        "NDEPOSIT_AMT NUMERIC(18,2), " +
                        "LPAID BIT, " +
                        "CDESCRIPTION VARCHAR(30))";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadDepositSaveDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_DEPOSIT", loObject.DepositList);

                lcQuery = "EXEC RSP_PM_UPLOAD_LEASE_AGREEMENT " +
                    "@CCOMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CTRANS_CODE, " +
                    "@CUSER_ID, " +
                    "@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "802030");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;
                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
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
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp);
                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery);
            }
        }

    }
}
