using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;
using PMB04000COMMON.Logs;
using PMB04000COMMON.Print;
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
using PMB04000COMMONPrintBatch;
using PMB04000COMMONPrintBatch.ParamDTO;
using PMB04000COMMON.Print.Distribute;
using R_Storage;
using R_StorageCommon;

namespace PMB04000BACK
{
    public class PMB04000PrintCls
    {
        private readonly LoggerPMB04000? _logger;
        private readonly ActivitySource _activitySource;
        RSP_PM_SEND_RECEIPTResources.Resources_Dummy_Class _RSPSendReceipt = new();

        public PMB04000PrintCls()
        {
            _logger = LoggerPMB04000.R_GetInstanceLogger();
            _activitySource = PMB04000Activity.R_GetInstanceActivitySource();
        }

        public List<PMB04000DataReportDTO> GetReportReceiptData(PMB04000ParamReportDTO poParameter)
        {
            string? lcMethodName = nameof(GetReportReceiptData);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMB04000DataReportDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                ;
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "RSP_PM_PRINT_RECEIPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, int.MaxValue, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@LPRINT ", DbType.Boolean, 2, poParameter.LPRINT);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMB04000DataReportDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }

            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMB04000DataReportDTO> GetReportReceiptDataPrintBatch(PMB04000ParamReportDTO poParameter,
            DbConnection poConn)
        {
            string? lcMethodName = nameof(GetReportReceiptDataPrintBatch);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<PMB04000DataReportDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = poConn;
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "RSP_PM_PRINT_RECEIPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, int.MaxValue, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@LPRINT ", DbType.Boolean, 2, poParameter.LPRINT);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                loReturn = R_Utility.R_ConvertTo<PMB04000DataReportDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }

            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;
        }

        public PMB04000BaseHeaderDTO GetLogoCompany(PMB04000ParamReportDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetLogoCompany")!;
            var loEx = new R_Exception();
            PMB04000BaseHeaderDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();
                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poParameter.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger!.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMB04000BaseHeaderDTO>(loDataTable).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult!;
        }

        public List<PMB04000SendReceiptDTO> SendReceiptCls(PMB04000ParamReportDTO poParameter,
            DbConnection poConnection)
        {
            string? lcMethodName = nameof(SendReceiptCls);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            DbConnection? loConn = null;

            R_Exception loException = new();
            List<PMB04000SendReceiptDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand = null;
            R_Db loDb;
            try
            {
                loDb = new();
                loConn = poConnection;
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "RSP_PM_SEND_RECEIPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, int.MaxValue, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@LDISTRIBUTE", DbType.Boolean, 3, poParameter.LDISTRIBUTE);
                loDb.R_AddCommandParameter(loCommand, "@CSTORAGE_ID", DbType.String, 40, poParameter.CSTORAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    loReturn = R_Utility.R_ConvertTo<PMB04000SendReceiptDTO>(loDataTable).ToList();
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
            }

            if (loException.Haserror)
            {
                _logger.LogError("Exception details: " + loException.ToString());
            }

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo("End GetSendReminderList PMB04000");
            return loReturn!;
        }

        public void UpdateSendReceiptCls(PMB04000ParamReportDTO poParameter, DbConnection poConnection)
        {
            string? lcMethodName = nameof(UpdateSendReceiptCls);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new();
            List<PMB04000SendReceiptDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = poConnection;
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "RSP_PM_SEND_RECEIPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, int.MaxValue, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@LDISTRIBUTE", DbType.Boolean, 3, poParameter.LDISTRIBUTE);
                loDb.R_AddCommandParameter(loCommand, "@CSTORAGE_ID", DbType.String, 40, poParameter.CSTORAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    loReturn = R_Utility.R_ConvertTo<PMB04000SendReceiptDTO>(loDataTable).ToList();
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

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }

            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }

        public R_ReadResult GetSignStorage(string pcGUID)
        {
            var lcMethodName = nameof(GetSignStorage);
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            R_Db loDb = null;
            DbConnection loConn = null;
            R_ReadParameter loReadParameter;
            R_ReadResult loReadResult = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = pcGUID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                _logger!.LogInfo(string.Format("Image with storage {0} found ", loReadParameter.StorageId));
            }
            catch (Exception ex)
            {
                // _logger!.LogInfo(string.Format("Image with storage {0} NOT found ", loReadParameter.StorageId));
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
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loReadResult;
        }
    }
}