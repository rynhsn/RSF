using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using FAT00100Back.DTOs;
using FAT00100BackResources;
using FAT00100Common.DTOs;
using System.Reflection.Metadata;
using R_Storage;
using R_StorageCommon;

namespace FAT00100Back
{
    /// <summary>
    /// Result DTO for stored procedure RSP_FAT00100_SAVE_TRANS_ASSET
    /// </summary>
    public class FAT0010002SaveResultDTO
    {
        public string CREC_ID { get; set; } = string.Empty;
    }

    /// <summary>
    /// Business logic class for FAT0010002 - FA Acquisition Detail operations
    /// Handles all business logic operations for FA Acquisition Detail
    /// </summary>
    public class FAT0010002Cls : R_BusinessObjectAsync<FAT0010002DTO>
    {
        private readonly FAT00100BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT0010002Cls()
        {
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = FAT00100Activity.R_GetInstanceActivitySource();
        }

        /// <summary>
        /// Helper method to get error messages from resources
        /// </summary>
        /// <param name="pcErrorId">Error ID from resource file</param>
        /// <returns>R_Error object</returns>
        private R_Error GetError(string pcErrorId)
        {
            try
            {
                return R_Utility.R_GetError(typeof(Resources_Dummy_Class), pcErrorId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        /// <summary>
        /// Get FA Acquisition Detail Header
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, and reference no</param>
        /// <returns>Result DTO with header information</returns>
        public async Task<FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>> GetFAAcquisitionDetailHeaderAsync(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetFAAcquisitionDetailHeaderAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>
            {
                Data = new FAT0010002GetFAAcquisitionDetailHeaderResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CDEPT_CODE,  " +
                                    " A.CTRANSACTION_CODE,  " +
                                    " CREFERENCE_NO,  " +
                                    " CTRANSACTION_DATE,  " +
                                    " CSTATUS,  " +
                                    " A.CCURRENCY_CODE, " +
                                    " NLBASE_RATE_AMOUNT,  " +
                                    " NLCURRENCY_RATE_AMOUNT,  " +
                                    " NBBASE_RATE_AMOUNT,  " +
                                    " NBCURRENCY_RATE_AMOUNT, " +
                                    " NTRANSACTION_AMOUNT,  " +
                                    " NLTRANSACTION_AMOUNT,  " +
                                    " NBTRANSACTION_AMOUNT, " +
                                    " CDOCUMENT_DATE,  " +
                                    " CSUPPLIER_ID,  " +
                                    " CSUPPLIER_NAME,  " +
                                    " CFR_MODULE,  " +
                                    " CFR_DEPT_CODE,  " +
                                    " CFR_TRANSACTION_CODE,  " +
                                    " CFR_REFERENCE_NO, " +
                                    " CDEPT_NAME = ISNULL(b.DESCRIPTION, ''), " +
                                    " CCURRENCY_NAME = ISNULL(CCURRENCY_NAME, ''), " +
                                    " CTRANSACTION_NAME = ISNULL(CTRANSACTION_NAME, ''), " +
                                    " NLRATE = ROUND(NLCURRENCY_RATE_AMOUNT / NLBASE_RATE_AMOUNT, 6),   " +
                                    " NBRATE = ROUND(NBCURRENCY_RATE_AMOUNT / NBBASE_RATE_AMOUNT, 6), " +
                                    " NBXRATE = ROUND((NBCURRENCY_RATE_AMOUNT * NLBASE_RATE_AMOUNT)/(NLCURRENCY_RATE_AMOUNT * NBBASE_RATE_AMOUNT), 6) " +
                                    " FROM FAT_TRANS_HD a (nolock) " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) b " +
                                    " ON b.CODE = a.CDEPT_CODE " +
                                    " LEFT JOIN SAB_CURRENCY c (nolock)  " +
                                    " ON c.CCURRENCY_CODE = a.CCURRENCY_CODE  " +
                                    " LEFT JOIN GSM_TRANSACTION_CODE d (nolock)  " +
                                    " ON d.CCOMPANY_ID = a.CCOMPANY_ID  " +
                                    " and d.CTRANSACTION_CODE = a.CTRANSACTION_CODE  " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID  " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get transaction detail using stored procedure RSP_FAT00100_GET_TRANS_DETAIL
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, rec ID, dept code, ref no, and language ID</param>
        /// <returns>Result DTO with transaction detail information</returns>
        public async Task<FAT0010002ResultDTO<FAT0010002GetTransDetailResultDTO>> FAT0010002GetTransDetailAsync(FAT0010002GetTransDetailParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT0010002GetTransDetailAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010002ResultDTO<FAT0010002GetTransDetailResultDTO>
            {
                Data = new FAT0010002GetTransDetailResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = "RSP_FAT00100_GET_TRANS_DETAIL";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("EXEC {StoredProcedure} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT0010002GetTransDetailResultDTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data = loRtn;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get FA Acquisition Detail Asset List
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, status, and update date</param>
        /// <returns>List of asset list result DTOs</returns>
        public async Task<List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>> GetFAAcquisitionDetailAssetListAsync(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetFAAcquisitionDetailAssetListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FAT00100_GET_TRANS_ASSET_LIST ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANG_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get FA Acquisition Detail Alloc Expen Page List
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, transaction code, reference no, asset code, and asset trans seqno</param>
        /// <returns>List of expense allocation result DTOs</returns>
        public async Task<List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>> GetFAAcquisitionDetailAllocExpenPageListAsync(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            string lcMethod = nameof(GetFAAcquisitionDetailAllocExpenPageListAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT CEXPENSE_DEPT_CODE, NEXPENSE_PCT, CEXPENSE_DEPT_NAME = isnull(y.DESCRIPTION,'') " +
                                    " FROM FAT_TRANS_EXP_ALLOC a (nolock) " +
                                    " LEFT JOIN RFT_GET_GSB_CODE_INFO('RHAPSODY', @CCOMPANY_ID, '_DEPARTMENT', '', @CFOREIGN_LANGUAGE) y  " +
                                    " on y.CODE = a.CEXPENSE_DEPT_CODE " +
                                    " WHERE a.CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND a.CDEPT_CODE = @CDEPT_CODE " +
                                    " AND a.CTRANSACTION_CODE = @CTRANSACTION_CODE " +
                                    " AND a.CREFERENCE_NO = @CREFERENCE_NO " +
                                    " and a.CASSET_CODE = @CASSET_CODE " +
                                    " AND a.CASSET_TRANS_SEQNO = @CASSET_TRANS_SEQNO ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFOREIGN_LANGUAGE", DbType.String, 50, poParameter.CFOREIGN_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQNO", DbType.String, 50, poParameter.CASSET_TRANS_SEQNO);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Delete asset transaction
        /// </summary>
        /// <param name="poEntity">Entity with key fields to delete</param>
        protected override async Task R_DeletingAsync(FAT0010002DTO poEntity)
        {
            string lcMethod = nameof(R_DeletingAsync);
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
                loCmd.CommandText = "RSP_FAT00100_DELETE_TRANS_ASSET";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQ_NO", DbType.String, 50, poEntity.CTRANS_SEQ_NO);

                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $" {p.ParameterName} ='{p.Value}'")));

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }

        /// <summary>
        /// Display single record with asset details and totals
        /// </summary>
        /// <param name="poEntity">Entity with key fields</param>
        /// <returns>Complete entity with asset details and totals</returns>
        protected override async Task<FAT0010002DTO> R_DisplayAsync(FAT0010002DTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loRtn = new FAT0010002DTO();
            R_ReadParameter loReadParameter = null;
            R_ReadResult loReadResult = null;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FAT00100_GET_TRANS_ASSET ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQ_NO", DbType.String, 30, poEntity.CTRANS_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poEntity.CLANGUAGE_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    if (string.IsNullOrEmpty(loRtn.CSTORAGE_ID) == false)
                    {
                        loReadParameter = new R_ReadParameter()
                        {
                            StorageId = loRtn.CSTORAGE_ID
                        };

                        loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                        loRtn.OIMAGE = loReadResult.Data;
                        loRtn.CFILE_EXTENSION = loReadResult.FileExtension;
                        loRtn.CFILE_NAME = loReadResult.FileName;
                        //loResult.Data.CFILE_NAME_EXTENSION = loReadResult.FileName + loReadResult.FileExtension;
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
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loRtn;
        }


        /// <summary>
        /// Save asset transaction using stored procedure RSP_FAT00100_SAVE_TRANS_ASSET
        /// </summary>
        /// <param name="poNewEntity">Entity to save</param>
        /// <param name="peCRUDMode">CRUD mode (Add or Edit)</param>
        protected override async Task R_SavingAsync(FAT0010002DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            string lcMethod = nameof(R_SavingAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            string lcCmd = string.Empty;
            string lcQuery = string.Empty;

            FAT00100ImageStorageTypeDTO loStorageType = null;

            try
            {
                // Determine action based on CRUD mode
                string lcAction = peCRUDMode == eCRUDMode.AddMode ? "NEW" : "EDIT";
                // if NEW create new GIU
                if (lcAction == "NEW")
                {
                    poNewEntity.CREC_ID = Guid.NewGuid().ToString();
                }
                if (poNewEntity.OIMAGE != null)
                {
                    loStorageType = await GetStorageType();
                    if (loStorageType != null)
                    {
                        await SetStorageID(poNewEntity, loStorageType);
                    }
                }
                
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                // Map DTO properties to stored procedure parameters
                loCmd.Parameters.Clear();
                lcQuery = $"RSP_FAT00100_SAVE_TRANS_ASSET ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);                //@CCOMPANY_ID	varchar	8                         
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, poNewEntity.CUSER_ID);                      //@CUSER_ID	varchar	30
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);                                   //@CACTION	varchar	10
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);                        //@CREC_ID	varchar	50
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);              //@CDEPT_CODE	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);                  //@CREF_NO	varchar	30
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);            //@CREF_DATE	varchar	8
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poNewEntity.CASSET_CODE);            //@CASSET_CODE	varchar	50
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQ_NO", DbType.String, 6, poNewEntity.CTRANS_SEQNO);             //@CTRANS_SEQ_NO	char	6
                loDb.R_AddCommandParameter(loCmd, "@CASSET_NAME", DbType.String, 50, poNewEntity.CASSET_NAME);                //@CASSET_NAME	nvarchar	200
                loDb.R_AddCommandParameter(loCmd, "@CASSET_OWNER", DbType.String, 200, poNewEntity.CASSET_OWNER);                //@CASSET_OWNER	varchar	50
                loDb.R_AddCommandParameter(loCmd, "@CASSET_DEPT_CODE", DbType.String, 20, poNewEntity.CASSET_DEPT_CODE);  //@CASSET_DEPT_CODE	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 20, poNewEntity.CJRNGRP_CODE);          //@CJRNGRP_CODE	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_CODE", DbType.String, 20, poNewEntity.CCATEGORY_CODE);      //@CCATEGORY_CODE	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@CTAX_CATEGORY_CODE", DbType.String, 20, poNewEntity.CTAX_CATEGORY_CODE);  //@CTAX_CATEGORY_CODE	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@IQTY", DbType.Int32, 4, poNewEntity.IQTY);                                //@IQTY	int	4
                loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, 40, poNewEntity.CUNIT);                        //@CUNIT	nvarchar	40
                loDb.R_AddCommandParameter(loCmd, "@CSERIAL_NO", DbType.String, 50, poNewEntity.CSERIAL_NO);                  //@CSERIAL_NO	varchar	30
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);          //@CPROPERTY_ID	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@CLOCATION_ID", DbType.String, 20, poNewEntity.CLOCATION_ID);              //@CLOCATION_ID	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 200, poNewEntity.CTRANS_DESC);               //@CTRANS_DESC	nvarchar	200
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 50, poNewEntity.CSTORAGE_ID);                //@CSTORAGE_ID	varchar	50
                loDb.R_AddCommandParameter(loCmd, "@CINSERVICE_DATE", DbType.String, 8, poNewEntity.CINSERVICE_DATE);    //@CINSERVICE_DATE	varchar	8
                loDb.R_AddCommandParameter(loCmd, "@LNEW", DbType.Boolean, 1, poNewEntity.LNEW);                              //@LNEW	bit	1
                loDb.R_AddCommandParameter(loCmd, "@NINIT_COST", DbType.Decimal, 9, poNewEntity.NINIT_COST);                  //@NINIT_COST	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@NADDITION", DbType.Decimal, 9, poNewEntity.NADDITION);                    //@NADDITION	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@NDEDUCTION", DbType.Decimal, 9, poNewEntity.NDEDUCTION);                  //@NDEDUCTION	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@NPRIOR_DEPR", DbType.Decimal, 9, poNewEntity.NPRIOR_DEPR);                //@NPRIOR_DEPR	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@NYTD_DEPR", DbType.Decimal, 9, poNewEntity.NYTD_DEPR);                    //@NYTD_DEPR	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 20, poNewEntity.CDEPR_METHOD);          //@CDEPR_METHOD	varchar	20
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE);            //@CSTART_DATE	varchar	8
                loDb.R_AddCommandParameter(loCmd, "@NBOOK_VALUE", DbType.Decimal, 9, poNewEntity.NBOOK_VALUE);                //@NBOOK_VALUE	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@NBEG_BOOK_VALUE", DbType.Decimal, 9, poNewEntity.NBEG_BOOK_VALUE);        //@NBEG_BOOK_VALUE	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@NRESIDUAL_VALUE", DbType.Decimal, 9, poNewEntity.NRESIDUAL_VALUE);        //@NRESIDUAL_VALUE	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIFE_YY", DbType.Int32, 4, poNewEntity.IUSEFUL_LIFE_YY);          //@IUSEFUL_LIFE_YY	int	4
                loDb.R_AddCommandParameter(loCmd, "@IUSEFUL_LIFE_MM", DbType.Int32, 4, poNewEntity.IUSEFUL_LIFE_MM);          //@IUSEFUL_LIFE_MM	int	4
                loDb.R_AddCommandParameter(loCmd, "@IREMAINING_LIFE_YY", DbType.Int32, 4, poNewEntity.IREMAINING_LIFE_YY);    //@IREMAINING_LIFE_YY	int	4
                loDb.R_AddCommandParameter(loCmd, "@IREMAINING_LIFE_MM", DbType.Int32, 4, poNewEntity.IREMAINING_LIFE_MM);    //@IREMAINING_LIFE_MM	int	4
                loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR_PCT", DbType.Decimal, 5, poNewEntity.NYEAR_DEPR_PCT);      //@NYEAR_DEPR_PCT	numeric	5
                loDb.R_AddCommandParameter(loCmd, "@NYEAR_DEPR", DbType.Decimal, 9, poNewEntity.NYEAR_DEPR);                  //@NYEAR_DEPR	numeric	9
                loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 13, poNewEntity.NLBASE_RATE);                //@NLBASE_RATE	numeric	13
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NLCURRENCY_RATE);        //@NLCURRENCY_RATE	numeric	13
                loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 13, poNewEntity.NBBASE_RATE);                //@NBBASE_RATE	numeric	13
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NBCURRENCY_RATE);        //@NBCURRENCY_RATE	numeric	13

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $" {p.ParameterName} ='{p.Value}'")));
                // Execute stored procedure and get result
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT0010002SaveResultDTO>(loDataTable).FirstOrDefault();

                // Update entity with returned CREC_ID if available
                if (loRtn != null && !string.IsNullOrWhiteSpace(loRtn.CREC_ID))
                {
                    poNewEntity.CREC_ID= loRtn.CREC_ID;
                }

                _logger.LogInfo("Executed RSP_FAT00100_SAVE_TRANS_ASSET with action: {Action}", lcAction);
                _logger.LogDebug("Stored procedure executed successfully");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
        }


        /// <summary>
        /// Validate department code
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, dept code, and user ID</param>
        /// <returns>Result DTO with validation result (1 if valid, 0 if not)</returns>
        public async Task<FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>> ValidateDeptCodeAsync(FAT0010002ValidateDeptCodeParameterDTO poParameter)
        {
            string lcMethod = nameof(ValidateDeptCodeAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>
            {
                Data = new FAT0010002ValidateDeptCodeResultDTO { Result = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = " SELECT TOP 1 1 FROM GSX_DEPARTMENT_USER " +
                                    " WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                                    " AND CDEPT_CODE = @CDEPT_CODE " +
                                    " AND CUSER_ID = @CUSER_ID ";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100DTO>(loDataTable);

                // Preserve original VB.NET logic exactly (even if it appears to be a bug)
                var loRtnTemp = new FAT00100DTO();
                if (loRtnTemp != null)
                {
                    loResult.Data.Result = 1;
                }
                else
                {
                    loResult.Data.Result = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get declining depreciation amount
        /// </summary>
        /// <param name="poParameter">Parameter containing depreciation method, useful life years/months, and beginning book value</param>
        /// <returns>Result DTO with depreciation amount</returns>
        public async Task<FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>> GetDecliningDeprAmtAsync(FAT0010002GetDecliningDeprAmtParameterDTO poParameter)
        {
            string lcMethod = nameof(GetDecliningDeprAmtAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>
            {
                Data = new FAT0010002GetDecliningDeprAmtResultDTO { DeprAmt = 0 }
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = "DECLARE @DeprAmt numeric(18,2) " +
                                    "EXEC RSP_FA_GET_DECL_YEARLY_DEPR_AMT @CDEPR_METHOD ,@IBEG_UL_YR ,@IBEG_UL_MO ,@IREM_UL_YR ,@IREM_UL_MO ,@NBEG_BOOK_VAL ";

                loDb.R_AddCommandParameter(loCmd, "@CDEPR_METHOD", DbType.String, 50, poParameter.CDEPR_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@IBEG_UL_YR", DbType.Int16, 0, poParameter.IBEG_UL_YR);
                loDb.R_AddCommandParameter(loCmd, "@IBEG_UL_MO", DbType.Int16, 0, poParameter.IBEG_UL_MO);
                loDb.R_AddCommandParameter(loCmd, "@IREM_UL_YR", DbType.Int16, 0, poParameter.IREM_UL_YR);
                loDb.R_AddCommandParameter(loCmd, "@IREM_UL_MO", DbType.Int16, 0, poParameter.IREM_UL_MO);
                loDb.R_AddCommandParameter(loCmd, "@NBEG_BOOK_VAL", DbType.Decimal, 0, poParameter.NBEG_BOOK_VAL);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT0010002DTO>(loDataTable).FirstOrDefault();

                if (loRtn != null)
                {
                    loResult.Data.DeprAmt = loRtn.DeprAmt;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        /// <summary>
        /// Get transaction expense allocation list
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, parent ID, dept code, trans code, ref no, asset code, asset trans seq no, and language ID</param>
        /// <returns>List of transaction expense allocation result DTOs</returns>
        public async Task<List<FAT00100GetTransExpAllocListResultDTO>> FAT00100GetTransExpAllocList(FAT00100GetTransExpAllocListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetTransExpAllocList);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetTransExpAllocListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FA_GET_TRANS_EXP_ALLOC_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, 50, poParameter.CPARENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_TRANS_SEQ_NO", DbType.String, 6, poParameter.CASSET_TRANS_SEQ_NO ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT00100GetTransExpAllocListResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loResult;
        }

        private async Task<FAT00100ImageStorageTypeDTO> GetStorageType()
        {
            using Activity activity = _activitySource.StartActivity("GetStorageType");
            var loEx = new R_Exception();
            FAT00100ImageStorageTypeDTO loResult = null;
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
                    loResult = R_Utility.R_ConvertTo<FAT00100ImageStorageTypeDTO>(loDataTable).FirstOrDefault();
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

        private async Task<FAT0010002DTO> SetStorageID(FAT0010002DTO poNewEntity, FAT00100ImageStorageTypeDTO poStorageType)
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
                    FileName = poNewEntity.CPROPERTY_ID,
                    FileExtension = poNewEntity.CPROPERTY_ID,
                    UploadData = poNewEntity.OIMAGE,
                    UserId = poNewEntity.CUSER_ID,
                    BusinessKeyParameter = new R_BusinessKeyParameter()
                    {
                        CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                        CDATA_TYPE = "STORAGE_DATA_TABLE",
                        CKEY01 = poNewEntity.CREC_ID,
                    }
                };
                loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                //Set Storage ID CSTORAGE_ID
                poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
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
            return poNewEntity;
        }

    }
}


