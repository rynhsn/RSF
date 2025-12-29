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

namespace FAT00100Back
{
    /// <summary>
    /// Business logic class for FAT00100 - Asset List operations
    /// Handles asset list retrieval operations
    /// </summary>
    public class FAT00100AssetListCls
    {
        private readonly FAT00100BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00100AssetListCls()
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
        /// Get transaction asset list
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, record ID, dept code, ref no, and language ID</param>
        /// <returns>List of transaction asset list result DTOs</returns>
        public async Task<List<FAT00100GetTransAssetListResultDTO>> FAT00100GetTransAssetList(FAT00100GetTransAssetListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetTransAssetList);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00100GetTransAssetListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FAT00100_GET_TRANS_ASSET_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT00100GetTransAssetListResultDTO>(loDataTable).ToList();
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
        /// Get transaction asset
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, record ID, dept code, ref no, trans seq no, and language ID</param>
        /// <returns>Transaction asset result DTO</returns>
        public async Task<FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>> FAT00100GetTransAssetAsync(FAT00100GetTransAssetParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00100GetTransAssetAsync);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>
            {
                Data = new FAT00100GetTransAssetResultDTO()
            };

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                var lcQuery = "RSP_FAT00100_GET_TRANS_ASSET";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_SEQ_NO", DbType.String, 30, poParameter.CTRANS_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00100GetTransAssetResultDTO>(loDataTable).FirstOrDefault();

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

        
    }
}

