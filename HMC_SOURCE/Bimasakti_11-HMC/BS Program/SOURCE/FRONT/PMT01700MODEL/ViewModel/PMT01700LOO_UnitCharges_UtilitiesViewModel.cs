using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL.ViewModel
{
    public class PMT01700LOO_UnitCharges_UtilitiesViewModel : R_ViewModel<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>
    {
        public ObservableCollection<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO> oListUtilities = new ObservableCollection<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>();
        private readonly PMT01700LOO_UnitCharges_UtilitiesModel _model = new PMT01700LOO_UnitCharges_UtilitiesModel();
        public PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO oEntityUtilities = new PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO();
        public List<PMT01700ComboBoxDTO> oComboBoxDataCCHARGES_TYPE = new List<PMT01700ComboBoxDTO>();
        public List<PMT01700ResponseUtilitiesCMeterNoParameterDTO> oComboBoxDataCMETER_NO = new List<PMT01700ResponseUtilitiesCMeterNoParameterDTO>();
        public PMT01700ParameterFrontChangePageDTO oParameterUtilities = new PMT01700ParameterFrontChangePageDTO();

        public bool LTAXABLE = false;

        #region Program
        public async Task<bool> GetComboBoxDataCMETER_NO(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            bool llReturn = false;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CUTILITY_TYPE) && !string.IsNullOrEmpty(poParameter.COTHER_UNIT_ID))
                {
                    var loResult = await _model.GetComboBoxDataCMETER_NOAsync(poParameter: poParameter);
                    oComboBoxDataCMETER_NO = new List<PMT01700ResponseUtilitiesCMeterNoParameterDTO>(loResult.Data);
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
        public async Task GetUtilitiesList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, oParameterUtilities.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CDEPT_CODE, oParameterUtilities.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CTRANS_CODE, oParameterUtilities.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CREF_NO, oParameterUtilities.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CUNIT_ID, oParameterUtilities.COTHER_UNIT_ID);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CFLOOR_ID, oParameterUtilities.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CBUILDING_ID, oParameterUtilities.CBUILDING_ID);

                var loResult = await _model.GetUtilitiesListAsync();
                if (loResult.Data.Any())
                {
                    oListUtilities = new ObservableCollection<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>(loResult.Data);

                }
                else 
                {
                    oListUtilities = new ObservableCollection<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>();
                }

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
                var loResult = await _model.GetComboBoxDataCCHARGES_TYPEAsync();
                oComboBoxDataCCHARGES_TYPE = new List<PMT01700ComboBoxDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Core CRUD
        public async Task GetEntity(PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //poEntity.CDEPT_CODE = poEntity.CDEPT_CODE ?? oParameter.CDEPT_CODE;
                //poEntity.CTRANS_CODE = poEntity.CTRANS_CODE ?? oParameter.CTRANS_CODE;
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                oEntityUtilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                oEntityUtilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                //poEntity.CTRANS_CODE = oParameter.CTRANS_CODE;
                //poEntity.CDEPT_CODE = oParameter.CDEPT_CODE;
                await _model.R_ServiceDeleteAsync(poEntity);
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
