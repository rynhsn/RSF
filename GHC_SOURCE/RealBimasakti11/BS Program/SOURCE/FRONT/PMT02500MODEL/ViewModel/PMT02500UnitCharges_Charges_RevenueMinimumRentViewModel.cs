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
    public class PMT02500UnitCharges_Charges_RevenueMinimumRentViewModel : R_ViewModel<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO>
    {

        #region From Back
        private readonly PMT02500UnitCharges_Charges_RevenueMinimumRentModel _model = new PMT02500UnitCharges_Charges_RevenueMinimumRentModel();
        public ObservableCollection<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> oListMinimumRent = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO>();
        public PMT02500UnitCharges_Charges_RevenueMinimumRentDTO? oEntityMinimumRent = new PMT02500UnitCharges_Charges_RevenueMinimumRentDTO();
        public PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO loParameterList = new PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO();
        #endregion

        #region For Front

        #endregion

        #region ChargesInfo_RevenueSharing

        public async Task GetRevenueMinimumRentList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CREVENUE_SHARING_ID))
                {
                    var loResult = await _model.GetRevenueMinimumRentListAsync(loParameterList);
                    oListMinimumRent = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO>(loResult.Data);
                }
                else
                {
                    oListMinimumRent = new ObservableCollection<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500UnitCharges_Charges_RevenueMinimumRentDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_SEQ_NO = loParameterList.CCHARGE_SEQ_NO;
                if (!string.IsNullOrEmpty(poEntity.CCHARGE_SEQ_NO))
                {
                    var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                    oEntityMinimumRent = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500UnitCharges_Charges_RevenueMinimumRentDTO poNewEntity, eCRUDMode peCRUDMode)
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

                oEntityMinimumRent = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500UnitCharges_Charges_RevenueMinimumRentDTO poEntity)
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
