using PMT00100COMMON.Booking;
using PMT00100COMMON.Logger;
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
using PMT00100COMMON.UtilityDTO;
using PMT00100COMMON.UnitList;

namespace PMT00100BACK
{
    public class PMT00100BookingCls : R_BusinessObject<PMT00100BookingDTO>
    {
        private LoggerPMT00100 _logger;
        private readonly ActivitySource _activitySource;

        private readonly RSP_PM_MAINTAIN_AGREEMENTResources.Resources_Dummy_Class _oRSPMAINTAINAGREEMENT = new();
        private readonly RSP_PM_UPDATE_AGREEMENT_TRANS_STSResources.Resources_Dummy_Class _oRSPMAINTAINTENANT = new();
        private readonly RSP_PM_MAINTAIN_AGREEMENT_UNITResources.Resources_Dummy_Class _oRSPMAINTAINUNIT = new();

        public PMT00100BookingCls()
        {
            _logger = LoggerPMT00100.R_GetInstanceLogger();
            _activitySource = PMT00100Activity.R_GetInstanceActivitySource();
        }
        public VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODEDb(PMT00100DbParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetVAR_GSM_TRANSACTION_CODEDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            VarGsmTransactionCodeDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, "802011");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() != null
                    ? R_Utility.R_ConvertTo<VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() : new VarGsmTransactionCodeDTO();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }
        public PMT00100BookingDTO GetAgreementDetailDb(PMT00100BookingDTO poParameter)
        {
            string? lcMethod = nameof(GetAgreementDetailDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT00100BookingDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<PMT00100BookingDTO>(loDataTable).FirstOrDefault() != null ?
                    R_Utility.R_ConvertTo<PMT00100BookingDTO>(loDataTable).FirstOrDefault() :
                    new PMT00100BookingDTO();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn!;
        }
        protected override PMT00100BookingDTO R_Display(PMT00100BookingDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT00100BookingDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                loDb = new();
                loCommand = loDb.GetCommand(); lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCommand, true);
                loRtn = R_Utility.R_ConvertTo<PMT00100BookingDTO>(loProfileDataTable).FirstOrDefault()!;
                _logger.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }

