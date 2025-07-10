using PMT02100BACK.OpenTelemetry;
using PMT02100COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Processor;
using R_ProcessorJob;
using R_ReportServerClient;
using R_ReportServerClient.DTO;
using R_ReportServerCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using PMT02100REPORTCOMMON.DTOs.PMT02100PDF;
using PMT02100REPORTCOMMON.DTOs.PMT02100Email;

namespace PMT02100BACK
{
    public class PMT02110InvitationEmailCls : R_IBatchProcessAsync, R_IProcessJobAsync<PMT02100InvitationDTO, R_Exception>
    {

        private readonly LoggerPMT02110Invitation? _logger;
        private readonly ActivitySource _activitySource;
        PMT02100BackResources.Resources_Dummy_Class _RSPStorage = new();
        private readonly R_ConnectionAttribute _connectionAttribute;

        #region Constructor

        public PMT02110InvitationEmailCls()
        {
            R_Db? loDb = new R_Db();
            _logger = LoggerPMT02110Invitation.R_GetInstanceLogger();
            _activitySource = PMT02110InvitationActivitySourceBase.R_GetInstanceActivitySource();
            _connectionAttribute = loDb.GetConnectionAttribute(R_Db.eDbConnectionStringType.ReportConnectionString);
            _cCompanyId = R_BackGlobalVar.COMPANY_ID;
            _cTenantId = R_BackGlobalVar.TENANT_ID;
            _cUserId = R_BackGlobalVar.USER_ID;
            _cReportCulture = R_BackGlobalVar.REPORT_CULTURE;

            _DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            _DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            _GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            _ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            _ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
            _LongDate = R_BackGlobalVar.REPORT_FORMAT_LONG_DATE;
            _LongTime = R_BackGlobalVar.REPORT_FORMAT_LONG_TIME;

            loDb = null;
            _logger.LogInfo("koneksi string di Constructor ={0}", _connectionAttribute.ConnectionString);
        }

        #endregion

        private R_UploadAndProcessKey? _oKey;
        private int _nMaxData;
        private int _nCountProcess;
        private int _nCountError;

        private R_UploadAndProcessKey? _olock = new R_UploadAndProcessKey(); // For locking multithread
        private R_ProcessorClass<PMT02100InvitationDTO, R_Exception>? _oProcessor;
        private List<PMT02100SendReceiptDTO> _listReceiptData = new();

        private string _cTenantId = "";
        private string _cCompanyId = "";
        private string _cUserId = "";
        private string _cReportCulture = "";

        private int _CountReportMade = 0;
        private int _DecimalPlaces;
        private string? _DecimalSeparator = "";
        private string? _GroupSeparator = "";
        private string? _ShortDate = "";
        private string? _ShortTime = "";
        private string? _LongDate = "";
        private string? _LongTime = "";

        //private ReportFormatDTO _reportFormat = new ReportFormatDTO();
        //Dictionary<(string CTENANT_ID, string CEMAIL_SEND), List<PMT02100InvitationValueReportDataDTO>> _DataSendEmailReport = new Dictionary<(string, string), List<PMT02100InvitationValueReportDataDTO>>();
        List<PMT02100InvitationValueReportDataDTO> _DataSendEmailReport = new List<PMT02100InvitationValueReportDataDTO>();
        PMT02100InvitationParameterReportDTO? _poParamReport = new PMT02100InvitationParameterReportDTO();
        // List<PMT02100InvitationDTO> listDataReport = new();

