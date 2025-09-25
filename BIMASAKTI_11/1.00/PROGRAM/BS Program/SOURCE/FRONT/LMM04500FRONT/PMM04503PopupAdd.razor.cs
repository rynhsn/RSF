using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using PMM04500COMMON;
using PMM04500COMMON.DTO_s;
using PMM04500MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Globalization;

namespace PMM04500FRONT
{
    public partial class PMM04503PopupAdd : R_Page
    {
        [Inject] private R_ILocalizer<PMM04500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        [Inject] IClientHelper _clientHelper { get; set; }

        private R_ConductorGrid _conGridPricingRate;

        private R_Grid<PricingRateBulkSaveDTO> _gridPricingRate;

        private PMM04501ViewModel _viewModel_PricingRate = new();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                //get & parse param
                var loParam = R_FrontUtility.ConvertObjectToObject<PricingRateDTO>(poParameter);

                //set param to class variable
                _viewModel_PricingRate._pricingRateDateDisplay = !string.IsNullOrWhiteSpace(loParam.CRATE_DATE) ? DateTime.ParseExact(loParam.CRATE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : DateTime.Now;
                _viewModel_PricingRate._propertyId = loParam.CPROPERTY_ID ?? "";
                _viewModel_PricingRate._pricingRateDate = loParam.CRATE_DATE ?? "";

                await _gridPricingRate.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricingRateDTO>(eventArgs.Data);

                var loCls = new R_LockingServiceClient(pcModuleName: ContextConstantPMM04500.DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: ContextConstantPMM04500.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = ContextConstantPMM04500.PROGRAM_ID,
                        Table_Name = ContextConstantPMM04500.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModel_PricingRate._propertyId, "02", _viewModel_PricingRate._pricingRateDate, loData.CCURRENCY_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = ContextConstantPMM04500.PROGRAM_ID,
                        Table_Name = ContextConstantPMM04500.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModel_PricingRate._propertyId, "02", _viewModel_PricingRate._pricingRateDate, loData.CCURRENCY_CODE)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }

        #region grid events
        private async Task PricingRateAdd_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel_PricingRate.GetPricingRateAddList();

                eventArgs.ListEntityResult = _viewModel_PricingRate._pricingSaveList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void PricingRateAdd_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void PricingRate_BeforeLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            Type loType;
            switch (eventArgs.ColumnName)
            {
                case nameof(PricingRateBulkSaveDTO.CCURRENCY_CODE):
                    loType = typeof(GSL00300);
                    eventArgs.Parameter = new GSL00300ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = ""
                    };
                    eventArgs.TargetPageType = loType;
                    break;
            }
        }

        private void PricingRate_AfterLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                switch (eventArgs.ColumnName)
                {
                    case nameof(PricingRateBulkSaveDTO.CCURRENCY_CODE):
                        var loResult = R_FrontUtility.ConvertObjectToObject<GSL00300DTO>(eventArgs.Result);
                        ((PricingRateBulkSaveDTO)eventArgs.ColumnData).CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region form

        private async Task PricingRateAddForm_RateDateValueChangedAsync(DateTime? poDateParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel_PricingRate._pricingRateDateDisplay = poDateParam;
                _viewModel_PricingRate._pricingRateDate = poDateParam.Value.ToString("yyyyMMdd");
                await _gridPricingRate.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region button

        private async Task PricingRateAdd_CancelAsync()
        {
            R_Exception loEx = new();
            try
            {
                await this.Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PricingRateAdd_Process()
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel_PricingRate.SavePricing();
                if (!loEx.HasError)
                {
                    R_eMessageBoxResult r_eMessageBoxResult = await R_MessageBox.Show("", "Process Complete", R_eMessageBoxButtonType.OK);
                }
                await this.Close(true, null);
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
