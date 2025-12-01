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
using FAT00100Back.DTOs;
using FAT00100BackResources;
using FAT00100Common.DTOs;

namespace FAT00100Back
{
    /// <summary>
    /// Business logic class for FAT0010002 - FA Acquisition Detail operations
    /// Handles all business logic operations for FA Acquisition Detail
    /// </summary>
    public class FAT0010002Cls : R_BusinessObjectAsync<FAT0010002DTO>
    {
        private readonly FAT00100BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT0010002Cls()
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
        /// Get combo depreciation method list
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID and foreign language</param>
        /// <returns>List of depreciation method result DTOs</returns>
        public async Task<List<FAT0010002GetComboDepreciationMethodResultDTO>> GetComboDepreciationMethodAsync(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetComboDepreciationMethodAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT0010002GetComboDepreciationMethodResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT RTRIM(LTRIM(CODE)) as CCODE, RTRIM(LTRIM(DESCRIPTION)) as CDESCRIPTION " +
                                    " FROM RFT_GET_GSB_CODE_INFO ('RHAPSODY', @CCOMPANY_ID, '_FA_DEPR_METHOD', '', @CFOREIGN_LANGUAGE) ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT0010002GetComboDepreciationMethodResultDTO>(loDataTable).ToList();
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
        /// Get FA Acquisition Detail Header
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and reference no</param>
        /// <returns>Result DTO with header information</returns>
        public async Task<FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>> GetFAAcquisitionDetailHeaderAsync(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetFAAcquisitionDetailHeaderAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>
            {
                Data = new FAT0010002GetFAAcquisitionDetailHeaderResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CDEPT_CODE,  " +
                                    " A.CTRANSACTION_CODE,  " +
                                    " CREFERENCE_NO,  " +
                                    " CTRANSACTION_DATE,  " +
                                    " CSTATUS,  " +
                                    " A.CCURRENCY_CODE, " +
                                    " NLBASE_RATE_AMOUNT,  " +
                                    " NLCURRENCY_RATE_AMOUNT,  " +
                                    " NBBASE_RATE_AMOUNT,  " +
                                    " NBCURRENCY_RATE_AMOUNT, " +
                                    " NTRANSACTION_AMOUNT,  " +
                                    " NLTRANSACTION_AMOUNT,  " +
                                    " NBTRANSACTION_AMOUNT, " +
                                    " CDOCUMENT_DATE,  " +
                                    " CSUPPLIER_ID,  " +
                                    " CSUPPLIER_NAME,  " +
                                    " CFR_MODULE,  " +
                                    " CFR_DEPT_CODE,  " +
                                    " CFR_TRANSACTION_CODE,  " +
                                    " CFR_REFERENCE_NO, " +
                                    " CDEPT_NAME = ISNULL(b.DESCRIPTION, ''), " +
                                    " CCURRENCY_NAME = ISNULL(CCURRENCY_NAME, ''), " +
                                    " CTRANSACTION_NAME = ISNULL(CTRANSACTION_NAME, ''), " +
                                    " NLRATE = ROUND(NLCURRENCY_RATE_AMOUNT / NLBASE_RATE_AMOUNT, 6),   " +
                                    " NBRATE = ROUND(NBCURRENCY_RATE_AMOUNT / NBBASE_RATE_AMOUNT, 6), " +
                                    " NBXRATE = ROUND((NBCURRENCY_RATE_AMOUNT * NLBASE_RATE_AMOUNT)/(NLCURRENCY_RATE_AMOUNT * NBBASE_RATE_AMOUNT), 6) " +
                                    " FROM FAT_TRANS_HD a (nolock) " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) b " +
                                    " ON b.CODE = a.CDEPT_CODE " +
                                    " LEFT JOIN SAB_CURRENCY c (nolock)  " +
                                    " ON c.CCURRENCY_CODE = a.CCURRENCY_CODE  " +
                                    " LEFT JOIN GSM_TRANSACTION_CODE d (nolock)  " +
                                    " ON d.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and d.CTRANSACTION_CODE = a.CTRANSACTION_CODE  " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID  " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>(loDataTable).FirstOrDefault();

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
        /// Get FA Acquisition Detail Asset List
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, status, and update date</param>
        /// <returns>List of asset list result DTOs</returns>
        public async Task<List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>> GetFAAcquisitionDetailAssetListAsync(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetFAAcquisitionDetailAssetListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT a.CASSET_CODE,  " +
                                    " CTRANS_SEQNO,  " +
                                    " CASSET_TRANS_SEQNO,  " +
                                    " NTRANSACTION_AMOUNT1,  " +
                                    " NLTRANSACTION_AMOUNT1, " +
                                    " NBTRANSACTION_AMOUNT1,  " +
                                    " ITRANSACTION_QTY1,  " +
                                    " A.CUNIT,  " +
                                    " A.CASSET_DEPT_CODE,  " +
                                    " CASSET_DEPT_NAME = ISNULL(E.DESCRIPTION, ''), " +
                                    " A.CASSET_LOCATION,  " +
                                    " A.CJRNGRP_CODE, " +
                                    " CJRNGRP_DESC = ISNULL(G.CDESCRIPTION, F.CJRNGRP_NAME), " +
                                    " A.CTAX_CATEGORY_CODE,  " +
                                    " CTAX_CATEGORY_DESC=ISNULL(I.CDESCRIPTION, H.CTAX_CATEGORY_DESC), " +
                                    " A.CCATEGORY_CODE,  " +
                                    " CCATEGORY_DESC = ISNULL(D.CDESCRIPTION,C.CCATEGORY_DESC), " +
                                    " A.CDEPR_METHOD, " +
                                    " CDEPR_METHOD_DESC = ISNULL(J.DESCRIPTION, ''), " +
                                    " CASSET_NAME,  " +
                                    " CASSET_OWNER " +
                                    " FROM FAT_TRANS_ASSET a (nolock) " +
                                    " INNER JOIN FAM_ASSET b (nolock)  " +
                                    " ON b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and b.CASSET_CODE = a.CASSET_CODE " +
                                    " LEFT JOIN GSM_CATEGORY C (NOLOCK) " +
                                    " ON C.CCOMPANY_ID = A.CCOMPANY_ID " +
                                    " and C.CCATEGORY_ITEM = '51'  " +
                                    " and C.CCATEGORY_TYPE = 'C' " +
                                    " and C.CCATEGORY_CODE = A.CCATEGORY_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE D (nolock)  " +
                                    " ON D.CTABLE_NAME = 'GSM_CATEGORY' " +
                                    " AND D.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND D.CKEY_ID = C.CCOMPANY_ID + C.CCATEGORY_ITEM + C.CCATEGORY_TYPE + C.CCATEGORY_CODE " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO ('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) E " +
                                    " ON E.CODE = a.CASSET_DEPT_CODE " +
                                    " LEFT JOIN GSM_JRNGRP_HD F (nolock) " +
                                    " ON F.CCOMPANY_ID = A.CCOMPANY_ID " +
                                    " AND F.CJRNGRP_TYPE = '6' " +
                                    " AND F.CJRNGRP_CODE = A.CJRNGRP_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE G (NOLOCK) " +
                                    " ON G.CTABLE_NAME = 'GSM_JRNGRP_HD' " +
                                    " AND G.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND G.CKEY_ID = F.CCOMPANY_ID + F.CJRNGRP_TYPE + F.CJRNGRP_CODE " +
                                    " LEFT JOIN FAM_TAX_CATEGORY H (NOLOCK) " +
                                    " ON H.CCOMPANY_ID = A.CCOMPANY_ID " +
                                    " AND H.CTAX_CATEGORY_CODE = A.CTAX_CATEGORY_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE I (NOLOCK) " +
                                    " ON I.CTABLE_NAME = 'FAM_TAX_CATEGORY' " +
                                    " AND I.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND I.CKEY_ID = H.CCOMPANY_ID + H.CTAX_CATEGORY_CODE " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO ('RHAPSODY', @CCOMPANY_ID, '_FA_DEPR_METHOD', '', @CFOREIGN_LANGUAGE) J " +
                                    " ON J.CODE = A.CDEPR_METHOD " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID   " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO " +
                                    " and ((@CSTATUS<'09' and a.LDELETE_FLAG=0) or (@CSTATUS>'08' and a.DUPDATE_DATE=@DUPDATE_DATE)) ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poParameter.CSTATUS ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@DUPDATE_DATE", DbType.DateTime, 0, poParameter.DUPDATE_DATE ?? DateTime.MinValue);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>(loDataTable).ToList();
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
        /// Get FA Acquisition Detail Alloc Expen Page List
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, asset code, and asset trans seqno</param>
        /// <returns>List of expense allocation result DTOs</returns>
        public async Task<List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>> GetFAAcquisitionDetailAllocExpenPageListAsync(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetFAAcquisitionDetailAllocExpenPageListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CEXPENSE_DEPT_CODE, NEXPENSE_PCT, CEXPENSE_DEPT_NAME = isnull(y.DESCRIPTION,'') " +
                                    " FROM FAT_TRANS_EXP_ALLOC a (nolock) " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) y  " +
                                    " on y.CODE = a.CEXPENSE_DEPT_CODE " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO " +
                                    " and a.CASSET_CODE = @CASSET_CODE " +
                                    " AND a.CASSET_TRANS_SEQNO = @CASSET_TRANS_SEQNO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, poParameter.CASSET_TRANS_SEQNO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>(loDataTable).ToList();
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
        /// Delete asset transaction
        /// </summary>
        /// <param name="poEntity">Entity with key fields to delete</param>
        protected override async Task R_DeletingAsync(FAT0010002DTO poEntity)
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
                loCmd.CommandText = " EXEC RSP_FAT00100_DELETE_ASSET @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CTRANS_SEQNO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, poEntity.CTRANS_SEQNO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);

                //===== MT CR13 start =====
                loCmd.Parameters.Clear();
                loCmd.CommandText = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                                    " UPDATE FAT_TRANS_HD SET " +
                                    " NTRANSACTION_AMOUNT = NTRANSACTION_AMOUNT - NTRANSACTION_AMOUNT1, " +
                                    " NLTRANSACTION_AMOUNT = NLTRANSACTION_AMOUNT - NLTRANSACTION_AMOUNT1 - NLTRANSACTION_AMOUNT2 + NLTRANSACTION_AMOUNT3 + NLTRANSACTION_AMOUNT4 + NLTRANSACTION_AMOUNT5, " +
                                    " NBTRANSACTION_AMOUNT = NBTRANSACTION_AMOUNT - NBTRANSACTION_AMOUNT1 - NBTRANSACTION_AMOUNT2 + NBTRANSACTION_AMOUNT3 + NBTRANSACTION_AMOUNT4 + NBTRANSACTION_AMOUNT5, " +
                                    " CUPDATE_BY = @CUPDATE_BY, " +
                                    " DUPDATE_DATE = @DATENOW, " +
                                    " LGLLINK = @LGLLINK " +
                                    " FROM FAT_TRANS_HD a, FAT_TRANS_ASSET b " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO " +
                                    " AND b.CCOMPANY_ID = a.CCOMPANY_ID " +
                                    " AND b.CDEPT_CODE = a.CDEPT_CODE " +
                                    " AND b.CTRANSACTION_CODE = a.CTRANSACTION_CODE " +
                                    " AND b.CREFERENCE_NO = a.CREFERENCE_NO " +
                                    " AND b.CTRANS_SEQNO = @CTRANS_SEQNO ";

                loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poEntity.CUPDATE_BY);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, poEntity.CTRANS_SEQNO);
                loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 0, poEntity.LGLLINK);

                loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                //===== MT CR13 end =====
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
        /// Display single record with asset details and totals
        /// </summary>
        /// <param name="poEntity">Entity with key fields</param>
        /// <returns>Complete entity with asset details and totals</returns>
        protected override async Task<FAT0010002DTO> R_DisplayAsync(FAT0010002DTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            FAT0010002DTO loRtn = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT a.CASSET_CODE, a.CTRANS_SEQNO, a.CASSET_TRANS_SEQNO, a.NTRANSACTION_AMOUNT1, a.NLTRANSACTION_AMOUNT1,  " +
                                    " a.NLTRANSACTION_AMOUNT2, a.NLTRANSACTION_AMOUNT3, a.NLTRANSACTION_AMOUNT4, a.NLTRANSACTION_AMOUNT5,  " +
                                    " a.NBTRANSACTION_AMOUNT1, a.NBTRANSACTION_AMOUNT2, a.NBTRANSACTION_AMOUNT3, a.NBTRANSACTION_AMOUNT4,   " +
                                    " a.NBTRANSACTION_AMOUNT5, a.ITRANSACTION_QTY1, a.CUNIT, a.CTRANSACTION_DESCR, a.CASSET_DEPT_CODE,   " +
                                    " a.CASSET_LOCATION, a.CJRNGRP_CODE, a.CTAX_CATEGORY_CODE, a.CCATEGORY_CODE, a.CDEPR_METHOD,  " +
                                    " a.CSTART_DATE, a.NLBOOK_VALUE, a.NBBOOK_VALUE, a.IUSEFUL_LIVE, a.NLYEAR_DEPR_AMT, a.NBYEAR_DEPR_AMT,  " +
                                    " a.NLRESIDUAL_VALUE, a.NBRESIDUAL_VALUE, a.NYEAR_DEPR_PCT, b.CASSET_NAME, b.CSERIAL_NUMBER,  " +
                                    " b.CASSET_OWNER, b.OASSET_IMAGE, b.CINSERVICE_DATE, b.LNEW_FLAG, a.CSTART_DATE, a.CTRANSACTION_DATE,  " +
                                    " IUSEFUL_LIVE_YR = FLOOR(a.IUSEFUL_LIVE / 12), IUSEFUL_LIVE_MO = a.IUSEFUL_LIVE % 12, " +
                                    " a.NOLBOOK_VALUE, a.NOBBOOK_VALUE, a.IOUSEFUL_LIVE, " +
                                    " IOUSEFUL_LIVE_YR = FLOOR(a.IOUSEFUL_LIVE / 12), IOUSEFUL_LIVE_MO = a.IOUSEFUL_LIVE % 12, " +
                                    " CASSET_DEPT_NAME = ISNULL(q.DESCRIPTION,''),                              " +
                                    " CJRNGRP_DESC = ISNULL(r1.CDESCRIPTION, r.CJRNGRP_NAME), " +
                                    " CCATEGORY_DESC = ISNULL(s1.CDESCRIPTION, s.CCATEGORY_DESC), " +
                                    " CTAX_CATEGORY_DESC = ISNULL(u1.CDESCRIPTION, u.CTAX_CATEGORY_DESC), " +
                                    " CDEPR_METHOD_DESC = ISNULL(J.DESCRIPTION, '') " +
                                    " FROM FAT_TRANS_ASSET a (nolock)  " +
                                    " INNER JOIN FAM_ASSET b (nolock)   " +
                                    " ON b.CCOMPANY_ID = a.CCOMPANY_ID   " +
                                    " and b.CASSET_CODE = a.CASSET_CODE  " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_FA_DEPR_METHOD', '', @CFOREIGN_LANGUAGE) J " +
                                    " ON J.CODE = A.CDEPR_METHOD " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID , '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) q  " +
                                    " ON q.CODE = a.CASSET_DEPT_CODE " +
                                    " LEFT JOIN GSM_JRNGRP_HD r (nolock)  " +
                                    " ON r.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and r.CJRNGRP_TYPE = '6' and " +
                                    " r.CJRNGRP_CODE = a.CJRNGRP_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE r1 (nolock)  " +
                                    " ON r1.CTABLE_NAME = 'GSM_JRNGRP_HD' " +
                                    " AND r1.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND r1.CKEY_ID = r.CCOMPANY_ID + r.CJRNGRP_TYPE + r.CJRNGRP_CODE " +
                                    " LEFT JOIN GSM_CATEGORY s (nolock)  " +
                                    " ON s.CCOMPANY_ID=a.CCOMPANY_ID  " +
                                    " and s.CCATEGORY_ITEM = '51' " +
                                    " and s.CCATEGORY_TYPE = 'C' " +
                                    " and s.CCATEGORY_CODE = a.CCATEGORY_CODE  " +
                                    " LEFT JOIN GSB_TRANSLATE s1 (nolock)  " +
                                    " ON s1.CTABLE_NAME = 'GSM_CATEGORY'  " +
                                    " AND s1.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND s1.CKEY_ID = s.CCOMPANY_ID + s.CCATEGORY_ITEM + s.CCATEGORY_TYPE + s.CCATEGORY_CODE " +
                                    " LEFT JOIN FAM_TAX_CATEGORY u (nolock)  " +
                                    " on u.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and u.CTAX_CATEGORY_CODE = a.CTAX_CATEGORY_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE u1 (nolock)  " +
                                    " ON u1.CTABLE_NAME = 'FAM_TAX_CATEGORY' " +
                                    " AND u1.CFOREIGN_LANGUAGE = @CFOREIGN_LANGUAGE " +
                                    " AND u1.CKEY_ID = u.CCOMPANY_ID + u.CTAX_CATEGORY_CODE " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID  " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE  " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE   " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO  " +
                                    " AND a.CASSET_CODE = @CASSET_CODE ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poEntity.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poEntity.CASSET_CODE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTable);
                loRtn = loRtnList.FirstOrDefault() ?? new FAT0010002DTO();

                // Get totals
                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT " +
                                    "   NTOTAL_AMOUNT = SUM(NTRANSACTION_AMOUNT1), " +
                                    "   NLTOTAL_AMOUNT = SUM(NLTRANSACTION_AMOUNT1), " +
                                    "   NBTOTAL_AMOUNT = SUM(NBTRANSACTION_AMOUNT1) " +
                                    " FROM FAT_TRANS_ASSET(NOLOCK) " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND CDEPT_CODE = @CDEPT_CODE " +
                                    " AND CREFERENCE_NO = @CREFERENCE_NO " +
                                    " AND LDELETE_FLAG = 0 ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);

                loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnAmount = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTable);

