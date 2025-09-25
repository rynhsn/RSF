using BaseAOC_BS11Common.DTO.Front;
using BaseAOC_BS11Common.DTO.Request.Request.List;
using BaseAOC_BS11Common.DTO.Request.Request.Single;
using BaseAOC_BS11Common.DTO.Response.Single;
using BaseAOC_BS11Common.Service;
using BaseAOC_BS11Model;
using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.DTO.Front;
using PMT01900FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace PMT01900Model.ViewModel
{
    public class PMT01900LOIViewModel : R_ViewModel<PMT01900LOI_SelectedLOIDTO>
    {
        #region From Back

        private readonly PMT01900LOI_SelectedLOIModel _model = new PMT01900LOI_SelectedLOIModel();
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();
        public PMT01900LOI_SelectedLOIDTO oEntity = new PMT01900LOI_SelectedLOIDTO();
        public BaseAOCResponseTransCodeInfoDTO oVarGSMTransactionCode = new BaseAOCResponseTransCodeInfoDTO();
        //public PMT01100LOO_Offer_SelectedOfferDTO oTempDataForAdd = new PMT01100LOO_Offer_SelectedOfferDTO();

        #endregion

        #region For Front

        public R_ILocalizer<Resources_PMT01900_Class>? _localizer { get; set; }
        public BaseAOCFunctionUtility _AOCService = new BaseAOCFunctionUtility();
        public PMT01900ParameterFrontChangePageDTO oParameter = new PMT01900ParameterFrontChangePageDTO();


        public bool lControlButtonSubmit = false;
        public bool lControlButtonRedraft = false;

        public bool lControlCRUDMode = true;
        public bool lControlExistingTenant = true;
        public BaseAOCFrontControlYMDDTO oControlYMD = new BaseAOCFrontControlYMDDTO();

        #endregion

        #region LOI - LOI

        #region Core

        public async Task GetEntity(PMT01900LOI_SelectedLOIDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                loResult.DREF_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CREF_DATE);
                loResult.DFOLLOW_UP_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CFOLLOW_UP_DATE);
                loResult.DSTART_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                loResult.DEND_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CEND_DATE);

                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01900LOI_SelectedLOIDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poNewEntity.CPROPERTY_ID = oParameter.CPROPERTY_ID;
                    poNewEntity.CTRANS_CODE = oParameter.CTRANS_CODE;
                }

                poNewEntity.CFOLLOW_UP_DATE = _AOCService.ConvertDateTimeToStringFormat(poNewEntity.DFOLLOW_UP_DATE);
                //poNewEntity.CHAND_OVER_DATE = _AOCService.ConvertDateTimeToStringFormat(poNewEntity.DHAND_OVER_DATE);
                poNewEntity.CSTART_DATE = _AOCService.ConvertDateTimeToStringFormat(poNewEntity.DSTART_DATE);
                poNewEntity.CEND_DATE = _AOCService.ConvertDateTimeToStringFormat(poNewEntity.DEND_DATE);
                poNewEntity.CREF_DATE = _AOCService.ConvertDateTimeToStringFormat(poNewEntity.DREF_DATE);

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loResult.DREF_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CREF_DATE);
                loResult.DFOLLOW_UP_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CFOLLOW_UP_DATE);
                loResult.DSTART_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                loResult.DEND_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                //loResult.DHAND_OVER_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CHAND_OVER_DATE);

                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01900LOI_SelectedLOIDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        public async Task<BaseAOCResponseGetAgreementDetailDTO> GetAgreementDetail(BaseAOCParameterRequestGetAgreementDetailDTO poParameter)
        {
            var loException = new R_Exception();
            BaseAOCResponseGetAgreementDetailDTO loReturn = new BaseAOCResponseGetAgreementDetailDTO();

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CREF_NO))
                {
                    loReturn = await _baseSingleDataModel.GetAgreementDetailAsync(poParameter);
                }
                else
                {
                    loReturn = new BaseAOCResponseGetAgreementDetailDTO();
                }
            }
            catch (Exception e)
            {
                loException.Add(e);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }


        #region Proses Update Status

        public async Task<BaseAOCResponseUpdateAgreementStatusDTO> ProsesUpdateAgreementStatus(string pcStatus)
        {
            BaseAOCResponseUpdateAgreementStatusDTO loReturn = new BaseAOCResponseUpdateAgreementStatusDTO();
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oEntity.CREF_NO))
                {
                    var loParam = new BaseAOCParameterRequestUpdateAgreementStatusDTO()
                    {
                        CPROPERTY_ID = oParameter.CPROPERTY_ID!,
                        CDEPT_CODE = oParameter.CDEPT_CODE!,
                        CTRANS_CODE = oParameter.CTRANS_CODE!,
                        CREF_NO = oEntity.CREF_NO,
                        CNEW_STATUS = pcStatus,

                    };

                    var loResult = await _baseSingleDataModel.ProsesUpdateAgreementStatusAsync(loParam);
                    loReturn = loResult;
                }
                else
                {
                    loReturn.LSUCCESS = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        #endregion

        public async Task GetVAR_GSM_TRANSACTION_CODE()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CTRANS_CODE))
                {
                    BaseAOCParameterRequestGetTransCodeInfoDTO loParam = new BaseAOCParameterRequestGetTransCodeInfoDTO()
                    {
                        CTRANS_CODE = oParameter.CTRANS_CODE,
                    };

                    var loResult = await _baseSingleDataModel.GetTransCodeInfoAsync(loParam);
                    oVarGSMTransactionCode = loResult;
                }
                else
                {
                    oVarGSMTransactionCode = new BaseAOCResponseTransCodeInfoDTO();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion


    }
}
