using PMT02600COMMON.DTOs.PMT02610;
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
using PMT02600COMMON.Loggers;
using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON.DTOs;
using System.Windows.Input;

namespace PMT02600BACK
{
    public class PMT02610Cls : R_BusinessObject<PMT02610ParameterDTO>
    {
        RSP_PM_MAINTAIN_AGREEMENTResources.Resources_Dummy_Class _loRspMaintainAgreement = new RSP_PM_MAINTAIN_AGREEMENTResources.Resources_Dummy_Class();

        private LoggerPMT02610 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT02610Cls()
        {
            _Logger = LoggerPMT02610.R_GetInstanceLogger();
            _activitySource = PMT02610ActivitySourceBase.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(PMT02610ParameterDTO poEntity)
        {
            string lcMethodName = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _Logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string? lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string? lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcAction = "DELETE";

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, ConstantVariable.VAR_TRANS_CODE);

                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.Data.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, poEntity.Data.CREF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.Data.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, string.IsNullOrWhiteSpace(poEntity.Data.CDOC_NO) ? "" : poEntity.Data.CDOC_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 8, string.IsNullOrWhiteSpace(poEntity.Data.CDOC_DATE) ? "" : poEntity.Data.CDOC_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poEntity.Data.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poEntity.Data.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, 50, poEntity.Data.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTHS", DbType.Int32, 50, poEntity.Data.IMONTHS);
                loDb.R_AddCommandParameter(loCommand, "@IYEARS", DbType.Int32, 50, poEntity.Data.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poEntity.Data.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poEntity.Data.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 255, poEntity.Data.CUNIT_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poEntity.Data.CNOTES);

                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poEntity.Data.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, ConstantVariable.VAR_LEASE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, ConstantVariable.VAR_CHARGE_MODE);

