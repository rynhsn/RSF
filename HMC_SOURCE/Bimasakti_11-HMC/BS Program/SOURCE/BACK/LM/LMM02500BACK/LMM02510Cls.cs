using LMM02500Common.DTO;
using LMM02500Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace LMM02500Back
{
    public class LMM02510Cls : R_BusinessObject<LMM02500ProfileAndTaxInfoDTO>
    {

        private LoggerLMM02500? _loggerLMM02510; 
        public LMM02510Cls()
        {
            _loggerLMM02510= LoggerLMM02500.R_GetInstanceLogger();
        }

        protected override LMM02500ProfileAndTaxInfoDTO R_Display(LMM02500ProfileAndTaxInfoDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerLMM02510.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new();
            LMM02500ProfileAndTaxInfoDTO? loRtn = new();
            string lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerLMM02510.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loDb);
                
                _loggerLMM02510.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_TENANT_GROUP_DETAIL";
                _loggerLMM02510.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02510.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMM02510.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.Profile.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poEntity.Profile.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 50, poEntity.Profile.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.Profile.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02510.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02510.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);


                _loggerLMM02510.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500ProfileDTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8601 // Possible null reference assignment.
                loRtn.Profile = R_Utility.R_ConvertTo<LMM02500ProfileDTO>(loProfileDataTable).FirstOrDefault();
#pragma warning restore CS8601 // Possible null reference assignment.
                _loggerLMM02510.LogDebug("{@ObjectReturn}", loRtn);

                _loggerLMM02510.LogInfo(string.Format("Checking Data for Profile, if LUSE_GROUP_TAX is true, another Query will be execute loRtn in Method {0}", lcMethod));
                if (loRtn.Profile.LUSE_GROUP_TAX)
                {
                _loggerLMM02510.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loDb);
                
                _loggerLMM02510.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                    lcQuery = "RSP_LM_GET_TENANT_GROUP_TAX_INFO";
                _loggerLMM02510.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02510.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMM02510.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                    loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.Profile.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poEntity.Profile.CPROPERTY_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 50, poEntity.Profile.CTENANT_GROUP_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.Profile.CUSER_LOGIN_ID);
                loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02510.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02510.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                    var loTaxInfoDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);


                _loggerLMM02510.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500TaxInfoDTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8601 // Possible null reference assignment.
                    loRtn.TaxInfo = R_Utility.R_ConvertTo<LMM02500TaxInfoDTO>(loTaxInfoDataTable).FirstOrDefault();
