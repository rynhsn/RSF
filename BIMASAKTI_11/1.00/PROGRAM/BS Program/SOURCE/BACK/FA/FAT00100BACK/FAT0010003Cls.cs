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
    /// Business logic class for FAT0010003 - Fixed Asset Transaction Detail operations
    /// Handles all business logic operations for FA Transaction Detail
    /// </summary>
    public class FAT0010003Cls : R_BusinessObjectAsync<FAT0010003DTO>
    {
        private readonly FAT00100BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT0010003Cls()
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
        /// Get transaction header data
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, foreign language, dept code, transaction code, and reference no</param>
        /// <returns>Result DTO with header information</returns>
        public async Task<FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>> GetDataHeaderAsync(FAT0010003GetDataHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetDataHeaderAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>
            {
                Data = new FAT0010003GetDataHeaderResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CDEPT_CODE, a.CTRANSACTION_CODE, CREFERENCE_NO, CTRANSACTION_DATE, a.CCURRENCY_CODE,  " +
                                    " NLBASE_RATE_AMOUNT, NLCURRENCY_RATE_AMOUNT, NBBASE_RATE_AMOUNT, NBCURRENCY_RATE_AMOUNT, " +
                                    " CTRANSACTION_DESCR, a.CSUPPLIER_ID, CINFO_SEQNO, " +
                                    " CDEPT_NAME = ISNULL(b.DESCRIPTION, ''), " +
                                    " CCURRENCY_NAME = ISNULL(CCURRENCY_NAME, ''), " +
                                    " CTRANSACTION_NAME = ISNULL(CTRANSACTION_NAME, ''), " +
                                    " CSUPPLIER_NAME = ISNULL(CSUPPLIER_NAME, '') " +
                                    " FROM PJT_TRANSACTION_HD a (nolock) " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) b " +
                                    " ON b.CODE = a.CDEPT_CODE " +
                                    " LEFT JOIN SAB_CURRENCY c (nolock)  " +
                                    " ON c.CCURRENCY_CODE = a.CCURRENCY_CODE  " +
                                    " LEFT JOIN GSM_TRANSACTION_CODE d (nolock)  " +
                                    " ON d.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and d.CTRANSACTION_CODE = a.CTRANSACTION_CODE  " +
                                    " LEFT JOIN GSM_SUPPLIER e (nolock)  " +
                                    " ON e.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and e.CSUPPLIER_ID = a.CSUPPLIER_ID " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID  " +
                                    " AND a.CDEPT_CODE = @PCFR_DEPT_CODE  " +
                                    " AND a.CTRANSACTION_CODE = @PCFR_TRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @PCFR_REFERENCE_NO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@PCFR_DEPT_CODE", DbType.String, 50, poParameter.PCFR_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@PCFR_TRANSACTION_CODE", DbType.String, 50, poParameter.PCFR_TRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@PCFR_REFERENCE_NO", DbType.String, 50, poParameter.PCFR_REFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT0010003GetDataHeaderResultDTO>(loDataTable).FirstOrDefault();

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
        /// Get transaction detail grid data (streaming method)
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and reference no</param>
        /// <returns>List of transaction detail result DTOs</returns>
        public async Task<List<FAT0010003GetDataGridResultDTO>> GetDataGridAsync(FAT0010003GetDataGridParameterDTO poParameter)
        {
            string lcMethod = nameof(GetDataGridAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<FAT0010003GetDataGridResultDTO> loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CSEQUENCE_NO, CPROD_DEPT_CODE, IPRODTYP, CPRODUCT_ID, CSUPP_PRODUCT_NAME,								" +
                                    "        a.CALLOC_EXPENSE_CODE, CWAREHOUSE_ID, CBILL_UNIT, NBILL_UNIT_QTY,										" +
                                    "        CPRODTYP_DESC = case IPRODTYP when 1 then 'Product' else 'Expenditure' end,							" +
                                    "        CDETAIL_DESCR, NTRANS_AMOUNT, NLTRANS_AMOUNT, NBTRANS_AMOUNT     										" +
                                    "   FROM PJT_TRANSACTION_DT a (nolock)																			" +
                                    "        LEFT JOIN FAT_TRANS_ASSET b (nolock) on b.CCOMPANY_ID=a.CCOMPANY_ID and b.CFR_DEPT_CODE=a.CDEPT_CODE	" +
                                    "          and b.CFR_TRANSACTION_CODE=a.CTRANSACTION_CODE and b.CFR_REFERENCE_NO=a.CREFERENCE_NO and			" +
                                    "           b.CFR_SEQUENCE_NO=a.CSEQUENCE_NO and b.LDELETE_FLAG=0												" +
                                    "   WHERE a.CCOMPANY_ID=@CCOMPANY_ID AND a.CDEPT_CODE=@PCFR_DEPT_CODE AND a.CTRANSACTION_CODE =				" +
                                    "        @PCFR_TRANSACTION_CODE AND a.CREFERENCE_NO=@PCFR_REFERENCE_NO and CSTATUS='08' and a.CALLOC_EXPENSE_CODE<>''					" +
                                    "        and b.CCOMPANY_ID is null																				";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@PCFR_DEPT_CODE", DbType.String, 50, poParameter.PCFR_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@PCFR_TRANSACTION_CODE", DbType.String, 50, poParameter.PCFR_TRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@PCFR_REFERENCE_NO", DbType.String, 50, poParameter.PCFR_REFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT0010003GetDataGridResultDTO>(loDataTable).ToList();
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
        /// Override method for deleting entity (empty implementation)
        /// </summary>
        /// <param name="poEntity">Entity to delete</param>
        protected override async Task R_DeletingAsync(FAT0010003DTO poEntity)
        {
            // Empty implementation - preserved from VB.NET
            await Task.CompletedTask;
        }

        /// <summary>
        /// Override method for displaying entity (empty implementation)
        /// </summary>
        /// <param name="poEntity">Entity to display</param>
        /// <returns>Entity DTO</returns>
        protected override async Task<FAT0010003DTO> R_DisplayAsync(FAT0010003DTO poEntity)
        {
            // Empty implementation - preserved from VB.NET
            return await Task.FromResult(poEntity);
        }

        /// <summary>
        /// Override method for saving entity (empty implementation)
        /// </summary>
        /// <param name="poNewEntity">Entity to save</param>
        /// <param name="peCRUDMode">CRUD mode</param>
        protected override async Task R_SavingAsync(FAT0010003DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            // Empty implementation - preserved from VB.NET
            await Task.CompletedTask;
        }
    }
}

