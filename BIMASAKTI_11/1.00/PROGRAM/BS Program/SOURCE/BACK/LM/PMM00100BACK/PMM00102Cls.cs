using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using PMM00100COMMON.DTO_s.Helper;

namespace PMM00100BACK
{
    public class PMM00102Cls : R_BusinessObject<HoUtilBuildingMappingDTO>
    {
        //var & constructor
        RSP_PM_MAINTAIN_SYSTEM_PARAMETERResources.Resources_Dummy_Class _resSysParam = new();
        RSP_PM_MAINTAIN_BUILDING_UTILITIESResources.Resources_Dummy_Class _resBuildingUtilMapping = new();
        private LoggerPMM00100 _logger;
        private readonly ActivitySource _activitySource;
        public PMM00102Cls()
        {
            _logger = LoggerPMM00100.R_GetInstanceLogger();
            _activitySource = PMM00100Activity.R_GetInstanceActivitySource();
        }

        //methods
        protected override HoUtilBuildingMappingDTO R_Display(HoUtilBuildingMappingDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            HoUtilBuildingMappingDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_BUILDING_UTILITIES";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, int.MaxValue, poEntity.CBUILDING_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<HoUtilBuildingMappingDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        protected override void R_Saving(HoUtilBuildingMappingDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "RSP_PM_MAINTAIN_BUILDING_UTILITIES";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@LALL_BUILDING", DbType.Boolean, 1, poNewEntity.LALL_BUILDING);
                loDb.R_AddCommandParameter(loCmd, "@LELECTRICITY", DbType.Boolean, 1, poNewEntity.LELECTRICITY);
                loDb.R_AddCommandParameter(loCmd, "@CELECTRICITY_CHARGES_ID", DbType.String, 20, poNewEntity.CELECTRICITY_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CELECTRICITY_TAX_ID", DbType.String, 20, poNewEntity.CELECTRICITY_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LCHILLER", DbType.Boolean, 1, poNewEntity.LCHILLER);
                loDb.R_AddCommandParameter(loCmd, "@CCHILLER_CHARGES_ID", DbType.String, 20, poNewEntity.CCHILLER_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHILLER_TAX_ID", DbType.String, 20, poNewEntity.CCHILLER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LGAS", DbType.Boolean, 1, poNewEntity.LGAS);
                loDb.R_AddCommandParameter(loCmd, "@CGAS_CHARGES_ID", DbType.String, 20, poNewEntity.CGAS_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CGAS_TAX_ID", DbType.String, 20, poNewEntity.CGAS_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LWATER", DbType.Boolean, 1, poNewEntity.LWATER);
                loDb.R_AddCommandParameter(loCmd, "@CWATER_CHARGES_ID", DbType.String, 20, poNewEntity.CWATER_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CWATER_TAX_ID", DbType.String, 20, poNewEntity.CWATER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 20, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    ShowLogError(loEx);
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
            }

            loEx.ThrowExceptionIfErrors();
        }
        protected override void R_Deleting(HoUtilBuildingMappingDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public List<HoUtilBuildingMappingDTO> GetBuildingList(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<HoUtilBuildingMappingDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_BUILDING_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loRtn = R_Utility.R_ConvertTo<HoUtilBuildingMappingDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        //helper
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