        protected override void R_Saving(PMT00100BookingDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

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
                switch (poNewEntity.CMODE_CRUD)
                {
                    case "1":
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        SaveSPMaintainAgreementUnit(poNewEntity, poCRUDMode, loConn);
                        break;
                    case "2":
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        break;
                    default:
                        break;
                }
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
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(PMT00100BookingDTO poEntity)
        {
            string lcMethodName = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

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

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 30, poEntity.CREF_DATE); //
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, poEntity.CDOC_NO); //
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 30, poEntity.CDOC_DATE); //
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);

                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE ", DbType.String, 20, poEntity.CHO_PLAN_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, 8, poEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTHS", DbType.Int32, 8, poEntity.IMONTHS);
                loDb.R_AddCommandParameter(loCommand, "@IYEARS", DbType.Int32, 8, poEntity.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 510, poEntity.CUNIT_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, poEntity.CLEASE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poEntity.CCHARGE_MODE);  //loDb.R_AddCommandParameter(loCommand, "@NCOMMON_AREA_SIZE", DbType.Int32, 10, poEntity.NCOMMON_AREA_SIZE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                //CR 14-11-2024
                loDb.R_AddCommandParameter(loCommand, "@CSTART_TIME", DbType.String, 8, poEntity.CSTART_TIME);
                loDb.R_AddCommandParameter(loCommand, "@CEND_TIME", DbType.String, 8, poEntity.CEND_TIME);


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
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
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
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
            }
            loException.ThrowExceptionIfErrors();
        }
        private void SaveSPMaintainAgreement(ref PMT00100BookingDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            string? lcMethodName = nameof(SaveSPMaintainAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCommand = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                //Set Action 
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                loConn = poConnection;
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);

                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 20, poNewEntity.CREF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 8, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, poNewEntity.CDOC_NO ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poNewEntity.CHO_PLAN_DATE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, int.MaxValue, poNewEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTHS", DbType.Int32, int.MaxValue, poNewEntity.IMONTHS);
                loDb.R_AddCommandParameter(loCommand, "@IYEARS", DbType.Int32, int.MaxValue, poNewEntity.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poNewEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 255, poNewEntity.CUNIT_DESCRIPTION ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poNewEntity.CNOTES ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, poNewEntity.CLEASE_MODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poNewEntity.CCHARGE_MODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CORIGINAL_REF_NO", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CFOLLOW_UP_DATE", DbType.String, 8, poNewEntity.CFOLLOW_UP_DATE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CEXPIRED_DATE", DbType.String, 8, poNewEntity.CEXPIRED_DATE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@LWITH_FO", DbType.Boolean, 1, false);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_TYPE", DbType.String, 10, poNewEntity.CBILLING_RULE_TYPE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 20, poNewEntity.CBILLING_RULE_CODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@NACTUAL_PRICE", DbType.String, 20, poNewEntity.NACTUAL_PRICE);
                loDb.R_AddCommandParameter(loCommand, "@NBOOKING_FEE", DbType.String, 20, poNewEntity.NBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CTC_CODE", DbType.String, 20, poNewEntity.CTC_CODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CLINK_TRANS_CODE", DbType.String, 10, poNewEntity.CLINK_TRANS_CODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CLINK_REF_NO", DbType.String, 30, poNewEntity.CLINK_REF_NO ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CHO_PLAN_DATE", DbType.String, 30, poNewEntity.CHO_PLAN_DATE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@IHOURS", DbType.Int32, int.MaxValue, poNewEntity.IHOURS);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_TIME", DbType.String, 8, poNewEntity.CSTART_TIME ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CEND_TIME", DbType.String, 8, poNewEntity.CEND_TIME ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 8, poNewEntity.CSTRATA_TAX_ID ?? "");

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {

                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    var loData = R_Utility.R_ConvertTo<PMT00100BookingDTO>(loDataTable).FirstOrDefault()!;
                    if (poNewEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREF_NO = loData.CREF_NO;
                    }
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.LogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void SaveSPMaintainAgreementUnit(PMT00100BookingDTO poDataUnit, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            string? lcMethod = nameof(SaveSPMaintainAgreementUnit);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCommand = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                loConn = poConnection;
                loCommand = loDb.GetCommand();

                _logger!.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_UNIT";
                _logger.LogDebug("{@ObjectTextQuery}", lcQuery);
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);


                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poDataUnit.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poDataUnit.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poDataUnit.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poDataUnit.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poDataUnit.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poDataUnit.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poDataUnit.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poDataUnit.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@NACTUAL_AREA_SIZE", DbType.Decimal, int.MaxValue, poDataUnit.NACTUAL_AREA_SIZE);
                loDb.R_AddCommandParameter(loCommand, "@NCOMMON_AREA_SIZE", DbType.Decimal, int.MaxValue, poDataUnit.NCOMMON_AREA_SIZE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poDataUnit.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.LogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public bool ProcessAgreement(AgreementProcessDTO poEntity)
        {
            string? lcMethodName = nameof(ProcessAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            bool llReturn = false;

            R_Exception loException = new R_Exception();
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            DbConnection loConn = null;
            try
            {
                loDb = new();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_UPDATE_AGREEMENT_TRANS_STS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CNEW_STATUS", DbType.String, 20, poEntity.CNEW_STATUS);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                try
                {
                    var loDataTable = loDb.SqlExecNonQuery(loConn, loCommand, false);
                    llReturn = true;
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    llReturn = false;
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                llReturn = false;
                _logger.LogError(loException);
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
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loException.ThrowExceptionIfErrors();
            return llReturn;
        }
    }
}
