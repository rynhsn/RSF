using PMR00100Common.DTOs;
using PMR00100Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;

namespace PMR00100Back
{
    public class PMR00100Cls : R_BusinessObjectAsync<PMR00100DTO>
    {
        private LoggerPMR00100 _logger;
        private readonly ActivitySource _activitySource;
        public PMR00100Cls()
        {
            _logger = LoggerPMR00100.R_GetInstanceLogger();
            _activitySource = PMR00100Activity.R_GetInstanceActivitySource();
        }

        #region CRUD
        protected override async Task<PMR00100DTO> R_DisplayAsync(PMR00100DTO poEntity)
        {
            throw new NotImplementedException();
        }
        protected override Task R_SavingAsync(PMR00100DTO poNewEntity,eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }
        protected override Task R_DeletingAsync(PMR00100DTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
        public async Task<List<PropertyDTO>> GetPropertyList(PMR00100ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            R_Exception loEx = new R_Exception();
            _logger.LogInfo("Start GetPropertyList PMR00100");
            List<PropertyDTO> loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID",DbType.String,20,poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd,"@CUSER_ID",DbType.String,20,poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName,x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}",loCmd.CommandText,loDbParam);


                var loDataTable = await loDb.SqlExecQueryAsync(loConn,loCmd,true);
                loResult = R_Utility.R_ConvertTo<PropertyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _logger.LogInfo("End GetPropertyList PMR00100");
            return loResult;
        }
        public async Task<PeriodYearRangeDTO> GetPeriodYear(PMR00100ParamDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            _logger.LogInfo("Start GetPeriodYear PMR00100");
            PeriodYearRangeDTO loResult = null;
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();
                var lcQuery = $"RSP_GS_GET_PERIOD_YEAR_RANGE '{poParameter.CCOMPANY_ID}','',''";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName,x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}",loCmd.CommandText,loDbParam);

                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn,loCmd,true);
                loResult = R_Utility.R_ConvertTo<PeriodYearRangeDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End GetPeriodYear PMR00100");
            return loResult;
        }
        public async Task<List<PeriodDT_DTO>> GetPeriodDTList(PMR00100ParamDTO poParameter,PMR00100ParamDTO poData)
        {
            var loEx = new R_Exception();
            _logger.LogInfo("Start GetPeriodDTList PMR00100");
            List<PeriodDT_DTO> loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GS_GET_PERIOD_DT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID",DbType.String,8,poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd,"@CYEAR",DbType.String,4,poData.CYEAR);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@","")).Name).Select(x => x.Value);
                _logger.LogDebug("EXEC RSP_GS_GET_DEPT_LOOKUP_LIST {@poParameter}",loDbParam);


                var loDataTable = await loDb.SqlExecQueryAsync(loConn,loCmd,true);
                loResult = R_Utility.R_ConvertTo<PeriodDT_DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _logger.LogInfo("End GetPeriodDTList PMR00100");
            return loResult;
        }
        public async Task<List<LOOStatusDTO>> GetLOOStatus(PMR00100ParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            _logger.LogInfo("Start GetLOOStatus PMR00100");
            List<LOOStatusDTO> loRtn = new List<LOOStatusDTO>();
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_AGREEMENT_STATUS', '', '{poParameter.CLANG_ID}')";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName,x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}",loCmd.CommandText,loDbParam);


                var loDataTable = await loDb.SqlExecQueryAsync(loConn,loCmd,true);
                loRtn = R_Utility.R_ConvertTo<LOOStatusDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End GetLOOStatus PMR00100");
            return loRtn;
        }

        #region Report
        public List<PMR00100DTO> GetPrintLOOList(PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintList");
            var loEx = new R_Exception();
            _logger.LogInfo("Start GetPrintList PMR00100");
            List<PMR00100DTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PMR00100_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID",DbType.String,8,poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd,"@CPROPERTY_ID",DbType.String,20,poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd,"@CFROM_DEPT_CODE",DbType.String,20,poParameter.CFROM_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd,"@CTO_DEPT_CODE",DbType.String,20,poParameter.CTO_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd,"@CFROM_SALESMAN_ID",DbType.String,8,poParameter.CFROM_SALESMAN_ID);
                loDb.R_AddCommandParameter(loCmd,"@CTO_SALESMAN_ID",DbType.String,8,poParameter.CTO_SALESMAN_ID);
                loDb.R_AddCommandParameter(loCmd,"@CFROM_PERIOD",DbType.String,6,poParameter.CFROM_PERIOD);
                loDb.R_AddCommandParameter(loCmd,"@CTO_PERIOD",DbType.String,6,poParameter.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd,"@CREPORT_TYPE",DbType.String,2,poParameter.CREPORT_TYPE);
                loDb.R_AddCommandParameter(loCmd,"@CLANG_ID",DbType.String,2,poParameter.CLANG_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName,x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}",loCmd.CommandText,loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn,loCmd,true);
                loResult = R_Utility.R_ConvertTo<PMR00100DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _logger.LogInfo("End GetPrintList PMR00100");
            return loResult;
        }
        public PMR00100DTO GetBaseHeaderLogoCompany(PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PMR00100DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();

                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID",DbType.String,15,poParameter.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO",loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn,loCmd,false);
                loResult = R_Utility.R_ConvertTo<PMR00100DTO>(loDataTable).FirstOrDefault();
                loCmd.Parameters.Clear();

                lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd,"@CCOMPANY_ID",DbType.String,15,poParameter.CCOMPANY_ID);

                //Debug Logs
                _logger.LogDebug(string.Format("RSP_GS_GET_COMPANY_INFO",loDbParam));
                loDataTable = loDb.SqlExecQuery(loConn,loCmd,false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<PMR00100DTO>(loDataTable).FirstOrDefault();

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

        #endregion

    }
}
