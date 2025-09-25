using PMT00500COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using System.Data.SqlClient;
using R_StorageCommon;
using R_Storage;
using System.Transactions;

namespace PMT00500BACK
{
    public class PMT00560Cls : R_BusinessObject<PMT00560DTO>
    {
        private LoggerPMT00560 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT00560Cls()
        {
            _Logger = LoggerPMT00560.R_GetInstanceLogger();
            _activitySource = PMT00560ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMT00560DTO> GetAllLOIDocument(PMT00560DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllLOIDocument");
            var loEx = new R_Exception();
            List<PMT00560DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_DOC_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_DOC_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT00560DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override PMT00560DTO R_Display(PMT00560DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMT00560DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_ReadResult loReadResult = null;
            R_ReadParameter loReadParameter;

            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_DOC_DT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 100, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_DOC_DT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMT00560DTO>(loDataTable).FirstOrDefault();

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

        #region Saving With Storage
        protected override void R_Saving(PMT00560DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();

            try
            {
                if (poNewEntity.Data == null)
                {
                    SavingSP(poNewEntity, poCRUDMode);
                }
                else
                {
                    var loGetStorageType = GetStorageType();

                    var loValidate = ValidateAlreadyExists(poNewEntity);
                    var loResult = SetStorageID(poNewEntity, loGetStorageType, loValidate);

                    SavingSP(loResult, poCRUDMode);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private PMT00500StorageTypeDTO GetStorageType()
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            PMT00500StorageTypeDTO loResult = null;
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
                    loResult = R_Utility.R_ConvertTo<PMT00500StorageTypeDTO>(loDataTable).FirstOrDefault();
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
        private bool ValidateAlreadyExists(PMT00560DTO poNewEntity)
        {
            using Activity activity = _activitySource.StartActivity("ValidateAlreadyExists");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            PMT00560DTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM PMT_AGREEMENT_DOC (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    "AND CPROPERTY_ID = @CPROPERTY_ID " +
                    "AND CDEPT_CODE = @CDEPT_CODE " +
                    "AND CTRANS_CODE = @CTRANS_CODE " +
                    "AND CREF_NO = @CREF_NO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);

                //Debug Logs
                string loCompanyIdLog = null;
                string loPropertyIdLog = null;
                string loRefNo = null;
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
                        case "@CREF_NO":
                            loRefNo = (string)x.Value;
                            break;
                        case "@CDEPT_CODE":
                            loDeptCodeLog = (string)x.Value;
                            break;
                    }
                });
                lcQueryLog = string.Format("SELECT TOP 1 1 FROM PMT_AGREEMENT_DOC (NOLOCK) " +
                     "WHERE CCOMPANY_ID = '{0}' " +
                     "AND CPROPERTY_ID = '{1}' " +
                     "AND CDEPT_CODE = '{2}' " +
                     "AND CTRANS_CODE = '{3}' " +
                     "AND CREF_NO = '{5}' ", loCompanyIdLog, loPropertyIdLog, loDeptCodeLog, ContextConstant.VAR_TRANS_CODE, loRefNo);
                _Logger.LogDebug(lcQueryLog);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT00560DTO>(loDataTable).FirstOrDefault();

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
        private PMT00560DTO SetStorageID(PMT00560DTO poNewEntity, PMT00500StorageTypeDTO poStorageType, bool plExists)
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
                        UserId = R_BackGlobalVar.USER_ID,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                            CDATA_TYPE = "STORAGE_DATA_TABLE",
                            CKEY01 = poNewEntity.CPROPERTY_ID,
                            CKEY02 = poNewEntity.CDEPT_CODE,
                            CKEY03 = ContextConstant.VAR_TRANS_CODE,
                            CKEY04 = poNewEntity.CREF_NO
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
                            UserId = R_BackGlobalVar.USER_ID,
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
                            UserId = R_BackGlobalVar.USER_ID,
                            BusinessKeyParameter = new R_BusinessKeyParameter()
                            {
                                CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                                CDATA_TYPE = "STORAGE_DATA_TABLE",
                                CKEY01 = poNewEntity.CPROPERTY_ID,
                                CKEY02 = poNewEntity.CDEPT_CODE,
                                CKEY03 = ContextConstant.VAR_TRANS_CODE,
                                CKEY04 = poNewEntity.CREF_NO
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
        private void SavingSP(PMT00560DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DOC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);

                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poNewEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEXPIRED_DATE", DbType.String, 8, poNewEntity.CEXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_FILE", DbType.String, 400, poNewEntity.CDOC_FILE);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 400, poNewEntity.CSTORAGE_ID);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_DOC {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecNonQuery(loConn, loCmd, false);
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

        #region Delete With Storage
        protected override void R_Deleting(PMT00560DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DeleteStorageData(poEntity);
                    DeleteDataSP(poEntity);

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void DeleteStorageData(PMT00560DTO poEntity)
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

            loEx.ThrowExceptionIfErrors();
        }
        private void DeleteDataSP(PMT00560DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DOC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);

                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 30, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEXPIRED_DATE", DbType.String, 8, poEntity.CEXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_FILE", DbType.String, 400, poEntity.CDOC_FILE);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 400, poEntity.CSTORAGE_ID);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_DOC {@poParameter}", loDbParam);

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