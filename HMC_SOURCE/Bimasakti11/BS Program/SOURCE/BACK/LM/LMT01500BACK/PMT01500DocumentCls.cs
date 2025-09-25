using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT01500Common.DTO._7._Document;
using PMT01500Common.Logs;
using PMT01500Common.Utilities;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Storage;
using R_StorageCommon;
using RSP_PM_MAINTAIN_AGREEMENT_DOCResources;

namespace PMT01500Back
{
    public class PMT01500DocumentCls : R_BusinessObject<PMT01500DocumentDetailDTO>
    {
        private Resources_Dummy_Class _oRsp = new Resources_Dummy_Class();
        private readonly LoggerPMT01500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public PMT01500DocumentCls()
        {
            _loggerLMT01500 = LoggerPMT01500.R_GetInstanceLogger();
            _activitySource = PMT01500Activity.R_GetInstanceActivitySource();
        }

        public PMT01500DocumentHeaderDTO GetDocumentHeaderDb(PMT01500UtilitiesParameterDTO poParameterInternal, PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetDocumentHeaderDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loEx = new R_Exception();
            PMT01500DocumentHeaderDTO? loRtn = null;
            DataTable? loDataTable;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCmd = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to data of LMT01500DocumentHeaderDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT01500DocumentHeaderDTO>(loDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT01500DocumentListDTO> GetDocumentListDb(PMT01500UtilitiesParameterDTO poParameterInternal, PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetDocumentListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01500DocumentListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DOC_LIST";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500DocumentListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01500DocumentListDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override PMT01500DocumentDetailDTO R_Display(PMT01500DocumentDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01500DocumentDetailDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DOC_DT";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 20, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loRtn to data of LMT01500DocumentDetailDTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loRtn = R_Utility.R_ConvertTo<PMT01500DocumentDetailDTO>(loProfileDataTable).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override void R_Saving(PMT01500DocumentDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            PMT01500DocumentDetailDTO? loResult = null;
            R_Db? loDb = new R_Db();
            DbConnection? loConn = null;


            try
            {
                loConn = loDb.GetConnection();

                if (poNewEntity.LINVOICE_TEMPLATE)
                {
                    PMT01500StorageTypeDTO loDataStorageType = GetStorageType(loConn);
                    bool loValidate = ValidateAlreadyExists(poNewEntity, loConn);
                    loResult = R_StorageId(ref poNewEntity, loDataStorageType, loValidate, loConn);
                }
                SaveDataSP(poNewEntity, poCRUDMode, loConn);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT01500.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
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

        private PMT01500StorageTypeDTO GetStorageType(DbConnection poConnection)
        {
            string? lcMethod = nameof(GetDocumentHeaderDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            var loEx = new R_Exception();
            PMT01500StorageTypeDTO? loResult = null;
            var loDb = new R_Db();
            DbConnection? loConn = null;
            DbCommand? loCmd = null;

            try
            {
                loConn = poConnection;
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMT01500StorageTypeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT01500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private void SaveDataSP(PMT01500DocumentDetailDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCmd = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                //Set Action 
                lcAction = poCRUDMode == eCRUDMode.AddMode ? "ADD" : "EDIT";

                loConn = poConnection;
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DOC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 8, poNewEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEXPIRED_DATE", DbType.String, 8, poNewEntity.CEXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, Int16.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_FILE", DbType.String, 200, poNewEntity.CDOC_FILE);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 40, poNewEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@"))
                        .ToDictionary(x => x.ParameterName, x => x.Value);
                    _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

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
                _loggerLMT01500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool ValidateAlreadyExists(PMT01500DocumentDetailDTO poNewEntity, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection? loConn = null;
            DbCommand? loCmd = null;
            PMT01500ResultDocumentUploadDTO? loResult;
            bool llRtn = false;

            try
            {
                loConn = poConnection;
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 AS CRESULT " +
                    "FROM PMT_AGREEMENT_DOC (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    "AND CPROPERTY_ID = @CPROPERTY_ID " +
                    "AND CDEPT_CODE = @CDEPT_CODE " +
                    "AND CTRANS_CODE = @CTRANS_CODE " +
                    "AND CREF_NO = @CREF_NO " +
                    "AND CSTORAGE_ID = @CSTORAGE_ID";

                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 3, "");
                //Debug Logs
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = loDataTable.DataSet != null ? R_Utility.R_ConvertTo<PMT01500ResultDocumentUploadDTO>(loDataTable).FirstOrDefault() : new PMT01500ResultDocumentUploadDTO();

                //Validasi Add
                llRtn = !string.IsNullOrEmpty(loResult.CRESULT);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT01500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }

        private PMT01500DocumentDetailDTO R_StorageId(ref PMT01500DocumentDetailDTO poNewEntity, PMT01500StorageTypeDTO poStorageType, bool plExists, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            var loDb = new R_Db();
            DbConnection? loConn = null;
            DbCommand? loCmd = null;
            R_SaveResult? loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = poConnection;
                loConnAttr = loDb.GetConnectionAttribute();
                loCmd = loDb.GetCommand();


                if (!plExists)
                {
                    //Add and create Storage ID
                    R_AddParameter loAddParameter;

                    loAddParameter = new R_AddParameter()
                    {
                        UserId = poNewEntity.CUSER_ID,
                        StorageType = poStorageType.CSTORAGE_TYPE == "1" ? R_EStorageType.Cloud : R_EStorageType.OnPremise,
                        ProviderCloudStorage = poStorageType.CSTORAGE_PROVIDER_ID.ToLower() == "azure" ? R_EProviderForCloudStorage.azure : R_EProviderForCloudStorage.google,
                        UploadData = poNewEntity.OData,
                        FileName = poNewEntity.CFileName,
                        FileExtension = poNewEntity.CFileExtension,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                            CDATA_TYPE = "STORAGE_DATA_TABLE",
                            CKEY01 = poNewEntity.CPROPERTY_ID,
                            CKEY02 = poNewEntity.CDEPT_CODE,
                            CKEY03 = poNewEntity.CTRANS_CODE,
                            CKEY04 = poNewEntity.CREF_NO,

                        }
                    };
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                    //Set Storage ID CSTORAGE_ID
                    poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
                }
                else
                {
                    R_UpdateParameter loUpdateParameter;

                    loUpdateParameter = new R_UpdateParameter()
                    {
                        StorageId = poNewEntity.CSTORAGE_ID,
                        UserId = poNewEntity.CUSER_ID,
                        UploadData = poNewEntity.OData,
                        OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                        {
                            FileName = poNewEntity.CFileName,
                            FileExtension = poNewEntity.CFileExtension
                        }
                    };
                    loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT01500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return poNewEntity;
        }

        protected override void R_Deleting(PMT01500DocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection? loConn = null;
            DbCommand? loCmd = null;
            R_DeleteParameter loDeleteParameter;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                if (!String.IsNullOrEmpty(poEntity.CSTORAGE_ID))
                {
                    var loCheckData = ValidateAlreadyExists(poEntity, loConn);

                    if (loCheckData)
                    {
                        loDeleteParameter = new R_DeleteParameter()
                        {
                            StorageId = poEntity.CSTORAGE_ID,
                            UserId = poEntity.CUSER_ID
                        };
                        R_StorageUtility.DeleteFile(loDeleteParameter, loConn);
                    }
                }

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DOC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 8, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CEXPIRED_DATE", DbType.String, 8, poEntity.CEXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, Int32.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_FILE", DbType.String, 200, poEntity.CDOC_FILE);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 40, poEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
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
                _loggerLMT01500.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
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

    }
}