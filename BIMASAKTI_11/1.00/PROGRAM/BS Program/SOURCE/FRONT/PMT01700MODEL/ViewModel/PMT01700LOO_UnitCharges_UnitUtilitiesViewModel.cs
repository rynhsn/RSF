using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL.ViewModel
{
    public class PMT01700LOO_UnitCharges_UnitUtilitiesViewModel : R_ViewModel<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>
    {
        #region From Back

        private readonly PMT01700LOO_UnitCharges_UnitUtilitiesModel _model = new PMT01700LOO_UnitCharges_UnitUtilitiesModel();
        public PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO oEntityUnitInfo = new PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO();
        public ObservableCollection<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> oListUnitInfo = new ObservableCollection<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>();
        public PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO oHeaderEntity = new PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO();

        #endregion

        #region For Front
        public PMT01700ParameterFrontChangePageDTO? oParameter = new PMT01700ParameterFrontChangePageDTO();
        public PMT01700LOO_UnitUtilities_ParameterDTO? oParameterUnitUtilities = new PMT01700LOO_UnitUtilities_ParameterDTO();
        public PMT01700ParameterChargesTab? oParameterChargesTab = new PMT01700ParameterChargesTab();

        public bool lControlTabUnitAndCharges = true;
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
                    PMT01700LOO_UnitUtilities_ParameterDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_ParameterDTO>(oParameter);

                    var loResult = await _model.GetUnitChargesHeaderAsync(poParameter: loParameter);
                    if (loResult != null)
                    {

                        loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                        loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);

                        oParameter.CCHARGE_MODE = loResult.CCHARGE_MODE;

                        oParameterChargesTab = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterChargesTab>(oParameter);
                        oParameterChargesTab.IYEARS = loResult.IYEARS;
                        oParameterChargesTab.IMONTHS = loResult.IMONTHS;
                        oParameterChargesTab.IDAYS = loResult.IDAYS;
                        oParameterChargesTab.CSTART_DATE = loResult.CSTART_DATE;
                        oParameterChargesTab.CEND_DATE = loResult.CEND_DATE;
                        oParameterChargesTab.CCURRENCY_CODE = loResult.CCURRENCY_CODE;

                    }
                    oHeaderEntity = loResult != null ? loResult : new PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO();
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
                    PMT01700LOO_UnitUtilities_ParameterDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_ParameterDTO>(oParameter);

                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, loParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CDEPT_CODE, loParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CTRANS_CODE, loParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CREF_NO, loParameter.CREF_NO);

                    var loResult = await _model.GetUnitInfoListAsync();
                    oListUnitInfo = new ObservableCollection<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
        #region Core

        public async Task<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> GetEntity(PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO loResult = new PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO();

            try
            {
                poEntity.CDEPT_CODE = poEntity.CDEPT_CODE ?? oParameter.CDEPT_CODE;

                poEntity.CTRANS_CODE = poEntity.CTRANS_CODE != "" ? poEntity.CTRANS_CODE  : oParameter.CTRANS_CODE;
                loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                oEntityUnitInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            return loResult;

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                oEntityUnitInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                poParam.CTRANS_CODE = oParameter.CTRANS_CODE;
                poParam.CDEPT_CODE = oParameter.CDEPT_CODE;
                await _model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Utilities

        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }
        #endregion


    }
}
