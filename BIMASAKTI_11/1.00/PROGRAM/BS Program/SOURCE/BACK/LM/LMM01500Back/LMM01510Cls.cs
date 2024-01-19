using LMM01500COMMON;
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
using R_Storage;
using R_StorageCommon;
using System.Diagnostics;
using System.Transactions;

namespace LMM01500BACK
{
    public class LMM01510Cls : R_BusinessObject<LMM01511DTO>
    {
        private LoggerLMM01510 _Logger;
        public LMM01510Cls()
        {
            _Logger = LoggerLMM01510.R_GetInstanceLogger();
        }

        public List<LMM01510DTO> GetAllTemplateAndBankAccount(LMM01510DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01510DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
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

                loResult = R_Utility.R_ConvertTo<LMM01510DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Delete Data 
        protected override void R_Deleting(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();


            try
            {
                DeleteStorageData(poEntity);
                DeleteDataSP(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
           
            loEx.ThrowExceptionIfErrors();
        }
        private void DeleteStorageData(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();
            var loDb = new R_Db();
            DbConnection loConn = null;
            R_DeleteParameter loDeleteParameter;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

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
           
            loEx.ThrowExceptionIfErrors();
        }
        private void DeleteDataSP(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                // set action delete
                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
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
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT {@poParameter}", loDbParam);

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
        #endregion

        protected override LMM01511DTO R_Display(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01511DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_ReadResult loReadResult = null;
            R_ReadParameter loReadParameter;

            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_INVGRP_DEPT_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
               .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_INVGRP_DEPT_DETAIL {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<LMM01511DTO>(loDataTable).FirstOrDefault();

                //Get Storage
                if (String.IsNullOrEmpty(loResult.CSTORAGE_ID) == false)
                {
                    loReadParameter = new R_ReadParameter()
                    {
                        StorageId = loResult.CSTORAGE_ID
                    };

                    loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                    loResult.Data = loReadResult.Data;
                    loResult.FileExtension = loReadResult.FileExtension;
                    loResult.FileName = loReadResult.FileName;
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

        #region Saving Data
        protected override void R_Saving(LMM01511DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loGetStorageType = GetStorageType();

                var loValidate = ValidateAlreadyExists(poNewEntity);
                var loResult = SetStorageID(poNewEntity, loGetStorageType, loValidate);

                SaveDataSP(loResult, poCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private LMM01500DTOStorageType GetStorageType()
        {
            var loEx = new R_Exception();
            LMM01500DTOStorageType loResult = null;
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

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01500DTOStorageType>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        private bool ValidateAlreadyExists(LMM01511DTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            LMM01511DTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM LMM_INVGRP_BANK_ACC_DEPT (NOLOCK) " +
                   "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                   "AND CPROPERTY_ID = @CPROPERTY_ID " +
                   "AND CINVGRP_CODE = @CINVGRP_CODE " +
                   "AND CDEPT_CODE = @CDEPT_CODE ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);

                //Debug Logs
                string loCompanyIdLog = null;
                string loPropertyIdLog = null;
                string loInvGrpCodeLog = null;
                string loDeptCodeLog = null;
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
                        case "@CDEPT_CODE":
                            loDeptCodeLog = (string)x.Value;
                            break;
                    }
                });
                lcQueryLog = string.Format("SELECT TOP 1 1 FROM LMM_INVGRP_BANK_ACC_DEPT (NOLOCK) " +
                     "WHERE CCOMPANY_ID = {0} " +
                     "AND CPROPERTY_ID = {1} " +
                     "AND CINVGRP_CODE = {2} " +
                     "AND CDEPT_CODE = {3} ", loCompanyIdLog, loPropertyIdLog, loInvGrpCodeLog, loDeptCodeLog);
                _Logger.LogDebug(lcQueryLog);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01511DTO>(loDataTable).FirstOrDefault();

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
        private LMM01511DTO SetStorageID(LMM01511DTO poNewEntity, LMM01500DTOStorageType poStorageType, bool plExists)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            R_SaveResult loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = loDb.GetConnection();
                loConnAttr = loDb.GetConnectionAttribute();

                if (plExists == true)
                {
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
                            CKEY03 = poNewEntity.CDEPT_CODE,
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

                        //Add and create Storage ID
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
                                CKEY03 = poNewEntity.CDEPT_CODE,
                            }
                        };
                        loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                        //Set Storage ID CSTORAGE_ID
                        poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
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
        private void SaveDataSP(LMM01511DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

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

                lcQuery = "RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poNewEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poNewEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poNewEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poNewEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                     .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT {@poParameter}", loDbParam);

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
    }
}
