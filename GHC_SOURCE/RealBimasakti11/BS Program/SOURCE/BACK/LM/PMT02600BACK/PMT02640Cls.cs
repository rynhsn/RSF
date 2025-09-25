using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON.DTOs.PMT02640;
using PMT02600COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs;
using R_Storage;
using R_StorageCommon;
using System.Reflection.Metadata;

namespace PMT02600BACK
{
    public class PMT02640Cls : R_BusinessObject<PMT02640DTO>
    {
        RSP_PM_MAINTAIN_AGREEMENT_DOCResources.Resources_Dummy_Class _loRSP = new RSP_PM_MAINTAIN_AGREEMENT_DOCResources.Resources_Dummy_Class();

        private LoggerPMT02640 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT02640Cls()
        {
            _Logger = LoggerPMT02640.R_GetInstanceLogger();
            _activitySource = PMT02640ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMT02640DTO> GetAllAgreementDocument(PMT02640DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementDocument");
            var loEx = new R_Exception();
            List<PMT02640DTO> loResult = null;

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
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_DOC_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02640DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override PMT02640DTO R_Display(PMT02640DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMT02640DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_DOC_DT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 100, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_DOC_DT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT02640DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        private void RSP_PM_MAINTAIN_AGREEMENT_DOCMethod(PMT02640DTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RSP_PM_MAINTAIN_AGREEMENT_DOCMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DOC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 8, poParameter.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poParameter.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEXPIRED_DATE", DbType.String, 8, poParameter.CEXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poParameter.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_FILE", DbType.String, 400, poParameter.CFILE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 40, poParameter.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poParameter.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_DOC {@Parameters} || RSP_PM_MAINTAIN_AGREEMENT_DOCMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                //R_ExternalException.R_SP_Init_Exception(loConn);

                //try
                //{
                //    //Debug Logs
                //    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                //    .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                //    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_DOC {@poParameter}", loDbParam);

                //    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                //    var loTempResult = R_Utility.R_ConvertTo<PMT02640DTO>(loDataTable).FirstOrDefault();

                //    //if (loTempResult != null)
                //    //    poNewEntity.CDOC_NO = loTempResult.CDOC_NO;
                //}
                //catch (Exception ex)
                //{
                //    loException.Add(ex);
                //}

                //loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Saving(PMT02640DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loEx = new R_Exception();

            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                CheckDTO loCheckingResult = DocumentValidation(poNewEntity);
                PMT02640StorageTypeDTO loStorageTypeResult = GetStorageType();
                PMT02640DTO loTemp = AddOrUpdateFile(poNewEntity, loCheckingResult, loStorageTypeResult);
                poNewEntity.CSTORAGE_ID = loTemp.CSTORAGE_ID;

                RSP_PM_MAINTAIN_AGREEMENT_DOCMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private PMT02640DTO AddOrUpdateFile(PMT02640DTO poParameter, CheckDTO loCheckingParameter, PMT02640StorageTypeDTO loStorageTypeParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            PMT02640DTO loResult = new PMT02640DTO();
            var loDb = new R_Db();
            DbConnection loConn = null;
            //DbCommand loCmd = null;
            R_ConnectionAttribute loConnAttr;
            R_SaveResult loSaveResult;

            try
            {
                loConnAttr = loDb.GetConnectionAttribute();
                loConn = loDb.GetConnection();
                if (loCheckingParameter == null)
                {
                    //Set Storage Type
                    R_EStorageType loStorageType;
                    loStorageType = loStorageTypeParameter.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;

                    R_EProviderForCloudStorage loProvider;
                    loProvider = loStorageTypeParameter.CSTORAGE_PROVIDER_ID.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                    //Add and create Storage ID
                    R_AddParameter loAddParameter;

                    loAddParameter = new R_AddParameter()
                    {
                        StorageType = loStorageType,
                        ProviderCloudStorage = loProvider,
                        FileName = poParameter.CFILE_NAME,
                        FileExtension = poParameter.CFILE_EXTENSION,
                        UploadData = poParameter.OIMAGE,
                        UserId = R_BackGlobalVar.USER_ID,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                            CDATA_TYPE = "STORAGE_DATA_TABLE",
                            CKEY01 = poParameter.CPROPERTY_ID,
                            CKEY02 = poParameter.CDEPT_CODE,
                            CKEY03 = ConstantVariable.VAR_TRANS_CODE,
                            CKEY04 = poParameter.CREF_NO,
                        }
                    };
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                    //Set Storage ID CSTORAGE_ID
                    loResult.CSTORAGE_ID = loSaveResult.StorageId;
                }
                else if (loCheckingParameter != null && loCheckingParameter.IRESULT > 0)
                {
                    //Set Storage Type
                    R_EStorageType loStorageType;
                    loStorageType = loStorageTypeParameter.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;

                    R_EProviderForCloudStorage loProvider;
                    loProvider = loStorageTypeParameter.CSTORAGE_PROVIDER_ID.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                    //Add and create Storage ID
                    R_UpdateParameter loUpdateParameter;

                    loUpdateParameter = new R_UpdateParameter()
                    {
                        StorageId = poParameter.CSTORAGE_ID,
                        UploadData = poParameter.OIMAGE,
                        UserId = R_BackGlobalVar.USER_ID,
                    };
                    loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);

                    //Set Storage ID CSTORAGE_ID
                    //poParameter.CSTORAGE_ID = loSaveResult.StorageId;
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

            return loResult;
        }

        protected override void R_Deleting(PMT02640DTO poEntity)
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
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 8, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEXPIRED_DATE", DbType.String, 8, poEntity.CEXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_FILE", DbType.String, 400, poEntity.CFILE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 40, poEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_DOC {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                //CheckDTO loCheckResult = DocumentValidation(poEntity);

                //if (loCheckResult != null && loCheckResult.IRESULT > 0)
                //{
                //    R_DeleteParameter loDeleteParameter = new R_DeleteParameter()
                //    {
                //        StorageId = poEntity.CSTORAGE_ID,
                //        UserId = R_BackGlobalVar.USER_ID
                //    };
                //    R_StorageUtility.DeleteFile(loDeleteParameter, loConn);
                //}
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

        private CheckDTO DocumentValidation(PMT02640DTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementDocument");
            R_Exception loEx = new R_Exception();
            R_Db loDb = new R_Db();
            CheckDTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = "SELECT TOP 1 1 AS IRESULT FROM PMT_AGREEMENT_DOC (NOLOCK) " +
                          "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                          "AND CPROPERTY_ID = @CPROPERTY_ID " +
                          "AND CDEPT_CODE = @CDEPT_CODE " +
                          "AND CTRANS_CODE = @CTRANS_CODE " +
                          "AND CREF_NO = @CREF_NO " +
                          "AND CSTORAGE_ID = ''";

                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<CheckDTO>(loDataTable).FirstOrDefault();

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

        private PMT02640StorageTypeDTO GetStorageType()
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            PMT02640StorageTypeDTO loResult = null;
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
                _Logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter} || GetStorageType(Cls)", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT02640StorageTypeDTO>(loDataTable).FirstOrDefault();
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