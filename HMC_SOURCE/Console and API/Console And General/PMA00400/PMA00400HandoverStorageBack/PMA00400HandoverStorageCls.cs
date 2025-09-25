using PMA00400Common.DTO;
using R_BackEnd;
using R_Common;
using R_Storage;
using R_StorageCommon;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using PMA00400Logger;

namespace PMA00400HandoverStorageBack
{
    public class PMA00400HandoverStorageCls
    {
        private readonly ActivitySource _activitySource;
        private APILogger _logger;
        private readonly RSP_PM_HANDOVER_NOTIFICATIONResources.Resources_Dummy_Class _oRSPHANDOVER = new();
        
        public PMA00400HandoverStorageCls()
        {
            _logger = APILogger.R_GetInstanceLogger();
            _activitySource = PMA00400Activity.R_GetInstanceActivitySource();
        }
        public async Task<R_ReadResult> GetHandoverStorage(string pcConnectionName, string pcGUID)
        {
            using Activity activity = _activitySource.StartActivity("Back GetHandoverStorage");
            string lcMethodName = nameof(GetHandoverStorage);
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            DbCommand loCommand;
            R_Db loDb = null;
            DbConnection loConn = null;
            R_ReadParameter loReadParameter;
            R_ReadResult loReadResult = null;
            try
            {
                loDb = new R_Db();
                loConn = await loDb.GetConnectionAsync(pcConnectionName);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_PMA00400_GET_HANDOVER_STORAGE_ID";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CGUID", DbType.String, 40, pcGUID);

                _logger!.LogInfo(string.Format("Execute query on method {0}", lcQuery));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                if (loReturnTemp.Rows.Count <= 0) 
                {
                    _logger!.LogInfo(string.Format("PDF with GUID {0} NOT found ", pcGUID));
                    loException.Add("001", string.Format("PDF with GUID {0} NOT found ", pcGUID));
                    goto ExitTry;
                }
                var loStorage = R_Utility.R_ConvertTo<GetFileHandoverDTO>(loReturnTemp).FirstOrDefault() == null 
                    ? new GetFileHandoverDTO() : R_Utility.R_ConvertTo<GetFileHandoverDTO>(loReturnTemp).FirstOrDefault()!;

                R_Exception loExceptionDt = new();
                try
                {
                    _logger!.LogInfo(string.Format("Get PDF  {0} ", loStorage));

                    if (!string.IsNullOrEmpty(loStorage.CIMAGE_STORAGE_ID))
                    {

                        loReadParameter = new R_ReadParameter()
                        {
                            StorageId = loStorage.CIMAGE_STORAGE_ID
                        };

                        loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                        _logger!.LogInfo(string.Format("PDF with storage {0} found ", loStorage.CIMAGE_STORAGE_ID));
                    }
                    else
                    {
                        _logger!.LogInfo(string.Format("PDF with GUID {0} NOT found ", pcGUID));
                        loExceptionDt.Add("001", string.Format("PDF with GUID {0} NOT found ", pcGUID));
                    }
                }
                catch (Exception exDt)
                {
                    _logger!.LogInfo(string.Format("PDF with GUID {0} NOT found ", pcGUID));
                    loExceptionDt.Add(exDt);
                }
                loExceptionDt.ThrowExceptionIfErrors();
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
            ExitTry:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loReadResult;
        }
    }
}