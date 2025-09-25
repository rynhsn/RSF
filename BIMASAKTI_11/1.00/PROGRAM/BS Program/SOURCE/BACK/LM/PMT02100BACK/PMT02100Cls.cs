using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.Loggers;
using PMT02100BACK.OpenTelemetry;
using System.Reflection.Metadata;

namespace PMT02100BACK
{
    public class PMT02100Cls
    {
        private LoggerPMT02100 _logger;
        private readonly ActivitySource _activitySource;

        public PMT02100Cls()
        {
            _logger = LoggerPMT02100.R_GetInstanceLogger();
            _activitySource = PMT02100ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<GetPropertyListDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            var loEx = new R_Exception();
            List<GetPropertyListDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPropertyListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetPMSystemParamDTO GetPMSystemParam(GetPMSystemParamParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetPMSystemParam");
            R_Exception loException = new R_Exception();
            GetPMSystemParamDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GET_SYSTEM_PARAMETER_DETAIL @CLOGIN_COMPANY_ID, @CPROPERTY_ID, @CUSER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_SYSTEM_PARAMETER_DETAIL {@Parameters} || GetPMSystemParam(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPMSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<PMT02100HandoverBuildingDTO> GetHandoverBuildingList(PMT02100HandoverBuildingParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetHandoverBuildingList");
            var loEx = new R_Exception();
            List<PMT02100HandoverBuildingDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_HANDOVER_BUILDING_LIST ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTYPE", DbType.String, 50, poParameter.CTYPE);
                loDb.R_AddCommandParameter(loCmd, "@CHANDOVER_STATUS", DbType.String, 50, poParameter.CHANDOVER_STATUS);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 50, poParameter.CFROM_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 50, poParameter.CTO_DATE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("EXEC RSP_PM_GET_HANDOVER_BUILDING_LIST  {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02100HandoverBuildingDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<PMT02100HandoverDTO> GetHandoverList(PMT02100HandoverParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetHandoverList");
            var loEx = new R_Exception();
            List<PMT02100HandoverDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_HANDOVER_SCHEDULE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTYPE", DbType.String, 50, poParameter.CTYPE);
                loDb.R_AddCommandParameter(loCmd, "@CHANDOVER_STATUS", DbType.String, 50, poParameter.CHANDOVER_STATUS);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 50, poParameter.CFROM_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 50, poParameter.CTO_DATE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("EXEC RSP_PM_GET_HANDOVER_SCHEDULE_LIST {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02100HandoverDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
