using FAT00700Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace FAT00700Back
{
    /// <summary>
    /// Business logic class for FAT00700 - FA Transaction operations
    /// Handles transaction CRUD operations, validations, and process workflows
    /// Initial Version MA 6/8/2023
    /// </summary>
    public class FAT00700Cls : R_BusinessObjectAsync<FAT00700DTO>
    {
        private readonly FAT00700BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00700 _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_FAT00700_SAVE_TRANSResources.Resources_Dummy_Class a = new RSP_FAT00700_SAVE_TRANSResources.Resources_Dummy_Class();
        private readonly RSP_FAT00700_SUBMIT_TRANSResources.Resources_Dummy_Class b = new RSP_FAT00700_SUBMIT_TRANSResources.Resources_Dummy_Class();
        private readonly RSP_FA_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class c = new RSP_FA_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class();

        public FAT00700Cls()
        {
            _logger = LoggerFAT00700.R_GetInstanceLogger();
            _activitySource = FAT00700Activity.R_GetInstanceActivitySource();
        }

        #region CRUD

        protected override async Task R_DeletingAsync(FAT00700DTO poEntity)
        {
            string lcMethod = nameof(R_DeletingAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_UPDATE_TRANS_HD_STATUS ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, poEntity.CNEW_STATUS);


                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                lcCmdforDebuging = string.Format(" EXEC RSP_FA_UPDATE_TRANS_HD_STATUS '{0}', '{1}', '{2}', '{3}'", poEntity.CCOMPANY_ID, poEntity.CDEPT_CODE, poEntity.CREC_ID, poEntity.CNEW_STATUS);
                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        protected override async Task<FAT00700DTO> R_DisplayAsync(FAT00700DTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            FAT00700DTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FAT00700_GET_TRANS_DETAIL ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poEntity.CLANG_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00700DTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loResult;
        }

        protected override async Task R_SavingAsync(FAT00700DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethod = nameof(R_SavingAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            string lcCmd = "";
            FAT00700DTO loRtn = new FAT00700DTO();
            int loRtnValid1 = 0;
            int loRtnValid2 = 0;
            //int loRtnValid3 = 0; // Not used in VB.NET
            int loRtnValid4 = 0;

            string LCPRD = "";
            FAT00700DTO LOPREPARATION = new FAT00700DTO();

            DataTable loRtn1;

            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FAT00700_SAVE_TRANS";
                loCmd.CommandType = CommandType.StoredProcedure;

                if (poNewEntity.CACTION == "NEW")
                {
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, poNewEntity.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQ_NO", DbType.String, 6, poNewEntity.CASSET_TRANS_SEQ_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@NLDISCARD_AMOUNT", DbType.Decimal, 9, poNewEntity.NLTRANS_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBDISCARD_AMOUNT", DbType.Decimal, 9, poNewEntity.NBTRANS_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@CEXPENSE_ALLOC_ID", DbType.String, 20, poNewEntity.CEXPENSE_ALLOC_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 400, poNewEntity.CTRANS_DESC);

                    R_ExternalException.R_SP_Init_Exception(loConn);

                    try
                    {
                        var loTemp = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                        var loResult = R_Utility.R_ConvertTo<FAT00700DTO>(loTemp).FirstOrDefault();
                        poNewEntity.CREC_ID = loResult?.CREC_ID ?? string.Empty;
                    }
                    catch (Exception ex)
                    {

                        loEx.Add(ex);
                    }

                    loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                }

                else

                {
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, poNewEntity.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQ_NO", DbType.String, 6, poNewEntity.CASSET_TRANS_SEQ_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@NLDISCARD_AMOUNT", DbType.Decimal, 9, poNewEntity.NLTRANS_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@NBDISCARD_AMOUNT", DbType.Decimal, 9, poNewEntity.NBTRANS_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@CEXPENSE_ALLOC_ID", DbType.String, 20, poNewEntity.CEXPENSE_ALLOC_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 400, poNewEntity.CTRANS_DESC);


                    R_ExternalException.R_SP_Init_Exception(loConn);

                    try
                    {
                        await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                    }
                    catch (Exception ex)
                    {

                        loEx.Add(ex);
                    }

                    loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
                }

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            if (loEx.Haserror)
            {
                loEx.ThrowExceptionIfErrors();
            }
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        #endregion

        #region Function get data and init

        public async Task<FAT00700ResultDTO<GetPeriodResultDTO>> GetPeriodAsync(GetPeriodParameterDTO poParameter)
        {
            string lcMethod = nameof(GetPeriodAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetPeriodResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CTRANS_DEPT_CODE as CDEFAULT_TRX_DEPT_CODE, 	  " +
                                  " CSOFT_PERIOD, CGLLINK_DATE, CRATETYPE_CODE         	  " +
                                  " FROM FAM_SYSTEM (nolock) WHERE CCOMPANY_ID=@CCOMPANY_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<GetPeriodResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetPeriodResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetCurrencyResultDTO>> GetCurrencyAsync(GetCurrencyParameterDTO poParameter)
        {
            string lcMethod = nameof(GetCurrencyAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetCurrencyResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CLOCAL_CURRENCY_CODE, CBASE_CURRENCY_CODE,   " +
                                  " LCUST_PERIOD_FLAG FROM HSM_PROPERTY_SYSTEM (nolock) " +
                                  " WHERE CCOMPANY_ID=@CCOMPANY_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<GetCurrencyResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetCurrencyResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetFATransactionDataResultDTO>> GetFATransactionDataAsync(GetFATransactionDataParameterDTO poParameter)
        {
            string lcMethod = nameof(GetFATransactionDataAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetFATransactionDataResultDTO loResult = new();

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

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poParameter.CLANGID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE); // 270010 Dari Depan

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<GetFATransactionDataResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetFATransactionDataResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetAssetInfoDataResultDTO>> GetAssetInfoDataAsync(GetAssetInfoDataParameterDTO poParameter)
        {
            string lcMethod = nameof(GetAssetInfoDataAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetAssetInfoDataResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CASSET_CODE, CASSET_NAME, CSERIAL_NUMBER, CASSET_DEPT_CODE, CASSET_LOCATION, c.CCATEGORY_CODE, " +
                                  "      CDEPR_METHOD, CSTART_DATE, NLBOOK_VALUE, NBBOOK_VALUE, NYEAR_DEPR_PCT,                           " +
                                  "      NLYEAR_DEPR_AMT, NBYEAR_DEPR_AMT, NLRESIDUAL_VALUE, NBRESIDUAL_VALUE,                            " +
                                  "      IQTY = IBEGINNING_QTY - IADDITION_QTY - IDEDUCTION_QTY, CUNIT, CLAST_TRANS_DATE,                 " +
                                  "      IUSEFUL_LIVE_YR = FLOOR(a.IUSEFUL_LIVE / 12), IUSEFUL_LIVE_MO = a.IUSEFUL_LIVE % 12,             " +
                                  "      CASSET_DEPT_NAME = ISNULL(b.DESCRIPTION,''),                                                     " +
                                  "      CCATEGORY_DESC=ISNULL(c1.CDESCRIPTION, c.CCATEGORY_DESC),                                        " +
                                  "      CDEPR_METHOD_DESC = ISNULL(d.DESCRIPTION,'')                                                     " +
                                  " FROM FAM_ASSET a (nolock)                                                                             " +
                                  "      LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT','',                           " +
                                  "           @CLANGID) b ON b.CODE = CASSET_DEPT_CODE                                                        " +
                                  "      LEFT JOIN GSM_CATEGORY c (nolock) ON c.CCOMPANY_ID=a.CCOMPANY_ID and CCATEGORY_ITEM='51' and     " +
                                  "           CCATEGORY_TYPE='C' and c.CCATEGORY_CODE=a.CCATEGORY_CODE                                    " +
                                  "      LEFT JOIN GSB_TRANSLATE c1 (nolock) ON c1.CTABLE_NAME='GSM_CATEGORY' AND c1.CFOREIGN_LANGUAGE=   " +
                                  "           @CLANGID AND c1.CKEY_ID = c.CCOMPANY_ID + c.CCATEGORY_ITEM + c.CCATEGORY_TYPE +                 " +
                                  "           c.CCATEGORY_CODE                                                                            " +
                                  "      LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY',@CCOMPANY_ID,'_FA_DEPR_METHOD','',                         " +
                                  "           @CLANGID) d ON d.CODE = a.CDEPR_METHOD                                                          " +
                                  " WHERE a.CCOMPANY_ID=@CCOMPANY_ID AND a.CASSET_CODE=@CASSET_CODE                                                      ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poParameter.CLANGID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<GetAssetInfoDataResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetAssetInfoDataResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<List<GetGridAllocDataResultDTO>>> GetGridAllocDataAsync(GetGridAllocDataParameterDTO poParameter)
        {
            string lcMethod = nameof(GetGridAllocDataAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<GetGridAllocDataResultDTO> loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "	  SELECT CEXPENSE_DEPT_CODE, NEXPENSE_PCT, CEXPENSE_DEPT_NAME = isnull(y.DESCRIPTION,'') " +
                                  "  FROM FAM_ASSET_EXP_ALLOC a (nolock)                                                       " +
                                  "     LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID,'_DEPARTMENT','',@CLANGID) y           " +
                                  "        on y.CODE = a.CEXPENSE_DEPT_CODE                                                    " +
                                  "  WHERE a.CCOMPANY_ID=@CCOMPANY_ID AND a.CASSET_CODE=@CASSET_CODE                                          ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poParameter.CLANGID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<GetGridAllocDataResultDTO>(loRtnDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<List<GetGridAllocDataResultDTO>> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetDateStatusResultDTO>> GetDateStatusAsync(GetDateStatusParameterDTO poParameter)
        {
            string lcMethod = nameof(GetDateStatusAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetDateStatusResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "		SELECT CLAST_TRANS_DATE = CLAST_TRANS_DATE,													" +
                                  "       CNEXT_DEPR_PERIOD = case when CASSET_STATUS<>'1' or CDEPR_METHOD='0' then '999912' when    " +
                                  "         CLAST_DEPR_PERIOD<>'' then LEFT(CONVERT(VARCHAR(8),DATEADD(MONTH,1,CONVERT(DATETIME,     " +
                                  "         CLAST_DEPR_PERIOD+'01')),112),6) when RIGHT(CSTART_DATE,2)<'16' then LEFT(CSTART_DATE,6) " +
                                  "         else LEFT(CONVERT(VARCHAR(8),DATEADD(MONTH,1,CONVERT(DATETIME,CSTART_DATE)),112),6) end, " +
                                  "       CASSET_STATUS = CASSET_STATUS                                                              " +
                                  "   FROM FAM_ASSET (nolock)                                                                        " +
                                  "   WHERE CCOMPANY_ID=@CCOMPANY_ID AND CASSET_CODE = @CASSET_CODE                                                 ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<GetDateStatusResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetDateStatusResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetAssetInformationResultDTO>> GetAssetInformationAsync(GetAssetInformationParameterDTO poParameter)
        {
            string lcMethod = nameof(GetAssetInformationAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetAssetInformationResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT NBBASE_RATE = NLAST_BBASE_RATE_AMOUNT,														" +
                                  "       NBCURRENCY_RATE = NLAST_BCURRENCY_RATE_AMOUNT,                                                 " +
                                  "       NLFA = NLBEGINNING_AMT + NLADDITION_AMT - NLDEDUCTION_AMT,                                     " +
                                  "       NLAD = NLPRIOR_DEPR_AMT + NLYTD_DEPR_AMT,                                                      " +
                                  "       NLRFA = NLREVALUATION_AMT,                                                                     " +
                                  "       NLRAD = NLPRIOR_REVALUATION_AMT + NLYTD_REVALUATION_AMT,                                       " +
                                  "       NBFA = NBBEGINNING_AMT + NBADDITION_AMT - NBDEDUCTION_AMT,                                     " +
                                  "       NBAD = NBPRIOR_DEPR_AMT + NBYTD_DEPR_AMT,                                                      " +
                                  "       NBRFA = NBREVALUATION_AMT,                                                                     " +
                                  "       NBRAD = NBPRIOR_REVALUATION_AMT + NBYTD_REVALUATION_AMT,                                       " +
                                  "       CNSEQUENCE_NO = right('000000' + convert(varchar(6),convert(integer,CLSEQUENCE_NO)+100),6),    " +
                                  "       CASSET_DEPT_CODE = CASSET_DEPT_CODE,                                                           " +
                                  "       CJRNGRP_CODE = CJRNGRP_CODE,                                                                   " +
                                  "       CTAX_CATEGORY_CODE = CTAX_CATEGORY_CODE,                                                       " +
                                  "       CDEPR_METHOD = CDEPR_METHOD                                                                    " +
                                  "  FROM FAM_ASSET (nolock)                                                                             " +
                                  "  WHERE CCOMPANY_ID=@CCOMPANY_ID AND CASSET_CODE = @CASSET_CODE                                                      ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<GetAssetInformationResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetAssetInformationResultDTO> { Data = loResult };
        }

        #endregion

        #region Validate func

        public async Task<FAT00700ResultDTO<GetTransDateValidationResultDTO>> GetTransDateValidationAsync(GetTransDateValidationParameterDTO poParameter)
        {
            string lcMethod = nameof(GetTransDateValidationAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetTransDateValidationResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT CPRD=CCYEAR+CPERIOD_NO FROM GSM_PERIOD_DT (nolock)     " +
                                  " WHERE CCOMPANY_ID=@CCOMPANY_ID and @CTRANSACTION_DATE BETWEEN " +
                                  " CSTART_DATE AND CEND_DATE ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poParameter.CTRANSACTION_DATE);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<GetTransDateValidationResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetTransDateValidationResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetUserRightApprovalResultDTO>> GetUserRightApprovalAsync(GetUserRightApprovalParameterDTO poParameter)
        {
            string lcMethod = nameof(GetUserRightApprovalAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetUserRightApprovalResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT TOP 1 1 FROM FAM_APPROVAL_USER (nolock) " +
                                  " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CTRANSACTION_CODE=@CTRANSACTION_CODE AND CUSER_ID=@CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult.Result = loRtnDataTable.Rows.Count;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetUserRightApprovalResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetUserActivityRightsResultDTO>> GetUserActivityRightsAsync(GetUserActivityRightsParameterDTO poParameter)
        {
            string lcMethod = nameof(GetUserActivityRightsAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetUserActivityRightsResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT TOP 1 1 FROM GSM_USER_RIGHT (nolock) " +
                                  " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CACTIVITY_CODE=@CACTIVITY_CODE AND CUSER_ID=@CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVITY_CODE", DbType.String, 50, poParameter.CACTIVITY_CODE); // FA013001 Dari Depan
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult.Result = loRtnDataTable.Rows.Count;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetUserActivityRightsResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<CheckOutstandingTransResultDTO>> CheckOutstandingTransAsync(CheckOutstandingTransParameterDTO poParameter)
        {
            string lcMethod = nameof(CheckOutstandingTransAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            CheckOutstandingTransResultDTO loResult = new();

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
                                  " AND a.CASSET_TRANS_SEQNO > b.CLSEQUENCE_NO                " +
                                  "UNION" +
                                  "SELECT a.CASSET_CODE" +
                                  "FROM FAT_RAPID_DISCARD_ASSET a (nolock), FAT_RAPID_DISCARD_HD c (nolock),FAM_ASSET b (nolock)" +
                                  "WHERE a.CCOMPANY_ID={login_company_id} AND a.CASSET_CODE = {Asset Code} and" +
                                  "c.CCOMPANY_ID=a.CCOMPANY_ID AND c.CDEPT_CODE=a.CDEPT_CODE AND c.CTRANSACTION_CODE=" +
                                  "a.CTRANSACTION_CODE and c.CREFERENCE_NO=a.CREFERENCE_NO and c.CSTATUS<=�08� AND" +
                                  "b.CCOMPANY_ID=a.CCOMPANY_ID AND b.CASSET_CODE=a.CASSET_CODE AND" +
                                  "a.CASSET_TRANS_SEQNO>b.CLSEQUENCE_NO";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<CheckOutstandingTransResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<CheckOutstandingTransResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<ValidateVoidResultDTO>> ValidateVoidAsync(ValidateVoidParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidateVoidAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            ValidateVoidResultDTO loResult = new();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT TOP 1 CASSET_CODE      " +
                                  " FROM FAT_TRANS_ASSET (nolock) " +
                                  " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CASSET_CODE=@CASSET_CODE AND " +
                                  " CASSET_TRANS_SEQNO > @CASSET_TRANS_SEQNO and LDELETE_FLAG=0 "; // CR5 MA

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, poParameter.CASSET_TRANS_SEQNO);

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<ValidateVoidResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loRtn != null)
                {
                    loResult = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<ValidateVoidResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<GetApprovalPrecheckResultDTO>> GetApprovalPrecheckAsync(GetApprovalPrecheckParameterDTO poParameter)
        {
            string lcMethod = nameof(GetApprovalPrecheckAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            GetApprovalPrecheckResultDTO loResult = new();
            byte loRtn = 0;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = " SELECT IAPPROVAL_OPTION FROM GSM_ACTIVITY_APPROVAL (NOLOCK)            " +
                                  " WHERE CCOMPANY_ID = @CCOMPANY_ID AND CAPPROVAL_CODE = @CAPPROVAL_CODE  ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_CODE", DbType.String, 50, "FA013002");

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                if (loRtnDataTable.Rows.Count > 0)
                {
                    var loRtnTemp = R_Utility.R_ConvertTo<byte>(loRtnDataTable).FirstOrDefault();
                    loRtn = loRtnTemp;
                }

                loResult.Result = (loRtn == 2);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<GetApprovalPrecheckResultDTO> { Data = loResult };
        }

        public async Task<FAT00700ResultDTO<ValidateFoundDeptResultDTO>> ValidateFoundDeptAsync(ValidateFoundDeptParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidateFoundDeptAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            ValidateFoundDeptResultDTO loResult = new();
            FAT00700DTO? loRtnTemp = null;

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

                var loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtnTemp = R_Utility.R_ConvertTo<FAT00700DTO>(loRtnDataTable).FirstOrDefault();

                if (loRtnTemp != null)
                {
                    loResult.Result = 1;
                }
                else
                {
                    loResult.Result = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700ResultDTO<ValidateFoundDeptResultDTO> { Data = loResult };
        }

        public async Task ValidateGLJournalAsync(ValidateGLJournalParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidateGLJournalAsync);
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

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                try
                {
                    await loDb.SqlExecQueryAsync(loConn, loCmd, false);
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
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        #endregion

        #region Button Click

        public async Task<FAT00700SubmitProcessParameterDTO> SubmitButtonAsync(FAT00700SubmitProcessParameterDTO poParameter)
        {
            string lcMethod = nameof(SubmitButtonAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                if (poParameter.CNEW_STATUS == "00")
                {
                    loCmd.CommandText = "RSP_FA_UPDATE_TRANS_HD_STATUS";
                    loCmd.CommandType = CommandType.StoredProcedure;
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, poParameter.CNEW_STATUS);

                }
                else
                {
                    loCmd.CommandText = "RSP_FAT00700_SUBMIT_TRANS ";
                    loCmd.CommandType = CommandType.StoredProcedure;
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);

                }

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                }
                catch (Exception ex)
                {

                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return new FAT00700SubmitProcessParameterDTO();
        }
        public async Task<FAT00700DTO> DeleteTransaction(FAT00700DTO poEntity)
        {
            string lcMethod = nameof(R_DeletingAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_UPDATE_TRANS_HD_STATUS ";
                loCmd.CommandType = CommandType.StoredProcedure;

                var lcNewStatus = poEntity.CTRANS_STATUS == "00" ? "99" : "98";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, lcNewStatus);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                }
                catch (Exception ex)
                {

                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                lcCmdforDebuging = string.Format(" EXEC RSP_FA_UPDATE_TRANS_HD_STATUS '{0}', '{1}', '{2}', '{3}'", poEntity.CCOMPANY_ID, poEntity.CDEPT_CODE, poEntity.CREC_ID, poEntity.CNEW_STATUS);
                //await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return new FAT00700DTO();
        }

        public async Task CloseButtonAsync(CloseButtonParameterDTO poParameter)
        {
            string lcMethod = nameof(CloseButtonAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = "RSP_FAT_CLOSE";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        public async Task ApproveButtonAsync(ApproveButtonParameterDTO poParameter)
        {
            string lcMethod = nameof(ApproveButtonAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = "RSP_FAT_APPROVE";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        public async Task VoidButtonAsync(VoidButtonParameterDTO poParameter)
        {
            string lcMethod = nameof(VoidButtonAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = "RSP_FAT_VOID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_REASON_CODE", DbType.String, 50, poParameter.CCANCEL_REASON_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_APPROVED_BY", DbType.String, 50, poParameter.CCANCEL_APPROVED_BY);

                await loDb.SqlExecQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        #endregion

        #region Initial Process with SP
        public async Task<FAT00700SystemParamResultDTO> GetSystemParamAsync(FAT00700SystemParamParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetSystemParamAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00700SystemParamResultDTO loRtn = new FAT00700SystemParamResultDTO();
            string lcCmdforDebuging = "";
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_GET_SYSTEM_PARAM ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParam.CLANGUAGE_ID);

                DataTable loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00700SystemParamResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00700SystemParamResultDTO();

                lcCmdforDebuging = string.Format("RSP_FA_GET_SYSTEM_PARAM '{0}', '{1}' ", poParam.CCOMPANY_ID, poParam.CLANGUAGE_ID);

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _logger.LogInfo("END method {MethodName}", lcMethod);
            }

            finally
            {
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        public async Task<FAT00700CompanyInfoResultDTO> GetCompanyInfoAsync(FAT00700CompanyInfoParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetCompanyInfoAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00700CompanyInfoResultDTO loRtn = new FAT00700CompanyInfoResultDTO();
            string lcCmdforDebuging = "";
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_GS_GET_COMPANY_INFO ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);

                DataTable loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                if (loDataTable.Rows.Count == 0)
                {
                    loRtn.IROW_COUNT = 0;
                }
                else
                {
                    loRtn = R_Utility.R_ConvertTo<FAT00700CompanyInfoResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00700CompanyInfoResultDTO();
                }

                lcCmdforDebuging = string.Format("RSP_GS_GET_COMPANY_INFO '{0}'", poParam.CCOMPANY_ID);

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _logger.LogInfo("END method {MethodName}", lcMethod);
            }

            finally
            {
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        public async Task<FAT00700PeriodInfoResultDTO> GetPeriodInfoAsync(FAT00700PeriodInfoParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetPeriodInfoAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00700PeriodInfoResultDTO loRtn = new FAT00700PeriodInfoResultDTO();
            string lcCmdforDebuging = "";
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_GS_GET_PERIOD_DT_INFO ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 2, poParam.CPERIOD_NO);

                DataTable loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00700PeriodInfoResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00700PeriodInfoResultDTO();

                lcCmdforDebuging = string.Format("RSP_FA_GET_SYSTEM_PARAM '{0}', '{1}', '{2}' ", poParam.CCOMPANY_ID, poParam.CYEAR, poParam.CPERIOD_NO);

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _logger.LogInfo("END method {MethodName}", lcMethod);
            }

            finally
            {
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        public async Task<FAT00700TransCodeInfoResultDTO> GetTransCodeInfoAsync(FAT00700TransCodeInfoParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetTransCodeInfoAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00700TransCodeInfoResultDTO loRtn = new FAT00700TransCodeInfoResultDTO();
            string lcCmdforDebuging = "";
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_GS_GET_TRANS_CODE_INFO ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);

                DataTable loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00700TransCodeInfoResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00700TransCodeInfoResultDTO();

                lcCmdforDebuging = string.Format("RSP_GS_GET_TRANS_CODE_INFO '{0}', '{1}'", poParam.CCOMPANY_ID, poParam.CTRANS_CODE);

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _logger.LogInfo("END method {MethodName}", lcMethod);
            }

            finally
            {
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        public async Task<FAT00700PeriodRangeResultDTO> GetPeriodRangeAsync(FAT00700PeriodRangeParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetPeriodRangeAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00700PeriodRangeResultDTO loRtn = new FAT00700PeriodRangeResultDTO();
            string lcCmdforDebuging = "";
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_GS_GET_PERIOD_YEAR_RANGE ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, poParam.CMODE);

                DataTable loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00700PeriodRangeResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00700PeriodRangeResultDTO();

                lcCmdforDebuging = string.Format("RSP_GS_GET_PERIOD_YEAR_RANGE '{0}', '{1}', '{2}'", poParam.CCOMPANY_ID, poParam.CCYEAR, poParam.CMODE);

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _logger.LogInfo("END method {MethodName}", lcMethod);
            }

            finally
            {
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion

        #region Streaming Methods
        public async Task<List<FAT00700GetDeptListResultDTO>> GetDeptList(FAT00700GetDeptListParameterDTO poParam)
        {
            string lcMethod = nameof(GetDeptList);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            List<FAT00700GetDeptListResultDTO> loRtn = new List<FAT00700GetDeptListResultDTO>();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_GS_GET_DEPT_LOOKUP_LIST ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00700GetDeptListResultDTO>(loRtnDataTable).ToList();

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loRtn;
        }

        public async Task<List<GetTransactionListResultDTO>> GetTransactionListAsync(GetTransactionListParameterDTO poParameter)
        {
            string lcMethod = nameof(GetTransactionListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            DbConnection? loConn = null;
            DbCommand? loCmd = null;
            List<GetTransactionListResultDTO> loResult = new();

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = "RSP_FAT00700_GET_TRANS_LIST";

                // Add parameters matching the stored procedure
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParameter.CFROM_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParameter.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 30, poParameter.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);

                string lcQuery = loCmd.CommandText.Trim();
                if (!lcQuery.StartsWith("EXEC", StringComparison.OrdinalIgnoreCase))
                {
                    lcQuery = "EXEC " + lcQuery;
                }
                _logger.LogDebug(lcQuery + " " + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GetTransactionListResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }
        #endregion
    }
}