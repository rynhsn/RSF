using PMT01300COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01331ViewModel : R_ViewModel<PMT01331DTO>
    {
        private PMT01330PopupModel _PMT01331Model = new PMT01330PopupModel();

        #region Property Class
        public PMT01330DTO LOI_Charges { get; set; } = new PMT01330DTO();
        public PMT01331DTO RevenueSharingHD { get; set; } = new PMT01331DTO();
        public PMT01332DTO RevenueSharing { get; set; } = new PMT01332DTO();
        public PMT01333DTO RevenueMintRent { get; set; } = new PMT01333DTO();
        public ObservableCollection<PMT01331DTO> RevenueSharingHDGrid { get; set; } = new  ObservableCollection<PMT01331DTO>();
        public ObservableCollection<PMT01332DTO> RevenueSharingGrid { get; set; } = new  ObservableCollection<PMT01332DTO>();
        public ObservableCollection<PMT01333DTO> RevenueMintRentGrid { get; set; } = new  ObservableCollection<PMT01333DTO>();
        #endregion

        #region Revenue Sharing HD
        public async Task GetRevenueSharingHDList(PMT01331DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01331Model.GetLOIChargeRevenueHDListStreamAsync(poEntity);

                RevenueSharingHDGrid = new ObservableCollection<PMT01331DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRevenueSharingHD(PMT01331DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                RevenueSharingHD = poEntity;

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRevenueSharingHD(PMT01331DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CREF_NO = LOI_Charges.CREF_NO;
                    poEntity.CCHARGE_SEQ_NO = LOI_Charges.CCHARGE_SEQ_NO;
                    poEntity.CDEPT_CODE = LOI_Charges.CDEPT_CODE;
                    poEntity.CPROPERTY_ID = LOI_Charges.CPROPERTY_ID;
                }
                var loParam = new PMT01300SaveDTO<PMT01331DTO> { Data = poEntity, CRUDMode= poCRUDMode };
                await _PMT01331Model.SaveDeleteLOIChargeRevenueHDAsync(loParam);

                RevenueSharingHD = poEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRevenueSharingHD(PMT01331DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new PMT01300SaveDTO<PMT01331DTO> { Data = poEntity, CRUDMode = eCRUDMode.DeleteMode };
                await _PMT01331Model.SaveDeleteLOIChargeRevenueHDAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Revenue Sharing
        public async Task GetRevenueSharingList(PMT01332DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01331Model.GetLOIChargeRevenueListStreamAsync(poEntity);

                RevenueSharingGrid = new ObservableCollection<PMT01332DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRevenueSharing(PMT01332DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                RevenueSharing = poEntity;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRevenueSharing(PMT01332DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CREF_NO = LOI_Charges.CREF_NO;
                    poEntity.CCHARGE_SEQ_NO = LOI_Charges.CCHARGE_SEQ_NO;
                    poEntity.CDEPT_CODE = LOI_Charges.CDEPT_CODE;
                    poEntity.CPROPERTY_ID = LOI_Charges.CPROPERTY_ID;
                }

                var loParam = new PMT01300SaveDTO<PMT01332DTO> { Data = poEntity, CRUDMode = poCRUDMode };
                var loData = await _PMT01331Model.SaveDeleteLOIChargeRevenueAsync(loParam);

                RevenueSharing = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRevenueSharing(PMT01332DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new PMT01300SaveDTO<PMT01332DTO> { Data = poEntity, CRUDMode = eCRUDMode.DeleteMode };
                await _PMT01331Model.SaveDeleteLOIChargeRevenueAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Revenue Mint Rent
        public async Task GetRevenueMintRentList(PMT01333DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01331Model.GetRevenueMintRentListStreamAsync(poEntity);

                RevenueMintRentGrid = new ObservableCollection<PMT01333DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRevenueMintRent(PMT01333DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                RevenueMintRent = poEntity;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRevenueMintRent(PMT01333DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CREF_NO = LOI_Charges.CREF_NO;
                    poEntity.CCHARGE_SEQ_NO = LOI_Charges.CCHARGE_SEQ_NO;
                    poEntity.CDEPT_CODE = LOI_Charges.CDEPT_CODE;
                    poEntity.CPROPERTY_ID = LOI_Charges.CPROPERTY_ID;
                }

                var loParam = new PMT01300SaveDTO<PMT01333DTO> { Data = poEntity, CRUDMode = poCRUDMode };
                await _PMT01331Model.SaveRevenueMintRentAsync(loParam);

                RevenueMintRent = poEntity;
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
