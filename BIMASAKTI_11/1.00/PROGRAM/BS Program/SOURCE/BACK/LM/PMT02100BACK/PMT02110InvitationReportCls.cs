using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using R_Storage;
using R_StorageCommon;
using PMT02100COMMON.Loggers;
using PMT02100BACK.OpenTelemetry;
using PMT02100REPORTCOMMON.DTOs.PMT02100PDF;
using PMT02100COMMON.DTOs.PMT02120Print;
using R_CommonFrontBackAPI.Log;
using BaseHeaderReportCOMMON;
using PMT02100REPORTCOMMON.DTOs.PMT02100Email;
using R_CommonFrontBackAPI;
using PMT02100COMMON.DTOs.PMT02110;
using Microsoft.SqlServer.Server;
using PMT02100COMMON.DTOs.PMT02120;

namespace PMT02100BACK
{
    public class PMT02110InvitationReportCls
    {
        RSP_PM_HANDOVER_REINVITEResources.Resources_Dummy_Class _loRspReinvite = new RSP_PM_HANDOVER_REINVITEResources.Resources_Dummy_Class();
        RSP_PM_HANDOVER_CONFIRM_SCHEDULEResources.Resources_Dummy_Class loRspConfirmSchedule = new RSP_PM_HANDOVER_CONFIRM_SCHEDULEResources.Resources_Dummy_Class();

        private readonly LoggerPMT02110Invitation? _logger;
        private readonly ActivitySource _activitySource;

        public PMT02110InvitationReportCls()
        {
            _logger = LoggerPMT02110Invitation.R_GetInstanceLogger(); ;
            _activitySource = PMT02110InvitationActivitySourceBase.R_GetInstanceActivitySource(); ;
        }

        #region Print Function

        public List<PMT02100InvitationDTO> GetDataPrintHeaderDB(PMT02100InvitationParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetDataPrintHeaderDB);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            List<PMT02100InvitationDTO> loReturn = new List<PMT02100InvitationDTO>();
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db? loDb;

            try
            {
                _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);


                //var tempData = R_Utility.R_ConvertCollectionToCollection<PMR01700ParameterDataListDTO, PMR01700ParameterDataListDTO>(poParameter.DataReport);
                //lcQuery = "CREATE TABLE #TEMP_INVOICE " +
                //    "(CINVOICE_CODE VARCHAR(20)) ";
                //_logger.LogDebug("CREATE TABLE #TEMP_INVOICE");

                //loDb.SqlExecNonQuery(lcQuery, loConn, false);
                //loDb.R_BulkInsert((SqlConnection)loConn, "#TEMP_INVOICE", tempData);
                //_logger.LogDebug("R_BulkInsert To TABLE #TEMP_INVOICE");

                //_logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));

                lcQuery = "RSP_PM_HANDOVER_INVITATION_PAYMENT_HEADER";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 20, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                // loDb.R_AddCommandParameter(loCommand, "@LPRINT", DbType.Boolean, 2, poParameter.ParameterSP.LPRINT);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);

                    _logger.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
                    loReturn = R_Utility.R_ConvertTo<PMT02100InvitationDTO>(loDataTable).ToList();
                    _logger.LogDebug("{@ObjectReturn}", loReturn);

