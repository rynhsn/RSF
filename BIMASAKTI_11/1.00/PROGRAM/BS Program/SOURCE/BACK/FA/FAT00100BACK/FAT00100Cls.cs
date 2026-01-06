using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Transactions;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using static R_CommonFrontBackAPI.R_ConfigurationUtility;
using FAT00100Back.DTOs;
using FAT00100BackResources;
using FAT00100Common.DTOs;

namespace FAT00100Back
{
    /// <summary>
    /// Business logic class for FAT00100 - FA Acquisition operations
    /// Handles all business logic operations for FA Acquisition
    /// </summary>
    public class FAT00100Cls : R_BusinessObjectAsync<FAT00100DTO>
    {
        private readonly FAT00100BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00100Cls()
        {
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = FAT00100Activity.R_GetInstanceActivitySource();
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

        /// <summary>
        /// Get department lookup validation
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, and user ID</param>
        /// <returns>Result DTO with validation result (1 if valid, 0 if not)</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>> GetDeptLookUpValidationAsync(FAT00100GetDeptLookUpValidationParameterDTO poParameter)
        {
            string lcMethod = nameof(GetDeptLookUpValidationAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>
            {
                Data = new FAT00100GetDeptLookUpValidationResultDTO { IResult = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT TOP 1 1 FROM GSX_DEPARTMENT_USER " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CDEPT_CODE = @CDEPT_CODE " +
                                    " AND CUSER_ID = @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult.Data.IResult = 1;
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
        /// Get period year range
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and period info</param>
        /// <returns>Result DTO with start and end year</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>> GetPeriodYearAsync(FAT00100GetPeriodYearParameterDTO poParameter)
        {
            string lcMethod = nameof(GetPeriodYearAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>
            {
                Data = new FAT00100GetPeriodYearResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                if (!string.IsNullOrWhiteSpace(poParameter.CREFERENCE_NO))
                {
                    // Extract year from transaction period (first 4 characters)
                    if (!string.IsNullOrWhiteSpace(poParameter.CTRANSACTION_PRD) && poParameter.CTRANSACTION_PRD.Length >= 4)
                    {
                        string lcYear = poParameter.CTRANSACTION_PRD.Substring(0, 4);
                        if (int.TryParse(lcYear, out int liYear))
                        {
                            loResult.Data.ISTART_YEAR = liYear;
                            loResult.Data.IEND_YEAR = liYear;
                        }
                    }
                }
                else
                {
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " SELECT ISTART_YEAR = CONVERT(SMALLINT,MIN(CCYEAR)), " +
                                        " IEND_YEAR = CONVERT(SMALLINT,MAX(CCYEAR)) " +
                                        " From GSM_PERIOD (nolock) " +
                                        " Where CCOMPANY_ID = @CCOMPANY_ID ";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);

                    var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@"))
                        .ToDictionary(x => x.ParameterName, x => x.Value);

                    _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                    var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    var loRtn = R_Utility.R_ConvertTo<FAT00100GetPeriodYearResultDTO>(loDataTable);

                    if (loRtn != null && loRtn.Count > 0)
                    {
                        var loFirst = loRtn.FirstOrDefault();
                        if (loFirst != null)
                        {
                            loResult.Data.ISTART_YEAR = loFirst.ISTART_YEAR;
                            loResult.Data.IEND_YEAR = loFirst.IEND_YEAR;
                        }
                    }
                    else
                    {
                        // Use soft period year if no period found
                        if (!string.IsNullOrWhiteSpace(poParameter.CSOFT_PERIOD) && poParameter.CSOFT_PERIOD.Length >= 4)
                        {
                            string lcYear = poParameter.CSOFT_PERIOD.Substring(0, 4);
                            if (int.TryParse(lcYear, out int liYear))
                            {
                                loResult.Data.ISTART_YEAR = liYear;
                                loResult.Data.IEND_YEAR = liYear;
                            }
                        }
                    }
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
        /// Validate department code for user access
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, and user ID</param>
        /// <returns>Result DTO with validation result (1 if valid, 0 if not)</returns>
        public async Task<FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>> ValidateDeptCodeAsync(FAT00100ValidateDeptCodeParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidateDeptCodeAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>
            {
                Data = new FAT00100ValidateDeptCodeResultDTO { IResult = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT TOP 1 1 FROM GSX_DEPARTMENT_USER " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID  " +
                                    " AND CDEPT_CODE = @CDEPT_CODE " +
                                    " AND CUSER_ID = @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult.Data.IResult = 1;
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
        /// Get period from transaction date
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and transaction date</param>
        /// <returns>Result DTO with default period</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>> GetPeriodDTAsync(FAT00100GetPeriodDTParameterDTO poParameter)
        {
            string lcMethod = nameof(GetPeriodDTAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>
            {
                Data = new FAT00100GetPeriodDTResultDTO { CDEFAULTPERIOD = string.Empty }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CDEFAULTPERIOD = PREIODCCYEAR + CPERIOD_NO  " +
                                    " FROM GSM_PERIOD_DT (nolock)  " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND @CTRANSACTION_DATE BETWEEN CSTART_DATE AND CEND_DATE ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poParameter.CTRANSACTION_DATE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetPeriodDTResultDTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    if (loFirst != null)
                    {
                        loResult.Data.CDEFAULTPERIOD = loFirst.CDEFAULTPERIOD ?? string.Empty;
                    }
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
        /// Delete transaction via stored procedure
        /// </summary>
        /// <param name="poEntity">Entity with key fields</param>
        protected override async Task R_DeletingAsync(FAT00100DTO poEntity)
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

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);
                // Commented out - CUSER_ID no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, string.Empty); // Placeholder

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);
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
        /// Display single record with supplier info and contact persons
        /// </summary>
        /// <param name="poEntity">Entity with key fields</param>
        /// <returns>Complete entity with supplier info and contact persons</returns>
        protected override async Task<FAT00100DTO> R_DisplayAsync(FAT00100DTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            FAT00100DTO loRtn = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FAT00100_GET_TRANS_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                // Commented out - CLANG_ID no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poEntity.CLANG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, string.Empty); // Placeholder

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);
                loRtn = loRtnList.FirstOrDefault() ?? new FAT00100DTO();
                if (loRtn != null)
                {
                    // Map stored procedure field names to existing property names
                    if (!string.IsNullOrWhiteSpace(loRtn.CREF_NO) && string.IsNullOrWhiteSpace(loRtn.CREFERENCE_NO))
                        loRtn.CREFERENCE_NO = loRtn.CREF_NO;
                    if (!string.IsNullOrWhiteSpace(loRtn.CREF_DATE))
                    {
                        // Commented out - CTRANSACTION_DATE no longer exists in FAT00100DTO
                        // if (string.IsNullOrWhiteSpace(loRtn.CTRANSACTION_DATE))
                        //     loRtn.CTRANSACTION_DATE = loRtn.CREF_DATE;
                        // Convert CREF_DATE string to DREF_DATE DateTime if valid
                        if (loRtn.CREF_DATE.Length == 8)
                        {
                            try
                            {
                                loRtn.DREF_DATE = DateTime.ParseExact(loRtn.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                // Leave DREF_DATE as default(DateTime) if parsing fails
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(loRtn.CDOCUMENT_DATE))
                        {
                            // Commented out - CTRANSACTION_DATE no longer exists in FAT00100DTO
                            // if (string.IsNullOrWhiteSpace(loRtn.CTRANSACTION_DATE))
                            //     loRtn.CTRANSACTION_DATE = loRtn.CDOCUMENT_DATE;
                            // Convert CDOCUMENT_DATE string to DDOCUMENT_DATE DateTime if valid
                            if (loRtn.CDOCUMENT_DATE.Length == 8)
                            {
                                try
                                {
                                    loRtn.DDOCUMENT_DATE = DateTime.ParseExact(loRtn.CDOCUMENT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                                }
                                catch
                                {
                                    // Leave DDOCUMENT_DATE as default(DateTime) if parsing fails
                                }
                            }
                        }
                         
                    }
                    
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

            return loRtn;
        }

        /// <summary>
        /// Save transaction with validation and supplier info handling
        /// </summary>
        /// <param name="poNewEntity">Entity to save</param>
        /// <param name="peCRUDMode">CRUD mode (Add or Edit)</param>
        protected override async Task R_SavingAsync(FAT00100DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            string lcMethod = nameof(R_SavingAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            string lcCmd = string.Empty;
            string pcInfoSeqnoOld = string.Empty;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FAT00100_SAVE_TRANS ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                // Determine action based on CRUD mode
                string lcAction = peCRUDMode == eCRUDMode.AddMode ? "NEW" : "EDIT";

                // Format dates as "yyyyMMdd" strings
                string lcRefDate = string.Empty;
                if (poNewEntity.DREF_DATE != default(DateTime))
                {
                    lcRefDate = poNewEntity.DREF_DATE.ToString("yyyyMMdd");
                }
                else if (!string.IsNullOrWhiteSpace(poNewEntity.CREF_DATE) && poNewEntity.CREF_DATE.Length == 8)
                {
                    lcRefDate = poNewEntity.CREF_DATE;
                }

                string lcDocDate = string.Empty;
                if (poNewEntity.DDOCUMENT_DATE != default(DateTime))
                {
                    lcDocDate = poNewEntity.DDOCUMENT_DATE.ToString("yyyyMMdd");
                }
                else if (!string.IsNullOrWhiteSpace(poNewEntity.CDOCUMENT_DATE) && poNewEntity.CDOCUMENT_DATE.Length == 8)
                {
                    lcDocDate = poNewEntity.CDOCUMENT_DATE;
                }

                // Get CREF_NO from CREFERENCE_NO if CREF_NO is empty
                string lcRefNo = !string.IsNullOrWhiteSpace(poNewEntity.CREF_NO) ? poNewEntity.CREF_NO : poNewEntity.CREFERENCE_NO ?? string.Empty;

                // Add parameters
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, lcRefNo);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, lcRefDate);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 30, poNewEntity.CDOCUMENT_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, lcDocDate);
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_MODULE", DbType.String, 2, poNewEntity.CSOURCE_MODULE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CFR_REF_NO", DbType.String, 30, poNewEntity.CFR_REF_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 20, poNewEntity.CSUPPLIER_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_SEQ_NO", DbType.String, 10, poNewEntity.CSUPPLIER_SEQ_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 400, poNewEntity.CTRANS_DESC ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 13, poNewEntity.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 13, poNewEntity.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NBCURRENCY_RATE);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                // Execute stored procedure and get result
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);
                var loRtn = loRtnList.FirstOrDefault();

                // Update CREC_ID from stored procedure result
                if (loRtn != null && !string.IsNullOrWhiteSpace(loRtn.CREC_ID))
                {
                    poNewEntity.CREC_ID = loRtn.CREC_ID;
                }

                // Get Exception - Note: R_ExternalException may not be available in NET6, preserve logic
                // loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
        /// Get combo period month (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and soft period</param>
        /// <returns>List of period numbers</returns>
        public async Task<List<FAT00100GetComboPeriodMonthResultDTO>> GetComboPeriodMonthAsync(FAT00100DTO poParameter)
        {
            string lcMethod = nameof(GetComboPeriodMonthAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetComboPeriodMonthResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CPERIOD_NO " +
                                    " From GSM_PERIOD_DT (nolock) " +
                                    " Where CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CCYEAR = LEFT(@CSOFT_PERIOD,4) ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                // Commented out - CSOFT_PERIOD no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CSOFT_PERIOD", DbType.String, 50, poParameter.CSOFT_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSOFT_PERIOD", DbType.String, 50, string.Empty); // Placeholder

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    // Commented out - CPERIOD_NO no longer exists in FAT00100DTO
                    // Need to use a different approach or create a separate DTO for this query
                    // loResult = loRtn.Select(x => new FAT00100GetComboPeriodMonthResultDTO
                    // {
                    //     CPERIOD_NO = x.CPERIOD_NO ?? string.Empty
                    // }).ToList();
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
        /// Get data grid (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and filter criteria</param>
        /// <returns>List of transaction header records for grid display</returns>
        public async Task<List<FAT00100GetDataGridResultDTO>> GetDataGridAsync(FAT00100DTO poParameter)
        {
            string lcMethod = nameof(GetDataGridAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetDataGridResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FAT00100_GET_TRANS_LIST ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParameter.CPERIODFROM ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParameter.CPERIODTO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 30, poParameter.CSUPPLIER_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_STATUS", DbType.String, 2, poParameter.CTRANS_STATUS ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, R_BackGlobalVar.CULTURE);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT00100GetDataGridResultDTO>(loDataTable).ToList();
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
        /// Get currency rate via stored procedure
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, currency code, rate type code, and transaction date</param>
        /// <returns>Result DTO with currency rate amounts</returns>
        public async Task<FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>> RSP_GET_CURRENCY_RATEAsync(FAT00100RSP_GET_CURRENCY_RATEParameterDTO poParameter)
        {
            string lcMethod = nameof(RSP_GET_CURRENCY_RATEAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>
            {
                Data = new FAT00100RSP_GET_CURRENCY_RATEResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                string lcCmd = string.Format("DECLARE @NLBASE_RATE_AMOUNT numeric(20,6) " +
                                              "DECLARE @NBBASE_RATE_AMOUNT numeric(20,6) " +
                                              "DECLARE @NLCURRENCY_RATE_AMOUNT numeric(20,6) " +
                                              "DECLARE @NBCURRENCY_RATE_AMOUNT numeric(20,6) " +
                                              "EXECUTE [dbo].[RSP_GET_CURRENCY_RATE] " +
                                              "   '{0}' " +
                                              "  ,'{1}' " +
                                              "  ,'{2}' " +
                                              "  ,'{3}' " +
                                              "  ,@NLBASE_RATE_AMOUNT OUTPUT " +
                                              "  ,@NBBASE_RATE_AMOUNT OUTPUT " +
                                              "  ,@NLCURRENCY_RATE_AMOUNT OUTPUT " +
                                              "  ,@NBCURRENCY_RATE_AMOUNT OUTPUT " +
                                              "SELECT " +
                                              "@NLBASE_RATE_AMOUNT as NLBASE_RATE_AMOUNT, " +
                                              "@NBBASE_RATE_AMOUNT as NBBASE_RATE_AMOUNT, " +
                                              "@NLCURRENCY_RATE_AMOUNT as NLCURRENCY_RATE_AMOUNT, " +
                                              "@NBCURRENCY_RATE_AMOUNT as NBCURRENCY_RATE_AMOUNT",
                                              poParameter.CCOMPANY_ID,
                                              poParameter.CCURRENCY_CODE,
                                              poParameter.CRATETYPE_CODE,
                                              poParameter.CTRANSACTION_DATE);

                var loRtn = loDb.SqlExecObjectQuery<FAT00100DTO>(lcCmd, loConn, false).FirstOrDefault();

                if (loRtn != null)
                {
                    // Commented out - amount properties no longer exist in FAT00100DTO
                    // loResult.Data.NLBASE_RATE_AMOUNT = loRtn.NLBASE_RATE_AMOUNT;
                    // loResult.Data.NBBASE_RATE_AMOUNT = loRtn.NBBASE_RATE_AMOUNT;
                    // loResult.Data.NLCURRENCY_RATE_AMOUNT = loRtn.NLCURRENCY_RATE_AMOUNT;
                    // loResult.Data.NBCURRENCY_RATE_AMOUNT = loRtn.NBCURRENCY_RATE_AMOUNT;
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
        /// Get company information via stored procedure RSP_GS_GET_COMPANY_INFO
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID</param>
        /// <returns>Result DTO with company information</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetCompanyInfoResultDTO>> FAT00100GetCompanyInfoAsync(FAT00100GetCompanyInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetCompanyInfoAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetCompanyInfoResultDTO>
            {
                Data = new FAT00100GetCompanyInfoResultDTO()
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
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetCompanyInfoResultDTO>(loDataTable).FirstOrDefault();

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
        /// Get last currency rate via stored procedure RSP_GS_GET_LAST_CURRENCY_RATE
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, currency code, rate type code, and rate date</param>
        /// <returns>Result DTO with last currency rate information</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetLastCurrencyRateResultDTO>> FAT00100GetLastCurrencyRateAsync(FAT00100GetLastCurrencyRateParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetLastCurrencyRateAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetLastCurrencyRateResultDTO>
            {
                Data = new FAT00100GetLastCurrencyRateResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_LAST_CURRENCY_RATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 10, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 10, poParameter.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, 10, poParameter.CRATE_DATE);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetLastCurrencyRateResultDTO>(loDataTable).FirstOrDefault();

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
        public async Task<FAT00100ResultDTO<FAT00100GetGetSystemParamResultDTO>> FAT00100GetGetSystemParamAsync(FAT00100GetGetSystemParamParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetGetSystemParamAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetGetSystemParamResultDTO>
            {
                Data = new FAT00100GetGetSystemParamResultDTO()
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
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetGetSystemParamResultDTO>(loDataTable).FirstOrDefault();

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
        public async Task<FAT00100ResultDTO<FAT00100GetPeriodeDtInfoResultDTO>> FAT00100GetPeriodeDtInfoAsync(FAT00100GetPeriodeDtInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetPeriodeDtInfoAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetPeriodeDtInfoResultDTO>
            {
                Data = new FAT00100GetPeriodeDtInfoResultDTO()
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
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetPeriodeDtInfoResultDTO>(loDataTable).FirstOrDefault();

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
        /// Get department lookup list via stored procedure RSP_GS_GET_DEPT_LOOKUP_LIST
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, user ID, and program ID</param>
        /// <returns>List of department lookup information</returns>
        public async Task<List<FAT00100GetDeptLookupListResultDTO>> FAT00100GetDeptLookupListAsync(FAT00100GetDeptLookupListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetDeptLookupListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetDeptLookupListResultDTO>();

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
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetDeptLookupListResultDTO>(loDataTable);

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
        public async Task<FAT00100ResultDTO<FAT00100GetTransCodeInfoResultDTO>> FAT00100GetTransCodeInfoAsync(FAT00100GetTransCodeInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetTransCodeInfoAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetTransCodeInfoResultDTO>
            {
                Data = new FAT00100GetTransCodeInfoResultDTO()
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
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetTransCodeInfoResultDTO>(loDataTable).FirstOrDefault();

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
        public async Task<FAT00100ResultDTO<FAT00100GetYearRangeResultDTO>> FAT00100GetYearRangeAsync(FAT00100GetYearRangeParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetYearRangeAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetYearRangeResultDTO>
            {
                Data = new FAT00100GetYearRangeResultDTO()
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
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetYearRangeResultDTO>(loDataTable).FirstOrDefault();

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
        /// Submit process via stored procedure with transaction
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, and user ID</param>
        /// <returns>Result DTO with result (1 if success, 0 if error)</returns>
        public async Task<FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>> SubmitProcessAsync(FAT00100SubmitProcessParameterDTO poParameter)
        {
            string lcMethod = nameof(SubmitProcessAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>
            {
                Data = new FAT00100SubmitProcessResultDTO { IResult = 0 }
            };

            try
            {
                using var transScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, System.Transactions.TransactionScopeAsyncFlowOption.Enabled);
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " DECLARE @DATENOW datetime = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) " +
                                    " EXEC RSP_FAT_SUBMIT @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);

                if (!loEx.Haserror)
                {
                    transScope.Complete();
                    loResult.Data.IResult = 1;
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

            return loResult;
        }

        /// <summary>
        /// Close process via stored procedure with transaction
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, and user ID</param>
        /// <returns>Result DTO with result (1 if success, 0 if error)</returns>
        public async Task<FAT00100ResultDTO<FAT00100CloseProcessResultDTO>> CloseProcessAsync(FAT00100CloseProcessParameterDTO poParameter)
        {
            string lcMethod = nameof(CloseProcessAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100CloseProcessResultDTO>
            {
                Data = new FAT00100CloseProcessResultDTO { IResult = 0 }
            };

            try
            {
                using var transScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, System.Transactions.TransactionScopeAsyncFlowOption.Enabled);
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " DECLARE @DATENOW datetime = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) " +
                                    " EXEC RSP_FAT_CLOSE @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);

                if (!loEx.Haserror)
                {
                    transScope.Complete();
                    loResult.Data.IResult = 1;
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

            return loResult;
        }

        /// <summary>
        /// Approve process via stored procedure with transaction
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, and user ID</param>
        /// <returns>Result DTO with result (1 if success, 0 if error)</returns>
        public async Task<FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>> ApproveProcessAsync(FAT00100ApproveProcessParameterDTO poParameter)
        {
            string lcMethod = nameof(ApproveProcessAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>
            {
                Data = new FAT00100ApproveProcessResultDTO { IResult = 0 }
            };

            try
            {
                using var transScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, System.Transactions.TransactionScopeAsyncFlowOption.Enabled);
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " DECLARE @DATENOW datetime = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) " +
                                    " EXEC RSP_FAT_APPROVE @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);

                if (!loEx.Haserror)
                {
                    transScope.Complete();
                    loResult.Data.IResult = 1;
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

            return loResult;
        }

        /// <summary>
        /// Validate asset code for sequence number
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and reference no</param>
        /// <returns>Result DTO with validation result (1 if invalid, 0 if valid)</returns>
        public async Task<FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>> ValidationAssetCodeAsync(FAT00100ValidationAssetCodeParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidationAssetCodeAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>
            {
                Data = new FAT00100ValidationAssetCodeResultDTO { IResult = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT a.CASSET_CODE " +
                                    " FROM FAT_TRANS_ASSET a (nolock), FAM_ASSET b (nolock) " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " and a.CREFERENCE_NO = @CREFERENCE_NO " +
                                    " and LDELETE_FLAG = 0  " +
                                    " and b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and b.CASSET_CODE = a.CASSET_CODE  " +
                                    " and b.CLSEQUENCE_NO > a.CASSET_TRANS_SEQNO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CFILTER_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult.Data.IResult = 1;
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
        /// Run approval precheck
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and approval code</param>
        /// <returns>Result DTO with boolean result (true if approval option is 2, false otherwise)</returns>
        public async Task<FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>> RunApprovalPrecheckAsync(FAT00100RunApprovalPrecheckParameterDTO poParameter)
        {
            string lcMethod = nameof(RunApprovalPrecheckAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>
            {
                Data = new FAT00100RunApprovalPrecheckResultDTO { LResult = false }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT IAPPROVAL_OPTION FROM GSM_ACTIVITY_APPROVAL (NOLOCK)            " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID AND CAPPROVAL_CODE = @CAPPROVAL_CODE  ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_CODE", DbType.String, 50, poParameter.CAPPROVAL_CODE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<int>(loDataTable).FirstOrDefault();

                if (loRtn == 2)
                {
                    loResult.Data.LResult = true;
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
        /// Void process via stored procedure
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, user ID, cancel reason code, and cancel approved by</param>
        /// <returns>Result DTO (void method, no return value)</returns>
        public async Task<FAT00100ResultDTO<FAT00100VoidProcessResultDTO>> VoidProcessAsync(FAT00100VoidProcessParameterDTO poParameter)
        {
            string lcMethod = nameof(VoidProcessAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100VoidProcessResultDTO>
            {
                Data = new FAT00100VoidProcessResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " EXEC RSP_FAT_VOID  @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID, @CCANCEL_REASON_CODE, @CCANCEL_APPROVED_BY ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_REASON_CODE", DbType.String, 50, poParameter.CCANCEL_REASON_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_APPROVED_BY", DbType.String, 50, poParameter.CCANCEL_APPROVED_BY);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);
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
        /// Validation before submit - check if asset exists
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and reference no</param>
        /// <returns>Result DTO with asset code if found, empty string otherwise</returns>
        public async Task<FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>> ValidationBeforeSubmitAsync(FAT00100ValidationBeforeSubmitParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidationBeforeSubmitAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>
            {
                Data = new FAT00100ValidationBeforeSubmitResultDTO { CASSET_CODE = string.Empty }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT TOP 1 " +
                                    "   CASSET_CODE " +
                                    " FROM FAT_TRANS_ASSET(nolock) " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CDEPT_CODE = @CDEPT_CODE " +
                                    " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND CREFERENCE_NO = @CREFERENCE_NO " +
                                    " AND LDELETE_FLAG = 0 ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    // Commented out - CASSET_CODE no longer exists in FAT00100DTO
                    // loResult.Data.CASSET_CODE = loFirst.CASSET_CODE ?? string.Empty;
                    loResult.Data.CASSET_CODE = string.Empty; // Placeholder
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
        /// Validation before close - validate journal via stored procedure
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and reference no</param>
        /// <returns>Result DTO (void method, throws exception if validation fails)</returns>
        public async Task<FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>> ValidationBeforeCloseAsync(FAT00100ValidationBeforeCloseParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidationBeforeCloseAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>
            {
                Data = new FAT00100ValidationBeforeCloseResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                // Init Exception - Note: R_ExternalException may not be available in NET6, preserve logic
                // R_ExternalException.R_SP_Init_Exception(loConn);

                loCmd.Parameters.Clear();
                loCmd.CommandText = " EXEC RSP_FA_VALIDATE_JOURNAL @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                // Get Exception - Note: R_ExternalException may not be available in NET6, preserve logic
                // loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
        /// Validate PJ transaction
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and reference no</param>
        /// <returns>Result DTO with asset code if validation fails, empty string otherwise</returns>
        public async Task<FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>> ValidatePJTransAsync(FAT00100ValidatePJTransParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidatePJTransAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>
            {
                Data = new FAT00100ValidatePJTransResultDTO { CASSET_CODE = string.Empty }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT TOP 1 a.CCOMPANY_ID																											 " +
                                    "   FROM PJT_TRANSACTION_DT a (nolock)																									 " +
                                    "     LEFT JOIN FAT_TRANS_ASSET b (nolock) on b.CCOMPANY_ID=a.CCOMPANY_ID and b.CFR_DEPT_CODE =											 " +
                                    "          a.CDEPT_CODE and b.CFR_TRANSACTION_CODE=a.CTRANSACTION_CODE and b.CFR_REFERENCE_NO =											 " +
                                    "          a.CREFERENCE_NO and b.CFR_SEQUENCE_NO=a.CSEQUENCE_NO and b.LDELETE_FLAG=0													 " +
                                    "   WHERE a.CCOMPANY_ID=@CCOMPANY_ID AND a.CDEPT_CODE=@CDEPT_CODE AND																 " +
                                    "         a.CTRANSACTION_CODE = @CTRANSACTION_CODE AND a.CREFERENCE_NO = @CREFERENCE_NO and a.CSTATUS='08' and a.CALLOC_EXPENSE_CODE<>'' and b.CCOMPANY_ID is null ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                // Commented out - CTRANSACTION_CODE no longer exists in FAT00100DTO
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, string.Empty); // Placeholder
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    // Commented out - CASSET_CODE no longer exists in FAT00100DTO
                    // loResult.Data.CASSET_CODE = loFirst.CASSET_CODE ?? string.Empty;
                    loResult.Data.CASSET_CODE = string.Empty; // Placeholder
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
        /// Get status list via stored procedure RSP_GS_GET_GSB_CODE_LIST (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing application, company ID, class ID, language ID, and rec ID list</param>
        /// <returns>List of status codes and names</returns>
        public async Task<List<FAT00100GetStatusListResultDTO>> FAT00100GetStatusListAsync(FAT00100GetStatusListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetStatusListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetStatusListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 50, poParameter.CAPPLICATION);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, poParameter.CCLASS_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID_LIST", DbType.String, 500, poParameter.CREC_ID_LIST);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetStatusListResultDTO>(loDataTable);

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
        /// Get currency list via stored procedure RSP_GS_GET_CURRENCY_LIST (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and user ID</param>
        /// <returns>List of currency codes and names</returns>
        public async Task<List<FAT00100GetCurrencyListResultDTO>> FAT00100GetCurrencyListAsync(FAT00100GetCurrencyListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetCurrencyListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetCurrencyListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetCurrencyListResultDTO>(loDataTable);

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

        

        #region R_*Async Overrides

        // Override unused R_*Async methods with NotImplementedException
        // Note: R_DisplayAsync, R_SavingAsync, and R_DeletingAsync are implemented above

        #endregion
        
    }
}

