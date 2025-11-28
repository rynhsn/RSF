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
    public class FAT00800Cls : R_BusinessObjectAsync<FAT00800DTO>
    {
        private readonly FAT00800BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00800 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00800Cls()
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

                loCmd.CommandText = " SELECT a.CDEPT_CODE        " +
                                    " ,a.CREFERENCE_NO           " +
                                    " ,a.CTRANSACTION_DATE       " +
                                    " ,a.CTRANSACTION_DESCR      " +
                                    " ,CSTATUS                   " +
                                    " ,LGLLINK                   " +
                                    " ,CGL_TRF_STATUS            " +
                                    " ,NLBASE_RATE_AMOUNT        " +
                                    " ,NLCURRENCY_RATE_AMOUNT    " +
                                    " ,a.NBBASE_RATE_AMOUNT      " +
                                    " ,a.NBCURRENCY_RATE_AMOUNT  " +
                                    " ,a.CCREATE_BY              " +
                                    " ,a.DCREATE_DATE            " +
                                    " ,a.CUPDATE_BY              " +
                                    " ,a.DUPDATE_DATE            " +
                                    " ,a.CTRANSACTION_PRD        " +
                                    " ,CSTATUS_DESC = ISNULL(p.DESCRIPTION, a.CSTATUS) " +
                                    " ,CALLOC_EXPENSE_CODE   " +
                                    " ,B.CASSET_CODE         " +
                                    " ,CASSET_TRANS_SEQNO    " +
                                    " ,NTRANSACTION_AMOUNT1  " +
                                    " ,NLTRANSACTION_AMOUNT1 " +
                                    " ,NBTRANSACTION_AMOUNT1 " +
                                    " ,CCURRENCY_CODE        " +
                                    " ,CASSET_NAME           " + // For Lookup Asset
                                    " ,CLSEQUENCE_NO         " + // For Lookup Asset
                                    " FROM FAT_TRANS_HD a(NOLOCK) " +
                                    " INNER JOIN FAT_TRANS_ASSET b(NOLOCK) ON b.CCOMPANY_ID = a.CCOMPANY_ID         " +
                                    " AND b.CDEPT_CODE = a.CDEPT_CODE AND b.CTRANSACTION_CODE = a.CTRANSACTION_CODE " +
                                    " AND b.CREFERENCE_NO = a.CREFERENCE_NO " + // CR AL 20/12/2023
                                    " INNER JOIN FAM_ASSET C ON B.CCOMPANY_ID=C.CCOMPANY_ID AND B.CASSET_CODE=C.CASSET_CODE " + // For Lookup Asset
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_TRX_STATUS', '', @CLANGID) " +
                                    " p ON p.CODE = a.CSTATUS                      " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID           " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE               " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poEntity.CLANG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 30, poEntity.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 30, poEntity.CREFERENCE_NO); // --{Input Transaction Number Find mode} or @PCREFERENCE_NO or @PCDEPT_CODE

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnList = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable);
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
            DataTable loRtnDataTable;
            FAT00800DTO loRtnAssetInfo = new();
            FAT00800DTO loRtnPeriod = new();
            FAT00800DTO loRtnloValidate = new();
            FAT00800DTO loRtnRefno = new();
            int loRtnValid1 = 0;
            int loRtnValid2 = 0;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                // Global Parameter for Validation & Preparation
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 20, poNewEntity.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poNewEntity.CTRANSACTION_DATE);

                // =======================================================First Validation================================================
                loCmd.CommandText = " SELECT CLAST_TRANS_DATE = CLAST_TRANS_DATE,                      " +
                                    " CASSET_STATUS = CASSET_STATUS FROM FAM_ASSET (nolock)            " +
                                    " WHERE CCOMPANY_ID= @CCOMPANY_ID AND CASSET_CODE = @CASSET_CODE   ";
                loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnValidateList = R_Utility.R_ConvertTo<FAT00800DTO>(loRtnDataTable);
                loRtnloValidate = loRtnValidateList.FirstOrDefault() ?? new FAT00800DTO();

                // Validate Status of the asset
                // NOTE: Bug in original VB.NET - condition (loRtnloValidate.CASSET_STATUS = "1" And loRtnloValidate.CASSET_STATUS = "2") is always false - preserved as-is
                if ((poCRUDMode != eCRUDMode.EditMode && (poNewEntity.LCHANGE_DESC || poNewEntity.LCHANGE_ALLOC)) || (loRtnloValidate.CASSET_STATUS == "1" && loRtnloValidate.CASSET_STATUS == "2"))
                {
                    loEx.Add(GetError("PS002"));
                }

                // Validate Transaction Date should not be before the last one of this asset
                if (!(poNewEntity.CTRANSACTION_DATE.CompareTo(loRtnloValidate.CLAST_TRANS_DATE) >= 0))
                {
                    loEx.Add(GetError("PS003"));
                }
                // =======================================================End First Validation===========================================
                if (loEx.Haserror)
                {
                    goto EndMethod;
                }

                // =======================================================Preparation===================================================
                // Get Asset information
                loCmd.CommandText = " SELECT NLFA = NLBEGINNING_AMT + NLADDITION_AMT - NLDEDUCTION_AMT,                            " +
                                    " NLAD = NLPRIOR_DEPR_AMT + NLYTD_DEPR_AMT,                                                    " +
                                    " NLRFA = NLREVALUATION_AMT,                                                                   " +
                                    " NLRAD = NLPRIOR_REVALUATION_AMT + NLYTD_REVALUATION_AMT,                                     " +
                                    " NBFA = NBBEGINNING_AMT + NBADDITION_AMT - NBDEDUCTION_AMT,                                   " +
                                    " NBAD = NBPRIOR_DEPR_AMT + NBYTD_DEPR_AMT,                                                    " +
                                    " NBRFA = NBREVALUATION_AMT,                                                                   " +
                                    " NBRAD = NBPRIOR_REVALUATION_AMT + NBYTD_REVALUATION_AMT,                                     " +
                                    " CNSEQUENCE_NO = right('000000' + convert(varchar(6),convert(integer,CLSEQUENCE_NO)+100),6),  " +
                                    " CASSET_DEPT_CODE = CASSET_DEPT_CODE,                                                         " +
                                    " CJRNGRP_CODE = CJRNGRP_CODE,                                                                 " +
                                    " CTAX_CATEGORY_CODE = CTAX_CATEGORY_CODE,                                                     " +
                                    " CDEPR_METHOD = CDEPR_METHOD                                                                  " +
                                    " FROM FAM_ASSET (nolock)                                                                      " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CASSET_CODE = @CASSET_CODE                                ";
                loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnAssetInfoList = R_Utility.R_ConvertTo<FAT00800DTO>(loRtnDataTable);
                loRtnAssetInfo = loRtnAssetInfoList.FirstOrDefault() ?? new FAT00800DTO();

                // Get Period
                loCmd.CommandText = " SELECT CPRD = CCYEAR + CPERIOD_NO FROM GSM_PERIOD_DT " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND @CTRANSACTION_DATE BETWEEN CSTART_DATE AND CEND_DATE ";
                loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtnPeriodList = R_Utility.R_ConvertTo<FAT00800DTO>(loRtnDataTable);
                loRtnPeriod = loRtnPeriodList.FirstOrDefault() ?? new FAT00800DTO();
                // =====================================================End Preparation===================================================

                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    // First Parameter
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CCREATE_BY", DbType.String, 50, poNewEntity.CCREATE_BY);
                    // @CREFERENCE_NO noterefno

                    // Validasi outstanding transaction-------------------------------------------------------------
                    loCmd.CommandText = " SELECT TOP 1 1 FROM FAT_TRANS_ASSET a(NOLOCK) " +
                                        " ,FAM_ASSET b(NOLOCK)                          " +
                                        " WHERE a.CCOMPANY_ID = @CCOMPANY_ID AND a.CASSET_CODE = @CASSET_CODE " +
                                        " AND a.LDELETE_FLAG = 0 AND b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                        " AND b.CASSET_CODE = a.CASSET_CODE          " +
                                        " AND a.CASSET_TRANS_SEQNO > b.CLSEQUENCE_NO " +
                                        " UNION " +
                                        " SELECT a.CASSET_CODE " +
                                        " FROM FAT_RAPID_DISCARD_ASSET a(NOLOCK) " +
                                        " ,FAT_RAPID_DISCARD_HD c(NOLOCK)    " +
                                        " ,FAM_ASSET b(NOLOCK)               " +
                                        " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                                        " AND a.CASSET_CODE = @CASSET_CODE   " +
                                        " AND c.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                        " AND c.CDEPT_CODE = a.CDEPT_CODE    " +
                                        " AND c.CTRANSACTION_CODE = a.CTRANSACTION_CODE  " +
                                        " AND c.CREFERENCE_NO = a.CREFERENCE_NO " +
                                        " AND c.CSTATUS <= '08' " +
                                        " AND b.CCOMPANY_ID = a.CCOMPANY_ID     " +
                                        " AND b.CASSET_CODE = a.CASSET_CODE     " +
                                        " AND a.CASSET_TRANS_SEQNO > b.CLSEQUENCE_NO ";

                    loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    loRtnValid2 = loRtnDataTable.Rows.Count;

                    if (loRtnValid2 == 1)
                    {
                        loEx.Add(GetError("PS001"));
                    }
                    if (loEx.Haserror)
                    {
                        goto EndMethod;
                    }

                    if (poNewEntity.LINCREMENT_FLAG == true) // Matic Refno
                    {
                        loCmd.CommandText = " DECLARE @CREFERENCE_NO AS VARCHAR(30) " +
                                            " EXEC RSP_GET_REFNO @CCOMPANY_ID, @CTRANSACTION_CODE , @CDEPT_CODE, @CTRANSACTION_DATE, @CCREATE_BY , @CREFERENCE_NO Output " +
                                            " SELECT @CREFERENCE_NO AS CREFERENCE_NO ";
                        loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                        var loRtnRefnoList = R_Utility.R_ConvertTo<FAT00800DTO>(loRtnDataTable);
                        loRtnRefno = loRtnRefnoList.FirstOrDefault() ?? new FAT00800DTO();

                        poNewEntity.CREFERENCE_NO = loRtnRefno.CREFERENCE_NO; // Balikan untuk R_Display
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, loRtnRefno.CREFERENCE_NO); // CREFERENCE_NO From RSP
                    }
                    else // Manual Refno
                    {
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO); // CREFERENCE_NO From Front
                        loCmd.CommandText = " SELECT TOP 1 1 FROM FAT_TRANS_HD (nolock) where CCOMPANY_ID = @CCOMPANY_ID " +
                                            " and CDEPT_CODE=@CDEPT_CODE and CTRANSACTION_CODE=@CTRANSACTION_CODE and CREFERENCE_NO=@CREFERENCE_NO ";
                        // CTRANSACTION_NO Dari CREFERENCE_NO (Look Spec)

                        loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                        loRtnValid1 = loRtnDataTable.Rows.Count;
                        // Validasi Transaction Number----------------------------------------------------------------------------------------
                        if (loRtnValid1 == 1)
                        {
                            loEx.Add(GetError("PS004"));
                        }
                    }

                    // Run Insert-----------------------------------------------------------------------------------

                    // Insert FAT_TRANS_HD
                    loCmd.CommandText = " DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                                        " INSERT INTO FAT_TRANS_HD (   " +
                                        " CCOMPANY_ID              " +
                                        " ,CDEPT_CODE              " +
                                        " ,CTRANSACTION_CODE       " +
                                        " ,CREFERENCE_NO           " +
                                        " ,CSUPPLIER_ID            " +
                                        " ,CINFO_SEQNO             " +
                                        " ,CSUPPLIER_NAME          " +
                                        " ,CTRANSACTION_DATE       " +
                                        " ,CTRANSACTION_PRD        " +
                                        " ,CTRANSACTION_DESCR      " +
                                        " ,CDOCUMENT_DATE          " +
                                        " ,CDOCUMENT_NO            " +
                                        " ,CCURRENCY_CODE          " +
                                        " ,CFR_MODULE              " +
                                        " ,CFR_DEPT_CODE           " +
                                        " ,CFR_TRANSACTION_CODE    " +
                                        " ,CFR_REFERENCE_NO        " +
                                        " ,NLBASE_RATE_AMOUNT      " +
                                        " ,NLCURRENCY_RATE_AMOUNT  " +
                                        " ,NBBASE_RATE_AMOUNT      " +
                                        " ,NBCURRENCY_RATE_AMOUNT  " +
                                        " ,NTRANSACTION_AMOUNT     " +
                                        " ,NLTRANSACTION_AMOUNT    " +
                                        " ,NBTRANSACTION_AMOUNT    " +
                                        " ,CSTATUS                 " +
                                        " ,LGLLINK                 " +
                                        " ,CGL_TRF_STATUS          " +
                                        " ,CGL_REFERENCE_NO        " +
                                        " ,CAPPROVED_BY            " +
                                        " ,DAPPROVED_DATE          " +
                                        " ,CCOMMIT_BY              " +
                                        " ,DCOMMIT_DATE            " +
                                        " ,CCANCEL_REASON_CODE     " +
                                        " ,CCANCEL_APPROVED_BY     " +
                                        " ,CCANCEL_BY              " +
                                        " ,DCANCEL_DATE            " +
                                        " ,CCREATE_BY              " +
                                        " ,DCREATE_DATE            " +
                                        " ,CUPDATE_BY              " +
                                        " ,DUPDATE_DATE)           " +
                                        " VALUES(@CCOMPANY_ID,     " +
                                        " @CDEPT_CODE,             " +
                                        " @CTRANSACTION_CODE,      " +
                                        " @CREFERENCE_NO,          " +
                                        " @CSUPPLIER_ID,           " +
                                        " @CINFO_SEQNO,            " +
                                        " @CSUPPLIER_NAME,         " +
                                        " @CTRANSACTION_DATE,      " +
                                        " @CTRANSACTION_PRD,       " +
                                        " @CTRANSACTION_DESCR,     " +
                                        " @CDOCUMENT_DATE,         " +
                                        " @CDOCUMENT_NO,           " +
                                        " @CCURRENCY_CODE,         " +
                                        " @CFR_MODULE,             " +
                                        " @CFR_DEPT_CODE,          " +
                                        " @CFR_TRANSACTION_CODE,   " +
                                        " @CFR_REFERENCE_NO,       " +
                                        " @NLBASE_RATE_AMOUNT,     " +
                                        " @NLCURRENCY_RATE_AMOUNT, " +
                                        " @NBBASE_RATE_AMOUNT,     " +
                                        " @NBCURRENCY_RATE_AMOUNT, " +
                                        " @NTRANSACTION_AMOUNT,    " +
                                        " @NLTRANSACTION_AMOUNT,   " +
                                        " @NBTRANSACTION_AMOUNT,   " +
                                        " @CSTATUS,                " +
                                        " @LGLLINK,                " +
                                        " @CGL_TRF_STATUS,         " +
                                        " @CGL_REFERENCE_NO,       " +
                                        " @CAPPROVED_BY,           " +
                                        " @DAPPROVED_DATE,         " +
                                        " @CCOMMIT_BY,             " +
                                        " @DCOMMIT_DATE,           " +
                                        " @CCANCEL_REASON_CODE,    " +
                                        " @CCANCEL_APPROVED_BY,    " +
                                        " @CCANCEL_BY,             " +
                                        " @DCANCEL_DATE,           " +
                                        " @CCREATE_BY,             " +
                                        " @DATENOW,                " +
                                        " @CUPDATE_BY,             " +
                                        " @DATENOW) ";
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CINFO_SEQNO", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_NAME", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_PRD", DbType.String, 50, loRtnPeriod.CPRD);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 50, poNewEntity.CTRANSACTION_DESCR);
                    loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_DATE", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_NO", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_MODULE", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CFR_TRANSACTION_CODE", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CFR_REFERENCE_NO", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLBASE_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBBASE_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NTRANSACTION_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NLTRANSACTION_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NBTRANSACTION_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poNewEntity.CSTATUS);
                    loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 1, poNewEntity.LGLLINK); // Dari Depan (Innit Process)
                    loDb.R_AddCommandParameter(loCmd, "@CGL_TRF_STATUS", DbType.String, 50, poNewEntity.CGL_TRF_STATUS);
                    loDb.R_AddCommandParameter(loCmd, "@CGL_REFERENCE_NO", DbType.String, 50, poNewEntity.CGL_REFERENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CAPPROVED_BY", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@DAPPROVED_DATE", DbType.DateTime, 0, DBNull.Value); // ignored - use DBNull.Value instead of null
                    loDb.R_AddCommandParameter(loCmd, "@CCOMMIT_BY", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@DCOMMIT_DATE", DbType.DateTime, 0, DBNull.Value); // ignored - use DBNull.Value instead of null
                    loDb.R_AddCommandParameter(loCmd, "@CCANCEL_REASON_CODE", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CCANCEL_APPROVED_BY", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@CCANCEL_BY", DbType.String, 50, ""); // ignored
                    loDb.R_AddCommandParameter(loCmd, "@DCANCEL_DATE", DbType.DateTime, 0, DBNull.Value); // ignored - use DBNull.Value instead of null
                    // @DCREATE_DATE
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                    // @DUPDATE_DATE

                    var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@"))
                        .ToDictionary(x => x.ParameterName, x => x.Value);

                    _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                    // Insert FAT_TRANS_ASSET
                    loCmd.CommandText = " DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                                        " INSERT INTO FAT_TRANS_ASSET (  " +
                                        " CCOMPANY_ID                " +
                                        " ,CDEPT_CODE                " +
                                        " ,CTRANSACTION_CODE         " +
                                        " ,CREFERENCE_NO             " +
                                        " ,CASSET_CODE               " +
                                        " ,CTRANS_SEQNO              " +
                                        " ,CASSET_TRANS_SEQNO        " +
                                        " ,CTRANSACTION_DATE         " +
                                        " ,NBBASE_RATE_AMOUNT        " +
                                        " ,NBCURRENCY_RATE_AMOUNT    " +
                                        " ,NTRANSACTION_AMOUNT1      " +
                                        " ,NTRANSACTION_AMOUNT2      " +
                                        " ,NTRANSACTION_AMOUNT3      " +
                                        " ,NTRANSACTION_AMOUNT4      " +
                                        " ,NTRANSACTION_AMOUNT5      " +
                                        " ,NTRANSACTION_AMOUNT6      " +
                                        " ,NLTRANSACTION_AMOUNT1     " +
                                        " ,NLTRANSACTION_AMOUNT2     " +
                                        " ,NLTRANSACTION_AMOUNT3     " +
                                        " ,NLTRANSACTION_AMOUNT4     " +
                                        " ,NLTRANSACTION_AMOUNT5     " +
                                        " ,NLTRANSACTION_AMOUNT6     " +
                                        " ,NBTRANSACTION_AMOUNT1     " +
                                        " ,NBTRANSACTION_AMOUNT2     " +
                                        " ,NBTRANSACTION_AMOUNT3     " +
                                        " ,NBTRANSACTION_AMOUNT4     " +
                                        " ,NBTRANSACTION_AMOUNT5     " +
                                        " ,NBTRANSACTION_AMOUNT6     " +
                                        " ,ITRANSACTION_QTY1         " +
                                        " ,ITRANSACTION_QTY2         " +
                                        " ,CUNIT                     " +
                                        " ,CALLOC_EXPENSE_CODE       " +
                                        " ,CFR_DEPT_CODE             " +
                                        " ,CFR_TRANSACTION_CODE      " +
                                        " ,CFR_REFERENCE_NO          " +
                                        " ,CFR_TRANSACTION_DATE      " +
                                        " ,CFR_SEQUENCE_NO           " +
                                        " ,CTRANSACTION_DESCR        " +
                                        " ,CASSET_DEPT_CODE          " +
                                        " ,CASSET_LOCATION           " +
                                        " ,CJRNGRP_CODE              " +
                                        " ,CTAX_CATEGORY_CODE        " +
                                        " ,CCATEGORY_CODE            " +
                                        " ,CSCATEGORY_CODE           " +
                                        " ,CDEPR_METHOD              " +
                                        " ,CSTART_DATE               " +
                                        " ,NLBOOK_VALUE              " +
                                        " ,NBBOOK_VALUE              " +
                                        " ,IUSEFUL_LIVE              " +
                                        " ,NLYEAR_DEPR_AMT           " +
                                        " ,NBYEAR_DEPR_AMT           " +
                                        " ,NLRESIDUAL_VALUE          " +
                                        " ,NBRESIDUAL_VALUE          " +
                                        " ,NYEAR_DEPR_PCT            " +
                                        " ,COASSET_DEPT_CODE         " +
                                        " ,COASSET_LOCATION          " +
                                        " ,COJRNGRP_CODE             " +
                                        " ,COTAX_CATEGORY_CODE       " +
                                        " ,COCATEGORY_CODE           " +
                                        " ,COSCATEGORY_CODE          " +
                                        " ,CODEPR_METHOD             " +
                                        " ,COSTART_DATE              " +
                                        " ,NOLBOOK_VALUE             " +
                                        " ,NOBBOOK_VALUE             " +
                                        " ,IOUSEFUL_LIVE             " +
                                        " ,NOLYEAR_DEPR_AMT          " +
                                        " ,NOBYEAR_DEPR_AMT          " +
                                        " ,NOLRESIDUAL_VALUE         " +
                                        " ,NOBRESIDUAL_VALUE         " +
                                        " ,NOYEAR_DEPR_PCT           " +
                                        " ,IOTRANSACTION_QTY1        " +
                                        " ,LDELETE_FLAG              " +
                                        " ,CDEPR_STATUS              " +
                                        " ,CCURRENT_PRD              " +
                                        " ,CLLINK_FLAG               " +
                                        " ,CCREATE_BY                " +
                                        " ,DCREATE_DATE              " +
                                        " ,CUPDATE_BY                " +
                                        " ,DUPDATE_DATE)             " +
                                        " VALUES( @CCOMPANY_ID,      " +
                                        " @CDEPT_CODE,               " +
                                        " @CTRANSACTION_CODE,        " +
                                        " @CREFERENCE_NO,            " +
                                        " @CASSET_CODE,              " +
                                        " @CTRANS_SEQNO,             " +
                                        " @CASSET_TRANS_SEQNO,       " +
                                        " @CTRANSACTION_DATE,        " +
                                        " @NBBASE_RATE_AMOUNT,       " +
                                        " @NBCURRENCY_RATE_AMOUNT,   " +
                                        " @NTRANSACTION_AMOUNT1,     " +
                                        " @NTRANSACTION_AMOUNT2,     " +
                                        " @NTRANSACTION_AMOUNT3,     " +
                                        " @NTRANSACTION_AMOUNT4,     " +
                                        " @NTRANSACTION_AMOUNT5,     " +
                                        " @NTRANSACTION_AMOUNT6,     " +
                                        " @NLTRANSACTION_AMOUNT1,    " +
                                        " @NLTRANSACTION_AMOUNT2,    " +
                                        " @NLTRANSACTION_AMOUNT3,    " +
                                        " @NLTRANSACTION_AMOUNT4,    " +
                                        " @NLTRANSACTION_AMOUNT5,    " +
                                        " @NLTRANSACTION_AMOUNT6,    " +
                                        " @NBTRANSACTION_AMOUNT1,    " +
                                        " @NBTRANSACTION_AMOUNT2,    " +
                                        " @NBTRANSACTION_AMOUNT3,    " +
                                        " @NBTRANSACTION_AMOUNT4,    " +
                                        " @NBTRANSACTION_AMOUNT5,    " +
                                        " @NBTRANSACTION_AMOUNT6,    " +
                                        " @ITRANSACTION_QTY1,        " +
                                        " @ITRANSACTION_QTY2,        " +
                                        " @CUNIT,                    " +
                                        " @CALLOC_EXPENSE_CODE,      " +
                                        " @CFR_DEPT_CODE,            " +
                                        " @CFR_TRANSACTION_CODE,     " +
                                        " @CFR_REFERENCE_NO,         " +
                                        " @CFR_TRANSACTION_DATE,     " +
                                        " @CFR_SEQUENCE_NO,          " +
                                        " @CTRANSACTION_DESCR,       " +
                                        " @CASSET_DEPT_CODE,         " +
                                        " @CASSET_LOCATION,          " +
                                        " @CJRNGRP_CODE,             " +
                                        " @CTAX_CATEGORY_CODE,       " +
                                        " @CCATEGORY_CODE,           " +
                                        " @CSCATEGORY_CODE,          " +
                                        " @CDEPR_METHOD,             " +
                                        " @CSTART_DATE,              " +
                                        " @NLBOOK_VALUE,             " +
                                        " @NBBOOK_VALUE,             " +
                                        " @IUSEFUL_LIVE,             " +
                                        " @NLYEAR_DEPR_AMT,          " +
                                        " @NBYEAR_DEPR_AMT,          " +
                                        " @NLRESIDUAL_VALUE,         " +
                                        " @NBRESIDUAL_VALUE,         " +
                                        " @NYEAR_DEPR_PCT,           " +
                                        " @COASSET_DEPT_CODE,        " +
                                        " @COASSET_LOCATION,         " +
                                        " @COJRNGRP_CODE,            " +
                                        " @COTAX_CATEGORY_CODE,      " +
                                        " @COCATEGORY_CODE,          " +
                                        " @COSCATEGORY_CODE,         " +
                                        " @CODEPR_METHOD,            " +
                                        " @COSTART_DATE,             " +
                                        " @NOLBOOK_VALUE,            " +
                                        " @NOBBOOK_VALUE,            " +
                                        " @IOUSEFUL_LIVE,            " +
                                        " @NOLYEAR_DEPR_AMT,         " +
                                        " @NOBYEAR_DEPR_AMT,         " +
                                        " @NOLRESIDUAL_VALUE,        " +
                                        " @NOBRESIDUAL_VALUE,        " +
                                        " @NOYEAR_DEPR_PCT,          " +
                                        " @IOTRANSACTION_QTY1,       " +
                                        " @LDELETE_FLAG,             " +
                                        " @CDEPR_STATUS,             " +
                                        " @CCURRENT_PRD,             " +
                                        " @CLLINK_FLAG,              " +
                                        " @CCREATE_BY,               " +
                                        " @DATENOW,                  " +
                                        " @CUPDATE_BY,               " +
                                        " @DATENOW) ";
                    // Set parameter values that were already added
                    if (loCmd.Parameters.Contains("@NBBASE_RATE_AMOUNT"))
                    {
                        loCmd.Parameters["@NBBASE_RATE_AMOUNT"].Value = 0.0m; // Edit Value Parameters Ignored
                    }
                    if (loCmd.Parameters.Contains("@NBCURRENCY_RATE_AMOUNT"))
                    {
                        loCmd.Parameters["@NBCURRENCY_RATE_AMOUNT"].Value = 0.0m; // Edit Value Parameters Ignored
                    }

                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, "000100");
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, loRtnAssetInfo.CNSEQUENCE_NO);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT1", DbType.Decimal, 50, poNewEntity.NTRANSACTION_AMOUNT); // Sama NTRANSACTION_AMOUNT FAT_TRANS_HD
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT2", DbType.Decimal, 50, loRtnAssetInfo.NLFA);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT3", DbType.Decimal, 50, loRtnAssetInfo.NLAD);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT4", DbType.Decimal, 50, loRtnAssetInfo.NLRFA);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT5", DbType.Decimal, 50, loRtnAssetInfo.NLRAD);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT6", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT1", DbType.Decimal, 50, poNewEntity.NLTRANSACTION_AMOUNT); // Sama NLTRANSACTION_AMOUNT FAT_TRANS_HD
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT2", DbType.Decimal, 50, loRtnAssetInfo.NLFA);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT3", DbType.Decimal, 50, loRtnAssetInfo.NLAD);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT4", DbType.Decimal, 50, loRtnAssetInfo.NLRFA);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT5", DbType.Decimal, 50, loRtnAssetInfo.NLRAD);
                    loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT6", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT1", DbType.Decimal, 50, poNewEntity.NBTRANSACTION_AMOUNT); // Sama NBTRANSACTION_AMOUNT FAT_TRANS_HD
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT2", DbType.Decimal, 50, loRtnAssetInfo.NBFA);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT3", DbType.Decimal, 50, loRtnAssetInfo.NBAD);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT4", DbType.Decimal, 50, loRtnAssetInfo.NBRFA);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT5", DbType.Decimal, 50, loRtnAssetInfo.NBRAD);
                    loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT6", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@ITRANSACTION_QTY1", DbType.Int16, 0, 0); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@ITRANSACTION_QTY2", DbType.Int16, 0, 0); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CALLOC_EXPENSE_CODE", DbType.String, 50, poNewEntity.CALLOC_EXPENSE_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CFR_TRANSACTION_DATE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CFR_SEQUENCE_NO", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 50, loRtnAssetInfo.CASSET_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_LOCATION", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 50, loRtnAssetInfo.CJRNGRP_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_CODE", DbType.String, 50, loRtnAssetInfo.CTAX_CATEGORY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_CODE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CSCATEGORY_CODE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 50, loRtnAssetInfo.CDEPR_METHOD);
                    loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NLBOOK_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NBBOOK_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIVE", DbType.Int16, 0, 0); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NLYEAR_DEPR_AMT", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NBYEAR_DEPR_AMT", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NLRESIDUAL_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NBRESIDUAL_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_PCT", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@COASSET_DEPT_CODE", DbType.String, 50, poNewEntity.COASSET_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@COASSET_LOCATION", DbType.String, 50, poNewEntity.COASSET_LOCATION);
                    loDb.R_AddCommandParameter(loCmd, "@COJRNGRP_CODE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@COTAX_CATEGORY_CODE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@COCATEGORY_CODE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@COSCATEGORY_CODE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CODEPR_METHOD", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@COSTART_DATE", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NOLBOOK_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NOBBOOK_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@IOUSEFUL_LIVE", DbType.Int16, 0, 0); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NOLYEAR_DEPR_AMT", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NOBYEAR_DEPR_AMT", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NOLRESIDUAL_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NOBRESIDUAL_VALUE", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@NOYEAR_DEPR_PCT", DbType.Decimal, 50, 0.0m); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@IOTRANSACTION_QTY1", DbType.Int16, 0, 0); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@LDELETE_FLAG", DbType.Boolean, 1, false);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPR_STATUS", DbType.String, 50, ""); // Ignored
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PRD", DbType.String, 50, loRtnPeriod.CPRD);
                    loDb.R_AddCommandParameter(loCmd, "@CLLINK_FLAG", DbType.String, 50, ""); // Ignored

                    loDbParams = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@"))
                        .ToDictionary(x => x.ParameterName, x => x.Value);

                    _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                }
                else // ========================================EDIT MODE==================================================================================
                {
                    // Btn btn Edit------------------------------------------------------------------------------------------------------------------------
                    if (poNewEntity.LCHANGE_DESC == false && poNewEntity.LCHANGE_ALLOC == false)
                    {
                        // Update FAT_TRANS_HD
                        loCmd.CommandText = " DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                                            " UPDATE FAT_TRANS_HD                                " +
                                            " SET CTRANSACTION_DATE = @CTRANSACTION_DATE         " +
                                            " ,CTRANSACTION_PRD  = @CTRANSACTION_PRD             " +
                                            " ,CTRANSACTION_DESCR  = @CTRANSACTION_DESCR         " +
                                            " ,CCURRENCY_CODE  = @CCURRENCY_CODE                 " +
                                            " ,NLBASE_RATE_AMOUNT  = @NLBASE_RATE_AMOUNT         " +
                                            " ,NLCURRENCY_RATE_AMOUNT  = @NLCURRENCY_RATE_AMOUNT " +
                                            " ,NBBASE_RATE_AMOUNT = @NBBASE_RATE_AMOUNT          " +
                                            " ,NBCURRENCY_RATE_AMOUNT = @NBCURRENCY_RATE_AMOUNT  " +
                                            " ,NTRANSACTION_AMOUNT = @NTRANSACTION_AMOUNT        " +
                                            " ,NLTRANSACTION_AMOUNT = @NLTRANSACTION_AMOUNT      " +
                                            " ,NBTRANSACTION_AMOUNT = @NBTRANSACTION_AMOUNT      " +
                                            " ,LGLLINK = @LGLLINK                                " +
                                            " ,CUPDATE_BY  = @CUPDATE_BY                         " +
                                            " ,DUPDATE_DATE = @DATENOW                           " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID                   " +
                                            " AND CDEPT_CODE = @CDEPT_CODE                       " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE         " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO                 ";
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_PRD", DbType.String, 50, loRtnPeriod.CPRD);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 50, poNewEntity.CTRANSACTION_DESCR);
                        loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLBASE_RATE_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBBASE_RATE_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NTRANSACTION_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NLTRANSACTION_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT", DbType.Decimal, 50, poNewEntity.NBTRANSACTION_AMOUNT);
                        loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 1, poNewEntity.LGLLINK);
                        loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                        // @DUPDATE_DATE
                        // ParamWhere
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);

                        var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                            .Where(x => x != null && x.ParameterName.StartsWith("@"))
                            .ToDictionary(x => x.ParameterName, x => x.Value);

                        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                        // Update FAT_TRANS_ASSET
                        loCmd.CommandText = " DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                                            " UPDATE FAT_TRANS_ASSET                           " +
                                            " SET CASSET_CODE  = @CASSET_CODE                  " +
                                            " ,CASSET_TRANS_SEQNO   = @CASSET_TRANS_SEQNO      " +
                                            " ,CTRANSACTION_DATE   = @CTRANSACTION_DATE        " +
                                            " ,NTRANSACTION_AMOUNT1 = @NTRANSACTION_AMOUNT1    " +
                                            " ,NTRANSACTION_AMOUNT2 = @NTRANSACTION_AMOUNT2    " +
                                            " ,NTRANSACTION_AMOUNT3 = @NTRANSACTION_AMOUNT3    " +
                                            " ,NTRANSACTION_AMOUNT4 = @NTRANSACTION_AMOUNT4    " +
                                            " ,NTRANSACTION_AMOUNT5 = @NTRANSACTION_AMOUNT5    " +
                                            " ,NLTRANSACTION_AMOUNT1 = @NLTRANSACTION_AMOUNT1  " +
                                            " ,NLTRANSACTION_AMOUNT2 = @NLTRANSACTION_AMOUNT2  " +
                                            " ,NLTRANSACTION_AMOUNT3 = @NLTRANSACTION_AMOUNT3  " +
                                            " ,NLTRANSACTION_AMOUNT4 = @NLTRANSACTION_AMOUNT4  " +
                                            " ,NLTRANSACTION_AMOUNT5 = @NLTRANSACTION_AMOUNT5  " +
                                            " ,NBTRANSACTION_AMOUNT1 = @NBTRANSACTION_AMOUNT1   " +
                                            " ,NBTRANSACTION_AMOUNT2 = @NBTRANSACTION_AMOUNT2  " +
                                            " ,NBTRANSACTION_AMOUNT3 = @NBTRANSACTION_AMOUNT3  " +
                                            " ,NBTRANSACTION_AMOUNT4 = @NBTRANSACTION_AMOUNT4  " +
                                            " ,NBTRANSACTION_AMOUNT5 = @NBTRANSACTION_AMOUNT5  " +
                                            " ,CALLOC_EXPENSE_CODE  = @CALLOC_EXPENSE_CODE     " +
                                            " ,CTRANSACTION_DESCR  = @CTRANSACTION_DESCR       " +
                                            " ,CASSET_DEPT_CODE = @CASSET_DEPT_CODE            " +
                                            " ,CJRNGRP_CODE = @CJRNGRP_CODE                    " +
                                            " ,CTAX_CATEGORY_CODE = @CTAX_CATEGORY_CODE        " +
                                            " ,CDEPR_METHOD = @CDEPR_METHOD                    " +
                                            " ,CCURRENT_PRD = @CCURRENT_PRD                    " +
                                            " ,CUPDATE_BY = @CUPDATE_BY                        " +
                                            " ,DUPDATE_DATE = @DATENOW                         " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID                 " +
                                            " AND CDEPT_CODE = @CDEPT_CODE                     " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE       " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO               " +
                                            " AND CTRANS_SEQNO = @CTRANS_SEQNO				   ";
                        loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQNO", DbType.String, 50, "000100");
                        loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, loRtnAssetInfo.CNSEQUENCE_NO);
                        loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT1", DbType.Decimal, 50, poNewEntity.NTRANSACTION_AMOUNT1); // From Form Front
                        loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT2", DbType.Decimal, 50, loRtnAssetInfo.NLFA);
                        loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT3", DbType.Decimal, 50, loRtnAssetInfo.NLAD);
                        loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT4", DbType.Decimal, 50, loRtnAssetInfo.NLRFA);
                        loDb.R_AddCommandParameter(loCmd, "@NTRANSACTION_AMOUNT5", DbType.Decimal, 50, loRtnAssetInfo.NLRAD);
                        loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT1", DbType.Decimal, 50, poNewEntity.NLTRANSACTION_AMOUNT1); // From Form Front
                        loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT2", DbType.Decimal, 50, loRtnAssetInfo.NLFA);
                        loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT3", DbType.Decimal, 50, loRtnAssetInfo.NLAD);
                        loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT4", DbType.Decimal, 50, loRtnAssetInfo.NLRFA);
                        loDb.R_AddCommandParameter(loCmd, "@NLTRANSACTION_AMOUNT5", DbType.Decimal, 50, loRtnAssetInfo.NLRAD);
                        loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT1", DbType.Decimal, 50, poNewEntity.NBTRANSACTION_AMOUNT1); // From Form Front
                        loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT2", DbType.Decimal, 50, loRtnAssetInfo.NBFA);
                        loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT3", DbType.Decimal, 50, loRtnAssetInfo.NBAD);
                        loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT4", DbType.Decimal, 50, loRtnAssetInfo.NBRFA);
                        loDb.R_AddCommandParameter(loCmd, "@NBTRANSACTION_AMOUNT5", DbType.Decimal, 50, loRtnAssetInfo.NBRAD);
                        loDb.R_AddCommandParameter(loCmd, "@CALLOC_EXPENSE_CODE", DbType.String, 50, poNewEntity.CALLOC_EXPENSE_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 50, loRtnAssetInfo.CASSET_DEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 50, loRtnAssetInfo.CJRNGRP_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_CODE", DbType.String, 50, loRtnAssetInfo.CTAX_CATEGORY_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 50, loRtnAssetInfo.CDEPR_METHOD);
                        loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PRD", DbType.String, 50, loRtnPeriod.CPRD);

                        loDbParams = loCmd.Parameters.Cast<DbParameter>()
                            .Where(x => x != null && x.ParameterName.StartsWith("@"))
                            .ToDictionary(x => x.ParameterName, x => x.Value);

                        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                        goto EndMethod;
                    }
                    // Btn Change Desc------------------------------------------------------------------------------------------------------------------------
                    if (poNewEntity.LCHANGE_DESC == true)
                    {
                        // Param
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DESCR", DbType.String, 50, poNewEntity.CTRANSACTION_DESCR);
                        loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                        // @DUPDATE_DATE
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);

                        // Update FAT_TRANS_HD
                        loCmd.CommandText = " DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                                            " UPDATE FAT_TRANS_HD                                " +
                                            " SET CTRANSACTION_DESCR  = @CTRANSACTION_DESCR      " +
                                            " ,CUPDATE_BY  = @CUPDATE_BY                         " +
                                            " ,DUPDATE_DATE = @DATENOW                           " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID                   " +
                                            " AND CDEPT_CODE = @CDEPT_CODE                       " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE         " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO                 ";

                        var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                            .Where(x => x != null && x.ParameterName.StartsWith("@"))
                            .ToDictionary(x => x.ParameterName, x => x.Value);

                        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                        // Update FAT_TRANS_ASSET
                        loCmd.CommandText = " DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                                            " UPDATE FAT_TRANS_ASSET                             " +
                                            " SET CTRANSACTION_DESCR  = @CTRANSACTION_DESCR      " +
                                            " ,CUPDATE_BY  = @CUPDATE_BY                         " +
                                            " ,DUPDATE_DATE = @DATENOW                           " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID                   " +
                                            " AND CDEPT_CODE = @CDEPT_CODE                       " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE         " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO                 ";

                        loDbParams = loCmd.Parameters.Cast<DbParameter>()
                            .Where(x => x != null && x.ParameterName.StartsWith("@"))
                            .ToDictionary(x => x.ParameterName, x => x.Value);

                        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                        goto EndMethod;
                    }
                    // Btn Change Allocation------------------------------------------------------------------------------------------------------------------------
                    if (poNewEntity.LCHANGE_ALLOC == true)
                    {
                        // Param
                        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poNewEntity.CREFERENCE_NO);
                        loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                        // EXEC RSP RSP_FA_TRANSACTION_JOURNAL
                        loCmd.CommandText = " EXEC RSP_FA_TRANSACTION_JOURNAL 'DELETE', @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";

                        var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                            .Where(x => x != null && x.ParameterName.StartsWith("@"))
                            .ToDictionary(x => x.ParameterName, x => x.Value);

                        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                        // Update FAT_TRANS_ASSET
                        loCmd.CommandText = " DECLARE @DATENOW DATETIME = dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID)" +
                                            " UPDATE FAT_TRANS_ASSET                         " +
                                            " SET CALLOC_EXPENSE_CODE = @CALLOC_EXPENSE_CODE " +
                                            " ,CUPDATE_BY  = @CUPDATE_BY                     " +
                                            " ,DUPDATE_DATE = @DATENOW                       " +
                                            " WHERE CCOMPANY_ID = @CCOMPANY_ID               " +
                                            " AND CDEPT_CODE = @CDEPT_CODE                   " +
                                            " AND CTRANSACTION_CODE = @CTRANSACTION_CODE     " +
                                            " AND CREFERENCE_NO = @CREFERENCE_NO             ";
                        loDb.R_AddCommandParameter(loCmd, "@CALLOC_EXPENSE_CODE", DbType.String, 50, poNewEntity.CALLOC_EXPENSE_CODE);
                        loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.CUPDATE_BY);
                        // @DUPDATE_DATE

                        loDbParams = loCmd.Parameters.Cast<DbParameter>()
                            .Where(x => x != null && x.ParameterName.StartsWith("@"))
                            .ToDictionary(x => x.ParameterName, x => x.Value);

                        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

                        // EXEC RSP RSP_FA_TRANSACTION_JOURNAL
                        loCmd.CommandText = " EXEC RSP_FA_TRANSACTION_JOURNAL 'ADD', @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";

                        loDbParams = loCmd.Parameters.Cast<DbParameter>()
                            .Where(x => x != null && x.ParameterName.StartsWith("@"))
                            .ToDictionary(x => x.ParameterName, x => x.Value);

                        _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    }
                }

            EndMethod:;
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

        #region Init Process

        /// <summary>
        /// Get period information from FAM_SYSTEM
        /// </summary>
        /// <param name="poParam">Parameter containing company ID</param>
        /// <returns>Result DTO with period information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetPeriodResultDTO>> GetPeriodAsync(FAT00800GetPeriodParameterDTO poParam)
        {
            string lcMethod = nameof(GetPeriodAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetPeriodResultDTO>
            {
                Data = new FAT00800GetPeriodResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CTRANS_DEPT_CODE as CDEFAULT_TRX_DEPT_CODE, " +
                                    " CSOFT_PERIOD, CCURRENT_PERIOD ,CGLLINK_DATE, CRATETYPE_CODE " +
                                    " FROM FAM_SYSTEM (nolock) WHERE CCOMPANY_ID=@CCOMPANY_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.CDEFAULT_TRX_DEPT_CODE = loRtn.CDEFAULT_TRX_DEPT_CODE;
                    loResult.Data.CSOFT_PERIOD = loRtn.CSOFT_PERIOD;
                    loResult.Data.CCURRENT_PERIOD = loRtn.CCURRENT_PERIOD;
                    loResult.Data.CGLLINK_DATE = loRtn.CGLLINK_DATE;
                    loResult.Data.CRATETYPE_CODE = loRtn.CRATETYPE_CODE;
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
        /// Get local and base currency information
        /// </summary>
        /// <param name="poParam">Parameter containing company ID</param>
        /// <returns>Result DTO with currency information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>> GetLocalBaseCurrAsync(FAT00800GetLocalBaseCurrParameterDTO poParam)
        {
            string lcMethod = nameof(GetLocalBaseCurrAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>
            {
                Data = new FAT00800GetLocalBaseCurrResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CLOCAL_CURRENCY_CODE, CBASE_CURRENCY_CODE,   " +
                                    " LCUST_PERIOD_FLAG FROM HSM_PROPERTY_SYSTEM (nolock) " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.CLOCAL_CURRENCY_CODE = loRtn.CLOCAL_CURRENCY_CODE;
                    loResult.Data.CBASE_CURRENCY_CODE = loRtn.CBASE_CURRENCY_CODE;
                    loResult.Data.LCUST_PERIOD_FLAG = loRtn.LCUST_PERIOD_FLAG;
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
        /// Get transaction type description
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, language ID, and transaction code</param>
        /// <returns>Result DTO with transaction description and flags</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>> GetTransTypeDescAsync(FAT00800GetTransTypeDescParameterDTO poParam)
        {
            string lcMethod = nameof(GetTransTypeDescAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>
            {
                Data = new FAT00800GetTransTypeDescResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT ISNULL(b.CDESCRIPTION, a.CTRANSACTION_NAME) AS CTRANS_DESC            " +
                                    " ,LAPPROVAL_FLAG AS LTRANS_APPROVAL ,LINCREMENT_FLAG AS LINCREMENT_FLAG       " +
                                    " FROM GSM_TRANSACTION_CODE a(NOLOCK)                                          " +
                                    " LEFT JOIN GSB_TRANSLATE b(NOLOCK) ON b.CTABLE_NAME = 'GSM_TRANSACTION_CODE'  " +
                                    " AND B.CFOREIGN_LANGUAGE = @CLANGID                                 		   " +
                                    " AND B.CKEY_ID = a.CCOMPANY_ID + a.CTRANSACTION_CODE                          " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID  AND CTRANSACTION_CODE = @CTRANSACTION_CODE ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poParam.CLANG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.CTRANS_DESC = loRtn.CTRANS_DESC;
                    loResult.Data.LTRANS_APPROVAL = loRtn.LTRANS_APPROVAL;
                    loResult.Data.LINCREMENT_FLAG = loRtn.LINCREMENT_FLAG;
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
        /// Get user right approval - returns integer (row count)
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, transaction code, and user ID</param>
        /// <returns>Result DTO with integer result (row count)</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>> GetUserRightApprovalAsync(FAT00800GetUserRightApprovalParameterDTO poParam)
        {
            string lcMethod = nameof(GetUserRightApprovalAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>
            {
                Data = new FAT00800GetUserRightApprovalResultDTO { Result = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT TOP 1 1 FROM FAM_APPROVAL_USER (nolock) " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CTRANSACTION_CODE=@CTRANSACTION_CODE AND CUSER_ID=@CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult.Data.Result = loDataTable.Rows.Count;
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
        /// Get user activity rights - returns integer (row count)
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, activity code, and user ID</param>
        /// <returns>Result DTO with integer result (row count)</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>> GetUserActivityRightsAsync(FAT00800GetUserActivityRightsParameterDTO poParam)
        {
            string lcMethod = nameof(GetUserActivityRightsAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>
            {
                Data = new FAT00800GetUserActivityRightsResultDTO { Result = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT TOP 1 1 FROM GSM_USER_RIGHT (nolock) " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CACTIVITY_CODE=@CACTIVITY_CODE AND CUSER_ID=@CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVITY_CODE", DbType.String, 50, poParam.CACTIVITY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult.Data.Result = loDataTable.Rows.Count;
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
        /// Get validate department - returns integer (row count)
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, department code, and user ID</param>
        /// <returns>Result DTO with integer result (row count)</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>> GetValidateDepartmentAsync(FAT00800GetValidateDepartmentParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateDepartmentAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>
            {
                Data = new FAT00800GetValidateDepartmentResultDTO { Result = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " Select TOP 1 1 From GSX_DEPARTMENT_USER (nolock) " +
                                    " Where CCOMPANY_ID = @CCOMPANY_ID " +
                                    " and CDEPT_CODE= @CDEPT_CODE " +
                                    " AND CUSER_ID = @CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult.Data.Result = loDataTable.Rows.Count;
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

        #region Validation

        /// <summary>
        /// Validate transaction date
        /// </summary>
        /// <param name="poParam">Parameter containing company ID and transaction date</param>
        /// <returns>Result DTO with period information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>> GetValidateTransDateAsync(FAT00800GetValidateTransDateParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateTransDateAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>
            {
                Data = new FAT00800GetValidateTransDateResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CPRD=CCYEAR+CPERIOD_NO FROM GSM_PERIOD_DT (nolock)     " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID and @CTRANSACTION_DATE BETWEEN " +
                                    " CSTART_DATE AND CEND_DATE ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poParam.CTRANSACTION_DATE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.CPRD = loRtn.CPRD;
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
        /// Validate outstanding transaction
        /// </summary>
        /// <param name="poParam">Parameter containing company ID and asset code</param>
        /// <returns>Result DTO with asset code if outstanding transaction exists</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>> GetValidateOutstandTransAsync(FAT00800GetValidateOutstandTransParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateOutstandTransAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>
            {
                Data = new FAT00800GetValidateOutstandTransResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT a.CASSET_CODE FROM FAT_TRANS_ASSET a(NOLOCK)       " +
                                    " ,FAM_ASSET b(NOLOCK)                                      " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID                        " +
                                    " AND a.CASSET_CODE = @CASSET_CODE                          " +
                                    " AND a.LDELETE_FLAG = 0 AND b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " AND b.CASSET_CODE = a.CASSET_CODE                         " +
                                    " AND a.CASSET_TRANS_SEQNO > b.CLSEQUENCE_NO                ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.CASSET_CODE = loRtn.CASSET_CODE;
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
        /// Validate void transaction
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, asset code, and asset trans seqno</param>
        /// <returns>Result DTO with asset code if void is not allowed</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>> GetValidateVoidAsync(FAT00800GetValidateVoidParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateVoidAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>
            {
                Data = new FAT00800GetValidateVoidResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT TOP 1 CASSET_CODE      " +
                                    " FROM FAT_TRANS_ASSET (nolock) " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CASSET_CODE=@CASSET_CODE AND " +
                                    " CASSET_TRANS_SEQNO > @CASSET_TRANS_SEQNO AND LDELETE_FLAG=0 ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, poParam.CASSET_TRANS_SEQNO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.CASSET_CODE = loRtn.CASSET_CODE;
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

        #region Button

        /// <summary>
        /// Submit transaction - executes RSP_FAT_SUBMIT
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, department code, transaction code, reference no, and user ID</param>
        public async Task DoSubmitAsync(FAT00800DoSubmitParameterDTO poParam)
        {
            string lcMethod = nameof(DoSubmitAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " EXEC RSP_FAT_SUBMIT @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

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
        /// Close transaction - executes RSP_FAT_CLOSE
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, department code, transaction code, reference no, and user ID</param>
        public async Task DoCloseAsync(FAT00800DoCloseParameterDTO poParam)
        {
            string lcMethod = nameof(DoCloseAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " EXEC RSP_FAT_CLOSE @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

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
        /// Validate GL - executes RSP_FA_VALIDATE_JOURNAL with exception handling
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, department code, transaction code, and reference no</param>
        public async Task GetValidateGLAsync(FAT00800GetValidateGLParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateGLAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                // Init Exception
                R_ExternalException.R_SP_Init_Exception(loConn);

                loCmd.CommandText = " EXEC RSP_FA_VALIDATE_JOURNAL @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                // Get Exception
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
        /// Approve transaction - executes RSP_FAT_APPROVE
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, department code, transaction code, reference no, and user ID</param>
        public async Task DoApproveAsync(FAT00800DoApproveParameterDTO poParam)
        {
            string lcMethod = nameof(DoApproveAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " EXEC RSP_FAT_APPROVE @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

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
        /// Void transaction - executes RSP_FAT_VOID
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, department code, transaction code, reference no, user ID, cancel reason code, and cancel approved by</param>
        public async Task DoVoidAsync(FAT00800DoVoidParameterDTO poParam)
        {
            string lcMethod = nameof(DoVoidAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " EXEC RSP_FAT_VOID @CCOMPANY_ID, @CDEPT_CODE, @CTRANSACTION_CODE, @CREFERENCE_NO, @CUSER_ID , @CCANCEL_REASON_CODE, @CCANCEL_APPROVED_BY ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_REASON_CODE", DbType.String, 50, poParam.CCANCEL_REASON_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_APPROVED_BY", DbType.String, 50, poParam.CCANCEL_APPROVED_BY);

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
        /// Get approval precheck - returns boolean
        /// NOTE: VB.NET code has type conversion issue (converts to Boolean but checks for integer value 2) - preserved as-is
        /// </summary>
        /// <param name="poParam">Parameter containing company ID</param>
        /// <returns>Result DTO with boolean result (true if IAPPROVAL_OPTION = 2)</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>> GetApprovalPrecheckAsync(FAT00800GetApprovalPrecheckParameterDTO poParam)
        {
            string lcMethod = nameof(GetApprovalPrecheckAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>
            {
                Data = new FAT00800GetApprovalPrecheckResultDTO { Result = false }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT IAPPROVAL_OPTION FROM GSM_ACTIVITY_APPROVAL (NOLOCK)            " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID AND CAPPROVAL_CODE = @CAPPROVAL_CODE  ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_CODE", DbType.String, 50, "FA013002");

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null && loRtn.IAPPROVAL_OPTION == 2)
                {
                    loResult.Data.Result = true;
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

        #region Display

        /// <summary>
        /// Get book value for asset
        /// </summary>
        /// <param name="poParam">Parameter containing company ID and asset code</param>
        /// <returns>Result DTO with book value information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetBookValueResultDTO>> GetBookValueAsync(FAT00800GetBookValueParameterDTO poParam)
        {
            string lcMethod = nameof(GetBookValueAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetBookValueResultDTO>
            {
                Data = new FAT00800GetBookValueResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT NLBOOKVAL = NLBOOK_VALUE - NLREVALUATION_AMT + NLPRIOR_REVALUATION_AMT + NLYTD_REVALUATION_AMT,  " +
                                    " NBBOOKVAL = NBBOOK_VALUE - NBREVALUATION_AMT + NBPRIOR_REVALUATION_AMT + NBYTD_REVALUATION_AMT          " +
                                    " FROM FAM_ASSET (nolock) WHERE CCOMPANY_ID=@CCOMPANY_ID AND CASSET_CODE=@CASSET_CODE ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.NLBOOKVAL = loRtn.NLBOOKVAL;
                    loResult.Data.NBBOOKVAL = loRtn.NBBOOKVAL;
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
        /// Get currency rate information using RSP_GET_CURRENCY_RATE
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, currency code, rate type code, and transaction date</param>
        /// <returns>Result DTO with currency rate information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>> GetCurrencyAsync(FAT00800GetCurrencyParameterDTO poParam)
        {
            string lcMethod = nameof(GetCurrencyAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>
            {
                Data = new FAT00800GetCurrencyResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " DECLARE @NLBASE_RATE_AMOUNT AS NUMERIC DECLARE @NBBASE_RATE_AMOUNT AS NUMERIC                         " +
                                    " DECLARE @NLCURRENCY_RATE_AMOUNT AS NUMERIC DECLARE @NBCURRENCY_RATE_AMOUNT AS NUMERIC                 " +
                                    " EXEC RSP_GET_CURRENCY_RATE @CCOMPANY_ID, @CCURRENCY_CODE ,@CRATETYPE_CODE , @CTRANSACTION_DATE        " +
                                    " ,@NLBASE_RATE_AMOUNT OUTPUT ,@NBBASE_RATE_AMOUNT OUTPUT                                               " +
                                    " ,@NLCURRENCY_RATE_AMOUNT OUTPUT ,@NBCURRENCY_RATE_AMOUNT OUTPUT                                       " +
                                    " SELECT @NLBASE_RATE_AMOUNT AS NLBASE_RATE_AMOUNT, @NBBASE_RATE_AMOUNT AS NBBASE_RATE_AMOUNT,          " +
                                    " @NLCURRENCY_RATE_AMOUNT AS NLCURRENCY_RATE_AMOUNT, @NBCURRENCY_RATE_AMOUNT AS NBCURRENCY_RATE_AMOUNT  ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poParam.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 50, poParam.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poParam.CTRANSACTION_DATE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

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

        #endregion

        #region Page 2

        /// <summary>
        /// Get grid allocation - streaming method returning List of ResultDTO
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, language ID, and asset code</param>
        /// <returns>List of ResultDTO with expense allocation information</returns>
        public async Task<List<FAT00800GetGridAllocResultDTO>> GetGridAllocAsync(FAT00800GetGridAllocParameterDTO poParam)
        {
            string lcMethod = nameof(GetGridAllocAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<FAT00800GetGridAllocResultDTO> loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CEXPENSE_DEPT_CODE ,NEXPENSE_PCT                                                " +
                                    " ,CEXPENSE_DEPT_NAME = isnull(y.DESCRIPTION, '')                                        " +
                                    " FROM FAM_ASSET_EXP_ALLOC a(NOLOCK)                                                     " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CLANGID) " +
                                    " y ON y.CODE = a.CEXPENSE_DEPT_CODE                                                     " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID AND a.CASSET_CODE = @CASSET_CODE                    ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poParam.CLANG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT00800GetGridAllocResultDTO>(loDataTable).ToList();
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
        /// Get transaction header list - streaming method returning List of ResultDTO
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, user ID, transaction code, department code, period range, asset code, and language ID</param>
        /// <returns>List of FAT00800TransListResultDTO with transaction header information</returns>
        public async Task<List<FAT00800TransListResultDTO>> FAT00800TransListAsync(FAT00800TransListParameterDTO poParam)
        {
            string lcMethod = nameof(FAT00800TransListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<FAT00800TransListResultDTO> loResult = new();
            string lcQuery;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                lcQuery = "RSP_FA_GET_TRANS_HD_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParam.CFROM_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParam.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 30, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CUSER_ID" or
                            "@CTRANS_CODE" or
                            "@CDEPT_CODE" or
                            "@CFROM_PERIOD" or
                            "@CTO_PERIOD" or
                            "@CASSET_CODE" or
                            "@CLANGUAGE_ID"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<FAT00800TransListResultDTO>(loDataTable).ToList();
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
        /// Get asset information
        /// </summary>
        /// <param name="poParam">Parameter containing company ID, language ID, and asset code</param>
        /// <returns>Result DTO with asset information</returns>
        public async Task<FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>> GetAssetInfoAsync(FAT00800GetAssetInfoParameterDTO poParam)
        {
            string lcMethod = nameof(GetAssetInfoAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>
            {
                Data = new FAT00800GetAssetInfoResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CASSET_CODE ,CASSET_NAME ,CSERIAL_NUMBER ,CASSET_DEPT_CODE ,CASSET_LOCATION          " +
                                    " ,a.CCATEGORY_CODE ,CDEPR_METHOD ,CSTART_DATE ,NLBOOK_VALUE ,NBBOOK_VALUE                    " +
                                    " ,NYEAR_DEPR_PCT ,NLYEAR_DEPR_AMT ,NBYEAR_DEPR_AMT ,NLRESIDUAL_VALUE ,NBRESIDUAL_VALUE       " +
                                    " ,IQTY = IBEGINNING_QTY - IADDITION_QTY - IDEDUCTION_QTY ,CUNIT ,CLAST_TRANS_DATE            " +
                                    " ,IUSEFUL_LIVE_YR = FLOOR(a.IUSEFUL_LIVE / 12) ,IUSEFUL_LIVE_MO = (a.IUSEFUL_LIVE % 12)      " +
                                    " ,CASSET_DEPT_NAME = ISNULL(b.DESCRIPTION, '')                                               " +
                                    " ,CCATEGORY_DESC = ISNULL(c1.CDESCRIPTION, c.CCATEGORY_DESC)                                 " +
                                    " ,CDEPR_METHOD_DESC = ISNULL(d.DESCRIPTION, '')                                              " +
                                    " FROM FAM_ASSET a(NOLOCK)                                                                    " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CLANGID)      " +
                                    " b ON b.CODE = CASSET_DEPT_CODE                                                              " +
                                    " LEFT JOIN GSM_CATEGORY c(NOLOCK) ON c.CCOMPANY_ID = a.CCOMPANY_ID                           " +
                                    " AND CCATEGORY_ITEM = '51' AND CCATEGORY_TYPE = 'C'  AND c.CCATEGORY_CODE = a.CCATEGORY_CODE " +
                                    " LEFT JOIN GSB_TRANSLATE c1(NOLOCK) ON c1.CTABLE_NAME = 'GSM_CATEGORY'                       " +
                                    " AND c1.CFOREIGN_LANGUAGE = @CLANGID                                                         " +
                                    " AND c1.CKEY_ID = c.CCOMPANY_ID + c.CCATEGORY_ITEM + c.CCATEGORY_TYPE + c.CCATEGORY_CODE     " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_FA_DEPR_METHOD', '', @CLANGID)  " +
                                    " d ON d.CODE = a.CDEPR_METHOD WHERE a.CCOMPANY_ID = @CCOMPANY_ID AND a.CASSET_CODE = @CASSET_CODE ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poParam.CLANG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.CASSET_CODE = loRtn.CASSET_CODE;
                    loResult.Data.CASSET_NAME = loRtn.CASSET_NAME;
                    loResult.Data.CSERIAL_NUMBER = loRtn.CSERIAL_NUMBER;
                    loResult.Data.CASSET_DEPT_CODE = loRtn.CASSET_DEPT_CODE;
                    loResult.Data.CASSET_LOCATION = loRtn.CASSET_LOCATION;
                    loResult.Data.CCATEGORY_CODE = loRtn.CCATEGORY_CODE;
                    loResult.Data.CDEPR_METHOD = loRtn.CDEPR_METHOD;
                    loResult.Data.CSTART_DATE = loRtn.CSTART_DATE;
                    loResult.Data.NLBOOK_VALUE = loRtn.NLBOOK_VALUE;
                    loResult.Data.NBBOOK_VALUE = loRtn.NBBOOK_VALUE;
                    loResult.Data.NYEAR_DEPR_PCT = loRtn.NYEAR_DEPR_PCT;
                    loResult.Data.NLYEAR_DEPR_AMT = loRtn.NLYEAR_DEPR_AMT;
                    loResult.Data.NBYEAR_DEPR_AMT = loRtn.NBYEAR_DEPR_AMT;
                    loResult.Data.NLRESIDUAL_VALUE = loRtn.NLRESIDUAL_VALUE;
                    loResult.Data.NBRESIDUAL_VALUE = loRtn.NBRESIDUAL_VALUE;
                    loResult.Data.IQTY = loRtn.IQTY;
                    loResult.Data.CUNIT = loRtn.CUNIT;
                    loResult.Data.CLAST_TRANS_DATE = loRtn.CLAST_TRANS_DATE;
                    loResult.Data.IUSEFUL_LIVE_YR = loRtn.IUSEFUL_LIVE_YR;
                    loResult.Data.IUSEFUL_LIVE_MO = loRtn.IUSEFUL_LIVE_MO;
                    loResult.Data.CASSET_DEPT_NAME = loRtn.CASSET_DEPT_NAME;
                    loResult.Data.CCATEGORY_DESC = loRtn.CCATEGORY_DESC;
                    loResult.Data.CDEPR_METHOD_DESC = loRtn.CDEPR_METHOD_DESC;
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