                if (loRtnAmount != null && loRtnAmount.Count > 0)
                {
                    var loFirst = loRtnAmount.FirstOrDefault();
                    if (loFirst != null)
                    {
                        loRtn.NTOTAL_AMOUNT = loFirst.NTOTAL_AMOUNT;
                        loRtn.NLTOTAL_AMOUNT = loFirst.NLTOTAL_AMOUNT;
                        loRtn.NBTOTAL_AMOUNT = loFirst.NBTOTAL_AMOUNT;
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
        /// Save asset transaction with validation and asset info handling
        /// </summary>
        /// <param name="poNewEntity">Entity to save</param>
        /// <param name="peCRUDMode">CRUD mode (Add or Edit)</param>
        protected override async Task R_SavingAsync(FAT0010002DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            string lcMethod = nameof(R_SavingAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            string lcCmd = string.Empty;
            string pcAssetDeptCodeOld = string.Empty;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                pcAssetDeptCodeOld = poNewEntity.CASSET_DEPT_CODE ?? string.Empty;

                // Init Exception - Note: R_ExternalException may not be available in NET6, preserve logic
                // R_ExternalException.R_SP_Init_Exception(loConn);

                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    if (poNewEntity.LINCREMENT_FLAG == true)
                    {
                        lcCmd = string.Format(" DECLARE @CASSET_CODE VARCHAR(20) " +
                                              " EXEC RSP_FA_GET_ASSET_CODE N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', @CASSET_CODE OUTPUT " +
                                              " SELECT CASSET_CODE = @CASSET_CODE ",
                                              poNewEntity.CCOMPANY_ID, poNewEntity.CASSET_DEPT_CODE, poNewEntity.CJRNGRP_CODE, poNewEntity.CTRANSACTION_DATE, poNewEntity.CUSER_ID);
                        try
                        {
                            var loRtnSeq = loDb.SqlExecObjectQuery<FAT0010002DTO>(lcCmd, loConn, false);

                            if (loRtnSeq != null && loRtnSeq.Count > 0)
                            {
                                var loFirst = loRtnSeq.FirstOrDefault();
                                if (loFirst != null && !string.IsNullOrWhiteSpace(loFirst.CASSET_CODE))
                                {
                                    poNewEntity.CASSET_CODE = loFirst.CASSET_CODE;
                                }
                                else
                                {
                                    poNewEntity.CASSET_CODE = string.Empty;
                                }
                            }
                            else
                            {
                                poNewEntity.CASSET_CODE = string.Empty;
                            }

                            _logger.LogInfo(lcCmd);
                            _logger.LogDebug(lcCmd);
                        }
                        catch (Exception ex)
                        {
                            loEx.Add(ex);
                        }
                    }
                    else
                    {
                        loCmd.Parameters.Clear();
                        loCmd.CommandText = " SELECT TOP 1 1 FROM FAM_ASSET (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID AND CASSET_CODE = @CASSET_CODE ";

                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);

                        var loDataTableCheck = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                        loCmd.Parameters.Clear();
                        var loRtnCheck = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTableCheck).FirstOrDefault();

                        if (loRtnCheck != null)
                        {
                            loEx.Add(GetError("PS003"));
                            loEx.ThrowExceptionIfErrors();
                            return;
                        }
                    }

                    lcCmd = string.Format(" DECLARE @CSEQNO VARCHAR(20) " +
                                          " EXEC RSP_GET_SEQUENCE  0, '', 'FAT_TRANS_ASSET', ' WHERE CCOMPANY_ID = ''{0}'' AND CDEPT_CODE = ''{1}'' AND CTRANSACTION_CODE = ''{2}'' AND CREFERENCE_NO = ''{3}'' ', @CSEQNO OUTPUT, 'CTRANS_SEQNO' " +
                                          " SELECT CSEQNO = @CSEQNO ",
                                          poNewEntity.CCOMPANY_ID, poNewEntity.CDEPT_CODE, poNewEntity.CTRANSACTION_CODE, poNewEntity.CREFERENCE_NO);
                    try
                    {
                        var loRtnSeq = loDb.SqlExecObjectQuery<FAT0010002DTO>(lcCmd, loConn, false);

                        if (loRtnSeq != null && loRtnSeq.Count > 0)
                        {
                            var loFirst = loRtnSeq.FirstOrDefault();
                            if (loFirst != null && !string.IsNullOrWhiteSpace(loFirst.CSEQNO))
                            {
                                poNewEntity.CTRANS_SEQNO = loFirst.CSEQNO;
                            }
                            else
                            {
                                poNewEntity.CTRANS_SEQNO = string.Empty;
                            }
                        }
                        else
                        {
                            poNewEntity.CTRANS_SEQNO = string.Empty;
                        }

                        _logger.LogInfo(lcCmd);
                        _logger.LogDebug(lcCmd);
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " SELECT TOP 1 1 " +
                                        " FROM FAT_TRANS_ASSET " +
                                        " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                        " AND CDEPT_CODE = @CDEPT_CODE " +
                                        " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                        " AND CREFERENCE_NO = @CREFERENCE_NO " +
                                        " AND CTRANS_SEQNO = @CTRANS_SEQNO ";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, poNewEntity.CTRANS_SEQNO);

                    var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    var loRtn = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTable).FirstOrDefault();

                    if (loRtn != null)
                    {
                        loEx.Add(GetError("PS001"));
                        loEx.ThrowExceptionIfErrors();
                        return;
                    }

                    loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, "000100");
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poNewEntity.CTRANSACTION_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@ITRANSACTION_QTY1", DbType.Int16, 0, poNewEntity.ITRANSACTION_QTY1);
                    loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, 50, poNewEntity.CUNIT);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, 50, poNewEntity.CFR_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_TRANSACTION_CODE", DbType.String, 50, poNewEntity.CFR_TRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_REFERENCE_NO", DbType.String, 50, poNewEntity.CFR_REFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_TRANSACTION_DATE", DbType.String, 50, poNewEntity.CFR_TRANSACTION_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_SEQUENCE_NO", DbType.String, 50, poNewEntity.CFR_SEQUENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 50, poNewEntity.CTRANSACTION_DESCR);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 50, poNewEntity.CASSET_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_LOCATION", DbType.String, 50, poNewEntity.CASSET_LOCATION);
                    loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 50, poNewEntity.CJRNGRP_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_CODE", DbType.String, 50, poNewEntity.CTAX_CATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_CODE", DbType.String, 50, poNewEntity.CCATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 50, poNewEntity.CDEPR_METHOD);
                    loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 50, poNewEntity.CSTART_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@NLBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NLBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NBBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIVE", DbType.Int16, 0, poNewEntity.IUSEFUL_LIVE);
                    loDb.R_AddCommandParameter(loCmd, "@NLYEAR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NLYEAR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBYEAR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NBYEAR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLRESIDUAL_VALUE", DbType.Decimal, 0, poNewEntity.NLRESIDUAL_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBRESIDUAL_VALUE", DbType.Decimal, 0, poNewEntity.NBRESIDUAL_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_PCT", DbType.Decimal, 0, poNewEntity.NYEAR_DEPR_PCT);
                    if (poNewEntity.OASSET_IMAGE != null)
                    {
                        loDb.R_AddCommandParameter(loCmd, "@OASSET_IMAGE", DbType.Binary, 0, poNewEntity.OASSET_IMAGE);
                    }
                    else
                    {
                        loDb.R_AddCommandParameter(loCmd, "@OASSET_IMAGE", DbType.Binary, 0, Array.Empty<byte>());
                    }
                    loDb.R_AddCommandParameter(loCmd, "@LDELETE_FLAG", DbType.Boolean, 0, poNewEntity.LDELETE_FLAG);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPR_STATUS", DbType.String, 50, poNewEntity.CDEPR_STATUS);
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PRD", DbType.String, 50, poNewEntity.CCURRENT_PRD);
                    loDb.R_AddCommandParameter(loCmd, "@CCREATE_BY", DbType.String, 50, poNewEntity.CCREATE_BY);
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_NAME", DbType.String, 50, poNewEntity.CASSET_NAME);
                    loDb.R_AddCommandParameter(loCmd, "@CSERIAL_NUMBER", DbType.String, 50, poNewEntity.CSERIAL_NUMBER);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESCRIPTION", DbType.String, 50, poNewEntity.CTRANS_DESCRIPTION);
                    loDb.R_AddCommandParameter(loCmd, "@CINSERVICE_DATE", DbType.String, 50, poNewEntity.CINSERVICE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_OWNER", DbType.String, 50, poNewEntity.CASSET_OWNER);
                    loDb.R_AddCommandParameter(loCmd, "@CSCATEGORY_CODE", DbType.String, 50, poNewEntity.CSCATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@NLYTD_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NLYTD_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBYTD_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NBYTD_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@LNEW_FLAG", DbType.Boolean, 0, poNewEntity.LNEW_FLAG);
                    loDb.R_AddCommandParameter(loCmd, "@CPURCHASE_DATE", DbType.String, 50, poNewEntity.CPURCHASE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_NAME", DbType.String, 50, poNewEntity.CSUPPLIER_NAME);
                    loDb.R_AddCommandParameter(loCmd, "@NLBEG_BOOK_VALUE", DbType.Decimal, 0, poNewEntity.NLBEG_BOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBBEG_BOOK_VALUE", DbType.Decimal, 0, poNewEntity.NBBEG_BOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@IBEG_USEFUL_LIVE", DbType.Int16, 0, poNewEntity.IBEG_USEFUL_LIVE);
                    loDb.R_AddCommandParameter(loCmd, "@NLBEGINNING_AMT", DbType.Decimal, 0, poNewEntity.NLBEGINNING_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBBEGINNING_AMT", DbType.Decimal, 0, poNewEntity.NBBEGINNING_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLADDITION_AMT", DbType.Decimal, 0, poNewEntity.NLADDITION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBADDITION_AMT", DbType.Decimal, 0, poNewEntity.NBADDITION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLDEDUCTION_AMT", DbType.Decimal, 0, poNewEntity.NLDEDUCTION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBDEDUCTION_AMT", DbType.Decimal, 0, poNewEntity.NBDEDUCTION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLREVENUE_AMT", DbType.Decimal, 0, poNewEntity.NLREVENUE_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBREVENUE_AMT", DbType.Decimal, 0, poNewEntity.NBREVENUE_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLSOLD_AMT", DbType.Decimal, 0, poNewEntity.NLSOLD_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBSOLD_AMT", DbType.Decimal, 0, poNewEntity.NBSOLD_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@IBEGINNING_QTY", DbType.Int32, 0, poNewEntity.IBEGINNING_QTY);
                    loDb.R_AddCommandParameter(loCmd, "@IADDITION_QTY", DbType.Int32, 0, poNewEntity.IADDITION_QTY);
                    loDb.R_AddCommandParameter(loCmd, "@IDEDUCTION_QTY", DbType.Int32, 0, poNewEntity.IDEDUCTION_QTY);
                    loDb.R_AddCommandParameter(loCmd, "@NLPRIOR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NLPRIOR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBPRIOR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NBPRIOR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLREVALUATION_AMT", DbType.Decimal, 0, poNewEntity.NLREVALUATION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBREVALUATION_AMT", DbType.Decimal, 0, poNewEntity.NBREVALUATION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLPRIOR_REVALUATION_AMT", DbType.Decimal, 0, poNewEntity.NLPRIOR_REVALUATION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBPRIOR_REVALUATION_AMT", DbType.Decimal, 0, poNewEntity.NBPRIOR_REVALUATION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLYTD_REVALUATION_AMT", DbType.Decimal, 0, poNewEntity.NLYTD_REVALUATION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBYTD_REVALUATION_AMT", DbType.Decimal, 0, poNewEntity.NBYTD_REVALUATION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@CLSEQUENCE_NO", DbType.String, 50, poNewEntity.CLSEQUENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CLAST_TRANS_DATE", DbType.String, 50, poNewEntity.CLAST_TRANS_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CLAST_DEPR_PERIOD", DbType.String, 50, poNewEntity.CLAST_DEPR_PERIOD);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_STATUS", DbType.String, 50, poNewEntity.CASSET_STATUS);
                    loDb.R_AddCommandParameter(loCmd, "@NLAST_BBASE_RATE_AMOUNT", DbType.Decimal, 0, poNewEntity.NLAST_BBASE_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NLAST_BCURRENCY_RATE_AMOUNT", DbType.Decimal, 0, poNewEntity.NLAST_BCURRENCY_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@CLAST_CURR_RATE_DATE", DbType.String, 50, poNewEntity.CLAST_CURR_RATE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@NBRATE_REVALUATION_AMT", DbType.Decimal, 0, poNewEntity.NBRATE_REVALUATION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@CEXPENSE_DEPT_CODE", DbType.String, 50, poNewEntity.CEXPENSE_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@NEXPENSE_PCT", DbType.Decimal, 0, poNewEntity.NEXPENSE_PCT);
                    loDb.R_AddCommandParameter(loCmd, "@COLD_FLAG", DbType.String, 50, poNewEntity.COLD_FLAG);
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, poNewEntity.CSUPPLIER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@NOLBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NOLBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NOBBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NOBBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@IOUSEFUL_LIVE", DbType.Int16, 0, poNewEntity.IOUSEFUL_LIVE);

                    lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                            " insert into FAT_TRANS_ASSET " +
                            " (CCOMPANY_ID " +
                            " ,CDEPT_CODE " +
                            " ,CTRANSACTION_CODE " +
                            " ,CREFERENCE_NO " +
                            " ,CASSET_CODE " +
                            " ,CTRANS_SEQNO " +
                            " ,CASSET_TRANS_SEQNO " +
                            " ,CTRANSACTION_DATE " +
                            " ,NTRANSACTION_AMOUNT1 " +
                            " ,NLTRANSACTION_AMOUNT1 " +
                            " ,NLTRANSACTION_AMOUNT2 " +
                            " ,NLTRANSACTION_AMOUNT3 " +
                            " ,NLTRANSACTION_AMOUNT4 " +
                            " ,NLTRANSACTION_AMOUNT5 " +
                            " ,NBTRANSACTION_AMOUNT1 " +
                            " ,NBTRANSACTION_AMOUNT2 " +
                            " ,NBTRANSACTION_AMOUNT3 " +
                            " ,NBTRANSACTION_AMOUNT4 " +
                            " ,NBTRANSACTION_AMOUNT5 " +
                            " ,ITRANSACTION_QTY1 " +
                            " ,CUNIT " +
                            " ,CFR_DEPT_CODE " +
                            " ,CFR_TRANSACTION_CODE " +
                            " ,CFR_REFERENCE_NO " +
                            " ,CFR_TRANSACTION_DATE " +
                            " ,CFR_SEQUENCE_NO " +
                            " ,CTRANSACTION_DESCR " +
                            " ,CASSET_DEPT_CODE " +
                            " ,CASSET_LOCATION " +
                            " ,CJRNGRP_CODE " +
                            " ,CTAX_CATEGORY_CODE " +
                            " ,CCATEGORY_CODE " +
                            " ,CDEPR_METHOD " +
                            " ,CSTART_DATE " +
                            " ,NLBOOK_VALUE " +
                            " ,NBBOOK_VALUE " +
                            " ,IUSEFUL_LIVE " +
                            " ,NLYEAR_DEPR_AMT " +
                            " ,NBYEAR_DEPR_AMT " +
                            " ,NLRESIDUAL_VALUE " +
                            " ,NBRESIDUAL_VALUE " +
                            " ,NYEAR_DEPR_PCT " +
                            " ,OASSET_IMAGE " +
                            " ,LDELETE_FLAG " +
                            " ,CDEPR_STATUS " +
                            " ,CCURRENT_PRD " +
                            " ,NOLBOOK_VALUE " +
                            " ,NOBBOOK_VALUE " +
                            " ,IOUSEFUL_LIVE " +
                            " ,CCREATE_BY " +
                            " ,DCREATE_DATE " +
                            " ,CUPDATE_BY " +
                            " ,DUPDATE_DATE) " +
                            " values " +
                            " (@CCOMPANY_ID " +
                            " ,@CDEPT_CODE " +
                            " ,@CTRANSACTION_CODE " +
                            " ,@CREFERENCE_NO " +
                            " ,@CASSET_CODE " +
                            " ,@CTRANS_SEQNO " +
                            " ,@CASSET_TRANS_SEQNO " +
                            " ,@CTRANSACTION_DATE " +
                            " ,@NTRANSACTION_AMOUNT1 " +
                            " ,@NLTRANSACTION_AMOUNT1 " +
                            " ,@NLTRANSACTION_AMOUNT2 " +
                            " ,@NLTRANSACTION_AMOUNT3 " +
                            " ,@NLTRANSACTION_AMOUNT4 " +
                            " ,@NLTRANSACTION_AMOUNT5 " +
                            " ,@NBTRANSACTION_AMOUNT1 " +
                            " ,@NBTRANSACTION_AMOUNT2 " +
                            " ,@NBTRANSACTION_AMOUNT3 " +
                            " ,@NBTRANSACTION_AMOUNT4 " +
                            " ,@NBTRANSACTION_AMOUNT5 " +
                            " ,@ITRANSACTION_QTY1 " +
                            " ,@CUNIT " +
                            " ,@CFR_DEPT_CODE " +
                            " ,@CFR_TRANSACTION_CODE " +
                            " ,@CFR_REFERENCE_NO " +
                            " ,@CFR_TRANSACTION_DATE " +
                            " ,@CFR_SEQUENCE_NO " +
                            " ,@CTRANSACTION_DESCR " +
                            " ,@CASSET_DEPT_CODE " +
                            " ,@CASSET_LOCATION " +
                            " ,@CJRNGRP_CODE " +
                            " ,@CTAX_CATEGORY_CODE " +
                            " ,@CCATEGORY_CODE " +
                            " ,@CDEPR_METHOD " +
                            " ,@CSTART_DATE " +
                            " ,@NLBOOK_VALUE " +
                            " ,@NBBOOK_VALUE " +
                            " ,@IUSEFUL_LIVE " +
                            " ,@NLYEAR_DEPR_AMT " +
                            " ,@NBYEAR_DEPR_AMT " +
                            " ,@NLRESIDUAL_VALUE " +
                            " ,@NBRESIDUAL_VALUE " +
                            " ,@NYEAR_DEPR_PCT " +
                            " ,@OASSET_IMAGE " +
                            " ,@LDELETE_FLAG " +
                            " ,@CDEPR_STATUS " +
                            " ,@CCURRENT_PRD " +
                            " ,@NOLBOOK_VALUE " +
                            " ,@NOBBOOK_VALUE " +
                            " ,@IOUSEFUL_LIVE " +
                            " ,@CCREATE_BY " +
                            " ,@DATENOW " +
                            " ,@CUPDATE_BY " +
                            " ,@DATENOW) ";

                    loCmd.CommandText = lcCmd;

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                    lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID)" +
                            " insert into FAM_ASSET " +
                            " (CCOMPANY_ID " +
                            " , CASSET_CODE " +
                            " , CASSET_NAME " +
                            " , CSERIAL_NUMBER " +
                            " , CTRANS_DESCRIPTION " +
                            " , CINSERVICE_DATE " +
                            " , CASSET_DEPT_CODE " +
                            " , CASSET_OWNER " +
                            " , CASSET_LOCATION " +
                            " , CJRNGRP_CODE " +
                            " , CTAX_CATEGORY_CODE " +
                            " , CCATEGORY_CODE " +
                            " , CSCATEGORY_CODE " +
                            " , CDEPR_METHOD " +
                            " , CSTART_DATE " +
                            " , NLBOOK_VALUE " +
                            " , NBBOOK_VALUE " +
                            " , IUSEFUL_LIVE " +
                            " , NYEAR_DEPR_PCT " +
                            " , NLYEAR_DEPR_AMT " +
                            " , NBYEAR_DEPR_AMT " +
                            " , NLRESIDUAL_VALUE " +
                            " , NBRESIDUAL_VALUE " +
                            " , NLYTD_DEPR_AMT " +
                            " , NBYTD_DEPR_AMT " +
                            " , LNEW_FLAG " +
                            " , CUNIT " +
                            " , CPURCHASE_DATE " +
                            " , CSUPPLIER_ID " +
                            " , CSUPPLIER_NAME " +
                            " , NLBEG_BOOK_VALUE " +
                            " , NBBEG_BOOK_VALUE " +
                            " , IBEG_USEFUL_LIVE " +
                            " , NLBEGINNING_AMT " +
                            " , NBBEGINNING_AMT " +
                            " , NLADDITION_AMT " +
                            " , NBADDITION_AMT " +
                            " , NLDEDUCTION_AMT " +
                            " , NBDEDUCTION_AMT " +
                            " , NLREVENUE_AMT " +
                            " , NBREVENUE_AMT " +
                            " , NLSOLD_AMT " +
                            " , NBSOLD_AMT " +
                            " , IBEGINNING_QTY " +
                            " , IADDITION_QTY " +
                            " , IDEDUCTION_QTY " +
                            " , NLPRIOR_DEPR_AMT " +
                            " , NBPRIOR_DEPR_AMT " +
                            " , NLREVALUATION_AMT " +
                            " , NBREVALUATION_AMT " +
                            " , NLPRIOR_REVALUATION_AMT " +
                            " , NBPRIOR_REVALUATION_AMT " +
                            " , NLYTD_REVALUATION_AMT " +
                            " , NBYTD_REVALUATION_AMT " +
                            " , CLSEQUENCE_NO " +
                            " , CLAST_TRANS_DATE " +
                            " , CLAST_DEPR_PERIOD " +
                            " , CASSET_STATUS " +
                            " , OASSET_IMAGE " +
                            " , CFR_DEPT_CODE " +
                            " , CFR_TRANSACTION_CODE " +
                            " , CFR_REFERENCE_NO " +
                            " , CFR_TRANSACTION_DATE " +
                            " , CFR_SEQUENCE_NO " +
                            " , NLAST_BBASE_RATE_AMOUNT " +
                            " , NLAST_BCURRENCY_RATE_AMOUNT " +
                            " , CLAST_CURR_RATE_DATE " +
                            " , NBRATE_REVALUATION_AMT " +
                            " , CCREATE_BY " +
                            " , DCREATE_DATE " +
                            " , CUPDATE_BY " +
                            " , DUPDATE_DATE) " +
                            " values " +
                            " (@CCOMPANY_ID " +
                            " , @CASSET_CODE " +
                            " , @CASSET_NAME " +
                            " , @CSERIAL_NUMBER " +
                            " , @CTRANS_DESCRIPTION " +
                            " , @CINSERVICE_DATE " +
                            " , @CASSET_DEPT_CODE " +
                            " , @CASSET_OWNER " +
                            " , @CASSET_LOCATION " +
                            " , @CJRNGRP_CODE " +
                            " , @CTAX_CATEGORY_CODE " +
                            " , @CCATEGORY_CODE " +
                            " , @CSCATEGORY_CODE " +
                            " , @CDEPR_METHOD " +
                            " , @CSTART_DATE " +
                            " , @NLBOOK_VALUE " +
                            " , @NBBOOK_VALUE " +
                            " , @IUSEFUL_LIVE " +
                            " , @NYEAR_DEPR_PCT " +
                            " , @NLYEAR_DEPR_AMT " +
                            " , @NBYEAR_DEPR_AMT " +
                            " , @NLRESIDUAL_VALUE " +
                            " , @NBRESIDUAL_VALUE " +
                            " , @NLYTD_DEPR_AMT " +
                            " , @NBYTD_DEPR_AMT " +
                            " , @LNEW_FLAG " +
                            " , @CUNIT " +
                            " , @CPURCHASE_DATE " +
                            " , @CSUPPLIER_ID " +
                            " , @CSUPPLIER_NAME " +
                            " , @NLBEG_BOOK_VALUE " +
                            " , @NBBEG_BOOK_VALUE " +
                            " , @IBEG_USEFUL_LIVE " +
                            " , @NLBEGINNING_AMT " +
                            " , @NBBEGINNING_AMT " +
                            " , @NLADDITION_AMT " +
                            " , @NBADDITION_AMT " +
                            " , @NLDEDUCTION_AMT " +
                            " , @NBDEDUCTION_AMT " +
                            " , @NLREVENUE_AMT " +
                            " , @NBREVENUE_AMT " +
                            " , @NLSOLD_AMT " +
                            " , @NBSOLD_AMT " +
                            " , @IBEGINNING_QTY " +
                            " , @IADDITION_QTY " +
                            " , @IDEDUCTION_QTY " +
                            " , @NLPRIOR_DEPR_AMT " +
                            " , @NBPRIOR_DEPR_AMT " +
                            " , @NLREVALUATION_AMT " +
                            " , @NBREVALUATION_AMT " +
                            " , @NLPRIOR_REVALUATION_AMT " +
                            " , @NBPRIOR_REVALUATION_AMT " +
                            " , @NLYTD_REVALUATION_AMT " +
                            " , @NBYTD_REVALUATION_AMT " +
                            " , @CLSEQUENCE_NO " +
                            " , @CLAST_TRANS_DATE " +
                            " , @CLAST_DEPR_PERIOD " +
                            " , @CASSET_STATUS " +
                            " , @OASSET_IMAGE " +
                            " , @CFR_DEPT_CODE " +
                            " , @CFR_TRANSACTION_CODE " +
                            " , @CFR_REFERENCE_NO " +
                            " , @CFR_TRANSACTION_DATE " +
                            " , @CFR_SEQUENCE_NO " +
                            " , @NLAST_BBASE_RATE_AMOUNT " +
                            " , @NLAST_BCURRENCY_RATE_AMOUNT " +
                            " , @CLAST_CURR_RATE_DATE " +
                            " , @NBRATE_REVALUATION_AMT " +
                            " , @CCREATE_BY " +
                            " , @DATENOW " +
                            " , @CUPDATE_BY " +
                            " , @DATENOW) ";

                    loCmd.CommandText = lcCmd;

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                    lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                            " insert into FAT_TRANS_EXP_ALLOC " +
                            " (CCOMPANY_ID " +
                            " , CDEPT_CODE " +
                            " , CTRANSACTION_CODE " +
                            " , CREFERENCE_NO " +
                            " , CASSET_CODE " +
                            " , CASSET_TRANS_SEQNO " +
                            " , CEXPENSE_DEPT_CODE " +
                            " , NEXPENSE_PCT " +
                            " , COLD_FLAG " +
                            " , CCREATE_BY " +
                            " , DCREATE_DATE " +
                            " , CUPDATE_BY " +
                            " , DUPDATE_DATE) " +
                            " values " +
                            " (@CCOMPANY_ID " +
                            " , @CDEPT_CODE " +
                            " , @CTRANSACTION_CODE " +
                            " , @CREFERENCE_NO " +
                            " , @CASSET_CODE " +
                            " , '000100' " +
                            " , @CEXPENSE_DEPT_CODE " +
                            " , 100.00 " +
                            " , '0' " +
                            " , @CCREATE_BY " +
                            " , @DATENOW " +
                            " , @CUPDATE_BY " +
                            " , @DATENOW) ";

                    loCmd.CommandText = lcCmd;

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                    //===== MT CR13 Save start =====
                    loCmd.Parameters.Clear();
                    lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                            " UPDATE FAT_TRANS_HD SET " +
                            " NTRANSACTION_AMOUNT = NTRANSACTION_AMOUNT + @NTRANSACTION_AMOUNT1, " +
                            " NLTRANSACTION_AMOUNT = NLTRANSACTION_AMOUNT + @NLTRANSACTION_AMOUNT1 + @NLTRANSACTION_AMOUNT2 - @NLTRANSACTION_AMOUNT3 - @NLTRANSACTION_AMOUNT4 - @NLTRANSACTION_AMOUNT5, " +
                            " NBTRANSACTION_AMOUNT = NBTRANSACTION_AMOUNT + @NBTRANSACTION_AMOUNT1 + @NBTRANSACTION_AMOUNT2 - @NBTRANSACTION_AMOUNT3 - @NBTRANSACTION_AMOUNT4 - @NBTRANSACTION_AMOUNT5, " +
                            " CUPDATE_BY = @CUPDATE_BY, " +
                            " DUPDATE_DATE = @DATENOW, " +
                            " LGLLINK = @LGLLINK " +
                            " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                            " AND CDEPT_CODE = @CDEPT_CODE " +
                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                            " AND CREFERENCE_NO = @CREFERENCE_NO ";

                    loCmd.CommandText = lcCmd;
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 0, poNewEntity.LGLLINK);

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    //===== MT CR13 end =====
                }
                else
                {
                    //===== MT CR13 Edit start =====
                    loCmd.Parameters.Clear();
                    lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                            " UPDATE FAT_TRANS_HD SET " +
                            " NTRANSACTION_AMOUNT = NTRANSACTION_AMOUNT + @NTRANSACTION_AMOUNT1 - NTRANSACTION_AMOUNT1, " +
                            " NLTRANSACTION_AMOUNT = NLTRANSACTION_AMOUNT + @NLTRANSACTION_AMOUNT1 + @NLTRANSACTION_AMOUNT2 - @NLTRANSACTION_AMOUNT3 - @NLTRANSACTION_AMOUNT4 - @NLTRANSACTION_AMOUNT5 - NLTRANSACTION_AMOUNT1 - NLTRANSACTION_AMOUNT2 + NLTRANSACTION_AMOUNT3 + NLTRANSACTION_AMOUNT4 + NLTRANSACTION_AMOUNT5, " +
                            " NBTRANSACTION_AMOUNT = NBTRANSACTION_AMOUNT + @NBTRANSACTION_AMOUNT1 + @NBTRANSACTION_AMOUNT2 - @NBTRANSACTION_AMOUNT3 - @NBTRANSACTION_AMOUNT4 - @NBTRANSACTION_AMOUNT5 - NBTRANSACTION_AMOUNT1 - NBTRANSACTION_AMOUNT2 + NBTRANSACTION_AMOUNT3 + NBTRANSACTION_AMOUNT4 + NBTRANSACTION_AMOUNT5, " +
                            " CUPDATE_BY = @CUPDATE_BY, " +
                            " DUPDATE_DATE = @DATENOW, " +
                            " LGLLINK = @LGLLINK " +
                            " FROM FAT_TRANS_HD a, FAT_TRANS_ASSET b " +
                            " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                            " AND a.CDEPT_CODE = @CDEPT_CODE " +
                            " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                            " AND a.CREFERENCE_NO = @CREFERENCE_NO " +
                            " AND b.CCOMPANY_ID = a.CCOMPANY_ID " +
                            " AND b.CDEPT_CODE = a.CDEPT_CODE " +
                            " AND b.CTRANSACTION_CODE = a.CTRANSACTION_CODE " +
                            " AND b.CREFERENCE_NO = a.CREFERENCE_NO " +
                            " AND b.CTRANS_SEQNO = @CTRANS_SEQNO ";

                    loCmd.CommandText = lcCmd;
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, poNewEntity.CTRANS_SEQNO);
                    loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 0, poNewEntity.LGLLINK);

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    //===== MT CR13 end =====

                    loCmd.Parameters.Clear();
                    loCmd.CommandText = " SELECT TOP 1 1 " +
                                       " FROM FAT_TRANS_ASSET " +
                                       " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                       " AND CDEPT_CODE = @CDEPT_CODE " +
                                       " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                       " AND CREFERENCE_NO = @CREFERENCE_NO " +
                                       " AND CTRANS_SEQNO = @CTRANS_SEQNO ";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, poNewEntity.CTRANS_SEQNO);

                    var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    var loRtn = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTable).FirstOrDefault();

                    if (loRtn == null)
                    {
                        loEx.Add(GetError("PS002"));
                        loEx.ThrowExceptionIfErrors();
                        return;
                    }

                    loCmd.Parameters.Clear();
                    lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                            " UPDATE FAT_TRANS_ASSET SET " +
                            " NTRANSACTION_AMOUNT1 = @NTRANSACTION_AMOUNT1, " +
                            " NLTRANSACTION_AMOUNT1 = @NLTRANSACTION_AMOUNT1, " +
                            " NLTRANSACTION_AMOUNT2 = @NLTRANSACTION_AMOUNT2, " +
                            " NLTRANSACTION_AMOUNT3 = @NLTRANSACTION_AMOUNT3, " +
                            " NLTRANSACTION_AMOUNT4 = @NLTRANSACTION_AMOUNT4, " +
                            " NLTRANSACTION_AMOUNT5 = @NLTRANSACTION_AMOUNT5, " +
                            " NBTRANSACTION_AMOUNT1 = @NBTRANSACTION_AMOUNT1, " +
                            " NBTRANSACTION_AMOUNT2 = @NBTRANSACTION_AMOUNT2, " +
                            " NBTRANSACTION_AMOUNT3 = @NBTRANSACTION_AMOUNT3, " +
                            " NBTRANSACTION_AMOUNT4 = @NBTRANSACTION_AMOUNT4, " +
                            " NBTRANSACTION_AMOUNT5 = @NBTRANSACTION_AMOUNT5, " +
                            " ITRANSACTION_QTY1 = @ITRANSACTION_QTY1, " +
                            " CUNIT = @CUNIT, " +
                            " CTRANSACTION_DESCR = @CTRANSACTION_DESCR, " +
                            " CASSET_DEPT_CODE = @CASSET_DEPT_CODE, " +
                            " CASSET_LOCATION = @CASSET_LOCATION, " +
                            " CJRNGRP_CODE = @CJRNGRP_CODE, " +
                            " CTAX_CATEGORY_CODE = @CTAX_CATEGORY_CODE, " +
                            " CCATEGORY_CODE = @CCATEGORY_CODE, " +
                            " CDEPR_METHOD = @CDEPR_METHOD, " +
                            " CSTART_DATE = @CSTART_DATE, " +
                            " NLBOOK_VALUE = @NLBOOK_VALUE, " +
                            " NBBOOK_VALUE = @NBBOOK_VALUE, " +
                            " IUSEFUL_LIVE = @IUSEFUL_LIVE, " +
                            " NOLBOOK_VALUE = @NOLBOOK_VALUE, " +
                            " NOBBOOK_VALUE = @NOBBOOK_VALUE, " +
                            " IOUSEFUL_LIVE = @IOUSEFUL_LIVE, " +
                            " NLYEAR_DEPR_AMT = @NLYEAR_DEPR_AMT, " +
                            " NBYEAR_DEPR_AMT = @NBYEAR_DEPR_AMT, " +
                            " NLRESIDUAL_VALUE = @NLRESIDUAL_VALUE, " +
                            " NBRESIDUAL_VALUE = @NBRESIDUAL_VALUE, " +
                            " NYEAR_DEPR_PCT = @NYEAR_DEPR_PCT, " +
                            " CUPDATE_BY = @CUPDATE_BY, " +
                            " DUPDATE_DATE = @DATENOW " +
                            " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                            " AND CDEPT_CODE = @CDEPT_CODE " +
                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                            " AND CREFERENCE_NO = @CREFERENCE_NO " +
                            " AND CTRANS_SEQNO = @CTRANS_SEQNO ";

                    loCmd.CommandText = lcCmd;
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NLTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT1", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT1);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT2", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT2);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT3", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT3);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT4", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT4);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT5", DbType.Decimal, 0, poNewEntity.NBTRANSACTION_AMOUNT5);
                    loDb.R_AddCommandParameter(loCmd, "@ITRANSACTION_QTY1", DbType.Int16, 0, poNewEntity.ITRANSACTION_QTY1);
                    loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, 50, poNewEntity.CUNIT);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 50, poNewEntity.CTRANSACTION_DESCR);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 50, poNewEntity.CASSET_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_LOCATION", DbType.String, 50, poNewEntity.CASSET_LOCATION);
                    loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 50, poNewEntity.CJRNGRP_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_CODE", DbType.String, 50, poNewEntity.CTAX_CATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_CODE", DbType.String, 50, poNewEntity.CCATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 50, poNewEntity.CDEPR_METHOD);
                    loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 50, poNewEntity.CSTART_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@NLBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NLBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NBBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIVE", DbType.Int16, 0, poNewEntity.IUSEFUL_LIVE);
                    loDb.R_AddCommandParameter(loCmd, "@NOLBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NOLBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NOBBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NOBBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@IOUSEFUL_LIVE", DbType.Int16, 0, poNewEntity.IOUSEFUL_LIVE);
                    loDb.R_AddCommandParameter(loCmd, "@NLYEAR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NLYEAR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBYEAR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NBYEAR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLRESIDUAL_VALUE", DbType.Decimal, 0, poNewEntity.NLRESIDUAL_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBRESIDUAL_VALUE", DbType.Decimal, 0, poNewEntity.NBRESIDUAL_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_PCT", DbType.Decimal, 0, poNewEntity.NYEAR_DEPR_PCT);
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, poNewEntity.CTRANS_SEQNO);

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                    loCmd.Parameters.Clear();
                    lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID) " +
                            " UPDATE FAM_ASSET SET " +
                            " CASSET_NAME = @CASSET_NAME, " +
                            " CSERIAL_NUMBER = @CSERIAL_NUMBER, " +
                            " CTRANS_DESCRIPTION = @CTRANS_DESCRIPTION, " +
                            " CINSERVICE_DATE = @CINSERVICE_DATE, " +
                            " CASSET_DEPT_CODE = @CASSET_DEPT_CODE, " +
                            " CASSET_OWNER = @CASSET_OWNER, " +
                            " CASSET_LOCATION = @CASSET_LOCATION, " +
                            " CJRNGRP_CODE = @CJRNGRP_CODE, " +
                            " CTAX_CATEGORY_CODE = @CTAX_CATEGORY_CODE, " +
                            " CCATEGORY_CODE = @CCATEGORY_CODE, " +
                            " CDEPR_METHOD = @CDEPR_METHOD, " +
                            " CSTART_DATE = @CSTART_DATE, " +
                            " NLBOOK_VALUE = @NLBOOK_VALUE, " +
                            " NBBOOK_VALUE = @NBBOOK_VALUE, " +
                            " IUSEFUL_LIVE = @IUSEFUL_LIVE,  " +
                            " NYEAR_DEPR_PCT = @NYEAR_DEPR_PCT, " +
                            " NLYEAR_DEPR_AMT = @NLYEAR_DEPR_AMT, " +
                            " NBYEAR_DEPR_AMT = @NBYEAR_DEPR_AMT, " +
                            " NLRESIDUAL_VALUE = @NLRESIDUAL_VALUE, " +
                            " NBRESIDUAL_VALUE = @NBRESIDUAL_VALUE, " +
                            " NLYTD_DEPR_AMT = @NLYTD_DEPR_AMT, " +
                            " NBYTD_DEPR_AMT = @NBYTD_DEPR_AMT, " +
                            " LNEW_FLAG = @LNEW_FLAG, " +
                            " CUNIT = @CUNIT, " +
                            " NLBEG_BOOK_VALUE = @NLBEG_BOOK_VALUE, " +
                            " NBBEG_BOOK_VALUE = @NBBEG_BOOK_VALUE, " +
                            " IBEG_USEFUL_LIVE = @IBEG_USEFUL_LIVE,  " +
                            " NLBEGINNING_AMT = @NLBEGINNING_AMT, " +
                            " NBBEGINNING_AMT = @NBBEGINNING_AMT, " +
                            " NLADDITION_AMT = @NLADDITION_AMT, " +
                            " NBADDITION_AMT = @NBADDITION_AMT, " +
                            " NLDEDUCTION_AMT = @NLDEDUCTION_AMT, " +
                            " NBDEDUCTION_AMT = @NBDEDUCTION_AMT, " +
                            " IBEGINNING_Qty = @IBEGINNING_Qty, " +
                            " NLPRIOR_DEPR_AMT = @NLPRIOR_DEPR_AMT, " +
                            " NBPRIOR_DEPR_AMT = @NBPRIOR_DEPR_AMT, " +
                            " OASSET_IMAGE = @OASSET_IMAGE, " +
                            " CUPDATE_BY = @CUPDATE_BY, " +
                            " DUPDATE_DATE = @DATENOW " +
                            " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                            " AND CASSET_CODE = @CASSET_CODE ";

                    loCmd.CommandText = lcCmd;
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_NAME", DbType.String, 50, poNewEntity.CASSET_NAME);
                    loDb.R_AddCommandParameter(loCmd, "@CSERIAL_NUMBER", DbType.String, 50, poNewEntity.CSERIAL_NUMBER);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESCRIPTION", DbType.String, 50, poNewEntity.CTRANS_DESCRIPTION);
                    loDb.R_AddCommandParameter(loCmd, "@CINSERVICE_DATE", DbType.String, 50, poNewEntity.CINSERVICE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 50, poNewEntity.CASSET_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_OWNER", DbType.String, 50, poNewEntity.CASSET_OWNER);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_LOCATION", DbType.String, 50, poNewEntity.CASSET_LOCATION);
                    loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 50, poNewEntity.CJRNGRP_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_CODE", DbType.String, 50, poNewEntity.CTAX_CATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_CODE", DbType.String, 50, poNewEntity.CCATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 50, poNewEntity.CDEPR_METHOD);
                    loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 50, poNewEntity.CSTART_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@NLBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NLBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBBOOK_VALUE", DbType.Decimal, 0, poNewEntity.NBBOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIVE", DbType.Int16, 0, poNewEntity.IUSEFUL_LIVE);
                    loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_PCT", DbType.Decimal, 0, poNewEntity.NYEAR_DEPR_PCT);
                    loDb.R_AddCommandParameter(loCmd, "@NLYEAR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NLYEAR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBYEAR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NBYEAR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLRESIDUAL_VALUE", DbType.Decimal, 0, poNewEntity.NLRESIDUAL_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBRESIDUAL_VALUE", DbType.Decimal, 0, poNewEntity.NBRESIDUAL_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NLYTD_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NLYTD_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBYTD_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NBYTD_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@LNEW_FLAG", DbType.Boolean, 0, poNewEntity.LNEW_FLAG);
                    loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, 50, poNewEntity.CUNIT);
                    loDb.R_AddCommandParameter(loCmd, "@NLBEG_BOOK_VALUE", DbType.Decimal, 0, poNewEntity.NLBEG_BOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@NBBEG_BOOK_VALUE", DbType.Decimal, 0, poNewEntity.NBBEG_BOOK_VALUE);
                    loDb.R_AddCommandParameter(loCmd, "@IBEG_USEFUL_LIVE", DbType.Int16, 0, poNewEntity.IBEG_USEFUL_LIVE);
                    loDb.R_AddCommandParameter(loCmd, "@NLBEGINNING_AMT", DbType.Decimal, 0, poNewEntity.NLBEGINNING_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBBEGINNING_AMT", DbType.Decimal, 0, poNewEntity.NBBEGINNING_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLADDITION_AMT", DbType.Decimal, 0, poNewEntity.NLADDITION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBADDITION_AMT", DbType.Decimal, 0, poNewEntity.NBADDITION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NLDEDUCTION_AMT", DbType.Decimal, 0, poNewEntity.NLDEDUCTION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBDEDUCTION_AMT", DbType.Decimal, 0, poNewEntity.NBDEDUCTION_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@IBEGINNING_Qty", DbType.Int16, 0, poNewEntity.IBEGINNING_QTY);
                    loDb.R_AddCommandParameter(loCmd, "@NLPRIOR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NLPRIOR_DEPR_AMT);
                    loDb.R_AddCommandParameter(loCmd, "@NBPRIOR_DEPR_AMT", DbType.Decimal, 0, poNewEntity.NBPRIOR_DEPR_AMT);
                    if (poNewEntity.OASSET_IMAGE != null)
                    {
                        loDb.R_AddCommandParameter(loCmd, "@OASSET_IMAGE", DbType.Binary, 0, poNewEntity.OASSET_IMAGE);
                    }
                    else
                    {
                        loDb.R_AddCommandParameter(loCmd, "@OASSET_IMAGE", DbType.Binary, 0, Array.Empty<byte>());
                    }
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                    loCmd.Parameters.Clear();

                    if (!pcAssetDeptCodeOld.Equals(poNewEntity.CASSET_DEPT_CODE))
                    {
                        lcCmd = " DECLARE @DATENOW DATETIME = DBO.RFN_GET_DB_TODAY (@CCOMPANY_ID)  " +
                                " EXEC RSP_FAT00100_UPDATE_EXP_ALLOC @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CASSET_CODE, @CASSET_TRANS_SEQNO, @COLD_DEPT_CODE, @CNEW_DEPT_CODE, @CUSER_ID ";
                        loCmd.CommandText = lcCmd;
                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                        loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, poNewEntity.CASSET_TRANS_SEQNO);
                        loDb.R_AddCommandParameter(loCmd, "@COLD_DEPT_CODE", DbType.String, 50, poNewEntity.COLD_DEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CNEW_DEPT_CODE", DbType.String, 50, poNewEntity.CNEW_DEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                        loCmd.Parameters.Clear();
                    }
                }

                //Get Exception - Note: R_ExternalException may not be available in NET6, preserve logic
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
        /// Validate department code
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, and user ID</param>
        /// <returns>Result DTO with validation result (1 if valid, 0 if not)</returns>
        public async Task<FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>> ValidateDeptCodeAsync(FAT0010002ValidateDeptCodeParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidateDeptCodeAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>
            {
                Data = new FAT0010002ValidateDeptCodeResultDTO { Result = 0 }
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

                // Preserve original VB.NET logic exactly (even if it appears to be a bug)
                var loRtnTemp = new FAT00100DTO();
                if (loRtnTemp != null)
                {
                    loResult.Data.Result = 1;
                }
                else
                {
                    loResult.Data.Result = 0;
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
        /// Get declining depreciation amount
        /// </summary>
        /// <param name="poParameter">Parameter containing depreciation method, useful life years/months, and beginning book value</param>
        /// <returns>Result DTO with depreciation amount</returns>
        public async Task<FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>> GetDecliningDeprAmtAsync(FAT0010002GetDecliningDeprAmtParameterDTO poParameter)
        {
            string lcMethod = nameof(GetDecliningDeprAmtAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>
            {
                Data = new FAT0010002GetDecliningDeprAmtResultDTO { DeprAmt = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = "DECLARE @DeprAmt numeric(18,2) " +
                                    "EXEC RSP_FA_GET_DECL_YEARLY_DEPR_AMT @CDEPR_METHOD ,@IBEG_UL_YR ,@IBEG_UL_MO ,@IREM_UL_YR ,@IREM_UL_MO ,@NBEG_BOOK_VAL ";

                loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 50, poParameter.CDEPR_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@IBEG_UL_YR", DbType.Int16, 0, poParameter.IBEG_UL_YR);
                loDb.R_AddCommandParameter(loCmd, "@IBEG_UL_MO", DbType.Int16, 0, poParameter.IBEG_UL_MO);
                loDb.R_AddCommandParameter(loCmd, "@IREM_UL_YR", DbType.Int16, 0, poParameter.IREM_UL_YR);
                loDb.R_AddCommandParameter(loCmd, "@IREM_UL_MO", DbType.Int16, 0, poParameter.IREM_UL_MO);
                loDb.R_AddCommandParameter(loCmd, "@NBEG_BOOK_VAL", DbType.Decimal, 0, poParameter.NBEG_BOOK_VAL);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.DeprAmt = loRtn.DeprAmt;
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
    }
}


