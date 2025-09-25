using log4net;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Globalization;
using PMA00400Common.Parameter;
using PMA00400Common.DTO;
using R_Storage;
using R_StorageCommon;
using R_ReportServerCommon;
using System.Reflection.Metadata;
using PMA00400BackResources;
using BaseHeaderReportCOMMON;
using System.Reflection.Emit;
using System.Text.Json;
using R_ReportServerClient;
using System.Transactions;
using log4net.Repository.Hierarchy;
using R_ReportFastReportBack;
using R_FileType = R_ReportServerCommon.R_FileType;
using static R_BackEnd.R_Db;
using System.Windows.Input;
using Microsoft.Extensions.Logging;

namespace PMA00400Back
{
    public class PMA00400Cls
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PMA00400Cls));
        private R_ReportFastReportBackClass _ReportCls;
        public async Task ProcessHandoverDistribute(string TenantCustomerId, string UserId, string ConnectionString)
        {
            R_Exception loEx = new R_Exception();
            string? lcMethodName = nameof(ProcessHandoverDistribute);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_ReportServerRule loReportRule;
            string lcReportFileName = "";
            R_FileType leReportOutputType;
            var loDb = new R_Db();
            ReportClientParameterDTO loClientReportParameter = new ReportClientParameterDTO();
            R_ReportServerCommon.R_ReportFormatDTO loReportFormat = null;
            //  DbConnection loConn = null;
            R_ConnectionAttribute _connectionAttribute;
            lcReportFileName = "PMA00400DistributeHandover.frx";
            _logger.Info("Get file Frx");
            var listDataHeader = GetDataHeader(ConnectionString: ConnectionString);

            string lcCompany = "";


            foreach (var item in listDataHeader)
            {
                //var item = listDataHeader.First();
                item.CUSER_ID = UserId;
                _logger.Info("assign loReportRule");
                loReportRule = new R_ReportServerRule(TenantCustomerId.ToLower(), item.CCOMPANY_ID.ToLower());

                if (lcCompany != item.CCOMPANY_ID)
                {
                    _logger.Info("get ClientReportFormat");
                    loClientReportParameter = GetClientReportFormat(CompanyId: item.CCOMPANY_ID, ConnectionString: ConnectionString);

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
                _logger.Info("Create object Report Parameter");

                CultureInfo loCultureInfo = new CultureInfo(loClientReportParameter.CREPORT_CULTURE ?? "EN");

                _logger.Info("Set Label Report");
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
                _logger!.Info("Get Data HANDOVER_EMPLOYEE");
                List<PMA00400EmployeeDTO> listDataHandoverEmployee = GetDataHandoverEmployee(poParameter: loParameter, ConnectionString: ConnectionString);

                _logger!.Info("Get Data HANDOVER_Checklist");
                List<PMA00400HandoverChecklistDTO> listDataHandoverChecklist = GetDataHandoverChecklist(poParameter: loParameter, ConnectionString: ConnectionString);

                _logger!.Info("Get Data HANDOVER_Utility");
                List<PMA00400UtilityDTO> listDataHandoverUtility = GetDataHandoverUtility(poParameter: loParameter, ConnectionString: ConnectionString);

                List<PMA00400GetImageDTO> listDataImagesChecklist = new();
                List<PMA00400GetImageDTO> listDataImagesUtility = new();
                if (item.LINCLUDE_IMAGE)
                {
                    _logger!.Info("Get Data HANDOVER_IMAGES_CHECKLIST");
                    listDataImagesChecklist = GetImagesData(poParameter: loParameter, pcImagesType: "01", ConnectionString: ConnectionString);
                    _logger!.Info("Get Data HANDOVER_IMAGES_UTILITY");
                    listDataImagesUtility = GetImagesData(poParameter: loParameter, pcImagesType: "02", ConnectionString: ConnectionString);
                }
                _logger!.Info("Get Connection Attribute");
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
                    _logger!.Info(" Convert Storage Id to Image data");

                    _logger!.Info("Convert Storage Id to Image Checklist data");
                    foreach (var itemImageChecklist in listDataImagesChecklist)
                    {
                        R_ReadResult DataTemp = ConvertStorageIdtoImageData(pcConnectionString: ConnectionString, pcStorageId: itemImageChecklist.CIMAGE_STORAGE_ID);
                        if (DataTemp != null)
                        {
                            itemImageChecklist.OData = DataTemp.Data;
                        }
                    }
                    _logger!.Info("Add Image Checklist on main Data");
                    resultData.ChecklistImageData = listDataImagesChecklist;

                    _logger!.Info("Convert Storage Id to Image Utility data");
                    foreach (var itemImageUtility in listDataImagesUtility)
                    {
                        R_ReadResult DataTemp = ConvertStorageIdtoImageData(pcConnectionString: ConnectionString, pcStorageId: itemImageUtility.CIMAGE_STORAGE_ID);
                        if (DataTemp != null)
                        {
                            itemImageUtility.OData = DataTemp.Data;
                        }
                    }
                    _logger!.Info("Add Image Utility on main Data");
                    resultData.UtilityImageData = listDataImagesUtility;
                }

                PMA00400ResultWithHeaderDTO resultDataWithHeader = new PMA00400ResultWithHeaderDTO()
                {
                    BaseHeaderData = loHeader,
                    PMA00400ResulDataFormatDTO = resultData
                };
                _logger!.Info(" get Parameter for Report");
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

                _logger!.Info(" Generate Report");
                byte[] loRtnInByte = null;
                R_Exception loexx = new R_Exception();
                try
                {
                    var abc = R_ReportServerClientService.R_GetHttpClient();
                    loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(R_ReportServerClientService.R_GetHttpClient(),
                    "api/ReportServer/GetReport", loParameterReport);
                }
                catch (Exception e)
                {
                    _logger!.Error(string.Format("Log Error {0} when convert Data to report byte ", e));
                    loexx.Add(e);
                }
                string fileExtension = leReportOutputType.ToString();

                _logger!.Info("Declare param to save storage");

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
                _logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(ParamSaveStorage));
                //Start Transaction BLOCK
                _logger!.Info("Start Transaction Block");
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    _logger.Info("Get connection in transcope [Connection and Connection attribute]");
                    var loConn = loDb.GetConnection(ConnectionString);

                    //if (abcad != null)
                    if (loRtnInByte != null)
                    {
                        string lcStorageId = SaveReportToAzure(ParamSaveStorage, loConn, _connectionAttribute);
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
                        string lcReturnSavePDF = SaveReportPDF(loSavePDFPArameter, loConn);
                        scope.Complete();
                    }
                }
            }
        }
        public List<PMA00400HeaderDTO> GetDataHeader(string ConnectionString)
        {
            string lcMethodName = nameof(GetDataHeader);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400HeaderDTO>? loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            R_ReadParameter loReadParameter;
            R_ReadResult loReadResult = null;
            try
            {
                loDb = new R_Db();
                //var abc = loDb.GetConnection(eDbConnectionStringType.MainConnectionString);
                loConn = loDb.GetConnection(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PMA00400_GET_HEADER_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                _logger!.Info(string.Format("Execute query on method {0} ", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<PMA00400HeaderDTO>(loReturnTemp).ToList();
                _logger!.Info(string.Format("Convert string to datetime format on method {0} ", lcMethodName));
                if (loResult.Count > 0)
                {
                    foreach (var item in loResult)
                    {
                        R_Exception loExceptionDt = new();
                        try
                        {
                            _logger!.Info(string.Format("Get Employee Signature Image {0} ", item.CREF_NO));

                            if (!string.IsNullOrEmpty(item.CEMPLOYEE_SIGNATURE_STORAGE_ID))
                            {

                                loReadParameter = new R_ReadParameter()
                                {
                                    StorageId = item.CEMPLOYEE_SIGNATURE_STORAGE_ID
                                };

                                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                                item.OData_EMPLOYEE_SIGNATURE = loReadResult.Data;
                                _logger!.Info(string.Format("Get Employee Signature Image found {0} ", item.CREF_NO));
                            }
                        }
                        catch (Exception exDt)
                        {
                            _logger!.Info(string.Format("Employee Signature Image notfound {0}", item.CREF_NO));
                            loExceptionDt.Add(exDt);
                        }

                        try
                        {
                            _logger!.Info(string.Format("Get Tenant Signature Image  {0}", item.CREF_NO));

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
                            _logger!.Info(string.Format("Employee Signature Image notfound {0}", item.CREF_NO));
                            loExceptionDt.Add(exDt);
                        }

                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                        item.DCONFIRMED_HO_DATE = ConvertStringToDateTimeFormat(item.CCONFIRMED_HO_DATE);
                        item.DSCHEDULED_HO_DATE = ConvertStringToDateTimeFormat(item.CSCHEDULED_HO_DATE);
                        item.DHO_ACTUAL_DATE = ConvertStringToDateTimeFormat(item.CHO_ACTUAL_DATE);
                    }//
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
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
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
        public List<PMA00400EmployeeDTO> GetDataHandoverEmployee(PMA00400ParamDTO poParameter, string ConnectionString)
        {
            string lcMethodName = nameof(GetDataHandoverEmployee);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400EmployeeDTO>? loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = loDb.GetConnection(ConnectionString);
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

                _logger!.Info(string.Format("Execute query on method", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<PMA00400EmployeeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
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
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
        public List<PMA00400HandoverChecklistDTO> GetDataHandoverChecklist(PMA00400ParamDTO poParameter, string ConnectionString)
        {
            string lcMethodName = nameof(GetDataHandoverChecklist);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400HandoverChecklistDTO>? loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = loDb.GetConnection(ConnectionString);
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

                _logger!.Info(string.Format("Execute query on method", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<PMA00400HandoverChecklistDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
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
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;
        }
        public List<PMA00400UtilityDTO> GetDataHandoverUtility(PMA00400ParamDTO poParameter, string ConnectionString)
        {
            string lcMethodName = nameof(GetDataHandoverUtility);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400UtilityDTO>? loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = loDb.GetConnection(ConnectionString);
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

                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
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
                _logger.Error("Error " + ex);
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
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;
        }
        public List<PMA00400GetImageDTO> GetImagesData(PMA00400ParamDTO poParameter, string pcImagesType, string ConnectionString)
        {
            string lcMethodName = nameof(GetImagesData);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMA00400GetImageDTO>? loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = loDb.GetConnection(ConnectionString);
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

                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<PMA00400GetImageDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
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
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;
        }
        private string SaveReportPDF(PMA00400ParamDTO poParameter, DbConnection poConnection)
        {
            string lcMethodName = nameof(SaveReportPDF);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            string loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();

                loConn = poConnection;
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

                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<string>(loReturnTemp).FirstOrDefault() == null ? "" : R_Utility.R_ConvertTo<string>(loReturnTemp).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;


        }

        public void SendEmail(ParamSendEmailDTO poParam, string poConnection)
        {
            string lcMethodName = nameof(SendEmail);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            ParamSendEmailDTO? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection(poConnection);
                //var loConn = poConnection;
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PMA00400_SEND_EMAIL";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 20, poParam.CPROGRAM_ID);
                loDb.R_AddCommandParameter(loCommand, "@CGET_FILE_API_URL", DbType.String, -1, poParam.CGET_FILE_API_URL);
                loDb.R_AddCommandParameter(loCommand, "@CDB_TENANT_ID", DbType.String, -1, poParam.CDB_TENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 2, poParam.CLANG_ID);
                _logger!.Info(string.Format("Execute query {0} on method {1}", lcQuery, lcMethodName));
                var loReturnTemp = loDb.SqlExecNonQuery(loConn, loCommand, true);
                //loResult = R_Utility.R_ConvertTo<PMA00300ReportClientParameterDTO>(loReturnTemp).ToList().Any() ?
                //           R_Utility.R_ConvertTo<PMA00300ReportClientParameterDTO>(loReturnTemp).FirstOrDefault()! : new PMA00300ReportClientParameterDTO();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
        }
        public ReportClientParameterDTO GetClientReportFormat(string CompanyId, string ConnectionString)
        {
            string lcMethodName = nameof(GetClientReportFormat);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            ReportClientParameterDTO? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();

                var loConn = loDb.GetConnection(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = $"select * from SAM_COMPANIES where CCOMPANY_ID ='{CompanyId}'";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<ReportClientParameterDTO>(loReturnTemp).ToList().Any() ?
                           R_Utility.R_ConvertTo<ReportClientParameterDTO>(loReturnTemp).FirstOrDefault()! : new ReportClientParameterDTO();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
        public BaseHeaderDTO GetLogoCompany(PMA00400ParamDTO poParameter)
        {
            string? lcMethodName = nameof(GetLogoCompany);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            var loEx = new R_Exception();
            BaseHeaderDTO loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();
                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poParameter.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<BaseHeaderDTO>(loDataTable).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.Error("Error " + ex);
            }
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            loEx.ThrowExceptionIfErrors();
            return loResult!;
        }
        private string SaveReportToAzure(ParamToSaveStorageDTO poParameter, DbConnection poConnection, R_ConnectionAttribute poConnAttribute)
        {
            R_Exception loEx = new R_Exception();

            string lcMethodName = nameof(SaveReportToAzure);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            string loResult = null;
            DbConnection loConn = null;
            R_SaveResult loSaveResult = null;
            try
            {
                var loDb = new R_Db();
                loConn = poConnection;
                var _connectionAttribute = poConnAttribute;
                var loCommand = loDb.GetCommand();

                _logger.Info("Get storage type");
                StorageType loGetStorageType = GetStorageType(poParameter, poConnection);

                R_EStorageType loStorageType = loGetStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;
                R_EProviderForCloudStorage loProvider = loGetStorageType.CSTORAGE_PROVIDER_ID!.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;


                // Add and create Storage ID
                if (string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {

                    _logger.Info("Add  storage id to save cause storage id not exist:");
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
                    _logger.Info("Call R_StorageUtility.AddFile to storage table");
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn);
                }
                else if (!string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
                {
                    _logger.Info("update with storage id to save cause storage id already exist:");
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
                    _logger.Info("Call R_StorageUtility.UpdateFile to storage table");
                    loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, _connectionAttribute.Provider);
                }
                if (loSaveResult != null)
                {
                    loResult = loSaveResult.StorageId;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.Error("Error " + ex);
            }
            _logger!.Info(string.Format("END process method {0} on Cls", lcMethodName));
            loEx.ThrowExceptionIfErrors();
            return loResult!;
        }
        private StorageType GetStorageType(ParamToSaveStorageDTO poParameter, DbConnection poConnection)
        {
            string lcMethodName = nameof(GetStorageType);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            var loEx = new R_Exception();
            StorageType loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                _logger!.Info("before exec RSP_GS_GET_STORAGE_TYPE");
                loConn = poConnection;
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poParameter.CUSER_ID);

              
                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

                _logger!.Debug(string.Format("Execute Command {0}", lcQuery));
                _logger!.Debug(string.Format("Parameter {0} : ", loDbParam));
                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<StorageType>(loDataTable).FirstOrDefault()!;
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.Error("Error " + ex);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex); 
                _logger.Error("Error " + ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult!;
        }
        public GetDomainDTO GetDataDomainType_URL(string pcConnectionMultiName)
        {
            string lcMethodName = nameof(GetDataDomainType_URL);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            GetDomainDTO? loResult = null;
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();

                //loConn = loDb.GetConnection();
                _logger!.Info("before exec RSP_SA_GET_DOMAIN_TYPE_URL");
                loConn = loDb.GetConnection(pcConnectionMultiName);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_SA_GET_DOMAIN_TYPE_URL";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CDOMAIN_ID", DbType.String, 20, "GENERAL_API");
                loDb.R_AddCommandParameter(loCommand, "@CDOMAIN_TYPE_ID", DbType.String, 20, "HANDOVER_SUMMARY");

                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                _logger!.Debug(string.Format("Execute Command {0}", lcQuery));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GetDomainDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.Error("Error " + ex);
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
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
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
        private R_ReadResult ConvertStorageIdtoImageData(string pcConnectionString, string pcStorageId)
        {
            R_ReadResult loReadResult = null;
            R_Exception loException = new();
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection(pcConnectionString);

                R_ReadParameter readParameter = new R_ReadParameter { StorageId = pcStorageId };
                return R_StorageUtility.ReadFile(readParameter, loConn);
            }
            catch (Exception ex)
            {
                _logger!.Info(string.Format("Image notfound {0}", pcStorageId));
                loException.Add(ex);
            }
            return loReadResult;
        }
        #endregion
    }
}