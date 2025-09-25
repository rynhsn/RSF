using BlazorClientHelper;
using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Utility;
using PMT02000MODEL;
using PMT02000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02000FRONT
{
    public partial class PMT02000 : R_Page
    {
        private PMT02000ViewModel _viewModel = new();
        private R_Grid<PMT02000LOIDTO>? _gridLOIref;
        private R_ConductorGrid? _conGridLOI;

        private R_TabStrip? _tabLOI;
        private R_TabPage? _tabHO;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region PropertyID
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyList();
                await _gridLOIref!.R_RefreshGrid(null);
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
                _viewModel.PropertyValueID = lsProperty;
                if (_tabLOI!.ActiveTab.Id == "TabLOI")
                {
                    await _gridLOIref!.R_RefreshGrid(null);
                }
                if (_tabLOI!.ActiveTab.Id == "Tab_HandOver")
                {
                    await _tabHO!.InvokeRefreshTabPageAsync(lsProperty);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            await R_DisplayExceptionAsync(loEx);
        }
        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "TabLOI":
                    await _gridLOIref!.R_RefreshGrid(null);
                    break;
            }

        }
        #endregion
        #region LOIList
        private async Task R_ServiceLOIListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAllList("LOI");
                eventArgs.ListEntityResult = _viewModel.LOIList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loTemp = (PMT02000LOIDTO)eventArgs.Data;
                    _viewModel._CurrentLOI = loTemp;
                }
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        private void Btn_HandOver(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader>(_viewModel._CurrentLOI);
                string lcPropertyName = _viewModel.PropertyList.FirstOrDefault(item => item.CPROPERTY_ID == _viewModel.PropertyValueID)?.CPROPERTY_NAME!;
                loParam.CSAVEMODE = "NEW";
                loParam.CPROPERTY_NAME = lcPropertyName;

                eventArgs.TargetPageType = typeof(PopUpHandOver);
                eventArgs.Parameter = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Before_Open_DepositInfo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _viewModel.PropertyValueID;
                eventArgs.TargetPageType = typeof(PMT02000HO);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
    }
}