                    if (loReturn.Count > 0)
                    {
                        loReturn.ForEach(x => x.DSCHEDULED_HO_DATE = DateTime.ParseExact((x.CSCHEDULED_HO_DATE + " " + x.CSCHEDULED_HO_TIME), "yyyyMMdd HH:mm", CultureInfo.InvariantCulture));
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _logger.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<PMT02100InvitationDetailDTO> GetDataPrintDetailDB(PMT02100InvitationDetailParameterDTO poParameter, DbConnection poConnection)
        {
            string? lcMethod = nameof(GetDataPrintDetailDB);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            List<PMT02100InvitationDetailDTO> loReturn = new List<PMT02100InvitationDetailDTO>();
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = poConnection;
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);


                //var tempData = R_Utility.R_ConvertCollectionToCollection<PMR01700ParameterDataListDTO, PMR01700ParameterDataListDTO>(poParameter.DataReport);
                //lcQuery = "CREATE TABLE #TEMP_INVOICE " +
                //    "(CINVOICE_CODE VARCHAR(20)) ";
                //_logger.LogDebug("CREATE TABLE #TEMP_INVOICE");

                //loDb.SqlExecNonQuery(lcQuery, loConn, false);
                //loDb.R_BulkInsert((SqlConnection)loConn, "#TEMP_INVOICE", tempData);
                //_logger.LogDebug("R_BulkInsert To TABLE #TEMP_INVOICE");

                //_logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));

                lcQuery = "RSP_PM_HANDOVER_INVITATION_PAYMENT_DETAIL";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 20, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                // loDb.R_AddCommandParameter(loCommand, "@LPRINT", DbType.Boolean, 2, poParameter.ParameterSP.LPRINT);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);

                    _logger.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
                    loReturn = R_Utility.R_ConvertTo<PMT02100InvitationDetailDTO>(loDataTable).ToList();
                    _logger.LogDebug("{@ObjectReturn}", loReturn);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _logger.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        //public PMT02100InvitationResultWithBaseHeaderPrintDTO GenerateDataPrint(PMT02100InvitationParameterDTO poParam, DbConnection poConnection, List<PMT02100InvitationDTO> poDataReport)
        //{
        //    string lcMethodName = nameof(GenerateDataPrint);
        //    using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        //    _logger.LogInfo(string.Format("START method {0}", lcMethodName));

        //    var loException = new R_Exception();
        //    PMT02100InvitationResultWithBaseHeaderPrintDTO? loReturn = null;
        //    //PMR01600Cls? loCls = null;
        //    //BaseAOCParameterCompanyAndUserDTO? loParameterInternal;
        //    //PMR00150SummaryResultDTO? loData = null;
        //    CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
        //    try
        //    {

        //        _logger.LogInfo("Init Cls");
        //        //loCls = new PMR01600Cls();

        //        _logger.LogInfo("Init Object return");
        //        loReturn = new PMT02100InvitationResultWithBaseHeaderPrintDTO();

        //        _logger.LogInfo("Create Object Print PMR01600");
        //        var loRawDataPrint = new List<PMT02100InvitationDTO>();
        //        loParameterInternal = new BaseAOCParameterCompanyAndUserDTO();
        //        //loParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID ?? poParam.ParameterSP.CCOMPANY_ID;
        //        //loParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID ?? poParam.ParameterSP.CUSER_ID;
        //        loParameterInternal.CCOMPANY_ID = poParam.ParameterSP.CCOMPANY_ID;
        //        loParameterInternal.CUSER_ID = poParam.ParameterSP.CUSER_ID;
        //        _logger.LogInfo("Get Data Report");
        //        loRawDataPrint = poDataReport;


        //        _logger.LogInfo("Set Parameter Db For Header Parameter");
        //        PMR01700ParameterGetPropertyPMR01600DTO loParameterGetPropertyPMR01600Db = new PMR01700ParameterGetPropertyPMR01600DTO()
        //        {
        //            //CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
        //            CPROPERTY_ID = poParam.ParameterSP.CPROPERTY_ID,
        //            //CUSER_ID = R_BackGlobalVar.USER_ID
        //        };
        //        _logger.LogInfo("Get Header Report");
        //        var loDataHeaderReport = GetPropertyPMR01600Db(poParameterInternal: loParameterInternal, poParameter: loParameterGetPropertyPMR01600Db, poConnection: poConnection);

        //        _logger.LogInfo("Set Header Report");
        //        var loBaseHeaderData = new PMR01600BaseHeaderDataDTO()
        //        {
        //            OLOGO = loDataHeaderReport.OLOGO,
        //            P_PROPERTY_NAME = loDataHeaderReport.CPROPERTY_ID + " - " + loDataHeaderReport.CPROPERTY_NAME,
        //            P_PROPERTY_ADDRESS = string.Join(" ", new[]
        //                                            {
        //                                                loDataHeaderReport.CADDRESS,
        //                                                loDataHeaderReport.CPROVINCE,
        //                                                loDataHeaderReport.CCITY,
        //                                                loDataHeaderReport.CDISTRICT,
        //                                                loDataHeaderReport.CSUBDISTRICT
        //                                            }.Where(part => !string.IsNullOrWhiteSpace(part))),
        //        };

        //        //_logger.LogInfo("Get Base Header Column");
        //        //var loColumnBaseHeader = AssignValuesWithMessages(typeof(Resource_PMR01600_Class), loCultureInfo, new PMR01600BaseHeaderColumnDTO());

        //        _logger.LogInfo("Get Column");
        //        //var loColumn = AssignValuesWithMessages(typeof(Resource_PMR01600_Class), loCultureInfo, new PMR01600ColumnDTO());
        //        var loColumn = AssignValuesWithMessages(typeof(PMT02100BackResources.Resources_Dummy_Class), loCultureInfo, new PMT02100InvitationColumnDTO);

        //        //_logger.LogInfo("Get Label");
        //        //var loLabel = AssignValuesWithMessages(typeof(Resource_PMR01600_Class), loCultureInfo, new PMR01600LabelDTO());

        //        //_logger.LogInfo("Get Label Data");
        //        //var loLabelData = AssignValuesWithMessages(typeof(Resource_PMR01600_Class), loCultureInfo, new PMR01600LabelDataDTO());


        //        _logger.LogInfo("Generate Header Data For Print");
        //        loReturn = new PMT02100InvitationResultWithBaseHeaderPrintDTO();
        //        loReturn.PageHeader = new PMR01600BaseHeaderDTO();
        //        loReturn.PageHeader.BaseHeaderData = new PMR01600BaseHeaderDataDTO();
        //        loReturn.PageHeader.BaseHeaderData = loBaseHeaderData;

        //        loReturn.PageHeader.BaseHeaderColumn = new PMR01600BaseHeaderColumnDTO();
        //        loReturn.PageHeader.BaseHeaderColumn = (PMR01600BaseHeaderColumnDTO)loColumnBaseHeader;

        //        _logger.LogInfo("Generate Data For Print");
        //        loReturn.Column = new PMR01600ColumnDTO();
        //        loReturn.Column = loColumn as PMR01600ColumnDTO;

        //        loReturn.Data = new List<PMT02100InvitationDTO>();
        //        var loDataPrint = loRawDataPrint.Any() ? ConvertResultToFormatPrint(loRawDataPrint) : new List<PMT02100InvitationDTO>();
        //        loReturn.Data = loDataPrint;

        //        //Handle BANK VA// Ambil semua CTENANT_ID dari loReturn.Data
        //        var loListTenantId = loReturn.Data
        //  .Select(data => data.CTENANT_ID)
        //  .Distinct()
        //  .ToList();

        //        // Dictionary untuk menyimpan hasil dari SP per tenant
        //        //var loTenantBankVADictionary = new Dictionary<string, List<PMR01600DataVABankDTO>>();

        //        //BaseAOCParameterCompanyDTO loParameterInternalCompanyId = new BaseAOCParameterCompanyDTO()
        //        //{
        //        //    CCOMPANY_ID = poParam.ParameterSP.CCOMPANY_ID,
        //        //};

        //        //PMR01700requestParameterGetDataBankVADTO loParameterGetDataBankVA = new PMR01700requestParameterGetDataBankVADTO()
        //        //{
        //        //    CPROPERTY_ID = poParam.ParameterSP.CPROPERTY_ID,
        //        //    CTENANT_ID = ""
        //        //};

        //        //// Loop melalui setiap tenant ID
        //        //foreach (var lcTenantId in loListTenantId)
        //        //{
        //        //    loParameterGetDataBankVA.CTENANT_ID = lcTenantId;

        //        //    // Ambil data dari SP untuk tenantId ini
        //        //    var bankVADataList = GenerateDataBankVADb(poParameterInternal: loParameterInternalCompanyId, poParameter: loParameterGetDataBankVA, poConnection: poConnection);

        //        //    // Simpan data bank VA berdasarkan tenant ID
        //        //    loTenantBankVADictionary[key: lcTenantId!] = bankVADataList;
        //        //}

        //        //// Mapping dictionary ke loReturn.Data
        //        //loReturn.Data = loReturn.Data
        //        //    .Select(data =>
        //        //    {
        //        //        // Cek apakah tenant ID ada di dictionary dan ambil data Bank VA
        //        //        if (loTenantBankVADictionary.TryGetValue(data.CTENANT_ID!, out var bankVADataList))
        //        //        {
        //        //            data.DataBankVA = bankVADataList;
        //        //        }
        //        //        else
        //        //        {
        //        //            data.DataBankVA = new List<PMR01600DataVABankDTO>();
        //        //        }
        //        //        return data;
        //        //    }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //        _logger.LogError(ex, "Error in GenerateDataPrint");
        //    }
        //    _logger.LogInfo("END Method GenerateDataPrint on Controller");
        //    loException.ThrowExceptionIfErrors();

        //    return loReturn!;
        //}

        public PMT02100InvitationResultWithBaseHeaderPrintDTO GenerateDataPrint(PMT02100InvitationDTO poParam, DbConnection poConnection, string pcCompanyID)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            _logger.LogInfo("Start || GenerateDataPrint(Controller)");
            R_Exception loException = new R_Exception();
            PMT02100InvitationResultWithBaseHeaderPrintDTO loRtn = new PMT02100InvitationResultWithBaseHeaderPrintDTO();
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            //System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo("id");
            try
            {
                //PMT02120PrintCls loCls = new PMT02120PrintCls();
                loRtn.ReportData = new PMT02100InvitationResultDTO();

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                PMT02100InvitationColumnDTO loColumnObject = new PMT02100InvitationColumnDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMT02100BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);


                //List<PMT02120PrintReportDTO> loCollection = GetDataPrintPMT02100DB(poParam);

                _logger.LogInfo("Group Data || GenerateDataPrint(Controller)");

                // Set Base Header Data
                var loBaseHeader = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY);
                _logger.LogInfo("Set Base Header || GenerateDataPrint(Controller)");
                BaseHeaderDTO loParam = new BaseHeaderDTO()
                {
                    CPRINT_CODE = pcCompanyID,
                    CPRINT_NAME = "HANDOVER INVITATION",
                    CUSER_ID = loBaseHeader.USER_ID,
                };

