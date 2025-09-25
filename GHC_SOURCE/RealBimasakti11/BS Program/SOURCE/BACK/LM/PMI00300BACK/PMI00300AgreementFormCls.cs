using PMI00300BACK.OpenTelemetry;
using PMI00300COMMON.DTO;
using PMI00300COMMON.Loggers;
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
using R_StorageCommon;
using R_Storage;
using System.Reflection;

namespace PMI00300BACK
{
    public class PMI00300AgreementFormCls
    {
        private LoggerPMI00300AgreementForm _logger;
        private readonly ActivitySource _activitySource;

        public PMI00300AgreementFormCls()
        {
            _logger = LoggerPMI00300AgreementForm.R_GetInstanceLogger();
            _activitySource = PMI00300AgreementFormActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMI00300GetAgreementFormListDTO> GetAgreementFormListDb(PMI00300GetAgreementFormListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementFormListDb");
            R_Exception loEx = new R_Exception();
            List<PMI00300GetAgreementFormListDTO> loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_PM_GET_UNIT_AGREE_FORM_LIST";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 8, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 2, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@IPERIOD_YEAR", DbType.Int32, 20, poParameter.IPERIOD_YEAR);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_UNIT_AGREE_FORM_LIST {@Parameters} || GetAgreementFormListDb(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300GetAgreementFormListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMI00300HandOverChecklistDTO> GetList_HandOverChecklist(PMI00300HandOverChecklistParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMI00300HandOverChecklistDTO> loRtn = null;
            R_ReadParameter loReadParameter;
            R_ReadResult loReadResult = null;
            R_Db loDb = new();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_PM_GET_HANDOVER_CHECKLIST_LIST";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CHO_DEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CHO_TRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CHO_REF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                ShowLogDebug(loCommand.CommandText, loCommand.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                loRtn = R_Utility.R_ConvertTo<PMI00300HandOverChecklistDTO>(loDataTable).ToList();
                foreach (var item in loRtn)
                {
                    ReadAndAssignImage(item, "CIMAGE_STORAGE_ID_01", "OIMAGE_STORAGE_ID_01", loConn);
                    ReadAndAssignImage(item, "CIMAGE_STORAGE_ID_02", "OIMAGE_STORAGE_ID_02", loConn);
                    ReadAndAssignImage(item, "CIMAGE_STORAGE_ID_03", "OIMAGE_STORAGE_ID_03", loConn);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public PMI00300GetPeriodYearRangeDTO GetPeriodYearRangeDb(PMI00300GetPeriodYearRangeParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetPeriodYearRangeDb");
            R_Exception loException = new R_Exception();

            PMI00300GetPeriodYearRangeDTO loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 4, "");
                loDb.R_AddCommandParameter(loCommand, "@CMODE", DbType.String, 10, "");

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@Parameters} || GetPeriodYearRangeDb(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300GetPeriodYearRangeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        //helper - call image
        private void ReadAndAssignImage(dynamic poItem, string pcImageField, string poImageField, DbConnection poConn)
        {
            var cImageStorageId = poItem.GetType().GetProperty(pcImageField).GetValue(poItem, null) as string;

            if (!string.IsNullOrEmpty(cImageStorageId))
            {
                var loReadParameter = new R_ReadParameter()
                {
                    StorageId = cImageStorageId
                };

                var loReadResult = R_StorageUtility.ReadFile(loReadParameter, poConn);

                poItem.GetType().GetProperty(poImageField).SetValue(poItem, loReadResult.Data);
            }
        }

        //helper - showdebug
        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }
    }
}
