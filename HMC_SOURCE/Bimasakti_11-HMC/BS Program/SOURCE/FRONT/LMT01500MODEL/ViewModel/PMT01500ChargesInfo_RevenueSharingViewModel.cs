using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PMT01500Common.DTO._4._Charges_Info;
using PMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace PMT01500Model.ViewModel
{
    public class PMT01500ChargesInfo_RevenueSharingViewModel : R_ViewModel<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>
    {
        #region From Back
        private readonly PMT01500ChargesInfo_RevenueSharingModel _modelPMT01500ChargesInfo_RevenueSharingModel = new PMT01500ChargesInfo_RevenueSharingModel();
        public ObservableCollection<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO> loListLPMT01500ChargesInfo_RevenueSharing = new ObservableCollection<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>();
        public ObservableCollection<PMT01500ChargesInfo_RevenueMinimumRentDTO> loListLPMT01500ChargesInfo_RevenueMinimumRent = new ObservableCollection<PMT01500ChargesInfo_RevenueMinimumRentDTO>();
        public PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO? loEntityChargesInfo_RevenueSharing = new PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO();
        public PMT01500ParameterForChargesInfo_RevenueSharingDTO loParameterList = new PMT01500ParameterForChargesInfo_RevenueSharingDTO();
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
                    var loResult = await _modelPMT01500ChargesInfo_RevenueSharingModel.GetRevenueSharingSchemeListAsync(loParameterList);
                    loListLPMT01500ChargesInfo_RevenueSharing = new ObservableCollection<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_SEQ_NO = loParameterList.CCHARGE_SEQ_NO;
                if (!string.IsNullOrEmpty(poEntity.CCHARGE_SEQ_NO))
                {
                    var loResult = await _modelPMT01500ChargesInfo_RevenueSharingModel.R_ServiceGetRecordAsync(poEntity);
                    loEntityChargesInfo_RevenueSharing = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poNewEntity, eCRUDMode peCRUDMode)
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

                var loResult = await _modelPMT01500ChargesInfo_RevenueSharingModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityChargesInfo_RevenueSharing = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
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
                await _modelPMT01500ChargesInfo_RevenueSharingModel.R_ServiceDeleteAsync(poEntity);
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
                    var loResult = await _modelPMT01500ChargesInfo_RevenueSharingModel.GetRevenueMinimumRentListAsync(loParameterList);
                    loListLPMT01500ChargesInfo_RevenueMinimumRent = new ObservableCollection<PMT01500ChargesInfo_RevenueMinimumRentDTO>(loResult);
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