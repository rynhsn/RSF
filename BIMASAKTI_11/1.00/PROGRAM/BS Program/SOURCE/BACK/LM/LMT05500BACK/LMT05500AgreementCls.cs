using R_BackEnd;
using R_Common;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using PMT05500COMMON.DTO;
using PMT05500COMMON.Logs;


namespace PMT05500Back
{
    public class LMT05500AgreementCls
    {
        private LoggerLMT05500 _loggerLMT05500;
        private readonly ActivitySource _activitySource;

        public LMT05500AgreementCls()
        {
            _loggerLMT05500 = LoggerLMT05500.R_GetInstanceLogger();
            _activitySource = LMT05500Activity.R_GetInstanceActivitySource();
        }

        public List<LMT05500AgreementDTO> GetAgreementList(LMT05500DBParameter poParameter)
        {
            string? lcMethodName = nameof(GetAgreementList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<LMT05500AgreementDTO>? loReturn = null;
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
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                //CR21/08/2024
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 100, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_STATUS_LIST", DbType.String, 50, poParameter.CTRANS_STATUS_LIST);
                //CR 02OKt2024
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 50, poParameter.CPROGRAM_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMT05500AgreementDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT05500.LogError(loException);
            }

            if (loException.Haserror)

                loException.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        public List<LMT05500UnitDTO> GetDepositUnitList(LMT05500DBParameter poParameter)
        {
            string? lcMethodName = nameof(GetDepositUnitList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            
            R_Exception loException = new R_Exception();
            List<LMT05500UnitDTO>? loReturn = null;
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
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMT05500UnitDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT05500.LogError(loException);
            }

            if (loException.Haserror)
                loException.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        public List<LMT05500PropertyDTO> GetAllPropertyList(LMT05500DBParameter poParameter)
        {
            string lcMethodName = nameof(GetAllPropertyList);
             Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<LMT05500PropertyDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMT05500PropertyDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT05500.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMT05500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn;
        }

    }
}