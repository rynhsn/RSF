using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using FAT01100Common.DTOs;
using FAT01100Back.DTOs;
using System.Transactions;
using System.Diagnostics;
using System.Text;
using R_OpenTelemetry;
using System.Data.SqlClient;

namespace FAT01100Back
{
    public class FAT01100ExpenseAllocationCls
    {
        private readonly LoggerFAT01100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT01100ExpenseAllocationCls()
        {
            _logger = LoggerFAT01100.R_GetInstanceLogger();
            _activitySource = FAT01100Activity.R_GetInstanceActivitySource();
        }

        

        //CATEGORY: other-function
        public async Task<List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>> RSP_FA_GET_ASSET_EXP_ALLOC_LIST(FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTParameterDTO poParam)
        {
            string lcFunction = nameof(RSP_FA_GET_ASSET_EXP_ALLOC_LIST);
            using var activity = _activitySource.StartActivity(lcFunction);
            _logger.LogInfo("START function {RSP_FA_GET_ASSET_EXP_ALLOC_LIST}", lcFunction);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>? loResult = null;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "RSP_FA_GET_ASSET_EXP_ALLOC_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "CASSET_CODE", DbType.String, 50, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "CLANGUAGE_ID", DbType.String, 10, poParam.CLANGUAGE_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END function {RSP_FA_GET_ASSET_EXP_ALLOC_LIST}", lcFunction);
            return loResult ?? new List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>();
        }


        //CATEGORY: other-function
        public async Task<List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>> RSP_FA_GET_TRANS_EXP_ALLOC_LIST(FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTParameterDTO poParam)
        {
            string lcFunction = nameof(RSP_FA_GET_TRANS_EXP_ALLOC_LIST);
            using var activity = _activitySource.StartActivity(lcFunction);
            _logger.LogInfo("START function {RSP_FA_GET_TRANS_EXP_ALLOC_LIST}", lcFunction);

            var loEx = new R_Exception();
            var loDb = new R_Db();
            List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>? loResult = null;

            try
            {
                using DbConnection loConn = await loDb.GetConnectionAsync();
                using DbCommand loCmd = loDb.GetCommand();
                loCmd.Parameters.Clear();

                loCmd.CommandText = "RSP_FA_GET_TRANS_EXP_ALLOC_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "CPARENT_ID", DbType.String, 50, poParam.CPARENT_ID);
                loDb.R_AddCommandParameter(loCmd, "CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "CREF_NO", DbType.String, 30, poParam.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "CASSET_CODE", DbType.String, 20, poParam.CASSET_CODE);
                loDb.R_AddCommandParameter(loCmd, "CASSET_TRANS_SEQ_NO", DbType.String, 6, poParam.CASSET_TRANS_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);

                var loDbParams = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParams);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null) loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END function {RSP_FA_GET_TRANS_EXP_ALLOC_LIST}", lcFunction);
            return loResult ?? new List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>();
        }



    }
}
