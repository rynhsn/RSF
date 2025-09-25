using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;

namespace HDA00100Console
{
    public class HDA00100Process
    {
        private ConsoleLogger _logger;
        public HDA00100Process()
        {
            _logger = ConsoleLogger.R_GetInstanceLogger();
        }
        public async Task Process_HD_SCHEDULER_ACTION(string ConnectionString)
        {
            string? lcMethodName = nameof(Process_HD_SCHEDULER_ACTION);
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new();
            DbCommand loCommand=null;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_HD_SCHEDULER_ACTION";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                _logger!.LogInfo(string.Format("Execute query on method {0}", lcMethodName));
                _logger!.LogDebug(string.Format("Execute query {0} ", lcQuery));
               await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.LogError(string.Format("Log Error {0} ", ex));
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            if (loException.Haserror)
            {
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }
    }
}
