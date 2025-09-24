using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT01700COMMON.Logs;
using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.Response;
using System.Reflection.Metadata;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;

namespace PMT01700BACK
{
    public class PMT01700LOO_OfferCls : R_BusinessObject<PMT01700LOO_Offer_SelectedOfferDTO>
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;

        private readonly RSP_PM_MAINTAIN_AGREEMENTResources.Resources_Dummy_Class _oRSPMAINTAINAGREEMENT = new();
        private readonly RSP_PM_MAINTAIN_TENANTResources.Resources_Dummy_Class _oRSPMAINTAINTENANT = new();
        private readonly RSP_PM_MAINTAIN_AGREEMENT_UNITResources.Resources_Dummy_Class _oRSPMAINTAINUNIT = new();


        public PMT01700LOO_OfferCls()
        {
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_GetInstanceActivitySource();
        }
        public PMT01700VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODEDb(PMT01700BaseParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetVAR_GSM_TRANSACTION_CODEDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01700VarGsmTransactionCodeDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, "802043");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() != null ? R_Utility.R_ConvertTo<PMT01700VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() : new PMT01700VarGsmTransactionCodeDTO();
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

        public List<PMT01700ResponseTenantCategoryDTO> GetComboBoxDataTenantCategoryDb(PMT01700BaseParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetComboBoxDataTenantCategoryDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01700ResponseTenantCategoryDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_GET_CATEGORY_LIST";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCATEGORY_TYPE", DbType.String, 2, "20");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700ResponseTenantCategoryDTO>(loDataTable).ToList();
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

