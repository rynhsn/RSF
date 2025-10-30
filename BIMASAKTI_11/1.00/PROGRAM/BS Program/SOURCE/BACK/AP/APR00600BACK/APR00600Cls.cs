using R_BackEnd;
using R_Common;
using R_Storage;
using R_StorageCommon;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APR00600COMMON;
using APR00600COMMON.DTOs;
using APR00600COMMON.DTOs.Print;
using APR00600COMMON.Params;

namespace APR00600BACK
{
    public class APR00600Cls
    {
        private LoggerAPR00600 _logger;
        private readonly ActivitySource _activitySource;

        public APR00600Cls()
        {
            _logger = LoggerAPR00600.R_GetInstanceLogger();
            _activitySource = APR00600Activity.R_GetInstanceActivitySource();
        }

        public async Task<List<APR00600PropertyDTO>> GetPropertyList(APR00600ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<APR00600PropertyDTO> loReturn = new List<APR00600PropertyDTO>();
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

                loReturn = R_Utility.R_ConvertTo<APR00600PropertyDTO>(DataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<APR00600GetCompanyInfoDTO> GetCompanyInfo(APR00600ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<APR00600GetCompanyInfoDTO> loReturnTemp = new List<APR00600GetCompanyInfoDTO>();
            APR00600GetCompanyInfoDTO loReturn = new APR00600GetCompanyInfoDTO();
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

                loReturnTemp = R_Utility.R_ConvertTo<APR00600GetCompanyInfoDTO>(DataTable).ToList();
                loReturn = loReturnTemp.FirstOrDefault() ?? new APR00600GetCompanyInfoDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<APR00600GetPeriodeYearRangeDTO> GetPeriodeYearRange(APR00600ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<APR00600GetPeriodeYearRangeDTO> loReturnTemp = new List<APR00600GetPeriodeYearRangeDTO>();
            APR00600GetPeriodeYearRangeDTO loReturn = new APR00600GetPeriodeYearRangeDTO();
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

                loReturnTemp = R_Utility.R_ConvertTo<APR00600GetPeriodeYearRangeDTO>(DataTable).ToList();
                loReturn = loReturnTemp.FirstOrDefault() ?? new APR00600GetPeriodeYearRangeDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<APR00600GetSystemParamDTO> GetSystemParam(APR00600ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<APR00600GetSystemParamDTO> loReturnTemp = new List<APR00600GetSystemParamDTO>();
            APR00600GetSystemParamDTO loReturn = new APR00600GetSystemParamDTO();
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

                loReturnTemp = R_Utility.R_ConvertTo<APR00600GetSystemParamDTO>(DataTable).ToList();
                loReturn = loReturnTemp.FirstOrDefault() ?? new APR00600GetSystemParamDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<List<APR00600GetPeriodDtListDTO>> GetPeriodDtList(APR00600ParamDbDTO poParams)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            R_Exception loEx = new();
            List<APR00600GetPeriodDtListDTO> loReturn = new List<APR00600GetPeriodDtListDTO>();
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

                loReturn = R_Utility.R_ConvertTo<APR00600GetPeriodDtListDTO>(DataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public List<APR00600DataResultDTO> GetReportData(APR00600ReportParamDTO poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetReportData));
            R_Exception loEx = new();
            List<APR00600DataResultDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_APR00600_GET_REPORT ";
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
                loRtn = R_Utility.R_ConvertTo<APR00600DataResultDTO>(loDataTable).ToList();
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

        public APR00600PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(APR00600ReportParamDTO poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
            var loEx = new R_Exception();
            APR00600PrintBaseHeaderLogoDTO loResult = null;
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
                loResult = R_Utility.R_ConvertTo<APR00600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

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
                var loCompanyNameResult = R_Utility.R_ConvertTo<APR00600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

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
