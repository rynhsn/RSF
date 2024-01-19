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
using R_StorageCommon;
using R_Storage;

namespace LMM01500BACK
{
    public class LMM01500Cls : R_BusinessObject<LMM01500DTO>
    {
        private LoggerLMM01500 _Logger;

        public LMM01500Cls()
        {
            _Logger = LoggerLMM01500.R_GetInstanceLogger();
        }

        public List<LMM01500DTOPropety> GetProperty(LMM01500PropertyParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01500DTOPropety> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
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

                loResult = R_Utility.R_ConvertTo<LMM01500DTOPropety>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01501DTO> GetAllInvoiceGrp(LMM01501ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01501DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_INVOICE_GROUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_INVOICE_GROUP_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01501DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override LMM01500DTO R_Display(LMM01500DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01500DTO loResult = null;
            R_ReadResult loReadResult = null;
            R_ReadParameter loReadParameter;
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();


                var lcQuery = "RSP_LM_GET_INVOICE_GROUP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CPROPERTY_ID" ||
                        x.ParameterName == "@CINVGRP_CODE" ||
                        x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_INVOICE_GROUP {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<LMM01500DTO>(loDataTable).FirstOrDefault();

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

        protected override void R_Saving(LMM01500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            LMM01500DTO loResult = null;

            try
            {
                if (poNewEntity.DeleteAllTabDept)
                {
                    var loParam = R_Utility.R_ConvertObjectToObject<LMM01500DTO, LMM01510DTO>(poNewEntity);
                    DeleteAllDataByInvoiceGroupCode(loParam);
                }

                var loGetStorageType = GetStorageType();

                var loValidate = ValidateAlreadyExists(poNewEntity);
                loResult = SetStorageID(poNewEntity, loGetStorageType, loValidate);

                SaveDataSP(loResult, poCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            
            loEx.ThrowExceptionIfErrors();
        }
        public bool ValidateCheckDataTab2(LMM01500DTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            LMM01500DTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM LMM_INVGRP_BANK_ACC_DEPT (NOLOCK) " +
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
                lcQueryLog = string.Format("SELECT TOP 1 1 FROM LMM_INVGRP_BANK_ACC_DEPT (NOLOCK) " +
                     "WHERE CCOMPANY_ID = {0} " +
                     "AND CPROPERTY_ID = {1} " +
                     "AND CINVGRP_CODE = {2} ", loCompanyIdLog, loPropertyIdLog, loInvGrpCodeLog);
                _Logger.LogDebug(lcQueryLog);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01500DTO>(loDataTable).FirstOrDefault();

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
        #region Delete All Data Tab Template 
        private List<LMM01510DTO> GetAllTemplateAndBankAccount(LMM01510DTO poEntity)
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
        public void DeleteAllDataByInvoiceGroupCode(LMM01510DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01511DTO loParam = null;

            try
            {
                var loListData = GetAllTemplateAndBankAccount(poEntity);

                if (loListData.Count > 0)
                {

                    foreach (var item in loListData)
                    {
                        loParam = R_Utility.R_ConvertObjectToObject<LMM01510DTO, LMM01511DTO>(item);
                        DeleteStorageData(loParam);
                    }

                    loParam = R_Utility.R_ConvertObjectToObject<LMM01510DTO, LMM01511DTO>(poEntity);
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
        private LMM01500DTOStorageType GetStorageType()
        {
            var loEx = new R_Exception();
            LMM01500DTOStorageType loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
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
        private bool ValidateAlreadyExists(LMM01500DTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            LMM01500DTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM LMM_INVGRP (NOLOCK) " +
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
                lcQueryLog = string.Format("SELECT TOP 1 1 FROM LMM_INVGRP (NOLOCK) " +
                     "WHERE CCOMPANY_ID = {0} " +
                     "AND CPROPERTY_ID = {1} " +
                     "AND CINVGRP_CODE = {2} ", loCompanyIdLog, loPropertyIdLog, loInvGrpCodeLog);
                _Logger.LogDebug(lcQueryLog);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01500DTO>(loDataTable).FirstOrDefault();

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
        private LMM01500DTO SetStorageID(LMM01500DTO poNewEntity, LMM01500DTOStorageType poStorageType, bool plExists)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            string lcQueryLog = "";
            R_SaveResult loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
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
                        R_StorageUtility.DeleteFile(loDeleteParameter, "R_DefaultConnectionString");

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
        private void SaveDataSP(LMM01500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            LMM01500DTO loResult = null;

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

                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 50, poNewEntity.CINVGRP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 50, poNewEntity.CINVOICE_DUE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 50, poNewEntity.CINVOICE_GROUP_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 50, poNewEntity.IDUE_DAYS);
                loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 50, poNewEntity.IFIXED_DUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.ILIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IBEFORE_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IAFTER_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poNewEntity.LUSE_STAMP);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 50, poNewEntity.CSTAMP_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poNewEntity.LBY_DEPARTMENT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poNewEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
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
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_INVOICE_GRP {@poParameter}", loDbParam);

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
        protected override void R_Deleting(LMM01500DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_DeleteParameter loDeleteParameter;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                if (String.IsNullOrEmpty(poEntity.CSTORAGE_ID) == false)
                {
                    loDeleteParameter = new R_DeleteParameter()
                    {
                        StorageId = poEntity.CSTORAGE_ID,
                        UserId = poEntity.CUSER_ID
                    };
                    R_StorageUtility.DeleteFile(loDeleteParameter, "R_DefaultConnectionString");
                }

                lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                // set action delete
                poEntity.CACTION = "DELETE";


                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 50, poEntity.CINVGRP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 50, poEntity.CINVOICE_DUE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 50, poEntity.CINVOICE_GROUP_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 50, poEntity.IDUE_DAYS);
                loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 50, poEntity.IFIXED_DUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 50, poEntity.ILIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 50, poEntity.IBEFORE_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 50, poEntity.IAFTER_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poEntity.LUSE_STAMP);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 50, poEntity.CSTAMP_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poEntity.LBY_DEPARTMENT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_INVOICE_GRP {@poParameter}", loDbParam);

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
        public void LMM01500ActiveInactiveSP(LMM01500DTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_LM_ACTIVE_INACTIVE_INVGRP";
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
                _Logger.LogDebug("EXEC RSP_LM_ACTIVE_INACTIVE_INVGRP {@poParameter}", loDbParam);

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
        public List<LMM01502DTO> LMM01530LookupBank(LMM01502DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01502DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
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

                loResult = loDb.SqlExecObjectQuery<LMM01502DTO>(lcQuery, loConn, true);
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
