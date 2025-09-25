using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace PMM00100BACK
{
    public class PMM00101Cls : R_BusinessObject<SystemParamDetailDTO>
    {
        RSP_PM_MAINTAIN_SYSTEM_PARAMETERResources.Resources_Dummy_Class _resSysParam = new();
        RSP_PM_MAINTAIN_BUILDING_UTILITIESResources.Resources_Dummy_Class _resBuildingUtilMapping = new();

        private LoggerPMM00100 _logger;

        private readonly ActivitySource _activitySource;

        public PMM00101Cls()
        {
            _logger = LoggerPMM00100.R_GetInstanceLogger();
            _activitySource = PMM00100Activity.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(SystemParamDetailDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override SystemParamDetailDTO R_Display(SystemParamDetailDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            SystemParamDetailDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_SYSTEM_PARAMETER_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<SystemParamDetailDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(SystemParamDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd = null;
            DbConnection loConn = null;

            try
            {
                loDb = new R_Db();
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_PM_MAINTAIN_SYSTEM_PARAMETER";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                poNewEntity.CACTION = poCRUDMode switch
                {
                    eCRUDMode.AddMode => "ADD",
                    eCRUDMode.EditMode => "EDIT",
                    _ => "",
                };
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSOFT_PERIOD", DbType.String, 6, poNewEntity.CSOFT_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PERIOD", DbType.String, 6, poNewEntity.CCURRENT_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CINV_PRD", DbType.String, 6, poNewEntity.CINV_PRD);
                loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.Boolean, 1, poNewEntity.LGLLINK);
                loDb.R_AddCommandParameter(loCmd, "@LINV_PROCESS_FLAG", DbType.Boolean, 1, poNewEntity.LINV_PROCESS_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_MODE", DbType.String, 2, poNewEntity.CINVOICE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CWHT_MODE", DbType.String, 2, poNewEntity.CWHT_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CCUR_RATE_TYPE_CODE", DbType.String, 20, poNewEntity.CCUR_RATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_RATE_TYPE_CODE", DbType.String, 20, poNewEntity.CTAX_RATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@COVER_RECEIPT", DbType.String, 20, poNewEntity.COVER_RECEIPT);
                loDb.R_AddCommandParameter(loCmd, "@NOVER_TOLERANCE_AMOUNT", DbType.Decimal, 10, poNewEntity.NOVER_TOLERANCE_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CLESS_RECEIPT", DbType.String, 20, poNewEntity.CLESS_RECEIPT);
                loDb.R_AddCommandParameter(loCmd, "@NLESS_TOLERANCE_AMOUNT", DbType.Decimal, 10, poNewEntity.NLESS_TOLERANCE_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CELECTRIC_PERIOD", DbType.String, 6, poNewEntity.CELECTRIC_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CWATER_PERIOD", DbType.String, 6, poNewEntity.CWATER_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CGAS_PERIOD", DbType.String, int.MaxValue, poNewEntity.CGAS_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@LELECTRIC_END_MONTH", DbType.Boolean, 1, poNewEntity.LELECTRIC_END_MONTH);
                loDb.R_AddCommandParameter(loCmd, "@LWATER_END_MONTH", DbType.Boolean, 1, poNewEntity.LWATER_END_MONTH);
                loDb.R_AddCommandParameter(loCmd, "@LGAS_END_MONTH", DbType.Boolean, 1, poNewEntity.LGAS_END_MONTH);
                loDb.R_AddCommandParameter(loCmd, "@CELECTRIC_DATE", DbType.String, 2, poNewEntity.CELECTRIC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CWATER_DATE", DbType.String, 2, poNewEntity.CWATER_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CGAS_DATE", DbType.String, 2, poNewEntity.CGAS_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LALL_BUILDING", DbType.Boolean, 1, poNewEntity.LALL_BUILDING);
                loDb.R_AddCommandParameter(loCmd, "@IMAX_DAYS", DbType.Int32, 4, poNewEntity.IMAX_DAYS);
                loDb.R_AddCommandParameter(loCmd, "@IMAX_ATTEMPTS", DbType.Int32, 4, poNewEntity.IMAX_ATTEMPTS);
                loDb.R_AddCommandParameter(loCmd, "@CCALL_TYPE_ID", DbType.String, 20, poNewEntity.CCALL_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@LPRIORITY", DbType.Boolean, 1, poNewEntity.LPRIORITY);
                loDb.R_AddCommandParameter(loCmd, "@LDISTRIBUTE_PDF", DbType.Boolean, 1, poNewEntity.LDISTRIBUTE_PDF);
                loDb.R_AddCommandParameter(loCmd, "@LINCLUDE_IMAGE", DbType.Boolean, 1, poNewEntity.LINCLUDE_IMAGE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
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
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }

        //helper method
        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }
    }
}
