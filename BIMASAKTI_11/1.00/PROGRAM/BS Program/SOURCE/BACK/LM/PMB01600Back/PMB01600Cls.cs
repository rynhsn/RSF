using PMB01600Common;
using PMB01600Common.Batch;
using PMB01600Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PMB01600Back
{
    public class PMB01600Cls
    {
        private readonly LoggerPMB01600? _logger;
        private readonly ActivitySource _activitySource;

        /*
         * Constructor
         * Digunakan untuk inisialisasi logger dan activity source
         * kemudian mendapatkan instance logger
         * dan instance activity source
         */
        public PMB01600Cls()
        {
            _logger = LoggerPMB01600.R_GetInstanceLogger(); // Get instance of logger
            _activitySource = PMB01600Activity.R_GetInstanceActivitySource(); // Get instance of activity source
        }

        /*
         * Get Property List
         * Digunakan untuk mendapatkan daftar property
         * kemudian dikirim sebagai response ke controller dalam bentuk List<PMB01600PropertyDTO>
         */
        public async Task<List<PMB01600PropertyDTO>> GetPropertyList(PMB01600ParameterDb poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList)); // Start activity
            R_Exception loEx = new(); // Create new exception object
            List<PMB01600PropertyDTO> loRtn = null; // Create new list of PMB01600PropertyDTO
            R_Db loDb; // Database object
            DbConnection loConn; // Database connection object
            DbCommand loCmd; // Database command object
            string lcQuery; // Query
            try
            {
                loDb = new R_Db(); // Create new instance of R_Db
                loConn = await loDb.GetConnectionAsync(); // Get database connection
                loCmd = loDb.GetCommand(); // Get database command

                lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParam.CCOMPANY_ID}', '{poParam.CUSER_ID}'"; // Query to get property list
                loCmd.CommandType = CommandType.Text; // Set command type to text
                loCmd.CommandText = lcQuery; // Set command text to query

                _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true); // Execute the query

                loRtn = R_Utility.R_ConvertTo<PMB01600PropertyDTO>(loDataTable).ToList(); // Convert the data table to list of PMB01600PropertyDTO
            }
            catch (Exception ex)
            {
                loEx.Add(ex); // Add the exception to the exception object
                _logger.LogError(loEx); // Log the exception
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors

            return loRtn; // Return the property list
        }

        /*
         * Get Year Range
         * Digunakan untuk mendapatkan range tahun
         * kemudian dikirim sebagai response ke controller dalam bentuk PMB01600YearRangeDTO
         */
        public async Task<PMB01600YearRangeDTO> GetYearRange(PMB01600ParameterDb poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetYearRange)); // Start activity
            R_Exception loEx = new(); // Create new exception object
            PMB01600YearRangeDTO loReturn = new(); // Create new instance of PMB01600YearRangeDTO
            R_Db loDb; // Database object
            DbConnection loConn; // Database connection object
            DbCommand loCmd; // Database command object
            string lcQuery; // Query

            try
            {
                loDb = new R_Db(); // Create new instance of R_Db
                loConn = await loDb.GetConnectionAsync(); // Get database connection
                loCmd = loDb.GetCommand(); // Get database command

                lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE '{poParam.CCOMPANY_ID}', '', ''"; // Query to get year range
                loCmd.CommandType = CommandType.Text; // Set command type to text
                loCmd.CommandText = lcQuery; // Set command text to query

                _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

                var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true); // Execute the query

                loReturn = R_Utility.R_ConvertTo<PMB01600YearRangeDTO>(DataTable).FirstOrDefault(); // Convert the data table to PMB01600YearRangeDTO
            }
            catch (Exception ex)
            {
                loEx.Add(ex); // Add the exception to the exception object
                _logger.LogError(loEx); // Log the exception
            }

            loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
            return loReturn; // Return the year range
        }

        /*
         * Get System Param
         * Digunakan untuk mendapatkan range tahun
         * kemudian dikirim sebagai response ke controller dalam bentuk PMB01600SystemParamDTO
         */
        public async Task<PMB01600SystemParamDTO> GetSystemParam(PMB01600ParameterDb poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam)); // Start activity
            R_Exception loEx = new(); // Create new exception object
            PMB01600SystemParamDTO loReturn = new(); // Create new instance of PMB01600SystemParamDTO
            R_Db loDb; // Database object
            DbConnection loConn; // Database connection object
            DbCommand loCmd; // Database command object
            string lcQuery; // Query

            try
            {
                loDb = new R_Db(); // Create new instance of R_Db
                loConn = await loDb.GetConnectionAsync(); // Get database connection
                loCmd = loDb.GetCommand(); // Get database command

                lcQuery = $"RSP_PM_GET_SYSTEM_PARAM"; // Query to get System Param
                loCmd.CommandType = CommandType.StoredProcedure; // Set command type to text
                loCmd.CommandText = lcQuery; // Set command text to query
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParam.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CLANGUAGE_ID" or
                            "@CPROPERTY_ID"
                    ).Select(x => x.Value); // Log the query

                var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true); // Execute the query

                loReturn = R_Utility.R_ConvertTo<PMB01600SystemParamDTO>(DataTable).FirstOrDefault(); // Convert the data table to PMB01600SystemParamDTO
            }
            catch (Exception ex)
            {
                loEx.Add(ex); // Add the exception to the exception object
                _logger.LogError(loEx); // Log the exception
            }

            loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
            return loReturn; // Return the System Param
        }


        public async Task<List<PMB01600PeriodDTO>> GetPeriodList(PMB01600ParameterDb poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetPeriodList));
            R_Exception loEx = new();
            List<PMB01600PeriodDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CYEAR"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<PMB01600PeriodDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }


        public async Task<List<PMB01600BillingStatementHeaderDTO>> GetBillingStatementHeaderList(PMB01600ParameterDb poParam)
        {
            using Activity loActivity = _activitySource.StartActivity(nameof(GetBillingStatementHeaderList));
            R_Exception loEx = new();
            List<PMB01600BillingStatementHeaderDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            var lcQuery = "";
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_GET_BILLING_STATEMENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_TENANT_ID", DbType.String, 20, poParam.CFROM_TENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_TENANT_ID", DbType.String, 20, poParam.CTO_TENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREF_PRD", DbType.String, 8, poParam.CREF_PRD);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CPROPERTY_ID" or
                            "@CFROM_TENANT_ID" or
                            "@CTO_TENANT_ID" or
                            "@CREF_PRD"
                    ).Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<PMB01600BillingStatementHeaderDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<List<PMB01600BillingStatementDetailDTO>> GetBillingStatementDetailList(PMB01600ParameterDb poParam)
        {
            using Activity loActivity = _activitySource.StartActivity(nameof(GetBillingStatementDetailList));
            R_Exception loEx = new();
            List<PMB01600BillingStatementDetailDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            var lcQuery = "";
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_GET_BILLING_STATEMENT_DT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poParam.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREF_PRD", DbType.String, 8, poParam.CREF_PRD);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CPROPERTY_ID" or
                            "@CTENANT_ID" or
                            "@CREF_PRD"
                    ).Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<PMB01600BillingStatementDetailDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

    }


    public class PMB01600BatchCls : R_IBatchProcess
    {
        private readonly ActivitySource _activitySource;
        private LoggerPMB01600 _logger;

        public PMB01600BatchCls()
        {
            _logger = LoggerPMB01600.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using var Activity = _activitySource.StartActivity(nameof(R_BatchProcess));
            _logger.LogInfo(string.Format("START process method {0} on Cls", nameof(R_BatchProcess)));
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                _logger.LogInfo("Start Batch Process");
                var loTask = Task.Run(() => { _batchProcess(poBatchProcessPar); });

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task _batchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using var Activity = _activitySource.StartActivity(nameof(_batchProcess));
            var loException = new R_Exception();
            var loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            List<PMB01600ForBatchDTO> loObject = new();
            //object loTempVar = "";
            var LcGroupCode = "";


            try
            {
                await Task.Delay(1000);

                loObject =
                    R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMB01600ForBatchDTO>>(
                        poBatchProcessPar.BigObject);

                _logger.LogInfo("Get User Parameters");
                var loProperty = poBatchProcessPar.UserParameters
                    .Where((x) => x.Key.Equals(PMB01600ContextConstantHeader.CPROPERTY_ID)).FirstOrDefault().Value;
                var loPeriod = poBatchProcessPar.UserParameters
                    .Where((x) => x.Key.Equals(PMB01600ContextConstantHeader.CREF_PRD)).FirstOrDefault().Value;

                var lcProperty = ((System.Text.Json.JsonElement)loProperty).GetString();
                var lcPeriod = ((System.Text.Json.JsonElement)loPeriod).GetString();


                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                _logger.LogInfo("Start Create Temporary Table and Bulk Insert Data");
                lcQuery += "CREATE TABLE #SELECTED_TENANT( " +
                           "INO              INT NOT NULL, " +
                           "CTENANT_ID      varchar(20) NOT NULL" +
                           ");";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                for (var i = 0; i < loObject.Count; i++)
                        {
                    _logger.LogDebug($"INSERT INTO #SELECTED_TENANT " +
                                     $"VALUES (" +
                                     $"{loObject[i].INO}, " +
                                     $"'{loObject[i].CTENANT_ID}');");
                }

                loDb.R_BulkInsert((SqlConnection)loConn, $"#SELECTED_TENANT", loObject);


                _logger.LogInfo("End Create Temporary Table and Bulk Insert Data");
                _logger.LogDebug(lcQuery);
                _logger.LogInfo("Start Exec Upload Query");

                lcQuery = $"RSP_PM_UNDO_BILL_STMT_PROCESS";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, lcPeriod);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CPROPERTY_ID" or
                            "@CRERIOD" or
                            "@CUSER_ID" or
                            "@CKEY_GUID"
                    )
                    .Select(x => x.Value);
                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
                _logger.LogInfo("End Process");

                loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                    if (!(loConn.State == ConnectionState.Closed))
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

            if (loException.Haserror)
            {
                lcQuery = string.Format("EXEC RSP_WRITEUPLOADPROCESSSTATUS '{0}', '{1}', '{2}', 100, '{3}', {4}", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID, poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp, 9);
                loCmd!.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(lcQuery);

                _logger.LogError("Exception Error", loException);
                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                          $"'{poBatchProcessPar.Key.USER_ID}', " +
                          $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                          $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}