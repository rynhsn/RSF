
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using GSM04500Common;
using System.Reflection.Metadata;
using GSM04500Common.Logs;
using System.Diagnostics;

namespace GSM04500Back
{
    public  class GSM04500Cls : R_BusinessObject<GSM04500DTO>
    {
        RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT_DEPTResources.Resources_Dummy_Class _loRSPGroupAcc = new();
        RSP_GS_MAINTAIN_JOURNAL_GROUPResources.Resources_Dummy_Class _loRSPGroup = new();
        RSP_GS_UPLOAD_JOURNAL_GROUPResources.Resources_Dummy_Class _loRSPUpload = new();

        private LoggerGSM04500 _loggerGSM04500;
        private readonly ActivitySource _activitySource;
        public GSM04500Cls()
        {
            //Initial and Get Logger
            _loggerGSM04500 = LoggerGSM04500.R_GetInstanceLogger();
            _activitySource = GSM04500Activity.R_GetInstanceActivitySource();
        }
        protected override void R_Deleting(GSM04500DTO poEntity)
        {
            string lcMethodName = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loException = new R_Exception();
            DbCommand loCommand;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                string lcAction = "DELETE";

                var lcQuery = "RSP_GS_MAINTAIN_JOURNAL_GROUP";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                //loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE", DbType.String, 8, poEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_NAME", DbType.String, 80, poEntity.CJRNGRP_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LACCRUAL", DbType.Boolean, 25, poEntity.LACCRUAL);

                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _loggerGSM04500.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override GSM04500DTO R_Display(GSM04500DTO poEntity)
        {
            string lcMethodName = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            GSM04500DTO? loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = @"RSP_GS_GET_JOURNAL_GRP_DT";

                loDb = new R_Db();
                var loCommand = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 100, poEntity.CPROPERTY_ID??"");
                loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE ", DbType.String, 20, poEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_LOGIN_ID", DbType.String, 8, poEntity.CUSER_ID);
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM04500DTO>(loReturnTemp).ToList().FirstOrDefault()!;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn!;
        }

        protected override void R_Saving(GSM04500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string? lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection? loConn = null;
            string? lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                    case eCRUDMode.NormalMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                    default:
                        break;
                }
                lcQuery = "RSP_GS_MAINTAIN_JOURNAL_GROUP";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;


                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                //loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_TYPE", DbType.String, 2, poNewEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE", DbType.String, 8, poNewEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_NAME", DbType.String, 80, poNewEntity.CJRNGRP_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LACCRUAL", DbType.Boolean, 25, poNewEntity.LACCRUAL);

                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _loggerGSM04500.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }

        public List<GSM04500DTO> JOURNAL_GROUP_LIST(GSM04500DBParameter poParameter)
        {
            string lcMethodName = nameof(JOURNAL_GROUP_LIST);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<GSM04500DTO>? loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_JOURNAL_GRP_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 100, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poParameter.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_LOGIN_ID", DbType.String, 25, poParameter.CUSER_ID);
               
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<GSM04500DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            
            return loReturn!;
        }


        public List<GSM04500JournalGroupTypeDTO> GetAllJournalGroupTypeList(GSM04500DBParameter poParameter)
        {
            string lcMethodName = nameof(GetAllJournalGroupTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<GSM04500JournalGroupTypeDTO>? loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', @CCOMPANY_ID, '_BS_JOURNAL_GRP_TYPE', '', @LANGUAGE_ID)";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;


                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@LANGUAGE_ID", DbType.String, 5, poParameter.LANGUAGE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM04500JournalGroupTypeDTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn!;
        }
    }
}