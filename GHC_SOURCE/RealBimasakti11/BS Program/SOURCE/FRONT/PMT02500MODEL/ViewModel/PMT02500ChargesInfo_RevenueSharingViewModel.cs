using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PMT02500Common.DTO._4._Charges_Info;
using PMT02500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace PMT02500Model.ViewModel
{
    public class PMT02500ChargesInfo_RevenueSharingViewModel : R_ViewModel<PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO>
    {

        #region From Back
        private readonly PMT02500ChargesInfo_RevenueSharingModel _modelPMT02500ChargesInfo_RevenueSharingModel = new PMT02500ChargesInfo_RevenueSharingModel();
        public ObservableCollection<PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO> loListLPMT02500ChargesInfo_RevenueSharing = new ObservableCollection<PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO>();
        public ObservableCollection<PMT02500ChargesInfo_RevenueMinimumRentDTO> loListLPMT02500ChargesInfo_RevenueMinimumRent = new ObservableCollection<PMT02500ChargesInfo_RevenueMinimumRentDTO>();
        public PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO? loEntityChargesInfo_RevenueSharing = new PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO();
        public PMT02500ParameterForChargesInfo_RevenueSharingDTO loParameterList = new PMT02500ParameterForChargesInfo_RevenueSharingDTO();
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
                    var loResult = await _modelPMT02500ChargesInfo_RevenueSharingModel.GetRevenueSharingSchemeListAsync(loParameterList);
                    loListLPMT02500ChargesInfo_RevenueSharing = new ObservableCollection<PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_SEQ_NO = loParameterList.CCHARGE_SEQ_NO;
                if (!string.IsNullOrEmpty(poEntity.CCHARGE_SEQ_NO))
                {
                    var loResult = await _modelPMT02500ChargesInfo_RevenueSharingModel.R_ServiceGetRecordAsync(poEntity);
                    loEntityChargesInfo_RevenueSharing = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO poNewEntity, eCRUDMode peCRUDMode)
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

                var loResult = await _modelPMT02500ChargesInfo_RevenueSharingModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityChargesInfo_RevenueSharing = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
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
                await _modelPMT02500ChargesInfo_RevenueSharingModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRevenueMinimumRentList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CCHARGE_SEQ_NO))
                {
                    var loResult = await _modelPMT02500ChargesInfo_RevenueSharingModel.GetRevenueMinimumRentListAsync(loParameterList);
                    loListLPMT02500ChargesInfo_RevenueMinimumRent = new ObservableCollection<PMT02500ChargesInfo_RevenueMinimumRentDTO>(loResult);
                }
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