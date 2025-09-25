using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace PMT01900Back
{
    public class PMT01900Charges_ChagesInfoDetailCls : R_BusinessObject<PMT01900Charges_ChagesInfoDetailDTO>
    {
        private readonly RSP_PM_MAINTAIN_AGREEMENT_CHARGESResources.Resources_Dummy_Class _oRSP = new();

        private readonly LoggerPMT01900? _loggerPMT01900;
        private readonly ActivitySource _activitySource;

        public PMT01900Charges_ChagesInfoDetailCls()
        {
            _loggerPMT01900 = LoggerPMT01900.R_GetInstanceLogger();
            _activitySource = PMT01900Activity.R_GetInstanceActivitySource();
        }

        protected override PMT01900Charges_ChagesInfoDetailDTO R_Display(PMT01900Charges_ChagesInfoDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _loggerPMT01900.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01900Charges_ChagesInfoDetailDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _loggerPMT01900.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01900.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01900.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01900.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01900.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_DT";
                _loggerPMT01900.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01900.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01900.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01900.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
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
                _loggerPMT01900.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerPMT01900.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCommand, true);

                _loggerPMT01900.LogInfo(string.Format("Convert the data in loRtn to data of LMT01500ChargesInfoDetailDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<PMT01900Charges_ChagesInfoDetailDTO>(loProfileDataTable).FirstOrDefault();
                _loggerPMT01900.LogDebug("{@ObjectReturn}", loRtn!);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01900.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01900.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }

        protected override void R_Saving(PMT01900Charges_ChagesInfoDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01900.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMT01900Charges_ChagesInfoDetailDTO loEntity;
            string? lcQuery;
            DbConnection? loConn = null;
            DbCommand? loCommand;
            string? lcAction;
            R_Db? loDb;

            try
            {
                _loggerPMT01900.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01900.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01900.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01900.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01900.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT01900.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01900.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerPMT01900.LogInfo(string.Format("Set lcAction based on the CRUD mode (EDIT for Update, NEW for Add) in Method {0}", lcMethod));
                lcAction = poCRUDMode == eCRUDMode.AddMode ? "ADD" : "EDIT";
                _loggerPMT01900.LogDebug("{@ObjectAction}", lcAction);

                R_ExternalException.R_SP_Init_Exception(loConn);

                if (poNewEntity.LCAL_UNIT)
                {
                    var tempData = R_Utility.R_ConvertCollectionToCollection<PMT01900Charges_ChargesInfoDetail_ChargesItemDTO, PMT01900Charges_ChargesInfoDetail_ChargesItemDTO>(poNewEntity.ODATA_CHARGES_ITEM);
                    lcQuery = "CREATE TABLE #CHARGES_ITEMS ( " +
                        "CITEM_NAME VARCHAR(255) " +
                        "IQTY INT " +
                        "NUNIT_PRICE NUMERIC(16,2) " +
                        "NDISCOUNT NUMERIC(16,2) " +
                        "NTOTAL_PRICE NUMERIC(16,2) " +
                        ")";
                    _loggerPMT01900.LogDebug("CREATE TABLE #CHARGES_ITEMS");

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    loDb.R_BulkInsert((SqlConnection)loConn, "#CHARGES_ITEMS", tempData);
                    _loggerPMT01900.LogDebug("R_BulkInsert To TABLE #CHARGES_ITEMS");
                }


                _loggerPMT01900.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                _loggerPMT01900.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01900.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01900.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01900.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 20, poNewEntity.CCHARGE_MODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 30, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poNewEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID ", DbType.String, 20, poNewEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_TYPE", DbType.String, 20, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_ID", DbType.String, 20, poNewEntity.CCHARGES_ID);

                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID ", DbType.String, 20, poNewEntity.CTAX_ID ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IYEAR", DbType.Int32, 8, poNewEntity.IYEAR);
                loDb.R_AddCommandParameter(loCommand, "@IDAY", DbType.Int32, 8, poNewEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTH", DbType.Int32, 8, poNewEntity.IMONTH);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poNewEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_MODE", DbType.String, 2, poNewEntity.CBILLING_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CFEE_METHOD", DbType.String, 2, poNewEntity.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCommand, "@NFEE_AMT", DbType.Decimal, 512, poNewEntity.NFEE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_PERIOD", DbType.String, 2, poNewEntity.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCommand, "@NINVOICE_AMT", DbType.Decimal, 512, poNewEntity.NINVOICE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@LCAL_UNIT", DbType.Boolean, 3, poNewEntity.LCAL_UNIT);
                loDb.R_AddCommandParameter(loCommand, "@LBASED_OPEN_DATE", DbType.Boolean, 3, false);
                loDb.R_AddCommandParameter(loCommand, "@LPRORATE", DbType.Boolean, 3, poNewEntity.LPRORATE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerPMT01900.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerPMT01900.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    loEntity = R_Utility.R_ConvertTo<PMT01900Charges_ChagesInfoDetailDTO>(loDataTable).FirstOrDefault()!;
                    if (loEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CSEQ_NO = string.IsNullOrEmpty(loEntity.CSEQ_NO) ? "" : loEntity.CSEQ_NO;
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerPMT01900.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerPMT01900.LogError("{@ErrorObject}", loException.Message);

            _loggerPMT01900.LogInfo(string.Format("End Method {0}", lcMethod));
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(PMT01900Charges_ChagesInfoDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01900.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            DbConnection? loConn = null;
            string? lcQuery;
            DbCommand? loCommand;
            R_Db? loDb;

            try
            {
                _loggerPMT01900.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDb = new();
                _loggerPMT01900.LogDebug("{@ObjectDb}", loDb);

                _loggerPMT01900.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerPMT01900.LogDebug("{@ObjectDb}", loCommand);

                _loggerPMT01900.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerPMT01900.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerPMT01900.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerPMT01900.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                _loggerPMT01900.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerPMT01900.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _loggerPMT01900.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerPMT01900.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
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
                loDb.R_AddCommandParameter(loCommand, "@IYEAR", DbType.Int32, 8, poEntity.IYEAR);
                loDb.R_AddCommandParameter(loCommand, "@IMONTH", DbType.Int32, 8, poEntity.IMONTH);
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
                _loggerPMT01900.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerPMT01900.LogInfo(string.Format("Execute the SQL non-query (delete) with loConn and loCommand in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerPMT01900.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
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
                _loggerPMT01900.LogError("{@ErrorObject}", loException.Message);

            _loggerPMT01900.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
        }

    }
}
