using PMM10000COMMON.Logs;
using PMM10000COMMON.SLA_Call_Type;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000BACK
{
    public class PMM10000CallTypeCls : R_BusinessObject<PMM10000SLACallTypeDTO>
    {
        private  LoggerPMM10000 _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_MAINTAIN_SLA_CALL_TYPEResources.Resources_Dummy_Class _objectRSP = new();

        public PMM10000CallTypeCls()
        {
            _logger = LoggerPMM10000.R_GetInstanceLogger();
            _activitySource = PMM10000Activity.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(PMM10000SLACallTypeDTO poEntity)
        {
            string lcMethodName = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string? lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string? lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcAction = "DELETE";

                lcQuery = "RSP_PM_MAINTAIN_SLA_CALL_TYPE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCALL_TYPE_ID", DbType.String, 20, poEntity.CCALL_TYPE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCALL_TYPE_NAME", DbType.String, 200, poEntity.CCALL_TYPE_NAME);

                loDb.R_AddCommandParameter(loCommand, "@CCATEGORY_ID", DbType.String, 20, poEntity.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, 20, poEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IHOURS", DbType.Int32, 20, poEntity.IHOURS);
                loDb.R_AddCommandParameter(loCommand, "@IMINUTES", DbType.Int32, 20, poEntity.IMINUTES);

                loDb.R_AddCommandParameter(loCommand, "@LPRIORITY", DbType.Boolean, 2, poEntity.LPRIORITY);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 20, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            loException.ThrowExceptionIfErrors();
        }

        protected override PMM10000SLACallTypeDTO R_Display(PMM10000SLACallTypeDTO poEntity)
        {
            string lcMethodName = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            PMM10000SLACallTypeDTO? loReturn = new PMM10000SLACallTypeDTO();
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db loDb;
            string lcQuery;
            try
            {

                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_SLA_CALL_TYPE_LIST";
                loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCALL_TYPE_ID", DbType.String, 20, poEntity.CCALL_TYPE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMM10000SLACallTypeDTO>(loProfileDataTable).FirstOrDefault()!;
                _logger.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethodName));
            return loReturn!;
        }
    

        protected override void R_Saving(PMM10000SLACallTypeDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string? lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
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
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }
                lcQuery = "RSP_PM_MAINTAIN_SLA_CALL_TYPE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCALL_TYPE_ID", DbType.String, 20, poNewEntity.CCALL_TYPE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCALL_TYPE_NAME", DbType.String, 200, poNewEntity.CCALL_TYPE_NAME);

                loDb.R_AddCommandParameter(loCommand, "@CCATEGORY_ID", DbType.String, 20, poNewEntity.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, 20, poNewEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IHOURS", DbType.Int32, 20, poNewEntity.IHOURS);
                loDb.R_AddCommandParameter(loCommand, "@IMINUTES", DbType.Int32, 20, poNewEntity.IMINUTES);

                loDb.R_AddCommandParameter(loCommand, "@LPRIORITY", DbType.Boolean, 2, poNewEntity.LPRIORITY);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 20, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    var loEntity = R_Utility.R_ConvertTo<PMM10000SLACallTypeDTO>(loDataTable).FirstOrDefault()!;

                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            loException.ThrowExceptionIfErrors();
        }
    }
}
