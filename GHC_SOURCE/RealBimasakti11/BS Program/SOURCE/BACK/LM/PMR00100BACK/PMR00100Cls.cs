using PMR00100Common.DTOs;
using PMR00100Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PMR00100Back
{
    public class PMR00100Cls : R_BusinessObject<PMR00100DTO>
    {
        private LoggerPMR00100 _loggerPMR00100;
        private readonly ActivitySource _activitySource;
        public PMR00100Cls()
        {
            _loggerPMR00100 = LoggerPMR00100.R_GetInstanceLogger();
            _activitySource = PMR00100Activity.R_GetInstanceActivitySource();
        }

        #region CRUD
        protected override PMR00100DTO R_Display(PMR00100DTO poEntity)
        {
            throw new NotImplementedException();
        }
        protected override void R_Saving(PMR00100DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }
        protected override void R_Deleting(PMR00100DTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
        public List<PropertyDTO> GetPropertyList(PMR00100ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetPropertyList PMR00100");
            List<PropertyDTO> loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMR00100.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PropertyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetPropertyList PMR00100");
            return loResult;
        }
        public PeriodYearRangeDTO GetPeriodYear(PMR00100ParamDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetPeriodYear PMR00100");
            PeriodYearRangeDTO loResult = null;
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                var lcQuery = $"RSP_GS_GET_PERIOD_YEAR_RANGE '{poParameter.CCOMPANY_ID}','',''";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMR00100.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodYearRangeDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
            {
                _loggerPMR00100.LogError(loException);

            }
            loException.ThrowExceptionIfErrors();
            _loggerPMR00100.LogInfo("End GetPeriodYear PMR00100");
            return loResult;
        }
        public List<PeriodDT_DTO> GetPeriodDTList(PMR00100ParamDTO poParameter, PMR00100ParamDTO poData)
        {
            var loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetPeriodDTList PMR00100");
            List<PeriodDT_DTO> loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GS_GET_PERIOD_DT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poData.CYEAR);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
                _loggerPMR00100.LogDebug("EXEC RSP_GS_GET_DEPT_LOOKUP_LIST {@poParameter}", loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodDT_DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetPeriodDTList PMR00100");
            return loResult;
        }
        public List<LOOStatusDTO> GetLOOStatus(PMR00100ParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetLOOStatus PMR00100");
            List<LOOStatusDTO> loRtn = new List<LOOStatusDTO>();
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_AGREEMENT_STATUS', '', '{poParameter.CLANG_ID}')";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMR00100.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<LOOStatusDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMR00100.LogInfo("End GetLOOStatus PMR00100");
            return loRtn;
        }
        public List<PMR00100DTO> GetPrintLOOList(PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintList");
            var loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start GetPrintList PMR00100");
            List<PMR00100DTO> loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PMR00100_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPARTMENT_ID", DbType.String, 20, poParameter.CFROM_DEPARTMENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DEPARTMENT_ID", DbType.String, 20, poParameter.CTO_DEPARTMENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_SALESMAN_ID", DbType.String, 20, poParameter.CFROM_SALESMAN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_SALESMAN_ID", DbType.String, 20, poParameter.CTO_SALESMAN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 20, poParameter.CFROM_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 20, poParameter.CTO_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 20, poParameter.CLANG_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMR00100.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMR00100DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMR00100.LogInfo("End GetPrintList PMR00100");
            return loResult;
        }


    }
}
