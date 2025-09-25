using BaseAOC_BS11Common.DTO.Request.Request.GridList;
using BaseAOC_BS11Common.DTO.Request.Request.List;
using BaseAOC_BS11Common.DTO.Request.Request.List.Enum;
using BaseAOC_BS11Common.DTO.Response.GridList;
using BaseAOC_BS11Common.DTO.Response.List;
using BaseAOC_BS11Common.DTO.Response.Single;
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
    public class PMT01900Unit_UtilitiesViewModel : R_ViewModel<PMT01900Unit_UtilitiesDetailDTO>
    {
        #region From Back
        private readonly BaseAOCGetDataListUtilityModel _baseListModel = new BaseAOCGetDataListUtilityModel();
        private readonly BaseAOCGetDataGridListModel _baseGridModel = new BaseAOCGetDataGridListModel();
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();

        private readonly PMT01900Unit_UtilitiesDetailModel _model = new PMT01900Unit_UtilitiesDetailModel();
        public PMT01900Unit_UtilitiesDetailDTO oEntity = new PMT01900Unit_UtilitiesDetailDTO();
        public ObservableCollection<BaseAOCResponseAgreementUtilitiesListDTO> oListUtilities = new ObservableCollection<BaseAOCResponseAgreementUtilitiesListDTO>();
        public List<BaseAOCResponseComboBoxDTO> oComboBoxDataCCHARGES_TYPE = new List<BaseAOCResponseComboBoxDTO>();
        public List<BaseAOCResponseUtilitiesCMeterNoDTO> oComboBoxDataCMETER_NO = new List<BaseAOCResponseUtilitiesCMeterNoDTO>();

        #endregion

        #region For Front

        public BaseAOCResponseGetAgreementDetailDTO oHeaderUtilitiesEntity = new BaseAOCResponseGetAgreementDetailDTO();
        public BaseAOCParameterRequestGetAgreementUtilitiesListDTO oParameterUtilitiesList = new BaseAOCParameterRequestGetAgreementUtilitiesListDTO();
        public bool LTAXABLE;
        #endregion

        #region Program

        public async Task<bool> GetComboBoxDataCMETER_NO(BaseAOCParameterRequestUtilitiesCMeterNoDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            bool llReturn = false;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CUTILITY_TYPE) && !string.IsNullOrEmpty(poParameter.CUNIT_ID))
                {
                    poParameter.EMODE = BaseAOCEnumUtilitiesCMeterNO.OtherUnit;
                    var loResult = await _baseListModel.GetDataCMETER_NOAsync(poParameter: poParameter);
                    oComboBoxDataCMETER_NO = new List<BaseAOCResponseUtilitiesCMeterNoDTO>(loResult.Data);
                }

                llReturn = oComboBoxDataCMETER_NO.Any();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return llReturn;
        }

        public async Task GetComboBoxDataCCHARGES_TYPE()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _baseListModel.GetDataCChargesTypeAsync();
                oComboBoxDataCCHARGES_TYPE = new List<BaseAOCResponseComboBoxDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUtilitiesList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameterUtilitiesList.CUNIT_ID) && !string.IsNullOrEmpty(oParameterUtilitiesList.CFLOOR_ID))
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementUtilitiesListDTO>(oParameterUtilitiesList);
                    var loResult = await _baseGridModel.GetAgreementUtilitiesListAsync(poParameter: loParam);
                    oListUtilities = new ObservableCollection<BaseAOCResponseAgreementUtilitiesListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Core

        public async Task GetEntity(PMT01900Unit_UtilitiesDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01900Unit_UtilitiesDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                switch (peCRUDMode)
                {
                    case eCRUDMode.NormalMode:
                        break;
                    case eCRUDMode.AddMode:
                        poNewEntity.CPROPERTY_ID = oParameterUtilitiesList.CPROPERTY_ID;
                        poNewEntity.CDEPT_CODE = oParameterUtilitiesList.CDEPT_CODE;
                        poNewEntity.CTRANS_CODE = oParameterUtilitiesList.CTRANS_CODE;
                        poNewEntity.CREF_NO = oParameterUtilitiesList.CREF_NO;
                        poNewEntity.CUNIT_ID = oParameterUtilitiesList.CUNIT_ID;
                        poNewEntity.CFLOOR_ID = oParameterUtilitiesList.CFLOOR_ID;
                        poNewEntity.CBUILDING_ID = oParameterUtilitiesList.CBUILDING_ID;
                        break;
                    case eCRUDMode.EditMode:
                        break;
                    case eCRUDMode.DeleteMode:
                        break;
                    default:
                        break;
                }

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01900Unit_UtilitiesDetailDTO poEntity)
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

        #endregion

    }
}
