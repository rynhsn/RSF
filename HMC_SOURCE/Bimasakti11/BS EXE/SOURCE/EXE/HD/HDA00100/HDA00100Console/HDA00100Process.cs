using log4net;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Globalization;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace HDA00100Console
{
    public class HDA00100Process
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(HDA00100Process));
        public async Task Process_HD_SCHEDULER_ACTION(string ConnectionString)
        {
            string? lcMethodName = nameof(Process_HD_SCHEDULER_ACTION);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new();
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(ConnectionString);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_HD_SCHEDULER_ACTION";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                _logger!.Info(string.Format("Execute query on method {0}", lcMethodName));
                _logger!.Debug(string.Format("Execute query {0} ", lcQuery));
                loDb.SqlExecNonQuery(loConn, loCommand, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            if (loException.Haserror)
            {
                _logger.Error(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
        }
    }
}
