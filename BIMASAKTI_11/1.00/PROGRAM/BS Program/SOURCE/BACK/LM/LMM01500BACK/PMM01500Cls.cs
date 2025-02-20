using PMM01500COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Input;
using R_StorageCommon;
using R_Storage;
using System.Diagnostics;
using System.Transactions;

namespace PMM01500BACK
{
    public class PMM01500Cls : R_BusinessObject<PMM01500DTO>
    {
        RSP_PM_MAINTAIN_INVOICE_GRPResources.Resources_Dummy_Class _loRsp = new RSP_PM_MAINTAIN_INVOICE_GRPResources.Resources_Dummy_Class();
        RSP_PM_MAINTAIN_INVGRP_CHARGESResources.Resources_Dummy_Class _loRsp2 = new RSP_PM_MAINTAIN_INVGRP_CHARGESResources.Resources_Dummy_Class();
        RSP_PM_MAINTAIN_INVGRP_PENALTYResources.Resources_Dummy_Class _loRsp3 = new RSP_PM_MAINTAIN_INVGRP_PENALTYResources.Resources_Dummy_Class();
        RSP_PM_MAINTAIN_INVGRP_BANK_ACC_DEPTResources.Resources_Dummy_Class _loRsp4 = new RSP_PM_MAINTAIN_INVGRP_BANK_ACC_DEPTResources.Resources_Dummy_Class();
        RSP_GS_GET_STORAGE_TYPEResources.Resources_Dummy_Class _loRspStorage = new RSP_GS_GET_STORAGE_TYPEResources.Resources_Dummy_Class();

        private LoggerPMM01500 _Logger;
        private readonly ActivitySource _activitySource;

