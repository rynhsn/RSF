using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using PMT03000MODEL.View_Model_s;
using PMT03000FrontResources;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Controls.Events;
using R_CommonFrontBackAPI;
using System.Xml.Linq;
using R_BlazorFrontEnd.Enums;
using System.Net.Http.Headers;
using System;

namespace PMT03000FRONT
{
    public partial class PMT03001 : R_Page
    {
        PMT03010ViewModel _viewModel = new();
        private R_Grid<UnitTypeCtgFacilityDTO> _gridUnitTypeCtgFacility;
        private R_Grid<TenantUnitFacilityDTO> _gridTenantUnitFacility;
        private R_ConductorGrid _conUnitTypeCtgFacility;
        private R_Conductor _conTenantUnitFacility;
        private int _pageSize_UnitTypeCtgFacility = 10;
        private int _pageSize_TenantUnitFacility = 10;
        private string _lbl_activeinactive = "Active/Inactive";
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        //methods
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (poParameter != null)
                {
                    _viewModel.TransByUnit = R_FrontUtility.ConvertObjectToObject<TransByUnitDTO>(poParameter);
                    await _gridUnitTypeCtgFacility.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //events - UnitTypeCtgFacility
        private async Task UnitTypeCtgFacility_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetList_UnitTypeCtgFacilityAsync();
                eventArgs.ListEntityResult = _viewModel.UnitTypeCtgFacilities;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task UnitTypeCtgFacility_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(eventArgs.Data);
                _viewModel.UnitFacilityTypeId = loData.CFACILITY_TYPE;
                _viewModel.UnitFacilityTypeName = loData.CFACILITY_TYPE_DESCR;
                if (!string.IsNullOrWhiteSpace(_viewModel.UnitFacilityTypeId))
                {
                    await _gridTenantUnitFacility.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //events - TenantUnitFacility
        private async Task TenantUnitFacility_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetList_TenantUnitFacilityAsync();
                eventArgs.ListEntityResult = _viewModel.TenantUnitFacilites;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantUnitFacility_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(eventArgs.Data);
                await _viewModel.GetRecord_TenantUnitFacilityAsync(loParam);
                eventArgs.Result = _viewModel.TenantUnitFacility;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TenantUnitFacility_RDisplay(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        var loData = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(eventArgs.Data);
                        _lbl_activeinactive = loData.LACTIVE ? _localizer["_btn_inactive"] : _localizer["_btn_active"];
                        break;
                    case R_eConductorMode.Add:
                        var loDataAdd = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(eventArgs.Data);
                        _lbl_activeinactive = loDataAdd.LACTIVE ? _localizer["_btn_inactive"] : _localizer["_btn_active"];
                        break;
                    case R_eConductorMode.Edit:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TenantUnitFacility_ConvertGridToEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TenantUnitFacility_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                TenantUnitFacilityDTO? loData = eventArgs.Data as TenantUnitFacilityDTO;
                loData.LACTIVE = true;
                loData.CSEQUENCE = _viewModel.TenantUnitFacilites.Count > 0 ? (int.Parse(_viewModel.TenantUnitFacilites.Select(loRowData => int.Parse(loRowData.CSEQUENCE)).Max().ToString("D3")) + 1).ToString("D3") : "001";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TenantUnitFacility_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = eventArgs.Data as TenantUnitFacilityDTO;
                loData.CSTART_DATE = loData.DSTART_DATE.Value.ToString("yyyyMMdd");
                loData.CEND_DATE = loData.DEND_DATE.Value.ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantUnitFacility_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(eventArgs.Data);
                await _viewModel.SaveRecord_TenantUnitFacilityAsync(loData, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.TenantUnitFacility;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantUnitFacility_Delete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(eventArgs.Data);
                await _viewModel.DeleteRecord_TenantUnitFacilityAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BtnActiveInactive_Onclick()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(_conTenantUnitFacility.R_GetCurrentData());
                await _viewModel.ActiveInactive_TenantUnitFacility(loParam);
                await _gridTenantUnitFacility.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
