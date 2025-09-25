using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs.GSM02580;
using GSM02500COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500BACK
{
    public class GSM02580Cls : R_BusinessObjectAsync<GSM02580ParameterDTO>
    {
        RSP_GS_MAINTAIN_PROPERTY_DEPTResources.Resources_Dummy_Class _loRsp = new RSP_GS_MAINTAIN_PROPERTY_DEPTResources.Resources_Dummy_Class();

        private LoggerGSM02580 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02580Cls()
        {
            _logger = LoggerGSM02580.R_GetInstanceLogger();
            _activitySource = GSM02580ActivitySourceBase.R_GetInstanceActivitySource();
        }

        private async Task RSP_GS_MAINTAIN_PROPERTY_OP_HOURSMethod(GSM02580ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_PROPERTY_OP_HOURSMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();
                lcQuery = $"EXEC RSP_GS_MAINTAIN_PROPERTY_OP_HOURS " +
                          $"@CCOMPANY_ID, " +
                          $"@CPROPERTY_ID, " +
                          $"@LMONDAY, " +
                          $"@CMON_OPEN_HOUR, " +
                          $"@CMON_CLOSE_HOUR, " +
                          $"@LTUESDAY, " +
                          $"@CTUE_OPEN_HOUR, " +
                          $"@CTUE_CLOSE_HOUR, " +
                          $"@LWEDNESDAY, " +
                          $"@CWED_OPEN_HOUR, " +
                          $"@CWED_CLOSE_HOUR, " +
                          $"@LTHURSDAY, " +
                          $"@CTHU_OPEN_HOUR, " +
                          $"@CTHU_CLOSE_HOUR, " +
                          $"@LFRIDAY, " +
                          $"@CFRI_OPEN_HOUR, " +
                          $"@CFRI_CLOSE_HOUR, " +
                          $"@LSATURDAY, " +
                          $"@CSAT_OPEN_HOUR, " +
                          $"@CSAT_CLOSE_HOUR, " +
                          $"@LSUNDAY, " +
                          $"@CSUN_OPEN_HOUR, " +
                          $"@CSUN_CLOSE_HOUR, " +
                          $"@LSHOW_IN_MOBILE, " +
                          $"@CNOTES, " +
                          $"@CACTION, " +
                          $"@CUSER_ID";


                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);

                loDb.R_AddCommandParameter(loCmd, "@LMONDAY", DbType.Boolean, 1, poEntity.Data.LMONDAY);
                loDb.R_AddCommandParameter(loCmd, "@CMON_OPEN_HOUR", DbType.String, 50, poEntity.Data.CMONDAY_OPEN_H + ":" + poEntity.Data.CMONDAY_OPEN_M);
                loDb.R_AddCommandParameter(loCmd, "@CMON_CLOSE_HOUR", DbType.String, 50, poEntity.Data.CMONDAY_CLOSE_H + ":" + poEntity.Data.CMONDAY_CLOSE_M);

                loDb.R_AddCommandParameter(loCmd, "@LTUESDAY", DbType.Boolean, 1, poEntity.Data.LTUESDAY);
                loDb.R_AddCommandParameter(loCmd, "@CTUE_OPEN_HOUR", DbType.String, 50, poEntity.Data.CTUESDAY_OPEN_H + ":" + poEntity.Data.CTUESDAY_OPEN_M);
                loDb.R_AddCommandParameter(loCmd, "@CTUE_CLOSE_HOUR", DbType.String, 50, poEntity.Data.CTUESDAY_CLOSE_H + ":" + poEntity.Data.CTUESDAY_CLOSE_M);

                loDb.R_AddCommandParameter(loCmd, "@LWEDNESDAY", DbType.Boolean, 1, poEntity.Data.LWEDNESDAY);
                loDb.R_AddCommandParameter(loCmd, "@CWED_OPEN_HOUR", DbType.String, 50, poEntity.Data.CWEDNESDAY_OPEN_H + ":" + poEntity.Data.CWEDNESDAY_OPEN_M);
                loDb.R_AddCommandParameter(loCmd, "@CWED_CLOSE_HOUR", DbType.String, 50, poEntity.Data.CWEDNESDAY_CLOSE_H + ":" + poEntity.Data.CWEDNESDAY_CLOSE_M);

                loDb.R_AddCommandParameter(loCmd, "@LTHURSDAY", DbType.Boolean, 1, poEntity.Data.LTHURSDAY);
                loDb.R_AddCommandParameter(loCmd, "@CTHU_OPEN_HOUR", DbType.String, 50, poEntity.Data.CTHURSDAY_OPEN_H + ":" + poEntity.Data.CTHURSDAY_OPEN_M);
                loDb.R_AddCommandParameter(loCmd, "@CTHU_CLOSE_HOUR", DbType.String, 50, poEntity.Data.CTHURSDAY_CLOSE_H + ":" + poEntity.Data.CTHURSDAY_CLOSE_M);

                loDb.R_AddCommandParameter(loCmd, "@LFRIDAY", DbType.Boolean, 1, poEntity.Data.LFRIDAY);
                loDb.R_AddCommandParameter(loCmd, "@CFRI_OPEN_HOUR", DbType.String, 50, poEntity.Data.CFRIDAY_OPEN_H + ":" + poEntity.Data.CFRIDAY_OPEN_M);
                loDb.R_AddCommandParameter(loCmd, "@CFRI_CLOSE_HOUR", DbType.String, 50, poEntity.Data.CFRIDAY_CLOSE_H + ":" + poEntity.Data.CFRIDAY_CLOSE_M);

                loDb.R_AddCommandParameter(loCmd, "@LSATURDAY", DbType.Boolean, 1, poEntity.Data.LSATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@CSAT_OPEN_HOUR", DbType.String, 50, poEntity.Data.CSATURDAY_OPEN_H + ":" + poEntity.Data.CSATURDAY_OPEN_M);
                loDb.R_AddCommandParameter(loCmd, "@CSAT_CLOSE_HOUR", DbType.String, 50, poEntity.Data.CSATURDAY_CLOSE_H + ":" + poEntity.Data.CSATURDAY_CLOSE_M);

                loDb.R_AddCommandParameter(loCmd, "@LSUNDAY", DbType.Boolean, 1, poEntity.Data.LSUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@CSUN_OPEN_HOUR", DbType.String, 50, poEntity.Data.CSUNDAY_OPEN_H + ":" + poEntity.Data.CSUNDAY_OPEN_M);
                loDb.R_AddCommandParameter(loCmd, "@CSUN_CLOSE_HOUR", DbType.String, 50, poEntity.Data.CSUNDAY_CLOSE_H + ":" + poEntity.Data.CSUNDAY_CLOSE_M);

                loDb.R_AddCommandParameter(loCmd, "@LSHOW_IN_MOBILE", DbType.Boolean, 1, poEntity.Data.LSHOW_IN_MOBILE);
                loDb.R_AddCommandParameter(loCmd, "@CNOTES", DbType.String, int.MaxValue, poEntity.Data.CNOTES);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, R_BackGlobalVar.USER_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_PROPERTY_OP_HOURS {@Parameters} || RSP_GS_MAINTAIN_PROPERTY_OP_HOURSMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
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

        protected override async Task<GSM02580ParameterDTO> R_DisplayAsync(GSM02580ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02580ParameterDTO loResult = new GSM02580ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_OP_HOURS " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_OP_HOURS {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02580DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override async Task R_SavingAsync(GSM02580ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                await RSP_GS_MAINTAIN_PROPERTY_OP_HOURSMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override async Task R_DeletingAsync(GSM02580ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                 await RSP_GS_MAINTAIN_PROPERTY_OP_HOURSMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
