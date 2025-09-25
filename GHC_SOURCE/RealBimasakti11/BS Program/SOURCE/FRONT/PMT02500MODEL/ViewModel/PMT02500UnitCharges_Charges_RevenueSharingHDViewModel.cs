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
    public class PMT02500UnitCharges_Charges_RevenueSharingHDViewModel : R_ViewModel<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>
    {

        #region From Back
        private readonly PMT02500UnitCharges_Charges_RevenueSharingHDModel _model = new PMT02500UnitCharges_Charges_RevenueSharingHDModel();
        public ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingHDDTO> oListRSHD = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>();
        public PMT02500UnitCharges_Charges_RevenueSharingHDDTO? oEntityRSHD = new PMT02500UnitCharges_Charges_RevenueSharingHDDTO();
        public PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO loParameterList = new PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO();
        #endregion

        #region For Front

        public bool lHasDataRSHD = false;
        #endregion

        #region ChargesInfo_RevenueSharing
        public async Task GetRevenueSharingHDList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CCHARGE_SEQ_NO))
                {
                    var loResult = await _model.GetRevenueSharingHDListAsync(loParameterList);
                    oListRSHD = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>(loResult.Data);
                }
                else
                {
                    oListRSHD = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueSharingHDDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500UnitCharges_Charges_RevenueSharingHDDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_SEQ_NO = loParameterList.CCHARGE_SEQ_NO;
                if (!string.IsNullOrEmpty(poEntity.CCHARGE_SEQ_NO))
                {
                    var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                    oEntityRSHD = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500UnitCharges_Charges_RevenueSharingHDDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                poNewEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                poNewEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                poNewEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                poNewEntity.CREF_NO = loParameterList.CREF_NO;
                poNewEntity.CCHARGE_SEQ_NO = loParameterList.CCHARGE_SEQ_NO;

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                oEntityRSHD = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500UnitCharges_Charges_RevenueSharingHDDTO poEntity)
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
