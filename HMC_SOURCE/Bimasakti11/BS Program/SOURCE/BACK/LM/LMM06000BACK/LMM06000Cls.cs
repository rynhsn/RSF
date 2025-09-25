using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows.Input;
using PMM06000COMMON;
using PMM06000COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMM06000Back
{
    public class LMM06000Cls : R_BusinessObject<LMM06000BillingRuleDetailDTO>
    {

        private LoggerLMM06000 _loggerLMM06000;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_MAINTAIN_STRATA_BILLING_RULEResources.Resources_Dummy_Class _objectRSP = new();
        public LMM06000Cls()
        {
            //Initial and Get Logger
            _loggerLMM06000 = LoggerLMM06000.R_GetInstanceLogger();
            _activitySource = LMM06000Activity.R_GetInstanceActivitySource();
        }
        protected override LMM06000BillingRuleDetailDTO R_Display(LMM06000BillingRuleDetailDTO poEntity)
        {
            string lcMethodName = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            
            R_Exception loException = new R_Exception();
            LMM06000BillingRuleDetailDTO loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_PM_GET_STRATA_BILLING_RULE_DT";
                loDb = new R_Db();
                var loCommand = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_TYPE_CTG_ID", DbType.String, 20, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 20, poEntity.CBILLING_RULE_CODE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loReturn = R_Utility.R_ConvertTo<LMM06000BillingRuleDetailDTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo("End process method R_Display on Cls");

            return loReturn;
        }

        protected override void R_Saving(LMM06000BillingRuleDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                    default:
                        break;
                }
                lcQuery = "RSP_PM_MAINTAIN_STRATA_BILLING_RULE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_TYPE_CTG_ID", DbType.String, 20, poNewEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 50, poNewEntity.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_NAME", DbType.String, 100, poNewEntity.CBILLING_RULE_NAME);

                loDb.R_AddCommandParameter(loCommand, "@LBOOKING_FEE", DbType.Boolean, 2, poNewEntity.LBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@LBOOKING_FEE_OVERWRITE", DbType.Boolean, 2, poNewEntity.LBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@NMIN_BOOKING_FEE", DbType.String, 21, poNewEntity.NMIN_BOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CBOOKING_FEE_CHARGES_ID", DbType.String, 50, poNewEntity.CBOOKING_FEE_CHARGE_ID);

                loDb.R_AddCommandParameter(loCommand, "@LWITH_DP", DbType.Boolean, 2, poNewEntity.LWITH_DP);
                loDb.R_AddCommandParameter(loCommand, "@IDP_PERCENTAGE", DbType.Int32, 25, poNewEntity.IDP_PERCENTAGE);
                loDb.R_AddCommandParameter(loCommand, "@IDP_INTERVAL", DbType.Int32, 25, poNewEntity.IDP_INTERVAL);
                loDb.R_AddCommandParameter(loCommand, "@CDP_PERIOD_MODE", DbType.String, 50, poNewEntity.CDP_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CDP_CHARGE_ID", DbType.String, 50, poNewEntity.CDP_CHARGE_ID);

                loDb.R_AddCommandParameter(loCommand, "@LINSTALLMENT", DbType.Boolean, 2, poNewEntity.LINSTALLMENT);
                loDb.R_AddCommandParameter(loCommand, "@IINSTALLMENT_PERCENTAGE", DbType.Int32, 50, poNewEntity.IINSTALLMENT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCommand, "@IINSTALLMENT_INTERVAL", DbType.Int32, 50, poNewEntity.IINSTALLMENT_INTERVAL);
                loDb.R_AddCommandParameter(loCommand, "@CINSTALLMENT_PERIOD_MODE", DbType.String, 50, poNewEntity.CINSTALLMENT_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CINSTALLMENT_CHARGE_ID", DbType.String, 50, poNewEntity.CINSTALLMENT_CHARGE_ID);


                loDb.R_AddCommandParameter(loCommand, "@LBANK_CREDIT", DbType.Boolean, 5, poNewEntity.LBANK_CREDIT);
                loDb.R_AddCommandParameter(loCommand, "@IBANK_CREDIT_PERCENTAGE", DbType.Int32, 50, poNewEntity.IBANK_CREDIT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCommand, "@IBANK_CREDIT_INTERVAL", DbType.Int32, 50, poNewEntity.IBANK_CREDIT_INTERVAL);

                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE", DbType.Boolean, 2, poNewEntity.LACTIVE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _loggerLMM06000.LogInfo("END process method R_Saving on Cls");
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _loggerLMM06000.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));


            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
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
        EndBlock:
            loException.ThrowExceptionIfErrors();

        }

        protected override void R_Deleting(LMM06000BillingRuleDetailDTO poEntity)
        {
            string lcMethodName = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loException = new R_Exception();
            DbCommand loCommand;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                string lcAction = "DELETE";

                var lcQuery = "RSP_PM_MAINTAIN_STRATA_BILLING_RULE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_TYPE_CTG_ID", DbType.String, 20, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 50, poEntity.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_NAME", DbType.String, 50, poEntity.CBILLING_RULE_NAME);

                loDb.R_AddCommandParameter(loCommand, "@LBOOKING_FEE", DbType.Boolean, 2, poEntity.LBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@LBOOKING_FEE_OVERWRITE", DbType.Boolean, 2, poEntity.LBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@NMIN_BOOKING_FEE", DbType.String, 21, poEntity.NMIN_BOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CBOOKING_FEE_CHARGES_ID", DbType.String, 50, poEntity.CBOOKING_FEE_CHARGE_ID);

                loDb.R_AddCommandParameter(loCommand, "@LWITH_DP", DbType.Boolean, 2, poEntity.LWITH_DP);
                loDb.R_AddCommandParameter(loCommand, "@IDP_PERCENTAGE", DbType.Int32, 25, poEntity.IDP_PERCENTAGE);
                loDb.R_AddCommandParameter(loCommand, "@IDP_INTERVAL", DbType.Int32, 25, poEntity.IDP_INTERVAL);
                loDb.R_AddCommandParameter(loCommand, "@CDP_PERIOD_MODE", DbType.String, 50, poEntity.CDP_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CDP_CHARGE_ID", DbType.String, 50, poEntity.CDP_PERIOD_MODE);

                loDb.R_AddCommandParameter(loCommand, "@LINSTALLMENT", DbType.Boolean, 2, poEntity.LINSTALLMENT);
                loDb.R_AddCommandParameter(loCommand, "@IINSTALLMENT_PERCENTAGE", DbType.Int32, 50, poEntity.IINSTALLMENT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCommand, "@IINSTALLMENT_INTERVAL", DbType.Int32, 50, poEntity.IINSTALLMENT_INTERVAL);
                loDb.R_AddCommandParameter(loCommand, "@CINSTALLMENT_PERIOD_MODE", DbType.String, 50, poEntity.CINSTALLMENT_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CINSTALLMENT_CHARGE_ID", DbType.String, 50, poEntity.CDP_PERIOD_MODE);

                loDb.R_AddCommandParameter(loCommand, "@LBANK_CREDIT", DbType.Boolean, 5, poEntity.LBANK_CREDIT);
                loDb.R_AddCommandParameter(loCommand, "@IBANK_CREDIT_PERCENTAGE", DbType.Int32, 50, poEntity.IBANK_CREDIT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCommand, "@IBANK_CREDIT_INTERVAL", DbType.Int32, 50, poEntity.IBANK_CREDIT_INTERVAL);

                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _loggerLMM06000.LogInfo("End process method R_Deleting on Cls");
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _loggerLMM06000.LogError(loException);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

        }
        public List<LMM06000BillingRuleDTO> BillingRuleList(LMM06000DBParameter poParameter)
        {
            string lcMethodName = nameof(BillingRuleList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<LMM06000BillingRuleDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_PM_GET_STRATA_BILLING_RULE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_TYPE_CTG_ID", DbType.String, 20, poParameter.CUNIT_TYPE_CTG_ID);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE_ONLY", DbType.Boolean, 2, poParameter.LACTIVE_ONLY);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMM06000BillingRuleDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            }
            if (loException.Haserror)
                loException.ThrowExceptionIfErrors();
            
            _loggerLMM06000.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
           
#pragma warning disable CS8603
            return loReturn;
#pragma warning restore CS8603
        }

        public List<LMM06000PropertyDTO> GetAllPropertyList(LMM06000DBParameter poParameter)
        {
            string lcMethodName = nameof(GetAllPropertyList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<LMM06000PropertyDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMM06000PropertyDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn;
        }

        public List<LMM06000UnitTypeDTO> GetAllUnitTypeList(LMM06000DBParameter poParameter)
        {
            string lcMethodName = nameof(GetAllUnitTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<LMM06000UnitTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_UNIT_TYPE_CTG_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMM06000UnitTypeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn;
        }

        public List<LMM06000PeriodDTO> GetAllPeriodList(LMM06000DBParameter poParameter)
        {
            string lcMethodName = nameof(GetAllPeriodList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<LMM06000PeriodDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            string Company = poParameter.CCOMPANY_ID;
            string Culture = poParameter.CULTURE;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = "SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', @CCOMPANY, '_PERIOD_MODE', '', @CULTURE )";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCommand, "CCOMPANY", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CULTURE", DbType.String, 255, poParameter.CULTURE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<LMM06000PeriodDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn;
        }

        public void SetActiveInactiveDb(LMM06000ActiveInactiveDTO poParameter)
        {
            string lcMethodName = nameof(SetActiveInactiveDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCommand;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                lcQuery = "RSP_PM_ACTIVE_INACTIVE_UNIT_TYPE_BILLING_RULE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.PROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE", DbType.Boolean, 1, poParameter.LACTIVE);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 50, poParameter.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 50, poParameter.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM06000.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                loDb.SqlExecNonQuery(loConn, loCommand, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            }

            _loggerLMM06000.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loException.ThrowExceptionIfErrors();
        }

    }
}