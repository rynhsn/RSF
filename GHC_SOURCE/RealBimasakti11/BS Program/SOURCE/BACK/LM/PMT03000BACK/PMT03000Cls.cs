using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using PMT03000COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMT03000BACK
{
    public class PMT03000Cls
    {
        private LoggerPMT03000 _logger;
        private readonly ActivitySource _activitySource;
        public PMT03000Cls()
        {
            _logger = LoggerPMT03000.R_GetInstanceLogger();
            _activitySource = PMT03000Activity.R_GetInstanceActivitySource();
        }

        //method
        public List<PropertyDTO> Getlist_Property(GeneralParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PropertyDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public List<BuildingDTO> Getlist_Building(GeneralParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<BuildingDTO> loRtn = null;
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

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<BuildingDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public List<BuildingUnitDTO> Getlist_BuildingUnit(BuildingDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<BuildingUnitDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_BUILDING_UNIT_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, int.MaxValue, poEntity.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, int.MaxValue, "");
                loDB.R_AddCommandParameter(loCmd, "@LAGREEMENT", DbType.Boolean, int.MaxValue, false);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, int.MaxValue, "");
                loDB.R_AddCommandParameter(loCmd, "@CLEASE_STATUS_LIST", DbType.String, int.MaxValue, "");
                loDB.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, int.MaxValue, PMT03000ContextConstant.CPROGRAM_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, "");
                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, int.MaxValue,"");
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_CATEGORY_LIST", DbType.String, int.MaxValue, "");
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<BuildingUnitDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public List<TransByUnitDTO> Getlist_TransByUnit(BuildingUnitDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<TransByUnitDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_TRX_BY_UNIT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, int.MaxValue, poEntity.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, int.MaxValue, poEntity.CFLOOR_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, int.MaxValue, poEntity.CUNIT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<TransByUnitDTO>(loRtnTemp).ToList();
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
