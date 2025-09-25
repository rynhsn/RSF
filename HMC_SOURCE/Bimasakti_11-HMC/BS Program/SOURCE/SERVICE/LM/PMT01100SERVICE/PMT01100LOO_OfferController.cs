using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01100Back;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.Interface;
using PMT01100Common.Logs;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01100Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01100LOO_OfferController : ControllerBase, IPMT01100LOO_Offer
    {

        private readonly LoggerPMT01100? _loggerPMT01100;
        private readonly ActivitySource _activitySource;

        public PMT01100LOO_OfferController(ILogger<PMT01100LOO_OfferController> logger)
        {
            LoggerPMT01100.R_InitializeLogger(logger);
            _loggerPMT01100 = LoggerPMT01100.R_GetInstanceLogger();
            _activitySource = PMT01100Activity.R_InitializeAndGetActivitySource(nameof(PMT01100LOO_OfferController));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataIDType()
        {
            string? lcMethod = nameof(GetComboBoxDataIDType);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesWithCultureIDParameterDTO? loDbParameterInternal;
            List<PMT01100ComboBoxDTO> loRtnTmp;
            PMT01100LOO_OfferCls loCls;
            IAsyncEnumerable<PMT01100ComboBoxDTO>? loReturn = null;
            PMT01100Utilities? loUtilities = null;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01100.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CULTURE_ID = R_BackGlobalVar.CULTURE;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@PMT01100AgreementCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataIDTypeDb(poParameterInternal: loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01100.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01100.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01100GetListStream(loRtnTmp);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataTaxType()
        {

            string? lcMethod = nameof(GetComboBoxDataTaxType);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesWithCultureIDParameterDTO? loDbParameterInternal;
            List<PMT01100ComboBoxDTO> loRtnTmp;
            PMT01100LOO_OfferCls loCls;
            IAsyncEnumerable<PMT01100ComboBoxDTO>? loReturn = null;
            PMT01100Utilities? loUtilities = null;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01100.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CULTURE_ID = R_BackGlobalVar.CULTURE;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@PMT01100AgreementCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataTaxTypeDb(poParameterInternal: loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01100.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01100.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01100GetListStream(loRtnTmp);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01100ResponseTenantCategoryDTO> GetComboBoxDataTenantCategory(PMT01100RequestTenantCategoryDTO poParam)
        {
            string? lcMethod = nameof(GetComboBoxDataTenantCategory);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesParameterDTO? loDbParameterInternal;
            PMT01100RequestTenantCategoryDTO? loDbParameter;
            List<PMT01100ResponseTenantCategoryDTO> loRtnTmp;
            PMT01100LOO_OfferCls loCls;
            IAsyncEnumerable<PMT01100ResponseTenantCategoryDTO>? loReturn = null;
            PMT01100Utilities? loUtilities = null;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();

                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01100.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter = poParam;
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@PMT01100UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataTenantCategoryDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01100.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01100.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01100.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01100GetListStream(loRtnTmp);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        [HttpPost]
        public PMT01100VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            string? lcMethod = nameof(GetVAR_GSM_TRANSACTION_CODE);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100VarGsmTransactionCodeDTO? loReturn = new PMT01100VarGsmTransactionCodeDTO();
            PMT01100UtilitiesParameterCompanyDTO loDbParameterInternal;
            PMT01100LOO_OfferCls loCls;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                loDbParameterInternal = new();
                _loggerPMT01100.LogDebug("{@PMT01100UnitListCls}", loCls);
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetVAR_GSM_TRANSACTION_CODEDb(poParameter: loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);


            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT01100LOO_Offer_SelectedOfferDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01100LOO_Offer_SelectedOfferDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT01100LOO_Offer_SelectedOfferDTO>();

            try
            {
                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls in method {0}", lcMethod));
                var loCls = new PMT01100LOO_OfferCls();
                _loggerPMT01100.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(poParameter.Entity.CCOMPANY_ID))
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);
                poParameter.Entity.CTRANS_CODE = !string.IsNullOrEmpty(poParameter.Entity.CTRANS_CODE) ? poParameter.Entity.CTRANS_CODE : "802041";

                _loggerPMT01100.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerPMT01100.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01100LOO_Offer_SelectedOfferDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01100LOO_Offer_SelectedOfferDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT01100LOO_Offer_SelectedOfferDTO> loReturn = new();
            try
            {
                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01100LOO_OfferCls? loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);
                _loggerPMT01100.LogInfo(string.Format("Checking Data From Profile, and edit if Profile has empty string or null in method {0}", lcMethod));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01100LOO_Offer_SelectedOfferDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceDeleteResultDTO? loReturn = new();
            try
            {
                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01100LOO_OfferCls? loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Perform the delete operation using the R_Delete method of Cls in method {0}", lcMethod));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }

    }

}
