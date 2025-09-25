using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using GST00500Common;
using GST00500Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_GS_MAINTAIN_APPROVALResources;


namespace GST00500Back
{
    public class GST00500Cls : R_IServiceCRUDBase<GST00500DTO>
    {
        Resources_Dummy_Class _loRSP = new();

        private LoggerGST00500 _loggerGST00500;
        private readonly ActivitySource _activitySource;
        public GST00500Cls()
        {
            //Initial and Get Logger
            _loggerGST00500 = LoggerGST00500.R_GetInstanceLogger();
            _activitySource = GST00500Activity.R_GetInstanceActivitySource();
        }
        public R_ServiceGetRecordResultDTO<GST00500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceSaveResultDTO<GST00500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        public List<GST00500DTO> Approval_Inbox_List(GST00500DBParameter poEntity)
        {
            string lcMethodName = nameof(Approval_Inbox_List);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<GST00500DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbCommand loCommand;
            DbConnection loConnection = null;
            string lcQuery;

            try
            {
                loCommand = loDb.GetCommand();
                loConnection = loDb.GetConnection();

                lcQuery = @"RSP_GS_GET_APPR_TRX_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANYID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_LOGIN_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_TYPE", DbType.String, 2, poEntity.CTRANS_TYPE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGST00500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConnection, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GST00500DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGST00500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        public GST00500UserNameDTO GetUserName(GST00500DBParameter poEntity)
        {
            string lcMethodName = nameof(GetUserName);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            var loResult = new GST00500UserNameDTO();
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                 loCommand = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_USER_DETAIL";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANYID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 20, "GST00500");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPARAMETER", DbType.String, 100, "");
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGST00500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loResultTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult.CUSER_NAME = loResultTemp.Rows[0]["CUSER_NAME"].ToString();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGST00500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            loEx.ThrowExceptionIfErrors();
            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }

        public List<GST00500RejectDTO> GetReasonRejectList(GST00500DBParameter poParameter)
        {
            string lcMethodName = nameof(GetReasonRejectList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            var loResult = new List<GST00500RejectDTO>();
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCommand = loDb.GetCommand();

                var lcQuery = $"SELECT * FROM RFT_GET_GSB_CODE_INFO('SIAPP', " +
                              $"'{poParameter.CCOMPANYID}', '_GS_REJECTTRANS_REASON', '', " +
                              $"'{poParameter.CLANGUAGE_ID}') ";

                
                _loggerGST00500.LogDebug("{@ObjectQuery} ", lcQuery);
                loResult = loDb.SqlExecObjectQuery<GST00500RejectDTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGST00500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }

    }
}
