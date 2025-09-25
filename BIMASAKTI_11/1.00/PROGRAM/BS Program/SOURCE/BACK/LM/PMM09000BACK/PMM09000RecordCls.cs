using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.Logs;
using PMM09000COMMON.UtiliyDTO;
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
using PMM09000COMMON.Amortization_Entry_DTO;

namespace PMM09000Back
{
    public class PMM09000RecordCls
    {
        private readonly LoggerPMM09000 _logger;
        private readonly ActivitySource _activitySource;
        public PMM09000RecordCls()
        {
            _logger = LoggerPMM09000.R_GetInstanceLogger();
            _activitySource = PMM09000Activity.R_GetInstanceActivitySource();
        }
        public PMM09000EntryHeaderDTO GetAmortizationDetail(PMM09000DbParameterDTO poParameter)
        {
            string? lcMethodName = nameof(GetAmortizationDetail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            PMM09000EntryHeaderDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AMORTIZATION_DT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_OPTION", DbType.String, 2, poParameter.CUNIT_OPTION);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_TYPE", DbType.String, 2, poParameter.CTRANS_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 40, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<PMM09000EntryHeaderDTO>(loDataTable).FirstOrDefault() != null ?
                    R_Utility.R_ConvertTo<PMM09000EntryHeaderDTO>(loDataTable).FirstOrDefault() : new PMM09000EntryHeaderDTO ();

                _logger.LogDebug("{@ObjectReturn} ", loReturn!);
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
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
