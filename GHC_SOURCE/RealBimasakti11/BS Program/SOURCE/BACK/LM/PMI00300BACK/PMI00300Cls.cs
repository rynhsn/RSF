using PMI00300BACK.OpenTelemetry;
using PMI00300COMMON.DTO;
using PMI00300COMMON.Loggers;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMI00300BACK
{
    public class PMI00300Cls
    {
        private LoggerPMI00300 _logger;
        private readonly ActivitySource _activitySource;
        public PMI00300Cls()
        {
            _logger = LoggerPMI00300.R_GetInstanceLogger();
            _activitySource = PMI00300ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMI00300GetPropertyListDTO> GetPropertyListDb(PMI00300GetPropertyListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            R_Exception loEx = new R_Exception();
            List<PMI00300GetPropertyListDTO> loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@Parameters} || GetPropertyList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300GetPropertyListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMI00300DTO> GetUnitInquiryHeaderListDb (PMI00300ParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInquiryHeaderListDb");
            R_Exception loEx = new R_Exception();
            List<PMI00300DTO> loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_PM_GET_UNIT_INQUIRY_HD";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_OPTION", DbType.String, 2, poParameter.CUNIT_OPTION);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@LALL_BUILDING", DbType.Boolean, 8, poParameter.LALL_BUILDING);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_FLOOR_ID", DbType.String, 20, poParameter.CFROM_FLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTO_FLOOR_ID", DbType.String, 20, poParameter.CTO_FLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_CATEGORY", DbType.String, 2, poParameter.CUNIT_CATEGORY);
                loDb.R_AddCommandParameter(loCommand, "@CSTATUS_ID", DbType.String, 2, poParameter.CSTATUS_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 8, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_UNIT_INQUIRY_HD {@Parameters} || GetUnitInquiryHeaderListDb(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMI00300DetailLeftDTO> GetUnitInquiryDetailLeftListDb(PMI00300DetailParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInquiryDetailLeftListDb");
            R_Exception loEx = new R_Exception();
            List<PMI00300DetailLeftDTO> loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_PM_GET_UNIT_INQUIRY_DT ";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_OPTION", DbType.String, 2, poParameter.CUNIT_OPTION);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CGRID_TYPE", DbType.String, 1, "1");
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 8, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@CAGREEMENT_NO", DbType.String, 20, poParameter.CAGREEMENT_NO);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_UNIT_INQUIRY_DT {@Parameters} || GetUnitInquiryDetailLeftListDb(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300DetailLeftDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMI00300DetailRightDTO> GetUnitInquiryDetailRightListDb(PMI00300DetailParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInquiryDetailRightListDb");
            R_Exception loEx = new R_Exception();
            List<PMI00300DetailRightDTO> loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_PM_GET_FACILITY_UNIT_INQUIRY";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, int.MaxValue, poParameter.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 8, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_FACILITY_UNIT_INQUIRY {@Parameters} || GetUnitInquiryDetailRightListDb(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300DetailRightDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMI00300GetGSBCodeListDTO> GetGSBCodeListDb(PMI00300GetGSBCodeListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetGSBCodeListDb");
            R_Exception loEx = new R_Exception();
            List<PMI00300GetGSBCodeListDTO> loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_GS_GET_GSB_CODE_LIST";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CAPPLICATION", DbType.String, 20, poParameter.CAPPLICATION);
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCLASS_ID", DbType.String, 40, poParameter.CCLASS_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_GSB_CODE_LIST {@Parameters} || GetGSBCodeListDb(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300GetGSBCodeListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMI00300GetLSStatusListDTO> GetLSStatusListDb(PMI00300GetLSStatusListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetLSStatusListDb");
            R_Exception loEx = new R_Exception();
            List<PMI00300GetLSStatusListDTO> loRtn = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;

            try
            {
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                loCommand.CommandText = "RSP_PMI00300_GET_LS_STATUS_LIST";
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@STATUS_TYPE", DbType.String, 1, poParameter.STATUS_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PMI00300_GET_LS_STATUS_LIST {@Parameters} || GetLSStatusListDb(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loRtn = R_Utility.R_ConvertTo<PMI00300GetLSStatusListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
