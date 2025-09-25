using GLM00100Back.DTOs;
using GLM00100Common.DTOs;
using GLM00100Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GLM00100Back
{
    public class GLM00100Cls : R_BusinessObject<GLM00100DTO>
    {
        private LoggerGLM00100? _loggerGLM00100;

        public GLM00100Cls()
        {
            _loggerGLM00100= LoggerGLM00100.R_GetInstanceLogger();
        }

        public void CreateSystemParameter(GLM00100CreateSystemParameterDbDTO poParameter)
        {
            string? lcMethod = nameof(CreateSystemParameter);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerGLM00100.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loDb);

                _loggerGLM00100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loCommand);

                 _loggerGLM00100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GL_CREATE_SYSTEM_PARAM";
                _loggerGLM00100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerGLM00100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerGLM00100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerGLM00100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID",DbType.String,50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CSTARTING_YY",DbType.String,4, poParameter.CSTARTING_YY);
                loDb.R_AddCommandParameter(loCommand,"@CSTARTING_MM",DbType.String,2, poParameter.CSTARTING_MM);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLM00100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                _loggerGLM00100.LogInfo(string.Format("Execute the SQL query for Create System Parameter and store the result in loCreateTable in Method {0}", lcMethod));
                var loCreateTable = loDb.SqlExecNonQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);
                _loggerGLM00100.LogDebug("{@ObjectExecuteQueryCreate}", loCreateTable);
                

            }
            catch (Exception ex)
            {
                loException.Add(ex);    

            }

            if (loException.Haserror)
                _loggerGLM00100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

        }

        public GLM00100GSMPeriod GetStartingPeriodYear(string pcCCOMPANY_ID)
        {
            string? lcMethod = nameof(GetStartingPeriodYear);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loEx = new R_Exception();
            GLM00100GSMPeriod? loRtn = null;
            DataTable? loDataTable;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            R_Db loDb;

            try
            {
                _loggerGLM00100.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loDb);

                _loggerGLM00100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCmd = loDb.GetCommand();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loCmd);

                 _loggerGLM00100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                _loggerGLM00100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerGLM00100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                 loConn = loDb.GetConnection();
                _loggerGLM00100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerGLM00100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                _loggerGLM00100.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerGLM00100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, "");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, "");
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLM00100.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                _loggerGLM00100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                _loggerGLM00100.LogInfo(string.Format("Convert the data in loDataTable to data of GLM00100GSMPeriod objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<GLM00100GSMPeriod>(loDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerGLM00100.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerGLM00100.LogError("{@ErrorObject}", loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.

        }

        public GLM00100ResultData GetCheckerSystemParameter(string pcCCOMPANY_ID)
        {
            string? lcMethod = nameof(GetCheckerSystemParameter);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loEx = new R_Exception();
            GLM00100ResultData? loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                _loggerGLM00100.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loDb);

                _loggerGLM00100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCmd = loDb.GetCommand();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loCmd);

                 _loggerGLM00100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = $"SELECT TOP 1 1 AS IRESULT " +
                        $"FROM GLM_SYSTEM_PARAM (NOLOCK) " +
                        $"WHERE CCOMPANY_ID= @CCOMPANY_ID";
                _loggerGLM00100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerGLM00100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerGLM00100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerGLM00100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                _loggerGLM00100.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerGLM00100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, pcCCOMPANY_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLM00100.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                _loggerGLM00100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                _loggerGLM00100.LogInfo(string.Format("Checking Data System Parameter is Found or not in Method {0}", lcMethod));
                if (loDataTable.DataSet == null)
                {
                    DataRow row = loDataTable.NewRow();
                    row["IRESULT"] = 0;
                    loDataTable.Rows.Add(row);
                }

                _loggerGLM00100.LogInfo(string.Format("Convert the data in loDataTable to a list of GLM00100ResultData objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<GLM00100ResultData>(loDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerGLM00100.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerGLM00100.LogError("{@ErrorObject}", loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.


        }

        protected override GLM00100DTO R_Display(GLM00100DTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            GLM00100DTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerGLM00100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loDb);
                
                _loggerGLM00100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loCommand);

                _loggerGLM00100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                _loggerGLM00100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerGLM00100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerGLM00100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerGLM00100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CLANGUAGE_ID",DbType.String,8, poEntity.CLANGUAGE_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLM00100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                _loggerGLM00100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);


                _loggerGLM00100.LogInfo(string.Format("Convert the data in loDataTable to a Data of GLM00100DTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loRtn = R_Utility.R_ConvertTo<GLM00100DTO>(loDataTable).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerGLM00100.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerGLM00100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override void R_Deleting(GLM00100DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(GLM00100DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            _loggerGLM00100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            string lcQuery;
            DbConnection? loConn = null;
            DbCommand loCommand;
            R_Db loDb;
            string lcAction = "";

            try
            {
                _loggerGLM00100.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loDb);

                _loggerGLM00100.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerGLM00100.LogDebug("{@ObjectDb}", loCommand);

                _loggerGLM00100.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerGLM00100.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerGLM00100.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerGLM00100.LogInfo(string.Format("Set lcAction based on the CRUD mode and this function just for Edit in Method {0}", lcMethod));
                lcAction = "EDIT";

                _loggerGLM00100.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GL_SAVE_SYSTEM_PARAM";
                _loggerGLM00100.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerGLM00100.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerGLM00100.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerGLM00100.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID",DbType.String,50, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand,"@CACTION",DbType.String,10, lcAction);
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CCLOSE_DEPT_CODE",DbType.String,8, poNewEntity.CCLOSE_DEPT_CODE);
                loDb.R_AddCommandParameter(loCommand,"@CRATETYPE_CODE",DbType.String,4, poNewEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand,"@IREVERSE_JRN_POST",DbType.Int32, Int32.MaxValue, poNewEntity.IREVERSE_JRN_POST);
                loDb.R_AddCommandParameter(loCommand,"@LCOMMIT_APVJRN",DbType.Boolean,2, poNewEntity.LCOMMIT_APVJRN);
                loDb.R_AddCommandParameter(loCommand,"@LALLOW_UNDO_COMMIT_JRN",DbType.Boolean, 2, poNewEntity.LALLOW_UNDO_COMMIT_JRN);
                loDb.R_AddCommandParameter(loCommand,"@LCOMMIT_IMPJRN",DbType.Boolean, 2, poNewEntity.LCOMMIT_IMPJRN);
                loDb.R_AddCommandParameter(loCommand,"@LALLOW_EDIT_IMPJRN_DESC",DbType.Boolean,2, poNewEntity.LALLOW_EDIT_IMPJRN_DESC);
                //Data jadi 0
                loDb.R_AddCommandParameter(loCommand,"@LCOMMIT_OTHJRN",DbType.Boolean, 2, poNewEntity.LCOMMIT_OTHJRN);
                loDb.R_AddCommandParameter(loCommand,"@LALLOW_EDIT_OTHJRN_DESC",DbType.Boolean, 2, poNewEntity.LALLOW_EDIT_OTHJRN_DESC);
                //END Of Data jadi 0
                loDb.R_AddCommandParameter(loCommand,"@CSUSPENSE_ACCOUNT_NO",DbType.String,20, poNewEntity.CSUSPENSE_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand,"@CRETAINED_ACCOUNT_NO",DbType.String,20, poNewEntity.CRETAINED_ACCOUNT_NO);
                //ini apaa
                loDb.R_AddCommandParameter(loCommand,"@LALLOW_CANCEL_SOFT_CLOSE",DbType.Boolean, 2, poNewEntity.LALLOW_CANCEL_SOFT_END);
                loDb.R_AddCommandParameter(loCommand,"@LALLOW_INTERCO_JRN",DbType.Boolean, 2, poNewEntity.LALLOW_INTERCO_JRN);
                loDb.R_AddCommandParameter(loCommand,"@LALLOW_MULTIPLE_JRN",DbType.Boolean, 2, poNewEntity.LALLOW_MULTIPLE_JRN);
                loDb.R_AddCommandParameter(loCommand,"@LWARNING_MULTIPLE_JRN",DbType.Boolean, 2, poNewEntity.LWARNING_MULTIPLE_JRN);
                loDb.R_AddCommandParameter(loCommand,"@LALLOW_DIFF_INTERCO",DbType.Boolean, 2, poNewEntity.LALLOW_DIFF_INTERCO);
                loDb.R_AddCommandParameter(loCommand,"@LWARNING_DIFF_INTERCO",DbType.Boolean, 2, poNewEntity.LWARNING_DIFF_INTERCO);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLM00100.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                try
                {
                    _loggerGLM00100.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerGLM00100.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            if (loException.Haserror)
                _loggerGLM00100.LogError("{@ErrorObject}", loException.Message);

            _loggerGLM00100.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

    }
}
