using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using FAT00800Back.DTOs;
using FAT00800BackResources;
using FAT00800Common.DTOs;

namespace FAT00800Back
{
    /// <summary>
    /// Business logic class for FAT00800 - Fixed Asset Transaction operations
    /// Handles all business logic operations for Fixed Asset Transaction
    /// </summary>
    public class FAT00800EntryCls : R_BusinessObjectAsync<FAT00800DTO>
    {
        private readonly FAT00800BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00800 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00800EntryCls()
        {
            _logger = LoggerFAT00800.R_GetInstanceLogger();
            _activitySource = FAT00800Activity.R_GetInstanceActivitySource();
        }

        /// <summary>
        /// Helper method to get error messages from resources
        /// </summary>
        /// <param name="pcErrorId">Error ID from resource file</param>
        /// <returns>R_Error object</returns>
        private R_Error GetError(string pcErrorId)
        {
            try
            {
                return R_Utility.R_GetError(typeof(Resources_Dummy_Class), pcErrorId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region CRUD

        /// <summary>
        /// Delete transaction record
        /// </summary>
        /// <param name="poEntity">Entity with key fields</param>
        protected override async Task R_DeletingAsync(FAT00800DTO poEntity)
        {
            string lcMethod = nameof(R_DeletingAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " EXEC RSP_FAT_DELETE @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 6, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 30, poEntity.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        /// <summary>
        /// Display transaction record with asset details
        /// </summary>
        /// <param name="poEntity">Entity with key fields</param>
        /// <returns>Complete entity with transaction and asset details</returns>
        protected override async Task<FAT00800DTO> R_DisplayAsync(FAT00800DTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            FAT00800DTO loRtn = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "RSP_FAT00800_GET_TRANS_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poEntity.CLANG_ID ?? string.Empty);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName!, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT00800DTO>(loRtnDataTable);
                loRtn = loRtnList.FirstOrDefault() ?? new FAT00800DTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loRtn;
        }

        /// <summary>
        /// Save transaction record with complex Add/Edit logic, validations, and multiple INSERT/UPDATE statements
        /// </summary>
        /// <param name="poNewEntity">Entity to save</param>
        /// <param name="poCRUDMode">CRUD mode (Add or Edit)</param>
        protected override async Task R_SavingAsync(FAT00800DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethod = nameof(R_SavingAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                string lcAction = poCRUDMode == eCRUDMode.AddMode ? "NEW" : "EDIT";

                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "RSP_FAT00800_SAVE_TRANS";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQ_NO", DbType.String, 6, poNewEntity.CASSET_TRANS_SEQ_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 20, poNewEntity.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 20, poNewEntity.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 20, poNewEntity.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 20, poNewEntity.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NSALES_AMOUNT", DbType.Decimal, 19, poNewEntity.NSALES_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NLBOOK_VALUE", DbType.Decimal, 19, poNewEntity.NLBOOK_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@NBBOOK_VALUE", DbType.Decimal, 19, poNewEntity.NBBOOK_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@CEXPENSE_ALLOC_ID", DbType.String, 20, poNewEntity.CEXPENSE_ALLOC_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 200, poNewEntity.CTRANS_DESC ?? string.Empty);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName!, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT00800DTO>(loRtnDataTable);
                var loRtnRow = loRtnList.FirstOrDefault();
                if (loRtnRow != null && !string.IsNullOrEmpty(loRtnRow.CREC_ID))
                {
                    poNewEntity.CREC_ID = loRtnRow.CREC_ID;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            if (loEx.Haserror)
            {
                loEx.ThrowExceptionIfErrors();
            }

            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        #endregion

        #region init

        /// <summary>
        /// Get company information via stored procedure RSP_GS_GET_COMPANY_INFO
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID</param>
        /// <returns>Result DTO with company information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>> FAT00800GetCompanyInfoAsync(FAT00800GetCompanyInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00800GetCompanyInfoAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>
            {
                Data = new FAT00800GetCompanyInfoResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetCompanyInfoResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get system parameters via stored procedure RSP_FA_GET_SYSTEM_PARAM
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and language ID</param>
        /// <returns>Result DTO with system parameters</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>> FAT00800GetGetSystemParamAsync(FAT00800GetGetSystemParamParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00800GetGetSystemParamAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>
            {
                Data = new FAT00800GetGetSystemParamResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FA_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParameter.CLANGUAGE_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetGetSystemParamResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get period detail information via stored procedure RSP_GS_GET_PERIOD_DT_INFO
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, year, and period number</param>
        /// <returns>Result DTO with period detail information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>> FAT00800GetPeriodeDtInfoAsync(FAT00800GetPeriodeDtInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00800GetPeriodeDtInfoAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>
            {
                Data = new FAT00800GetPeriodeDtInfoResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 2, poParameter.CPERIOD_NO);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetPeriodeDtInfoResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get currency list via stored procedure RSP_GS_GET_CURRENCY_LIST (streaming).
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, user ID, and language ID</param>
        /// <returns>List of currency code and name</returns>
        public async Task<List<FAT00800GetCurrencyListResultDTO>> GetCurrencyListAsync(FAT00800GetCurrencyListParameterDTO poParameter)
        {
            string lcMethod = nameof(GetCurrencyListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00800GetCurrencyListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANG_ID ?? string.Empty);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName!, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", lcQuery, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetCurrencyListResultDTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.ToList();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get department lookup list via stored procedure RSP_GS_GET_DEPT_LOOKUP_LIST
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, user ID, and program ID</param>
        /// <returns>List of department lookup information</returns>
        public async Task<List<FAT00800GetDeptLookupListResultDTO>> FAT00800GetDeptLookupListAsync(FAT00800GetDeptLookupListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00800GetDeptLookupListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00800GetDeptLookupListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_DEPT_LOOKUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, 20, poParameter.CPROGRAM_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetDeptLookupListResultDTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.ToList();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get transaction code information via stored procedure RSP_GS_GET_TRANS_CODE_INFO
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and transaction code</param>
        /// <returns>Result DTO with transaction code information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>> FAT00800GetTransCodeInfoAsync(FAT00800GetTransCodeInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00800GetTransCodeInfoAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>
            {
                Data = new FAT00800GetTransCodeInfoResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetTransCodeInfoResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get year range via stored procedure RSP_GS_GET_PERIOD_YEAR_RANGE
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, year, and mode</param>
        /// <returns>Result DTO with minimum and maximum year</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>> FAT00800GetYearRangeAsync(FAT00800GetYearRangeParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00800GetYearRangeAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>
            {
                Data = new FAT00800GetYearRangeResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParameter.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, poParameter.CMODE);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetYearRangeResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        #endregion

       
    }
}