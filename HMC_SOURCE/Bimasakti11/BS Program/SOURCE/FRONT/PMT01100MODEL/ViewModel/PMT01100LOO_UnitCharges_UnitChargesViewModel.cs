using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges;
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
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01100Model.ViewModel
{
    public class PMT01100LOO_UnitCharges_UnitChargesViewModel : R_ViewModel<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>
    {
        #region From Back

        private readonly PMT01100LOO_UnitCharges_UnitChargesModel _model = new PMT01100LOO_UnitCharges_UnitChargesModel();
        public PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO oEntity = new PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO();
        public ObservableCollection<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO> oListUnitInfo = new ObservableCollection<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>();
        public ObservableCollection<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO> oListCharges = new ObservableCollection<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO>();
        public PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO oHeaderEntity = new PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO();

        #endregion

        #region For Front
        public PMT01100ParameterFrontChangePageDTO oParameter = new PMT01100ParameterFrontChangePageDTO();
        public PMT01100UtilitiesParameterChargesListDTO oParameterChargesList = new PMT01100UtilitiesParameterChargesListDTO();
        public PMT01100UtilitiesParameterUtilitiesListDTO oParameterUtilitiesPage = new PMT01100UtilitiesParameterUtilitiesListDTO();

        public bool lControlTabUnitAndCharges = true;
        public bool lControlTabUtilities = true;

        #endregion

        #region Program

        public async Task GetUnitChargesHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    PMT01100LOO_UnitCharges_UnitCharges_RequestBodyUnitChargesHeaderDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_UnitCharges_UnitCharges_RequestBodyUnitChargesHeaderDTO>(oParameter);

                    var loResult = await _model.GetUnitChargesHeaderAsync(poParameter: loParameter);
                    loResult.IMONTH = string.IsNullOrEmpty(loResult.CMONTH) ? 0 : int.Parse(loResult.CMONTH);
                    loResult.IYEAR = string.IsNullOrEmpty(loResult.CYEAR) ? 0 : int.Parse(loResult.CYEAR);
                    loResult.IDAY = string.IsNullOrEmpty(loResult.CDAY) ? 0 : int.Parse(loResult.CDAY);
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
                    PMT01100UtilitiesParameterUnitInfoListDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01100UtilitiesParameterUnitInfoListDTO>(oParameter);
                    var loResult = await _model.GetUnitInfoListAsync(poParameter: loParameter);
                    oListUnitInfo = new ObservableCollection<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameterChargesList.CREF_NO) && !string.IsNullOrEmpty(oParameterChargesList.CBUILDING_ID))
                {
                    var loResult = await _model.GetChargesListAsync(poParameter: oParameterChargesList);
                    if (loResult.Data.Any())
                    {
                        foreach (PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO item in loResult.Data)
                            item.CCHARGES_ID_AND_NAME = item.CCHARGES_NAME + " (" + item.CCHARGES_ID + ")";

                        oListCharges = new ObservableCollection<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO>(loResult.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Core

        public async Task GetEntity(PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = poEntity.CDEPT_CODE ?? oParameter.CDEPT_CODE;
                poEntity.CTRANS_CODE = poEntity.CTRANS_CODE ?? oParameter.CTRANS_CODE;
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

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

        public async Task ServiceDelete(PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                poEntity.CTRANS_CODE = oParameter.CTRANS_CODE;
                poEntity.CDEPT_CODE = oParameter.CDEPT_CODE;
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
