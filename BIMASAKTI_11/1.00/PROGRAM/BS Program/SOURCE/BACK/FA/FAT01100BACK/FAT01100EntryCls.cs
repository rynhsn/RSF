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
using FAT01100Back.DTOs;
using FAT01100BackResources;
using FAT01100Common.DTOs;
using R_Storage;
using R_StorageCommon;

namespace FAT01100Back
{
    /// <summary>
    /// Business logic class for FAT01100 - Change Asset Data Transaction operations
    /// Handles R_DisplayAsync, R_SavingAsync, R_DeletingAsync and init/lookup methods via stored procedures.
    /// Implements all methods required by IFAT01100Entry (interface is implemented at Service layer).
    /// </summary>
    public class FAT01100EntryCls : R_BusinessObjectAsync<FAT01100DTO>
    {
        private readonly FAT01100BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT01100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT01100EntryCls()
        {
            _logger = LoggerFAT01100.R_GetInstanceLogger();
            _activitySource = FAT01100Activity.R_GetInstanceActivitySource();
        }

        /// <summary>
        /// Helper method to get error messages from resources
        /// </summary>
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
        /// Delete transaction record via RSP_FA_UPDATE_TRANS_HD_STATUS
        /// </summary>
        protected override async Task R_DeletingAsync(FAT01100DTO poEntity)
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