        public async Task R_BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            _logger.LogInfo("start R_BatchProcessAsync ");
            string lcMethodName = nameof(R_BatchProcessAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            R_Exception loException = new R_Exception();

            _logger.LogInfo("before R_Db() R_BatchProcessAsync ");
            R_Db loDb = new R_Db();
            _logger.LogInfo("after R_Db() R_BatchProcessAsync ");
            DbConnection? loConnection = null;
            string lcCompany;
            string lcUserId;
            string lcLangId;
            string lcGuidId;
            PMT02100InvitationParameterDTO loTempListForProcess = new();
            R_ReportServerClientConfiguration loReportConfiguration;

            using TransactionScope scope = new(TransactionScopeOption.Required);

            try
            {
                //_logger.LogInfo("koneksi loConn di R_BatchProcessAsync {0}", loConnection.ConnectionString);

                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("", "Error where Connection to database");
                    _logger.LogError(loException);
                    goto EndBlock;
                }

                loTempListForProcess = R_NetCoreUtility.R_DeserializeObjectFromByte<PMT02100InvitationParameterDTO>(poBatchProcessPar.BigObject);
                _logger.LogInfo("get param", lcMethodName);

                #region GetParameter

                lcCompany = poBatchProcessPar.Key.COMPANY_ID;
                lcUserId = poBatchProcessPar.Key.USER_ID;
                lcLangId = R_BackGlobalVar.CULTURE;
                lcGuidId = poBatchProcessPar.Key.KEY_GUID;

                _poParamReport = new PMT02100InvitationParameterReportDTO();
                _poParamReport.loParameter = new PMT02100InvitationParameterDTO();
                _poParamReport.DataReport = new List<PMT02100InvitationDTO>();
                //_poParamReport.ParameterSP = new PMR01700ParameterSPDataReportDTO();
                //_poParamReport.DataReport = new List<PMT02100InvitationDTO>();

                _poParamReport.loParameter.CCOMPANY_ID = lcCompany;
                _poParamReport.loParameter.CPROPERTY_ID = loTempListForProcess.CPROPERTY_ID;
                _poParamReport.loParameter.CDEPT_CODE = loTempListForProcess.CDEPT_CODE;
                _poParamReport.loParameter.CTRANS_CODE = loTempListForProcess.CTRANS_CODE;
                _poParamReport.loParameter.CREF_NO = loTempListForProcess.CREF_NO;
                _poParamReport.loParameter.CUSER_ID = lcUserId;

                PMT02110InvitationReportCls loCls = new PMT02110InvitationReportCls();

                if (loTempListForProcess.LCONFIRM_SCHEDULE)
                {
                    loCls.ConfirmScheduleProcess(loTempListForProcess); //, loConnection);
                }
                else
                {
                    loCls.ReinviteProcess(loTempListForProcess); //, loConnection);
                }

                var loResultFromDB = loCls.GetDataPrintHeaderDB(loTempListForProcess);
                _oKey = poBatchProcessPar.Key;

                if (loResultFromDB.Any(x => x.LWITH_DETAIL == true))
                {
                    _poParamReport.DataReport = loResultFromDB.Where(x => x.LWITH_DETAIL == true).ToList();

                    //_nMaxData = loTempListForProcess.DataReport.Count; // Setel nilai _nMaxData berdasarkan jumlah total data
                    _nCountProcess = 0; // Inisialisasi _nCountProcess
                    loReportConfiguration = R_ReportServerClientService.R_GetReportServerConfiguration();
                    _oProcessor = new R_ProcessorClass<PMT02100InvitationDTO, R_Exception>(this);
                    _logger.LogInfo("before call method R_ProcessAsync", lcMethodName);
                    await _oProcessor.R_ProcessAsync("key1");
                }
                else
                {
                    _logger.LogInfo("koneksi di R_BatchProcessAsync {0}", _connectionAttribute.ConnectionString);
                    loConnection = loDb.GetConnection();
                    ProcessEmailDistribution(loConnection, loDb, loException);
                    HandleFinalStatus(loConnection, loDb, loException.Haserror ? 9 : 1 , loException);
                }
                #endregion

                scope.Complete();
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
            _logger.LogInfo("end R_BatchProcessAsync ");
            loException.ThrowExceptionIfErrors();
        }

