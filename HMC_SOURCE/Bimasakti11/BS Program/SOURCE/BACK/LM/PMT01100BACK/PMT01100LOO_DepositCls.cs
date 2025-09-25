using PMT01100Common.DTO._2._LOO._4._LOO___Deposit;
using PMT01100Common.Logs;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Request;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using PMT01100Common.Utilities.Response;

namespace PMT01100Back
{
    public class PMT01100LOO_DepositCls : R_BusinessObject<PMT01100LOO_Deposit_DepositDetailDTO>
    {
        private readonly LoggerPMT01100? _loggerPMT01100;
        private readonly ActivitySource _activitySource;

        public PMT01100LOO_DepositCls()
        {
            _loggerPMT01100 = LoggerPMT01100.R_GetInstanceLogger();
            _activitySource = PMT01100Activity.R_GetInstanceActivitySource();
        }


        public List<PMT01100ResponseUtilitiesCurrencyParameterDTO> GetComboBoxDataCurrencyDb(PMT01100UtilitiesParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetComboBoxDataCurrencyDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100ResponseUtilitiesCurrencyParameterDTO>? loReturn = null;
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
                lcQuery = "RSP_GS_GET_CURRENCY_LIST";
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
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ComboBoxCMeterNoDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100ResponseUtilitiesCurrencyParameterDTO>(loDataTable).ToList();
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
                /*
                if (!loReturn.Any())
                {
                    loException.Add("Err", string.Format("No Data from Selected Utility Type {0}", poParameter.CUTILITY_TYPE));
                }
                */

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

        public PMT01100LOO_Deposit_DepositHeaderDTO GetDepositHeaderDb(PMT01100UtilitiesParameterDTO poParameterInternal, PMT01100LOO_Deposit_RequestDepositHeaderDTO poParameter)
        {
            string? lcMethod = nameof(GetDepositHeaderDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01100LOO_Deposit_DepositHeaderDTO? loReturn = null;
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

                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100LOO_Deposit_DepositHeaderDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100LOO_Deposit_DepositHeaderDTO>(loDataTable).FirstOrDefault() != null ? R_Utility.R_ConvertTo<PMT01100LOO_Deposit_DepositHeaderDTO>(loDataTable).FirstOrDefault() : new PMT01100LOO_Deposit_DepositHeaderDTO();
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

        public List<PMT01100LOO_Deposit_DepositListDTO> GetDepositListDb(PMT01100UtilitiesParameterDTO poParameterInternal, PMT01100UtilitiesParameterDepositListDTO poParameter)
        {
            string? lcMethod = nameof(GetDepositListDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100LOO_Deposit_DepositListDTO>? loReturn = null;
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
                lcQuery = "RSP_PM_GET_AGREEMENT_DEPOSIT_LIST";
                _loggerPMT01100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerPMT01100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01100.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, "");
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID	", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100LOO_Deposit_DepositListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100LOO_Deposit_DepositListDTO>(loDataTable).ToList();
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

        protected override PMT01100LOO_Deposit_DepositDetailDTO R_Display(PMT01100LOO_Deposit_DepositDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01100LOO_Deposit_DepositDetailDTO? loRtn = null;
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
                lcQuery = "RSP_PM_GET_AGREEMENT_DEPOSIT_DT";
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
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loRtn to data of PMT01100LOO_Deposit_DepositDetailDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT01100LOO_Deposit_DepositDetailDTO>(loProfileDataTable).FirstOrDefault()!;
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

        protected override void R_Saving(PMT01100LOO_Deposit_DepositDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMT01100LOO_Deposit_DepositDetailDTO loEntity;
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            string? lcAction;
            R_Db? loDb;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
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

                _loggerPMT01100.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                _loggerPMT01100.LogDebug("{@ObjectAction}", lcAction);

                _loggerPMT01100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
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
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, ""); //Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean, 2, false);
                loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, "");
                // loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_TYPE", DbType.String, 2, poNewEntity.CDEPOSIT_JRNGRP_TYPE);
                //loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_CODE", DbType.String, 8, poNewEntity.CDEPOSIT_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_ID", DbType.String, 20, poNewEntity.CDEPOSIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_DATE", DbType.String, 8, poNewEntity.CDEPOSIT_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NDEPOSIT_AMT", DbType.Decimal, int.MaxValue, poNewEntity.NDEPOSIT_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, Int32.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerPMT01100.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    loEntity = R_Utility.R_ConvertTo<PMT01100LOO_Deposit_DepositDetailDTO>(loDataTable).FirstOrDefault()!;
                    if (loEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CSEQ_NO = loEntity.CSEQ_NO ?? "";
                    }

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

        protected override void R_Deleting(PMT01100LOO_Deposit_DepositDetailDTO poEntity)
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
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
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
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, ""); //Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean, 2, false);
                loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, "");
                // loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_TYPE", DbType.String, 2, poEntity.CDEPOSIT_JRNGRP_TYPE);
                //loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_CODE", DbType.String, 8, poEntity.CDEPOSIT_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_ID", DbType.String, 20, poEntity.CDEPOSIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_DATE", DbType.String, 8, poEntity.CDEPOSIT_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NDEPOSIT_AMT", DbType.Decimal, 255, poEntity.NDEPOSIT_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, Int32.MaxValue, poEntity.CDESCRIPTION);
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

    }
}
