using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT01500Common.DTO._6._Deposit;
using PMT01500Common.Logs;
using PMT01500Common.Utilities;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_PM_MAINTAIN_AGREEMENT_DEPOSITResources;

namespace PMT01500Back
{
    public class PMT01500DepositCls : R_BusinessObject<PMT01500DepositDetailDTO>
    {
        private Resources_Dummy_Class _oRsp = new Resources_Dummy_Class();
        private readonly LoggerPMT01500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public PMT01500DepositCls()
        {
            _loggerLMT01500 = LoggerPMT01500.R_GetInstanceLogger();
            _activitySource = PMT01500Activity.R_GetInstanceActivitySource();
        }

        public PMT01500DepositHeaderDTO GetDepositHeaderDb(PMT01500UtilitiesParameterDTO poParameterInternal, PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetDepositHeaderDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loEx = new R_Exception();
            PMT01500DepositHeaderDTO? loRtn = null;
            DataTable? loDataTable;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCmd = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to data of LMT01500DepositHeaderDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT01500DepositHeaderDTO>(loDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT01500DepositListDTO> GetDepositListDb(PMT01500UtilitiesParameterDTO poParameterInternal, PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetDepositListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01500DepositListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DEPOSIT_LIST";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poParameter.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500DepositListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01500DepositListDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override PMT01500DepositDetailDTO R_Display(PMT01500DepositDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01500DepositDetailDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DEPOSIT_DT";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
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
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loRtn to data of LMT01500DepositDetailDTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loRtn = R_Utility.R_ConvertTo<PMT01500DepositDetailDTO>(loProfileDataTable).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override void R_Saving(PMT01500DepositDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMT01500DepositDetailDTO? loEntity;
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            string? lcAction;
            R_Db? loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMT01500.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = poCRUDMode == eCRUDMode.AddMode ? "ADD" : "EDIT";
                _loggerLMT01500.LogDebug("{@ObjectAction}", lcAction);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, string.IsNullOrEmpty(poNewEntity.CREF_NO) ? "" : poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poNewEntity.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poNewEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poNewEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean, 2, poNewEntity.LCONTRACTOR);
                loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, poNewEntity.CCONTRACTOR_ID);
                // loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_TYPE", DbType.String, 2, poNewEntity.CDEPOSIT_JRNGRP_TYPE);
                //loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_CODE", DbType.String, 8, poNewEntity.CDEPOSIT_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_ID", DbType.String, 20, poNewEntity.CDEPOSIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_DATE", DbType.String, 8, poNewEntity.CDEPOSIT_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NDEPOSIT_AMT", DbType.Decimal, 255, poNewEntity.NDEPOSIT_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, Int32.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerLMT01500.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    loEntity = R_Utility.R_ConvertTo<PMT01500DepositDetailDTO>(loDataTable).FirstOrDefault();
                    if (loEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CSEQ_NO = string.IsNullOrEmpty(loEntity.CSEQ_NO) ? "" : loEntity.CSEQ_NO;
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMT01500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(PMT01500DepositDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            DbConnection? loConn = null;
            string? lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, string.IsNullOrEmpty(poEntity.CREF_NO) ? "" : poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poEntity.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean, 2, poEntity.LCONTRACTOR);
                loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, poEntity.CCONTRACTOR_ID);
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
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                try
                {
                    _loggerLMT01500.LogInfo(string.Format("Execute the SQL non-query (delete) with loConn and loCommand in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMT01500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
        }

    }
}