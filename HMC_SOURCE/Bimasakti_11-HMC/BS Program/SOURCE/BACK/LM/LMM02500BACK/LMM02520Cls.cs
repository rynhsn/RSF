
using LMM02500Back.DTO;
using LMM02500Common.DTO;
using LMM02500Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace LMM02500Back
{
    public class LMM02520Cls : R_BusinessObject<LMM02520DTO>
    {
        private LoggerLMM02500? _loggerLMM02520; 
        public LMM02520Cls()
        {
            _loggerLMM02520= LoggerLMM02500.R_GetInstanceLogger();
        }

        public void SaveMoveTenantGroupCategoryDb(SaveMoveTenantGroupParameterDbDTO poEntity)
        {
            string? lcMethod = nameof(SaveMoveTenantGroupCategoryDb);
            _loggerLMM02520.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_Db? loDb = new R_Db();
            DbConnection? loConn = loDb.GetConnection();

            try
            {
                _loggerLMM02520.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                string lcQuery = $"DECLARE @CTENANT_LIST AS RDT_TENANT_LIST " +
                    $"INSERT INTO @CTENANT_LIST VALUES {poEntity.CTENANT_ID} " +
                    $"EXEC RSP_LM_MOVE_TENANT_GROUP " +
                    $"'{poEntity.CLOGIN_COMPANY_ID}', " +
                    $"'{poEntity.CSELECTED_PROPERTY_ID}', " +
                    $"'{poEntity.CFROM_TENANT_CATEGORY_ID}', " +
                    $"'{poEntity.CTO_TENANT_CATEGORY_ID}', " +
                    $"'{poEntity.CLOGIN_USER_ID}', " +
                    $"@CTENANT_LIST";
                _loggerLMM02520.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerLMM02520.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                DbCommand? loCmd = loDb.GetCommand();
                _loggerLMM02520.LogDebug("{@ObjectDb}", loCmd);

                _loggerLMM02520.LogInfo(string.Format("Set the command's text to lcQuery in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                _loggerLMM02520.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerLMM02520.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    _loggerLMM02520.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                _loggerLMM02520.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }
            if (loException.Haserror)
                _loggerLMM02520.LogError("{@ErrorObject}", loException.Message);

            _loggerLMM02520.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

        public List<LMM02520GridDTO> GetAllTenantGroupStreamDb(LMM02500DetailDbParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetAllTenantGroupStreamDb);
            _loggerLMM02520.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            List<LMM02520GridDTO>? loRtn = null;
            string lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerLMM02520.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02520.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02520.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02520.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02520.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_TENANT_LIST_GROUP";
                _loggerLMM02520.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02520.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMM02520.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02520.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMM02520.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02520.LogInfo(string.Format("Add command parameters for CCOMPANY_ID, CPROPERTY_ID, and CLANGUAGE_ID in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,50,poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CPROPERTY_ID",DbType.String,50,poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CTENANT_GROUP_ID",DbType.String,50,poParameter.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID", DbType.String,50,poParameter.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02520.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02520.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMM02520.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02520GridDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<LMM02520GridDTO>(loDataTable).ToList();
                _loggerLMM02520.LogDebug("{@ObjectReturn}", loRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02520.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02520.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override LMM02520DTO R_Display(LMM02520DTO poEntity)
        {
            string? lnMethod = nameof(R_Display);
            _loggerLMM02520.LogInfo(string.Format("Start Method {0}", lnMethod));
            R_Exception? loException = new R_Exception();
            LMM02520DTO? loRtn = null;
            string lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerLMM02520.LogInfo(string.Format("initialization R_Db in Method {0}", lnMethod));
                loDb = new();
                _loggerLMM02520.LogDebug("{@ObjectDb}", loDb);
                
                _loggerLMM02520.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lnMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02520.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02520.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lnMethod));
                lcQuery = "RSP_LM_GET_TENANT_DETAIL";
                _loggerLMM02520.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02520.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lnMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMM02520.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02520.LogInfo(string.Format("Add command parameters in Method {0}", lnMethod));
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CPROPERTY_ID",DbType.String,50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CTENANT_ID",DbType.String,50, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID",DbType.String,50, poEntity.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02520.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02520.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lnMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);

                _loggerLMM02520.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02520DTO objects and assign it to loRtn in Method {0}", lnMethod));
                loRtn = R_Utility.R_ConvertTo<LMM02520DTO>(loProfileDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMM02520.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02520.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02520.LogInfo(string.Format("End Method {0}", lnMethod));


#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override void R_Deleting(LMM02520DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(LMM02520DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

    }
}
