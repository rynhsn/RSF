using PMT01600COMMON.DTO.CRUDBase;
using PMT01600COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;

namespace PMT01600BACK
{
    public class PMT01600LOI_SelectedLOICls : R_BusinessObject<PMT01600LOI_SelectedLOIDTO>
    {
        private readonly RSP_PM_MAINTAIN_AGREEMENTResources.Resources_Dummy_Class _oRSP = new();

        private readonly LoggerPMT01600? _loggerPMT01600;
        private readonly ActivitySource _activitySource;

        public PMT01600LOI_SelectedLOICls()
        {
            _loggerPMT01600 = LoggerPMT01600.R_GetInstanceLogger();
            _activitySource = PMT01600Activity.R_GetInstanceActivitySource();
        }

        protected override PMT01600LOI_SelectedLOIDTO R_Display(PMT01600LOI_SelectedLOIDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerPMT01600.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01600LOI_SelectedLOIDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerPMT01600.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01600.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01600.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01600.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01600.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                _loggerPMT01600.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01600.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01600.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01600.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01600.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01600.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCommand, true);

                _loggerPMT01600.LogInfo(string.Format("Convert the data in loRtn to data of PMT01600LOI_SelectedLOIDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT01600LOI_SelectedLOIDTO>(loProfileDataTable).FirstOrDefault()! != null
                    ? R_Utility.R_ConvertTo<PMT01600LOI_SelectedLOIDTO>(loProfileDataTable).FirstOrDefault()!
                    : new PMT01600LOI_SelectedLOIDTO();
                _loggerPMT01600.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01600.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01600.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }

        protected override void R_Saving(PMT01600LOI_SelectedLOIDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01600.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            string? lcAction;
            R_Db? loDb;

            try
            {
                _loggerPMT01600.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01600.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01600.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01600.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01600.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT01600.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01600.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerPMT01600.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                _loggerPMT01600.LogDebug("{@ObjectAction}", lcAction);

                _loggerPMT01600.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                _loggerPMT01600.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01600.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01600.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01600.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 20, poNewEntity.CREF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 8, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, poNewEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poNewEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, int.MaxValue, poNewEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTHS", DbType.Int32, int.MaxValue, poNewEntity.IMONTHS);
                loDb.R_AddCommandParameter(loCommand, "@IYEARS", DbType.Int32, int.MaxValue, poNewEntity.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poNewEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 255, poNewEntity.CUNIT_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poNewEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, poNewEntity.CLEASE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poNewEntity.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CORIGINAL_REF_NO", DbType.String, 8, "");//Tanya Pak IB
                loDb.R_AddCommandParameter(loCommand, "@CFOLLOW_UP_DATE", DbType.String, 8, poNewEntity.CFOLLOW_UP_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEXPIRED_DATE", DbType.String, 8, "");//Tanya Pak IB
                loDb.R_AddCommandParameter(loCommand, "@LWITH_FO", DbType.Boolean, 1, false);
                loDb.R_AddCommandParameter(loCommand, "@CHAND_OVER_DATE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 8, poNewEntity.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NBOOKING_FEE", DbType.String, 8, poNewEntity.NBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CTC_CODE", DbType.String, 8, poNewEntity.CTC_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_TRANS_CODE", DbType.String, 8, poNewEntity.CLINK_TRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_REF_NO", DbType.String, 8, poNewEntity.CLINK_REF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01600.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerPMT01600.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    var loTakeRefNo = R_Utility.R_ConvertTo<PMT01600LOI_SelectedLOIDTO>(loDataTable).FirstOrDefault()!;
                    if (loTakeRefNo != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREF_NO = loTakeRefNo.CREF_NO;
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerPMT01600.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerPMT01600.LogError("{@ErrorObject}", loException.Message);

            _loggerPMT01600.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(PMT01600LOI_SelectedLOIDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01600.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            DbConnection? loConn = null;
            string? lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerPMT01600.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01600.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01600.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01600.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01600.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT01600.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01600.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerPMT01600.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                _loggerPMT01600.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01600.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01600.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01600.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 20, poEntity.CREF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 8, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, poEntity.CDOC_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 8, poEntity.CDOC_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 510, poEntity.CUNIT_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, poEntity.CLEASE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poEntity.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01600.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerPMT01600.LogInfo(string.Format("Execute the SQL non-query (delete) with loConn and loCommand in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerPMT01600.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerPMT01600.LogError("{@ErrorObject}", loException.Message);

            _loggerPMT01600.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
        }

    }
}
