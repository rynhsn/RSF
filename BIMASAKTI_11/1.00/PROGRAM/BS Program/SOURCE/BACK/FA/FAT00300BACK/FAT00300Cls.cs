using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using FAT00300Back.DTOs;
using FAT00300BackResources;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_OpenTelemetry;
using R_Storage;
using R_StorageCommon;

namespace FAT00300Back
{
    /// <summary>
    /// Business logic class for FAT00300 - FA Transaction Depreciation
    /// </summary>
    public class FAT00300Cls : R_BusinessObjectAsync<FAT00300DTO>
    {
        private readonly FAT00300BackResources.Resources_Dummy_Class loRsp = new();
        private readonly RSP_FAT00300_SAVE_TRANSResources.Resources_Dummy_Class a = new();
        private readonly RSP_FAT00300_SUBMIT_TRANSResources.Resources_Dummy_Class b = new();
        private readonly LoggerFAT00300 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00300Cls()
        {
            _logger = LoggerFAT00300.R_GetInstanceLogger();
            _activitySource = FAT00300Activity.R_GetInstanceActivitySource();
        }

        protected override async Task R_DeletingAsync(FAT00300DTO poEntity)
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

                loCmd.CommandText = "RSP_FA_UPDATE_TRANS_HD_STATUS";
                loCmd.CommandType = CommandType.StoredProcedure;

                var lcNewStatus = poEntity.CTRANS_STATUS == "00" ? "99" : "98";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 3, lcNewStatus);

