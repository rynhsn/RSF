using BlazorClientHelper;
using HDM00600COMMON.DTO;
using HDM00600MODEL.View_Model_s;
using HDM00600FrontResources;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Helpers;
using HDM00600COMMON.DTO_s.Helper;
using System.Globalization;
using Lookup_PMCOMMON.DTOs;
using R_APICommonDTO;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDModel.ViewModel.HDL00100;
using Lookup_GSModel.ViewModel;
using Lookup_PMModel.ViewModel.LML00200;

namespace HDM00600FRONT
{
    public partial class HDM00601 : R_Page
    {
        private HDM00601ViewModel _viewModel = new();
        private R_Grid<PricelistDTO> _gridPricelist;
        private R_ConductorGrid _conPricelist;
        private int _pageSize_Pricelist = 10;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_ILocalizer<Lookup_GSFrontResources.Resources_Dummy_Class> _localizerGS { get; set; }
        [Inject] private R_ILocalizer<Lookup_HDFrontResources.Resources_Dummy_Class_LookupHD> _localizerHD { get; set; }
        [Inject] private R_ILocalizer<Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM> _localizerPM { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        //methods - action
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        private void DisplayErrorInvoke(R_APIException poEx)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poEx);
            this.R_DisplayException(loEx);
        }
        private async Task ShowSuccessInvoke()
        {
            R_Exception loEx = new();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_batchComplete"], R_eMessageBoxButtonType.OK);
                if (loValidate == R_eMessageBoxResult.OK)
                {
                    await Close(true, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //method - override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                _viewModel._companyId = _clientHelper.CompanyId;
                _viewModel._userId = _clientHelper.UserId;
                if (poParameter != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PropertyDTO>(poParameter);
                    _viewModel._propertyId = loParam.CPROPERTY_ID;
                    _viewModel._propertyName = loParam.CPROPERTY_NAME;
                    _viewModel._propertyCurr = loParam.CCURRENCY;
                }
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.DisplayErrorAction = DisplayErrorInvoke;
                _viewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
                await _gridPricelist.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //methods - events
        private void Pricelist_Getlist(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.ListEntityResult = _viewModel._pricelist_List;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Data);
                if (DateTime.TryParseExact(loData.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                {
                    loData.DSTART_DATE = ldRefDate;
                }
                else
                {
                    loData.DSTART_DATE = null;
                }
                eventArgs.Result = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Data);

                if (string.IsNullOrWhiteSpace(loData.CPRICELIST_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add1"));
                }
                if (string.IsNullOrWhiteSpace(loData.CPRICELIST_NAME))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add2"));
                }
                if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add8"));
                }
                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add3"));
                }
                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add4"));
                }
                if (string.IsNullOrWhiteSpace(loData.CUNIT))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add5"));
                }
                if (loData.IPRICE < 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add6"));
                }
                if (loData.DSTART_DATE == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add7"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Data is PricelistDTO loData)
                {
                    loData.CCURRENCY_CODE = _viewModel._propertyCurr;
                    loData.DSTART_DATE = DateTime.Now;
                }
            }
            catch (Exception ex) { loEx.Add(ex); }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_BeforeOpenGridLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                switch (eventArgs.ColumnName)
                {
                    case nameof(PricelistDTO.CDEPT_CODE):
                        eventArgs.Parameter = new GSL00700ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CUSER_ID = _clientHelper.UserId
                        };
                        eventArgs.TargetPageType = typeof(GSL00700);
                        break;
                    case nameof(PricelistDTO.CCHARGES_ID):
                        eventArgs.Parameter = new LML00200ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CUSER_ID = _clientHelper.UserId,
                            CPROPERTY_ID = _viewModel._propertyId,
                            CCHARGE_TYPE_ID = "01",
                            CTAXABLE_TYPE = "0",
                            CACTIVE_TYPE = "1"
                        };
                        eventArgs.TargetPageType = typeof(LML00200);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_AfterOpenGridLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }
                switch (eventArgs.ColumnName)
                {
                    case nameof(PricelistDTO.CDEPT_CODE):
                        var loDeptResult = R_FrontUtility.ConvertObjectToObject<GSL00700DTO>(eventArgs.Result);
                        ((PricelistDTO)eventArgs.ColumnData).CDEPT_CODE = loDeptResult.CDEPT_CODE;
                        break;
                    case nameof(PricelistDTO.CCHARGES_ID):
                        var loChrgResult = R_FrontUtility.ConvertObjectToObject<LML00200DTO>(eventArgs.Result);
                        ((PricelistDTO)eventArgs.ColumnData).CCHARGES_ID = loChrgResult.CCHARGES_ID;
                        ((PricelistDTO)eventArgs.ColumnData).CINVGRP_CODE = loChrgResult.CINVGRP_CODE;
                        ((PricelistDTO)eventArgs.ColumnData).LTAXABLE = loChrgResult.LTAXABLE;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task Pricelist_CellLostFocused(R_CellLostFocusedEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = eventArgs.CurrentRow as PricelistDTO;
                if (eventArgs.ColumnName == nameof(PricelistDTO.CDEPT_CODE))
                {
                    if (!string.IsNullOrWhiteSpace(eventArgs.Value.ToString()))
                    {
                        eventArgs.Value.ToString();
                        LookupGSL00700ViewModel loHDVM = new();
                        GSL00700DTO loResult = await loHDVM.GetDepartment(new GSL00700ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CUSER_ID = _viewModel._propertyName,
                            CSEARCH_TEXT = eventArgs.Value.ToString(),
                        });

                        if (loResult == null)
                        {
                            loData.CDEPT_CODE = "";
                            R_eMessageBoxResult loMessageResult = await R_MessageBox.Show("", _localizerGS["_ErrLookup01"], R_eMessageBoxButtonType.OK);
                        }
                        else
                        {
                            loData.CDEPT_CODE = loResult.CDEPT_CODE ?? "";
                        }
                    }
                    else
                    {
                        loData.CDEPT_CODE = "";
                    }
                }
                else if (eventArgs.ColumnName == nameof(PricelistDTO.CCHARGES_ID))
                {
                    if (!string.IsNullOrWhiteSpace(eventArgs.Value.ToString()))
                    {
                        eventArgs.Value.ToString();
                        LookupLML00200ViewModel loHDVM = new();
                        LML00200DTO loResult = await loHDVM.GetUnitCharges(new LML00200ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CUSER_ID = _clientHelper.UserId,
                            CPROPERTY_ID = _viewModel._propertyId,
                            CCHARGE_TYPE_ID = "01",
                            CTAXABLE_TYPE = "0",
                            CACTIVE_TYPE = "1",
                            CSEARCH_TEXT = eventArgs.Value.ToString(),
                        });

                        if (loResult == null)
                        {
                            loData.CCHARGES_ID = "";
                            loData.CINVGRP_CODE = "";
                            loData.LTAXABLE = false;
                            R_eMessageBoxResult loMessageResult = await R_MessageBox.Show("", _localizerPM["_ErrLookup01"], R_eMessageBoxButtonType.OK);
                        }
                        loData.CCHARGES_ID = loResult.CCHARGES_ID ?? "";
                        loData.CINVGRP_CODE = loResult.CINVGRP_CODE ?? "";
                        loData.LTAXABLE = loResult.LTAXABLE ? loResult.LTAXABLE : false;
                        await Task.CompletedTask;
                    }
                    else
                    {
                        loData.CCHARGES_ID = "";
                        loData.CINVGRP_CODE = "";
                        loData.LTAXABLE = false;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = eventArgs.Data as PricelistDTO;
                loData.CSTART_DATE = loData.DSTART_DATE.Value.ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_Delete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Data);
                if (loData != null)
                {
                    _viewModel._pricelist_List.Remove(loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnClick_Process()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.SaveList_PricelistAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnClick_Cancel()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Close(false, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
