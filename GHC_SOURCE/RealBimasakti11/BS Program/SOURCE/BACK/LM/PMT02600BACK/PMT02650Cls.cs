using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON.DTOs.PMT02650;
using PMT02600COMMON.DTOs;
using PMT02600COMMON.Loggers;
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

namespace PMT02600BACK
{
    public class PMT02650Cls
    {
        private LoggerPMT02650 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT02650Cls()
        {
            _Logger = LoggerPMT02650.R_GetInstanceLogger();
            _activitySource = PMT02650ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMT02650ChargeDTO> GetAllAgreementCharge(PMT02650ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementCharge");
            var loEx = new R_Exception();
            List<PMT02650ChargeDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_MODE", DbType.String, 50, "01");
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_CHARGES_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02650ChargeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<PMT02650DTO> GetAllAgreementInvoicePlan(PMT02650ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementInvoicePlan");
            var loEx = new R_Exception();
            List<PMT02650DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_INV_PLAN_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CLOI_REC_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CAGRMT_REC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_DOC_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02650DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
