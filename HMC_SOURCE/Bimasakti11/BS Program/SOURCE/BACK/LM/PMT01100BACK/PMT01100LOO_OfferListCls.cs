using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using PMT01100Common.Logs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using PMT01100Common.Utilities.Db;

namespace PMT01100Back
{
    public class PMT01100LOO_OfferListCls
    {
        private readonly LoggerPMT01100? _loggerPMT01100;
        private readonly ActivitySource _activitySource;


        public PMT01100LOO_OfferListCls()
        {
            _loggerPMT01100 = LoggerPMT01100.R_GetInstanceLogger();
            _activitySource = PMT01100Activity.R_GetInstanceActivitySource();
        }


        public List<PMT01100LOO_OfferList_OfferListDTO> GetOfferListDb(PMT01100UtilitiesParameterDTO poParameterInternal, PMT01100UtilitiesParameterOfferListDTO poParameter)
        {
            string? lcMethod = nameof(GetOfferListDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100LOO_OfferList_OfferListDTO>? loReturn = null;
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
                lcQuery = "RSP_PM_GET_AGREEMENT_LIST";
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
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100LOO_OfferList_OfferListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100LOO_OfferList_OfferListDTO>(loDataTable).ToList();
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

        public List<PMT01100LOO_OfferList_UnitListDTO> GetUnitListDb(PMT01100UtilitiesParameterDTO poParameterInternal, PMT01100UtilitiesParameterGetUnitListDTO poParameter)
        {
            string? lcMethod = nameof(GetUnitListDb);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01100LOO_OfferList_UnitListDTO>? loReturn = null;
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
                lcQuery = "RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST";
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

                _loggerPMT01100.LogInfo(string.Format("Convert the data in loDataTable to a list of PMT01100LOO_OfferList_UnitListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT01100LOO_OfferList_UnitListDTO>(loDataTable).ToList();
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