        public List<PMT01700ComboBoxDTO> GetComboBoxDataTaxTypeDb(PMT01700BaseParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetComboBoxDataTaxTypeDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01700ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
               "(@BIMASAKTI, @CCOMPANY_ID, @BS_LEASE_MODE, @NONE, @CULTURE_ID);";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_LEASE_MODE", DbType.String, 20, "_BS_TAX_FOR_TYPE");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParameterInternal.CLANGUAGE);
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

        public List<PMT01700ComboBoxDTO> GetComboBoxDataIDTypeDb(PMT01700BaseParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetVAR_GSM_TRANSACTION_CODEDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01700ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
              "(@BIMASAKTI, @CCOMPANY_ID, @BS_LEASE_MODE, @NONE, @CULTURE_ID);";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@BS_LEASE_MODE", DbType.String, 15, "_BS_ID_TYPE");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParameterInternal.CLANGUAGE);
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
        protected override void R_Deleting(PMT01700LOO_Offer_SelectedOfferDTO poEntity)
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

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 30, poEntity.CREF_DATE); //
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 30, poEntity.CDOC_NO); //
                loDb.R_AddCommandParameter(loCommand, "@CDOC_DATE", DbType.String, 30, poEntity.CDOC_DATE); //
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);

                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE ", DbType.String, 20, poEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 20, poEntity.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IDAYS", DbType.Int32, 8, poEntity.IDAYS);
                loDb.R_AddCommandParameter(loCommand, "@IMONTHS", DbType.Int32, 8, poEntity.IMONTHS);
                loDb.R_AddCommandParameter(loCommand, "@IYEARS", DbType.Int32, 8, poEntity.IYEARS);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_DESCRIPTION", DbType.String, 510, poEntity.CUNIT_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, int.MaxValue, poEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLEASE_MODE", DbType.String, 2, poEntity.CLEASE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poEntity.CCHARGE_MODE);  //loDb.R_AddCommandParameter(loCommand, "@NCOMMON_AREA_SIZE", DbType.Int32, 10, poEntity.NCOMMON_AREA_SIZE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                //CR 14-11-2024
                loDb.R_AddCommandParameter(loCommand, "@CSTART_TIME", DbType.String, 8, poEntity.CSTART_TIME);
                loDb.R_AddCommandParameter(loCommand, "@CEND_TIME", DbType.String, 8, poEntity.CEND_TIME);


                /*
                loDb.R_AddCommandParameter(loCommand, "@CORIGINAL_REF_NO", DbType.String, 30, poEntity.CORIGINAL_REF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CFOLLOW_UP_DATE", DbType.String, 30, poEntity.CFOLLOW_UP_DATE);
                loDb.R_AddCommandParameter(loCommand, "@LWITH_FO ", DbType.Boolean, 30, poEntity.LWITH_FO );
                loDb.R_AddCommandParameter(loCommand, "@CHAND_OVER_DATE", DbType.String, 20, poEntity.CHAND_OVER_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NBOOKING_FEE", DbType.Decimal, 30, poEntity.NBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CTC_CODE", DbType.String, 32, poEntity.CTC_CODE);
                */
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

        protected override PMT01700LOO_Offer_SelectedOfferDTO R_Display(PMT01700LOO_Offer_SelectedOfferDTO poEntity)
        {
            string? lcMethod = nameof(R_Display);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01700LOO_Offer_SelectedOfferDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                loDb = new();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loProfileDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCommand, true);
                loRtn = R_Utility.R_ConvertTo<PMT01700LOO_Offer_SelectedOfferDTO>(loProfileDataTable).FirstOrDefault()!;
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
        public PMT01700LOO_Offer_TenantDetailDTO GetTenantDetailDb(PMT01700LOO_Offer_TenantParamDTO poParameter)
        {
            string? lcMethod = nameof(GetTenantDetailDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01700LOO_Offer_TenantDetailDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_TENANT_DETAIL";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poParameter.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<PMT01700LOO_Offer_TenantDetailDTO>(loDataTable).FirstOrDefault() != null ?
                    R_Utility.R_ConvertTo<PMT01700LOO_Offer_TenantDetailDTO>(loDataTable).FirstOrDefault() :
                    new PMT01700LOO_Offer_TenantDetailDTO();
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

        public PMT01700LOO_Offer_SelectedOfferDTO GetAgreementDetailDb(PMT01700LOO_Offer_SelectedOfferDTO poParameter)
        {
            string? lcMethod = nameof(GetAgreementDetailDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT01700LOO_Offer_SelectedOfferDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<PMT01700LOO_Offer_SelectedOfferDTO>(loDataTable).FirstOrDefault() != null ?
                    R_Utility.R_ConvertTo<PMT01700LOO_Offer_SelectedOfferDTO>(loDataTable).FirstOrDefault() :
                    new PMT01700LOO_Offer_SelectedOfferDTO();
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

        protected override void R_Saving(PMT01700LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string? lcMethod = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            //PMT01100LOO_Offer_SelectedOfferDTO? loResult = null;
            R_Db? loDb = new R_Db();
            DbConnection? loConn = null;

            try
            {
                var loDataUnitList = poNewEntity.ODATA_UNIT_LIST ?? new List<PMT01700LOO_Offer_SelectedOtherDataUnitListDTO>();
                loConn = loDb.GetConnection();
                switch (poNewEntity.CMODE_CRUD)
                {
                    case "1":
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        break;
                    case "2":
                        SaveSPMaintainTenant(ref poNewEntity, poCRUDMode, loConn);
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        break;

                    case "3":
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        if (loDataUnitList.Any())
                        {
                            foreach (PMT01700LOO_Offer_SelectedOtherDataUnitListDTO loDataUnit in loDataUnitList)
                            {
                                loDataUnit.CREF_NO = poNewEntity.CREF_NO;
                                SaveSPMaintainAgreementUnit(poDataUnit: loDataUnit, poCRUDMode: poCRUDMode, poConnection: loConn);
                            }
                        }
                        break;
                    case "4":
                        SaveSPMaintainTenant(ref poNewEntity, poCRUDMode, loConn);
                        SaveSPMaintainAgreement(ref poNewEntity, poCRUDMode, loConn);
                        if (loDataUnitList.Any())
                        {
                            foreach (PMT01700LOO_Offer_SelectedOtherDataUnitListDTO loDataUnit in loDataUnitList)
                            {
                                loDataUnit.CREF_NO = poNewEntity.CREF_NO;
                                SaveSPMaintainAgreementUnit(poDataUnit: loDataUnit, poCRUDMode: poCRUDMode, poConnection: loConn);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
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
            loEx.ThrowExceptionIfErrors();
        }

        private void SaveSPMaintainTenant(ref PMT01700LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
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
                loConn = poConnection;
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";

                lcQuery = "RSP_PM_MAINTAIN_TENANT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;


                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_NAME", DbType.String, 100, poNewEntity.CTENANT_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CADDRESS", DbType.String, 255, poNewEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CCITY_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CPROVINCE_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CCOUNTRY_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CZIP_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CEMAIL", DbType.String, 100, poNewEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE1", DbType.String, 30, poNewEntity.CPHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CPHONE2", DbType.String, 30, poNewEntity.CPHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_CATEGORY_ID", DbType.String, 20, poNewEntity.CTENANT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_TYPE_ID", DbType.String, 20, "03");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_NAME", DbType.String, 100, poNewEntity.CATTENTION1_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_POSITION", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_EMAIL", DbType.String, 100, poNewEntity.CATTENTION1_EMAIL);

                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_MOBILE_PHONE1", DbType.String, 30, poNewEntity.CATTENTION1_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION1_MOBILE_PHONE2", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_NAME", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_POSITION", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_EMAIL", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_MOBILE_PHONE1", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CATTENTION2_MOBILE_PHONE2", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CPAYMENT_TERM_CODE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, "");// ini ada tapi di petik"
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", DbType.String, 8, poNewEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLOB_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CFAMILY_CARD", DbType.String, 40, "");

                loDb.R_AddCommandParameter(loCommand, "@CTAX_TYPE", DbType.String, 2, poNewEntity.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@LPPH", DbType.Boolean, 1, false);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 40, poNewEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_NAME", DbType.String, 100, poNewEntity.CTAX_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CID_TYPE", DbType.String, 2, poNewEntity.CID_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CID_NO", DbType.String, 80, poNewEntity.CID_NO);
                loDb.R_AddCommandParameter(loCommand, "@CID_EXPIRED_DATE", DbType.String, 8, poNewEntity.CID_EXPIRED_DATE);

                loDb.R_AddCommandParameter(loCommand, "@CTAX_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 18, 0);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ADDRESS", DbType.String, 255, poNewEntity.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE1", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE2", DbType.String, 30, "");
                loDb.R_AddCommandParameter(loCommand, "@CTAX_EMAIL", DbType.String, 100, "");
                loDb.R_AddCommandParameter(loCommand, "@CCUSTOMER_TYPE", DbType.String, 2, "01");

                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                //CR-19-07-2024
                loDb.R_AddCommandParameter(loCommand, "@CTAX_EMAIL2", DbType.String, 100, "");
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
            loException.ThrowExceptionIfErrors();
        }

        private void SaveSPMaintainAgreement(ref PMT01700LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            string? lcMethodName = nameof(SaveSPMaintainAgreement);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCommand = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                //Set Action 
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                loConn = poConnection;
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);

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
                loDb.R_AddCommandParameter(loCommand, "@CORIGINAL_REF_NO", DbType.String, 30, poNewEntity.CORIGINAL_REF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CFOLLOW_UP_DATE", DbType.String, 8, poNewEntity.CFOLLOW_UP_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEXPIRED_DATE", DbType.String, 8, poNewEntity.CEXPIRED_DATE);
                loDb.R_AddCommandParameter(loCommand, "@LWITH_FO", DbType.Boolean, 1, poNewEntity.LWITH_FO);
              //  loDb.R_AddCommandParameter(loCommand, "@CHAND_OVER_DATE", DbType.String, 8, poNewEntity.CHAND_OVER_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_TYPE", DbType.String, 20, poNewEntity.CBILLING_RULE_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CBILLING_RULE_CODE", DbType.String, 2, poNewEntity.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NBOOKING_FEE", DbType.Decimal, int.MaxValue, poNewEntity.NBOOKING_FEE);
                loDb.R_AddCommandParameter(loCommand, "@CTC_CODE", DbType.String, 20, poNewEntity.CTC_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_TRANS_CODE", DbType.String, 10, poNewEntity.CLINK_TRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_REF_NO", DbType.String, 30, poNewEntity.CLINK_REF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                //CR 19-07-2024
                loDb.R_AddCommandParameter(loCommand, "@IHOURS", DbType.Int32, int.MaxValue, poNewEntity.IHOURS);
                //CR 14-11-2024
                loDb.R_AddCommandParameter(loCommand, "@CSTART_TIME", DbType.String, 8, poNewEntity.CSTART_TIME);
                loDb.R_AddCommandParameter(loCommand, "@CEND_TIME", DbType.String, 8, poNewEntity.CEND_TIME);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {

                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    var loData = R_Utility.R_ConvertTo<PMT01700LOO_Offer_SelectedOfferDTO>(loDataTable).FirstOrDefault()!;
                    if (poNewEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREF_NO = loData.CREF_NO;
                    }
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.LogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SaveSPMaintainAgreementUnit(PMT01700LOO_Offer_SelectedOtherDataUnitListDTO poDataUnit, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            string? lcMethod = nameof(SaveSPMaintainAgreementUnit);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand? loCommand = null;
            string lcAction = "";
            DbConnection? loConn = null;

            try
            {
                lcAction = (poCRUDMode == eCRUDMode.AddMode) ? "ADD" : "EDIT";
                loConn = poConnection;
                loCommand = loDb.GetCommand();

                _logger!.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_UNIT";
                _logger.LogDebug("{@ObjectTextQuery}", lcQuery);
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                _logger.LogDebug("{@ObjectDbCommand}", loCommand);


                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poDataUnit.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poDataUnit.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poDataUnit.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poDataUnit.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poDataUnit.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poDataUnit.COTHER_UNIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poDataUnit.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poDataUnit.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@NACTUAL_AREA_SIZE", DbType.Decimal, int.MaxValue, poDataUnit.NACTUAL_AREA_SIZE);
                loDb.R_AddCommandParameter(loCommand, "@NCOMMON_AREA_SIZE", DbType.Decimal, int.MaxValue, poDataUnit.NCOMMON_AREA_SIZE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poDataUnit.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //loDb.SqlExecNonQuery(loConn, loCommand, false);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _logger.LogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger!.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }
}
