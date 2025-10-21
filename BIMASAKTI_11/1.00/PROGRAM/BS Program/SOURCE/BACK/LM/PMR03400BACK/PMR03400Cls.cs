using PMR03400COMMON;
using PMR03400COMMON.DTO_s;
using PMR03400COMMON.DTOs;
using PMR03400COMMON.Print_DTO;
using R_BackEnd;
using R_Common;
using R_Storage;
using R_StorageCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMR03400BACK
{
    public class PMR03400Cls
    {
        private PMR03400Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR03400Cls()
        {
            _logger = PMR03400Logger.R_GetInstanceLogger();
            _activitySource = PMR03400Activity.R_GetInstanceActivitySource();
        }

        public List<PMR03400SPResultDTO>GetReportData(PMR03400SPParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR03400SPResultDTO> loRtn = new List<PMR03400SPResultDTO>();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_PMR03400_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFR_PERIOD", DbType.String, 6, poParam.CFR_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParam.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParam.CCURRENCY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CFR_CODE", DbType.String, 30, poParam.CFR_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CODE", DbType.String, 30, poParam.CTO_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LPROFORMA", DbType.Boolean, 1, poParam.LPROFORMA);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParam.CLANGUAGE_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR03400SPResultDTO>(loRtnTemp).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public PrintLogoResultDTO GetPropertyLogo(PMR03400ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            PrintLogoResultDTO loResult = null;
            try
            {
                R_Db loDb = new();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_DETAIL";
                loCmd = loDb.GetCommand();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);
                _logger.LogDebug("EXEC {lcQuery} {@Parameters}", lcQuery, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();

                if (string.IsNullOrEmpty(loResult.CSTORAGE_ID) == false)
                {
                    var loReadParameter = new R_ReadParameter()
                    {
                        StorageId = loResult.CSTORAGE_ID
                    };

                    var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                    loResult.CLOGO = loReadResult.Data;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public PrintLogoResultDTO GetCompanyName(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);

            var loEx = new R_Exception();
            PrintLogoResultDTO loResult = null;
            try
            {
                R_Db loDb = new();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();
                var lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{pcCompanyId}'";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                //loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #region log method helper

        private void ShowLogDebug(string pcQUery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} : '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQUery} {paramValues}");
        }

        private void ShowLogError(Exception poException)
        {
            _logger.LogError(poException);
        }

        #endregion
    }
}
