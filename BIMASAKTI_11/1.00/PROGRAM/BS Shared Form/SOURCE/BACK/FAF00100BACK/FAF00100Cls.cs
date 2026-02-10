using FAF00100BACK.DTOs;
using FAF00100COMMON.DTOs;
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

namespace FAF00100BACK
{
    public class FAF00100Cls : R_BusinessObjectAsync<FAF00100GetAssetResultDTO>
    {
        private readonly LoggerFAF00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAF00100Cls()
        {
            _logger = LoggerFAF00100.R_GetInstanceLogger();
            _activitySource = FAF00100Activity.R_GetInstanceActivitySource();
        }
        protected override Task R_DeletingAsync(FAF00100GetAssetResultDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override async Task<FAF00100GetAssetResultDTO> R_DisplayAsync(FAF00100GetAssetResultDTO poEntity)
        {
            string lcMethod = nameof(R_DisplayAsync);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            FAF00100GetAssetResultDTO loRtn = new FAF00100GetAssetResultDTO();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_GET_ASSET ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poEntity.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poEntity.CLANGUAGE_ID);


                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAF00100GetAssetResultDTO>(loRtnDataTable).FirstOrDefault() ?? new FAF00100GetAssetResultDTO();

                if (!string.IsNullOrEmpty(loRtn.CSTORAGE_ID))
                {
                    var loReadParameter = new R_ReadParameter();
                    loReadParameter.StorageId = loRtn.CSTORAGE_ID;

                    var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                    loRtn.OASSET_IMAGE = loReadResult.Data;
                }

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);

            return loRtn;
        }

        protected override Task R_SavingAsync(FAF00100GetAssetResultDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FAF00100GetAssetAllocResultDTO>> GetListAssetAlloc(FAF00100GetAssetAllocParameterDTO poParam)
        {
            string lcMethod = nameof(GetListAssetAlloc);
            using Activity? loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            List<FAF00100GetAssetAllocResultDTO> loRtn = new List<FAF00100GetAssetAllocResultDTO>();
            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;

            try
            {
                loDb = new R_Db();
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.CommandText = "RSP_FA_GET_ASSET_EXP_ALLOC_LIST ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 20, poParam.CLANGUAGE_ID);

                DataTable loRtnDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAF00100GetAssetAllocResultDTO>(loRtnDataTable).ToList();

                _logger?.LogDebug("EXEC " + loCmd.CommandText +
                string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) { loDb = null; }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loRtn;
        }
    }
}
