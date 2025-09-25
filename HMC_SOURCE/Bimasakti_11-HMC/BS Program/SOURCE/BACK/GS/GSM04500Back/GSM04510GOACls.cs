using GSM04500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Windows.Input;
using GSM04500Common.Logs;
using System.Diagnostics;

namespace GSM04500Back
{
    public class GSM04510GOACls : R_BusinessObject<GSM04510GOADTO>
    {
        RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT_DEPTResources.Resources_Dummy_Class _loRSPGroupAcc = new();
        RSP_GS_MAINTAIN_JOURNAL_GROUPResources.Resources_Dummy_Class _loRSPGroup = new();
        RSP_GS_UPLOAD_JOURNAL_GROUPResources.Resources_Dummy_Class _loRSPUpload = new();

        private LoggerGSM04500 _loggerGSM04500;
        private readonly ActivitySource _activitySource;
        public GSM04510GOACls()
        {
            //Initial and Get Logger
            _loggerGSM04500 = LoggerGSM04500.R_GetInstanceLogger();
            _activitySource = GSM04500Activity.R_GetInstanceActivitySource();
        }
        protected override void R_Deleting(GSM04510GOADTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override GSM04510GOADTO R_Display(GSM04510GOADTO poEntity)
        {
            string lcMethodName = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            GSM04510GOADTO loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_GS_GET_JOURNAL_GRP_GOA_DETAIL";
                loDb = new R_Db();
                var loCmd = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 30, poEntity.CGOA_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poEntity.CUSER_ID);
                
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM04510GOADTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn;
        }

        protected override void R_Saving(GSM04510GOADTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();

                lcAction = "EDIT";
                lcQuery = @"RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poNewEntity.CCOMPANY_ID);
               // loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 100, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_TYPE", DbType.String, 2, poNewEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE", DbType.String, 30, poNewEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CGOA_CODE", DbType.String, 30, poNewEntity.CGOA_CODE);
                loDb.R_AddCommandParameter(loCommand, "@LLDEPARTMENT_MODE", DbType.Boolean, 2, poNewEntity.LDEPARTMENT_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CGLACCOUNT_NO", DbType.String, 20, poNewEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poNewEntity.CUSER_ID);
               
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
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public List<GSM04510GOADTO> JOURNAL_GROUP_GOA_LIST(GSM04510GOADBParameter poParameter)
        {
            string lcMethodName = nameof(JOURNAL_GROUP_GOA_LIST);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<GSM04510GOADTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_JOURNAL_GRP_GOA_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 100, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poParameter.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poParameter.CJOURNAL_GRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_LOGIN_ID", DbType.String, 25, poParameter.CUSER_ID);
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM04510GOADTO>(loReturnTemp).ToList();

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
