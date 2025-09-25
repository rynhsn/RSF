using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._2._LOO___Unit___Charges___Utilities;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Front;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01100Model.ViewModel
{
    public class PMT01100LOO_UnitCharges_UtilitiesViewModel : R_ViewModel<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>
    {
        #region From Back

        private readonly PMT01100LOO_UnitCharges_UtilitiesModel _model = new PMT01100LOO_UnitCharges_UtilitiesModel();
        public PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO oEntity = new PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO();
        public ObservableCollection<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> oListUtilities = new ObservableCollection<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>();
        public List<PMT01100ComboBoxDTO> oComboBoxDataCCHARGES_TYPE = new List<PMT01100ComboBoxDTO>();
        public List<PMT01100ResponseUtilitiesCMeterNoParameterDTO> oComboBoxDataCMETER_NO = new List<PMT01100ResponseUtilitiesCMeterNoParameterDTO>();

        #endregion

        #region For Front

        public PMT01100LOO_UnitCharges_Utilities_UnitInfoDTO oHeaderUtilitiesEntity = new PMT01100LOO_UnitCharges_Utilities_UnitInfoDTO();
        //public PMT01100ParameterFrontChangePageDTO oParameter = new PMT01100ParameterFrontChangePageDTO();
        //public PMT01100RequestUtilitiesCMeterNoParameterDTO oParameterCMeterNo = new PMT01100RequestUtilitiesCMeterNoParameterDTO();
        public PMT01100UtilitiesParameterUtilitiesListDTO oParameterUtilitiesList = new PMT01100UtilitiesParameterUtilitiesListDTO();

        #endregion

        #region Program

        public async Task<bool> GetComboBoxDataCMETER_NO(PMT01100RequestUtilitiesCMeterNoParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            bool llReturn = false;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CUTILITY_TYPE) && !string.IsNullOrEmpty(poParameter.CUNIT_ID))
                {
                    var loResult = await _model.GetComboBoxDataCMETER_NOAsync(poParameter: poParameter);
                    oComboBoxDataCMETER_NO = new List<PMT01100ResponseUtilitiesCMeterNoParameterDTO>(loResult.Data);
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
                var loResult = await _model.GetComboBoxDataCCHARGES_TYPEAsync();
                oComboBoxDataCCHARGES_TYPE = new List<PMT01100ComboBoxDTO>(loResult.Data);
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
                    var loResult = await _model.GetUtilitiesListAsync(poParameter: oParameterUtilitiesList);
                    oListUtilities = new ObservableCollection<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Core

        public async Task GetEntity(PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO poEntity)
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

        public async Task ServiceSave(PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO poNewEntity, eCRUDMode peCRUDMode)
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

        public async Task ServiceDelete(PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO poEntity)
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
