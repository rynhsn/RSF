using BMM00500BACK.OpenTelemetry;
using BMM00500COMMON.Loggers;
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
using BMM00500COMMON.DTO;
using R_CommonFrontBackAPI;
using System.Windows.Input;
using System.Reflection.Metadata;

namespace BMM00500BACK
{
    public class BMM00500Cls : R_BusinessObject<BMM00500CRUDParameterDTO>
    {
        private readonly RSP_BM_MAINTAIN_MOBILE_PROGRAMResources.Resources_Dummy_Class _oRSP = new();

        private readonly LoggerBMM00500 _logger;
        private readonly ActivitySource _activitySource;
        public BMM00500Cls()
        {
            _logger = LoggerBMM00500.R_GetInstanceLogger();
            _activitySource = BMM00500ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<BMM00500DTO> GetMobileProgramDb(BMM00500ParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetMobileProgramDb);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();
            List<BMM00500DTO> loRtn = new List<BMM00500DTO>();
            R_Db? loDb = null;
            DbConnection? loConn = null;
            DbCommand? loCommand = null;

            try
            {
                _logger.LogInfo(string.Format("Initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = "RSP_BM_GET_MOBILE_PROGRAM";
                loCommand.CommandType = CommandType.StoredProcedure;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);

                _logger.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 8, poParameter.CPROGRAM_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _logger.LogInfo(string.Format("Convert the data in loDataTable to a list of {1} objects and assign it to loRtn in Method {0}", lcMethod, nameof(BMM00500DTO)));
                loRtn = R_Utility.R_ConvertTo<BMM00500DTO>(loDataTable).ToList();
                _logger.LogDebug("{@ObjectReturn}", loRtn!);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
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

                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }

                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        public void RSP_BM_MAINTAIN_MOBILE_PROGRAM(BMM00500CRUDParameterDTO poParameter)
        {
            string? lcMethod = nameof(RSP_BM_MAINTAIN_MOBILE_PROGRAM);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();
            R_Db? loDb = null;
            DbConnection? loConn = null;
            DbCommand? loCommand = null;

            try
            {
                _logger.LogInfo(string.Format("Initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _logger.LogDebug("{@ObjectDb}", loDb);

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _logger.LogDebug("{@ObjectDbConnection}", loConn);

                _logger.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _logger.LogDebug("{@ObjectDb}", loCommand);

                _logger.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = "RSP_BM_MAINTAIN_MOBILE_PROGRAM";
                loCommand.CommandType = CommandType.StoredProcedure;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);

                _logger.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID",   DbType.String,  8,      poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID",   DbType.String,  8,      poParameter.Data.CPROGRAM_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_NAME", DbType.String,  800,    poParameter.Data.CPROGRAM_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LPRIORITY",     DbType.Boolean, 8,      poParameter.Data.LPRIORITY);
                loDb.R_AddCommandParameter(loCommand, "@LTENANT",       DbType.Boolean, 8,      poParameter.Data.LTENANT);
                loDb.R_AddCommandParameter(loCommand, "@LPORTAL",       DbType.Boolean, 8,      poParameter.Data.LPORTAL);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE",       DbType.Boolean, 8,      poParameter.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION",       DbType.String,  10,     poParameter.CACTION);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID",      DbType.String,  8,      poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
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
                _logger.LogError("{@ErrorObject}", loEx.Message);
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

                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }

                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
        }

        protected override void R_Deleting(BMM00500CRUDParameterDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();

            try
            {
                _logger.LogInfo(string.Format("Run {1} || {0}", lcMethod, nameof(RSP_BM_MAINTAIN_MOBILE_PROGRAM)));
                RSP_BM_MAINTAIN_MOBILE_PROGRAM(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
        }

        protected override BMM00500CRUDParameterDTO R_Display(BMM00500CRUDParameterDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();
            BMM00500CRUDParameterDTO loRtn = new BMM00500CRUDParameterDTO();
            BMM00500ParameterDTO? loParam = null;

            try
            {
                _logger.LogInfo(string.Format("Set Parameter for {1} || {0}", lcMethod, nameof(GetMobileProgramDb)));
                loParam = new BMM00500ParameterDTO()
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CPROGRAM_ID = poEntity.Data.CPROGRAM_ID
                };
                _logger.LogDebug("{@ObjectDb}", loParam);

                _logger.LogInfo(string.Format("Run {1} || {0}", lcMethod, nameof(GetMobileProgramDb)));
                loRtn.Data = GetMobileProgramDb(loParam).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        protected override void R_Saving(BMM00500CRUDParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();

            try
            {
                _logger.LogInfo(string.Format("Run {1} || {0}", lcMethod, nameof(RSP_BM_MAINTAIN_MOBILE_PROGRAM)));
                RSP_BM_MAINTAIN_MOBILE_PROGRAM(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));
        }
    }
}
