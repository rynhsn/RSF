using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using PMB04000BackResources;
using PMB04000COMMON.Context;
using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;
using PMB04000COMMON.Logs;
using PMB04000COMMON.Print.Distribute;
using PMB04000COMMONPrintBatch;
using PMB04000COMMONPrintBatch.ParamDTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_EmailEngine;
using R_Processor;
using R_ProcessorJob;
using R_ReportServerClient;
using R_ReportServerClient.DTO;
using R_ReportServerCommon;
using R_Storage;
using R_StorageCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PMB04000BACK
{
    public class PMB04000BatchReportCls : R_IBatchProcess, R_IProcessJob<string, R_Exception>
    {
        private readonly LoggerPMB04000? _logger;
        private readonly ActivitySource _activitySource;
        RSP_GS_GET_STORAGE_TYPEResources.Resources_Dummy_Class _RSPStorage = new();
        private readonly R_ConnectionAttribute _connectionAttribute;

        #region Constructor

        #endregion

        public PMB04000BatchReportCls()
        {
            R_Db loDb = new R_Db();
            _logger = LoggerPMB04000.R_GetInstanceLogger();
            _activitySource = PMB04000Activity.R_GetInstanceActivitySource();
            _connectionAttribute = loDb.GetConnectionAttribute(R_Db.eDbConnectionStringType.ReportConnectionString);
            _cCompanyId = R_BackGlobalVar.COMPANY_ID;
            _cTenantId = R_BackGlobalVar.TENANT_ID;
            _cUserId = R_BackGlobalVar.USER_ID;
            _cReportCulture = R_BackGlobalVar.REPORT_CULTURE;
            _reportFormat = GetReportFormat();
            loDb = null;
            _logger.LogInfo("koneksi string di Constructor ={0}", _connectionAttribute.ConnectionString);
        }

        private R_UploadAndProcessKey _oKey;
        private int _nMaxData;
        private int _nCountProcess;
        private int _nCountError;

        private R_UploadAndProcessKey _olock = new R_UploadAndProcessKey(); // For locking multithread
        private R_ProcessorClass<string, R_Exception> _oProcessor;
        private List<PMB04000SendReceiptDTO> _listReceiptData = new();

        private string _cTenantId = "";
        private string _cCompanyId = "";
        private string _cUserId = "";
        private string _cReportCulture = "";
        private ReportFormatDTO _reportFormat = new ReportFormatDTO();
        PMB04000ParamReportDTO _poParamReport;
        List<PMB04000DataReportDTO> listDataReport = new();
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            _logger.LogInfo("start R_BatchProcess ");
            string lcMethodName = nameof(R_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            R_Exception loException = new R_Exception();

            _logger.LogInfo("before R_Db() R_BatchProcess ");
            R_Db loDb = new R_Db();
            _logger.LogInfo("after R_Db() R_BatchProcess ");
            //DbCommand loCommand = null;
            DbConnection loConnection = null;
            R_ReportServerClientConfiguration loReportConfiguration;

            try
            {
                _logger.LogInfo("koneksi di R_BatchProcess {0}", _connectionAttribute.ConnectionString);
                loConnection = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_BatchProcess {0}", loConnection.ConnectionString);

                if (!loDb.R_TestConnection(loConnection))
                {
                    loException.Add("", "Error where Connection to database");
                    _logger.LogError(loException);
                    goto EndBlock;
                }
                _logger.LogInfo("get param", lcMethodName);

                //get parameter
                string? lcProperty = poBatchProcessPar.UserParameters
                                     .FirstOrDefault(x => x.Key.Equals(PMB04000ContextDTO.CPROPERTY_ID))
                                     ?.Value is JsonElement jsonProperty ? jsonProperty.GetString() : null;

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMB04000ContextDTO.CDEPT_CODE)).FirstOrDefault()!.Value;
                string lcDeptCode = ((System.Text.Json.JsonElement)loVar).GetString()!;

                var abc = poBatchProcessPar.BigObject;
                var lcRefNoCombined = R_NetCoreUtility.R_DeserializeObjectFromByte<string>(poBatchProcessPar.BigObject);

                _poParamReport = new PMB04000ParamReportDTO
                {
                    CCOMPANY_ID = _cCompanyId,
                    CPROPERTY_ID = lcProperty,
                    CDEPT_CODE = lcDeptCode,
                    CREF_NO = lcRefNoCombined,
                    CUSER_ID = _cUserId,
                    CLANG_ID = _cReportCulture,
                    LPRINT = false
                };

                _oKey = poBatchProcessPar.Key;
                var laSplitCrefNoTemp = lcRefNoCombined.Split(',');
                _nCountProcess = laSplitCrefNoTemp.Count();
                loReportConfiguration = R_ReportServerClientService.R_GetReportServerConfiguration();
                _oProcessor = new R_ProcessorClass<string, R_Exception>(this);
                _logger.LogInfo("before call method R_ProcessAsync", lcMethodName);
                _oProcessor.R_ProcessAsync("key1");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConnection != null)
                {
                    if (loConnection.State != ConnectionState.Closed)
                        loConnection.Close();
                    loConnection.Dispose();
                    loConnection = null;
                }
            }
        EndBlock:
            _logger.LogInfo("end R_BatchProcess ");
            loException.ThrowExceptionIfErrors();
        }
        public List<string> R_InitData(string poKey)
        {
            string lcMethodName = nameof(R_InitData);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START method {0}", lcMethodName));
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loException = new R_Exception();
            List<string> loReturn = null;

            try
            {
                List<string> loRes = new List<string>();
                _logger.LogInfo("connection di R_InitData = {0} ", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_InitData {0}", loConn.ConnectionString);

                var loCls = new PMB04000PrintCls();
                _poParamReport.LDISTRIBUTE = false;
                _poParamReport.CSTORAGE_ID = "";

                _logger.LogInfo("Get data from Report from DB");
                string[] laSplitCrefNo = _poParamReport.CREF_NO.Split(',');
                loReturn = laSplitCrefNo.ToList<string>();


                _logger.LogInfo("Before Method method SendReceiptCls");
                _listReceiptData = loCls.SendReceiptCls(_poParamReport, loConn); // get data list of send email
                _logger.LogInfo("after method SendReceiptCls");

                _logger.LogInfo($"End {lcMethodName}");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger!.LogInfo(string.Format("END method {0}", lcMethodName));
            return loReturn!;
        }
        public async Task R_SingleProcessAsync(string poKey, string pcParameter)
        {
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_SingleProcessAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo("Get data from Report from DB");

            string loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            R_ConnectionAttribute loConnAttr;
            PMB04000ResultDataDTO loData = null;
            R_GenerateReportParameter loParameter;
            R_ReportServerRule loReportRule;
            R_FileType leReportOutputType;
            List<PMB04000DataReportDTO> loPrintResults = null;
            PMB04000SendReceiptDTO loEmailMessageDetail = null;

            int lnCountProcess;
            string lcCmd;
            string lcReportFileName = "";
            string lcStorageId = "";
            byte[] loRtnInByte;
            string lcExtension;
            try
            {
                lock (_olock)
                {
                    lnCountProcess = _nCountProcess;
                    _nCountProcess += 1;
                }

                loDb = new R_Db();
                _logger.LogInfo("koneksi di R_SingleProcessAsync {0}", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_SingleProcessAsync {0}", loConn.ConnectionString);

                var loParam = new PMB04000ParamReportDTO
                {
                    CCOMPANY_ID = _poParamReport.CCOMPANY_ID,
                    CPROPERTY_ID = _poParamReport.CPROPERTY_ID,
                    CDEPT_CODE = _poParamReport.CDEPT_CODE,
                    CREF_NO = pcParameter,
                    CUSER_ID = _poParamReport.CUSER_ID,
                    CLANG_ID = _poParamReport.CLANG_ID,
                    LPRINT = false,
                };

                var loCls = new PMB04000PrintCls();
                _logger.LogInfo("Before to  method GetReportReceiptDataPrintBatch");
                loPrintResults = loCls.GetReportReceiptDataPrintBatch(loParam, loConn);
                _logger!.LogInfo("Start Single process");

                lcCmd = string.Format("exec RSP_WriteUploadProcessStatus '{0}','{1}','{2}',{3},'{4}'",
                 _oKey.COMPANY_ID.Trim(),
                 _oKey.USER_ID.Trim(),
                 _oKey.KEY_GUID.Trim(),
                 _GetPropCount(lnCountProcess),
                 string.Format("Process data Ref NO {0}", pcParameter));
                loDb.SqlExecNonQuery(lcCmd, loConn, false);

                _logger.LogInfo("panggil program GenerateDataPrint", lcMethodName);
                loData = GenerateDataPrint(loPrintResults, loConn);//GENERATE PRINT WITH LOGO AND DATA
                loEmailMessageDetail = _listReceiptData.Where(a => a.CREF_NO!.Trim().ToLower().Equals(pcParameter.Trim().ToLower())).FirstOrDefault()!;

                if (loEmailMessageDetail == null)
                {
                    loException.Add("02", $"CREF_NO {pcParameter} not found in on List");
                }

                //Set Report Rule and Name
                loReportRule = new R_ReportServerRule(_cTenantId.ToLower(), _cCompanyId.ToLower());
                _logger.LogInfo("assign loReportRule", loReportRule);

                lcReportFileName = "PMB04000.frx";
                _logger.LogInfo("Get file Frx");
                _logger.LogDebug("Get file Frx", lcReportFileName);

                //Prepare Parameter
                _logger!.LogInfo("Get Parameter");
                leReportOutputType = R_FileType.PDF;
                lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;
                _logger.LogInfo("Create object Report Parameter");

                loParameter = new R_GenerateReportParameter()
                {
                    ReportRule = loReportRule,
                    ReportFileName = lcReportFileName,
                    ReportData = JsonSerializer.Serialize<PMB04000ResultDataDTO>(loData),
                    ReportDataSourceName = "ResponseDataModel",

                    ReportFormat = R_Utility.R_ConvertObjectToObject<ReportFormatDTO, R_ReportFormatDTO>(_reportFormat),

                    ReportDataType = typeof(PMB04000ResultDataDTO).ToString(),
                    ReportOutputType = leReportOutputType,
                    ReportAssemblyName = "PMB04000COMMONPrintBatch.dll",
                    ReportParameter = null
                };
                _logger.LogDebug("{@Parameter}", loParameter);
                _logger!.LogInfo("Succes get Parameter");

                //GENERATE REPORT
                _logger!.LogInfo("Call method [R_GenerateReportByte] to Genearate Report");
                loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(R_ReportServerClientService.R_GetHttpClient(),
                    "api/ReportServer/GetReport", loParameter); //menghubungkan ke API dari engine yang sudah ada
                _logger.LogDebug("Report yang telah menjadi byte:", loRtnInByte);

                _logger!.LogInfo("Start Transaction Block");
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        _logger.LogInfo("start transcope:", lcMethodName);
                        loConn = loDb.GetConnection();
                        loConnAttr = _connectionAttribute;
                        _logger.LogInfo("koneksi di TransactionScope {0}", _connectionAttribute.ConnectionString);
                        _logger.LogInfo("koneksi loConnAttr di TransactionScope {0}", loConnAttr);
                        _logger.LogInfo("get connection in transcope:", lcMethodName);

                        if (loRtnInByte != null)
                        {
                            string emailId = SendEmail(loRtnInByte, loEmailMessageDetail, loConn);
                            _logger.LogInfo("Finish run Send Email method");
                            _logger.LogDebug("email id from of email:", emailId);


                            _logger.LogInfo("Get storage type");

                            _logger.LogInfo("Get Parameter to save file");
                            PMB040000ParamSaveStorageDTO poParamSaveFile = new PMB040000ParamSaveStorageDTO
                            {
                                CSTORAGE_ID = loEmailMessageDetail.CSTORAGE_ID,
                                CREF_NO = pcParameter,
                                FileExtension = loParameter.ReportOutputType.ToString(),
                                REPORT = loRtnInByte
                            };
                            //Save report to DB
                            lcStorageId = SaveReportFile(poParamSaveFile, loConn);

                            loParam.LDISTRIBUTE = true;
                            loParam.CSTORAGE_ID = lcStorageId;

                            _logger.LogInfo(" if send email success update field on store procedure value distribute = true");
                            loCls.UpdateSendReceiptCls(loParam, loConn);
                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        loException.Add(ex);
                        _logger!.LogError(loException);
                    }
                }
                _logger.LogInfo("end R_SingleProcessAsync ");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }
        #region status
        public void R_Status(string poKey, string pcMessage)
        {
            _logger.LogInfo("start R_Status ");
            string lcMethodName = nameof(R_Status);
            R_Exception loException = new R_Exception();
            DbConnection loConn = null;
            R_Db loDb = new R_Db();
            string lcCmd;
            int lnCountProcess;
            try
            {
                _logger.LogInfo("koneksi di R_Status {0}", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_Status {0}", loConn.ConnectionString);

                _logger.LogInfo("before exec RSP_WriteUploadProcessStatus", lcMethodName);

                lock (_olock)
                    lnCountProcess = _nCountProcess;

                lcCmd = string.Format("exec RSP_WriteUploadProcessStatus '{0}','{1}','{2}',{3},'{4}'",
                    _oKey.COMPANY_ID.Trim(), _oKey.USER_ID.Trim(), _oKey.KEY_GUID.Trim(), _GetPropCount(lnCountProcess), pcMessage.Trim());
                _logger.LogInfo("koneksi sebelum SqlExecNonQuery di R_Status {0}", loConn.ConnectionString);
                loDb.SqlExecNonQuery(lcCmd, loConn, false);
                _logger.LogInfo("after exec RSP_WriteUploadProcessStatus", lcMethodName);
                _logger.LogInfo("end R_Status ");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(loException);

            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
            }
            loException.ThrowExceptionIfErrors();
        }
        public void R_SingleSuccessStatus(string poKey, string poParameter)
        {
            _logger.LogInfo("start R_SingleSuccessStatus ");
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_SingleSuccessStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcCmd;
            int lnCountProcess;
            try
            {
                _logger.LogInfo("koneksi di R_SingleSuccessStatus {0}", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_SingleSuccessStatus {0}", loConn.ConnectionString);

                lock (_olock)
                    lnCountProcess = _nCountProcess;
                _logger.LogInfo("before exec RSP_WriteUploadProcessStatus", lcMethodName);

                lcCmd = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}','{1}','{2}',{3},'{4}'",
                    _oKey.COMPANY_ID.Trim(), _oKey.USER_ID.Trim(), _oKey.KEY_GUID.Trim(), _GetPropCount(lnCountProcess), string.Format("Process data CREF {0}", poParameter));

                _logger.LogInfo("koneksi loConn di R_SingleSuccessStatus {0}", loConn.ConnectionString);
                loDb.SqlExecNonQuery(lcCmd, loConn, false);
                _logger.LogInfo("after exec RSP_WriteUploadProcessStatus", lcMethodName);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
            }
            _logger.LogInfo("end R_SingleSuccessStatus ");

            loException.ThrowExceptionIfErrors();
        }

        public void R_SingleErrorStatus(string poKey, string poParameter, R_Exception poException)
        {
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_SingleErrorStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcCmd;
            R_Db loDb = new R_Db();
            int lnCountError;
            string lcError = "";
            try
            {
                _logger.LogInfo("koneksi di R_SingleErrorStatus {0}", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_SingleErrorStatus {0}", loConn.ConnectionString);
                _logger.LogInfo("before INSERT INTO GST_UPLOAD_ERROR_STATUS", lcMethodName);

                lock (_olock)
                {
                    lnCountError = _nCountError;
                    _nCountError += 1;
                }
                if (poException.Haserror)
                {
                    lcError = poException.ErrorList.FirstOrDefault().ErrDescp;
                    lcError.Replace("\"", " ").Replace("'", " ");
                }
                loCommand = loDb.GetCommand();
                var lcQuery = $"INSERT INTO  GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID, CUSER_ID, CKEY_GUID, ISEQ_NO, CERROR_MESSAGE) " +
                    $"VALUES ( @COMPANY_ID, @USER_ID, @KEY_GUID, @CountError, @lcErrMsg ); ";

                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCommand, "@COMPANY_ID", DbType.String, 10, _oKey.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@USER_ID", DbType.String, 20, _oKey.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@KEY_GUID", DbType.String, 256, _oKey.KEY_GUID);
                loDb.R_AddCommandParameter(loCommand, "@CountError", DbType.Int32, 256, lnCountError);
                loDb.R_AddCommandParameter(loCommand, "@lcErrMsg", DbType.String, 256, lcError);

                _logger.LogInfo("koneksi loConn di R_SingleErrorStatus {0}", loConn.ConnectionString);
                var loReturnTempVal = loDb.SqlExecQuery(loConn, loCommand, false);
                _logger.LogInfo("after INSERT INTO GST_UPLOAD_ERROR_STATUS", lcMethodName);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
            }
            _logger.LogInfo("end R_SingleErrorStatus ");

            loException.ThrowExceptionIfErrors();
        }
        public void R_InitDataErrorStatus(string poKey, R_Exception poException)
        {
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_InitDataErrorStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            DbConnection loConn = null;
            R_Db loDb = new R_Db();
            try
            {
                string lcCmd;
                string lcErrMsg;
                int lnCountError;

                lock (_olock)
                    lnCountError = _nCountError;
                lcErrMsg = $"ERROR run method {lcMethodName}";

                if (poException != null)
                {
                    if (poException.ErrorList.Count > 0)
                    {
                        lcErrMsg = string.Format("{0} with message {1}", lcErrMsg, poException.ErrorList.FirstOrDefault()!.ErrDescp);
                        lcErrMsg = lcErrMsg.Replace("'", "").Replace(((char)34).ToString(), "");
                    }
                }
                _logger.LogInfo("koneksi di R_InitDataErrorStatus {0}", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_InitDataErrorStatus {0}", loConn.ConnectionString);

                var lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
              string.Format("('{0}', '{1}', ", _oKey.COMPANY_ID, _oKey.USER_ID) +
              string.Format("'{0}', -1, '{1}')", _oKey.KEY_GUID, loException.ErrorList[0].ErrDescp);

                _logger.LogInfo("koneksi loConn di R_InitDataErrorStatus {0}", loConn.ConnectionString);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", _oKey.COMPANY_ID) +
                          string.Format("'{0}', ", _oKey.USER_ID) +
                          string.Format("'{0}', ", _oKey.KEY_GUID) +
                          string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
            }
            _logger.LogInfo("end R_InitDataErrorStatus ");

            loException.ThrowExceptionIfErrors();
        }
        public void R_ProcessCompleteStatus(string poKey, List<R_Exception> poExceptions)
        {
            _logger.LogInfo("start R_ProcessCompleteStatus ");

            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_ProcessCompleteStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            DbConnection loConn = null;
            R_Db loDb = new R_Db();
            string lcCmd;
            int lnFlag;

            try
            {

                if (poExceptions.Count > 0)
                    lnFlag = 9;
                else
                    lnFlag = 1;

                _logger.LogInfo("koneksi di R_ProcessCompleteStatus {0}", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("koneksi loConn di R_ProcessCompleteStatus {0}", loConn.ConnectionString);

                lcCmd = string.Format("exec RSP_WriteUploadProcessStatus '{0}','{1}','{2}',{3},'{4}',{5}",
                    _oKey.COMPANY_ID.Trim(), _oKey.USER_ID.Trim(), _oKey.KEY_GUID.Trim(), _GetPropCount(_nMaxData), "Finish Process", lnFlag);

                _logger.LogInfo("koneksi sebelum SqlExecNonQuery di R_ProcessCompleteStatus {0}", loConn.ConnectionString);
                loDb.SqlExecNonQuery(lcCmd, loConn, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
            }
            _logger.LogInfo("end R_ProcessCompleteStatus ");
        }
        #endregion

        #region Helper

        private List<PMB04000DataReportDTO> ConvertResultToFormatPrint(List<PMB04000DataReportDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(ConvertResultToFormatPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<PMB04000DataReportDTO> loReturn = poCollectionDataRaw;

            try
            {
                foreach (var item in loReturn)
                {
                    item.DINVOICE_DATE = ConvertStringToDateTimeFormat(item.CINVOICE_DATE);
                    item.DTODAY_DATE = ConvertStringToDateTimeFormat(item.CTODAY_DATE);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;

        }
        private ReportFormatDTO GetReportFormat()
        {
            return new ReportFormatDTO()
            {
                _DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES,
                _DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR,
                _GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR,
                _ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE,
                _ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME,
                _LongDate = R_BackGlobalVar.REPORT_FORMAT_LONG_DATE,
                _LongTime = R_BackGlobalVar.REPORT_FORMAT_LONG_TIME,
            };
        }

        private PMB04000StorageType GetStorageType(DbConnection poConnection)
        {
            string lcMethodName = nameof(GetStorageType);
            var loEx = new R_Exception();
            PMB04000StorageType loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                _logger!.LogInfo("before exec RSP_GS_GET_STORAGE_TYPE", lcMethodName);
                loConn = poConnection;
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, _poParamReport.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, _poParamReport.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter}", loDbParam);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<PMB04000StorageType>(loDataTable).FirstOrDefault()!;
                    _logger.LogInfo("after exec RSP_GS_GET_STORAGE_TYPE", lcMethodName);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger!.LogError(loEx);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }
        private string SaveReportFile(PMB040000ParamSaveStorageDTO poParameter, DbConnection poConnection)
        {
            R_Exception loEx = new R_Exception();
            string lcMethodName = nameof(SaveReportFile);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            string loResult = null;
            DbConnection loConn = null;
            R_ConnectionAttribute loConnAttr;
            R_SaveResult loSaveResult = null;
            try
            {
                var loDb = new R_Db();
                loConn = poConnection;
                loConnAttr = _connectionAttribute;
                _logger.LogInfo("koneksi loConnAttr di TransactionScope {0}", loConnAttr);
                _logger.LogInfo("Get storage type");
                var loGetStorageType = GetStorageType(poConnection);

                if (string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {
                    R_EStorageType loStorageType = loGetStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;
                    R_EProviderForCloudStorage loProvider = loGetStorageType.CSTORAGE_PROVIDER_ID!.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                    _logger.LogInfo("Add Or Create storage id to save cause storage id not exist:");

                    // Add and create Storage ID
                    R_AddParameter loAddParameter = new R_AddParameter()
                    {
                        StorageType = loStorageType,
                        ProviderCloudStorage = loProvider,
                        FileName = $"Report_{poParameter.CREF_NO}",
                        FileExtension = poParameter.FileExtension,
                        UploadData = poParameter.REPORT,
                        UserId = _poParamReport.CUSER_ID,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = _poParamReport.CCOMPANY_ID,
                            CDATA_TYPE = "PMT_TRANS_HD",
                            CKEY01 = _poParamReport.CPROPERTY_ID,
                            CKEY02 = poParameter.CREF_NO,
                        }
                    };
                    _logger.LogInfo("Call R_StorageUtility.AddFile to storage table");
                    _logger.LogDebug("Add Parameter value:", loAddParameter);
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn);
                }
                else if (!string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {
                    _logger.LogInfo("update with storage id to save cause storage id already exist:");
                    R_UpdateParameter loUpdateParameter;

                    loUpdateParameter = new R_UpdateParameter()
                    {
                        StorageId = poParameter.CSTORAGE_ID,
                        UploadData = poParameter.REPORT,
                        UserId = _poParamReport.CUSER_ID,
                        OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                        {
                            FileExtension = poParameter.FileExtension,
                            FileName = $"Report_{poParameter.CREF_NO}.{poParameter.FileExtension}"
                        }
                    };
                    _logger.LogInfo("Call R_StorageUtility.UpdateFile to storage table");
                    _logger.LogDebug("Add Parameter value:", loUpdateParameter);
                    loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);
                }

                if (loSaveResult != null)
                {
                    loResult = loSaveResult.StorageId;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            _logger!.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loEx.ThrowExceptionIfErrors();
            return loResult!;
        }
        private PMB04000ResultDataDTO GenerateDataPrint(List<PMB04000DataReportDTO> poParam, DbConnection poConnection)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START method {0}", lcMethodName));
            var loException = new R_Exception();
            PMB04000ResultDataDTO loRtn = null;

            PMB04000PrintCls? loCls = null;
            try
            {
                var poParameterLogo = new PMB04000ParamReportDTO
                {
                    CCOMPANY_ID = _cCompanyId
                };

                loCls = new PMB04000PrintCls();
                var loCollectionFromDb = poParam;
                _logger.LogInfo("Set BaseHeader Report");
                PMB04000BaseHeaderDTO loHeader = new();
                var loGetLogo = loCls.GetLogoCompany(poParameterLogo);

                //SET LOGO DAN HEADER
                _logger.LogInfo("Set LOGO AND HEADER Report");

                CultureInfo loCultureInfo = new CultureInfo(_cReportCulture);
                loHeader.KWITANSI = R_Utility.R_GetMessage(typeof(Resources_PMB04000), "Kwitansi", loCultureInfo);
                loHeader.CLOGO = loGetLogo.CLOGO!;

                _logger.LogInfo("Set Column and Label Report");
                var loColumn = AssignValuesWithMessages(typeof(Resources_PMB04000), loCultureInfo, new PMB04000ColumnDTO());
                var loLabel = AssignValuesWithMessages(typeof(Resources_PMB04000), loCultureInfo, new PMB04000LabelDTO());

                //CONVERT DATA TO DISPLAY IF DATA EXIST
                var oListData = loCollectionFromDb.Any() ? ConvertResultToFormatPrint(loCollectionFromDb) : new List<PMB04000DataReportDTO>();
                //Assign DATA
                _logger.LogInfo("Set Data Report");
                loRtn = new PMB04000ResultDataDTO
                {

                    Header = loHeader,
                    Column = (PMB04000ColumnDTO)loColumn,
                    Label = (PMB04000LabelDTO)loLabel,
                    Data = oListData
                };

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex, "Error in GenerateDataPrint");
            }
            _logger.LogInfo("END Method GenerateDataPrint on Controller");
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion

        #region Send Email
        public string SendEmail(byte[] EmailMessageData, PMB04000SendReceiptDTO EmailMessageDetails, DbConnection poConnection)
        {
            string lcMethodName = nameof(SendEmail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            R_EmailEngineBackCommandPar loEmailPar;
            List<R_EmailEngineBackObject> loEmailFiles = new List<R_EmailEngineBackObject>();
            R_Db loDb = null;
            DbConnection loConn = null;
            string lcRtn = "";
            try
            {
                loEmailFiles = new List<R_EmailEngineBackObject>
                {
                    new R_EmailEngineBackObject()
                    {
                        FileName = EmailMessageDetails!.CFILE_NAME,
                        FileExtension = Path.GetExtension(".pdf"),
                        FileId = EmailMessageDetails.CFILE_ID,
                        FileData = EmailMessageData
                    }
                };

                loEmailPar = new R_EmailEngineBackCommandPar()
                {
                    COMPANY_ID = EmailMessageDetails.CCOMPANY_ID,
                    USER_ID = EmailMessageDetails.CUSER_ID,
                    PROGRAM_ID = EmailMessageDetails.CPROGRAM_ID,
                    Message = new R_EmailEngineMessage()
                    {
                        EMAIL_FROM = EmailMessageDetails.CEMAIL_FROM!,
                        EMAIL_BODY = EmailMessageDetails.CEMAIL_BODY!,
                        EMAIL_SUBJECT = EmailMessageDetails.CEMAIL_SUBJECT!,
                        EMAIL_TO = EmailMessageDetails.CEMAIL_TO!,
                        EMAIL_CC = "",
                        EMAIL_BCC = "",
                        FLAG_HTML = EmailMessageDetails.LFLAG_HTML
                    }
                };
                _logger.LogInfo("Get data to send to email engine");
                _logger.LogDebug("Data loEmailPar to send to email engine: {@Parameter}", loEmailPar);
                loConn = poConnection;
                loEmailPar.Attachments = loEmailFiles;
                lcRtn = R_EmailEngineBack.R_EmailEngineSaveFromBack(loEmailPar, loConn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            
            _logger.LogInfo("end SaveEmailFromBack ");
            loException.ThrowExceptionIfErrors();
            _logger!.LogInfo(string.Format("End process method {0} on Cls", lcMethodName));
            return lcRtn;
        }
        #endregion


        #region Utilities

        private int _GetPropCount(int pnCount)
        {
            if (_nMaxData == 0)
                return 0;
            return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(100) * pnCount / _nMaxData));
        }
        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }
            else
            {
                DateTime result;
                if (pcEntity.Length == 6)
                {
                    pcEntity += "01";
                }

                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        //Helper Assign Object
        private object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
        {
            object loObj = Activator.CreateInstance(poObject.GetType())!;
            var loGetPropertyObject = poObject.GetType().GetProperties();

            foreach (var property in loGetPropertyObject)
            {
                string propertyName = property.Name;
                string message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
                property.SetValue(loObj, message);
            }

            return loObj;
        }
        #endregion

    }
}




