using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT03000COMMON;
using PMT03000MODEL.View_Model_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using PMT03000FrontResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls.DataControls;
using PMT03000COMMON.DTO_s;

namespace PMT03000FRONT
{
    public partial class PMT03000 : R_Page
    {
        //var
        private R_Grid<BuildingDTO> _gridBuilding;
        private R_Grid<BuildingUnitDTO> _gridBuildingUnit;
        private R_Grid<TransByUnitDTO> _gridTransByUnit;
        private R_ConductorGrid _conBuilding;
        private R_ConductorGrid _conBuildingUnit;
        private R_ConductorGrid _conTransByUnit;
        private PMT03000ViewModel _viewModel = new();
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        private R_TabStrip _tabStrip; //ref Tabstrip
        private R_TabPage _tabUnitList;
        private int _pageSize_Building = 10;
        private int _pageSize_BuildingUnit = 10;
        private int _pageSize_TrxByUnit = 10;


        //methods
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetList_PropertyAsync();
                if (!string.IsNullOrWhiteSpace(_viewModel.PropertyId))
                {
                    await _gridBuilding.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task ComboboxProperty_ValueChangedAsync(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.PropertyId = poParam;
                _viewModel.PropertyName = _viewModel.Properties.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PropertyId).CPROPERTY_NAME;
                await Task.Delay(300);
                if (!string.IsNullOrWhiteSpace(_viewModel.PropertyId))
                {
                    await _gridBuilding.R_RefreshGrid(null);
                    if (_viewModel.Buildings.Count < 1)
                    {
                        _viewModel.Building = new();
                        _viewModel.Buildings = new();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //events - building
        private async Task Building_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetList_BuildingAsync();
                eventArgs.ListEntityResult = _viewModel.Buildings;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Building_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (_viewModel.Buildings.Count > 0)
                {
                    _viewModel.Building = R_FrontUtility.ConvertObjectToObject<BuildingDTO>(eventArgs.Data);
                    await _gridBuildingUnit.R_RefreshGrid(null);
                    if (_viewModel.BuildingUnits.Count < 1)
                    {
                        _viewModel.TransByUnits = new();
                        _viewModel.TransByUnit = new();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //events - unit
        private async Task BuildingUnit_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetList_BuildingUnitAsync();
                eventArgs.ListEntityResult = _viewModel.BuildingUnits;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BuildingUnit_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (_viewModel.BuildingUnits.Count > 0)
                {
                    _viewModel.BuildingUnit = R_FrontUtility.ConvertObjectToObject<BuildingUnitDTO>(eventArgs.Data);
                    await _gridTransByUnit.R_RefreshGrid(null);
                    if (_viewModel.TransByUnits.Count < 1)
                    {
                        _viewModel.TransByUnit = new();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //events - trans by unit
        private async Task TransUnit_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetList_TransUnitAsync();
                eventArgs.ListEntityResult = _viewModel.TransByUnits;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TransUnit_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.TransByUnit = R_FrontUtility.ConvertObjectToObject<TransByUnitDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //before open popup
        private async Task BeforeOpenPopup_MaintainFacility(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (_viewModel.TransByUnit.CTRANS_STATUS != PMT03000ContextConstant.APPROVED_STS)
                {
                    var loMsg = await R_MessageBox.Show(_localizer["_msg_cannotmaintain"]);
                    return;
                }
                else
                {
                    eventArgs.Parameter = new TransByUnitDTO()
                    {
                        CPROPERTY_ID = _viewModel.PropertyId,
                        CPROPERTY_NAME = _viewModel.PropertyName,
                        CTENANT_ID = _viewModel.TransByUnit.CTENANT_ID,
                        CTENANT_NAME = _viewModel.TransByUnit.CTENANT_NAME,
                        CBUILDING_ID = _viewModel.Building.CBUILDING_ID,
                        CBUILDING_NAME = _viewModel.Building.CBUILDING_NAME,
                        CFLOOR_ID = _viewModel.BuildingUnit.CFLOOR_ID,
                        CFLOOR_NAME = _viewModel.BuildingUnit.CFLOOR_NAME,
                        CUNIT_ID = _viewModel.BuildingUnit.CUNIT_ID,
                        CUNIT_NAME = _viewModel.BuildingUnit.CUNIT_NAME,
                        CUNIT_TYPE_CATEGORY_ID = _viewModel.BuildingUnit.CUNIT_TYPE_CATEGORY_ID,
                        CUNIT_TYPE_CATEGORY_NAME = _viewModel.BuildingUnit.CUNIT_TYPE_CATEGORY_NAME,
                    };
                    eventArgs.PageTitle = _localizer["_title_maintainfacility"];
                    eventArgs.TargetPageType = typeof(PMT03001);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterOpenPopup_MaintainFacility(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.GetList_BuildingAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BeforeOpenPopup_MaintainMember(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (_viewModel.TransByUnit.CTRANS_STATUS != PMT03000ContextConstant.APPROVED_STS)
                {
                    var loMsg = await R_MessageBox.Show(_localizer["_msg_cannotmaintain"]);
                    return;
                }
                eventArgs.Parameter = new MaintainMemberParamDTO()
                {
                    CSELECTED_PROPERTY_ID = _viewModel.PropertyId,
                    CSELECTED_TENANT_ID = _viewModel.TransByUnit.CTENANT_ID,
                    PARAM_BUILDING_ID = _viewModel.Building.CBUILDING_ID,
                    PARAM_BUILDING_NAME = _viewModel.Building.CBUILDING_NAME,
                    PARAM_FLOOR_ID = _viewModel.BuildingUnit.CFLOOR_ID,
                    PARAM_FLOOR_NAME = _viewModel.BuildingUnit.CFLOOR_NAME,
                    PARAM_UNIT_ID = _viewModel.BuildingUnit.CUNIT_ID,
                    PARAM_UNIT_NAME = _viewModel.BuildingUnit.CUNIT_NAME,
                    PARAM_PROGRAM_ID=PMT03000ContextConstant.CPROGRAM_ID,
                };
                eventArgs.PageNamespace = "PMM03500FRONT.PMM03507";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterOpenDetail_MaintainMember(R_AfterOpenDetailEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.GetList_BuildingAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