        public async Task<List<PMT02100InvitationDTO>> R_InitDataAsync(string poKey)
        {
            string lcMethodName = nameof(R_InitDataAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START method {0}", lcMethodName));
            var loDb = new R_Db();
            //DbConnection? loConn = null;
            var loException = new R_Exception();
            List<PMT02100InvitationDTO>? loReturn = null;

            try
            {
                _logger.LogInfo("Assign Data for Report");
                loReturn = _poParamReport.DataReport;

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

        public async Task R_SingleProcessAsync(string poKey, PMT02100InvitationDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_SingleProcessAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo("Get data from Report from DB");

            R_Db? loDb = null;
            DbConnection? loConn = null;
            R_ConnectionAttribute loConnAttr;
            R_GenerateReportParameter loParameter;
            R_ReportServerRule loReportRule;
            R_FileType leReportOutputType;
            PMT02100InvitationResultWithBaseHeaderPrintDTO? loData = null;
            List<PMT02100InvitationDetailDTO>? loPrintResults = null;
            //List<PMT02100InvitationResultDTO>? filteredData = null;

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

                // Initialize database connection
                loDb = new R_Db();
                _logger.LogInfo("Connecting to database: {0}", _connectionAttribute.ConnectionString);
                loConn = loDb.GetConnection();
                _logger.LogInfo("Connection established: {0}", loConn.ConnectionString);

                // Prepare report parameters
                var loParam = new PMT02100InvitationDetailParameterDTO
                {
                    CCOMPANY_ID = _poParamReport.loParameter.CCOMPANY_ID,
                    CPROPERTY_ID = _poParamReport.loParameter.CPROPERTY_ID,
                    CDEPT_CODE = _poParamReport.loParameter.CDEPT_CODE,
                    CTRANS_CODE = poParameter.CTRANS_CODE,
                    CREF_NO = poParameter.CREF_NO,
                    CBUILDING_ID = poParameter.CBUILDING_ID,
                    CFLOOR_ID = poParameter.CFLOOR_ID,
                    CUNIT_ID = poParameter.CUNIT_ID,
                    CUSER_ID = _poParamReport.loParameter.CUSER_ID
                };

                var loCls = new PMT02110InvitationReportCls();
                //_logger.LogInfo("Fetching data for report generation");
                //loPrintResults = loCls.GetDataPrintDetailDB(loParam, loConn);
                //var loDataParameterSaveStorage = loPrintResults?.FirstOrDefault();

                lcCmd = $"exec RSP_WriteUploadProcessStatus '{_oKey.COMPANY_ID.Trim()}','{_oKey.USER_ID.Trim()}','{_oKey.KEY_GUID.Trim()}',{_GetPropCount(lnCountProcess)},'Processing CREF_NO {poParameter.CREF_NO}'";
                loDb.SqlExecNonQuery(lcCmd, loConn, false);
                loReportRule = new R_ReportServerRule(_cTenantId.ToLower(), _cCompanyId.ToLower());
                _logger.LogInfo("Assigning report rule", loReportRule);

                //if (string.IsNullOrEmpty(loDataParameterSaveStorage.CFILE_NAME))
                //{
                //    loException.Add("", "Report Name not Supplied!");
                //    goto EndBlock;
                //}
                //lcReportFileName = loDataParameterSaveStorage.CFILE_NAME.EndsWith(".frx")
                //                    ? loDataParameterSaveStorage.CFILE_NAME
                //                    : $"{loDataParameterSaveStorage.CFILE_NAME}.frx";

                lcReportFileName = "PMT02100InvitationPDF.frx";

                _logger.LogInfo("Preparing report parameters");

                leReportOutputType = R_FileType.PDF;
                lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;

                _logger.LogInfo("Generate Data Print");

                //loData = loCls.GenerateDataPrint(loParam, loConn, filteredData!);
                loData = loCls.GenerateDataPrint(poParameter, loConn, _poParamReport.loParameter.CCOMPANY_ID);

                loData.ReportData.Data.Detail = loCls.GetDataPrintDetailDB(loParam, loConn);

                loParameter = new R_GenerateReportParameter
                {
                    ReportRule = loReportRule,
                    ReportFileName = lcReportFileName,
                    ReportData = JsonSerializer.Serialize(loData),
                    ReportDataSourceName = "ResponseDataModel",
                    ReportFormat = new R_ReportFormatDTO
                    {
                        DecimalSeparator = _DecimalSeparator,
                        DecimalPlaces = _DecimalPlaces,
                        GroupSeparator = _GroupSeparator,
                        ShortDate = _ShortDate,
                        ShortTime = _ShortTime,
                        LongDate = _LongDate,
                        LongTime = _LongTime
                    },
                    ReportDataType = typeof(PMT02100InvitationResultWithBaseHeaderPrintDTO).ToString(),
                    ReportOutputType = leReportOutputType,
                    ReportAssemblyName = "PMT02100REPORTCOMMON.dll"
                };

                _logger.LogInfo("Generating report");

                loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(R_ReportServerClientService.R_GetHttpClient(),
                    "api/ReportServer/GetReport", loParameter);
                _logger.LogDebug("Generated report in byte format", loRtnInByte);

                // Transaction block to save generated report
                _logger.LogInfo("Starting transaction");

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromSeconds(30)))
                {
                    try
                    {
                        //if (loDataParameterSaveStorage == null)
                        //{
                        //    throw new Exception($"Data for Invoice {poParameter.CINVOICE_CODE} not found");
                        //}

                        //loConn = loDb.GetConnection();
                        //loConnAttr = _connectionAttribute;

                        //_logger.LogInfo("Connection in TransactionScope established");

                        //// Save the report file in the database
                        //PMR01700ParamSaveStorageDTO loParameterSaveReportFile = new PMR01700ParamSaveStorageDTO
                        //{
                        //    CCOMPANY_ID = _cCompanyId,
                        //    CUSER_ID = _cUserId,
                        //    CSTORAGE_ID = loDataParameterSaveStorage.CSTORAGE_ID,
                        //    CINVOICE_CODE = poParameter.CINVOICE_CODE,
                        //    CFILE_EXTENSION = loParameter.ReportOutputType.ToString(),
                        //    OFILE_REPORT = loRtnInByte,
                        //    CPROPERTY_ID = loDataParameterSaveStorage.CPROPERTY_ID,
                        //    CDATA_TYPE = "PMT_TRANS_HD",
                        //    CDEPT_CODE = loDataParameterSaveStorage.CDEPT_CODE,
                        //    CTRANS_CODE = loDataParameterSaveStorage.CTRANS_CODE
                        //};

                        //lcStorageId = loCls.SaveReportFile(loParameterSaveReportFile, loConn, loConnAttr);

                        //_logger.LogInfo("Report file saved. Adding data to dictionary");

                        //if (string.IsNullOrEmpty(loDataParameterSaveStorage.CTENANT_ID))
                        //{
                        //    loException.Add("", "Tenant not exist!");
                        //}
                        //if (string.IsNullOrEmpty(loDataParameterSaveStorage.CEMAIL))
                        //{
                        //    loException.Add("", "Email not supplied!");
                        //}

                        _CountReportMade++;
                        var loParamReportData = new PMT02100InvitationValueReportDataDTO
                        {
                            //CSTORAGE_ID = lcStorageId,
                            //CINVOICE_CODE = poParameter.CINVOICE_CODE,
                            CFILE_NAME = $"INVOICE_{poParameter.CREF_NO}_{_CountReportMade}",
                            CFILE_ID = $"INVOICE_{poParameter.CREF_NO}_{_CountReportMade}",
                            //CTENANT_NAME = loDataParameterSaveStorage.CTENANT_NAME,
                            OFILE_DATA_REPORT = loRtnInByte,
                            LDATA_READY = true
                        };

                        AddFileToDictionary(loParamReportData); //(loDataParameterSaveStorage.CTENANT_ID!, loDataParameterSaveStorage.CEMAIL!, loParamReportData);

                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        loException.Add(ex);
                        _logger.LogError(loException);
                    }
                }
            EndBlock:
                _logger.LogInfo("End of R_SingleProcessAsync");
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

        public async Task R_StatusAsync(string poKey, string pcMessage)
        {
            _logger.LogInfo("start R_Status ");
            string lcMethodName = nameof(R_StatusAsync);
            R_Exception loException = new R_Exception();
            DbConnection? loConn = null;
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

        #region status

        public async Task R_SingleSuccessStatusAsync(string poKey, PMT02100InvitationDTO poParameter)
        {
            _logger.LogInfo("start R_SingleSuccessStatus ");
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_SingleSuccessStatusAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            R_Db loDb = new R_Db();
            DbConnection? loConn = null;
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

        public async Task R_SingleErrorStatusAsync(string poKey, PMT02100InvitationDTO poParameter, R_Exception poException)
        {
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_SingleErrorStatusAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            DbCommand loCommand;
            DbConnection? loConn = null;
            //string? lcCmd;
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

        public async Task R_InitDataErrorStatusAsync(string poKey, R_Exception poException)
        {
            R_Exception loException = new R_Exception();
            string lcMethodName = nameof(R_InitDataErrorStatusAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            DbConnection? loConn = null;
            R_Db loDb = new R_Db();
            try
            {
                //string? lcCmd;
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

        public async Task R_ProcessCompleteStatusAsync(string poKey, List<R_Exception> poExceptions)
        {
            _logger.LogInfo("Start R_ProcessCompleteStatus");
            R_Exception loException = new();
            string lcMethodName = nameof(R_ProcessCompleteStatusAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            DbConnection? loConn = null;
            R_Db? loDb = new();
            int lnFlag = poExceptions.Count > 0 ? 9 : 1;

            try
            {
                loConn = loDb.GetConnection();
                _logger.LogInfo("Database connection: {0}", loConn.ConnectionString);

                if (lnFlag == 1)
                {
                    ProcessEmailDistribution(loConn, loDb, loException);
                }

                HandleFinalStatus(loConn, loDb, lnFlag, loException);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(loException);
            }
            finally
            {
                loConn?.Close();
                loDb = null;
            }

            _logger.LogInfo("End R_ProcessCompleteStatus");
            loException.ThrowExceptionIfErrors();
        }

        private void ProcessEmailDistribution(DbConnection loConn, R_Db loDb, R_Exception loException)
        {
            var loCls = new PMT02110InvitationReportCls();
            //foreach (var loDataDictionaryReport in _DataSendEmailReport)
            //{
            //}

            using TransactionScope scope = new(TransactionScopeOption.Required);

            try
            {
                //var (lcTenantId, lcEmailSend) = loDataDictionaryReport.Key;
                //var reportDataList = loDataDictionaryReport.Value;

                var emailFiles = _DataSendEmailReport.Select(reportData => new R_EmailEngineBackObject
                {
                    FileName = reportData.CFILE_NAME,
                    FileExtension = ".pdf",
                    FileId = reportData.CFILE_ID,
                    FileData = reportData.OFILE_DATA_REPORT
                }).ToList();

                //var loDataSaveStorage = reportDataList.Select(reportData => new PMR01700TempTableUpdateStorageDTO
                //{
                //    CSTORAGE_ID = reportData.CSTORAGE_ID,
                //    CINVOICE_CODE = reportData.CINVOICE_CODE
                //}).ToList();

                //var loParameterUpdateDistribute = new PMR01700UpdateDistributeorStorageIdDTO
                //{
                //    LDISTRIBUTE = true,
                //    CCOMPANY_ID = _cCompanyId,
                //    CPROPERTY_ID = _poParamReport.ParameterSP.CPROPERTY_ID,
                //    CUSER_ID = _cUserId,
                //    ODATA_STORAGE_SAVED = loDataSaveStorage
                //};

                var loTemplateEmail = loCls.GetBodyEmailInvoiceDb(
                    new PMT02100ParameterRequestEmailDTO
                    {
                        CCOMPANY_ID = _poParamReport.loParameter.CCOMPANY_ID,
                        CPROPERTY_ID = _poParamReport.loParameter.CPROPERTY_ID,
                        CDEPT_CODE = _poParamReport.loParameter.CDEPT_CODE,
                        CREF_NO = _poParamReport.loParameter.CREF_NO,
                        CTRANS_CODE = _poParamReport.loParameter.CTRANS_CODE
                        //CUSER_ID = _poParamReport.ParameterSP.CUSER_ID
                    },
                    loConn
                );

                loTemplateEmail.CUSER_ID = _poParamReport.loParameter.CUSER_ID;

                //var emailMessageDetails = new PMT02100SendReceiptDTO
                //{
                //    CCOMPANY_ID = _cCompanyId,
                //    CUSER_ID = _cUserId,
                //    CFILE_NAME = reportDataList.First().CFILE_NAME!,
                //    CFILE_ID = reportDataList.First().CFILE_ID!,
                //    CEMAIL_FROM = loTemplateEmail.CEMAIL_FROM,
                //    CEMAIL_TO = loTemplateEmail.CTENANT_EMAIL,
                //    // CEMAIL_TO = "alvan.ghon@gmail.com",
                //    CEMAIL_SUBJECT = loTemplateEmail.CEMAIL_SUBJECT,
                //    CEMAIL_BODY = loTemplateEmail.CEMAIL_BODY,
                //    CSMTP_ID = loTemplateEmail.CSMTP_ID
                //};

                //loCls.UpdateDistributeorStorageId(loParameterUpdateDistribute, loConn);
                loCls.SendEmail(emailFiles, loTemplateEmail, loConn);

                scope.Complete();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
        }

        private void HandleFinalStatus(DbConnection loConn, R_Db loDb, int lnFlag, R_Exception loException)
        {
            if (lnFlag == 9 && loException.Haserror)
            {
                string lcErrMsg = $"ERROR run method {nameof(R_ProcessCompleteStatusAsync)}";
                if (loException.ErrorList.Count > 0)
                {
                    lcErrMsg = $"{lcErrMsg} with message {loException.ErrorList.First().ErrDescp}".Replace("'", "").Replace("\"", "");
                }

                string lcQuery = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID, CUSER_ID, CKEY_GUID, ISEQ_NO, CERROR_MESSAGE) " +
                                 $"VALUES('{_oKey.COMPANY_ID}', '{_oKey.USER_ID}', '{_oKey.KEY_GUID}', -1, '{lcErrMsg}')";
                loDb.SqlExecNonQuery(lcQuery, loConn, false);
            }

            string lcCmd = $"EXEC RSP_WriteUploadProcessStatus '{_oKey.COMPANY_ID}', '{_oKey.USER_ID}', '{_oKey.KEY_GUID}', {_GetPropCount(_nMaxData)}, 'Finish Process', {lnFlag}";
            loDb.SqlExecNonQuery(lcCmd, loConn, false);
        }

        #endregion

        #region Utilities

        private int _GetPropCount(int pnCount)
        {
            if (_nMaxData == 0)
                return 0;
            return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(100) * pnCount / _nMaxData));
        }

        private void AddFileToDictionary(PMT02100InvitationValueReportDataDTO poReportData) //(string pcTenantId, string pcEmailSend, PMT02100InvitationValueReportDataDTO poReportData)
        {
            //var key = (pcTenantId, pcEmailSend);

            //lock (_DataSendEmailReport) // lock to prevent race condition
            //{
            //    if (!_DataSendEmailReport.ContainsKey(key))
            //    {
            //        _DataSendEmailReport[key] = new List<PMT02100InvitationValueReportDataDTO>();
            //    }
            //    _DataSendEmailReport[key].Add(poReportData);
            //}

            lock (_DataSendEmailReport)
            {
                _DataSendEmailReport.Add(poReportData);
            }
        }

        #endregion
    }
}