using APT00200COMMON.DTOs.APT00230;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00200COMMON.DTOs.APT00221;
using System.Reflection.Metadata;
using APT00200COMMON.Loggers;
using APT00200BACK.OpenTelemetry;
using System.Diagnostics;

namespace APT00200BACK
{
    public class APT00230Cls : R_BusinessObject<APT00230ParameterDTO>
    {
        RSP_AP_SAVE_TRANS_HDResources.Resources_Dummy_Class _loRspSaveTransHd = new RSP_AP_SAVE_TRANS_HDResources.Resources_Dummy_Class();

        private LoggerAPT00230 _logger;
        private readonly ActivitySource _activitySource;
        public APT00230Cls()
        {
            _logger = LoggerAPT00230.R_GetInstanceLogger();
            _activitySource = APT00230ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public APT00230DTO GetSummaryInfo(GetSummaryParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetSummaryInfo");
            R_Exception loException = new R_Exception();
            APT00230DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GET_TRANS_HD " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_TRANS_HD {@Parameters} || GetSummaryInfo(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00230DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Deleting(APT00230ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override APT00230ParameterDTO R_Display(APT00230ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            APT00230ParameterDTO loResult = null;
            string lcQuery;

            try
            {
                loResult = new APT00230ParameterDTO()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CLANGUAGE_ID = poEntity.CLANGUAGE_ID,
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CLOGIN_USER_ID = poEntity.CLOGIN_USER_ID
                };
                loResult.Data = GetSummaryInfo(new GetSummaryParameterDTO()
                {
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CREC_ID = poEntity.Data.CREC_ID,
                    CLANGUAGE_ID = poEntity.CLANGUAGE_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Saving(APT00230ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_AP_SAVE_TRANS_HD_SUMARRY " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CREC_ID, " +
                    "@LSUMMARY_DISCOUNT, " +
                    "@CSUMMARY_DISC_TYPE, " +
                    "@NSUMMARY_DISC_PCT, " +
                    "@NSUMMARY_DISCOUNT, " +
                    "@NADD_ON";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@LSUMMARY_DISCOUNT", DbType.Boolean, 50, poNewEntity.Data.LSUMMARY_DISCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CSUMMARY_DISC_TYPE", DbType.String, 50, poNewEntity.Data.CSUMMARY_DISC_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NSUMMARY_DISC_PCT", DbType.Decimal, 50, poNewEntity.Data.NSUMMARY_DISC_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NSUMMARY_DISCOUNT", DbType.Decimal, 50, poNewEntity.Data.NSUMMARY_DISCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NADD_ON", DbType.Decimal, 50, poNewEntity.Data.NADD_ON);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_SAVE_TRANS_HD_SUMARRY {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
