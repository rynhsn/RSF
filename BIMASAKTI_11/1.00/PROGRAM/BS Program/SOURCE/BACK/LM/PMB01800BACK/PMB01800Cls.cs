using PMB01800COMMON;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMB01800Common.DTOs;
using PMB01800COMMON.DTOs;

namespace PMB01800BACK
{
    public class PMB01800Cls
    {
        private readonly LoggerPMB01800? _logger;
        private readonly ActivitySource _activitySource;

        /*
         * Constructor
         * Digunakan untuk inisialisasi logger dan activity source
         * kemudian mendapatkan instance logger
         * dan instance activity source
         */
        public PMB01800Cls()
        {
            _logger = LoggerPMB01800.R_GetInstanceLogger(); // Get instance of logger
            _activitySource = PMB01800Activity.R_GetInstanceActivitySource(); // Get instance of activity source
        }

        /*
         * Get Property List
         * Digunakan untuk mendapatkan daftar property
         * kemudian dikirim sebagai response ke controller dalam bentuk List<PMB01600PropertyDTO>
         */
        public async Task<List<PMB01800PropertyDTO>> GetPropertyList(PMB01800PropertyParamDTO poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList)); // Start activity
            R_Exception loEx = new(); // Create new exception object
            List<PMB01800PropertyDTO> loRtn = null; // Create new list of PMB01600PropertyDTO
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

                loRtn = R_Utility.R_ConvertTo<PMB01800PropertyDTO>(loDataTable).ToList(); // Convert the data table to list of PMB01600PropertyDTO
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

        public async Task<List<PMB01800GetDepositListDTO>> PMB01800GetDepositList(PMB01800GetDepositListParamDTO poParam)
        {
            using Activity loActivity = _activitySource.StartActivity(nameof(PMB01800GetDepositList));
            R_Exception loEx = new();
            List<PMB01800GetDepositListDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            var lcQuery = "";
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_GET_GENERATE_DEPOSIT_ADJ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_TYPE", DbType.String, 2, poParam.CTRANS_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CPAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 10, poParam.CPAR_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParam.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CPROPERTY_ID" or
                            "@CTRANS_TYPE" or
                            "@CTRANS_CODE" or
                            "@CDEPT_CODE" or
                            "@CUSER_ID"
                    ).Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<PMB01800GetDepositListDTO>(loDataTable).ToList();
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
}
