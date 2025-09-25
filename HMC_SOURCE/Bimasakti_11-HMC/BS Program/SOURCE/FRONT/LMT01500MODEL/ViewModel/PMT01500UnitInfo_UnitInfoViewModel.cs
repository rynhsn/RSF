using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PMT01500Common.DTO._3._Unit_Info;
using PMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace PMT01500Model.ViewModel
{
    public class PMT01500UnitInfo_UnitInfoViewModel : R_ViewModel<PMT01500UnitInfoUnitInfoDetailDTO>
    {
        #region From Back
        private readonly PMT01500UnitInfo_UnitInfoModel _modelPMT01500UnitInfo_UnitInfoModel = new PMT01500UnitInfo_UnitInfoModel();
        public ObservableCollection<PMT01500UnitInfoUnitInfoListDTO> loListPMT01500UnitInfo_UnitInfo = new ObservableCollection<PMT01500UnitInfoUnitInfoListDTO>();
        public PMT01500UnitInfoUnitInfoDetailDTO? loEntityUnitInfo_UnitInfo = new PMT01500UnitInfoUnitInfoDetailDTO();
        public PMT01500UnitInfoHeaderDTO? loEntityUnitInfoHeader = new PMT01500UnitInfoHeaderDTO();
        public PMT01500GetHeaderParameterDTO loParameterList = new PMT01500GetHeaderParameterDTO();
        public PMT01500FrontParameterForUnitInfo_UtilitiesDTO loParameterForUtilitiesPage = new PMT01500FrontParameterForUnitInfo_UtilitiesDTO();
        #endregion

        #region For Front
        public string _cPropertyId = "";
        #endregion

        #region UnitInfo_UnitInfo
        
        public async Task GetUnitInfoHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01500UnitInfo_UnitInfoModel.GetUnitInfoHeaderAsync(poParameter: loParameterList);
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
                    var loResult = await _modelPMT01500UnitInfo_UnitInfoModel.GetUnitInfoListAsync(poParameter: loParameterList);
                    loListPMT01500UnitInfo_UnitInfo = new ObservableCollection<PMT01500UnitInfoUnitInfoListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT01500UnitInfoUnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelPMT01500UnitInfo_UnitInfoModel.R_ServiceGetRecordAsync(poEntity);
                loEntityUnitInfo_UnitInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01500UnitInfoUnitInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
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

                var loResult = await _modelPMT01500UnitInfo_UnitInfoModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityUnitInfo_UnitInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01500UnitInfoUnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                poEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                poEntity.CREF_NO = loParameterList.CREF_NO;
                poEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                // Validation Before Delete
                await _modelPMT01500UnitInfo_UnitInfoModel.R_ServiceDeleteAsync(poEntity);
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