using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM00500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Reflection.Metadata;
using PMM00500Common;
using PMM00500Common.Logs;

namespace PMM00500Back
{
    public class PMM00510Cls : R_BusinessObject<PMM00510DTO>
    {
        private LoggerPMM00500 _loggerPMM00510;
        private readonly ActivitySource _activitySource;
        public PMM00510Cls()
        {
            _loggerPMM00510 = LoggerPMM00500.R_GetInstanceLogger();
            _activitySource = PMM00500Activity.R_GetInstanceActivitySource();
        }

        #region CRUD
        protected override PMM00510DTO R_Display(PMM00510DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start R_Display PMM00510");
            PMM00510DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_TYPE_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 2, poEntity.CCHARGE_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_ID", DbType.String, 20, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMM00510.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM00510DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End R_Display PMM00510");
            return loResult;
        }
        protected override void R_Saving(PMM00510DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start R_Display PMM00510");
            var loDb = new R_Db();
            DbCommand loCmd;
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            string lcAction = "";
            string lcQuery = "";

            try
            {
                loCmd = loDb.GetCommand();
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                lcQuery = "RSP_PM_MAINTAIN_UNIT_CHARGES";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "CCHARGES_TYPE", DbType.String, 2, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "CCHARGES_ID", DbType.String, 20, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "CCHARGES_NAME", DbType.String, 100, poNewEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "CDESCRIPTION", DbType.String, 200, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "LACTIVE", DbType.Boolean, 20, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "LACCRUAL", DbType.Boolean, 20, poNewEntity.LACCRUAL);
                loDb.R_AddCommandParameter(loCmd, "LTAXABLE", DbType.Boolean, 20, poNewEntity.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "LTAX_EXEMPTION", DbType.Boolean, 20, poNewEntity.LTAX_EXEMPTION);
                loDb.R_AddCommandParameter(loCmd, "CTAX_EXEMPTION_CODE", DbType.String, 2, poNewEntity.CTAX_EXEMPTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "NTAX_EXEMPTION_PCT", DbType.Decimal, 20, poNewEntity.NTAX_EXEMPTION_PCT);
                loDb.R_AddCommandParameter(loCmd, "LOTHER_TAX", DbType.String, 20, poNewEntity.LOTHER_TAX);
                loDb.R_AddCommandParameter(loCmd, "COTHER_TAX_ID", DbType.String, 20, poNewEntity.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "LWITHHOLDING_TAX", DbType.Boolean, 1, poNewEntity.LWITHHOLDING_TAX);
                loDb.R_AddCommandParameter(loCmd, "CWITHHOLDING_TAX_TYPE", DbType.String, 20, poNewEntity.CWITHHOLDING_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "CWITHHOLDING_TAX_ID", DbType.String, 20, poNewEntity.CWITHHOLDING_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "CSERVICE_JRNGRP_CODE", DbType.String, 20, poNewEntity.CSERVICE_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "CACCRUAL_METHOD", DbType.String, 20, poNewEntity.CACCRUAL_METHOD);
                loDb.R_AddCommandParameter(loCmd, "CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "CUSER_ID", DbType.String, 20, poNewEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMM00510.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                if (loEx.Haserror)
                {
                    _loggerPMM00510.LogError(loEx);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            _loggerPMM00510.LogInfo("Close Connection, End R_Saving PMM00510");
            loEx.ThrowExceptionIfErrors();
        }
        protected override void R_Deleting(PMM00510DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start R_Deleting PMM00510");
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = "DELETE";

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_MAINTAIN_UNIT_CHARGES";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "CCHARGES_TYPE", DbType.String, 2, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "CCHARGES_ID", DbType.String, 20, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "CCHARGES_NAME", DbType.String, 100, poEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "CDESCRIPTION", DbType.String, 1000, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "LACCRUAL", DbType.Boolean, 20, poEntity.LACCRUAL);
                loDb.R_AddCommandParameter(loCmd, "LTAXABLE", DbType.Boolean, 20, poEntity.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "LTAX_EXEMPTION", DbType.Boolean, 20, poEntity.LTAX_EXEMPTION);
                loDb.R_AddCommandParameter(loCmd, "CTAX_EXEMPTION_CODE", DbType.String, 2, poEntity.CTAX_EXEMPTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "NTAX_EXEMPTION_PCT", DbType.Decimal, 20, poEntity.NTAX_EXEMPTION_PCT);
                loDb.R_AddCommandParameter(loCmd, "LOTHER_TAX", DbType.String, 20, poEntity.LOTHER_TAX);
                loDb.R_AddCommandParameter(loCmd, "COTHER_TAX_ID", DbType.String, 20, poEntity.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "LWITHHOLDING_TAX", DbType.Boolean, 1, poEntity.LWITHHOLDING_TAX);
                loDb.R_AddCommandParameter(loCmd, "CWITHHOLDING_TAX_TYPE", DbType.String, 20, poEntity.CWITHHOLDING_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "CWITHHOLDING_TAX_ID", DbType.String, 20, poEntity.CWITHHOLDING_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "CSERVICE_JRNGRP_CODE", DbType.String, 20, poEntity.CSERVICE_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMM00510.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            _loggerPMM00510.LogInfo("End R_Deleting PMM00510");
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region GetList

        public List<PropertyListStreamChargeDTO> PropertyListDB(PMM00500ParameterDB poParameter)
        {
            using Activity activity = _activitySource.StartActivity("PropertyListDB");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start PropertyListDB PMM00510");
            List<PropertyListStreamChargeDTO> loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMM00510.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PropertyListStreamChargeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End PropertyListDB PMM00510");
            return loResult;
        }
        public List<PMM00500GridDTO> GetChargesList(PMM00500ParameterDB poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetChargesList");
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetChargesList PMM00510");
            List<PMM00500GridDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_TYPE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 20, poParameter.CCHARGE_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMM00510.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM00500GridDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End GetChargesList PMM00510");
            return loResult;
        }
        public List<ChargesTaxTypeDTO> TaxTypeListDB(PMM00500ParameterDB poParameter)
        {
            using Activity activity = _activitySource.StartActivity("TaxTypeListDB");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start TaxTypeListDB PMM00510");
            List<ChargesTaxTypeDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_TAX_TYPE', '', '{poParameter.CCULTURE}')";
                _loggerPMM00510.LogDebug("exec query function: {@lcQuery}", lcQuery);
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<ChargesTaxTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMM00510.LogInfo("End TaxTypeListDB PMM00510");
            return loRtn;
        }
        public List<ChargesTaxCodeDTO> TaxCodeListDB(PMM00500ParameterDB poParameter)
        {
            using Activity activity = _activitySource.StartActivity("TaxCodeListDB");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start TaxCodeListDB PMM00510");
            List<ChargesTaxCodeDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_TAX_CODE', '07,08', '{poParameter.CCULTURE}')";
                _loggerPMM00510.LogDebug("exec query function: {@lcQuery}", lcQuery);
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<ChargesTaxCodeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMM00510.LogInfo("End TaxCodeListDB PMM00510");
            return loRtn;
        }
        public List<AccurualDTO> AccrualListDB(PMM00500ParameterDB poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AccrualListDB");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start AccrualListDB PMM00510");
            List<AccurualDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_ACCRUAL_METHOD', '', '{poParameter.CCULTURE}')";
                _loggerPMM00510.LogDebug("exec query function: {@lcQuery}", lcQuery);
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<AccurualDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMM00510.LogInfo("End AccrualListDB PMM00510");
            return loRtn;
        }
        public List<PMM00510DTO> GetPrintParam(PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintParam");
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetPrintParam PMM00510");
            List<PMM00510DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_PRINT_UNIT_CHARGES";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_FROM", DbType.String, 20, poParameter.CCHARGES_TYPE_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_TO", DbType.String, 20, poParameter.CCHARGES_TYPE_TO);
                loDb.R_AddCommandParameter(loCmd, "@CSHORT_BY", DbType.String, 20, poParameter.CSHORT_BY);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_INACTIVE", DbType.Boolean, 20, poParameter.LPRINT_INACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_DETAIL_ACC", DbType.Boolean, 20, poParameter.LPRINT_DETAIL_ACC);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poParameter.CUSER_LOGIN_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMM00510.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM00510DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End GetPrintParam PMM00510");
            return loResult;
        }

        public List<UnitChargesTypeDTO> UnitChargesTypeListDB(PMM00500ParameterDB poParameter)
        {
            using Activity activity = _activitySource.StartActivity("UnitChargesTypeListDB");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start UnitTaxTypeListDB PMM00510");
            List<UnitChargesTypeDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_UNIT_CHARGES_TYPE', '', '{poParameter.CCULTURE}')";


                _loggerPMM00510.LogDebug("exec query function: {@lcQuery}", lcQuery);
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<UnitChargesTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMM00510.LogInfo("End TaxCodeListDB PMM00510");
            return loRtn;
        }
        #endregion

        public void CopyNewProcess(PMM00500ParameterDB poParameter, PMM00510DTO poData)
        {
            using Activity activity = _activitySource.StartActivity("CopyNewProcess");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start CopyNewProcess PMM00510");
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"EXEC RSP_PM_COPY_NEW_UNIT_CHARGES '{poParameter.CCOMPANY_ID}', '{poData.CPROPERTY_ID}', '{poData.CCURRENT_CHARGES_TYPE}','{poData.CCURRENT_CHARGE_ID}', '{poData.CNEW_CHARGES_ID}', '{poData.CNEW_CHARGES_NAME}','{poParameter.CUSER_ID}'       ";

                _loggerPMM00510.LogDebug("exec query function: {@lcQuery}", lcQuery);
                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
        EndBlock:
            _loggerPMM00510.LogInfo("End CopyNewProcess PMM00510");
            loEx.ThrowExceptionIfErrors();
        }
        public void RSP_GS_ACTIVE_INACTIVE_UnitChargesMethod(PMM00500ParameterDB poParameter, PMM00510DTO poData)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_UnitChargesMethod");
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start RSP_GS_ACTIVE_INACTIVE_UnitChargesMethod PMM00510");
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_ACTIVE_INACTIVE_UNIT_CHARGES";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poData.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poData.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poData.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "LACTIVE", DbType.String, 20, poData.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMM00510.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            _loggerPMM00510.LogInfo("End RSP_GS_ACTIVE_INACTIVE_UnitChargesMethod PMM00510");
            loEx.ThrowExceptionIfErrors();
        }


    }
}
