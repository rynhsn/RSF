using PMT01400COMMON.DTOs.Helper;
using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON.DTOs;
using PMT02600COMMON.DTOs.PMT02620;
using PMT02600COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02600BACK
{
    public class PMT02620ChargesCls : R_BusinessObject<PMT02620ChargesDTO>
    {
        RSP_PM_MAINTAIN_AGREEMENT_CHARGESResources.Resources_Dummy_Class _loRspMaintainCharges = new RSP_PM_MAINTAIN_AGREEMENT_CHARGESResources.Resources_Dummy_Class();

        private LoggerPMT02620Charges _Logger;
        private readonly ActivitySource _activitySource;

        public PMT02620ChargesCls()
        {
            _Logger = LoggerPMT02620Charges.R_GetInstanceLogger();
            _activitySource = PMT02620ChargesActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMT02620AgreementChargeCalUnitDTO> GetAllAgreementChargesCallUnit(PMT02620ParameterAgreementChargeCalUnitDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementChargesCallUnit");
            var loEx = new R_Exception();
            List<PMT02620AgreementChargeCalUnitDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_CAL_UNIT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 50, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_CHARGES_CAL_UNIT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02620AgreementChargeCalUnitDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<CodeDescDTO> GetFeeMethod()
        {
            using Activity activity = _activitySource.StartActivity("GetFeeMethod");
            var loEx = new R_Exception();
            List<CodeDescDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', @CCOMPANY_ID , @CPARAMETER, '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPARAMETER", DbType.String, 50, "_BS_FEE_METHOD");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                string loCompanyIdLog = null;
                string loUserLanLog = null;
                string loParameterLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CPARAMETER":
                            loParameterLog = (string)x.Value;
                            break;
                        case "@CUSER_LANGUAGE":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "{1}, '' , {2})", loCompanyIdLog, loParameterLog, loUserLanLog);
                _Logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CodeDescDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMT02620ChargesDTO> GetAllAgreementCharges(PMT02620ChargesDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllAgreementCharges");
            var loEx = new R_Exception();
            List<PMT02620ChargesDTO> loResult = null;

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
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_CHARGES_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT02620ChargesDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override PMT02620ChargesDTO R_Display(PMT02620ChargesDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMT02620ChargesDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_DT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 100, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 50, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_CHARGES_DT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT02620ChargesDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Saving(PMT02620ChargesDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                    poNewEntity.CSEQ_NO = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                //Save Bulk List Data
                _Logger.LogInfo("Start Save Bulk #CHARGES_CAL_UNIT");
                lcQuery = @"CREATE TABLE #CHARGES_CAL_UNIT  (
                            CUNIT_ID VARCHAR(20), 
                            CFLOOR_ID VARCHAR(20), 
                            CBUILDING_ID VARCHAR(20), 
                            NTOTAL_AREA NUMERIC(6,2), 
                            NFEE_PER_AREA NUMERIC(16,2), 
                        );";
                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<PMT02620AgreementChargeCalUnitDTO>((SqlConnection)loConn, "#CHARGES_CAL_UNIT", poNewEntity.CHARGE_CALL_UNIT_LIST);
                _Logger.LogInfo("End Save Bulk #CHARGES_CAL_UNIT");

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO);

                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_MODE", DbType.String, 2, "01");
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, "");

                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 20, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 20, poNewEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IYEAR", DbType.Int32, 50, poNewEntity.IYEARS);
                loDb.R_AddCommandParameter(loCmd, "@IMONTH", DbType.Int32, 50, poNewEntity.IMONTHS);
                loDb.R_AddCommandParameter(loCmd, "@IDAY", DbType.Int32, 50, poNewEntity.IDAYS);

                loDb.R_AddCommandParameter(loCmd, "@CEND_DATE", DbType.String, 8, poNewEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_MODE", DbType.String, 2, poNewEntity.CBILLING_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CFEE_METHOD", DbType.String, 2, poNewEntity.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@NFEE_AMT", DbType.Decimal, 100, poNewEntity.NFEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_PERIOD", DbType.String, 2, poNewEntity.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@NINVOICE_AMT", DbType.Decimal, 100, poNewEntity.NINVOICE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LCAL_UNIT", DbType.Boolean, 10, poNewEntity.LCAL_UNIT);

                loDb.R_AddCommandParameter(loCmd, "@LPRORATE", DbType.Boolean, 10, poNewEntity.LPRORATE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_CHARGES {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<PMT02620ChargesDTO>(loDataTable).FirstOrDefault();

                    if (loTempResult != null)
                    {
                        poNewEntity.CSEQ_NO = loTempResult.CSEQ_NO;
                    }
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }
        protected override void R_Deleting(PMT02620ChargesDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ConstantVariable.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);

                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_MODE", DbType.String, 2,ConstantVariable.VAR_CHARGE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 20, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 20, poEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IYEAR", DbType.Int32, 50, poEntity.IYEARS);
                loDb.R_AddCommandParameter(loCmd, "@IMONTH", DbType.Int32, 50, poEntity.IMONTHS);

                loDb.R_AddCommandParameter(loCmd, "@CEND_DATE", DbType.String, 8, poEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_MODE", DbType.String, 2, poEntity.CBILLING_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CFEE_METHOD", DbType.String, 2, poEntity.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@NFEE_AMT", DbType.Decimal, 100, poEntity.NFEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_PERIOD", DbType.String, 2, poEntity.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@NINVOICE_AMT", DbType.Decimal, 100, poEntity.NINVOICE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LCAL_UNIT", DbType.Boolean, 10, poEntity.LCAL_UNIT);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).ToDictionary(x => x.ParameterName, x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_CHARGES {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}