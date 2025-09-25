using PMT02500Common.DTO._4._Charges_Info.Db;
using PMT02500Common.DTO._4._Charges_Info.Revenue_Sharing_Process;
using PMT02500Common.Logs;
using PMT02500Common.Utilities;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace PMT02500Back
{
    public class PMT02500UnitCharges_Charges_RevenueSharingHDCls : R_BusinessObject<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>
    {
        private readonly LoggerPMT02500? _loggerPMT02500;
        private readonly ActivitySource _activitySource;

        public PMT02500UnitCharges_Charges_RevenueSharingHDCls()
        {
            _loggerPMT02500 = LoggerPMT02500.R_GetInstanceLogger();
            _activitySource = PMT02500Activity.R_GetInstanceActivitySource();
        }

        // Yang kanan
        public List<PMT02500UnitCharges_Charges_RevenueSharingHDDTO> GetRevenueSharingHDListDb(PMT02500UtilitiesParameterDTO poParameterInternal, PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO poParameter)
        {
            string? lcMethod = nameof(GetRevenueSharingHDListDb);
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerPMT02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_REVENUE_HD";
                _loggerPMT02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerPMT02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerPMT02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                //Updated 26 Apr 2024
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_SEQ_NO", DbType.String, 3, poParameter.CCHARGE_SEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerPMT02500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ChargesInfo_RevenueMinimumRentDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>(loDataTable).ToList();
                _loggerPMT02500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        protected override PMT02500UnitCharges_Charges_RevenueSharingHDDTO R_Display(PMT02500UnitCharges_Charges_RevenueSharingHDDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT02500UnitCharges_Charges_RevenueSharingHDDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerPMT02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_REVENUE_HD_INFO";
                _loggerPMT02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                //Updated 26 Apr 2024
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_SEQ_NO", DbType.String, 3, poEntity.CCHARGE_SEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREVENUE_SHARING_ID", DbType.String, 20, poEntity.CREVENUE_SHARING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT02500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCommand, true);

                _loggerPMT02500.LogInfo(string.Format("Convert the data in loRtn to data of LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>(loProfileDataTable).FirstOrDefault();

                _loggerPMT02500.LogDebug("{@ObjectReturn}", loRtn!);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }

        protected override void R_Saving(PMT02500UnitCharges_Charges_RevenueSharingHDDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            string? lcAction;
            R_Db? loDb;

            try
            {
                _loggerPMT02500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT02500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerPMT02500.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = poCRUDMode == eCRUDMode.AddMode ? "ADD" : "EDIT";
                _loggerPMT02500.LogDebug("{@ObjectAction}", lcAction);

                _loggerPMT02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE_HD";
                _loggerPMT02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                //Updated 26 Apr 2024 : Add CCHARGES_SEQ_NO
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_SEQ_NO", DbType.String, 3, poNewEntity.CCHARGE_SEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREVENUE_SHARING_ID", DbType.String, 20, poNewEntity.CREVENUE_SHARING_ID); // DITANYAKAN DARIMANA
                loDb.R_AddCommandParameter(loCommand, "@CREVENUE_SHARING_NAME", DbType.String, 100, poNewEntity.CREVENUE_SHARING_NAME); // DITANYAKAN DARIMANA
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                try
                {
                    _loggerPMT02500.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    ////var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    //loEntity = R_Utility.R_ConvertTo<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>(loDataTable).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerPMT02500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerPMT02500.LogError("{@ErrorObject}", loException.Message);

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(PMT02500UnitCharges_Charges_RevenueSharingHDDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            DbConnection? loConn = null;
            string? lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerPMT02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT02500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT02500.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT02500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT02500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);


                _loggerPMT02500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE_HD";
                _loggerPMT02500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT02500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT02500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT02500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                //Updated 26 Apr 2024 : Add CCHARGES_SEQ_NO
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_SEQ_NO", DbType.String, 3, poEntity.CCHARGE_SEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREVENUE_SHARING_ID", DbType.String, 20, poEntity.CREVENUE_SHARING_ID); // DITANYAKAN DARIMANA
                loDb.R_AddCommandParameter(loCommand, "@CREVENUE_SHARING_NAME", DbType.String, 100, poEntity.CREVENUE_SHARING_NAME); // DITANYAKAN DARIMANA
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT02500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerPMT02500.LogInfo(string.Format("Execute the SQL non-query (delete) with loConn and loCommand in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerPMT02500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerPMT02500.LogError("{@ErrorObject}", loException.Message);

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
        }


    }
}
