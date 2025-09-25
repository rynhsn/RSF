using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using R_BlazorFrontEnd;
using PMT01600COMMON.DTO.CRUDBase;
using BaseAOC_BS11Model;
using BaseAOC_BS11Common.DTO.Response.Single;
using PMT01600COMMON.DTO.Front;
using BaseAOC_BS11Common.DTO.Request.Request.Single;
using BaseAOC_BS11Common.DTO.Request.Request.GridList;
using BaseAOC_BS11Common.DTO.Response.GridList;

namespace PMT01600Model.ViewModel
{
    public class PMT01600UnitCharges_UnitViewModel : R_ViewModel<PMT01600Unit_UnitInfoDetailDTO>
    {
        #region From Back

        private readonly BaseAOCGetDataGridListModel _baseGridModel = new BaseAOCGetDataGridListModel();
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();

        private readonly PMT01600Unit_UnitInfoDetailModel _model = new PMT01600Unit_UnitInfoDetailModel();
        public BaseAOCResponseAgreementUnitInfoListDTO oEntity = new BaseAOCResponseAgreementUnitInfoListDTO();
        public ObservableCollection<BaseAOCResponseAgreementUnitInfoListDTO> oListUnitInfo = new ObservableCollection<BaseAOCResponseAgreementUnitInfoListDTO>();
        public BaseAOCResponseGetAgreementDetailDTO oHeaderEntity = new BaseAOCResponseGetAgreementDetailDTO();

        #endregion

        #region For Front
        public PMT01600ParameterFrontChangePageDTO oParameter = new PMT01600ParameterFrontChangePageDTO();
        public PMT01600ParameterFrontChangePageToChargesDTO oParameterChargesList = new PMT01600ParameterFrontChangePageToChargesDTO();

        public bool lControlTabUnitAndUtilities = true;
        public bool lControlTabCharges = true;

        #endregion

        #region Program

        public async Task GetUnitChargesHeader()
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

        public async Task GetUnitInfoList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CREF_NO))
                {
                    var loParameter = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementUnitInfoListDTO>(oParameter);
                    var loResult = await _baseGridModel.GetAgreementUnitInfoListAsync(poParameter: loParameter);
                    oListUnitInfo = new ObservableCollection<BaseAOCResponseAgreementUnitInfoListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Core

        public async Task GetEntity(PMT01600Unit_UnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = poEntity.CDEPT_CODE ?? oParameter.CDEPT_CODE!;
                poEntity.CTRANS_CODE = poEntity.CTRANS_CODE ?? oParameter.CTRANS_CODE!;
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                oEntity = R_FrontUtility.ConvertObjectToObject<BaseAOCResponseAgreementUnitInfoListDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01600Unit_UnitInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                oEntity = R_FrontUtility.ConvertObjectToObject<BaseAOCResponseAgreementUnitInfoListDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01600Unit_UnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                poEntity.CTRANS_CODE = oParameter.CTRANS_CODE!;
                poEntity.CDEPT_CODE = oParameter.CDEPT_CODE!;
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
