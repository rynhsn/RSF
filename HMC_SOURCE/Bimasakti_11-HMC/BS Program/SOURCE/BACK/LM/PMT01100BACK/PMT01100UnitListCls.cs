using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.Logs;
using PMT01100Common.Utilities;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using PMT01100Common.Utilities.Db;

namespace PMT01100Back
{
    public class PMT01100UnitListCls
    {
        private readonly LoggerPMT01100? _loggerPMT01100;
        private readonly ActivitySource _activitySource;

        public PMT01100UnitListCls()
        {
            _loggerPMT01100 = LoggerPMT01100.R_GetInstanceLogger();
            _activitySource = PMT01100Activity.R_GetInstanceActivitySource();
        }

        public List<PMT01100UnitList_BuildingDTO> GetBuildingListDb(PMT01100UtilitiesParameterDTO poParameterInternal, PMT01100UtilitiesParameterPropertyDTO poParameter)
        {
            string? lcMethod = nameof(GetBuildingListDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100UnitList_BuildingDTO>? loReturn = null;
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
                lcQuery = "RSP_GS_GET_BUILDING_LIST";
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
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100UnitList_BuildingDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100UnitList_BuildingDTO>(loDataTable).ToList();
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

        public List<PMT01100UnitList_UnitListDTO> GetUnitListDb(PMT01100UtilitiesParameterDTO poParameterInternal, PMT01100UtilitiesParameterPropertyandBuildingDTO poParameter)
        {
            string? lcMethod = nameof(GetUnitListDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100UnitList_UnitListDTO>? loReturn = null;
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
                lcQuery = "RSP_GS_GET_BUILDING_UNIT_LIST";
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
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100UnitList_UnitListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100UnitList_UnitListDTO>(loDataTable).ToList();
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

        public List<PMT01100PropertyListDTO> GetPropertyListDb(PMT01100UtilitiesParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetPropertyListDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100PropertyListDTO>? loReturn = null;
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
                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
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
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100PropertyListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100PropertyListDTO>(loDataTable).ToList();
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

    }
}