#pragma warning restore CS8601 // Possible null reference assignment.
                _loggerLMM02510.LogDebug("{@ObjectReturn}", loRtn);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02510.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02510.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override void R_Deleting(LMM02500ProfileAndTaxInfoDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
            _loggerLMM02510.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            string lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {

                _loggerLMM02510.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02510.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMM02510.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02510.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMM02510.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_MAINTAIN_TENANT_GRP";
                _loggerLMM02510.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02510.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMM02510.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.Profile.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.Profile.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 20, poEntity.Profile.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_NAME", DbType.String, 100, poEntity.Profile.CTENANT_GROUP_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CADDRESS", DbType.String, 255, poEntity.Profile.CADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE1", DbType.String, 30, poEntity.Profile.CPHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE2", DbType.String, 30, poEntity.Profile.CPHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CEMAIL", DbType.String, 100, poEntity.Profile.CEMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_NAME", DbType.String, 100, poEntity.Profile.CPIC_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_POSITION", DbType.String, 100, poEntity.Profile.CPIC_POSITION);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_EMAIL", DbType.String, 100, poEntity.Profile.CPIC_EMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE1", DbType.String, 30, poEntity.Profile.CPIC_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE2", DbType.String, 30, poEntity.Profile.CPIC_MOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCommand, "@LUSE_GROUP_TAX", DbType.Boolean, 2, poEntity.Profile.LUSE_GROUP_TAX);

                loDb.R_AddCommandParameter(loCommand, "@CTAX_CODE", DbType.String, 2, poEntity.TaxInfo.CTAX_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_TYPE", DbType.String, 2, poEntity.TaxInfo.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 40, poEntity.TaxInfo.CTAX_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_NAME", DbType.String, 100, poEntity.TaxInfo.CTAX_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LPPH", DbType.Boolean, 2, poEntity.TaxInfo.LPPH);
                loDb.R_AddCommandParameter(loCommand, "@CID_NO", DbType.String, 40, poEntity.TaxInfo.CID_NO);
                loDb.R_AddCommandParameter(loCommand, "@CID_TYPE", DbType.String, 2, poEntity.TaxInfo.CID_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CID_EXPIRED_DATE", DbType.String, 20, poEntity.TaxInfo.CID_EXPIRED_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 18, poEntity.TaxInfo.NTAX_CODE_LIMIT_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ADDRESS", DbType.String, 255, poEntity.TaxInfo.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE1", DbType.String, 30, poEntity.TaxInfo.CTAX_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE2", DbType.String, 30, poEntity.TaxInfo.CTAX_PHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_EMAIL", DbType.String, 100, poEntity.TaxInfo.CTAX_EMAIL);

                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 50, "DELETE");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.Profile.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02510.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerLMM02510.LogInfo(string.Format("Execute the SQL non-query (delete) with loConn and loCommand in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMM02510.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerLMM02510.LogError("{@ErrorObject}", loException.Message);

            _loggerLMM02510.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Saving(LMM02500ProfileAndTaxInfoDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            _loggerLMM02510.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            string lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            R_Db? loDb;
            string lcAction = "";

            try
            {
                _loggerLMM02510.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02510.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02510.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMM02510.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02510.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMM02510.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = "EDIT";
                if (poCRUDMode == eCRUDMode.AddMode)
                    lcAction = "ADD";
                _loggerLMM02510.LogDebug("{@ObjectAction}", lcAction);

                _loggerLMM02510.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_MAINTAIN_TENANT_GRP";
                _loggerLMM02510.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02510.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMM02510.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02510.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.Profile.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.Profile.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 20, poNewEntity.Profile.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_NAME", DbType.String, 100, poNewEntity.Profile.CTENANT_GROUP_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CADDRESS", DbType.String, 255, poNewEntity.Profile.CADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE1", DbType.String, 30, poNewEntity.Profile.CPHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE2", DbType.String, 30, poNewEntity.Profile.CPHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CEMAIL", DbType.String, 100, poNewEntity.Profile.CEMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_NAME", DbType.String, 100, poNewEntity.Profile.CPIC_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_POSITION", DbType.String, 100, poNewEntity.Profile.CPIC_POSITION);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_EMAIL", DbType.String, 100, poNewEntity.Profile.CPIC_EMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE1", DbType.String, 30, poNewEntity.Profile.CPIC_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE2", DbType.String, 30, poNewEntity.Profile.CPIC_MOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCommand, "@LUSE_GROUP_TAX", DbType.Boolean, 2, poNewEntity.Profile.LUSE_GROUP_TAX);

                loDb.R_AddCommandParameter(loCommand, "@CTAX_CODE", DbType.String, 2, poNewEntity.TaxInfo.CTAX_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_TYPE", DbType.String, 2, poNewEntity.TaxInfo.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 40, poNewEntity.TaxInfo.CTAX_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_NAME", DbType.String, 100, poNewEntity.TaxInfo.CTAX_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LPPH", DbType.Boolean, 2, poNewEntity.TaxInfo.LPPH);
                loDb.R_AddCommandParameter(loCommand, "@CID_NO", DbType.String, 40, poNewEntity.TaxInfo.CID_NO);
                loDb.R_AddCommandParameter(loCommand, "@CID_TYPE", DbType.String, 2, poNewEntity.TaxInfo.CID_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CID_EXPIRED_DATE", DbType.String, 20, poNewEntity.TaxInfo.CID_EXPIRED_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 18, poNewEntity.TaxInfo.NTAX_CODE_LIMIT_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ADDRESS", DbType.String, 255, poNewEntity.TaxInfo.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE1", DbType.String, 30, poNewEntity.TaxInfo.CTAX_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE2", DbType.String, 30, poNewEntity.TaxInfo.CTAX_PHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_EMAIL", DbType.String, 100, poNewEntity.TaxInfo.CTAX_EMAIL);

                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 50, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poNewEntity.Profile.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02510.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerLMM02510.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMM02510.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerLMM02510.LogError("{@ErrorObject}", loException.Message);

            _loggerLMM02510.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

    }
}