                R_ExternalException.R_SP_Init_Exception(loConn);

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FA_UPDATE_TRANS_HD_STATUS ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                string lcNewStatus = poEntity.CTRANS_STATUS == "00" ? "99" : "98";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, lcNewStatus);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p =>
                {
                    string lcValue = p.Value?.ToString() ?? "NULL";
                    if (p.DbType == DbType.String || p.DbType == DbType.StringFixedLength || p.DbType == DbType.AnsiString || p.DbType == DbType.AnsiStringFixedLength ||
                        p.DbType == DbType.Boolean || p.Value is bool || p.Value is string)
                    {
                        return $" {p.ParameterName} ='{lcValue}'";
                    }
                    return $" {p.ParameterName} ={lcValue}";
                })));

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
        /// Display transaction record via RSP_FAT01100_GET_TRANS_DETAIL
        /// </summary>
        protected override async Task<FAT01100DTO> R_DisplayAsync(FAT01100DTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            FAT01100DTO loRtn = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "RSP_FAT01100_GET_TRANS_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poEntity.CLANG_ID ?? string.Empty);

                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT01100DTO>(loRtnDataTable);
                loRtn = loRtnList.FirstOrDefault() ?? new FAT01100DTO();
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
        /// Save transaction record via RSP_FAT01100_SAVE_TRANS
        /// </summary>
        protected override async Task R_SavingAsync(FAT01100DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            string lcMethod = nameof(R_SavingAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            string lcQuery = "RSP_FAT01100_SAVE_TRANS";

            try
            {
                string lcAction = peCRUDMode == eCRUDMode.AddMode ? "NEW" : "EDIT";

                string lcRefDate = string.Empty;
                if (poNewEntity.DREF_DATE != default(DateTime))
                {
                    lcRefDate = poNewEntity.DREF_DATE.ToString("yyyyMMdd");
                }
                else if (!string.IsNullOrWhiteSpace(poNewEntity.CREF_DATE) && poNewEntity.CREF_DATE.Length >= 8)
                {
                    lcRefDate = poNewEntity.CREF_DATE.Substring(0, 8);
                }

                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                loCmd.Parameters.Clear();
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQ_NO", DbType.String, 6, poNewEntity.CASSET_TRANS_SEQ_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, lcRefDate);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 200, poNewEntity.CTRANS_DESC ?? string.Empty);

                loDb.R_AddCommandParameter(loCmd, "@CASSET_NAME_OLD", DbType.String, 100, poNewEntity.CASSET_NAME_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_NAME", DbType.String, 100, poNewEntity.CASSET_NAME ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE_OLD", DbType.String, 20, poNewEntity.CASSET_DEPT_CODE_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 20, poNewEntity.CASSET_DEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE_OLD", DbType.String, 20, poNewEntity.CJRNGRP_CODE_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 20, poNewEntity.CJRNGRP_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID_OLD", DbType.String, 20, poNewEntity.CCATEGORY_ID_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 20, poNewEntity.CCATEGORY_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_ID_OLD", DbType.String, 20, poNewEntity.CTAX_CATEGORY_ID_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_ID", DbType.String, 20, poNewEntity.CTAX_CATEGORY_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@IQTY_OLD", DbType.Int32, 0, poNewEntity.IQTY_OLD);
                loDb.R_AddCommandParameter(loCmd, "@IQTY", DbType.Int32, 0, poNewEntity.IQTY);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_OLD", DbType.String, 30, poNewEntity.CUNIT_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, 30, poNewEntity.CUNIT ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_OWNER_OLD", DbType.String, 50, poNewEntity.CASSET_OWNER_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_OWNER", DbType.String, 50, poNewEntity.CASSET_OWNER ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSERIAL_NO_OLD", DbType.String, 30, poNewEntity.CSERIAL_NO_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSERIAL_NO", DbType.String, 30, poNewEntity.CSERIAL_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_DESC_OLD", DbType.String, 300, poNewEntity.CASSET_DESC_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_DESC", DbType.String, 300, poNewEntity.CASSET_DESC ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID_OLD", DbType.String, 50, poNewEntity.CSTORAGE_ID_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 50, poNewEntity.CSTORAGE_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD_OLD", DbType.String, 20, poNewEntity.CDEPR_METHOD_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 20, poNewEntity.CDEPR_METHOD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE_OLD", DbType.String, 8, poNewEntity.CSTART_DATE_OLD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIFE_YY_OLD", DbType.Int32, 0, poNewEntity.IUSEFUL_LIFE_YY_OLD);
                loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIFE_MM_OLD", DbType.Int32, 0, poNewEntity.IUSEFUL_LIFE_MM_OLD);
                loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIFE_YY", DbType.Int32, 0, poNewEntity.IUSEFUL_LIFE_YY);
                loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIFE_MM", DbType.Int32, 0, poNewEntity.IUSEFUL_LIFE_MM);
                loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_PCT_OLD", DbType.Decimal, 0, poNewEntity.NYEAR_DEPR_PCT_OLD);
                loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_PCT", DbType.Decimal, 0, poNewEntity.NYEAR_DEPR_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NBOOK_VALUE", DbType.Decimal, 19, poNewEntity.NBOOK_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@NLBOOK_VALUE", DbType.Decimal, 19, poNewEntity.NLBOOK_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@NBBOOK_VALUE", DbType.Decimal, 19, poNewEntity.NBBOOK_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@NRESIDUAL_VALUE_OLD", DbType.Decimal, 19, poNewEntity.NRESIDUAL_VALUE_OLD);
                loDb.R_AddCommandParameter(loCmd, "@NLRESIDUAL_VALUE_OLD", DbType.Decimal, 19, poNewEntity.NLRESIDUAL_VALUE_OLD);
                loDb.R_AddCommandParameter(loCmd, "@NBRESIDUAL_VALUE_OLD", DbType.Decimal, 19, poNewEntity.NBRESIDUAL_VALUE_OLD);
                loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_OLD", DbType.Decimal, 19, poNewEntity.NYEAR_DEPR_OLD);
                loDb.R_AddCommandParameter(loCmd, "@NLYEAR_DEPR_OLD", DbType.Decimal, 19, poNewEntity.NLYEAR_DEPR_OLD);
                loDb.R_AddCommandParameter(loCmd, "@NBYEAR_DEPR_OLD", DbType.Decimal, 19, poNewEntity.NBYEAR_DEPR_OLD);
                loDb.R_AddCommandParameter(loCmd, "@NRESIDUAL_VALUE", DbType.Decimal, 19, poNewEntity.NRESIDUAL_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR", DbType.Decimal, 19, poNewEntity.NYEAR_DEPR);
                loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 20, poNewEntity.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 20, poNewEntity.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 20, poNewEntity.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 20, poNewEntity.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE ?? string.Empty);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p =>
                {
                    string lcValue = p.Value?.ToString() ?? "NULL";
                    if (p.DbType == DbType.String || p.DbType == DbType.StringFixedLength || p.DbType == DbType.AnsiString || p.DbType == DbType.AnsiStringFixedLength ||
                        p.DbType == DbType.Boolean || p.Value is bool || p.Value is string)
                    {
                        return $" {p.ParameterName} ='{lcValue}'";
                    }
                    return $" {p.ParameterName} ={lcValue}";
                })));

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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

        #endregion

        // add new method for get data from stored procedure RSP_FA_GET_ASSET 

        public async Task<FAT01100ResultDTO<FAT01100GetAssetResultDTO>> FAT01100GetAsset(FAT01100GetAssetParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetAsset);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<FAT01100GetAssetResultDTO> { Data = new FAT01100GetAssetResultDTO() };
            R_ReadParameter loReadParameter = null;
            R_ReadResult loReadResult = null;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_FA_GET_ASSET ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.@CLANGUAGE_ID);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GetAssetResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    if (string.IsNullOrEmpty(loRtn.CSTORAGE_ID) == false)
                    {
                        loReadParameter = new R_ReadParameter()
                        {
                            StorageId = loRtn.CSTORAGE_ID
                        };

                        loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                        loRtn.OASSET_IMAGE= loReadResult.Data;
                        //loRtn.CFILE= loReadResult.FileExtension;
                        //loRtn.CFILE_NAME = loReadResult.FileName;
                        //loResult.Data.CFILE_NAME_EXTENSION = loReadResult.FileName + loReadResult.FileExtension;
                    }
                }

                if (loRtn != null)
                    loResult.Data = loRtn;


            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        #region IFAT01100Entry init/lookup

        /// <summary>RSP_GS_GET_COMPANY_INFO</summary>
        public async Task<FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO>> FAT01100GetCompanyInfo(FAT01100GetCompanyInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetCompanyInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO> { Data = new FAT01100GetCompanyInfoResultDTO() };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GetCompanyInfoResultDTO>(loDataTable).FirstOrDefault();
                if (loRtn != null)
                    loResult.Data = loRtn;
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        /// <summary>RSP_FA_GET_SYSTEM_PARAM</summary>
        public async Task<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>> FAT01100GetGetSystemParam(FAT01100GetGetSystemParamParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetGetSystemParam);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO> { Data = new FAT01100GetGetSystemParamResultDTO() };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_FA_GET_SYSTEM_PARAM";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParameter.CLANGUAGE_ID);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GetGetSystemParamResultDTO>(loDataTable).FirstOrDefault();
                loResult.Data = loRtn;
                //if (loRtn != null)
                //    loResult.Data = loRtn;
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        /// <summary>RSP_GS_GET_PERIOD_DT_INFO</summary>
        public async Task<FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO>> FAT01100GetPeriodeDtInfo(FAT01100GetPeriodeDtInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetPeriodeDtInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO> { Data = new FAT01100GetPeriodeDtInfoResultDTO() };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_GS_GET_PERIOD_DT_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 2, poParameter.CPERIOD_NO);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GetPeriodeDtInfoResultDTO>(loDataTable).FirstOrDefault();
                if (loRtn != null)
                    loResult.Data = loRtn;
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        /// <summary>RSP_GS_GET_CURRENCY_LIST (streaming) - follow back_streaming_method_pattern</summary>
        public async Task<List<FAT01100GetCurrencyListResultDTO>> GetCurrencyList(FAT01100GetCurrencyListParameterDTO poParameter)
        {
            string lcMethod = nameof(GetCurrencyList);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<FAT01100GetCurrencyListResultDTO> loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName!, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT01100GetCurrencyListResultDTO>(loDataTable).ToList();
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

        /// <summary>RSP_GS_GET_DEPT_LOOKUP_LIST (streaming) - follow back_streaming_method_pattern</summary>
        public async Task<List<FAT01100GetDeptLookupListResultDTO>> FAT01100GetDeptLookupList(FAT01100GetDeptLookupListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetDeptLookupList);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<FAT01100GetDeptLookupListResultDTO> loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "RSP_GS_GET_DEPT_LOOKUP_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, 20, poParameter.CPROGRAM_ID ?? string.Empty);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName!, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT01100GetDeptLookupListResultDTO>(loDataTable).ToList();
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

        /// <summary>RSP_GS_GET_TRANS_CODE_INFO</summary>
        public async Task<FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO>> FAT01100GetTransCodeInfo(FAT01100GetTransCodeInfoParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetTransCodeInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO> { Data = new FAT01100GetTransCodeInfoResultDTO() };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_GS_GET_TRANS_CODE_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GetTransCodeInfoResultDTO>(loDataTable).FirstOrDefault();
                if (loRtn != null)
                    loResult.Data = loRtn;
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        /// <summary>RSP_GS_GET_PERIOD_YEAR_RANGE</summary>
        public async Task<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>> FAT01100GetYearRange(FAT01100GetYearRangeParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetYearRange);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<FAT01100GetYearRangeResultDTO> { Data = new FAT01100GetYearRangeResultDTO() };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParameter.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, poParameter.CMODE ?? string.Empty);

                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GetYearRangeResultDTO>(loDataTable).FirstOrDefault();
                if (loRtn != null)
                    loResult.Data = loRtn;
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        /// <summary>RSP_GS_GET_LAST_CURRENCY_RATE</summary>
        public async Task<FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO>> FAT01100GetLastCurrencyRate(FAT01100GetLastCurrencyRateParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GetLastCurrencyRate);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO> { Data = new FAT01100GetLastCurrencyRateResultDTO() };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_GS_GET_LAST_CURRENCY_RATE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 10, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 10, poParameter.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, 10, poParameter.CRATE_DATE);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GetLastCurrencyRateResultDTO>(loDataTable).FirstOrDefault();
                if (loRtn != null)
                    loResult.Data = loRtn;
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        /// <summary>RSP_FA_UPDATE_TRANS_HD_STATUS</summary>
        public async Task<FAT01100ResultDTO<object>> FAT01100UpdateTransHdStatus(FAT01100UpdateTransHdStatusParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100UpdateTransHdStatus);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<object> { Data = null };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_FA_UPDATE_TRANS_HD_STATUS ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, poParameter.CNEW_STATUS);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        /// <summary>RSP_FAT01100_SUBMIT_TRANS</summary>
        public async Task<FAT01100ResultDTO<object>> FAT01100SubmitTrans(FAT01100SubmitTransParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100SubmitTrans);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT01100ResultDTO<object> { Data = null };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_FAT01100_SUBMIT_TRANS ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex) { loEx.Add(ex); _logger.LogError(loEx); }
            finally { if (loDb != null) loDb = null; }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        #endregion

        private async Task<FAT01100ImageStorageTypeDTO> GetStorageType()
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            FAT01100ImageStorageTypeDTO loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter}", loDbParam);

                    var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<FAT01100ImageStorageTypeDTO>(loDataTable).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        private async Task<FAT01100DTO> SetStorageID(FAT01100DTO poNewEntity, FAT01100ImageStorageTypeDTO poStorageType)
        {
            using Activity activity = _activitySource.StartActivity("SetStorageID");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            R_SaveResult loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loConnAttr = loDb.GetConnectionAttribute();

                //Set Storage Type
                R_EStorageType loStorageType;
                loStorageType = poStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;

                R_EProviderForCloudStorage loProvider;
                loProvider = poStorageType.CSTORAGE_PROVIDER_ID.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                //Add and create Storage ID
                R_AddParameter loAddParameter;

                loAddParameter = new R_AddParameter()
                {
                    StorageType = loStorageType,
                    ProviderCloudStorage = loProvider,
                    FileName = poNewEntity.CPROPERTY_ID,
                    FileExtension = poNewEntity.CPROPERTY_ID,
                    UploadData = poNewEntity.OASSET_IMAGE,
                    UserId = poNewEntity.CUSER_ID,
                    BusinessKeyParameter = new R_BusinessKeyParameter()
                    {
                        CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                        CDATA_TYPE = "STORAGE_DATA_TABLE",
                        CKEY01 = poNewEntity.CREC_ID,
                    }
                };
                loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                //Set Storage ID CSTORAGE_ID
                poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return poNewEntity;
        }
    }
}