                lcCmdforDebuging = string.Format(" EXEC RSP_FA_UPDATE_TRANS_HD_STATUS '{0}', '{1}', '{2}' ", poEntity.CCOMPANY_ID, poEntity.CUSER_ID, poEntity.CREC_ID, lcNewStatus);
                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

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
        }

        protected override async Task<FAT00300DTO> R_DisplayAsync(FAT00300DTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300DTO loRtn = new FAT00300DTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FAT00300_GET_TRANS_DETAIL ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poEntity.CLANG_ID);


                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00300DTO>(loRtnDataTable).FirstOrDefault() ?? new FAT00300DTO();

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

        protected override async Task R_SavingAsync(FAT00300DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethod = nameof(R_SavingAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                // Add your logic here for saving the entity
                loCmd.CommandText = "RSP_FAT00300_SAVE_TRANS ";
                loCmd.CommandType = CommandType.StoredProcedure;

                if (poNewEntity.CMODE == "NEW")
                {

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, poNewEntity.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 30, poNewEntity.CMODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, "");
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQ_NO", DbType.String, 6, poNewEntity.CASSET_TRANS_SEQ_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Decimal, 9, poNewEntity.NTRANS_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 400, poNewEntity.CTRANS_DESC);
                    loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 13, poNewEntity.NLBASE_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NLCURRENCY_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 13, poNewEntity.NBBASE_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NBCURRENCY_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);

                    R_ExternalException.R_SP_Init_Exception(loConn);

                    try
                    {
                        var loTemp = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                        var loTempEntity = R_Utility.R_ConvertTo<FAT00300DTO>(loTemp).FirstOrDefault();
                        poNewEntity.CREC_ID = loTempEntity?.CREC_ID ?? string.Empty;
                    }
                    catch (Exception ex)
                    {

                        loEx.Add(ex);
                    }

                    loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                }
                else
                {
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, poNewEntity.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 30, poNewEntity.CMODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQ_NO", DbType.String, 6, poNewEntity.CASSET_TRANS_SEQ_NO);
                    loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Decimal, 9, poNewEntity.NTRANS_AMOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 400, poNewEntity.CTRANS_DESC);
                    loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 13, poNewEntity.NLBASE_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NLCURRENCY_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 13, poNewEntity.NBBASE_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NBCURRENCY_RATE);
                    loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);

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

        public async Task<FAT00300GetValidationDataResultDTO> GetValidationDataAsync(FAT00300GetValidationDataParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidationDataAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetValidationDataResultDTO loRtn = new FAT00300GetValidationDataResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "SELECT NLBOOKVAL = NLBEGINNING_AMT + NLADDITION_AMT - NLDEDUCTION_AMT + NLREVALUATION_AMT " +
                                    "- NLPRIOR_DEPR_AMT - NLYTD_DEPR_AMT - NLPRIOR_REVALUATION_AMT - NLYTD_REVALUATION_AMT, " +
                                    "NLRESIDUAL = NLRESIDUAL_VALUE, " +
                                    "IQTY = IBEGINNING_QTY + IADDITION_QTY - IDEDUCTION_QTY, " +
                                    "CLAST_TRANS_DATE, " +
                                    "CNEXT_DEPR_PERIOD = case when CASSET_STATUS<>'1' or CDEPR_METHOD='0' then '999912' when " +
                                    "CLAST_DEPR_PERIOD<>'' then LEFT(CONVERT(VARCHAR(8),DATEADD(MONTH,1,CONVERT(DATETIME, " +
                                    "CLAST_DEPR_PERIOD+'01')),112),6) when RIGHT(CSTART_DATE,2)<'16' then LEFT(CSTART_DATE,6) " +
                                    "else LEFT(CONVERT(VARCHAR(8),DATEADD(MONTH,1,CONVERT(DATETIME,CSTART_DATE)),112),6) end, " +
                                    " CASSET_STATUS, " +
                                    " CDEPR_METHOD " +
                                    " FROM FAM_ASSET (nolock) " +
                                    "WHERE CCOMPANY_ID = @CCOMPANY_ID AND CASSET_CODE = @CASSET_CODE";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetValidationDataResultDTO? loTemp = R_Utility.R_ConvertTo<FAT00300GetValidationDataResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loTemp != null)
                {
                    loRtn = loTemp;
                }
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

        public async Task<FAT00300GetInitialProcessResultDTO> GetInitialProcessAsync(FAT00300GetInitialProcessParameterDTO poParam)
        {
            string lcMethod = nameof(GetInitialProcessAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetInitialProcessResultDTO loRslt = new FAT00300GetInitialProcessResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "SELECT CDEFAULT_TRX_DEPT_CODE = CTRANS_DEPT_CODE, " +
                                     "CSOFT_PERIOD, " +
                                     "CCURRENT_PERIOD, " +
                                     "CGLLINK_DATE " +
                                     "FROM FAM_SYSTEM (nolock) " +
                                     "WHERE CCOMPANY_ID=@CCOMPANY_ID";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetInitialProcessResultDTO? loTemp = R_Utility.R_ConvertTo<FAT00300GetInitialProcessResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loTemp != null)
                {
                    loRslt = loTemp;
                }

                loCmd.CommandText = "SELECT CLOCAL_CURRENCY_CODE, " +
                                     "CBASE_CURRENCY_CODE, " +
                                     "LCUST_PERIOD_FLAG " +
                                     "FROM HSM_PROPERTY_SYSTEM (nolock) " +
                                     "WHERE CCOMPANY_ID=@CCOMPANY_ID";
                loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetInitialProcessResultDTO? loRtn2 = R_Utility.R_ConvertTo<FAT00300GetInitialProcessResultDTO>(loRtnDataTable).FirstOrDefault();

                if (loRtn2 != null)
                {
                    loRslt.CLOCAL_CURRENCY_CODE = loRtn2.CLOCAL_CURRENCY_CODE;
                    loRslt.CBASE_CURRENCY_CODE = loRtn2.CBASE_CURRENCY_CODE;
                    loRslt.LCUST_PERIOD_FLAG = loRtn2.LCUST_PERIOD_FLAG;
                }

                loCmd.CommandText = "SELECT CTRANS_DESC = ISNULL(b.CDESCRIPTION,a.CTRANSACTION_NAME), " +
                                    "LTRANS_APPROVAL = LAPPROVAL_FLAG, " +
                                    " LINCREMENT_FLAG " +
                                    "From GSM_TRANSACTION_CODE a (nolock) " +
                                    "LEFT JOIN GSB_TRANSLATE b (nolock) ON b.CTABLE_NAME ='GSM_TRANSACTION_CODE' AND " +
                                    "B.CFOREIGN_LANGUAGE=@CLANG_ID AND B.CKEY_ID=a.CCOMPANY_ID + a.CTRANSACTION_CODE " +
                                    "Where a.CCOMPANY_ID=@CCOMPANY_ID AND CTRANSACTION_CODE=@CTRANS_CODE";
                loCmd.Parameters.Clear();
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 10, poParam.CLANG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANS_CODE);
                loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetInitialProcessResultDTO? loRtn3 = R_Utility.R_ConvertTo<FAT00300GetInitialProcessResultDTO>(loRtnDataTable).FirstOrDefault();

                if (loRtn3 != null)
                {
                    loRslt.CTRANS_DESC = loRtn3.CTRANS_DESC;
                    loRslt.LTRANS_APPROVAL = loRtn3.LTRANS_APPROVAL;
                    loRslt.LINCREMENT_FLAG = loRtn3.LINCREMENT_FLAG;
                }
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

            return loRslt;
        }

        public async Task<FAT00300GetAssetInformationTABResultDTO> GetAssetInformationTABAsync(FAT00300GetAssetInformationTABParameterDTO poParam)
        {
            string lcMethod = nameof(GetAssetInformationTABAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetAssetInformationTABResultDTO loRslt = new FAT00300GetAssetInformationTABResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_GET_ASSET ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANG_ID);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetAssetInformationTABResultDTO? loTemp = R_Utility.R_ConvertTo<FAT00300GetAssetInformationTABResultDTO>(loRtnDataTable).FirstOrDefault();

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                if (loTemp != null)
                {
                    loRslt = loTemp;

                    if (!string.IsNullOrEmpty(loRslt.CSTORAGE_ID))
                    {
                        var loReadParameter = new R_ReadParameter();
                        loReadParameter.StorageId = loRslt.CSTORAGE_ID;

                        var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                        loRslt.OASSET_IMAGE = loReadResult.Data;
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
                if (loDb != null) { loDb = null; }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loRslt;
        }

        public async Task<FAT00300ValidateDeptCodeResultDTO> ValidateDeptCodeAsync(FAT00300ValidateDeptCodeParameterDTO poParam)
        {
            string lcMethod = nameof(ValidateDeptCodeAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300ValidateDeptCodeResultDTO loRtn = new FAT00300ValidateDeptCodeResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                string lcCmd = string.Format(" SELECT TOP 1 1 FROM GSX_DEPARTMENT_USER " +
                                              " WHERE CCOMPANY_ID = @CCOMPANY_ID  " +
                                              " AND CDEPT_CODE = @CDEPT_CODE " +
                                              " AND CUSER_ID = @CUSER_ID ");

                loCmd.CommandText = lcCmd;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                DataTable loRtn1 = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn.Result = loRtn1.Rows.Count;
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

        public async Task<FAT00300ValidateGLJournalAccountResultDTO> ValidateGLJournalAccountAsync(FAT00300ValidateGLJournalAccountParameterDTO poParam)
        {
            string lcMethod = nameof(ValidateGLJournalAccountAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300ValidateGLJournalAccountResultDTO loRtn = new FAT00300ValidateGLJournalAccountResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                string lcCmd = string.Format(" EXEC RSP_FA_VALIDATE_JOURNAL @CCOMPANY_ID, @CDEPT_CODE, @CTRANS_CODE, @CREFERENCE_NO");

                loCmd.CommandText = lcCmd;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);

                DataTable loRtn1 = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn.Result = loRtn1.Rows.Count;
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

        public async Task<FAT00300GetUserCanApproveResultDTO> GetUserCanApproveAsync(FAT00300GetUserCanApproveParameterDTO poParam)
        {
            string lcMethod = nameof(GetUserCanApproveAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetUserCanApproveResultDTO loRtn = new FAT00300GetUserCanApproveResultDTO();
            int liRtn = 0;
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " SELECT TOP 1 1 FROM FAM_APPROVAL_USER (nolock) " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CTRANSACTION_CODE=@CTRANSACTION_CODE AND CUSER_ID=@CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                liRtn = loRtnDataTable.Rows.Count;

                if (liRtn == 1)
                {
                    loRtn.Result = true;
                }
                else
                {
                    loRtn.Result = false;
                }
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

        public async Task<FAT00300GetUserCanCloseResultDTO> GetUserCanCloseAsync(FAT00300GetUserCanCloseParameterDTO poParam)
        {
            string lcMethod = nameof(GetUserCanCloseAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetUserCanCloseResultDTO loRtn = new FAT00300GetUserCanCloseResultDTO();
            int liRtn = 0;
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " SELECT TOP 1 1 FROM GSM_USER_RIGHT (nolock) " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CACTIVITY_CODE=@CACTIVITY_CODE AND CUSER_ID=@CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVITY_CODE", DbType.String, 50, "FA013001"); //FA013001 Dari Depan
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                liRtn = loRtnDataTable.Rows.Count;

                if (liRtn == 1)
                {
                    loRtn.Result = true;
                }
                else
                {
                    loRtn.Result = false;
                }
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

        public async Task<FAT00300GetApprovalPrecheckResultDTO> GetApprovalPrecheckAsync(FAT00300GetApprovalPrecheckParameterDTO poParam)
        {
            string lcMethod = nameof(GetApprovalPrecheckAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetApprovalPrecheckResultDTO loRtn = new FAT00300GetApprovalPrecheckResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "SELECT IAPPROVAL_OPTION " +
                                    "FROM GSM_ACTIVITY_APPROVAL(NOLOCK) " +
                                    "WHERE CCOMPANY_ID = @CCOMPANY_ID AND CAPPROVAL_CODE = 'FA013002'";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                byte? liApprOpt = R_Utility.R_ConvertTo<byte>(loRtnDataTable).FirstOrDefault();

                if (liApprOpt.HasValue && liApprOpt.Value == 2)
                {
                    loRtn.Result = true;
                }
                else
                {
                    loRtn.Result = false;
                }
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

        public async Task<FAT00300GetValidateVoidResultDTO> GetValidateVoidAsync(FAT00300GetValidateVoidParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateVoidAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetValidateVoidResultDTO loRtn = new FAT00300GetValidateVoidResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " SELECT TOP 1 CASSET_CODE      " +
                                    " FROM FAT_TRANS_ASSET (nolock) " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID AND CASSET_CODE=@CASSET_CODE AND " +
                                    " CASSET_TRANS_SEQNO > @CASSET_TRANS_SEQNO and LDELETE_FLAG=0 "; //CR4 MA
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, poParam.CASSET_TRANS_SEQNO);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetValidateVoidResultDTO? loTemp = R_Utility.R_ConvertTo<FAT00300GetValidateVoidResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loTemp != null)
                {
                    loRtn = loTemp;
                }
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

        public async Task<FAT00300GetValidateTransDateResultDTO> GetValidateTransDateAsync(FAT00300GetValidateTransDateParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateTransDateAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetValidateTransDateResultDTO loRtn = new FAT00300GetValidateTransDateResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " SELECT CPRD=CCYEAR+CPERIOD_NO FROM GSM_PERIOD_DT (nolock)     " +
                                    " WHERE CCOMPANY_ID=@CCOMPANY_ID and @CTRANSACTION_DATE BETWEEN " +
                                    " CSTART_DATE AND CEND_DATE ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DATE", DbType.String, 50, poParam.CTRANSACTION_DATE);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetValidateTransDateResultDTO? loTemp = R_Utility.R_ConvertTo<FAT00300GetValidateTransDateResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loTemp != null)
                {
                    loRtn = loTemp;
                }
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

        public async Task<FAT00300GetValidateOutstandTransResultDTO> GetValidateOutstandTransAsync(FAT00300GetValidateOutstandTransParameterDTO poParam)
        {
            string lcMethod = nameof(GetValidateOutstandTransAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetValidateOutstandTransResultDTO loRtn = new FAT00300GetValidateOutstandTransResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " SELECT a.CASSET_CODE FROM FAT_TRANS_ASSET a(NOLOCK)       " +
                                    " ,FAM_ASSET b(NOLOCK)                                      " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID                        " +
                                    " AND a.CASSET_CODE = @CASSET_CODE                          " +
                                    " AND a.LDELETE_FLAG = 0 AND b.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " AND b.CASSET_CODE = a.CASSET_CODE                         " +
                                    " AND a.CASSET_TRANS_SEQNO > b.CLSEQUENCE_NO                " +
                                    " UNION                                                                                             " +
                                    " SELECT a.CASSET_CODE                                                                              " +
                                    "    FROM FAT_RAPID_DISCARD_ASSET a(nolock), FAT_RAPID_DISCARD_HD c(nolock),FAM_ASSET b(nolock)     " +
                                    "    WHERE a.CCOMPANY_ID = @CCOMPANY_ID                                                       " +
                                    "                 AND a.CASSET_CODE = @CASSET_CODE                                                " +
                                    "                 and                                                                               " +
                                    "          c.CCOMPANY_ID = a.CCOMPANY_ID AND c.CDEPT_CODE = a.CDEPT_CODE AND c.CTRANSACTION_CODE =  " +
                                    "          a.CTRANSACTION_CODE and c.CREFERENCE_NO = a.CREFERENCE_NO and c.CSTATUS <= '08' AND       " +
                                    "          b.CCOMPANY_ID = a.CCOMPANY_ID AND b.CASSET_CODE = a.CASSET_CODE AND                      " +
                                    "          a.CASSET_TRANS_SEQNO > b.CLSEQUENCE_NO                                                   ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                FAT00300GetValidateOutstandTransResultDTO? loTemp = R_Utility.R_ConvertTo<FAT00300GetValidateOutstandTransResultDTO>(loRtnDataTable).FirstOrDefault();
                if (loTemp != null)
                {
                    loRtn = loTemp;
                }
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

        public async Task<FAT00300SubmitProcessResultDTO> SubmitProcessAsync(FAT00300SubmitProcessParameterDTO poParam)
        {
            string lcMethod = nameof(SubmitProcessAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300SubmitProcessResultDTO loRtn = new FAT00300SubmitProcessResultDTO();
            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                if (poParam.CNEW_STATUS == "00")
                {
                    loCmd.CommandText = "RSP_FA_UPDATE_TRANS_HD_STATUS ";
                    loCmd.CommandType = CommandType.StoredProcedure;
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, poParam.CNEW_STATUS);
                }
                else
                {
                    loCmd.CommandText = "RSP_FAT00300_SUBMIT_TRANS";
                    loCmd.CommandType = CommandType.StoredProcedure;
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                    //loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, poParam.CNEW_STATUS);
                }

                lcCmdforDebuging = string.Format(" '{0}', '{1}', '{2}', '{3}', '{4}'", loCmd.CommandText, poParam.CCOMPANY_ID, poParam.CUSER_ID, poParam.CREC_ID, poParam.CNEW_STATUS);

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

        public async Task<FAT00300ApproveProcessResultDTO> ApproveProcessAsync(FAT00300ApproveProcessParameterDTO poParam)
        {
            string lcMethod = nameof(ApproveProcessAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300ApproveProcessResultDTO loRtn = new FAT00300ApproveProcessResultDTO();
            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " EXEC RSP_FAT_APPROVE @CCOMPANY_ID, @CDEPT_CODE, @CTRANS_CODE, @CREFERENCE_NO, @CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);

                lcCmdforDebuging = string.Format(" EXEC RSP_FAT_APPROVE '{0}', '{1}', '{2}', '{3}', '{4}' ", poParam.CCOMPANY_ID, poParam.CDEPT_CODE, poParam.CTRANS_CODE, poParam.CREFERENCE_NO, poParam.CUSER_ID);
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

            return loRtn;
        }

        public async Task<FAT00300VoidProcessResultDTO> VoidProcessAsync(FAT00300VoidProcessParameterDTO poParam)
        {
            string lcMethod = nameof(VoidProcessAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300VoidProcessResultDTO loRtn = new FAT00300VoidProcessResultDTO();
            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " EXEC RSP_FAT_VOID @CCOMPANY_ID, @CDEPT_CODE, @CTRANS_CODE, @CREFERENCE_NO, @CUSER_ID, @CCANCEL_REASON_CODE, @CCANCEL_APPROVED_BY ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_REASON_CODE", DbType.String, 50, poParam.CCANCEL_REASON_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCANCEL_APPROVED_BY", DbType.String, 50, poParam.CCANCEL_APPROVED_BY);

                lcCmdforDebuging = string.Format(" EXEC RSP_FAT_VOID '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ", poParam.CCOMPANY_ID, poParam.CDEPT_CODE, poParam.CTRANS_CODE, poParam.CREFERENCE_NO, poParam.CUSER_ID, poParam.CCANCEL_REASON_CODE, poParam.CCANCEL_APPROVED_BY);
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

            return loRtn;
        }

        public async Task<FAT00300CloseProcessResultDTO> CloseProcessAsync(FAT00300CloseProcessParameterDTO poParam)
        {
            string lcMethod = nameof(CloseProcessAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300CloseProcessResultDTO loRtn = new FAT00300CloseProcessResultDTO();
            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = " EXEC RSP_FAT_CLOSE @CCOMPANY_ID, @CDEPT_CODE, @CTRANS_CODE, @CREFERENCE_NO, @CUSER_ID ";
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParam.CREFERENCE_NO);

                lcCmdforDebuging = string.Format(" EXEC RSP_FAT_CLOSE '{0}', '{1}', '{2}', '{3}', '{4}'", poParam.CCOMPANY_ID, poParam.CDEPT_CODE, poParam.CTRANS_CODE, poParam.CREFERENCE_NO, poParam.CUSER_ID);
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

            return loRtn;
        }

        public async Task<List<FAT00300GetAllocationExpenseListResultDTO>> GetAllocationExpenseListAsync(FAT00300GetAllocationExpenseListParameterDTO poParam)
        {
            string lcMethod = nameof(GetAllocationExpenseListAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            List<FAT00300GetAllocationExpenseListResultDTO> loRtn = new List<FAT00300GetAllocationExpenseListResultDTO>();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_GET_ASSET_EXP_ALLOC_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParam.CLANG_ID);

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00300GetAllocationExpenseListResultDTO>(loRtnDataTable).ToList();
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
        public async Task<List<FAT00300GetTransListResultDTO>> GetTransListAsync(FAT00300GetTransListParameterDTO poParam)
        {
            string lcMethod = nameof(GetTransListAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            List<FAT00300GetTransListResultDTO> loRtn = new List<FAT00300GetTransListResultDTO>();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FAT00300_GET_TRANS_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
                //loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParam.CFROM_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParam.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 20, poParam.CLANGUAGE_ID);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00300GetTransListResultDTO>(loRtnDataTable).ToList();

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
        public async Task<List<FAT00300GetDeptListResultDTO>> GetDeptList(FAT00300GetDeptListParameterDTO poParam)
        {
            string lcMethod = nameof(GetTransListAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            List<FAT00300GetDeptListResultDTO> loRtn = new List<FAT00300GetDeptListResultDTO>();
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
                loRtn = R_Utility.R_ConvertTo<FAT00300GetDeptListResultDTO>(loRtnDataTable).ToList();

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

        public async Task<FAT00300GetAssetResultDTO> GetAssetAsync(FAT00300GetAssetParameterDTO poParam)
        {
            string lcMethod = nameof(GetAssetAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetAssetResultDTO loRtn = new FAT00300GetAssetResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = ("RSP_FA_GET_ASSET ");
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 30, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParam.CLANG_ID);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00300GetAssetResultDTO>(loRtnDataTable).FirstOrDefault() ?? new FAT00300GetAssetResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;



        }
        public async Task<FAT00300DTO> DeleteTransactionAsync(FAT00300DTO poParameter)
        {
            string lcMethod = nameof(R_DeletingAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            string lcCmdforDebuging = "";
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;
            FAT00300DTO loRtn = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_UPDATE_TRANS_HD_STATUS";
                loCmd.CommandType = CommandType.StoredProcedure;

                var lcNewStatus = poParameter.CTRANS_STATUS == "00" ? "99" : "98";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 3, lcNewStatus);

                lcCmdforDebuging = string.Format(" EXEC RSP_FA_UPDATE_TRANS_HD_STATUS '{0}', '{1}', '{2}' ", poParameter.CCOMPANY_ID, poParameter.CUSER_ID, poParameter.CREC_ID, lcNewStatus);
                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);

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

            return loRtn;
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        #region Initial Process with SP
        public async Task<FAT00300GetCompanyInfoResultDTO> GetCompanyInfoAsync(FAT00300GetCompanyInfoParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetAssetAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetCompanyInfoResultDTO loRtn = new FAT00300GetCompanyInfoResultDTO();
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
                loRtn = R_Utility.R_ConvertTo<FAT00300GetCompanyInfoResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00300GetCompanyInfoResultDTO();

                lcCmdforDebuging = string.Format("RSP_GS_GET_COMPANY_INFO '{0}'", poParam.CCOMPANY_ID);

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

        public async Task<FAT00300GetSystemParamResultDTO> GetSystemParamAsync(FAT00300GetSystemParamParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetAssetAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetSystemParamResultDTO loRtn = new FAT00300GetSystemParamResultDTO();
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
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParam.CLANG_ID);

                DataTable loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00300GetSystemParamResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00300GetSystemParamResultDTO();

                lcCmdforDebuging = string.Format("RSP_FA_GET_SYSTEM_PARAM '{0}', '{1}' ", poParam.CCOMPANY_ID, poParam.CLANG_ID);

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

        public async Task<FAT00300GetPeriodInfoResultDTO> GetPeriodInfoAsync(FAT00300GetPeriodInfoParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetAssetAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetPeriodInfoResultDTO loRtn = new FAT00300GetPeriodInfoResultDTO();
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
                loRtn = R_Utility.R_ConvertTo<FAT00300GetPeriodInfoResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00300GetPeriodInfoResultDTO();

                lcCmdforDebuging = string.Format("RSP_FA_GET_SYSTEM_PARAM '{0}', '{1}', '{2}' ", poParam.CCOMPANY_ID, poParam.CYEAR, poParam.CPERIOD_NO);

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

        public async Task<FAT00300GetTransCodeInfoResultDTO> GetTransCodeInfoAsync(FAT00300GetTransCodeInfoParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetAssetAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetTransCodeInfoResultDTO loRtn = new FAT00300GetTransCodeInfoResultDTO();
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
                loRtn = R_Utility.R_ConvertTo<FAT00300GetTransCodeInfoResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00300GetTransCodeInfoResultDTO();

                lcCmdforDebuging = string.Format("RSP_GS_GET_TRANS_CODE_INFO '{0}', '{1}'", poParam.CCOMPANY_ID, poParam.CTRANS_CODE);

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

        public async Task<FAT00300GetPeriodRangeResultDTO> GetPeriodRangeAsync(FAT00300GetPeriodRangeParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            string lcMethod = nameof(GetAssetAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAT00300GetPeriodRangeResultDTO loRtn = new FAT00300GetPeriodRangeResultDTO();
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
                loRtn = R_Utility.R_ConvertTo<FAT00300GetPeriodRangeResultDTO>(loDataTable).FirstOrDefault() ?? new FAT00300GetPeriodRangeResultDTO();

                lcCmdforDebuging = string.Format("RSP_GS_GET_TRANS_CODE_INFO '{0}', '{1}', '{2}'", poParam.CCOMPANY_ID, poParam.CCYEAR, poParam.CMODE);

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
    }
}

