using PMT01700COMMON.Logs;
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
using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO.Utilities.Print;

namespace PMT01700BACK
{
    public class PMT01700LOO_OfferListCls
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;

        private readonly RSP_PM_UPDATE_AGREEMENT_TRANS_STSResources.Resources_Dummy_Class _oRSPUpdateAgreementSTS = new();
        public PMT01700LOO_OfferListCls()
        {
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_GetInstanceActivitySource();
        }

        public List<PMT01700LOO_OfferList_OfferListDTO> GetOfferListDb(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            string? lcMethodName = nameof(GetOfferListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<PMT01700LOO_OfferList_OfferListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 20, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_REF_DATE", DbType.String, 8, poParameter.CFROM_REF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_STATUS_LIST", DbType.String, 50, poParameter.CTRANS_STATUS_LIST);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700LOO_OfferList_OfferListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
        }

        public List<PMT01700LOO_OfferList_UnitListDTO> GetUnitListDb(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            string? lcMethodName = nameof(GetUnitListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<PMT01700LOO_OfferList_UnitListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700LOO_OfferList_UnitListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
        }
        public bool ProcessAgreement(PMT01700LOO_ProcessOffer_DTO poEntity)
        {
            string? lcMethodName = nameof(ProcessAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            bool llReturn = false;

            R_Exception loException = new R_Exception();
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            DbConnection loConn = null;
            try
            {
                loDb = new();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_UPDATE_AGREEMENT_TRANS_STS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CNEW_STATUS", DbType.String, 20, poEntity.CNEW_STATUS);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                try
                {
                    var loDataTable = loDb.SqlExecNonQuery(loConn, loCommand, false);
                    llReturn = true;
                    // loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    llReturn = false;
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                llReturn = false;
                _logger.LogError(loException);
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
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loException.ThrowExceptionIfErrors();
            return llReturn;
        }
        public List<ReportTemplateListDTO> GetDataSign_ReportTemplate(ParamaterGetReportTemplateListDTO poParameter)
        {

            string? lcMethod = nameof(GetDataSign_ReportTemplate);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<ReportTemplateListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));

                lcQuery = "RSP_GET_REPORT_TEMPLATE_LIST";
                _logger.LogDebug("{@ObjectQuery} ", lcQuery);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 30, poParameter.CPROGRAM_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTEMPLATE_ID ", DbType.String, 30, poParameter.CTEMPLATE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _logger.LogInfo(string.Format("Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<ReportTemplateListDTO>(loDataTable).ToList();
                _logger.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

    }
}
