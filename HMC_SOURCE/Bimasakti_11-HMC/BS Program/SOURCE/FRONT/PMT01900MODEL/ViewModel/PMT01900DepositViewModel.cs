using BaseAOC_BS11Common.DTO.Request.Request.GridList;
using BaseAOC_BS11Common.DTO.Request.Request.Single;
using BaseAOC_BS11Common.DTO.Response.GridList;
using BaseAOC_BS11Common.DTO.Response.List;
using BaseAOC_BS11Common.DTO.Response.Single;
using BaseAOC_BS11Common.Service;
using BaseAOC_BS11Model;
using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.DTO.Front;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01900Model.ViewModel
{
    public class PMT01900DepositViewModel : R_ViewModel<PMT01900Deposit_DepositDetailDTO>
    {
        #region From Back
        private readonly BaseAOCGetDataListUtilityModel _baseListModel = new BaseAOCGetDataListUtilityModel();
        private readonly BaseAOCGetDataGridListModel _baseGridModel = new BaseAOCGetDataGridListModel();
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();

        private readonly PMT01900Deposit_DepositDetailModel _model = new PMT01900Deposit_DepositDetailModel();
        public PMT01900Deposit_DepositDetailDTO oEntity = new PMT01900Deposit_DepositDetailDTO();
        public ObservableCollection<BaseAOCResponseAgreementDepositListDTO> oListDeposit = new ObservableCollection<BaseAOCResponseAgreementDepositListDTO>();
        public List<BaseAOCResponseCurrencyCodeListDTO> oComboBoxDataCurrency = new List<BaseAOCResponseCurrencyCodeListDTO>();
        public BaseAOCResponseGetAgreementDetailDTO oHeaderEntity = new BaseAOCResponseGetAgreementDetailDTO();

        #endregion

        #region For Front

        public BaseAOCFunctionUtility _AOCService = new BaseAOCFunctionUtility();
        public PMT01900ParameterFrontChangePageDTO oParameter = new PMT01900ParameterFrontChangePageDTO();

        #endregion

        #region Program


        public async Task GetDepositHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    var loParameter = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementDetailDTO>(oParameter);

                    var loResult = await _baseSingleDataModel.GetAgreementDetailAsync(poParameter: loParameter);
                    oHeaderEntity = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCurrency()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _baseListModel.GetDataCCurrencyCodeAsync();
                oComboBoxDataCurrency = new List<BaseAOCResponseCurrencyCodeListDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDepositList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CREF_NO))
                {
                    var loParameter = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementDepositListDTO>(oParameter);
                    var loResult = await _baseGridModel.GetAgreementDepositListAsync(poParameter: loParameter);
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                            item.DDEPOSIT_DATE = _AOCService.ConvertStringToDateTimeFormat(item.CDEPOSIT_DATE);
                    }
                    oListDeposit = new ObservableCollection<BaseAOCResponseAgreementDepositListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #region Core

        public async Task GetEntity(PMT01900Deposit_DepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                if (poEntity != null)
                {
                    var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                    loResult.DDEPOSIT_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CDEPOSIT_DATE);
                    oEntity = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01900Deposit_DepositDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {

                switch (peCRUDMode)
                {
                    case eCRUDMode.NormalMode:
                        break;
                    case eCRUDMode.AddMode:
                        poNewEntity.CPROPERTY_ID = oParameter.CPROPERTY_ID;
                        poNewEntity.CBUILDING_ID = oParameter.CBUILDING_ID;
                        poNewEntity.CTRANS_CODE = oParameter.CTRANS_CODE;
                        poNewEntity.CDEPT_CODE = oParameter.CDEPT_CODE;
                        poNewEntity.CREF_NO = oParameter.CREF_NO;
                        break;
                    case eCRUDMode.EditMode:
                        break;
                    case eCRUDMode.DeleteMode:
                        break;
                    default:
                        break;
                }

                poNewEntity.CDEPOSIT_DATE = _AOCService.ConvertDateTimeToStringFormat(poNewEntity.DDEPOSIT_DATE);

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loResult.DDEPOSIT_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CDEPOSIT_DATE);

                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01900Deposit_DepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPOSIT_DATE = _AOCService.ConvertDateTimeToStringFormat(poEntity.DDEPOSIT_DATE);
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

        #endregion



    }
}
