using BaseHeaderReportCOMMON;
using PMA00400BackResources;
using PMA00400Common.DTO;
using PMA00400Common.Parameter;
using PMA00400Logger;
using R_BackEnd;
using R_Common;
using R_ReportServerClient;
using R_ReportServerCommon;
using R_Storage;
using R_StorageCommon;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text.Json;
using System.Transactions;
using R_FileType = R_ReportServerCommon.R_FileType;

namespace PMA00400Back
{
    public class PMA00400Cls
    {
        private ConsoleLogger _logger;
        public PMA00400Cls()
        {
            _logger = ConsoleLogger.R_GetInstanceLogger();
        }
        public async Task ProcessHandoverDistribute(string TenantCustomerId, string UserId, string ConnectionString)
        {
            R_Exception loEx = new R_Exception();

            string lcMethodName = nameof(ProcessHandoverDistribute);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loDb = new R_Db();
            R_ConnectionAttribute _connectionAttribute;
            //  DbConnection loConn = null;

            R_ReportServerRule loReportRule;
            string lcReportFileName = "";
            R_FileType leReportOutputType;
            ReportClientParameterDTO loClientReportParameter = new ReportClientParameterDTO();
            R_ReportServerCommon.R_ReportFormatDTO? loReportFormat = null;
            lcReportFileName = "PMA00400DistributeHandover.frx";
            _logger.LogInfo("Get file Frx");
            var listDataHeader = await GetDataHeaderAsync(ConnectionString: ConnectionString);

            string lcCompany = "";

            try
            {
                foreach (var item in listDataHeader)
                {
                    //var item = listDataHeader.First();
                    item.CUSER_ID = UserId;
                    _logger.LogInfo("assign loReportRule");
                    loReportRule = new R_ReportServerRule(TenantCustomerId.ToLower(), item.CCOMPANY_ID.ToLower());

                    if (lcCompany != item.CCOMPANY_ID)
                    {
                        _logger.LogInfo("get ClientReportFormat");
                        loClientReportParameter = await GetClientReportFormatAsync(CompanyId: item.CCOMPANY_ID, ConnectionName: ConnectionString);

                        loReportFormat = new R_ReportServerCommon.R_ReportFormatDTO()
                        {
                            DecimalSeparator = loClientReportParameter.CNUMBER_FORMAT,
                            GroupSeparator = loClientReportParameter.CNUMBER_FORMAT == "." ? "," : ".",
                            DecimalPlaces = loClientReportParameter.IDECIMAL_PLACES,
                            LongDate = loClientReportParameter.CDATE_LONG_FORMAT,
                            ShortDate = loClientReportParameter.CDATE_SHORT_FORMAT,
                            ShortTime = loClientReportParameter.CTIME_SHORT_FORMAT,
                            LongTime = loClientReportParameter.CTIME_LONG_FORMAT,
                        };

                        lcCompany = item.CCOMPANY_ID;
                    }

                    //Set Report Rule and Name

                    //Prepare Parameter
                    leReportOutputType = R_ReportServerCommon.R_FileType.PDF;
                    string lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;
                    _logger.LogInfo("Create object Report Parameter");

                    CultureInfo loCultureInfo = new CultureInfo(loClientReportParameter.CREPORT_CULTURE ?? "EN");

                    _logger.LogInfo("Set Label Report");
                    var loLabel = AssignValuesWithMessages(typeof(Resources_PMA00400), loCultureInfo, new PMA00400LabelDTO());

                    PMA00400ParamDTO loParameter = new()
                    {
                        CCOMPANY_ID = item.CCOMPANY_ID,
                        CPROPERTY_ID = item.CPROPERTY_ID,
                        CDEPT_CODE = item.CDEPT_CODE,
                        CTRANS_CODE = item.CTRANS_CODE,
                        CREF_NO = item.CREF_NO,
                        LASSIGNED = true,
                        CUNIT_ID = item.CUNIT_ID,
                        CFLOOR_ID = item.CFLOOR_ID,
                        CBUILDING_ID = item.CBUILDING_ID,
                        CLANG_ID = loClientReportParameter.CREPORT_CULTURE
                    };

                    _logger.LogInfo("Get Data HANDOVER_EMPLOYEE");
                    List<PMA00400EmployeeDTO> listDataHandoverEmployee = await GetDataHandoverEmployeeAsync(poParameter: loParameter, ConnectionString: ConnectionString);

                    _logger.LogInfo("Get Data HANDOVER_Checklist");
                    List<PMA00400HandoverChecklistDTO> listDataHandoverChecklist = await GetDataHandoverChecklistAsync(poParameter: loParameter, ConnectionString: ConnectionString);

                    _logger.LogInfo("Get Data HANDOVER_Utility");
                    List<PMA00400UtilityDTO> listDataHandoverUtility = await GetDataHandoverUtilityAsync(poParameter: loParameter, ConnectionString: ConnectionString);

                    List<PMA00400GetImageDTO> listDataImagesChecklist = new();
                    List<PMA00400GetImageDTO> listDataImagesUtility = new();
                    if (item.LINCLUDE_IMAGE)
                    {
                        _logger.LogInfo("Get Data HANDOVER_IMAGES_CHECKLIST");
                        listDataImagesChecklist = await GetImagesDataAsync(poParameter: loParameter, pcImagesType: "01", ConnectionString: ConnectionString);
                        _logger.LogInfo("Get Data HANDOVER_IMAGES_UTILITY");
                        listDataImagesUtility = await GetImagesDataAsync(poParameter: loParameter, pcImagesType: "02", ConnectionString: ConnectionString);
                    }

                    _logger.LogInfo("Get Connection Attribute");
                    _connectionAttribute = loDb.GetConnectionAttribute(ConnectionString);

                    BaseHeaderDTO loHeader = new BaseHeaderDTO()
                    {
                        BLOGO_COMPANY = loClientReportParameter.OCOMPANY_LOGO,
                        CCOMPANY_NAME = loClientReportParameter.CCOMPANY_NAME,
                        CPRINT_NAME = "HANDOVER SUMMARY",
                        CUSER_ID = item.CUSER_ID
                    };

                    PMA00400ResultDataDTO resultData = new PMA00400ResultDataDTO()
                    {
                        Label = (PMA00400LabelDTO)loLabel,
                        HeaderData = item,
                        EmployeeData = listDataHandoverEmployee,
                        HandoverChecklistData = listDataHandoverChecklist,
                        UtilityData = listDataHandoverUtility
                    };
                    // add image if field LINCLUDE_IMAGE = true
                    if (item.LINCLUDE_IMAGE)
                    {
                        _logger.LogInfo("Convert Storage Id to Image data");

                        _logger.LogInfo("Convert Storage Id to Image Checklist data");
                        foreach (var itemImageChecklist in listDataImagesChecklist)
                        {
                            R_ReadResult DataTemp = await ConvertStorageIdtoImageData(pcConnectionString: ConnectionString, pcStorageId: itemImageChecklist.CIMAGE_STORAGE_ID);
                            if (DataTemp != null)
                            {
                                itemImageChecklist.OData = DataTemp.Data;
                            }
                        }
                        _logger.LogInfo("Add Image Checklist on main Data");
                        resultData.ChecklistImageData = listDataImagesChecklist;

                        _logger.LogInfo("Convert Storage Id to Image Utility data");
                        foreach (var itemImageUtility in listDataImagesUtility)
                        {
                            R_ReadResult DataTemp = await ConvertStorageIdtoImageData(pcConnectionString: ConnectionString, pcStorageId: itemImageUtility.CIMAGE_STORAGE_ID);
                            if (DataTemp != null)
                            {
                                itemImageUtility.OData = DataTemp.Data;
                            }
                        }
                        _logger.LogInfo("Add Image Utility on main Data");
                        resultData.UtilityImageData = listDataImagesUtility;
                    }

                    PMA00400ResultWithHeaderDTO resultDataWithHeader = new PMA00400ResultWithHeaderDTO()
                    {
                        BaseHeaderData = loHeader,
                        PMA00400ResulDataFormatDTO = resultData
                    };

                    _logger.LogInfo(" get Parameter for Report");
                    R_GenerateReportParameter loParameterReport = new R_GenerateReportParameter()
                    {
                        ReportRule = loReportRule,
                        ReportFileName = lcReportFileName,
                        ReportData = JsonSerializer.Serialize(resultDataWithHeader),
                        ReportDataSourceName = "ResponseDataModel",
                        ReportFormat = loReportFormat,
                        ReportDataType = typeof(PMA00400ResultWithHeaderDTO).ToString(),
                        ReportOutputType = leReportOutputType,
                        ReportAssemblyName = "PMA00400Common.dll",
                        ReportParameter = null
                    };

                    _logger.LogInfo("Generate Report");
                    byte[]? loRtnInByte = null;

                    try
                    {
                        var abc = R_ReportServerClientService.R_GetHttpClient();
                        loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(R_ReportServerClientService.R_GetHttpClient(),
                        "api/ReportServer/GetReport", loParameterReport);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(string.Format("Log Error {0} when convert Data to report byte ", ex));
                        loEx.Add(ex);
                    }

                    if (loRtnInByte == null)
                        continue; // Skip process below

                    string fileExtension = leReportOutputType.ToString();
                    _logger.LogInfo("Declare param to save storage");

                    var ParamSaveStorage = new ParamToSaveStorageDTO()
                    {
                        CCOMPANY_ID = item.CCOMPANY_ID,
                        CUSER_ID = item.CUSER_ID,
                        CREF_NO = item.CREF_NO,
                        CPROPERTY_ID = item.CPROPERTY_ID,
                        CREC_ID = item.CREC_ID,
                        CUNIT_ID = item.CUNIT_ID,
                        CFLOOR_ID = item.CFLOOR_ID,
                        CBUILDING_ID = item.CBUILDING_ID,
                        FileExtension = fileExtension,
                        REPORT = loRtnInByte
                    };
                    _logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(ParamSaveStorage));

                    //Start Transaction BLOCK
                    _logger.LogInfo("Start Transaction Block");
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
                    {
                        _logger.LogInfo("Get connection in transcope [Connection and Connection attribute]");
                        var loConn = await loDb.GetConnectionAsync(ConnectionString);

                        //if (abcad != null)
                        if (loRtnInByte != null)
                        {
                            string lcStorageId = await SaveReportToAzureAsync(ParamSaveStorage, loConn, _connectionAttribute);
                            PMA00400ParamDTO loSavePDFPArameter = new()
                            {
                                CCOMPANY_ID = item.CCOMPANY_ID,
                                CPROPERTY_ID = item.CPROPERTY_ID,
                                CDEPT_CODE = item.CDEPT_CODE,
                                CTRANS_CODE = item.CTRANS_CODE,
                                CREF_NO = item.CREF_NO,
                                CUNIT_ID = item.CUNIT_ID,
                                CFLOOR_ID = item.CFLOOR_ID,
                                CBUILDING_ID = item.CBUILDING_ID,
                                CSTORAGE_ID = lcStorageId,
                                CPROGRAM_ID = "PMA00400",
                            };
                            string lcReturnSavePDF = await SaveReportPDFAsync(loSavePDFPArameter, loConn);
                            scope.Complete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Unexpected error {0} in foreach inside {1} method", ex, lcMethodName));
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<List<PMA00400HeaderDTO>> GetDataHeaderAsync(string ConnectionString)
        {
            string lcMethodName = nameof(GetDataHeaderAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400HeaderDTO> loResult = new List<PMA00400HeaderDTO>();
            DbCommand? loCommand = null;
            R_Db? loDb = null;
            DbConnection? loConn = null;
            R_ReadParameter loReadParameter;
            R_ReadResult? loReadResult = null;

            try
            {
                loDb = new R_Db();
                //var abc = loDb.GetConnection(eDbConnectionStringType.MainConnectionString);
                loConn = await loDb.GetConnectionAsync(ConnectionString);
                loCommand = loDb.GetCommand();

                var lcQuery = "RSP_PMA00400_GET_HEADER_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                _logger.LogInfo(string.Format("Execute query on method {0} ", lcMethodName));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<PMA00400HeaderDTO>(loReturnTemp).ToList();

                _logger.LogInfo(string.Format("Convert string to datetime format on method {0} ", lcMethodName));
                if (loResult.Count > 0)
                {
                    foreach (var item in loResult)
                    {
                        R_Exception loExceptionDt = new();
                        try
                        {
                            _logger.LogInfo(string.Format("Get Employee Signature Image {0} ", item.CREF_NO));

                            if (!string.IsNullOrEmpty(item.CEMPLOYEE_SIGNATURE_STORAGE_ID))
                            {

                                loReadParameter = new R_ReadParameter()
                                {
                                    StorageId = item.CEMPLOYEE_SIGNATURE_STORAGE_ID
                                };

                                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                                item.OData_EMPLOYEE_SIGNATURE = loReadResult.Data;
                                _logger.LogInfo(string.Format("Get Employee Signature Image found {0} ", item.CREF_NO));
                            }
                        }
                        catch (Exception exDt)
                        {
                            _logger.LogInfo(string.Format("Employee Signature Image notfound {0}", item.CREF_NO));
                            loExceptionDt.Add(exDt);
                        }

                        try
                        {
                            _logger.LogInfo(string.Format("Get Tenant Signature Image  {0}", item.CREF_NO));

                            if (!string.IsNullOrEmpty(item.CTENANT_SIGNATURE_STORAGE_ID))
                            {
                                loReadParameter = new R_ReadParameter()
                                {
                                    StorageId = item.CTENANT_SIGNATURE_STORAGE_ID
                                };

                                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                                item.OData_TENANT_SIGNATURE = loReadResult.Data;
                            }
                        }
                        catch (Exception exDt)
                        {
                            _logger.LogInfo(string.Format("Employee Signature Image notfound {0}", item.CREF_NO));
                            loExceptionDt.Add(exDt);
                        }

                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                        item.DCONFIRMED_HO_DATE = ConvertStringToDateTimeFormat(item.CCONFIRMED_HO_DATE);
                        item.DSCHEDULED_HO_DATE = ConvertStringToDateTimeFormat(item.CSCHEDULED_HO_DATE);
                        item.DHO_ACTUAL_DATE = ConvertStringToDateTimeFormat(item.CHO_ACTUAL_DATE);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public async Task<List<PMA00400EmployeeDTO>> GetDataHandoverEmployeeAsync(PMA00400ParamDTO poParameter, string ConnectionString)
        {
            R_Exception loException = new();
            string lcMethodName = nameof(GetDataHandoverEmployeeAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            List<PMA00400EmployeeDTO> loResult = new List<PMA00400EmployeeDTO>();
            DbCommand? loCommand = null;
            R_Db? loDb = null;
            DbConnection? loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = await loDb.GetConnectionAsync(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PM_GET_HANDOVER_EMPLOYEE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE ", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO ", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@LASSIGNED", DbType.Boolean, 10, poParameter.LASSIGNED);

                _logger.LogInfo(string.Format("Execute query on method", lcMethodName));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<PMA00400EmployeeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public async Task<List<PMA00400HandoverChecklistDTO>> GetDataHandoverChecklistAsync(PMA00400ParamDTO poParameter, string ConnectionString)
        {
            string lcMethodName = nameof(GetDataHandoverChecklistAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400HandoverChecklistDTO> loResult = new List<PMA00400HandoverChecklistDTO>();
            DbCommand? loCommand = null;
            R_Db? loDb = null;
            DbConnection? loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = await loDb.GetConnectionAsync(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PM_GET_HANDOVER_CHECKLIST_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE ", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID ", DbType.String, 20, poParameter.CBUILDING_ID);

                _logger.LogInfo(string.Format("Execute query on method", lcMethodName));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<PMA00400HandoverChecklistDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        public async Task<List<PMA00400UtilityDTO>> GetDataHandoverUtilityAsync(PMA00400ParamDTO poParameter, string ConnectionString)
        {
            string lcMethodName = nameof(GetDataHandoverUtilityAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400UtilityDTO> loResult = new List<PMA00400UtilityDTO>();
            DbCommand? loCommand = null;
            R_Db? loDb = null;
            DbConnection? loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = await loDb.GetConnectionAsync(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PM_GET_HANDOVER_UTILITY_LIST ";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE ", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID ", DbType.String, 20, poParameter.CBUILDING_ID);

                _logger.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<PMA00400UtilityDTO>(loReturnTemp).ToList();
                if (loResult.Count > 0)
                {
                    foreach (var item in loResult)
                    {
                        item.CSTART_INV_PRD_CONVERT = ConvertToMonthYear(item.CSTART_INV_PRD);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public async Task<List<PMA00400GetImageDTO>> GetImagesDataAsync(PMA00400ParamDTO poParameter, string pcImagesType, string ConnectionString)
        {
            string lcMethodName = nameof(GetImagesDataAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400GetImageDTO> loResult = new List<PMA00400GetImageDTO>();
            DbCommand? loCommand = null;
            R_Db? loDb = null;
            DbConnection? loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = await loDb.GetConnectionAsync(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PMA00400_GET_IMAGES";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE ", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID ", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CIMAGE_TYPE	 ", DbType.String, 2, pcImagesType);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);

                _logger.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<PMA00400GetImageDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        private async Task<string> SaveReportPDFAsync(PMA00400ParamDTO poParameter, DbConnection poConnection)
        {
            string lcMethodName = nameof(SaveReportPDFAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            string? loResult = null;
            DbCommand? loCommand = null;
            R_Db? loDb = null;
            try
            {
                loDb = new R_Db();

                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PMA00400_SAVE_PDF";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE ", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID ", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSTORAGE_ID	 ", DbType.String, 40, poParameter.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 20, poParameter.CPROGRAM_ID);

                _logger.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = await loDb.SqlExecQueryAsync(poConnection, loCommand, false);
                loResult = R_Utility.R_ConvertTo<string>(loReturnTemp).FirstOrDefault() == null ? "" : R_Utility.R_ConvertTo<string>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);

            }
            finally
            {
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            if (loResult == null)
            {
                loResult = "";
                _logger.LogError("Error " + string.Format(format: "Result on method {0} is null", lcMethodName));
            }

            return loResult;
        }

        public async Task SendEmailAsync(ParamSendEmailDTO poParam, string pcConnectionName)
        {
            string lcMethodName = nameof(SendEmailAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            R_Db? loDb = null;
            DbCommand? loCommand = null;
            DbConnection? loConn = null;

            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync(pcConnectionName);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PMA00400_SEND_EMAIL";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 20, poParam.CPROGRAM_ID);
                loDb.R_AddCommandParameter(loCommand, "@CGET_FILE_API_URL", DbType.String, -1, poParam.CGET_FILE_API_URL);
                loDb.R_AddCommandParameter(loCommand, "@CDB_TENANT_ID", DbType.String, -1, poParam.CDB_TENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 2, poParam.CLANG_ID);
                _logger.LogInfo(string.Format("Execute query {0} on method {1}", lcQuery, lcMethodName));
                var loReturnTemp = await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }
        public async Task<ReportClientParameterDTO> GetClientReportFormatAsync(string CompanyId, string ConnectionName)
        {
            string lcMethodName = nameof(GetClientReportFormatAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            ReportClientParameterDTO? loResult = null;
            R_Db? loDb = null;
            DbCommand? loCommand = null;
            DbConnection? loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = await loDb.GetConnectionAsync(ConnectionName);
                loCommand = loDb.GetCommand();
                var lcQuery = $"select * from SAM_COMPANIES where CCOMPANY_ID ='{CompanyId}'";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _logger.LogInfo(string.Format("Execute query on method {0}", lcMethodName));

                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<ReportClientParameterDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            if (loResult == null)
            {
                loResult = new ReportClientParameterDTO();
                _logger.LogError("Error " + string.Format(format: "Result on method {0} is null", lcMethodName));
            }

            return loResult;
        }
        public async Task<BaseHeaderDTO> GetLogoCompanyAsync(PMA00400ParamDTO poParameter)
        {
            string? lcMethodName = nameof(GetLogoCompanyAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            var loEx = new R_Exception();
            BaseHeaderDTO? loResult = null;
            R_Db? loDb = null;
            DbCommand? loCommand = null;
            DbConnection? loConn = null;
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCommand = loDb.GetCommand();
                string lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 15, poParameter.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<BaseHeaderDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loEx.ThrowExceptionIfErrors();

            if (loResult == null)
            {
                loResult = new BaseHeaderDTO();
                _logger.LogError("Error " + string.Format(format: "Result on method {0} is null", lcMethodName));
            }

            return loResult;
        }
        private async Task<string> SaveReportToAzureAsync(ParamToSaveStorageDTO poParameter, DbConnection poConnection, R_ConnectionAttribute poConnAttribute)
        {
            R_Exception loEx = new R_Exception();
            string lcMethodName = nameof(SaveReportToAzureAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Db? loDb = null;
            DbCommand? loCommand = null;
            string? loResult = null;
            R_SaveResult? loSaveResult = null;
            try
            {
                loDb = new R_Db();
                loCommand = loDb.GetCommand();

                _logger.LogInfo("Get storage type");
                StorageType loGetStorageType = await GetStorageTypeAsync(poParameter, poConnection);

                R_EStorageType loStorageType = loGetStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;
                R_EProviderForCloudStorage loProvider = loGetStorageType.CSTORAGE_PROVIDER_ID!.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;


                // Add and create Storage ID
                if (string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {

                    _logger.LogInfo("Add  storage id to save cause storage id not exist:");
                    R_AddParameter loAddParameter = new R_AddParameter()
                    {
                        StorageType = loStorageType,
                        ProviderCloudStorage = loProvider,
                        FileName = $"Report_HandoverSummary_{poParameter.CREF_NO}",
                        FileExtension = poParameter.FileExtension,
                        UploadData = poParameter.REPORT,
                        UserId = poParameter.CUSER_ID,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = poParameter.CCOMPANY_ID,
                            CDATA_TYPE = "PMT_AGREEMENT_UNIT",
                            CKEY01 = poParameter.CCOMPANY_ID,
                            CKEY02 = poParameter.CPROPERTY_ID,
                            CKEY03 = poParameter.CREC_ID,
                            CKEY04 = poParameter.CUNIT_ID,
                            CKEY05 = "05",
                            CKEY06 = "PMA00400",
                            CKEY07 = "1",
                            CKEY08 = poParameter.CFLOOR_ID,
                            CKEY09 = poParameter.CBUILDING_ID,
                        }
                    };
                    _logger.LogInfo("Call R_StorageUtility.AddFile to storage table");
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, poConnection);
                }
                else if (!string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {
                    _logger.LogInfo("update with storage id to save cause storage id already exist:");
                    R_UpdateParameter loUpdateParameter;

                    loUpdateParameter = new R_UpdateParameter()
                    {
                        StorageId = poParameter.CSTORAGE_ID,
                        UploadData = poParameter.REPORT,
                        UserId = poParameter.CUSER_ID,
                        OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                        {
                            FileExtension = poParameter.FileExtension,
                            FileName = $"Report_{poParameter.CREF_NO}.{poParameter.FileExtension}"
                        }
                    };
                    _logger.LogInfo("Call R_StorageUtility.UpdateFile to storage table");
                    loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, poConnection, poConnAttribute.Provider);
                }

                if (loSaveResult != null)
                {
                    loResult = loSaveResult.StorageId;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("Error " + ex);
            }
            finally
            {
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loEx.ThrowExceptionIfErrors();

            if (loResult == null)
            {
                loResult = "";
                _logger.LogError("Error " + string.Format(format: "Result on method {0} is null", lcMethodName));
            }

            return loResult;
        }
        private async Task<StorageType> GetStorageTypeAsync(ParamToSaveStorageDTO poParameter, DbConnection poConnection)
        {
            string lcMethodName = nameof(GetStorageTypeAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            var loEx = new R_Exception();

            StorageType? loResult = null;
            R_Db? loDb = null;
            DbCommand? loCmd = null;

            try
            {
                loDb = new R_Db();
                _logger.LogInfo("before exec RSP_GS_GET_STORAGE_TYPE");
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poParameter.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

                _logger.LogDebug(string.Format("Execute Command {0}", lcQuery));
                _logger.LogDebug(string.Format("Parameter {0} : ", loDbParam));

                try
                {
                    var loDataTable = await loDb.SqlExecQueryAsync(poConnection, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<StorageType>(loDataTable).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.LogError("Error " + ex);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("Error " + ex);
            }
            finally
            {
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();

            if (loResult == null)
            {
                loResult = new StorageType();
                _logger.LogError("Error " + string.Format(format: "Result on method {0} is null", lcMethodName));
            }

            return loResult;
        }
        public async Task<GetDomainDTO> GetDataDomainType_URLAsync(string pcConnectionMultiName)
        {
            string lcMethodName = nameof(GetDataDomainType_URLAsync);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            GetDomainDTO? loResult = null;
            DbCommand? loCommand = null;
            R_Db? loDb = null;
            DbConnection? loConn = null;
            try
            {
                loDb = new R_Db();

                _logger.LogInfo("before exec RSP_SA_GET_DOMAIN_TYPE_URL");
                loConn = await loDb.GetConnectionAsync(pcConnectionMultiName);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_SA_GET_DOMAIN_TYPE_URL";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CDOMAIN_ID", DbType.String, 20, "GENERAL_API");
                loDb.R_AddCommandParameter(loCommand, "@CDOMAIN_TYPE_ID", DbType.String, 20, "HANDOVER_SUMMARY");

                _logger.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
                _logger.LogDebug(string.Format("Execute Command {0}", lcQuery));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<GetDomainDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            if (loResult == null)
            {
                loResult = new GetDomainDTO();
                _logger.LogError("Error " + string.Format(format: "Result on method {0} is null", lcMethodName));
            }

            return loResult;
        }

        #region utility
        private static object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
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
        private static DateTime ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return DateTime.Now;
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
                    return DateTime.Now;
                }
            }
        }
        private string ConvertToMonthYear(string? input)
        {
            // Jika input null atau kosong, gunakan tanggal hari ini
            if (string.IsNullOrWhiteSpace(input))
            {
                return DateTime.Now.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
            }

            // Jika format tidak valid, gunakan tanggal hari ini
            if (!DateTime.TryParseExact(input, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                date = DateTime.Now; // Pakai tanggal hari ini
            }

            return date.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
        }
        private async Task<R_ReadResult> ConvertStorageIdtoImageData(string pcConnectionString, string pcStorageId)
        {
            R_ReadResult? loReadResult = null;
            R_Exception loException = new();
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = await loDb.GetConnectionAsync(pcConnectionString);

                R_ReadParameter readParameter = new R_ReadParameter { StorageId = pcStorageId };
                return R_StorageUtility.ReadFile(readParameter, loConn);
            }
            catch (Exception ex)
            {
                _logger.LogInfo(string.Format("Image not found {0}", pcStorageId));
                loException.Add(ex);
            }

            if (loReadResult == null)
            {
                loReadResult = new R_ReadResult();
                _logger.LogError("Error " + string.Format(format: "Result on method {0} is null", nameof(ConvertStorageIdtoImageData)));
            }

            return loReadResult;
        }
        #endregion
    }
}