                PMT02120PrintReportDTO loGetLogo = GetBaseHeaderLogoCompany(new PMT02120PrintReportParameterDTO()
                {
                    CCOMPANY_ID = pcCompanyID
                });
                loParam.BLOGO_COMPANY = loGetLogo.OLOGO;
                loParam.CCOMPANY_NAME = loGetLogo.CCOMPANY_NAME;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loGetLogo.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);

                _logger.LogInfo("Set Parameter || GenerateDataPrint(Controller)");
                PMT02100InvitationResultDTO loData = new PMT02100InvitationResultDTO()
                {
                    Column = (PMT02100InvitationColumnDTO)loColumn,
                    Data = poParam
                };

                loRtn.BaseHeaderData = loParam;
                loRtn.ReportData = loData;
            }
            catch (Exception ex)
            {
                _logger.LogError(loException);
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GenerateDataPrint(Controller)");
            return loRtn;
        }

        public PMT02120PrintReportDTO GetBaseHeaderLogoCompany(PMT02120PrintReportParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            R_Exception loEx = new R_Exception();
            PMT02120PrintReportDTO loResult = null;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();


                lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as OLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as OLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMT02120PrintReportDTO>(loDataTable).FirstOrDefault();
                lcQuery = "EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _logger.LogDebug(string.Format("EXEC RSP_GS_GET_COMPANY_INFO '@CCOMPANY_ID'", loDbParam));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<PMT02120PrintReportDTO>(loDataTable).FirstOrDefault();

                loResult.CCOMPANY_NAME = loCompanyNameResult.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
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

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        //private List<PMT02100InvitationDTO> ConvertResultToFormatPrint(List<PMT02100InvitationDTO> poCollectionDataRaw)
        //{
        //    var loException = new R_Exception();
        //    string lcMethodName = nameof(ConvertResultToFormatPrint);
        //    using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        //    _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
        //    //List<PMT02100InvitationDTO> loReturn = poCollectionDataRaw;

        //    try
        //    {
        //        poCollectionDataRaw = poCollectionDataRaw
        //                .Select(item =>
        //                {
        //                    item.DDUE_DATE = ConvertStringToDateTimeFormat(item.CDUE_DATE);
        //                    item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE);
        //                    item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE);
        //                    item.DINV_PERIOD = ConvertStringToDateTimeFormat(item.CINV_PERIOD);
        //                    return item;
        //                })
        //                .ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //        _logger.LogError(loException);
        //    }
        //    loException.ThrowExceptionIfErrors();
        //    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        //    return poCollectionDataRaw;

        //}

        #endregion

        #region Storage

        //public string SaveReportFile(PMR01700ParamSaveStorageDTO poParameter, DbConnection poConnection, R_ConnectionAttribute poConnectionAttribute)
        //{
        //    R_Exception loEx = new R_Exception();
        //    string lcMethodName = nameof(SaveReportFile);
        //    using Activity activity = _activitySource.StartActivity(lcMethodName)!;
        //    _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
        //    string? loResult = null;
        //    DbConnection? loConn = null;
        //    R_ConnectionAttribute loConnAttr;
        //    R_SaveResult? loSaveResult = null;
        //    try
        //    {
        //        var loDb = new R_Db();
        //        loConn = poConnection;
        //        loConnAttr = poConnectionAttribute;
        //        _logger.LogInfo("koneksi loConnAttr di TransactionScope {0}", loConnAttr);
        //        _logger.LogInfo("Get storage type");
        //        BaseAOCParameterCompanyAndUserDTO loParameterGetStorageType = new BaseAOCParameterCompanyAndUserDTO()
        //        {
        //            CCOMPANY_ID = poParameter.CCOMPANY_ID,
        //            CUSER_ID = poParameter.CUSER_ID,
        //        };
        //        var loGetStorageType = GetStorageType(poParameter: loParameterGetStorageType, poConnection: poConnection);

        //        if (string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
        //        {
        //            R_EStorageType loStorageType = loGetStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;
        //            R_EProviderForCloudStorage loProvider = loGetStorageType.CSTORAGE_PROVIDER_ID!.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

        //            _logger.LogInfo("Add Or Create storage id to save cause storage id not exist:");

        //            // Add and create Storage ID
        //            R_AddParameter loAddParameter = new R_AddParameter()
        //            {
        //                StorageType = loStorageType,
        //                ProviderCloudStorage = loProvider,
        //                FileName = $"Report_{poParameter.CINVOICE_CODE}",
        //                FileExtension = poParameter.CFILE_EXTENSION,
        //                UploadData = poParameter.OFILE_REPORT,
        //                UserId = poParameter.CUSER_ID,
        //                BusinessKeyParameter = new R_BusinessKeyParameter()
        //                {
        //                    CCOMPANY_ID = poParameter.CCOMPANY_ID,
        //                    CDATA_TYPE = poParameter.CDATA_TYPE,
        //                    CKEY01 = poParameter.CPROPERTY_ID,
        //                    CKEY02 = poParameter.CDEPT_CODE,
        //                    CKEY03 = poParameter.CTRANS_CODE,
        //                    CKEY04 = poParameter.CINVOICE_CODE,
        //                }
        //            };
        //            _logger.LogInfo("Call R_StorageUtility.AddFile to storage table");
        //            _logger.LogDebug("Add Parameter value:", loAddParameter);
        //            loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn);
        //            poParameter.CSTORAGE_ID = loSaveResult!.StorageId;


        //            //For Data Save to Storage
        //            List<PMR01700TempTableUpdateStorageDTO> loDataSingleSaveStorage = new List<PMR01700TempTableUpdateStorageDTO>();

        //            loDataSingleSaveStorage.Add(new PMR01700TempTableUpdateStorageDTO()
        //            {
        //                CSTORAGE_ID = poParameter.CSTORAGE_ID,
        //                CINVOICE_CODE = poParameter.CINVOICE_CODE,
        //            });

        //            PMR01700UpdateDistributeorStorageIdDTO loParameterUpdateDistributeorStorageId = new PMR01700UpdateDistributeorStorageIdDTO();
        //            loParameterUpdateDistributeorStorageId.ODATA_STORAGE_SAVED = loDataSingleSaveStorage;
        //            loParameterUpdateDistributeorStorageId.LDISTRIBUTE = false;
        //            loParameterUpdateDistributeorStorageId.CCOMPANY_ID = poParameter.CCOMPANY_ID;
        //            loParameterUpdateDistributeorStorageId.CUSER_ID = poParameter.CUSER_ID;
        //            loParameterUpdateDistributeorStorageId.CPROPERTY_ID = poParameter.CPROPERTY_ID;

        //            UpdateDistributeorStorageId(loParameterUpdateDistributeorStorageId, loConn);
        //        }
        //        else if (!string.IsNullOrEmpty(poParameter.CSTORAGE_ID))
        //        {
        //            _logger.LogInfo("update with storage id to save cause storage id already exist:");
        //            R_UpdateParameter loUpdateParameter;

        //            loUpdateParameter = new R_UpdateParameter()
        //            {
        //                StorageId = poParameter.CSTORAGE_ID,
        //                UploadData = poParameter.OFILE_REPORT,
        //                UserId = poParameter.CUSER_ID,
        //                OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
        //                {
        //                    FileExtension = poParameter.CFILE_EXTENSION,
        //                    FileName = $"Report_{poParameter.CINVOICE_CODE}.{poParameter.CFILE_EXTENSION}"
        //                }
        //            };

        //            _logger.LogInfo("Call R_StorageUtility.UpdateFile to storage table");
        //            _logger.LogDebug("Add Parameter value:", loUpdateParameter);
        //            loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);
        //        }

        //        if (loSaveResult != null)
        //        {
        //            loResult = loSaveResult.StorageId;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //        _logger.LogError(loEx);
        //    }
        //    _logger!.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        //    loEx.ThrowExceptionIfErrors();
        //    return loResult!;
        //}

        //private PMR01700StorageTypeDTO GetStorageType(BaseAOCParameterCompanyAndUserDTO poParameter, DbConnection poConnection)
        //{
        //    string lcMethodName = nameof(GetStorageType);
        //    var loEx = new R_Exception();
        //    PMR01700StorageTypeDTO? loResult = null;
        //    var loDb = new R_Db();
        //    DbConnection? loConn = null;
        //    DbCommand? loCmd = null;

        //    try
        //    {
        //        _logger!.LogInfo("before exec RSP_GS_GET_STORAGE_TYPE", lcMethodName);
        //        loConn = poConnection;
        //        loCmd = loDb.GetCommand();
        //        R_ExternalException.R_SP_Init_Exception(loConn);

        //        var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
        //        loCmd.CommandText = lcQuery;
        //        loCmd.CommandType = CommandType.StoredProcedure;

        //        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
        //        loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParameter.CUSER_ID);

        //        //Debug Logs
        //        var loDbParam = loCmd.Parameters.Cast<DbParameter>()
        //            .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
        //        _logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter}", loDbParam);

        //        try
        //        {
        //            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
        //            loResult = R_Utility.R_ConvertTo<PMR01700StorageTypeDTO>(loDataTable).FirstOrDefault()!;
        //            _logger.LogInfo("after exec RSP_GS_GET_STORAGE_TYPE", lcMethodName);
        //        }
        //        catch (Exception ex)
        //        {
        //            loEx.Add(ex);
        //            _logger!.LogError(loEx);
        //        }
        //        loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //        _logger!.LogError(loEx);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loResult!;
        //}

        //public void UpdateDistributeorStorageId(PMR01700UpdateDistributeorStorageIdDTO poParameter, DbConnection poConnection)
        //{
        //    string? lcMethod = nameof(UpdateDistributeorStorageId);
        //    using Activity activity = _activitySource.StartActivity(lcMethod)!;
        //    _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
        //    R_Exception? loException = new R_Exception();
        //    List<PMT02100InvitationDTO> loReturn = new List<PMT02100InvitationDTO>();
        //    string? lcQuery;
        //    DbConnection? loConn = null;
        //    DbCommand? loCommand;
        //    R_Db? loDb;

        //    try
        //    {
        //        _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
        //        loDb = new();
        //        _logger.LogDebug("{@ObjectDb}", loDb);

        //        _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
        //        loCommand = loDb.GetCommand();
        //        _logger.LogDebug("{@ObjectDb}", loCommand);

        //        _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
        //        loConn = poConnection;
        //        _logger.LogDebug("{@ObjectDbConnection}", loConn);

        //        _logger.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
        //        R_ExternalException.R_SP_Init_Exception(loConn);

        //        //List<PMR01700TempTableUpdateStorageDTO> loTempData = new List<PMR01700TempTableUpdateStorageDTO>();
        //        //loTempData.Add(new PMR01700TempTableUpdateStorageDTO()
        //        //{
        //        //    CSTORAGE_ID = poParameter.CSTORAGE_ID,
        //        //    CINVOICE_CODE = poParameter.CINVOICE_CODE,
        //        //});

        //        lcQuery = "CREATE TABLE #TEMP_INVOICE_DISTRIBUTE " +
        //            "(CINVOICE_CODE VARCHAR(20) " +
        //            ", CSTORAGE_ID VARCHAR(50)" +
        //            ") ";
        //        _logger.LogDebug("CREATE TABLE #TEMP_INVOICE_DISTRIBUTE");

        //        loDb.SqlExecNonQuery(lcQuery, loConn, false);
        //        loDb.R_BulkInsert((SqlConnection)loConn, "#TEMP_INVOICE_DISTRIBUTE", poParameter.ODATA_STORAGE_SAVED);
        //        _logger.LogDebug("R_BulkInsert To TABLE #TEMP_INVOICE_DISTRIBUTE");


        //        _logger.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
        //        lcQuery = "RSP_PM_UPD_STORAGE_INVOICE";
        //        _logger.LogDebug("{@ObjectTextQuery}", lcQuery);

        //        _logger.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
        //        loCommand.CommandType = CommandType.StoredProcedure;
        //        loCommand.CommandText = lcQuery;
        //        _logger.LogDebug("{@ObjectDbCommand}", loCommand);

        //        _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
        //        loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
        //        loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
        //        loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
        //        loDb.R_AddCommandParameter(loCommand, "@LDISTRIBUTE", DbType.Boolean, 2, poParameter.LDISTRIBUTE);
        //        var loDbParam = loCommand.Parameters.Cast<DbParameter>()
        //            .Where(x => x != null && x.ParameterName.StartsWith("@"))
        //            .ToDictionary(x => x.ParameterName, x => x.Value);
        //        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

        //        try
        //        {
        //            _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
        //            //var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
        //            int liResult = loDb.SqlExecNonQuery(loConn, loCommand, false);

        //            //_loggerPMR01600.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
        //            //loReturn = R_Utility.R_ConvertTo<PMT02100InvitationDTO>(loDataTable).ToList();
        //            //_loggerPMR01600.LogDebug("{@ObjectReturn}", loReturn);
        //        }
        //        catch (Exception ex)
        //        {
        //            loException.Add(ex);
        //        }
        //        _logger.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
        //        loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    if (loException.Haserror)
        //        _logger.LogError("{@ErrorObject}", loException.Message);

        //    _logger.LogInfo(string.Format("End Method {0}", lcMethod));
        //    loException.ThrowExceptionIfErrors();

        //    //return loReturn;
        //}

        #endregion

        #region Email

        public PMT02100ResponseEmailDTO GetBodyEmailInvoiceDb(PMT02100ParameterRequestEmailDTO poParameter, DbConnection poConnection)
        {
            string? lcMethod = nameof(GetBodyEmailInvoiceDb);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMT02100ResponseEmailDTO loReturn = new PMT02100ResponseEmailDTO();
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = poConnection;
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _logger.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_HANDOVER_INVITE";
                _logger.LogDebug("{@ObjectTextQuery}", lcQuery);

                _logger.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@LSEND", DbType.Boolean, 8, false);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);

                    _logger.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
                    loReturn = R_Utility.R_ConvertTo<PMT02100ResponseEmailDTO>(loDataTable).FirstOrDefault() ?? new PMT02100ResponseEmailDTO();
                    _logger.LogDebug("{@ObjectReturn}", loReturn);


                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _logger.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public string SendEmail(List<R_EmailEngineBackObject> poDataAttachment, PMT02100ResponseEmailDTO EmailMessageDetails, DbConnection poConnection)
        {
            string lcMethodName = nameof(SendEmail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            R_EmailEngineBackCommandPar loEmailPar;
            List<R_EmailEngineBackObject> loEmailFiles;
            DbConnection? loConn = null;
            string lcRtn = "";
            try
            {
                loEmailFiles = new List<R_EmailEngineBackObject>();
                loEmailFiles = poDataAttachment;

                loEmailPar = new R_EmailEngineBackCommandPar()
                {
                    COMPANY_ID = EmailMessageDetails.CCOMPANY_ID,
                    USER_ID = EmailMessageDetails.CUSER_ID,
                    PROGRAM_ID = "PMT02100",
                    //PROGRAM_ID = !string.IsNullOrEmpty(EmailMessageDetails.CPROGRAM_ID) ? EmailMessageDetails.CPROGRAM_ID : "PMR01700",
                    Message = new R_EmailEngineMessage()
                    {
                        EMAIL_FROM = EmailMessageDetails.CEMAIL_FROM!,
                        EMAIL_BODY = EmailMessageDetails.CEMAIL_BODY!,
                        EMAIL_SUBJECT = EmailMessageDetails.CEMAIL_SUBJECT!,
                        EMAIL_TO = EmailMessageDetails.CTENANT_EMAIL!,
                        EMAIL_CC = "", //"ericsonwen123@gmail.com",//"hafizmursiddd@gmail.com", // hafiz.codeid@realta.net
                        EMAIL_BCC = "",
                        FLAG_HTML = true
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


        public void ConfirmScheduleProcess(PMT02100InvitationParameterDTO poParameter)//, DbConnection poConn)
        {
            using Activity activity = _activitySource.StartActivity("ConfirmScheduleProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_HANDOVER_CONFIRM_SCHEDULE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSCHEDULED_HO_DATE", DbType.String, 50, poParameter.CSCHEDULED_HO_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CSCHEDULED_HO_TIME", DbType.String, 50, poParameter.CSCHEDULED_HO_TIME);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                //var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                //.Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                //_logger.LogDebug("EXEC RSP_PM_HANDOVER_PROCESS {@poParameter}", loDbParam);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_HANDOVER_CONFIRM_SCHEDULE {@Parameters} || ConfirmScheduleProcess(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
            loException.ThrowExceptionIfErrors();
        }

        public void ReinviteProcess(PMT02100InvitationParameterDTO poParameter)//, DbConnection poConn)
        {
            using Activity activity = _activitySource.StartActivity("ReinviteProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_HANDOVER_REINVITE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                ////Debug Logs
                //var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                //.Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                //_logger.LogDebug("EXEC RSP_PM_Reinvite_PROCESS {@poParameter}", loDbParam);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_HANDOVER_REINVITE {@Parameters} || ReinviteProcess(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
            loException.ThrowExceptionIfErrors();
        }

    }
}