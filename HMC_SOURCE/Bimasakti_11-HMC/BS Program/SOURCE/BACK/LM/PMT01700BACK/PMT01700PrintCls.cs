using PMT01700COMMON.DTO.Utilities.Print;
using PMT01700COMMON.Logs;
using PMT01700CommonReport;
using R_BackEnd;
using R_Common;
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
using System.Text;
using System.Threading.Tasks;

namespace PMT01700BACK
{
    public class PMT01700PrintCls
    {

        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;
        //   private static readonly HashSet<string> _transCodeEventSet = new(new[] { "802043", "802063", "802033" });

        public PMT01700PrintCls()
        {
            _logger = LoggerPMT01700.R_GetInstanceLogger(); ;
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        #region Print Function

        public PMTDataReportDTO GetDataPrintDb(ParameterPrintDTO poParameter, DbConnection poConnection)
        {
            string? lcMethod = nameof(GetDataPrintDb);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMTDataReportDTO loReturn = new PMTDataReportDTO();
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

                lcQuery = "RSP_PM_PRINT_AGREEMENT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);

                    _logger.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
                    loReturn = R_Utility.R_ConvertTo<PMTDataReportDTO>(loDataTable).ToList().FirstOrDefault()!;
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

        public List<PMTDataChargesReportDTO> GetDataDataChargesDb(ParameterGetAgreementChargeListDTO poParameter, DbConnection poConnection)
        {
            string? lcMethod = nameof(GetDataDataChargesDb);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            List<PMTDataChargesReportDTO> loReturn = new List<PMTDataChargesReportDTO>();
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

                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_ITEMS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_SEQ_NO", DbType.String, 3, poParameter.CCHARGES_SEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);


                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);

                    _logger.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
                    loReturn = R_Utility.R_ConvertTo<PMTDataChargesReportDTO>(loDataTable).ToList();
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

        public PMTResultDataReportDTO GenerateDataPrint(ParameterPrintDTO poParam)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            var loException = new R_Exception();
            PMTResultDataReportDTO? loReturn = null;
            //PMR01600Cls? loCls = null;
            //BaseAOCParameterCompanyAndUserDTO? loParameterInternal;
            DbConnection? loConn = null;
            //DbCommand? loCommand;
            R_Db? loDb = new();
            CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {

                _logger.LogInfo("Init Cls");
                //loCls = new PMR01600Cls();

                _logger.LogInfo("Init Object return");
                loReturn = new PMTResultDataReportDTO();

                _logger.LogInfo("Create Object Print PMTPrintWorld");

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethodName));
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo("Get Data Report");
                PMTDataReportDTO DataPrint = GetDataPrintDb(poParameter: poParam, poConnection: loConn);
                _logger.LogInfo("Get Label");
                var loLabel = AssignValuesWithMessages(typeof(PMT01700BackResources.Resources_PMT01700_Class), loCultureInfo, new PMTLabelReportDTO());

                //_logger.LogInfo("Get Label Data");
                //var loLabelData = AssignValuesWithMessages(typeof(Resource_PMR01600_Class), loCultureInfo, new PMR01600LabelDataDTO());


                //_logger.LogInfo("Generate Header Data For Print");
                loReturn = new PMTResultDataReportDTO();

                if (!string.IsNullOrEmpty(poParam.CTRANS_CODE))
                {
                    var DataCharges = GetDataDataChargesDb(new ParameterGetAgreementChargeListDTO()
                    {
                        CCOMPANY_ID = poParam.CCOMPANY_ID,
                        CPROPERTY_ID = poParam.CPROPERTY_ID,
                        CDEPT_CODE = poParam.CDEPT_CODE,
                        CTRANS_CODE = poParam.CTRANS_CODE,
                        CREF_NO = poParam.CREF_NO,
                        CCHARGES_SEQ_NO = "", //INI TANYAKAN NANTI
                        CUSER_ID = poParam.CUSER_ID
                    }, loConn);

                    loReturn.DataCharges = new List<PMTDataChargesReportDTO>(DataCharges);
                }
                else
                {
                    loReturn.DataCharges = Enumerable.Empty<PMTDataChargesReportDTO>().ToList();
                }

                #region Try to get Data Signature

                ParamaterGetReportTemplateListDTO loParameterGetDigitalSignReport = new ParamaterGetReportTemplateListDTO()
                {
                    CCOMPANY_ID = poParam.CCOMPANY_ID,
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CPROGRAM_ID = poParam.CPROGRAM_ID,
                    CTEMPLATE_ID = poParam.CTEMPLATE_ID
                };

                loReturn.DigitalSign = new PMTDigitalSignDTO();

                ReportSignDTO loDataDigitalSign = GetDigitalSignReport(loParameterGetDigitalSignReport);

                if (loDataDigitalSign != null)
                {
                    loReturn.DigitalSign = ConvertDigitalSignReportFromDataDBtoDataReport<ReportSignDTO, PMTDigitalSignDTO>(loDataDigitalSign);
                }

                #endregion
                loReturn.Title = poParam.CTITLE ?? "TITlE REPORT";
                loReturn.LabelReport = new PMTLabelReportDTO();
                loReturn.LabelReport = loLabel as PMTLabelReportDTO;
                loReturn.Data = new PMTDataReportDTO();
                var loDataPrint = DataPrint != null ? DataPrint : new PMTDataReportDTO();
                loReturn.Data = loDataPrint;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex, "Error in GenerateDataPrint");
            }
            _logger.LogInfo("END Method GenerateDataPrint on Controller");
            loException.ThrowExceptionIfErrors();

