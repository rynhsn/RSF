using PMM10000COMMON.Logs;
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
using PMM10000COMMON.UtilityDTO;

namespace PMM10000BACK
{//PMM10000PricelistDTO
    public class PMM10000CallTypePricelistCls
    {
        private LoggerPMM10000 _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_ASSIGN_PRICELISTResources.Resources_Dummy_Class _objectRSP = new();
        private readonly RSP_PM_UNASSIGN_PRICELISTResources.Resources_Dummy_Class _objectRSPUnassign = new();

        public PMM10000CallTypePricelistCls()
        {
            _logger = LoggerPMM10000.R_GetInstanceLogger();
            _activitySource = PMM10000Activity.R_GetInstanceActivitySource();
        }

        public void AssignPricelist(PMM10000DbParameterDTO poParameter)
        {
            string lcMethodName = nameof(AssignPricelist);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_PM_ASSIGN_PRICELIST";
                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCALL_TYPE_ID", DbType.String, 20, poParameter.CCALL_TYPE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPRICELIST_ID", DbType.String, int.MaxValue, poParameter.CPRICELIST_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID); ;

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
        public void UnassignPricelist(PMM10000DbParameterDTO poParameter)
        {
            string lcMethodName = nameof(AssignPricelist);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_PM_UNASSIGN_PRICELIST ";
                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCALL_TYPE_ID", DbType.String, 20, poParameter.CCALL_TYPE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPRICELIST_ID", DbType.String, int.MaxValue, poParameter.CPRICELIST_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID); ;

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
    }
}
