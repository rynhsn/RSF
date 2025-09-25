using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Storage;
using R_StorageCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500BACK
{
    public class GSM02501Cls : R_BusinessObjectAsync<GSM02501ParameterDTO>
    {
        RSP_GS_MAINTAIN_PROPERTYResources.Resources_Dummy_Class _loRsp = new RSP_GS_MAINTAIN_PROPERTYResources.Resources_Dummy_Class();
        RSP_GS_GET_STORAGE_TYPEResources.Resources_Dummy_Class _loRsp1 = new RSP_GS_GET_STORAGE_TYPEResources.Resources_Dummy_Class();

        private LoggerGSM02501 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02501Cls()
        {
            _logger = LoggerGSM02501.R_GetInstanceLogger();
            _activitySource = GSM02501ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public async Task<List<GSM02501PropertyDTO>> GetPropertyList(GetPropertyListDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            R_Exception loException = new R_Exception();
            List<GSM02501PropertyDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@Parameters} || GetPropertyList(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02501PropertyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override async Task R_DeletingAsync(GSM02501ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                await RSP_GS_MAINTAIN_PROPERTYMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override async Task<GSM02501ParameterDTO> R_DisplayAsync(GSM02501ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02501ParameterDTO loResult = new GSM02501ParameterDTO();
            R_Db loDb = new R_Db();
            R_ReadResult loReadResult = null;
            R_ReadParameter loReadParameter = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_DETAIL " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.Data.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);

                loResult.Data = R_Utility.R_ConvertTo<GSM02501DetailDTO>(loDataTable).FirstOrDefault();
                
                if (string.IsNullOrEmpty(loResult.Data.CSTORAGE_ID) == false)
                {
                    loReadParameter = new R_ReadParameter()
                    {
                        StorageId = loResult.Data.CSTORAGE_ID
                    };

                    loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                    loResult.Data.OIMAGE = loReadResult.Data;
                    loResult.Data.CFILE_EXTENSION = loReadResult.FileExtension;
                    loResult.Data.CFILE_NAME = loReadResult.FileName;
                    //loResult.Data.CFILE_NAME_EXTENSION = loReadResult.FileName + loReadResult.FileExtension;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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

            return loResult;
        }

        protected override async Task R_SavingAsync(GSM02501ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            GSM02503ImageStorageTypeDTO loStorageType = null;
            GSM02501DetailDTO loStorageResult = null;

            try
            {
                loStorageType = await GetStorageType();
                loStorageResult = await SetStorageID(poNewEntity, loStorageType);

                poNewEntity.Data.CSTORAGE_ID = loStorageResult.CSTORAGE_ID;

                await RSP_GS_MAINTAIN_PROPERTYMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_PROPERTY " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@LACTIVE, " +
                    $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.String, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_PROPERTY {@Parameters} || RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(Cls) ", loDbParam);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        private async Task RSP_GS_MAINTAIN_PROPERTYMethod(GSM02501ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_PROPERTYMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = $"RSP_GS_MAINTAIN_PROPERTY " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CPROPERTY_NAME, " +
                                 $"@CADDRESS, " +
                                 $"@CPROVINCE, " +
                                 $"@CCITY, " +
                                 $"@CDISTRICT, " +
                                 $"@CSUBDISTRICT, " +
                                 $"@CSALES_TAX_ID, " +
                                 $"@CCURRENCY, " +
                                 $"@CUOM, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CUSER_ID, " +
                                 $"@CSTORAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.Data.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_NAME", DbType.String, 200, poEntity.Data.CPROPERTY_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, 510, poEntity.Data.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CPROVINCE", DbType.String, 30, poEntity.Data.CPROVINCE);
                loDb.R_AddCommandParameter(loCmd, "@CCITY", DbType.String, 30, poEntity.Data.CCITY);
                loDb.R_AddCommandParameter(loCmd, "@CDISTRICT", DbType.String, 30, poEntity.Data.CDISTRICT);
                loDb.R_AddCommandParameter(loCmd, "@CSUBDISTRICT", DbType.String, 30, poEntity.Data.CSUBDISTRICT);
                loDb.R_AddCommandParameter(loCmd, "@CSALES_TAX_ID", DbType.String, 20, poEntity.Data.CSALES_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY", DbType.String, 5, poEntity.Data.CCURRENCY);
                loDb.R_AddCommandParameter(loCmd, "@CUOM", DbType.String, 20, poEntity.Data.CUOM);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poEntity.Data.CSTORAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_PROPERTY {@Parameters} || RSP_GS_MAINTAIN_PROPERTYMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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

        private async Task<GSM02503ImageStorageTypeDTO> GetStorageType()
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            GSM02503ImageStorageTypeDTO loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = await loDb.GetConnectionAsync();
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
                    _logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter}", loDbParam);

                    var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<GSM02503ImageStorageTypeDTO>(loDataTable).FirstOrDefault();
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

        private async Task<GSM02501DetailDTO> SetStorageID(GSM02501ParameterDTO poNewEntity, GSM02503ImageStorageTypeDTO poStorageType)
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
                loConn = await loDb.GetConnectionAsync();
                loConnAttr = loDb.GetConnectionAttribute();

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
                    FileName = poNewEntity.Data.CPROPERTY_ID,
                    FileExtension = poNewEntity.Data.CPROPERTY_ID,
                    UploadData = poNewEntity.Data.OIMAGE,
                    UserId = poNewEntity.CUSER_ID,
                    BusinessKeyParameter = new R_BusinessKeyParameter()
                    {
                        CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                        CDATA_TYPE = "STORAGE_DATA_TABLE",
                        CKEY01 = poNewEntity.Data.CPROPERTY_ID,
                    }
                };
                loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                //Set Storage ID CSTORAGE_ID
                poNewEntity.Data.CSTORAGE_ID = loSaveResult.StorageId;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
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
            return poNewEntity.Data;
        }
    }
}