            return loReturn!;
        }
        public ReportSignDTO GetDigitalSignReport(ParamaterGetReportTemplateListDTO poParameter)
        {

            string? lcMethod = nameof(GetDigitalSignReport);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            ReportSignDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));

                lcQuery = "RSP_GET_REPORT_TEMPLATE_LIST";
                _logger.LogDebug("{@ObjectQuery} ", lcQuery);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 30, poParameter.CPROGRAM_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTEMPLATE_ID ", DbType.String, 30, poParameter.CTEMPLATE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _logger.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<ReportSignDTO>(loDataTable).FirstOrDefault();
                _logger.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }


        #endregion

        private R ConvertDigitalSignReportFromDataDBtoDataReport<F, R>(F poParameter) where R : new()
        {
            string lcMethod = nameof(ConvertDigitalSignReportFromDataDBtoDataReport);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethod));
            if (poParameter == null) return new R();

            R_Exception loException = new R_Exception();
            R_Db loDb;
            DbCommand loCommand;
            DbConnection? loConn = null;
            R loResult = new R();
            Type typeF = typeof(F);
            Type typeR = typeof(R);

            try
            {
                _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                _logger.LogDebug("{@ObjectDbConnection}", loConn);


                for (int i = 1; i <= 6; i++)
                {
                    string lcStoragePropertyName = $"CSIGN_STORAGE_ID0{i}";
                    string loImagePropertyName = $"OSIGN_IMAGE0{i}";

                    string[] loPropertyNames =
                    {
                        $"CSIGN_NAME0{i}",
                        $"CSIGN_POSITION0{i}",
                        lcStoragePropertyName
                    };
                    foreach (string lcPropertyName in loPropertyNames)
                    {
                        var loSourceProperty = typeF.GetProperty(lcPropertyName);
                        var loTargetProperty = typeR.GetProperty(lcPropertyName);

                        if (loSourceProperty == null) continue; // Skip jika properti tidak ditemukan

                        object? loValue = loSourceProperty.GetValue(poParameter);

                        // Jika bukan CSIGN_STORAGE_ID0{i}, set nilai langsung ke target
                        if (lcPropertyName != lcStoragePropertyName)
                        {
                            if (loTargetProperty != null && loTargetProperty.CanWrite)
                            {
                                loTargetProperty.SetValue(loResult, loValue);
                            }
                        }
                        else
                        {
                            // Jika CSIGN_STORAGE_ID0{i}, gunakan untuk generate OSIGN_IMAGE0{i}
                            var loImageProperty = typeR.GetProperty(loImagePropertyName);
                            if (loImageProperty != null && loImageProperty.CanWrite && loValue is string lcStorageId && !string.IsNullOrEmpty(lcStorageId))
                            {
                                var loReadParameter = new R_ReadParameter()
                                {
                                    StorageId = lcStorageId
                                };

                                var loReadResult = R_StorageUtility.ReadFile(loReadParameter, poConnection: loConn);
                                loImageProperty.SetValue(loResult, loReadResult.Data);
                            }
                            else
                            {
                                loImageProperty.SetValue(loResult, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        #region Utilities

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
