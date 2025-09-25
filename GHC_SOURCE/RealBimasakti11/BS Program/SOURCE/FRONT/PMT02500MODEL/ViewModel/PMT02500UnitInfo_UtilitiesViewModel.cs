using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PMT02500Common.DTO._3._Unit_Info;
using PMT02500Common.Utilities;
using PMT02500Common.Utilities.Front.Unit_and_Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace PMT02500Model.ViewModel
{
    public class PMT02500UnitInfo_UtilitiesViewModel : R_ViewModel<PMT02500UnitInfoUnit_UtilitiesDetailDTO>
    {
        #region From Back
        private readonly PMT02500UnitInfo_UtilitiesModel _modelPMT02500UnitInfo_UtilitiesModel = new PMT02500UnitInfo_UtilitiesModel();
        public ObservableCollection<PMT02500UnitInfoUnit_UtilitiesListDTO> loListPMT02500UnitInfo_Utilities = new ObservableCollection<PMT02500UnitInfoUnit_UtilitiesListDTO>();
        public PMT02500UnitInfoUnit_UtilitiesDetailDTO? loEntityUnitInfo_Utilities = new PMT02500UnitInfoUnit_UtilitiesDetailDTO();
        public PMT02500FrontParameterForUnitInfo_UtilitiesDTO loParameterList = new PMT02500FrontParameterForUnitInfo_UtilitiesDTO();
        public List<PMT02500ComboBoxDTO> loComboBoxDataCCHARGES_TYPE { get; set; } = new List<PMT02500ComboBoxDTO>();
        public List<PMT02500ComboBoxStartInvoicePeriodYearDTO> loComboBoxDataCSTART_INV_PRD_YEAR { get; set; } = new List<PMT02500ComboBoxStartInvoicePeriodYearDTO>();
        public List<PMT02500ComboBoxStartInvoicePeriodMonthDTO> loComboBoxDataCSTART_INV_PRD_MONTH { get; set; } = new List<PMT02500ComboBoxStartInvoicePeriodMonthDTO>();
        public List<PMT02500ComboBoxCMeterNoDTO> loComboBoxDataCMETER_NO { get; set; } = new List<PMT02500ComboBoxCMeterNoDTO>();
        #endregion

        #region For Front

        public bool LTAXABLE = false;
        public PMT02500UtilitiesParameterUtilitiesListDTO oParameterUtilities = new PMT02500UtilitiesParameterUtilitiesListDTO();

        #endregion

        #region UnitInfo_Utilities
        public async Task GetUtilitiesList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    PMT02500GetUnitInfo_UtilitiesParameterDTO loParameter = new PMT02500GetUnitInfo_UtilitiesParameterDTO()
                    {
                        CPROPERTY_ID = loParameterList.CPROPERTY_ID,
                        CDEPT_CODE = loParameterList.CDEPT_CODE,
                        CREF_NO = loParameterList.CREF_NO,
                        CUNIT_ID = loParameterList.CUNIT_ID,
                        CFLOOR_ID = loParameterList.CFLOOR_ID,
                        CTRANS_CODE = loParameterList.CTRANS_CODE,
                        CBUILDING_ID = loParameterList.CBUILDING_ID,
                    };
                    var loResult = await _modelPMT02500UnitInfo_UtilitiesModel.GetUnitInfoListAsync(poParameter: loParameter);
                    loListPMT02500UnitInfo_Utilities = new ObservableCollection<PMT02500UnitInfoUnit_UtilitiesListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500UnitInfoUnit_UtilitiesDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelPMT02500UnitInfo_UtilitiesModel.R_ServiceGetRecordAsync(poEntity);

                loEntityUnitInfo_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500UnitInfoUnit_UtilitiesDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    //poNewEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                    //poNewEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                    //poNewEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                    //poNewEntity.CREF_NO = loParameterList.CREF_NO;
                    //poNewEntity.CUNIT_ID = loParameterList.CUNIT_ID;
                    //poNewEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                    //poNewEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                }

                var loResult = await _modelPMT02500UnitInfo_UtilitiesModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityUnitInfo_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500UnitInfoUnit_UtilitiesDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelPMT02500UnitInfo_UtilitiesModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCCHARGES_TYPE()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500UnitInfo_UtilitiesModel.GetComboBoxDataCCHARGES_TYPEAsync();
                loComboBoxDataCCHARGES_TYPE = new List<PMT02500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCSTART_INV_PRDYear()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500UnitInfo_UtilitiesModel.GetComboBoxDataCSTART_INV_PRDYearAsync();
                loComboBoxDataCSTART_INV_PRD_YEAR = new List<PMT02500ComboBoxStartInvoicePeriodYearDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCSTART_INV_PRDMonth()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500UnitInfo_UtilitiesModel.GetComboBoxDataCSTART_INV_PRDMonthAsync();
                loComboBoxDataCSTART_INV_PRD_MONTH = new List<PMT02500ComboBoxStartInvoicePeriodMonthDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<bool> GetComboBoxDataCMETER_NO(PMT02500GetUnitInfo_UtilitiesCMeterNoParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            bool llReturn = false;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CUTILITY_TYPE))
                {
                    var loResult = await _modelPMT02500UnitInfo_UtilitiesModel.GetComboBoxDataCMETER_NOAsync(poParameter);
                    loComboBoxDataCMETER_NO = new List<PMT02500ComboBoxCMeterNoDTO>(loResult);
                }
                llReturn = loComboBoxDataCMETER_NO.Any();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return llReturn;
        }

        #endregion


    }
}