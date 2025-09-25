using PMA00400Common.DTO;
using R_BackEnd;
using R_Common;
using R_Storage;
using R_StorageCommon;
using System.Data.Common;
using System.Data;
using log4net;

namespace PMA00400HandoverStorageBack
{
    public class PMA00400HandoverStorageCls
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PMA00400HandoverStorageCls));
        private readonly RSP_PM_HANDOVER_NOTIFICATIONResources.Resources_Dummy_Class _oRSPHANDOVER = new();
        public R_ReadResult GetHandoverStorage(string pcConnectionName, string pcGUID)
        {
            string lcMethodName = nameof(GetHandoverStorage);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            R_ReadParameter loReadParameter;
            R_ReadResult loReadResult = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(pcConnectionName);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PMA00400_GET_HANDOVER_STORAGE_ID";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CGUID", DbType.String, 40, pcGUID);

                _logger!.Info(string.Format("Execute query on method {0}", lcQuery));
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                var loStorage = R_Utility.R_ConvertTo<GetFileHandoverDTO>(loReturnTemp).FirstOrDefault() == null 
                    ? new GetFileHandoverDTO() : R_Utility.R_ConvertTo<GetFileHandoverDTO>(loReturnTemp).FirstOrDefault()!;

                R_Exception loExceptionDt = new();
                try
                {
                    _logger!.Info(string.Format("Get PDF  {0} ", loStorage));

                    if (!string.IsNullOrEmpty(loStorage.CIMAGE_STORAGE_ID))
                    {

                        loReadParameter = new R_ReadParameter()
                        {
                            StorageId = loStorage.CIMAGE_STORAGE_ID
                        };

                        loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                        _logger!.Info(string.Format("PDF with storage {0} found ", loStorage));
                    }
                }
                catch (Exception exDt)
                {
                    _logger!.Info(string.Format("PDF with storage {0} NOT found ", loStorage));
                    loExceptionDt.Add(exDt);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            loException.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Cls", lcMethodName));
            return loReadResult;
        }
    }
}