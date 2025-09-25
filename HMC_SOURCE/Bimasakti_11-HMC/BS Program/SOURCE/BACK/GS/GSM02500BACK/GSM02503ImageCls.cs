using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02550;
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
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500BACK
{
    public class GSM02503ImageCls : R_BusinessObject<GSM02503ImageParameterDTO>
    {
        private LoggerGSM02503Image _logger;
        private readonly ActivitySource _activitySource;
        public GSM02503ImageCls()
        {
            _logger = LoggerGSM02503Image.R_GetInstanceLogger();
            _activitySource = GSM02503ImageActivitySourceBase.R_GetInstanceActivitySource();
        }

        public ShowUnitTypeImageDTO ShowUnitTypeImage(ShowUnitTypeImageParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("ShowUnitTypeImage");
            R_Exception loException = new R_Exception();
            ShowUnitTypeImageDTO loResult = new ShowUnitTypeImageDTO();
            GSM02503ImageDTO loGetDetail = null;
            R_ReadResult loReadResult = null;
            R_ReadParameter loReadParameter = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_ID, " +
                    $"@CSELECTED_IMAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_IMAGE_ID", DbType.String, 50, poEntity.CSELECTED_IMAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL {@Parameters} || ShowUnitTypeImage(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                loGetDetail = R_Utility.R_ConvertTo<GSM02503ImageDTO>(loDataTable).FirstOrDefault();

                if (String.IsNullOrEmpty(loGetDetail.CSTORAGE_ID) == false)
                {
                    loReadParameter = new R_ReadParameter()
                    {
                        StorageId = loGetDetail.CSTORAGE_ID
                    };

                    loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                    loResult.OIMAGE = loReadResult.Data;
                    loResult.CFILE_EXTENSION = loReadResult.FileExtension;
                    loResult.CFILE_NAME = loReadResult.FileName;
                    loResult.CFILE_NAME_EXTENSION = loReadResult.FileName + loReadResult.FileExtension;
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

        private void RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(GSM02503ImageParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_TYPE_IMAGE " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CUNIT_TYPE_ID, " +
                                 $"@CIMAGE_ID, " +
                                 $"@CIMAGE_NAME, " +
                                 $"@CSTORAGE_ID, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                //loDb.R_AddCommandParameter(loCmd, "@OIMAGE", DbType.Binary, 100, poEntity.OIMAGE);
                /*
                                var loPar = loCmd.CreateParameter();
                                loPar.ParameterName = "@OIMAGE";
                                //loPar.Value = poEntity.OIMAGE;

                                loPar.Value = new SqlBinary(poEntity.OIMAGE);

                                loCmd.Parameters.Add(loPar);

                var loPar = loDb.GetParameter();
                loPar.ParameterName = "@OIMAGE";
                loPar.DbType = DbType.Binary;
                loPar.Value = poEntity.OIMAGE == null? DBNull.Value: poEntity.OIMAGE;
                
                loCmd.Parameters.Add(loPar);
                                */

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CIMAGE_ID", DbType.String, 50, poEntity.Data.CIMAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CIMAGE_NAME", DbType.String, 50, poEntity.Data.CIMAGE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 50, poEntity.Data.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_TYPE_IMAGE {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(Cls) ", loDbParam);

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

        public List<GSM02503ImageDTO> GetUnitTypeImageList(GetUnitTypeImageListParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeImageList");
            R_Exception loException = new R_Exception();
            List<GSM02503ImageDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_LIST {@Parameters} || GetUnitTypeImageList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02503ImageDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(GSM02503ImageParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                DeleteStorage(poEntity.Data);
                RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void DeleteStorage(GSM02503ImageDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("DeleteStorage");
            R_Exception loEx = new R_Exception();
            R_Db loDb = new R_Db();
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
            }

            loEx.ThrowExceptionIfErrors();
        }

        protected override GSM02503ImageParameterDTO R_Display(GSM02503ImageParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02503ImageParameterDTO loResult = new GSM02503ImageParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CUNIT_TYPE_ID, " +
                    $"@CIMAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CIMAGE_ID", DbType.String, 50, poEntity.Data.CIMAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02503ImageDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02503ImageParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            GSM02503ImageStorageTypeDTO loStorageType = null;
            GSM02503ImageDTO loStorageResult = null;

            try
            {
                loStorageType = GetStorageType();
                loStorageResult = SetStorageID(poNewEntity, loStorageType);

                poNewEntity.Data.CSTORAGE_ID = loStorageResult.CSTORAGE_ID;
                RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }


        private GSM02503ImageStorageTypeDTO GetStorageType()
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            GSM02503ImageStorageTypeDTO loResult = null;
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
                _logger.LogDebug("EXEC RSP_GS_GET_STORAGE_TYPE {@poParameter} || GetStorageType(Cls)", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GSM02503ImageStorageTypeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        private GSM02503ImageDTO SetStorageID(GSM02503ImageParameterDTO poNewEntity, GSM02503ImageStorageTypeDTO poStorageType)
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
                    FileName = poNewEntity.CFILE_NAME,
                    FileExtension = poNewEntity.CFILE_EXTENSION,
                    UploadData = poNewEntity.OIMAGE,
                    UserId = poNewEntity.CLOGIN_USER_ID,
                    BusinessKeyParameter = new R_BusinessKeyParameter()
                    {
                        CCOMPANY_ID = poNewEntity.CLOGIN_COMPANY_ID,
                        CDATA_TYPE = "STORAGE_DATA_TABLE",
                        CKEY01 = poNewEntity.CSELECTED_PROPERTY_ID,
                        CKEY02 = poNewEntity.CUNIT_TYPE_ID,
                        CKEY03 = poNewEntity.Data.CIMAGE_ID
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
