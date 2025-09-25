using PMT02500Common.DTO._4._Charges_Info.Db;
using PMT02500Common.DTO._4._Charges_Info.Revenue_Sharing_Process;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PMT02500Model.ViewModel
{
    public class PMT02500UnitCharges_Charges_RevenueSharingSchemeViewModel : R_ViewModel<PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO>
    {

        #region From Back
        private readonly PMT02500UnitCharges_Charges_RevenueSharingSchemeModel _model = new PMT02500UnitCharges_Charges_RevenueSharingSchemeModel();
        public ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO> loListChargesInfo_RevenueSharing = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO>();
        //public ObservableCollection<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> loListChargesInfo_RevenueMinimumRent = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO>();
        public PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO? loEntityChargesInfo_RevenueSharing = new PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO();
        public PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO loParameterList = new PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO();
        #endregion

        #region For Front

        #endregion

        #region ChargesInfo_RevenueSharing
        public async Task GetRevenueSharingSchemeList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CCHARGE_SEQ_NO))
                {
                    var loResult = await _model.GetRevenueSharingSchemeListAsync(loParameterList);
                    loListChargesInfo_RevenueSharing = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO>(loResult.Data);
                }
                else
                {
                    loListChargesInfo_RevenueSharing = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrEmpty(poEntity.CCHARGE_SEQ_NO))
                {
                    var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                    loEntityChargesInfo_RevenueSharing = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityChargesInfo_RevenueSharing = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500UnitCharges_Charges_RevenueSharingSchemeOriginalDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                poEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                poEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                poEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                poEntity.CREF_NO = loParameterList.CREF_NO;
                poEntity.CCHARGE_SEQ_NO = loParameterList.CCHARGE_SEQ_NO;
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
