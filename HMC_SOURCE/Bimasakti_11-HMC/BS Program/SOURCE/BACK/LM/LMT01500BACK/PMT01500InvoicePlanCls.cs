using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT01500Common.DTO._5._Invoice_Plan;
using PMT01500Common.Logs;
using PMT01500Common.Utilities;
using R_BackEnd;
using R_Common;

namespace PMT01500Back
{
    public class PMT01500InvoicePlanCls
    {
        private readonly LoggerPMT01500? _loggerPMT01500;
        private readonly ActivitySource _activitySource;

        public PMT01500InvoicePlanCls()
        {
            _loggerPMT01500 = LoggerPMT01500.R_GetInstanceLogger();
            _activitySource = PMT01500Activity.R_GetInstanceActivitySource();
        }

        public PMT01500InvoicePlanHeaderDTO GetInvoicePlanHeaderDb(PMT01500UtilitiesParameterDTO poParameterInternal, PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetInvoicePlanHeaderDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loEx = new R_Exception();
            PMT01500InvoicePlanHeaderDTO? loRtn = null;
            DataTable? loDataTable;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            R_Db loDb;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCmd = loDb.GetCommand();
                _loggerPMT01500.LogDebug("{@ObjectDb}", loCmd);

                _loggerPMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_AGREEMENT_DETAIL";
                _loggerPMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                _loggerPMT01500.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerPMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                _loggerPMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                _loggerPMT01500.LogInfo(string.Format("Convert the data in loDataTable to data of PMT01500InvoicePlanHeaderDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT01500InvoicePlanHeaderDTO>(loDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }



        public List<PMT01500InvoicePlanChargesListDTO> GetInvoicePlanChargeListDb(PMT01500UtilitiesParameterDTO poParameterInternal, PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetInvoicePlanChargeListDb);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01500InvoicePlanChargesListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_AGREEMENT_CHARGES_LIST";
                _loggerPMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerPMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01500InvoicePlanChargesListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01500InvoicePlanChargesListDTO>(loDataTable).ToList();
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT01500InvoicePlanListDTO> GetInvoicePlanListDb(PMT01500UtilitiesParameterDTO poParameterInternal, PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetInvoicePlanChargeListDb);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01500InvoicePlanListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_AGREEMENT_INV_PLAN_LIST";
                _loggerPMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerPMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NOT DONE YET", DbType.String, 10, "NOT DONE YET");
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01500InvoicePlanListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01500InvoicePlanListDTO>(loDataTable).ToList();
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}