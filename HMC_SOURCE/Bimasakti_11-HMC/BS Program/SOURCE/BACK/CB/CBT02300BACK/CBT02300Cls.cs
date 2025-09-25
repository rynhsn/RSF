using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBT02300COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Reflection.Metadata;

namespace CBT02300BACK
{
    public class CBT02300Cls : R_BusinessObject<CBT02300ChequeInfoDTO>
    {
        private LoggerCBT02300? _loggerCBT02300;

        private readonly ActivitySource _activitySource;
        public CBT02300Cls()
        {
            _loggerCBT02300 = LoggerCBT02300.R_GetInstanceLogger();
            _activitySource = CBT02300Activity.R_GetInstanceActivitySource();
        }
        protected override CBT02300ChequeInfoDTO R_Display(CBT02300ChequeInfoDTO poEntity)
        {
            string lcMethodName = nameof(R_Display);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethodName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            R_Exception loException = new R_Exception();
            CBT02300ChequeInfoDTO? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_CB_GET_CHEQUE_HD";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poEntity.CLANGUAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerCBT02300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<CBT02300ChequeInfoDTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerCBT02300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerCBT02300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override void R_Saving(CBT02300ChequeInfoDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(CBT02300ChequeInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public List<CBT02300BankInChequeDTO> BankInChequeList(CBT02300DBFilterListParamDTO poParameter)
        {
            string lcMethodName = nameof(BankInChequeList);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethodName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            R_Exception loException = new R_Exception();
            List<CBT02300BankInChequeDTO>? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_CB_GET_BANK_IN_CHEQUE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                loDb.R_AddCommandParameter(loCommand, "@CTRANS_TYPE", DbType.String, 20, poParameter.CTRANS_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCB_CODE", DbType.String, 8, poParameter.CCB_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCB_ACCOUNT_NO", DbType.String, 20, poParameter.CCB_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDATE", DbType.String, 8, poParameter.CDATE);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerCBT02300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<CBT02300BankInChequeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerCBT02300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerCBT02300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public CBT02300ChequeInfoDTO BankInChequeInfo(CBT02300DBParamDetailDTO poParameter)
        {
            string lcMethodName = nameof(BankInChequeInfo);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethodName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            R_Exception loException = new R_Exception();
            CBT02300ChequeInfoDTO? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_CB_GET_CHEQUE_HD";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerCBT02300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<CBT02300ChequeInfoDTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerCBT02300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerCBT02300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
