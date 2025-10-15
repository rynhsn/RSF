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

        public async Task<List<PMR03300PropertyDTO>> GetPropertyList(PMR03300PropertyParamDTO poParams)
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

        public async Task<PMR03300GetCompanyInfoDTO> GetCompanyInfo(PMR03300GetCompanyInfoParamDTO poParams)
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

        public async Task<PMR03300GetPeriodeYearRangeDTO> GetPeriodeYearRange(PMR03300GetPeriodeYearRangeParamDTO poParams)
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

        public async Task<PMR03300GetSystemParamDTO> GetSystemParam(PMR03300GetSystemParamParamDTO poParams)
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

        public async Task<List<PMR03300GetPeriodDtListDTO>> GetPeriodDtList(PMR03300GetPeriodDtListParamDTO poParams)
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

                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
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


    }
}
