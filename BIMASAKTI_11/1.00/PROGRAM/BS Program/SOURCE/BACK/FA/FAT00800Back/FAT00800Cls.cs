using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using R_BackEnd;
using R_Common;
using FAT00800Back.DTOs;
using FAT00800Common.DTOs;

namespace FAT00800Back
{
    /// <summary>
    /// List operations class for FAT00800 - Get Transaction List
    /// </summary>
    public class FAT00800Cls
    {
        private readonly LoggerFAT00800 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00800Cls()
        {
            _logger = LoggerFAT00800.R_GetInstanceLogger();
            _activitySource = FAT00800Activity.R_GetInstanceActivitySource();
        }

        /// <summary>
        /// Get transaction list via stored procedure RSP_FAT00800_GET_TRANS_LIST
        /// </summary>
        /// <param name="poParameter">Parameter containing company ID, user ID, department code, period range, asset code, and language ID</param>
        /// <returns>List of transaction list result DTOs</returns>
        public async Task<List<FAT00800GetTransListResultDTO>> FAT00800GetTransListAsync(FAT00800GetTransListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT00800GetTransListAsync);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT00800GetTransListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_FAT00800_GET_TRANS_LIST ";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParameter.CFROM_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParameter.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 30, poParameter.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x?.ParameterName != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName ?? string.Empty, x => x.Value);

                _logger.LogDebug("EXEC {Query} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT00800GetTransListResultDTO>(loDataTable);

                if (loRtn != null && loRtn.Count > 0)
                {
                    loResult = loRtn.ToList();
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
