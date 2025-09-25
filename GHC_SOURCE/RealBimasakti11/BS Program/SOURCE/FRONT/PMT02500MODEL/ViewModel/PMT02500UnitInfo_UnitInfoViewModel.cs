using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BaseAOC_BS11Common.DTO.Response.GridList;
using BaseAOC_BS11Common.Service;
using PMT02500Common.DTO._3._Unit_Info;
using PMT02500Common.Utilities;
using PMT02500Common.Utilities.Front.Unit_and_Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT02500Model.ViewModel
{
    public class PMT02500UnitInfo_UnitInfoViewModel : R_ViewModel<PMT02500UnitInfoUnitInfoDetailDTO>
    {
        #region From Back
        private readonly PMT02500UnitInfo_UnitInfoModel _modelPMT02500UnitInfo_UnitInfoModel = new PMT02500UnitInfo_UnitInfoModel();
        public ObservableCollection<PMT02500UnitInfoUnitInfoListDTO> loListPMT02500UnitInfo_UnitInfo = new ObservableCollection<PMT02500UnitInfoUnitInfoListDTO>();
        public PMT02500UnitInfoUnitInfoListDTO? loEntityUnitInfo_UnitInfo = new PMT02500UnitInfoUnitInfoListDTO();
        public PMT02500UnitInfoHeaderDTO? loEntityUnitInfoHeader = new PMT02500UnitInfoHeaderDTO();
        public PMT02500GetHeaderParameterDTO loParameterList = new PMT02500GetHeaderParameterDTO();
        public PMT02500FrontParameterForUnitInfo_UtilitiesDTO loParameterForUtilitiesPage = new PMT02500FrontParameterForUnitInfo_UtilitiesDTO();
        #endregion

        #region For Front
        public string _cPropertyId = "";
        public bool lControlTabCharges = false;
        public bool lControlTabUnitAndUtilities = true;
        public BaseAOCFunctionUtility _AOCService = new BaseAOCFunctionUtility();
        public PMT02500UtilitiesParameterChargesListDTO oParameterChargesList = new PMT02500UtilitiesParameterChargesListDTO();
        #endregion

        #region UnitInfo_UnitInfo

        public async Task GetUnitInfoHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT02500UnitInfo_UnitInfoModel.GetUnitInfoHeaderAsync(poParameter: loParameterList);
                    loResult.DSTART_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                    loResult.DEND_DATE = _AOCService.ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                    loParameterList.CCHARGE_MODE ??= loResult.CCHARGE_MODE!;
                    loEntityUnitInfoHeader = loResult;
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
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT02500UnitInfo_UnitInfoModel.GetUnitInfoListAsync(poParameter: loParameterList);
                    loListPMT02500UnitInfo_UnitInfo = new ObservableCollection<PMT02500UnitInfoUnitInfoListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500UnitInfoUnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = poEntity.CTRANS_CODE ?? loParameterList.CTRANS_CODE;
                var loResult = await _modelPMT02500UnitInfo_UnitInfoModel.R_ServiceGetRecordAsync(poEntity);
                loEntityUnitInfo_UnitInfo = R_FrontUtility.ConvertObjectToObject<PMT02500UnitInfoUnitInfoListDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500UnitInfoUnitInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                switch (peCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        poNewEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                        poNewEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                        poNewEntity.CREF_NO = loParameterList.CREF_NO;
                        poNewEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                        break;
                    case eCRUDMode.EditMode:
                        poNewEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                        poNewEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                        break;
                    default:
                        break;
                }

                var loResult = await _modelPMT02500UnitInfo_UnitInfoModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityUnitInfo_UnitInfo = R_FrontUtility.ConvertObjectToObject<PMT02500UnitInfoUnitInfoListDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500UnitInfoUnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                poEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                poEntity.CREF_NO = loParameterList.CREF_NO;
                poEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                // Validation Before Delete
                await _modelPMT02500UnitInfo_UnitInfoModel.R_ServiceDeleteAsync(poEntity);
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