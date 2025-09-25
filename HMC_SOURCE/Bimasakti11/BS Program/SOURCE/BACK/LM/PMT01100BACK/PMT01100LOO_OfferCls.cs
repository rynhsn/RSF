using PMT01100Common.Logs;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using R_CommonFrontBackAPI;
using PMT01100Common.Utilities.Response;
using PMT01100Common.Utilities.Request;
using System.Windows.Input;
using PMT01100Common.DTO._1._Unit_List;

namespace PMT01100Back
{
    public class PMT01100LOO_OfferCls : R_BusinessObject<PMT01100LOO_Offer_SelectedOfferDTO>
    {
        private readonly LoggerPMT01100? _loggerPMT01100;
        private readonly ActivitySource _activitySource;


        public PMT01100LOO_OfferCls()
        {
            _loggerPMT01100 = LoggerPMT01100.R_GetInstanceLogger();
            _activitySource = PMT01100Activity.R_GetInstanceActivitySource();
        }


        public PMT01100VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODEDb(PMT01100UtilitiesParameterCompanyDTO poParameter)
        {
            string? lcMethod = nameof(GetVAR_GSM_TRANSACTION_CODEDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01100VarGsmTransactionCodeDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                _loggerPMT01100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, "802041");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100VarGsmTransactionCodeDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() != null ? R_Utility.R_ConvertTo<PMT01100VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() : new PMT01100VarGsmTransactionCodeDTO();
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn!);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        public List<PMT01100ResponseTenantCategoryDTO> GetComboBoxDataTenantCategoryDb(PMT01100UtilitiesParameterDTO poParameterInternal, PMT01100RequestTenantCategoryDTO poParameter)
        {
            string? lcMethod = nameof(GetComboBoxDataTenantCategoryDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100ResponseTenantCategoryDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GS_GET_CATEGORY_LIST";
                _loggerPMT01100.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCATEGORY_TYPE", DbType.String, 2, "20");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ComboBoxDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100ResponseTenantCategoryDTO>(loDataTable).ToList();
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT01100ComboBoxDTO> GetComboBoxDataTaxTypeDb(PMT01100UtilitiesWithCultureIDParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetComboBoxDataTaxTypeDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                "(@BIMASAKTI, @CCOMPANY_ID, @BS_LEASE_MODE, @NONE, @CULTURE_ID);";
                _loggerPMT01100.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_LEASE_MODE", DbType.String, 20, "_BS_TAX_FOR_TYPE");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParameterInternal.CULTURE_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100ComboBoxDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100ComboBoxDTO>(loDataTable).ToList();
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        public List<PMT01100ComboBoxDTO> GetComboBoxDataIDTypeDb(PMT01100UtilitiesWithCultureIDParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetComboBoxDataIDTypeDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                "(@BIMASAKTI, @CCOMPANY_ID, @BS_LEASE_MODE, @NONE, @CULTURE_ID);";
                _loggerPMT01100.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_LEASE_MODE", DbType.String, 15, "_BS_ID_TYPE");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParameterInternal.CULTURE_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100ComboBoxDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100ComboBoxDTO>(loDataTable).ToList();
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        protected override PMT01100LOO_Offer_SelectedOfferDTO R_Display(PMT01100LOO_Offer_SelectedOfferDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01100LOO_Offer_SelectedOfferDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                _loggerPMT01100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loRtn to data of PMT01100LOO_Offer_SelectedOfferDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT01100LOO_Offer_SelectedOfferDTO>(loProfileDataTable).FirstOrDefault()!;
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }

        protected override void R_Deleting(PMT01100LOO_Offer_SelectedOfferDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            DbConnection? loConn = null;
            string? lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01100.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                _loggerPMT01100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 20, poEntity.CREF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 8, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 8, poEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 510, poEntity.CUNIT_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, poEntity.CLEASE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poEntity.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerPMT01100.LogInfo(string.Format("Execute the SQL non-query (delete) with loConn and loCommand in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerPMT01100.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Saving(PMT01100LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            //PMT01100LOO_Offer_SelectedOfferDTO? loResult = null;
            R_Db? loDb = new R_Db();
            DbConnection? loConn = null;


            try
            {
                var loDataUnitList = poNewEntity.ODATA_UNIT_LIST ?? new List<PMT01100LOO_Offer_SelectedDataUnitListDTO>();
                loConn = loDb.GetConnection();
                switch (poNewEntity.CMODE_CRUD)
                {
                    case "1":
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        break;
                    case "2":
                        SaveSPMaintainTenant(ref poNewEntity, poCRUDMode, loConn);
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        break;
                    case "3":
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        if (loDataUnitList.Any())
                        {
                            foreach (PMT01100LOO_Offer_SelectedDataUnitListDTO loDataUnit in loDataUnitList)
                            {
                                SaveSPMaintainAgreementUnit(poDataUnit: loDataUnit, poCRUDMode: poCRUDMode, poConnection: loConn);
                            }
                        }
                        break;
                    case "4":
                        SaveSPMaintainTenant(ref poNewEntity, poCRUDMode, loConn);
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        if (loDataUnitList.Any())
                        {
                            foreach (PMT01100LOO_Offer_SelectedDataUnitListDTO loDataUnit in loDataUnitList)
                            {
                                SaveSPMaintainAgreementUnit(poDataUnit: loDataUnit, poCRUDMode: poCRUDMode, poConnection: loConn);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerPMT01100.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }


            loEx.ThrowExceptionIfErrors();
        }

        private void SaveSPMaintainTenant(ref PMT01100LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            string? lcMethod = nameof(SaveSPMaintainTenant);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCommand = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                //Set Action 
                _loggerPMT01100.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                _loggerPMT01100.LogDebug("{@ObjectAction}", lcAction);


                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = poConnection;
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);


                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);



                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_TENANT";
                _loggerPMT01100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);


                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_NAME", DbType.String, 100, poNewEntity.CTENANT_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CADDRESS", DbType.String, 255, poNewEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CCITY_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CPROVINCE_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CCOUNTRY_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CZIP_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CEMAIL", DbType.String, 100, poNewEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE1", DbType.String, 30, poNewEntity.CPHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE2", DbType.String, 30, poNewEntity.CPHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_CATEGORY_ID", DbType.String, 20, poNewEntity.CTENANT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_TYPE_ID", DbType.String, 20, "Prospect");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_NAME", DbType.String, 100, poNewEntity.CATTENTION1_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_POSITION", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_EMAIL", DbType.String, 100, poNewEntity.CATTENTION1_EMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_MOBILE_PHONE1", DbType.String, 30, poNewEntity.CATTENTION1_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_MOBILE_PHONE2", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_NAME", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_POSITION", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_EMAIL", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_MOBILE_PHONE1", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_MOBILE_PHONE2", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CPAYMENT_TERM_CODE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, "");// ini ada tapi di petik"
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poNewEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLOB_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CFAMILY_CARD", DbType.String, 40, "");
                loDb.R_AddCommandParameter(loCommand, "@CTAX_TYPE", DbType.String, 2, poNewEntity.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@LPPH", DbType.Boolean, 1, false);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 40, poNewEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_NAME", DbType.String, 100, poNewEntity.CTAX_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CID_TYPE", DbType.String, 2, poNewEntity.CID_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CID_NO", DbType.String, 80, poNewEntity.CID_NO);
                loDb.R_AddCommandParameter(loCommand, "@CID_EXPIRED_DATE", DbType.String, 8, poNewEntity.CID_EXPIRED_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 18, 0);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ADDRESS", DbType.String, 255, poNewEntity.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE1", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE2", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CTAX_EMAIL", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CCUSTOMER_TYPE", DbType.String, 2, "01");
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {

                    _loggerPMT01100.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    poNewEntity = R_Utility.R_ConvertTo<PMT01100LOO_Offer_SelectedOfferDTO>(loDataTable).FirstOrDefault()!;
                    if (poNewEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREF_NO = string.IsNullOrEmpty(poNewEntity.CREF_NO) ? "" : poNewEntity.CREF_NO;
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
                _loggerPMT01100.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SaveSPMaintainAgreement(ref PMT01100LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            string? lcMethod = nameof(SaveSPMaintainAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCommand = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                //Set Action 
                _loggerPMT01100.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                _loggerPMT01100.LogDebug("{@ObjectAction}", lcAction);


                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = poConnection;
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);


                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);



                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                _loggerPMT01100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);


                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 20, poNewEntity.CREF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 8, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, poNewEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poNewEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poNewEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 255, poNewEntity.CUNIT_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poNewEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, poNewEntity.CLEASE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poNewEntity.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {

                    _loggerPMT01100.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    poNewEntity = R_Utility.R_ConvertTo<PMT01100LOO_Offer_SelectedOfferDTO>(loDataTable).FirstOrDefault()!;
                    if (poNewEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREF_NO = string.IsNullOrEmpty(poNewEntity.CREF_NO) ? "" : poNewEntity.CREF_NO;
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
                _loggerPMT01100.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SaveSPMaintainAgreementUnit(PMT01100LOO_Offer_SelectedDataUnitListDTO poDataUnit, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            string? lcMethod = nameof(SaveSPMaintainAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCommand = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                //Set Action 
                _loggerPMT01100.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                _loggerPMT01100.LogDebug("{@ObjectAction}", lcAction);


                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = poConnection;
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);


                _loggerPMT01100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01100.LogDebug("{@ObjectDb}", loCommand);



                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_UNIT";
                _loggerPMT01100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);


                _loggerPMT01100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
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
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {

                    _loggerPMT01100.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
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
                _loggerPMT01100.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }
}
