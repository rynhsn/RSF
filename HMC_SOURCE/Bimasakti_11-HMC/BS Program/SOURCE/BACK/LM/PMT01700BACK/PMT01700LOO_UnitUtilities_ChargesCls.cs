using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMT01700BACK
{
    public class PMT01700LOO_UnitUtilities_ChargesCls : R_BusinessObject<PMT01700LOO_UnitCharges_ChargesDetailDTO>
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_MAINTAIN_AGREEMENT_CHARGESResources.Resources_Dummy_Class _oRSP = new();


        public PMT01700LOO_UnitUtilities_ChargesCls()
        {
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_GetInstanceActivitySource();
        }

        public List<PMT01700ComboBoxDTO> GetFeeMethodListDb(PMT01700BaseParameterDTO poParam)
        {
            string? lcMethod = nameof(GetFeeMethodListDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01700ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO" +
               "(@BIMASAKTI, @CCOMPANY_ID, @BS_LEASE_MODE, @NONE, @CULTURE_ID);";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_LEASE_MODE", DbType.String, 20, "_BS_FEE_METHOD");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParam.CLANGUAGE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700ComboBoxDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }
        public List<PMT01700ComboBoxDTO> GetPeriodeModeListDb(PMT01700BaseParameterDTO poParam)
        {
            string? lcMethod = nameof(GetPeriodeModeListDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01700ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO" +
               "(@BIMASAKTI, @CCOMPANY_ID, @BS_LEASE_MODE, @NONE, @CULTURE_ID);";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_LEASE_MODE", DbType.String, 20, "_BS_INVOICE_PERIOD");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParam.CLANGUAGE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700ComboBoxDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }
        public List<PMT01700LOO_UnitCharges_ChargesListDTO> GetChargesListDb(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            string? lcMethodName = nameof(GetChargesListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<PMT01700LOO_UnitCharges_ChargesListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poParameter.CCHARGE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID ", DbType.String, 20, poParameter.COTHER_UNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700LOO_UnitCharges_ChargesListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
        }

        protected override PMT01700LOO_UnitCharges_ChargesDetailDTO R_Display(PMT01700LOO_UnitCharges_ChargesDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01700LOO_UnitCharges_ChargesDetailDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            DbConnection? loConn = null;

            try
            {
                loDb = new();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_DT";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

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
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loProfileDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loRtn = R_Utility.R_ConvertTo<PMT01700LOO_UnitCharges_ChargesDetailDTO>(loProfileDataTable).FirstOrDefault()!;
                _logger.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }

        protected override void R_Saving(PMT01700LOO_UnitCharges_ChargesDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }
                if (poNewEntity.LCAL_UNIT)
                {
                    var tempData = R_Utility.R_ConvertCollectionToCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO, PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>(poNewEntity.ChargeItemList);

                    lcQuery = "CREATE TABLE #CHARGES_ITEMS ( " +
                                  "ISEQ INT, " +
                                  "CITEM_NAME VARCHAR(255), " +
                                  "IQTY INT, " +
                                  "NUNIT_PRICE NUMERIC(16,2), " +
                                  "NDISCOUNT NUMERIC(16,2), " +
                                  "NTOTAL_PRICE NUMERIC(16,2) " +
                                  ")";
                    _logger.LogDebug("CREATE TABLE #CHARGES_ITEMS");

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert((SqlConnection)loConn, "#CHARGES_ITEMS", tempData);
                    _logger.LogDebug("R_BulkInsert To TABLE #CHARGES_ITEMS");
                }

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

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
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poNewEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_MODE", DbType.String, 2, poNewEntity.CBILLING_MODE); //""
                loDb.R_AddCommandParameter(loCommand, "@CFEE_METHOD", DbType.String, 2, poNewEntity.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCommand, "@NFEE_AMT", DbType.Decimal, 512, poNewEntity.NFEE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_PERIOD", DbType.String, 2, poNewEntity.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCommand, "@NINVOICE_AMT", DbType.Decimal, 512, poNewEntity.NINVOICE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@LCAL_UNIT", DbType.Boolean, 3, poNewEntity.LCAL_UNIT);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

                loDb.R_AddCommandParameter(loCommand, "@IYEAR", DbType.Int32, 8, poNewEntity.IYEARS);
                 loDb.R_AddCommandParameter(loCommand, "@IDAY", DbType.Int32, 4, poNewEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTH", DbType.Int32, 4, poNewEntity.IMONTHS);                //CR10/07/2024
                loDb.R_AddCommandParameter(loCommand, "@LPRORATE", DbType.Boolean, 3, poNewEntity.LPRORATE);
                 loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);


                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);

                    var loEntity = R_Utility.R_ConvertTo<PMT01700LOO_UnitCharges_ChargesDetailDTO>(loDataTable).FirstOrDefault()!;
                    if (loEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CSEQ_NO = string.IsNullOrEmpty(loEntity.CSEQ_NO) ? "" : loEntity.CSEQ_NO;
                    }
                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));


            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();

        }

        protected override void R_Deleting(PMT01700LOO_UnitCharges_ChargesDetailDTO poEntity)
        {
            string lcMethodName = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string? lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string? lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcAction = "DELETE";

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO ?? ""); //

                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 20, poEntity.CCHARGE_MODE ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 30, poEntity.CBUILDING_ID); //
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID ", DbType.String, 20, poEntity.CUNIT_ID);

                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_TYPE", DbType.String, 20, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_ID", DbType.String, 20, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID ", DbType.String, 20, poEntity.CTAX_ID ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IYEAR", DbType.Int32, 8, poEntity.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_MODE", DbType.String, 2, poEntity.CEND_DATE); //""
                loDb.R_AddCommandParameter(loCommand, "@CFEE_METHOD", DbType.String, 2, poEntity.CFEE_METHOD);
                loDb.R_AddCommandParameter(loCommand, "@NFEE_AMT", DbType.Decimal, 512, poEntity.NFEE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_PERIOD", DbType.String, 2, poEntity.CINVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCommand, "@NINVOICE_AMT", DbType.Decimal, 512, poEntity.NINVOICE_AMT);
                loDb.R_AddCommandParameter(loCommand, "@LCAL_UNIT", DbType.Boolean, 3, poEntity.LCAL_UNIT);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                // loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, 8, poEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTH", DbType.Int32, 8, poEntity.IMONTHS);                //CR10/07/2024
                loDb.R_AddCommandParameter(loCommand, "@LPRORATE", DbType.Boolean, 3, poEntity.LPRORATE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }


        public List<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> GetChargesItemListDb(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            string? lcMethodName = nameof(GetChargesItemListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_ITEMS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_SEQ_NO", DbType.String, 3, poParameter.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
        }

    }
}
