using PMR03300COMMON;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMR03300COMMON.DTOs;
using R_Storage;
using R_StorageCommon;
using PMR03300COMMON.DTOs.Print;
using PMR03300COMMON.Params;
using System.Windows.Input;

namespace PMR03300BACK
{
    public class PMR03300Cls
    {
        private LoggerPMR03300 _logger;
        private readonly ActivitySource _activitySource;

        public PMR03300Cls()
        {
            _logger = LoggerPMR03300.R_GetInstanceLogger();
            _activitySource = PMR03300Activity.R_GetInstanceActivitySource();
        }

        public async Task<List<PMR03300PropertyDTO>> GetPropertyList(PMR03300ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<PMR03300PropertyDTO> loReturn= new List<PMR03300PropertyDTO>();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CUSER_ID"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<PMR03300PropertyDTO>(DataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<PMR03300GetCompanyInfoDTO> GetCompanyInfo(PMR03300ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<PMR03300GetCompanyInfoDTO> loReturnTemp = new List<PMR03300GetCompanyInfoDTO>();
            PMR03300GetCompanyInfoDTO loReturn = new PMR03300GetCompanyInfoDTO();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" 
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loReturnTemp = R_Utility.R_ConvertTo<PMR03300GetCompanyInfoDTO>(DataTable).ToList();
                loReturn = loReturnTemp.FirstOrDefault() ?? new PMR03300GetCompanyInfoDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<PMR03300GetPeriodeYearRangeDTO> GetPeriodeYearRange(PMR03300ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<PMR03300GetPeriodeYearRangeDTO> loReturnTemp = new List<PMR03300GetPeriodeYearRangeDTO>();
            PMR03300GetPeriodeYearRangeDTO loReturn = new PMR03300GetPeriodeYearRangeDTO();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParams.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, poParams.CMODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CYEAR" or
                            "@CMODE"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loReturnTemp = R_Utility.R_ConvertTo<PMR03300GetPeriodeYearRangeDTO>(DataTable).ToList();
                loReturn = loReturnTemp.FirstOrDefault() ?? new PMR03300GetPeriodeYearRangeDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<PMR03300GetSystemParamDTO> GetSystemParam(PMR03300ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<PMR03300GetSystemParamDTO> loReturnTemp = new List<PMR03300GetSystemParamDTO>();
            PMR03300GetSystemParamDTO loReturn = new PMR03300GetSystemParamDTO();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_GET_SYSTEM_PARAM";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParams.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CLANGUAGE_ID" or
                            "@CPROPERTY_ID"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loReturnTemp = R_Utility.R_ConvertTo<PMR03300GetSystemParamDTO>(DataTable).ToList();
                loReturn = loReturnTemp.FirstOrDefault() ?? new PMR03300GetSystemParamDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<List<PMR03300GetPeriodDtListDTO>> GetPeriodDtList(PMR03300ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<PMR03300GetPeriodDtListDTO> loReturn = new List<PMR03300GetPeriodDtListDTO>();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_PERIOD_DT_LIST ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 8, poParams.CYEAR);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CYEAR"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<PMR03300GetPeriodDtListDTO>(DataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public List<PMR03300DataResultDTO> GetReportData(PMR03300ReportParamDTO poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetReportData));
            R_Exception loEx = new();
            List<PMR03300DataResultDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PMR03300_GET_REPORT ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFR_PERIOD", DbType.String, 6, poParam.CFR_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParam.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParam.CCURRENCY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CFILTER_BY", DbType.String, 30, poParam.CFILTER_BY);
                loDb.R_AddCommandParameter(loCmd, "@CFR_CODE", DbType.String, 30, poParam.CFR_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CODE", DbType.String, 30, poParam.CTO_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LSUPPRESS", DbType.Boolean, 1, poParam.LSUPPRESS);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParam.CLANG_ID);

               _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                int rowCount = loDataTable.Rows.Count;
                loRtn = R_Utility.R_ConvertTo<PMR03300DataResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public PMR03300PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(PMR03300ReportParamDTO poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
            var loEx = new R_Exception();
            PMR03300PrintBaseHeaderLogoDTO loResult = null;
            R_Db loDb = null; // Database object    
            DbConnection loConn = null;
            DbCommand loCmd = null;


            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();

                //var lcQuery = $"SELECT dbo.RFN_GET_COMPANY_LOGO('{pcCompanyId}') as BLOGO";
                //loCmd.CommandText = lcQuery;
                //loCmd.CommandType = CommandType.Text;

                //_logger.LogDebug("{pcQuery}", lcQuery);

                //var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                //loResult = R_Utility.R_ConvertTo<PMR02600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();


                var lcQuery = "RSP_GS_GET_PROPERTY_DETAIL";
                loCmd = loDb.GetCommand();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);
                _logger.LogDebug("EXEC {lcQuery} {@Parameters}", lcQuery, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMR03300PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

                if (string.IsNullOrEmpty(loResult.CSTORAGE_ID) == false)
                {
                    var loReadParameter = new R_ReadParameter()
                    {
                        StorageId = loResult.CSTORAGE_ID
                    };

                    var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                    loResult.BLOGO = loReadResult.Data;
                }

                //ambil company name
                lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{poParam.CCOMPANY_ID}'"; // Query to get company name
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _logger.LogDebug(lcQuery);
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<PMR03300PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

                loResult!.CCOMPANY_NAME = loCompanyNameResult?.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;

            }
            catch (Exception ex)
            {
                loEx.Add(ex); // Add the exception to the exception object
                _logger.LogError(loEx); // Log the exception
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
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


    }
}
