using PMT02000COMMON.Logs;
using PMT02000COMMON.Upload;
using PMT02000COMMON.Upload.Agreement;
using PMT02000COMMON.Upload.Unit;
using PMT02000COMMON.Upload.Utility;
using PMT02000COMMON.Utility;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMT02000Back
{
    public class PMT02000UploadCls : R_IBatchProcess
    {
        private LoggerPMT02000 _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_UPLOAD_HANDOVERResources.Resources_Dummy_Class _objectRSPUpload = new();

        public PMT02000UploadCls()
        {
            _logger = LoggerPMT02000.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(R_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                _logger.LogInfo(string.Format("START Test Connection", lcMethodName));
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("000", "Database Connection Failed");
                    _logger.LogError(loException);
                    goto EndBlock;
                }
                _logger.LogInfo(string.Format("Finish Test Connection", lcMethodName));
                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            try
            {
                await Task.Delay(100);

                loCommand = loDb.GetCommand();
                _logger.LogDebug(string.Format("Initialitation loCommand", loCommand));
                loConn = loDb.GetConnection();
                _logger.LogDebug(string.Format("Initialitation loConn", loCommand));

                //Get data from poBatchPRocessParam
                PMT0200MultiListDataDTO loDataMultiList = R_NetCoreUtility.R_DeserializeObjectFromByte<PMT0200MultiListDataDTO>(poBatchProcessPar.BigObject);
                _logger.LogDebug(string.Format("Get Multi List From Front "));
                _logger.LogDebug(string.Format("Get Multi List From Front ", loDataMultiList));

                #region GetParameterProperty
                //get parameter
                string? lcProperty = poBatchProcessPar.UserParameters
                                     .FirstOrDefault(x => x.Key.Equals(ContextConstant.CPROPERTY_ID))
                                     ?.Value is JsonElement jsonProperty ? jsonProperty.GetString() : null;
                string? lcTransCode = poBatchProcessPar.UserParameters
                                       .FirstOrDefault(x => x.Key.Equals(ContextConstant.CTRANS_CODE))
                                       ?.Value is JsonElement jsonTransCode ? jsonTransCode.GetString() : null;
                _logger.LogDebug(string.Format("Get Parameter Batch "));
                _logger.LogDebug(string.Format("Get Parameter Batch ", lcProperty));
                _logger.LogDebug(string.Format("Get Parameter Batch ", lcTransCode));
                #endregion

                #region Agreement
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT " +
                          @"( NO INT
                                ,CCOMPANY_ID VARCHAR(8)
                                , CPROPERTY_ID VARCHAR(20)
                                ,CTRANS_CODE VARCHAR(10)
                                ,CDEPT_CODE VARCHAR(20)
                                ,CLOI_REF_NO VARCHAR(30)
                                ,CBUILDING_ID VARCHAR(20)
                                ,CREF_NO VARCHAR(30)
                                ,CREF_DATE CHAR(8)
                                ,CHO_ACTUAL_DATE    VARCHAR(8)
                                ,ISEQ_NO_ERROR INT)";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                _logger.LogInfo(string.Format("Bulk Insert #LEASE AGREEMENT "));
                _logger.LogDebug(string.Format("Bulk Insert #LEASE AGREEMENT ", loDataMultiList.AgreementList));
              
                var loAgreementList  = new List<AgreementUploadDTO>();
                loAgreementList = loDataMultiList.AgreementList;
                loDb.R_BulkInsert<AgreementUploadDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT", loAgreementList);
                #endregion

                #region Unit
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_UNIT (" +
                        @"NO INT
                            ,CCOMPANY_ID VARCHAR(8)
                            ,CPROPERTY_ID VARCHAR(20)
                            ,CTRANS_CODE VARCHAR(10)
                            ,CDEPT_CODE VARCHAR(20)
                            ,CLOI_REF_NO VARCHAR(30)
                            ,CBUILDING_ID VARCHAR(20)
                            ,CFLOOR_ID VARCHAR(30)
                            ,CUNIT_ID VARCHAR(20)
                            ,NACTUAL_AREA_SIZE NUMERIC(8,2)
                            ,ISEQ_NO_ERROR INT)";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                _logger.LogInfo(string.Format("Bulk Insert #LEASE_AGREEMENT_UNIT "));
                _logger.LogDebug(string.Format("Bulk Insert #LEASE_AGREEMENT_UNIT ", loDataMultiList.UnitList));

                List<UnitUploadDTO> loAgreementUnitList = new List<UnitUploadDTO>();
                loAgreementUnitList =  loDataMultiList.UnitList!;

                loDb.R_BulkInsert<UnitUploadDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_UNIT", loAgreementUnitList);
                #endregion
                #region Utility
                lcQuery = "CREATE TABLE #LEASE_AGREEMENT_UTILITY (" +
                            @"NO INT
                            ,CCOMPANY_ID VARCHAR(8)
                            ,CPROPERTY_ID VARCHAR(20)
                            ,CTRANS_CODE VARCHAR(10)
                            ,CDEPT_CODE VARCHAR(20)

                            ,CLOI_REF_NO VARCHAR(30)
                            ,CBUILDING_ID VARCHAR(20)
                            ,CFLOOR_ID VARCHAR(30)
                            ,CUNIT_ID VARCHAR(20)

                            ,CCHARGES_TYPE_ID VARCHAR(200)

                            ,CCHARGES_TYPE_NAME VARCHAR(200)
                            ,CCHARGES_ID	 VARCHAR(20)
                            ,CCHARGES_NAME	 VARCHAR(200)
                            ,CMETER_NO	 VARCHAR(50)
                            ,CSTART_INV_PRD	 VARCHAR(6)
                            ,NMETER_START NUMERIC(16,2)
                            ,NBLOCK1_START NUMERIC(16,2)
                            ,NBLOCK2_START NUMERIC(16,2)
                            ,ISEQ_NO_ERROR INT)";

                //convert dto
                var loListUtility = R_Utility.R_ConvertCollectionToCollection<UtilityUploadDataFromDBDTO, UtilityUploadDTO>(loDataMultiList.UtilityList);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                _logger.LogInfo(string.Format("Bulk Insert #LEASE_AGREEMENT_UTILITY "));
                _logger.LogDebug("#LEASE_AGREEMENT_UTILITY ", loListUtility);

                loDb.R_BulkInsert<UtilityUploadDTO>((SqlConnection)loConn, "#LEASE_AGREEMENT_UTILITY", loListUtility);
                _logger.LogInfo("End Bulk Insert");
                #endregion

                //lcQuery = "";
                lcQuery = "EXEC RSP_PM_UPLOAD_HANDOVER " +
               "@CCOMPANY_ID, " +
               "@CPROPERTY_ID, " +
               "@CTRANS_CODE, " +
               "@CUSER_ID, " +
               "@CKEY_GUID";
                loCommand.CommandText = lcQuery;
              //  loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, lcTransCode);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogInfo("Execute query : ");
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var abc = loDb.SqlExecNonQuery(loConn, loCommand, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery);
                _logger.LogInfo(string.Format("Exec query to inform framework from outer exception on cls"));
                _logger.LogDebug("{@ObjectQuery}", lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                _logger.LogDebug("{@ObjectQuery}", lcQuery);
                _logger.LogInfo("Exec query to inform framework that process upload is finished");
                loDb.SqlExecNonQuery(lcQuery);
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));



        }

    }
}