        public PMM01500Cls()
        {
            _Logger = LoggerPMM01500.R_GetInstanceLogger();
            _activitySource = PMM01500ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMM01500DTOPropety> GetProperty(PMM01500PropertyParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetProperty");
            var loEx = new R_Exception();
            List<PMM01500DTOPropety> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01500DTOPropety>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01500StampRateDTO> GetAllStampRate(string pcPropertyId)
        {
            using Activity activity = _activitySource.StartActivity("GetProperty");
            var loEx = new R_Exception();
            List<PMM01500StampRateDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_STAMP_RATE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, pcPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_STAMP_RATE_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01500StampRateDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01501DTO> GetAllInvoiceGrp(PMM01501ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllInvoiceGrp");
            var loEx = new R_Exception();
            List<PMM01501DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_INVOICE_GROUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_INVOICE_GROUP_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01501DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01500DTOInvTemplate> GetInvoiceTemplate(string pcPropertyId)
        {
            using Activity activity = _activitySource.StartActivity("GetInvoiceTemplate");
            var loEx = new R_Exception();
            List<PMM01500DTOInvTemplate> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GET_REPORT_TEMPLATE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, pcPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, 30, "PMM01500");
                loDb.R_AddCommandParameter(loCmd, "@CTEMPLATE_ID", DbType.String, 20, "");

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GET_REPORT_TEMPLATE_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01500DTOInvTemplate>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public List<PMM01500UniversalDTO> GetUniversalList(string pcParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetUniversalList");
            var loEx = new R_Exception();
            List<PMM01500UniversalDTO> loResult = null;

            try
            {
                string loLen = "";
                if (pcParameter == "_TAX_CODE")
                {
                    loLen = "07,08";
                }

                var loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', @CCOMPANY_ID , @CPARAMETER, @CLEN , @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPARAMETER", DbType.String, 50, pcParameter);
                loDb.R_AddCommandParameter(loCmd, "@CLEN", DbType.String, 50, loLen);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                string loCompanyIdLog = null;
                string loUserLanLog = null;
                string loParameterLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CPARAMETER":
                            loParameterLog = (string)x.Value;
                            break;
                        case "@CUSER_LANGUAGE":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "{1}, {2} , {3})", loCompanyIdLog, loParameterLog, loLen, loUserLanLog);
                _Logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01500UniversalDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override PMM01500DTO R_Display(PMM01500DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMM01500DTO loResult = null;
            R_ReadResult loReadResult = null;
            R_ReadParameter loReadParameter;
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();


                var lcQuery = "RSP_PM_GET_INVOICE_GROUP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_INVOICE_GROUP {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMM01500DTO>(loDataTable).FirstOrDefault();

                //Get Storage
                if (String.IsNullOrEmpty(loResult.CSTORAGE_ID) == false)
                {
                    loReadParameter = new R_ReadParameter()
                    {
                        StorageId = loResult.CSTORAGE_ID
                    };

                    loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                    loResult.Data = loReadResult.Data;
                    loResult.FileName = loReadResult.FileName;
                    loResult.FileExtension = loReadResult.FileExtension;
                    loResult.FileNameExtension = loReadResult.FileName + loReadResult.FileExtension;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
            }
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public bool ValidateCheckDataTab2(PMM01500DTO poNewEntity)
        {
            using Activity activity = _activitySource.StartActivity("ValidateCheckDataTab2");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            PMM01500DTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM PMM_INVGRP_BANK_ACC_DEPT (NOLOCK) " +
                   "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                   "AND CPROPERTY_ID = @CPROPERTY_ID " +
                   "AND CINVGRP_CODE = @CINVGRP_CODE ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

                //Debug Logs
                string loCompanyIdLog = null;
                string loPropertyIdLog = null;
                string loInvGrpCodeLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CPROPERTY_ID":
                            loPropertyIdLog = (string)x.Value;
                            break;
                        case "@CINVGRP_CODE":
                            loInvGrpCodeLog = (string)x.Value;
                            break;
                    }
                });
                lcQueryLog = string.Format("SELECT TOP 1 1 FROM PMM_INVGRP_BANK_ACC_DEPT (NOLOCK) " +
                     "WHERE CCOMPANY_ID = {0} " +
                     "AND CPROPERTY_ID = {1} " +
                     "AND CINVGRP_CODE = {2} ", loCompanyIdLog, loPropertyIdLog, loInvGrpCodeLog);
                _Logger.LogDebug(lcQueryLog);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01500DTO>(loDataTable).FirstOrDefault();

                //Validasi Add
                if (loResult == null)
                {
                    llRtn = false;
                }
                else
                {
                    llRtn = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }

        #region Saving
        protected override void R_Saving(PMM01500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            PMM01500DTO loResult = null;

            try
            {
                // if (poNewEntity.DeleteAllTabDept)
                // {
                //     var loParam = R_Utility.R_ConvertObjectToObject<PMM01500DTO, PMM01510DTO>(poNewEntity);
                //     DeleteAllDataByInvoiceGroupCode(loParam);
                // }

                // var loGetStorageType = GetStorageType();

                // var loValidate = ValidateAlreadyExists(poNewEntity);
                // loResult = SetStorageID(poNewEntity, loGetStorageType, loValidate);

                // SaveDataSP(loResult, poCRUDMode);
             
                SaveDataSP(poNewEntity, poCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            
            loEx.ThrowExceptionIfErrors();
        }
        #region Delete All Data Tab Template 
        private List<PMM01510DTO> GetAllTemplateAndBankAccount(PMM01510DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllTemplateAndBankAccount");
            var loEx = new R_Exception();
            List<PMM01510DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_INVGRP_DEPT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_INVGRP_DEPT_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01510DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        private void DeleteStorageData(PMM01511DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("DeleteStorageData");
            var loEx = new R_Exception();
            var loDb = new R_Db();
            DbConnection loConn = null;
            R_DeleteParameter loDeleteParameter;

            try
            {
                loConn = loDb.GetConnection();

                if (String.IsNullOrEmpty(poEntity.CSTORAGE_ID) == false)
                {
                    loDeleteParameter = new R_DeleteParameter()
                    {
                        StorageId = poEntity.CSTORAGE_ID,
                        UserId = R_BackGlobalVar.USER_ID
                    };
                    R_StorageUtility.DeleteFile(loDeleteParameter, loConn);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
            loEx.ThrowExceptionIfErrors();
        }
        private void DeleteDataSP(PMM01511DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("DeleteDataSP");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                // set action delete
                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_PM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_INVGRP_BANK_ACC_DEPT {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                _Logger.LogError(loEx);
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
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void DeleteAllDataByInvoiceGroupCode(PMM01510DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("DeleteAllDataByInvoiceGroupCode");
            var loEx = new R_Exception();
            PMM01511DTO loParam = null;

            try
            {
                var loListData = GetAllTemplateAndBankAccount(poEntity);

                if (loListData.Count > 0)
                {

                    foreach (var item in loListData)
                    {
                        loParam = R_Utility.R_ConvertObjectToObject<PMM01510DTO, PMM01511DTO>(item);
                        DeleteStorageData(loParam);
                    }

                    loParam = R_Utility.R_ConvertObjectToObject<PMM01510DTO, PMM01511DTO>(poEntity);
                    DeleteDataSP(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Set Storage ID
        private PMM01500DTOStorageType GetStorageType()
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            PMM01500DTOStorageType loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
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
                    _Logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<PMM01500DTOStorageType>(loDataTable).FirstOrDefault();
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
                _Logger.LogError(loEx);
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
        private bool ValidateAlreadyExists(PMM01500DTO poNewEntity)
        {
            using Activity activity = _activitySource.StartActivity("ValidateAlreadyExists");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            PMM01500DTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM PMM_INVGRP (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    "AND CPROPERTY_ID = @CPROPERTY_ID " +
                    "AND CINVGRP_CODE = @CINVGRP_CODE ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

                //Debug Logs
                string loCompanyIdLog = null;
                string loPropertyIdLog = null;
                string loInvGrpCodeLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CPROPERTY_ID":
                            loPropertyIdLog = (string)x.Value;
                            break;
                        case "@CINVGRP_CODE":
                            loInvGrpCodeLog = (string)x.Value;
                            break;
                    }
                });
                lcQueryLog = string.Format("SELECT TOP 1 1 FROM PMM_INVGRP (NOLOCK) " +
                     "WHERE CCOMPANY_ID = '{0}' " +
                     "AND CPROPERTY_ID = '{1}' " +
                     "AND CINVGRP_CODE = '{2}' ", loCompanyIdLog, loPropertyIdLog, loInvGrpCodeLog);
                _Logger.LogDebug(lcQueryLog);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01500DTO>(loDataTable).FirstOrDefault();

                //Validasi Add
                if (loResult == null)
                {
                    llRtn = true;
                }
                else
                {
                    llRtn = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }
        private PMM01500DTO SetStorageID(PMM01500DTO poNewEntity, PMM01500DTOStorageType poStorageType, bool plExists)
        {
            using Activity activity = _activitySource.StartActivity("SetStorageID");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            string lcQueryLog = "";
            R_SaveResult loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = loDb.GetConnection();
                loConnAttr = loDb.GetConnectionAttribute();
                if (poNewEntity.LBY_DEPARTMENT ==  false)
                {
                    if (plExists)
                    {
                        //Set Storage Type
                        R_EStorageType loStorageType;
                        loStorageType = poStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;

                        R_EProviderForCloudStorage loProvider;
                        loProvider = poStorageType.CSTORAGE_PROVIDER_ID.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                        R_AddParameter loAddParameter;
                        loAddParameter = new R_AddParameter()
                        {
                            StorageType = loStorageType,
                            ProviderCloudStorage = loProvider,
                            FileName = poNewEntity.FileName,
                            FileExtension = poNewEntity.FileExtension,
                            UploadData = poNewEntity.Data,
                            UserId = poNewEntity.CUSER_ID,
                            BusinessKeyParameter = new R_BusinessKeyParameter()
                            {
                                CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                                CDATA_TYPE = "STORAGE_DATA_TABLE",
                                CKEY01 = poNewEntity.CPROPERTY_ID,
                                CKEY02 = poNewEntity.CINVGRP_CODE,
                            }
                        };
                        loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                        //Set Storage ID CSTORAGE_ID
                        poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(poNewEntity.CSTORAGE_ID) == false)
                        {
                            R_UpdateParameter loUpdateParameter;

                            loUpdateParameter = new R_UpdateParameter()
                            {
                                StorageId = poNewEntity.CSTORAGE_ID,
                                UploadData = poNewEntity.Data,
                                UserId = poNewEntity.CUSER_ID,
                                OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                                {
                                    FileExtension = poNewEntity.FileExtension,
                                    FileName = poNewEntity.FileName
                                }
                            };
                            loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);

                            //Set Storage ID CSTORAGE_ID
                            poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
                        }
                        else
                        {
                            //Set Storage Type
                            R_EStorageType loStorageType;
                            loStorageType = poStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;

                            R_EProviderForCloudStorage loProvider;
                            loProvider = poStorageType.CSTORAGE_PROVIDER_ID.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                            R_AddParameter loAddParameter;
                            loAddParameter = new R_AddParameter()
                            {
                                StorageType = loStorageType,
                                ProviderCloudStorage = loProvider,
                                FileName = poNewEntity.FileName,
                                FileExtension = poNewEntity.FileExtension,
                                UploadData = poNewEntity.Data,
                                UserId = poNewEntity.CUSER_ID,
                                BusinessKeyParameter = new R_BusinessKeyParameter()
                                {
                                    CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                                    CDATA_TYPE = "STORAGE_DATA_TABLE",
                                    CKEY01 = poNewEntity.CPROPERTY_ID,
                                    CKEY02 = poNewEntity.CINVGRP_CODE,
                                }
                            };
                            loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                            //Set Storage ID CSTORAGE_ID
                            poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
                        }
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(poNewEntity.CSTORAGE_ID) == false)
                    {
                        var loDeleteParameter = new R_DeleteParameter()
                        {
                            StorageId = poNewEntity.CSTORAGE_ID,
                            UserId = poNewEntity.CUSER_ID
                        };
                        R_StorageUtility.DeleteFile(loDeleteParameter, loConn);

                        //Set Storage ID CSTORAGE_ID
                        poNewEntity.CSTORAGE_ID = "";
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
        #endregion

        #region Save Data
        private void SaveDataSP(PMM01500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveDataSP");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            PMM01500DTO loResult = null;

            try
            {
                //Set Action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_MAINTAIN_INVOICE_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 8, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 100, poNewEntity.CINVGRP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 6, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 2, poNewEntity.CINVOICE_DUE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 2, poNewEntity.CINVOICE_GROUP_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 100, poNewEntity.IDUE_DAYS);
                loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 100, poNewEntity.IFIXED_DUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 100, poNewEntity.ILIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 100, poNewEntity.IBEFORE_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 100, poNewEntity.IAFTER_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poNewEntity.LUSE_STAMP);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_CODE", DbType.String, 20, poNewEntity.CSTAMP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 20, poNewEntity.CSTAMP_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poNewEntity.LBY_DEPARTMENT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DEPT_CODE", DbType.String, 20, poNewEntity.CINV_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 100, poNewEntity.CINVOICE_TEMPLATE);
                //loDb.R_AddCommandParameter(loCmd, "@LGENERAL_TEMPLATE", DbType.Boolean, 50, poNewEntity.LGENERAL_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 8, poNewEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 20, poNewEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@LTAX_EXEMPTION", DbType.Boolean, 50, poNewEntity.LTAX_EXEMPTION);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EXEMPTION_CODE", DbType.String, 2, poNewEntity.CTAX_EXEMPTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ADD_DESCR", DbType.String, 2, poNewEntity.CTAX_ADD_DESCR);
                // loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poNewEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                     .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_INVOICE_GRP {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                _Logger.LogError(loEx);
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
        }
        #endregion
        #endregion
        protected override void R_Deleting(PMM01500DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_DeleteParameter loDeleteParameter;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection();
                    loCmd = loDb.GetCommand();

                    if (String.IsNullOrEmpty(poEntity.CSTORAGE_ID) == false)
                    {
                        loDeleteParameter = new R_DeleteParameter()
                        {
                            StorageId = poEntity.CSTORAGE_ID,
                            UserId = poEntity.CUSER_ID
                        };
                        R_StorageUtility.DeleteFile(loDeleteParameter, loConn);
                    }

                    lcQuery = "RSP_PM_MAINTAIN_INVOICE_GRP";
                    loCmd.CommandText = lcQuery;
                    loCmd.CommandType = CommandType.StoredProcedure;

                    // set action delete
                    poEntity.CACTION = "DELETE";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 8, poEntity.CINVGRP_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 100, poEntity.CINVGRP_NAME);
                    loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 6, poEntity.CSEQUENCE);
                    loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
                    loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 2, poEntity.CINVOICE_DUE_MODE);
                    loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 2, poEntity.CINVOICE_GROUP_MODE);
                    loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 100, poEntity.IDUE_DAYS);
                    loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 100, poEntity.IFIXED_DUE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 100, poEntity.ILIMIT_INVOICE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 100, poEntity.IBEFORE_LIMIT_INVOICE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 100, poEntity.IAFTER_LIMIT_INVOICE_DATE);
                    loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                    loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                    loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                    loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poEntity.LUSE_STAMP);
                    loDb.R_AddCommandParameter(loCmd, "@CSTAMP_CODE", DbType.String, 20, poEntity.CSTAMP_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 20, poEntity.CSTAMP_ADD_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);
                    loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poEntity.LBY_DEPARTMENT);
                    loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DEPT_CODE", DbType.String, 20, poEntity.CINV_DEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 100, poEntity.CINVOICE_TEMPLATE);
                    //loDb.R_AddCommandParameter(loCmd, "@LGENERAL_TEMPLATE", DbType.Boolean, 50, poEntity.LGENERAL_TEMPLATE);
                    loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 8, poEntity.CBANK_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 20, poEntity.CBANK_ACCOUNT);
                    loDb.R_AddCommandParameter(loCmd, "@LTAX_EXEMPTION", DbType.Boolean, 50, poEntity.LTAX_EXEMPTION);
                    loDb.R_AddCommandParameter(loCmd, "@CTAX_EXEMPTION_CODE", DbType.String, 2, poEntity.CTAX_EXEMPTION_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CTAX_ADD_DESCR", DbType.String, 2, poEntity.CTAX_ADD_DESCR);
                    loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poEntity.CSTORAGE_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                    R_ExternalException.R_SP_Init_Exception(loConn);

                    try
                    {
                        //Debug Logs
                        var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                        _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_INVOICE_GRP {@poParameter}", loDbParam);

                        loDb.SqlExecNonQuery(loConn, loCmd, false);
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void PMM01500ActiveInactiveSP(PMM01500DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("PMM01500ActiveInactiveSP");
            R_Exception loException = new R_Exception();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_PM_ACTIVE_INACTIVE_INVGRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CPROPERTY_ID" ||
                        x.ParameterName == "@CINVGRP_CODE" ||
                        x.ParameterName == "@LACTIVE" ||
                        x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_ACTIVE_INACTIVE_INVGRP {@poParameter}", loDbParam);

                loDb.SqlExecQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        public List<PMM01502DTO> PMM01530LookupBank(PMM01502DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("PMM01530LookupBank");
            var loEx = new R_Exception();
            List<PMM01502DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT CCB_CODE, CCB_NAME FROM GSM_CB (NOLOCK) " +
                    string.Format("WHERE CCOMPANY_ID = '{0}' ", poEntity.CCOMPANY_ID) +
                    "AND CCB_TYPE = 'B' ";

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("SELECT CCB_CODE, CCB_NAME FROM GSM_CB (NOLOCK) " +
                    string.Format("WHERE CCOMPANY_ID = '{0}' ", poEntity.CCOMPANY_ID) +
                    "AND CCB_TYPE = 'B' ");

                loResult = loDb.SqlExecObjectQuery<PMM01502DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
