
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
    public class LMM02500Cls : R_BusinessObject<LMM02500ProfileDTO>
    {
        private LoggerLMM02500? _loggerLMM02500; 
        public LMM02500Cls()
        {
            _loggerLMM02500= LoggerLMM02500.R_GetInstanceLogger();
        }

        public List<LMM02500ProfileDTO> LMM02500ListDb(LMM02500ListDbParameterDTO poParameter)
        {
            string? lcMethod = nameof(LMM02500ListDb);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMM02500ProfileDTO>? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_TENANT_GROUP_LIST";
                _loggerLMM02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMM02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMM02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Add command parameters for CCOMPANY_ID, CPROPERTY_ID, and CLANGUAGE_ID in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,50,poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CPROPERTY_ID",DbType.String,50,poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID", DbType.String,50,poParameter.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMM02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500ProfileDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<LMM02500ProfileDTO>(loDataTable).ToList();
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<LMM02500ComboBoxType> GetTaxTypeComboBoxDb(LMM02500ComboBoxDbParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetTaxTypeComboBoxDb);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMM02500ComboBoxType>? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = $"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_TAX_TYPE', '', '{poParameter.CULTURE_ID}')";
                _loggerLMM02500.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMM02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _loggerLMM02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);


                _loggerLMM02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500ComboBoxType objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<LMM02500ComboBoxType>(loDataTable).ToList();
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<LMM02500ComboBoxType> GetIdTypeComboBoxDb(LMM02500ComboBoxDbParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetIdTypeComboBoxDb);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMM02500ComboBoxType>? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = $"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_BS_ID_TYPE', '', '{poParameter.CULTURE_ID}')";
                _loggerLMM02500.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMM02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _loggerLMM02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMM02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500ComboBoxType objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<LMM02500ComboBoxType>(loDataTable).ToList();
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<LMM02500ComboBoxType> GetTaxCodeComboBoxDb(LMM02500ComboBoxDbParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetTaxCodeComboBoxDb);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMM02500ComboBoxType>? loRtn = null;
            string lcQuery;
            DbConnection? loConn = null;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = $"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{poParameter.CCOMPANY_ID}', '_TAX_CODE', '', '{poParameter.CULTURE_ID}')";
                _loggerLMM02500.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMM02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _loggerLMM02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMM02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500ComboBoxType objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<LMM02500ComboBoxType>(loDataTable).ToList();
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<LMM02500ParameterDTO> LMM02500GetPropertyDb(LMM02500GetPropertyDbParameterDTO poParameter)
        {
            string? lcMethod = nameof(LMM02500GetPropertyDb);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMM02500ParameterDTO>? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                _loggerLMM02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMM02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMM02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Add command parameters for CCOMPANY_ID, CPROPERTY_ID, and CLANGUAGE_ID in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,50,poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID", DbType.String,50,poParameter.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMM02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500ParameterDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<LMM02500ParameterDTO>(loDataTable).ToList();
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.

        }

        public LMM02500TaxInfoDTO GetTaxInfoTenantGroupDb(LMM02500DetailDbParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetTaxInfoTenantGroupDb);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500TaxInfoDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {

                _loggerLMM02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMM02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                _loggerLMM02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMM02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_TENANT_GROUP_TAX_INFO";
                _loggerLMM02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMM02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Add command parameters for CCOMPANY_ID, CPROPERTY_ID, and CLANGUAGE_ID in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CPROPERTY_ID",DbType.String,50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CTENANT_GROUP_ID",DbType.String,50, poParameter.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID",DbType.String,50, poParameter.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02500.LogInfo(string.Format("Execute the SQL query and store the result in loProfileDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMM02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500TaxInfoDTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loRtn = R_Utility.R_ConvertTo<LMM02500TaxInfoDTO>(loProfileDataTable).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override LMM02500ProfileDTO R_Display(LMM02500ProfileDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500ProfileDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loDb);
                
                _loggerLMM02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMM02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_TENANT_GROUP_DETAIL";
                _loggerLMM02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMM02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMM02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMM02500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CPROPERTY_ID",DbType.String,50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CTENANT_GROUP_ID",DbType.String,50, poEntity.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID",DbType.String,50, poEntity.CUSER_LOGIN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMM02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMM02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection("R_DefaultConnectionString"), loCommand, true);

                _loggerLMM02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMM02500ProfileDTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loRtn = R_Utility.R_ConvertTo<LMM02500ProfileDTO>(loProfileDataTable).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override void R_Saving(LMM02500ProfileDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(LMM02500ProfileDTO poEntity)
        {
            throw new NotImplementedException();
        }

    }
}
