using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01700BACK;
using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using PMT01700COMMON.Interface;
using PMT01700COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01700LOO_OfferController : ControllerBase, IPMT01700LOO_Offer
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;
        public PMT01700LOO_OfferController(ILogger<PMT01700LOO_OfferController> logger)
        {
            //Initial and Get Logger
            LoggerPMT01700.R_InitializeLogger(logger);
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_InitializeAndGetActivitySource(nameof(PMT01700LOO_OfferController));

        }

        [HttpPost]
        public PMT01700LOO_Offer_SelectedOfferDTO GetAgreementDetail(PMT01700LOO_Offer_SelectedOfferDTO poParam)
        {
            string lcMethodName = nameof(GetAgreementDetail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            PMT01700LOO_Offer_SelectedOfferDTO loRtn = null;
            try
            {
                var loCls = new PMT01700LOO_OfferCls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poParam);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetAgreementDetailDb(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataIDType()
        {
            string lcMethodName = nameof(GetComboBoxDataIDType);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700ComboBoxDTO> loRtn = null;
            List<PMT01700ComboBoxDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700BaseParameterDTO();
                var loCls = new PMT01700LOO_OfferCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANGUAGE = R_BackGlobalVar.CULTURE;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetComboBoxDataIDTypeDb(loDbParameter);

                loUtilities = new();

                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataTaxType()
        {
            string lcMethodName = nameof(GetComboBoxDataTaxType);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700ComboBoxDTO> loRtn = null;
            List<PMT01700ComboBoxDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700BaseParameterDTO();
                var loCls = new PMT01700LOO_OfferCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANGUAGE = R_BackGlobalVar.CULTURE;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetComboBoxDataTaxTypeDb(loDbParameter);

                loUtilities = new();

                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public IAsyncEnumerable<PMT01700ResponseTenantCategoryDTO> GetComboBoxDataTenantCategory(PMT01700BaseParameterDTO poParam)
        {
            string lcMethodName = nameof(GetComboBoxDataTaxType);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700ResponseTenantCategoryDTO> loRtn = null;
            List<PMT01700ResponseTenantCategoryDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loCls = new PMT01700LOO_OfferCls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poParam);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetComboBoxDataTenantCategoryDb(poParam);
                loUtilities = new();
                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public PMT01700LOO_Offer_TenantDetailDTO GetTenantDetail(PMT01700LOO_Offer_TenantParamDTO poParam)
        {
            string lcMethodName = nameof(GetTenantDetail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            PMT01700LOO_Offer_TenantDetailDTO loRtn = null;
            try
            {
                var loCls = new PMT01700LOO_OfferCls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poParam);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetTenantDetailDb(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public PMT01700VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            string lcMethodName = nameof(GetVAR_GSM_TRANSACTION_CODE);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            PMT01700VarGsmTransactionCodeDTO loRtn = null;
            try
            {
                var loDbParameter = new PMT01700BaseParameterDTO();
                var loCls = new PMT01700LOO_OfferCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetVAR_GSM_TRANSACTION_CODEDb(loDbParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01700LOO_Offer_SelectedOfferDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            PMT01700LOO_OfferCls loCls;

            try
            {
                loCls = new PMT01700LOO_OfferCls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn!;
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT01700LOO_Offer_SelectedOfferDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01700LOO_Offer_SelectedOfferDTO> poParameter)
        {

            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT01700LOO_Offer_SelectedOfferDTO>();

            try
            {
                var loCls = new PMT01700LOO_OfferCls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));


            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01700LOO_Offer_SelectedOfferDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01700LOO_Offer_SelectedOfferDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT01700LOO_Offer_SelectedOfferDTO>? loRtn = null;
            PMT01700LOO_OfferCls loCls;

            try
            {
                loCls = new PMT01700LOO_OfferCls();
                loRtn = new R_ServiceSaveResultDTO<PMT01700LOO_Offer_SelectedOfferDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_ServiceSave");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn!;
        }
    }
}
