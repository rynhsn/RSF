using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT02500Common.DTO._4._Charges_Info;
using PMT02500Common.Logs;
using PMT02500Common.Utilities;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_PM_MAINTAIN_AGREEMENT_CHARGESResources;
using System.Data.SqlClient;
using System;

namespace PMT02500Back
{
    public class PMT02500ChargesInfoCls : R_BusinessObject<PMT02500ChargesInfoDetailDTO>
    {
        private Resources_Dummy_Class _oRsp = new Resources_Dummy_Class();
        private readonly LoggerPMT02500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public PMT02500ChargesInfoCls()
        {
            _loggerLMT01500 = LoggerPMT02500.R_GetInstanceLogger();
            _activitySource = PMT02500Activity.R_GetInstanceActivitySource();
        }

        public PMT02500ChargesInfoHeaderDTO GetChargesInfoHeaderDb(PMT02500UtilitiesParameterDTO poParameterInternal, PMT02500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetChargesInfoHeaderDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loEx = new R_Exception();
            PMT02500ChargesInfoHeaderDTO? loRtn = null;
            DataTable? loDataTable;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCmd = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to data of LMT01500ChargesInfoHeaderDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT02500ChargesInfoHeaderDTO>(loDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT02500ChargesInfoListDTO> GetChargesInfoListDb(PMT02500UtilitiesParameterDTO poParameterInternal, PMT02500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetChargesInfoListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT02500ChargesInfoListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_LIST";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                //New Updated 18 Apr 2024
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poParameter.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ChargesInfoListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT02500ChargesInfoListDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT02500FrontChargesInfo_FeeCalculationDetailDTO> GetChargesInfoCalUnitListDb(PMT02500UtilitiesParameterDTO poParameterInternal, PMT02500GetHeaderParameterChargesInfoCalUnitDTO poParameter)
        {
            string? lcMethod = nameof(GetChargesInfoCalUnitListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_CAL_UNIT";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poParameter.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ChargesInfoListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT02500ComboBoxDTO> GetComboBoxDataCFEE_METHODDb(PMT02500UtilitiesWithCultureIDParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetComboBoxDataCFEE_METHODDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT02500ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                "(@BIMASAKTI, @CCOMPANY_ID, @BS_FEE_METHOD, @NONE, @CULTURE_ID);";
                _loggerLMT01500.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_FEE_METHOD", DbType.String, 15, "_BS_FEE_METHOD");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParameterInternal.CULTURE_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ComboBoxDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT02500ComboBoxDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public PMT02500ChargesInfoResultActiveDTO ProcessChangeStatusChargesInfoActiveDb(PMT02500ChargesInfoParameterActiveDTO poParameter)
        {
            string? lcMethod = nameof(ProcessChangeStatusChargesInfoActiveDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT02500ChargesInfoResultActiveDTO? loRtn = new PMT02500ChargesInfoResultActiveDTO();
            DbConnection? loConn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);


                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_ACTIVE_INACTIVE_AGREEMENT_CHARGES";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poParameter.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE", DbType.Boolean, 2, poParameter.LACTIVE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerLMT01500.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMT01500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);

            }

            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT02500ComboBoxDTO> GetComboBoxDataCINVOICE_PERIODDb(PMT02500UtilitiesWithCultureIDParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetComboBoxDataCINVOICE_PERIODDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT02500ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                "(@BIMASAKTI, @CCOMPANY_ID, @BS_INVOICE_PERIOD, @NONE, @CULTURE_ID);";
                _loggerLMT01500.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_INVOICE_PERIOD", DbType.String, 20, "_BS_FEE_METHOD");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParameterInternal.CULTURE_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ComboBoxDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT02500ComboBoxDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override PMT02500ChargesInfoDetailDTO R_Display(PMT02500ChargesInfoDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT02500ChargesInfoDetailDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_DT";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loRtn to data of LMT01500ChargesInfoDetailDTO objects and assign it to loRtn in Method {0}", lcMethod));
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loRtn = R_Utility.R_ConvertTo<PMT02500ChargesInfoDetailDTO>(loProfileDataTable).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        protected override void R_Saving(PMT02500ChargesInfoDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMT02500ChargesInfoDetailDTO loEntity;
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            string? lcAction;
            R_Db? loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMT01500.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = poCRUDMode == eCRUDMode.AddMode ? "ADD" : "EDIT";
                _loggerLMT01500.LogDebug("{@ObjectAction}", lcAction);
                
                R_ExternalException.R_SP_Init_Exception(loConn);

                if (poNewEntity.LCAL_UNIT)
                {
                    var tempData = R_Utility.R_ConvertCollectionToCollection<PMT02500FrontChargesInfo_FeeCalculationDetailDTO, PMT02500FrontChargesInfo_FeeCalculationDetailDTO>(poNewEntity.ODATA_FEE_CALCULATION);
                    lcQuery = "CREATE TABLE #CHARGES_CAL_UNIT( " +
                              "CUNIT_ID VARCHAR(20), " +
                              "CFLOOR_ID VARCHAR(20), " +		
                              "CBUILDING_ID VARCHAR(20), " +		
                              "NTOTAL_AREA NUMERIC(6,2), " +	
                              "NFEE_PER_AREA NUMERIC(16,2) "+	
                               " )";
                    _loggerLMT01500.LogDebug("CREATE TABLE #CHARGES_CAL_UNIT");

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    loDb.R_BulkInsert((SqlConnection)loConn, "#CHARGES_CAL_UNIT", tempData);
                    _loggerLMT01500.LogDebug("R_BulkInsert To TABLE #CHARGES_CAL_UNIT");
                }


                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO ?? ""); //

                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 20, poNewEntity.CCHARGE_MODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 30, poNewEntity.CBUILDING_ID); //
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poNewEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID ", DbType.String, 20, poNewEntity.CUNIT_ID);

                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_TYPE", DbType.String, 20, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_ID", DbType.String, 20, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID ", DbType.String, 20, poNewEntity.CTAX_ID ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IYEAR", DbType.Int32, 8, poNewEntity.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTH", DbType.Int32, 8, poNewEntity.IMONTHS);                //CR10/07/2024
                loDb.R_AddCommandParameter(loCommand, "@IDAY", DbType.Int32, 8, poNewEntity.IDAYS);                //CR10/07/2024
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poNewEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE); //""
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_MODE", DbType.String, 2, poNewEntity.CBILLING_MODE); //""
                loDb.R_AddCommandParameter(loCommand, "@CFEE_METHOD", DbType.String, 2, poNewEntity.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCommand, "@NFEE_AMT", DbType.Decimal, 512, poNewEntity.NFEE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_PERIOD", DbType.String, 2, poNewEntity.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCommand, "@NINVOICE_AMT", DbType.Decimal, Int16.MaxValue, poNewEntity.NINVOICE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@LCAL_UNIT", DbType.Boolean, 2, poNewEntity.LCAL_UNIT);
                loDb.R_AddCommandParameter(loCommand, "@LBASED_OPEN_DATE", DbType.Boolean, 2, poNewEntity.LBASED_OPEN_DATE);
                loDb.R_AddCommandParameter(loCommand, "@LPRORATE", DbType.Boolean, 2, poNewEntity.LPRORATE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                
                try
                {
                    _loggerLMT01500.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    loEntity = R_Utility.R_ConvertTo<PMT02500ChargesInfoDetailDTO>(loDataTable).FirstOrDefault()!;
                    if (loEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CSEQ_NO = string.IsNullOrEmpty(loEntity.CSEQ_NO) ? "" : loEntity.CSEQ_NO;
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMT01500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(PMT02500ChargesInfoDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            DbConnection? loConn = null;
            string? lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poEntity.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_TYPE", DbType.String, 20, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_ID", DbType.String, 20, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 20, poEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poEntity.CSTART_DATE);
                //Updated 26 Apr 2024
                loDb.R_AddCommandParameter(loCommand, "@IYEAR", DbType.Int32, 8, poEntity.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@IDAY", DbType.Int32, 8, poEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTH", DbType.Int32, 8, poEntity.IMONTHS);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_MODE", DbType.String, 2, poEntity.CBILLING_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CFEE_METHOD", DbType.String, 2, poEntity.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCommand, "@NFEE_AMT", DbType.Decimal, Int16.MaxValue, poEntity.NFEE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_PERIOD", DbType.String, 2, poEntity.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCommand, "@NINVOICE_AMT", DbType.Decimal, Int16.MaxValue, poEntity.NINVOICE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@LCAL_UNIT", DbType.Boolean, 2, poEntity.LCAL_UNIT);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerLMT01500.LogInfo(string.Format("Execute the SQL non-query (delete) with loConn and loCommand in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMT01500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
        }

    }
}