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
        /// Get initial process data including system parameters, supplier info, and user rights
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, transaction code, etc.</param>
        /// <returns>Result DTO with initial process data</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>> GetInitialProcessAsync(FAT00100GetInitialProcessParameterDTO poParameter)
        {
            string lcMethod = nameof(GetInitialProcessAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>
            {
                Data = new FAT00100GetInitialProcessResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                // Get system parameters from FAM_SYSTEM
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CTRANS_DEPT_CODE, " +
                                    " CASSET_DEPT_CODE, " +
                                    " LINCREMENT_FLAG,  " +
                                    " LJRNGRP_MODE,  " +
                                    " LDEPT_MODE, " +
                                    " CPERIOD_MODE,  " +
                                    " CCURRENT_PERIOD,  " +
                                    " CSOFT_PERIOD, " +
                                    " CRATETYPE_CODE,  " +
                                    " CGLLINK_DATE,  " +
                                    " CPJLINK_DATE " +
                                    " FROM FAM_SYSTEM (nolock) " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    loResult.Data.CTRANS_DEPT_CODE = loFirst.CTRANS_DEPT_CODE ?? string.Empty;
                    loResult.Data.CASSET_DEPT_CODE = loFirst.CASSET_DEPT_CODE ?? string.Empty;
                    loResult.Data.LASSET_INCREMENT_FLAG = loFirst.LINCREMENT_FLAG;
                    loResult.Data.LJRNGRP_MODE = loFirst.LJRNGRP_MODE;
                    loResult.Data.LDEPT_MODE = loFirst.LDEPT_MODE;
                    loResult.Data.CPERIOD_MODE = loFirst.CPERIOD_MODE ?? string.Empty;
                    loResult.Data.CCURRENT_PERIOD = loFirst.CCURRENT_PERIOD ?? string.Empty;
                    loResult.Data.CSOFT_PERIOD = loFirst.CSOFT_PERIOD ?? string.Empty;
                    loResult.Data.CRATETYPE_CODE = loFirst.CRATETYPE_CODE ?? string.Empty;
                    loResult.Data.CGLLINK_DATE = loFirst.CGLLINK_DATE ?? string.Empty;
                    loResult.Data.CPJLINK_DATE = loFirst.CPJLINK_DATE ?? string.Empty;
                }

                // Set default dept code if not provided
                string lcDeptCode = poParameter.CDEPT_CODE;
                if (string.IsNullOrWhiteSpace(lcDeptCode))
                {
                    lcDeptCode = loResult.Data.CTRANS_DEPT_CODE;
                }

                // Get supplier info if reference number is provided
                if (!string.IsNullOrWhiteSpace(poParameter.CREFERENCE_NO))
                {
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " SELECT CSUPPLIER_ID, " +
                                        " CTRANSACTION_PRD " +
                                        " From FAT_TRANS_HD (nolock) " +
                                        " Where CCOMPANY_ID = @CCOMPANY_ID " +
                                        " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                        " and CDEPT_CODE = @CDEPT_CODE " +
                                        " and CREFERENCE_NO = @CREFERENCE_NO ";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, lcDeptCode);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                    loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                    if (loRtn != null && loRtn.Count > 0)
                    {
                        var loFirst = loRtn.FirstOrDefault();
                        loResult.Data.CSUPPLIER_ID = loFirst.CSUPPLIER_ID ?? string.Empty;
                        loResult.Data.CTRANSACTION_PRD = loFirst.CTRANSACTION_PRD ?? string.Empty;
                    }
                }
                else
                {
                    loResult.Data.CSUPPLIER_ID = string.Empty;
                    loResult.Data.CTRANSACTION_PRD = string.Empty;
                }

                // Get currency info from HSM_PROPERTY_SYSTEM
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CLOCAL_CURRENCY_CODE,  " +
                                    " CBASE_CURRENCY_CODE, " +
                                    " LCUST_PERIOD_FLAG " +
                                    " FROM HSM_PROPERTY_SYSTEM (nolock) " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);

                loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    loResult.Data.CLOCAL_CURRENCY_CODE = loFirst.CLOCAL_CURRENCY_CODE ?? string.Empty;
                    loResult.Data.CBASE_CURRENCY_CODE = loFirst.CBASE_CURRENCY_CODE ?? string.Empty;
                    loResult.Data.LCUST_PERIOD_FLAG = loFirst.LCUST_PERIOD_FLAG;
                }

                // Get transaction description and flags
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CFILTER_TRANS_DESC = ISNULL(b.CDESCRIPTION,a.CTRANSACTION_NAME), " +
                                    " LAPPROVAL_FLAG, " +
                                    " LINCREMENT_FLAG " +
                                    " From GSM_TRANSACTION_CODE a (nolock) " +
                                    " LEFT JOIN GSB_TRANSLATE b (nolock)  " +
                                    " ON b.CTABLE_NAME = 'GSM_TRANSACTION_CODE' " +
                                    " AND B.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND B.CKEY_ID = a.CCOMPANY_ID + a.CTRANSACTION_CODE " +
                                    " Where a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CTRANSACTION_CODE = @CTRANSACTION_CODE1 ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE1", DbType.String, 50, poParameter.CTRANSACTION_CODE);

                loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    loResult.Data.CFILTER_TRANS_DESC = loFirst.CFILTER_TRANS_DESC ?? string.Empty;
                    loResult.Data.LAPPROVAL_FLAG = loFirst.LAPPROVAL_FLAG;
                    loResult.Data.LINCREMENT_FLAG = loFirst.LINCREMENT_FLAG;
                }

                // Get PJ transaction description
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CPJ_TRANS_DESC = ISNULL(b.CDESCRIPTION,a.CTRANSACTION_NAME) " +
                                    " From GSM_TRANSACTION_CODE a (nolock) " +
                                    " LEFT JOIN GSB_TRANSLATE b (nolock) ON " +
                                    " b.CTABLE_NAME = 'GSM_TRANSACTION_CODE' " +
                                    " AND B.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND B.CKEY_ID = a.CCOMPANY_ID + a.CTRANSACTION_CODE " +
                                    " Where a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CTRANSACTION_CODE = @CPJ_TRANS_CODE ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CPJ_TRANS_CODE", DbType.String, 50, poParameter.CPJ_TRANS_CODE);

                loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    loResult.Data.CPJ_TRANS_DESC = loFirst.CPJ_TRANS_DESC ?? string.Empty;
                }

                // Check if user can approve
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CAST(1 as bit) as LCAN_APPROVE " +
                                    " FROM FAM_APPROVAL_USER (nolock) " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CTRANSACTION_CODE = @CTRANSACTION_CODE1 " +
                                    " AND CUSER_ID = @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE1", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    loResult.Data.LCAN_APPROVE = loFirst.LCAN_APPROVE;
                }

                // Check if user can close
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CAST(1 as bit) as LCAN_CLOSE " +
                                    " FROM GSM_USER_RIGHT (nolock)  " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CACTIVITY_CODE = 'FA013001'  " +
                                    " AND CUSER_ID = @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    loResult.Data.LCAN_CLOSE = loFirst.LCAN_CLOSE;
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
                    var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                    if (loRtn != null && loRtn.Count > 0)
                    {
                        var loFirst = loRtn.FirstOrDefault();
                        loResult.Data.ISTART_YEAR = loFirst.ISTART_YEAR;
                        loResult.Data.IEND_YEAR = loFirst.IEND_YEAR;
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
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    var loFirst = loRtn.FirstOrDefault();
                    loResult.Data.CDEFAULTPERIOD = loFirst.CDEFAULTPERIOD ?? string.Empty;
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

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
                loCmd.CommandText = " SELECT CDEPT_CODE,  " +
                                    " a.CTRANSACTION_CODE,  " +
                                    " CREFERENCE_NO,  " +
                                    " CTRANSACTION_DATE,  " +
                                    " CSTATUS,  " +
                                    " CSTATUS_DESC = ISNULL(e.DESCRIPTION, CSTATUS), " +
                                    " a.CCURRENCY_CODE, " +
                                    " NLBASE_RATE_AMOUNT,  " +
                                    " NLCURRENCY_RATE_AMOUNT,  " +
                                    " NBBASE_RATE_AMOUNT,  " +
                                    " NBCURRENCY_RATE_AMOUNT, " +
                                    " NTRANSACTION_AMOUNT,  " +
                                    " NLTRANSACTION_AMOUNT,  " +
                                    " NBTRANSACTION_AMOUNT, " +
                                    " CDOCUMENT_DATE,  " +
                                    " CDOCUMENT_NO, " +
                                    " CSUPPLIER_ID,  " +
                                    " CSUPPLIER_NAME,  " +
                                    " CFR_MODULE,  " +
                                    " CFR_DEPT_CODE,  " +
                                    " CFR_TRANSACTION_CODE,  " +
                                    " CFR_REFERENCE_NO, " +
                                    " LGLLINK, CGL_TRF_STATUS, " +
                                    " a.DUPDATE_DATE, " +
                                    " CDEPT_NAME = ISNULL(b.DESCRIPTION, ''), " +
                                    " CCURRENCY_NAME = ISNULL(CCURRENCY_NAME, ''), " +
                                    " CTRANSACTION_NAME = ISNULL(CTRANSACTION_NAME, ''), " +
                                    " CTRANSACTION_DESCR, " +
                                    " CINFO_SEQNO, " +
                                    " a.CCREATE_BY, " +
                                    " a.DCREATE_DATE, " +
                                    " a.CUPDATE_BY, " +
                                    " a.DUPDATE_DATE " +
                                    " FROM FAT_TRANS_HD a (nolock) " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) b " +
                                    " ON b.CODE = a.CDEPT_CODE " +
                                    " LEFT JOIN SAB_CURRENCY c (nolock)  " +
                                    " ON c.CCURRENCY_CODE = a.CCURRENCY_CODE  " +
                                    " LEFT JOIN GSM_TRANSACTION_CODE d (nolock)  " +
                                    " ON d.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and d.CTRANSACTION_CODE = a.CTRANSACTION_CODE  " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_TRX_STATUS', '', @CFOREIGN_LANGUAGE) e " +
                                    " ON e.CODE = a.CSTATUS " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CFILTER_TRANS_CODE AND a.CREFERENCE_NO = @CREFERENCE_NO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poEntity.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFILTER_TRANS_CODE", DbType.String, 50, poEntity.CFILTER_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);
                loRtn = loRtnList.FirstOrDefault() ?? new FAT00100DTO();

                // Initialize supplier and contact person collections
                loRtn.oSupp = new FAT00100SuppDTO();
                loRtn.oCP = new List<FAT00100CPDTO>();

                // Get supplier info
                if (!string.IsNullOrWhiteSpace(loRtn.CSUPPLIER_ID) && !string.IsNullOrWhiteSpace(loRtn.CINFO_SEQNO))
                {
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " SELECT * FROM GSM_SUPPLIER_INFO WHERE CCOMPANY_ID = @CCOMPANY_ID AND CSUPPLIER_ID = @CSUPPLIER_ID AND CINFO_SEQNO = @CINFO_SEQNO ";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, loRtn.CSUPPLIER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CINFO_SEQNO", DbType.String, 50, loRtn.CINFO_SEQNO);

                    loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    var loSuppList = R_Utility.R_ConvertTo<FAT00100SuppDTO>(loDataTable);
                    loRtn.oSupp = loSuppList.FirstOrDefault() ?? new FAT00100SuppDTO();

                    // Get contact persons
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " SELECT * FROM GSM_SUPPLIER_CONTACT WHERE CCOMPANY_ID = @CCOMPANY_ID AND CSUPPLIER_ID = @CSUPPLIER_ID AND CINFO_SEQNO = @CINFO_SEQNO ";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, loRtn.CSUPPLIER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CINFO_SEQNO", DbType.String, 50, loRtn.CINFO_SEQNO);

                    loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    loRtn.oCP = R_Utility.R_ConvertTo<FAT00100CPDTO>(loDataTable).ToList();
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

                pcInfoSeqnoOld = poNewEntity.CINFO_SEQNO ?? string.Empty;

                // Init Exception - Note: R_ExternalException may not be available in NET6, preserve logic
                // R_ExternalException.R_SP_Init_Exception(loConn);

                // Check if record exists
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT TOP 1 1 " +
                                    " FROM FAT_TRANS_HD " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CDEPT_CODE = @CDEPT_CODE " +
                                    " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND CREFERENCE_NO = @CREFERENCE_NO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);

                _logger.LogDebug("Executing SQL Query: {Query}", loCmd.CommandText);
                foreach (DbParameter param in loCmd.Parameters)
                {
                    _logger.LogDebug("Parameter: {Name} = {Value}", param.ParameterName, param.Value ?? "NULL");
                }
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable).FirstOrDefault();

                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    if (loRtn != null)
                    {
                        loEx.Add(GetError("PS001"));
                        loEx.ThrowExceptionIfErrors();
                        return;
                    }

                    // Handle reference number generation
                    if (poNewEntity.LINCREMENT_FLAG == true)
                    {
                        loCmd.Parameters.Clear();
                        loCmd.CommandText = " EXEC RSP_GET_REFNO @CCOMPANY_ID, @CTRANSACTION_CODE, @CDEPT_CODE, @CTRANSACTION_DATE, @CUSER_ID, @CREFERENCE_NO OUTPUT " +
                                            " SELECT @CREFERENCE_NO AS CREFERENCE_NO ";

                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poNewEntity.CTRANSACTION_DATE);
                        loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, string.Empty);
                        // Set output parameter direction
                        if (loCmd.Parameters["@CREFERENCE_NO"] is DbParameter loRefNoParam)
                        {
                            loRefNoParam.Direction = System.Data.ParameterDirection.Output;
                        }

                        _logger.LogDebug("Executing SQL Query for Reference Number Generation: {Query}", loCmd.CommandText);
                        foreach (DbParameter param in loCmd.Parameters)
                        {
                            _logger.LogDebug("Parameter: {Name} = {Value}", param.ParameterName, param.Value ?? "NULL");
                        }

                        loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                        loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable).FirstOrDefault();
                        if (loRtn != null && !string.IsNullOrWhiteSpace(loRtn.CREFERENCE_NO))
                        {
                            poNewEntity.CREFERENCE_NO = loRtn.CREFERENCE_NO;
                        }
                    }
                    else
                    {
                        // Validate reference number exists
                        loCmd.Parameters.Clear();
                        loCmd.CommandText = " SELECT TOP 1 1 " +
                                            " FROM FAT_TRANS_HD " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                            " AND CDEPT_CODE = @CDEPT_CODE " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO ";

                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);

                        _logger.LogInfo(loCmd.CommandText);
                        _logger.LogDebug("Executing SQL Query for Reference Number Validation: {Query}", loCmd.CommandText);
                        foreach (DbParameter param in loCmd.Parameters)
                        {
                            _logger.LogDebug("Parameter: {Name} = {Value}", param.ParameterName, param.Value ?? "NULL");
                        }
                        loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                        loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable).FirstOrDefault();
                    }

                    // Handle supplier info sequence number
                    if (poNewEntity.LONETIME_FLAG == false)
                    {
                        poNewEntity.CINFO_SEQNO = string.Empty;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(poNewEntity.CINFO_SEQNO))
                        {
                            lcCmd = string.Format(" DECLARE @CINFO_SEQNO VARCHAR(20) " +
                                                  " EXEC RSP_GET_SEQUENCE  0, '', 'GSM_SUPPLIER_INFO', 'Where CCOMPANY_ID = ''{0}'' AND CSUPPLIER_ID = ''{1}'' ', @CINFO_SEQNO OUTPUT, 'CINFO_SEQNO' " +
                                                  " SELECT CINFO_SEQNO = @CINFO_SEQNO ",
                                                  poNewEntity.CCOMPANY_ID,
                                                  poNewEntity.CSUPPLIER_ID);

                            _logger.LogInfo(lcCmd);
                            _logger.LogDebug("Executing SQL Query for Supplier Info SeqNo: {Query}", lcCmd);
                            try
                            {
                                var loRtnSeq = loDb.SqlExecObjectQuery<FAT00100DTO>(lcCmd, loConn, false);

                                if (loRtnSeq != null && loRtnSeq.Count > 0)
                                {
                                    poNewEntity.CINFO_SEQNO = loRtnSeq.FirstOrDefault().CINFO_SEQNO ?? string.Empty;
                                }
                                else
                                {
                                    poNewEntity.CINFO_SEQNO = string.Empty;
                                }

                                _logger.LogInfo(lcCmd);
                                _logger.LogDebug(lcCmd);
                            }
                            catch (Exception ex)
                            {
                                loEx.Add(ex);
                            }
                        }
                    }

                    // Insert into FAT_TRANS_HD
                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                                        " INSERT INTO FAT_TRANS_HD " +
                                        " (CCOMPANY_ID, " +
                                        " CDEPT_CODE, " +
                                        " CTRANSACTION_CODE, " +
                                        " CREFERENCE_NO, " +
                                        " CSUPPLIER_ID, " +
                                        " CINFO_SEQNO, " +
                                        " CSUPPLIER_NAME, " +
                                        " CTRANSACTION_DATE, " +
                                        " CTRANSACTION_PRD, " +
                                        " CTRANSACTION_DESCR, " +
                                        " CDOCUMENT_DATE, " +
                                        " CDOCUMENT_NO, " +
                                        " CCURRENCY_CODE, " +
                                        " CFR_MODULE, " +
                                        " CFR_DEPT_CODE, " +
                                        " CFR_TRANSACTION_CODE, " +
                                        " CFR_REFERENCE_NO, " +
                                        " NLBASE_RATE_AMOUNT, " +
                                        " NLCURRENCY_RATE_AMOUNT, " +
                                        " NBBASE_RATE_AMOUNT, " +
                                        " NBCURRENCY_RATE_AMOUNT, " +
                                        " NTRANSACTION_AMOUNT, " +
                                        " NLTRANSACTION_AMOUNT, " +
                                        " NBTRANSACTION_AMOUNT, " +
                                        " CSTATUS, " +
                                        " LGLLINK, " +
                                        " CGL_TRF_STATUS, " +
                                        " CGL_REFERENCE_NO, " +
                                        " CCREATE_BY, " +
                                        " DCREATE_DATE, " +
                                        " CUPDATE_BY, " +
                                        " DUPDATE_DATE) " +
                                        " VALUES " +
                                        " (@CCOMPANY_ID, " +
                                        " @CDEPT_CODE, " +
                                        " @CTRANSACTION_CODE, " +
                                        " @CREFERENCE_NO, " +
                                        " @CSUPPLIER_ID, " +
                                        " @CINFO_SEQNO, " +
                                        " @CSUPPLIER_NAME, " +
                                        " @CTRANSACTION_DATE, " +
                                        " @CTRANSACTION_PRD, " +
                                        " @CTRANSACTION_DESCR, " +
                                        " @CDOCUMENT_DATE, " +
                                        " @CDOCUMENT_NO, " +
                                        " @CCURRENCY_CODE, " +
                                        " @CFR_MODULE, " +
                                        " @CFR_DEPT_CODE, " +
                                        " @CFR_TRANSACTION_CODE, " +
                                        " @CFR_REFERENCE_NO, " +
                                        " @NLBASE_RATE_AMOUNT, " +
                                        " @NLCURRENCY_RATE_AMOUNT, " +
                                        " @NBBASE_RATE_AMOUNT, " +
                                        " @NBCURRENCY_RATE_AMOUNT, " +
                                        " @NTRANSACTION_AMOUNT, " +
                                        " @NLTRANSACTION_AMOUNT, " +
                                        " @NBTRANSACTION_AMOUNT, " +
                                        " @CSTATUS, " +
                                        " @LGLLINK, " +
                                        " @CGL_TRF_STATUS, " +
                                        " @CGL_REFERENCE_NO, " +
                                        " @CCREATE_BY, " +
                                        " @DATENOW, " +
                                        " @CUPDATE_BY, " +
                                        " @DATENOW) ";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, poNewEntity.CSUPPLIER_ID ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CINFO_SEQNO", DbType.String, 50, poNewEntity.CINFO_SEQNO ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_NAME", DbType.String, 200, poNewEntity.CSUPPLIER_NAME ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poNewEntity.CTRANSACTION_DATE ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_PRD", DbType.String, 50, poNewEntity.CTRANSACTION_PRD ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 500, poNewEntity.CTRANSACTION_DESCR ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_DATE", DbType.String, 50, poNewEntity.CDOCUMENT_DATE ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_NO", DbType.String, 50, poNewEntity.CDOCUMENT_NO ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_MODULE", DbType.String, 50, poNewEntity.CFR_MODULE ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, 50, poNewEntity.CFR_DEPT_CODE ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_TRANSACTION_CODE", DbType.String, 50, poNewEntity.CFR_TRANSACTION_CODE ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_REFERENCE_NO", DbType.String, 50, poNewEntity.CFR_REFERENCE_NO ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLBASE_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBBASE_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NTRANSACTION_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NLTRANSACTION_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NBTRANSACTION_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poNewEntity.CSTATUS ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 1, poNewEntity.LGLLINK);
                    loDb.R_AddCommandParameter(loCmd, "@CGL_TRF_STATUS", DbType.String, 50, poNewEntity.CGL_TRF_STATUS ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CGL_REFERENCE_NO", DbType.String, 50, poNewEntity.CGL_REFERENCE_NO ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CCREATE_BY", DbType.String, 50, poNewEntity.CCREATE_BY ?? string.Empty);
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY ?? string.Empty);

                    var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@"))
                        .ToDictionary(x => x.ParameterName, x => x.Value);

                    _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                }
                else
                {
                    // Edit Mode
                    if (loRtn == null)
                    {
                        loEx.Add(GetError("PS002"));
                        loEx.ThrowExceptionIfErrors();
                        return;
                    }

                    if (poNewEntity.LCHANGE_DESC == false)
                    {
                        // Full update
                        loCmd.Parameters.Clear();
                        loCmd.CommandText = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                                            " UPDATE FAT_TRANS_HD SET " +
                                            " CSUPPLIER_ID = @CSUPPLIER_ID, " +
                                            " CINFO_SEQNO = @CINFO_SEQNO, " +
                                            " CSUPPLIER_NAME = @CSUPPLIER_NAME, " +
                                            " CTRANSACTION_DATE = @CTRANSACTION_DATE, " +
                                            " CTRANSACTION_PRD = @CTRANSACTION_PRD, " +
                                            " CTRANSACTION_DESCR = @CTRANSACTION_DESCR, " +
                                            " CDOCUMENT_DATE = @CDOCUMENT_DATE, " +
                                            " CDOCUMENT_NO = @CDOCUMENT_NO, " +
                                            " CCURRENCY_CODE = @CCURRENCY_CODE, " +
                                            " CFR_MODULE = @CFR_MODULE, " +
                                            " CFR_DEPT_CODE = @CFR_DEPT_CODE, " +
                                            " CFR_TRANSACTION_CODE = @CFR_TRANSACTION_CODE, " +
                                            " CFR_REFERENCE_NO = @CFR_REFERENCE_NO, " +
                                            " LGLLINK = @LGLLINK, " +
                                            " CUPDATE_BY = @CUPDATE_BY, " +
                                            " DUPDATE_DATE = @DATENOW, " +
                                            " NLBASE_RATE_AMOUNT = @NLBASE_RATE_AMOUNT, " +
                                            " NLCURRENCY_RATE_AMOUNT = @NLCURRENCY_RATE_AMOUNT, " +
                                            " NBBASE_RATE_AMOUNT = @NBBASE_RATE_AMOUNT, " +
                                            " NBCURRENCY_RATE_AMOUNT = @NBCURRENCY_RATE_AMOUNT " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                            " AND CDEPT_CODE = @CDEPT_CODE " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO ";

                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                        loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, poNewEntity.CSUPPLIER_ID ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CINFO_SEQNO", DbType.String, 50, poNewEntity.CINFO_SEQNO ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_NAME", DbType.String, 200, poNewEntity.CSUPPLIER_NAME ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poNewEntity.CTRANSACTION_DATE ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_PRD", DbType.String, 50, poNewEntity.CTRANSACTION_PRD ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 500, poNewEntity.CTRANSACTION_DESCR ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_DATE", DbType.String, 50, poNewEntity.CDOCUMENT_DATE ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_NO", DbType.String, 50, poNewEntity.CDOCUMENT_NO ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CFR_MODULE", DbType.String, 50, poNewEntity.CFR_MODULE ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, 50, poNewEntity.CFR_DEPT_CODE ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CFR_TRANSACTION_CODE", DbType.String, 50, poNewEntity.CFR_TRANSACTION_CODE ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CFR_REFERENCE_NO", DbType.String, 50, poNewEntity.CFR_REFERENCE_NO ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 1, poNewEntity.LGLLINK);
                        loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLBASE_RATE_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBBASE_RATE_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE_AMOUNT);
                        // Log the query for auditing/debugging
                        try
                        {
                            string lcLogQuery = R_GetAppSettings<string>("LOG_QUERY");
                            if (lcLogQuery == "1")
                            {
                                string logQuery = loCmd.CommandText;
                                foreach (DbParameter param in loCmd.Parameters)
                                {
                                    logQuery = logQuery.Replace(param.ParameterName, param.Value != null ? $"'{param.Value.ToString()}'" : "NULL");
                                }
                                _logger.LogDebug($"[FAT00100 - Update Query] {logQuery}");
                            }
                        }
                        catch
                        {
                            // Ignore if LOG_QUERY setting is not available
                        }

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    }
                    else
                    {
                        // Update description only
                        loCmd.Parameters.Clear();
                        loCmd.CommandText = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                                            " UPDATE FAT_TRANS_HD SET " +
                                            " CTRANSACTION_DESCR = @CTRANSACTION_DESCR, " +
                                            " CUPDATE_BY = @CUPDATE_BY, " +
                                            " DUPDATE_DATE = @DATENOW " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                            " AND CDEPT_CODE = @CDEPT_CODE " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO ";

                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 500, poNewEntity.CTRANSACTION_DESCR ?? string.Empty);
                        loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY ?? string.Empty);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    }
                }

                // Handle supplier info and contact persons if one-time flag is true
                if (poNewEntity.LONETIME_FLAG == true && poNewEntity.oSupp != null)
                {
                    if (!string.IsNullOrWhiteSpace(pcInfoSeqnoOld))
                    {
                        // Update existing supplier info
                        lcCmd = string.Format(" DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY (N'{0}') " +
                                              " UPDATE GSM_SUPPLIER_INFO SET " +
                                              " CSUPPLIER_NAME = N'{1}', " +
                                              " CADDRESS = N'{2}', " +
                                              " CPOSTAL_CODE = N'{3}', " +
                                              " CCITY = N'{4}', " +
                                              " CCOUNTRY_CODE = N'{5}', " +
                                              " CSTATE_CODE = N'{6}', " +
                                              " CPHONE_1 = N'{7}', " +
                                              " CPHONE_2 = N'{8}', " +
                                              " CFAX_NO1 = N'{9}', " +
                                              " CFAX_NO2 = N'{10}', " +
                                              " CEMAIL_1 = N'{11}', " +
                                              " CEMAIL_2 = N'{12}', " +
                                              " CTAX_NAME = N'{13}', " +
                                              " CTAX_REG_TP = N'{14}', " +
                                              " CTAX_REGISTER_ID = N'{15}', " +
                                              " DTAX_REGISTER_DATE = '{16}', " +
                                              " CTAX_BUSINESS_TYPE = N'{17}', " +
                                              " CTAX_BUSINESS_NAME = N'{18}', " +
                                              " CNPWP = N'{19}', " +
                                              " CNPKP = N'{20}', " +
                                              " CUPDATE_BY = N'{21}', " +
                                              " DUPDATE_DATE = @DATENOW " +
                                              " WHERE CCOMPANY_ID = N'{0}' " +
                                              " AND CSUPPLIER_ID = N'{22}' " +
                                              " AND CINFO_SEQNO = N'{23}' ",
                                              poNewEntity.oSupp.CCOMPANY_ID ?? string.Empty,
                                              poNewEntity.oSupp.CSUPPLIER_NAME ?? string.Empty,
                                              poNewEntity.oSupp.CADDRESS ?? string.Empty,
                                              poNewEntity.oSupp.CPOSTAL_CODE ?? string.Empty,
                                              poNewEntity.oSupp.CCITY ?? string.Empty,
                                              poNewEntity.oSupp.CCOUNTRY_CODE ?? string.Empty,
                                              poNewEntity.oSupp.CSTATE_CODE ?? string.Empty,
                                              poNewEntity.oSupp.CPHONE_1 ?? string.Empty,
                                              poNewEntity.oSupp.CPHONE_2 ?? string.Empty,
                                              poNewEntity.oSupp.CPHONE_2 ?? string.Empty, // BUG in net4 line 955: CPHONE_2 used twice (position 9 should be CPHONE_3 or CFAX_NO1)
                                              poNewEntity.oSupp.CFAX_NO1 ?? string.Empty,
                                              poNewEntity.oSupp.CFAX_NO2 ?? string.Empty,
                                              poNewEntity.oSupp.CEMAIL_1 ?? string.Empty,
                                              poNewEntity.oSupp.CEMAIL_2 ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_NAME ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_REG_TP ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_REGISTER_ID ?? string.Empty,
                                              poNewEntity.oSupp.DTAX_REGISTER_DATE != default(DateTime) ? poNewEntity.oSupp.DTAX_REGISTER_DATE.ToString("yyyy-MM-dd") : string.Empty,
                                              poNewEntity.oSupp.CTAX_BUSINESS_TYPE ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_BUSINESS_NAME ?? string.Empty,
                                              poNewEntity.oSupp.CNPWP ?? string.Empty,
                                              poNewEntity.oSupp.CNPKP ?? string.Empty,
                                              poNewEntity.oSupp.CUPDATE_BY ?? string.Empty,
                                              poNewEntity.oSupp.CSUPPLIER_ID ?? string.Empty,
                                              pcInfoSeqnoOld);

                        // Log the executed SQL command for debugging/auditing
                        _logger.LogDebug("Executing Update Supplier Query: " + lcCmd);

                        await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);

                        // Delete existing contact persons
                        lcCmd = string.Format(" DELETE GSM_SUPPLIER_CONTACT " +
                                              " WHERE CCOMPANY_ID = N'{0}'  " +
                                              " AND CSUPPLIER_ID = N'{1}'  " +
                                              " and CINFO_SEQNO = N'{2}'  ",
                                              poNewEntity.CCOMPANY_ID,
                                              poNewEntity.CSUPPLIER_ID ?? string.Empty,
                                              pcInfoSeqnoOld);
                        // Log the executed SQL command for debugging/auditing
                        _logger.LogDebug("Executing Delete Supplier Contact Query: " + lcCmd);
                        await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);

                        // Insert contact persons with sequence
                        string cSequence = "000000";
                        if (poNewEntity.oCP != null)
                        {
                            foreach (var x in poNewEntity.oCP)
                            {
                                lcCmd = string.Format(" DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY (N'{0}') " +
                                                      " INSERT INTO GSM_SUPPLIER_CONTACT " +
                                                      " (CCOMPANY_ID, CSUPPLIER_ID, CINFO_SEQNO, CCONTACT_SEQNO, CFIRST_NAME, CLAST_NAME,  " +
                                                      " CTITLE, COCCUP_CODE, LDEFAULT, CCREATE_BY, DCREATE_DATE, CUPDATE_BY, DUPDATE_DATE) " +
                                                      " VALUES " +
                                                      " (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', " +
                                                      " N'{6}', N'{7}', '{8}', N'{9}', @DATENOW, N'{10}', @DATENOW) ",
                                                      x.CCOMPANY_ID ?? string.Empty,
                                                      x.CSUPPLIER_ID ?? string.Empty,
                                                      x.CINFO_SEQNO ?? string.Empty,
                                                      cSequence,
                                                      x.CFIRST_NAME ?? string.Empty,
                                                      x.CLAST_NAME ?? string.Empty,
                                                      x.CTITLE ?? string.Empty,
                                                      x.COCCUP_CODE ?? string.Empty,
                                                      x.LDEFAULT ? "1" : "0",
                                                      poNewEntity.CCREATE_BY ?? string.Empty,
                                                      poNewEntity.CUPDATE_BY ?? string.Empty);

                                await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);

                                // Increment sequence: Right("000" + (CInt(cSequence) + 100).ToString, 6)
                                // Match net4 line 979 exactly: parse as int, add 100, convert to string, prepend "000", take RIGHT 6 chars
                                int liSeq = int.Parse(cSequence) + 100;
                                string lcTemp = "000" + liSeq.ToString();
                                cSequence = lcTemp.Length >= 6 ? lcTemp.Substring(lcTemp.Length - 6) : lcTemp.PadLeft(6, '0');
                            }
                        }
                    }
                    else
                    {
                        // Insert new supplier info
                        lcCmd = string.Format(" DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY (N'{0}') " +
                                              " INSERT INTO GSM_SUPPLIER_INFO " +
                                              " (CCOMPANY_ID, CSUPPLIER_ID, CINFO_SEQNO, CSUPPLIER_NAME, CADDRESS, " +
                                              " CPOSTAL_CODE, CCITY, CCOUNTRY_CODE, CSTATE_CODE, CPHONE_1, " +
                                              " CPHONE_2, CFAX_NO1, CFAX_NO2, CEMAIL_1, CEMAIL_2, " +
                                              " CTAX_NAME, CTAX_REG_TP, CTAX_REGISTER_ID, DTAX_REGISTER_DATE, CTAX_BUSINESS_TYPE, " +
                                              " CTAX_BUSINESS_NAME, CNPWP, CNPKP,  " +
                                              " CCREATE_BY, DCREATE_DATE, CUPDATE_BY, DUPDATE_DATE) " +
                                              " VALUES " +
                                              " (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', " +
                                              " N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', " +
                                              " N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', " +
                                              " N'{15}', N'{16}', N'{17}', '{18}', N'{19}', " +
                                              " N'{20}', N'{21}', N'{22}', " +
                                              " N'{23}', @DATENOW, N'{24}', @DATENOW) ",
                                              poNewEntity.oSupp.CCOMPANY_ID ?? string.Empty,
                                              poNewEntity.oSupp.CSUPPLIER_ID ?? string.Empty,
                                              poNewEntity.CINFO_SEQNO ?? string.Empty,
                                              poNewEntity.oSupp.CSUPPLIER_NAME ?? string.Empty,
                                              poNewEntity.oSupp.CADDRESS ?? string.Empty,
                                              poNewEntity.oSupp.CPOSTAL_CODE ?? string.Empty,
                                              poNewEntity.oSupp.CCITY ?? string.Empty,
                                              poNewEntity.oSupp.CCOUNTRY_CODE ?? string.Empty,
                                              poNewEntity.oSupp.CSTATE_CODE ?? string.Empty,
                                              poNewEntity.oSupp.CPHONE_1 ?? string.Empty,
                                              poNewEntity.oSupp.CPHONE_2 ?? string.Empty,
                                              poNewEntity.oSupp.CFAX_NO1 ?? string.Empty,
                                              poNewEntity.oSupp.CFAX_NO2 ?? string.Empty,
                                              poNewEntity.oSupp.CEMAIL_1 ?? string.Empty,
                                              poNewEntity.oSupp.CEMAIL_2 ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_NAME ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_REG_TP ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_REGISTER_ID ?? string.Empty,
                                              poNewEntity.oSupp.DTAX_REGISTER_DATE != default(DateTime) ? poNewEntity.oSupp.DTAX_REGISTER_DATE.ToString("yyyy-MM-dd") : string.Empty,
                                              poNewEntity.oSupp.CTAX_BUSINESS_TYPE ?? string.Empty,
                                              poNewEntity.oSupp.CTAX_BUSINESS_NAME ?? string.Empty,
                                              poNewEntity.oSupp.CNPWP ?? string.Empty,
                                              poNewEntity.oSupp.CNPKP ?? string.Empty,
                                              poNewEntity.CCREATE_BY ?? string.Empty,
                                              poNewEntity.CUPDATE_BY ?? string.Empty);
                        _logger.LogInfo($"Insert Supplier Info Query: {lcCmd}");
                        await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);

                        // Insert contact persons
                        string cSequence = "000000";
                        if (poNewEntity.oCP != null)
                        {
                            foreach (var x in poNewEntity.oCP)
                            {
                                lcCmd = string.Format(" DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY (N'{0}') " +
                                                      " INSERT INTO GSM_SUPPLIER_CONTACT " +
                                                      " (CCOMPANY_ID, CSUPPLIER_ID, CINFO_SEQNO, CCONTACT_SEQNO, CFIRST_NAME, CLAST_NAME,  " +
                                                      " CTITLE, COCCUP_CODE, LDEFAULT, CCREATE_BY, DCREATE_DATE, CUPDATE_BY, DUPDATE_DATE) " +
                                                      " VALUES " +
                                                      " (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', " +
                                                      " N'{6}', N'{7}', '{8}', N'{9}', @DATENOW, N'{10}', @DATENOW) ",
                                                      x.CCOMPANY_ID ?? string.Empty,
                                                      x.CSUPPLIER_ID ?? string.Empty,
                                                      poNewEntity.CINFO_SEQNO ?? string.Empty,
                                                      cSequence,
                                                      x.CFIRST_NAME ?? string.Empty,
                                                      x.CLAST_NAME ?? string.Empty,
                                                      x.CTITLE ?? string.Empty,
                                                      x.COCCUP_CODE ?? string.Empty,
                                                      x.LDEFAULT ? "1" : "0",
                                                      poNewEntity.CCREATE_BY ?? string.Empty,
                                                      poNewEntity.CUPDATE_BY ?? string.Empty);

                                await loDb.SqlExecNonQueryAsync(lcCmd, loConn, false);

                                // Increment sequence: Right("000" + (CInt(cSequence) + 100).ToString, 6)
                                // Match net4 line 1018 exactly: parse as int, add 100, convert to string, prepend "000", take RIGHT 6 chars
                                int liSeq = int.Parse(cSequence) + 100;
                                string lcTemp = "000" + liSeq.ToString();
                                cSequence = lcTemp.Length >= 6 ? lcTemp.Substring(lcTemp.Length - 6) : lcTemp.PadLeft(6, '0');
                            }
                        }
                    }
                }

                // Get Exception - Note: R_ExternalException may not be available in NET6, preserve logic
                // loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                _logger.LogInfo(lcCmd);
                _logger.LogDebug(lcCmd);
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
                loDb.R_AddCommandParameter(loCmd, "@CSOFT_PERIOD", DbType.String, 50, poParameter.CSOFT_PERIOD);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.Select(x => new FAT00100GetComboPeriodMonthResultDTO
                    {
                        CPERIOD_NO = x.CPERIOD_NO ?? string.Empty
                    }).ToList();
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
        /// Get asset list (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, status, and update date</param>
        /// <returns>List of asset information</returns>
        public async Task<List<FAT00100GetAssetListResultDTO>> GetAssetListAsync(FAT00100DTO poParameter)
        {
            string lcMethod = nameof(GetAssetListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetAssetListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT a.CASSET_CODE,  " +
                                    " a.CASSET_TRANS_SEQNO,  " +
                                    " a.NTRANSACTION_AMOUNT1,  " +
                                    " a.NLTRANSACTION_AMOUNT1, " +
                                    " a.ITRANSACTION_QTY1,  " +
                                    " a.CUNIT,  " +
                                    " a.CTRANSACTION_DESCR,  " +
                                    " a.CASSET_DEPT_CODE,  " +
                                    " CASSET_DEPT_NAME = ISNULL(c.DESCRIPTION, ''), " +
                                    " a.CASSET_LOCATION, " +
                                    " a.CJRNGRP_CODE,  " +
                                    " CJRNGRP_NAME = ISNULL(e.CDESCRIPTION, d.CJRNGRP_NAME), " +
                                    " a.CTAX_CATEGORY_CODE,  " +
                                    " CTAX_CATEGORY_DESC = ISNULL(g.CDESCRIPTION, f.CTAX_CATEGORY_DESC), " +
                                    " b.CASSET_NAME " +
                                    " FROM FAT_TRANS_ASSET a (nolock)  " +
                                    " INNER JOIN FAM_ASSET b (nolock) " +
                                    " ON b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and b.CASSET_CODE = a.CASSET_CODE " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT','', @CFOREIGN_LANGUAGE) c " +
                                    " ON c.CODE = a.CASSET_DEPT_CODE " +
                                    " LEFT JOIN GSM_JRNGRP_HD d (nolock) " +
                                    " ON d.CCOMPANY_ID = a.CCOMPANY_ID " +
                                    " and d.CJRNGRP_TYPE = '6' " +
                                    " and d.CJRNGRP_CODE = a.CJRNGRP_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE e (nolock) " +
                                    " ON e.CTABLE_NAME = 'GSM_JRNGRP_HD' " +
                                    " and e.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " and e.CKEY_ID = d.CCOMPANY_ID + d.CJRNGRP_TYPE + d.CJRNGRP_CODE " +
                                    " LEFT JOIN FAM_TAX_CATEGORY f (NOLOCK) " +
                                    " ON f.CCOMPANY_ID = a.CCOMPANY_ID " +
                                    " and f.CTAX_CATEGORY_CODE = a.CTAX_CATEGORY_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE g (NOLOCK) " +
                                    " ON g.CTABLE_NAME = 'FAM_TAX_CATEGORY' " +
                                    " and g.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " and g.CKEY_ID = f.CCOMPANY_ID + f.CTAX_CATEGORY_CODE " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO " +
                                    " and b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and b.CASSET_CODE = a.CASSET_CODE " +
                                    " and ((@CSTATUS<'09' and a.LDELETE_FLAG=0) or (@CSTATUS>'08' and a.DUPDATE_DATE=@DUPDATE_DATE)) ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poParameter.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@DUPDATE_DATE", DbType.DateTime, 0, poParameter.DUPDATE_DATE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.Select(x => new FAT00100GetAssetListResultDTO
                    {
                        CASSET_CODE = x.CASSET_CODE ?? string.Empty,
                        CASSET_TRANS_SEQNO = x.CASSET_TRANS_SEQNO ?? string.Empty,
                        NTRANSACTION_AMOUNT1 = x.NTRANSACTION_AMOUNT1,
                        NLTRANSACTION_AMOUNT1 = x.NLTRANSACTION_AMOUNT1,
                        ITRANSACTION_QTY1 = x.ITRANSACTION_QTY1,
                        CUNIT = x.CUNIT ?? string.Empty,
                        CTRANSACTION_DESCR = x.CTRANSACTION_DESCR ?? string.Empty,
                        CASSET_DEPT_CODE = x.CASSET_DEPT_CODE ?? string.Empty,
                        CASSET_DEPT_NAME = x.CASSET_DEPT_NAME ?? string.Empty,
                        CASSET_LOCATION = x.CASSET_LOCATION ?? string.Empty,
                        CJRNGRP_CODE = x.CJRNGRP_CODE ?? string.Empty,
                        CJRNGRP_NAME = x.CJRNGRP_NAME ?? string.Empty,
                        CTAX_CATEGORY_CODE = x.CTAX_CATEGORY_CODE ?? string.Empty,
                        CTAX_CATEGORY_DESC = x.CTAX_CATEGORY_DESC ?? string.Empty,
                        CASSET_NAME = x.CASSET_NAME ?? string.Empty
                    }).ToList();
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
                loCmd.CommandText = " SELECT CDEPT_CODE,  " +
                                    " CREFERENCE_NO,  " +
                                    " CTRANSACTION_DATE,  " +
                                    " CSUPPLIER_NAME = ISNULL(b.CSUPPLIER_NAME, ''),  " +
                                    " CTRANSACTION_PRD,  " +
                                    " CSTATUS_DESC = ISNULL(c.DESCRIPTION, a.CSTATUS) " +
                                    " FROM FAT_TRANS_HD a (nolock) " +
                                    " LEFT JOIN GSM_SUPPLIER b(nolock)  " +
                                    " ON b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " AND b.CSUPPLIER_ID = a.CSUPPLIER_ID " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_TRX_STATUS', '', @CFOREIGN_LANGUAGE) c  " +
                                    " ON c.CODE = a.CSTATUS " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID  " +
                                    " AND CDEPT_CODE = @CDEPT_CODE " +
                                    " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND ((@CREFERENCE_NO <> '' AND CREFERENCE_NO = @CREFERENCE_NO)  " +
                                    " OR (@CREFERENCE_NO = '' AND (@CSUPPLIER_ID = '' or a.CSUPPLIER_ID = @CSUPPLIER_ID)  " +
                                    " AND CTRANSACTION_PRD BETWEEN @CPERIODFROM AND @CPERIODTO AND ((a.CSTATUS = '00' AND @CSTATUSDRAFT = 1) " +
                                    " OR (a.CSTATUS IN('01', '02') AND @CSTATUSOPEN = 1) OR (a.CSTATUS = '03' AND @CSTATUSAPPROVED = 1)  " +
                                    " OR (a.CSTATUS = '08' AND @CSTATUSCLOSED = 1)))) ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, poParameter.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIODFROM", DbType.String, 50, poParameter.CPERIODFROM);
                loDb.R_AddCommandParameter(loCmd, "@CPERIODTO", DbType.String, 50, poParameter.CPERIODTO);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUSDRAFT", DbType.String, 50, poParameter.CSTATUSDRAFT);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUSOPEN", DbType.String, 50, poParameter.CSTATUSOPEN);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUSAPPROVED", DbType.String, 50, poParameter.CSTATUSAPPROVED);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUSCLOSED", DbType.String, 50, poParameter.CSTATUSCLOSED);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.Select(x => new FAT00100GetDataGridResultDTO
                    {
                        CDEPT_CODE = x.CDEPT_CODE ?? string.Empty,
                        CREFERENCE_NO = x.CREFERENCE_NO ?? string.Empty,
                        CTRANSACTION_DATE = x.CTRANSACTION_DATE ?? string.Empty,
                        CSUPPLIER_NAME = x.CSUPPLIER_NAME ?? string.Empty,
                        CTRANSACTION_PRD = x.CTRANSACTION_PRD ?? string.Empty,
                        CSTATUS_DESC = x.CSTATUS_DESC ?? string.Empty
                    }).ToList();
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
        /// Get GSM supplier info (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, supplier ID, and info seqno</param>
        /// <returns>List of supplier info records</returns>
        public async Task<List<FAT00100GetGSM_SUPPLIER_INFOResultDTO>> GetGSM_SUPPLIER_INFOAsync(FAT00100DTO poParameter)
        {
            string lcMethod = nameof(GetGSM_SUPPLIER_INFOAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetGSM_SUPPLIER_INFOResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                string lcCmd = string.Format("SELECT * FROM GSM_SUPPLIER_INFO WHERE CCOMPANY_ID ='{0}' AND CSUPPLIER_ID ='{1}' AND CINFO_SEQNO ='{2}'",
                                              poParameter.CCOMPANY_ID, poParameter.CSUPPLIER_ID, poParameter.CINFO_SEQNO);

                var loRtn = loDb.SqlExecObjectQuery<FAT00100SuppDTO>(lcCmd, loConn, false);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.Select(x => new FAT00100GetGSM_SUPPLIER_INFOResultDTO
                    {
                        CCOMPANY_ID = x.CCOMPANY_ID ?? string.Empty,
                        CSUPPLIER_ID = x.CSUPPLIER_ID ?? string.Empty,
                        CINFO_SEQNO = x.CINFO_SEQNO ?? string.Empty,
                        CSUPPLIER_NAME = x.CSUPPLIER_NAME ?? string.Empty,
                        CADDRESS = x.CADDRESS ?? string.Empty,
                        CPOSTAL_CODE = x.CPOSTAL_CODE ?? string.Empty,
                        CCITY = x.CCITY ?? string.Empty,
                        CCOUNTRY_CODE = x.CCOUNTRY_CODE ?? string.Empty,
                        CSTATE_CODE = x.CSTATE_CODE ?? string.Empty,
                        CPHONE_1 = x.CPHONE_1 ?? string.Empty,
                        CPHONE_2 = x.CPHONE_2 ?? string.Empty,
                        CPHONE_3 = x.CPHONE_3 ?? string.Empty,
                        CFAX_NO1 = x.CFAX_NO1 ?? string.Empty,
                        CFAX_NO2 = x.CFAX_NO2 ?? string.Empty,
                        CFAX_NO3 = x.CFAX_NO3 ?? string.Empty,
                        CEMAIL_1 = x.CEMAIL_1 ?? string.Empty,
                        CEMAIL_2 = x.CEMAIL_2 ?? string.Empty,
                        CEMAIL_3 = x.CEMAIL_3 ?? string.Empty,
                        CTAX_REG_TP = x.CTAX_REG_TP ?? string.Empty,
                        CTAX_NAME = x.CTAX_NAME ?? string.Empty,
                        CTAX_REGISTER_ID = x.CTAX_REGISTER_ID ?? string.Empty,
                        DTAX_REGISTER_DATE = x.DTAX_REGISTER_DATE,
                        CTAX_BUSINESS_TYPE = x.CTAX_BUSINESS_TYPE ?? string.Empty,
                        CTAX_BUSINESS_NAME = x.CTAX_BUSINESS_NAME ?? string.Empty,
                        CNPWP = x.CNPWP ?? string.Empty,
                        CNPKP = x.CNPKP ?? string.Empty,
                        CNOTES = x.CNOTES ?? string.Empty,
                        CCREATE_BY = x.CCREATE_BY ?? string.Empty,
                        DCREATE_DATE = x.DCREATE_DATE,
                        CUPDATE_BY = x.CUPDATE_BY ?? string.Empty,
                        DUPDATE_DATE = x.DUPDATE_DATE
                    }).ToList();
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
        /// Get GSM supplier contact (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, supplier ID, and info seqno</param>
        /// <returns>List of supplier contact records</returns>
        public async Task<List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>> GetGSM_SUPPLIER_CONTACTAsync(FAT00100DTO poParameter)
        {
            string lcMethod = nameof(GetGSM_SUPPLIER_CONTACTAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                string lcCmd = string.Format("SELECT * FROM GSM_SUPPLIER_CONTACT WHERE CCOMPANY_ID ='{0}' AND CSUPPLIER_ID ='{1}' AND CINFO_SEQNO ='{2}'",
                                              poParameter.CCOMPANY_ID, poParameter.CSUPPLIER_ID, poParameter.CINFO_SEQNO);

                var loRtn = loDb.SqlExecObjectQuery<FAT00100CPDTO>(lcCmd, loConn, false);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.Select(x => new FAT00100GetGSM_SUPPLIER_CONTACTResultDTO
                    {
                        CCOMPANY_ID = x.CCOMPANY_ID ?? string.Empty,
                        CSUPPLIER_ID = x.CSUPPLIER_ID ?? string.Empty,
                        CINFO_SEQNO = x.CINFO_SEQNO ?? string.Empty,
                        CCONTACT_SEQNO = x.CCONTACT_SEQNO ?? string.Empty,
                        CFIRST_NAME = x.CFIRST_NAME ?? string.Empty,
                        CLAST_NAME = x.CLAST_NAME ?? string.Empty,
                        CTITLE = x.CTITLE ?? string.Empty,
                        COCCUP_CODE = x.COCCUP_CODE ?? string.Empty,
                        LDEFAULT = x.LDEFAULT,
                        CCREATE_BY = x.CCREATE_BY ?? string.Empty,
                        DCREATE_DATE = x.DCREATE_DATE,
                        CUPDATE_BY = x.CUPDATE_BY ?? string.Empty,
                        DUPDATE_DATE = x.DUPDATE_DATE
                    }).ToList();
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
                    loResult.Data.NLBASE_RATE_AMOUNT = loRtn.NLBASE_RATE_AMOUNT;
                    loResult.Data.NBBASE_RATE_AMOUNT = loRtn.NBBASE_RATE_AMOUNT;
                    loResult.Data.NLCURRENCY_RATE_AMOUNT = loRtn.NLCURRENCY_RATE_AMOUNT;
                    loResult.Data.NBCURRENCY_RATE_AMOUNT = loRtn.NBCURRENCY_RATE_AMOUNT;
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
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
                    loResult.Data.CASSET_CODE = loFirst.CASSET_CODE ?? string.Empty;
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
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
                    loResult.Data.CASSET_CODE = loFirst.CASSET_CODE ?? string.Empty;
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

