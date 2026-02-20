using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using R_BackEnd;
using R_Common;
using FAT01100Back.DTOs;
using FAT01100BackResources;
using FAT01100Common;
using FAT01100Common.DTOs;

namespace FAT01100Back
{
    /// <summary>
    /// Business logic class for FAT01100 - Get Transaction List (RSP_FAT01100_GET_TRANS_LIST)
    /// </summary>
    public class FAT01100Cls 
    {
        private readonly FAT01100BackResources.Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT01100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT01100Cls()
        {
            _logger = LoggerFAT01100.R_GetInstanceLogger();
            _activitySource = FAT01100Activity.R_GetInstanceActivitySource();
        }

        /// <summary>
        /// Get transaction list via RSP_FAT01100_GET_TRANS_LIST
        /// </summary>
        /// <param name="poParameter">Parameter containing company, user, dept, period range, asset code, language</param>
        /// <returns>Result DTO containing list of transaction list result DTOs</returns>
        public async Task<FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>>> FAT01100GeTransList(FAT01100GeTransListParameterDTO poParameter)
        {
            string lcMethod = nameof(FAT01100GeTransList);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            var loResult = new List<FAT01100GeTransListResultDTO>();

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();
                loCmd.CommandText = "RSP_FAT01100_GET_TRANS_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParameter.CFROM_PERIOD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParameter.CTO_PERIOD ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 30, poParameter.CASSET_CODE ?? string.Empty);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID ?? string.Empty);

                _logger.LogDebug("EXEC " + loCmd.CommandText + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                var loRtn = R_Utility.R_ConvertTo<FAT01100GeTransListResultDTO>(loDataTable);
                if (loRtn != null && loRtn.Count > 0)
                    loResult = loRtn.ToList();
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
            return new FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>> { Data = loResult };
        }
    }
}