                loDb.R_AddCommandParameter(loCommand, "@CFOLLOW_UP_DATE", DbType.String, 8, string.IsNullOrWhiteSpace(poEntity.Data.CFOLLOW_UP_DATE) ? "" : poEntity.Data.CFOLLOW_UP_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CHO_PLAN_DATE", DbType.String, 8, string.IsNullOrWhiteSpace(poEntity.Data.CHO_PLAN_DATE) ? "" : poEntity.Data.CHO_PLAN_DATE);
                loDb.R_AddCommandParameter(loCommand, "@LWITH_FO", DbType.Boolean, 20, poEntity.Data.LWITH_FO);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_TYPE", DbType.String, 20, string.IsNullOrWhiteSpace(poEntity.Data.CBILLING_RULE_TYPE) ? "" : poEntity.Data.CBILLING_RULE_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 20, poEntity.Data.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NBOOKING_FEE", DbType.Decimal, 100, poEntity.Data.NBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CTC_CODE", DbType.String, 20, poEntity.Data.CTC_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_TRANS_CODE", DbType.String, 30, poEntity.Data.CLINK_TRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_REF_NO", DbType.String, 30, poEntity.Data.CLINK_REF_NO);

                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                /*
                loDb.R_AddCommandParameter(loCommand, "@CORIGINAL_REF_NO", DbType.String, 30, poEntity.CORIGINAL_REF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CFOLLOW_UP_DATE", DbType.String, 30, poEntity.CFOLLOW_UP_DATE);
                loDb.R_AddCommandParameter(loCommand, "@LWITH_FO ", DbType.Boolean, 30, poEntity.LWITH_FO );
                loDb.R_AddCommandParameter(loCommand, "@CHAND_OVER_DATE", DbType.String, 20, poEntity.CHAND_OVER_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NBOOKING_FEE", DbType.Decimal, 30, poEntity.NBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CTC_CODE", DbType.String, 32, poEntity.CTC_CODE);
                */
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _Logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _Logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _Logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        protected override PMT02610ParameterDTO R_Display(PMT02610ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetLOIDisplay");
            var loEx = new R_Exception();
            PMT02610ParameterDTO loResult = new PMT02610ParameterDTO();

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_AGREEMENT_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.Data.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.Data.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.Data.CTRANS_CODE) ? ConstantVariable.VAR_TRANS_CODE : poEntity.Data.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.Data.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_DETAIL {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult.Data = R_Utility.R_ConvertTo<PMT02610DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(PMT02610ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.Data.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ConstantVariable.VAR_TRANS_CODE);

                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.Data.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.Data.CREF_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poNewEntity.Data.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 30, string.IsNullOrWhiteSpace(poNewEntity.Data.CDOC_NO) ? "" : poNewEntity.Data.CDOC_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, string.IsNullOrWhiteSpace(poNewEntity.Data.CDOC_DATE) ? "" : poNewEntity.Data.CDOC_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poNewEntity.Data.CSTART_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CEND_DATE", DbType.String, 8, poNewEntity.Data.CEND_DATE);
                loDB.R_AddCommandParameter(loCmd, "@IDAYS", DbType.Int32, 50, poNewEntity.Data.IDAYS);
                loDB.R_AddCommandParameter(loCmd, "@IMONTHS", DbType.Int32, 50, poNewEntity.Data.IMONTHS);
                loDB.R_AddCommandParameter(loCmd, "@IYEARS", DbType.Int32, 50, poNewEntity.Data.IYEARS);
                loDB.R_AddCommandParameter(loCmd, "@CSALESMAN_ID", DbType.String, 8, poNewEntity.Data.CSALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poNewEntity.Data.CTENANT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_DESCRIPTION", DbType.String, 255, poNewEntity.Data.CUNIT_DESCRIPTION);
                loDB.R_AddCommandParameter(loCmd, "@CNOTES", DbType.String, int.MaxValue, poNewEntity.Data.CNOTES);

                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.Data.CCURRENCY_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLEASE_MODE", DbType.String, 2, ConstantVariable.VAR_LEASE_MODE);
                loDB.R_AddCommandParameter(loCmd, "@CCHARGE_MODE", DbType.String, 2, ConstantVariable.VAR_CHARGE_MODE);

                loDB.R_AddCommandParameter(loCmd, "@CFOLLOW_UP_DATE", DbType.String, 8, string.IsNullOrWhiteSpace(poNewEntity.Data.CFOLLOW_UP_DATE) ? "" : poNewEntity.Data.CFOLLOW_UP_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CHO_PLAN_DATE", DbType.String, 8, string.IsNullOrWhiteSpace(poNewEntity.Data.CHO_PLAN_DATE) ? "" : poNewEntity.Data.CHO_PLAN_DATE);
                loDB.R_AddCommandParameter(loCmd, "@LWITH_FO", DbType.Boolean, 20, poNewEntity.Data.LWITH_FO);
                loDB.R_AddCommandParameter(loCmd, "@CBILLING_RULE_TYPE", DbType.String, 20, string.IsNullOrWhiteSpace(poNewEntity.Data.CBILLING_RULE_TYPE) ? "" : poNewEntity.Data.CBILLING_RULE_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CBILLING_RULE_CODE", DbType.String, 20, poNewEntity.Data.CBILLING_RULE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@NBOOKING_FEE", DbType.Decimal, 100, poNewEntity.Data.NBOOKING_FEE);
                loDB.R_AddCommandParameter(loCmd, "@CTC_CODE", DbType.String, 20, poNewEntity.Data.CTC_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLINK_TRANS_CODE", DbType.String, 30, poNewEntity.Data.CLINK_TRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLINK_REF_NO", DbType.String, 30, poNewEntity.Data.CLINK_REF_NO);

                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT {@poParameter}", loDbParam);

                    var loDataTable = loDB.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<PMT02610DTO>(loDataTable).FirstOrDefault();

                    if (loTempResult != null)
                        poNewEntity.Data.CREF_NO = loTempResult.CREF_NO;
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
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
