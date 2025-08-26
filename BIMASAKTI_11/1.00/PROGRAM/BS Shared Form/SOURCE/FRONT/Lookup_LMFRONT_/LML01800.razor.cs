using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01700;
using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.UtilityDTO;
using Lookup_PMModel.ViewModel.LML01600;
using Lookup_PMModel.ViewModel.LML01800;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMFRONT
{
    public partial class LML01800 : R_Page
    {
        private LookupLML01800ViewModel _viewModel = new LookupLML01800ViewModel();
        private R_Grid<LML01800DTO> GridRef;
        private int _pageSize = 12;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Parameter = (LML01800ParameterDTO)poParameter;
                await PropertyListRecord(null);

                if (_viewModel.UnitList.Count > 0)
                {
                    await GridRef!.R_RefreshGrid(_viewModel.Parameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region Property
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyList();

                if (_viewModel.PropertyList.Count>0)
                {
                    await BuildingListRecord(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                _viewModel.GetList = new();

                PropertyDTO PropertyTemp = _viewModel.PropertyList
                    .FirstOrDefault(data => data.CPROPERTY_ID == lsProperty)!;
                _viewModel.PropertyValue = PropertyTemp;
               await BuildingListRecord(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        #region Building
        private async Task BuildingListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetBuildingList();
                if (_viewModel.BuildingList.Count > 0)
                {
                    await FloorListRecord(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BuildingDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsBuilding = (string)poParam;
            try
            {
                _viewModel.GetList = new();

                BuildingDTO BuildingTemp = _viewModel.BuildingList
                    .FirstOrDefault(data => data.CBUILDING_ID == lsBuilding)!;
                _viewModel.BuildingValue = BuildingTemp;

                await FloorListRecord(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        #region FLoor
        private async Task FloorListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetFloorList();
                if (_viewModel.FloorList.Count > 0)
                {
                    await UnitListRecord(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task FloorDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsFloor = (string)poParam;
            try
            {

                _viewModel.GetList = new();
                FloorDTO FloorTemp = _viewModel.FloorList
                    .FirstOrDefault(data => data.CFLOOR_ID == lsFloor)!;
                _viewModel.FloorValue = FloorTemp;

                await UnitListRecord(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        #region Unit
        private async Task UnitListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetUnitList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task UnitDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsUnit = (string)poParam;
            try
            {
                _viewModel.GetList = new();

                UnitDTO UnitTemp = _viewModel.UnitList
                    .FirstOrDefault(data => data.CUNIT_ID == lsUnit)!;
                _viewModel.UnitValue = UnitTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.ValidationFieldEmpty();
                _viewModel.Parameter.CPROPERTY_ID = _viewModel.PropertyValue.CPROPERTY_ID;
                _viewModel.Parameter.CBUILDING_ID = _viewModel.BuildingValue.CBUILDING_ID;
                _viewModel.Parameter.CFLOOR_ID = _viewModel.FloorValue.CFLOOR_ID;
                _viewModel.Parameter.CUNIT_ID = _viewModel.UnitValue.CUNIT_ID;
                await GridRef!.R_RefreshGrid(_viewModel.Parameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LML01800ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetUnitTenantList(loParam);
                eventArgs.ListEntityResult = _viewModel.GetList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = GridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
