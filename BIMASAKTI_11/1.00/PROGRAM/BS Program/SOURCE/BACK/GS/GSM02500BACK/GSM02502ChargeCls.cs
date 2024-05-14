using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Charge;
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
using GSM02500COMMON.Loggers;
using System.Diagnostics;
using GSM02500BACK.OpenTelemetry;

namespace GSM02500BACK
{
    public class GSM02502ChargeCls : R_BusinessObject<GSM02502ChargeParameterDTO>
    {
        private LoggerGSM02502Charge _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502ChargeCls()
        {
            _logger = LoggerGSM02502Charge.R_GetInstanceLogger();
            _activitySource = GSM02502ChargeActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<GSM02502ChargeDTO> GetChargeList(GetChargeListDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetChargeList");
            R_Exception loException = new R_Exception();
            List<GSM02502ChargeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_CHARGES_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_CHARGES_LIST {@Parameters} || GetChargeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02502ChargeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM02502ChargeComboboxDTO> GetChargeComboBoxList(GSM02502ChargeComboboxParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetChargeComboBoxList");
            R_Exception loException = new R_Exception();
            List<GSM02502ChargeComboboxDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"SELECT CCODE, CDESCRIPTION " +
                    $"FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_CLASS_ID, " +
                    $"'', " +
                    $"@CLOGIN_LANGUAGE_ID)";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_CLASS_ID", DbType.String, 50, poEntity.CSELECTED_CLASS_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                string lcCompanyIdLog = "";
                string lcUserLanLog = "";
                string lcSelectedClassId = "";
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CLOGIN_COMPANY_ID":
                            lcCompanyIdLog = (string)x.Value;
                            break;
                        case "@CSELECTED_CLASS_ID":
                            lcSelectedClassId = (string)x.Value;
                            break;
                        case "@CLOGIN_LANGUAGE_ID":
                            lcUserLanLog = (string)x.Value;
                            break;
                    }
                }); 
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                                       "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0}, {1}, '', {2}) || GetChargeComboBoxList(Cls)",
                                       lcCompanyIdLog, lcSelectedClassId, lcUserLanLog);

                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02502ChargeComboboxDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        private void RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_CHARGEMethod(GSM02502ChargeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_CHARGEMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_CHARGE " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                                 $"@CSEQ, " +
                                 $"@CCHARGES_TYPE_ID, " +
                                 $"@CCHARGES_ID, " +
                                 $"@NFEE, " +
                                 $"@CDESCRIPTION, " +
                                 $"@CFEE_METHOD, " +
                                 $"@CINVOICE_PERIOD, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ", DbType.String, 50, poEntity.Data.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE_ID", DbType.String, 50, poEntity.Data.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.Data.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@NFEE", DbType.Decimal, 10, poEntity.Data.NFEE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.Data.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CFEE_METHOD", DbType.String, 50, poEntity.Data.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_PERIOD", DbType.String, 50, poEntity.Data.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_CHARGE {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_CHARGEMethod(Cls) ", loDbParam);


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


        protected override void R_Deleting(GSM02502ChargeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_CHARGEMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override GSM02502ChargeParameterDTO R_Display(GSM02502ChargeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02502ChargeParameterDTO loResult = new GSM02502ChargeParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_CHARGES_DT " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                    $"@CCHARGES_TYPE_ID, " +
                    $"@CCHARGES_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE_ID", DbType.String, 50, poEntity.Data.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.Data.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_CHARGES_DT {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02502ChargeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02502ChargeParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_CHARGEMethod(poNewEntity);
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
