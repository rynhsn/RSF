using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PMT05500COMMON;
using PMT05500COMMON.DTO;
using PMT05500Model;
using PMT05500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT05500Front
{
    public partial class PMT05500:R_Page
    {
        private LMT05500AgreementViewModel _agreementViewModel = new();
        private R_Grid<LMT05500AgreementDTO>? _gridAgreementRef;
        private R_ConductorGrid? _conGridAgreement;

        private R_Grid<LMT05500UnitDTO>? _gridDepositUnitRef;
        private R_ConductorGrid? _conGridDepositUnit;

        private R_TabStrip? _tabStrip;
        private R_TabPage _tabPageDeposit;
        public bool _pageOnCRUDmode;
        private int _pageSizeAgg = 10;
        private int _pageSizeUnit = 5;
        #region PropertyID
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
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementViewModel.GetPropertyList();
                await _gridAgreementRef.R_RefreshGrid(null);
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
                _agreementViewModel.PropertyValueContext = lsProperty;
                await _gridAgreementRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region AgreementList
        private async Task R_ServiceAgreementListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementViewModel.GetAllAgreementList();
                eventArgs.ListEntityResult = _agreementViewModel.AgreementList;

                if (_agreementViewModel.AgreementList.Count > 0)
                {
                    await _gridDepositUnitRef.R_RefreshGrid(null);
                }
                else
                {
                    _agreementViewModel.poParamTabDeposit = null;
                    _agreementViewModel.DepositUnitList.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_DisplayAgreement(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loTemp = (LMT05500AgreementDTO)eventArgs.Data;
                    var loParam = R_FrontUtility.ConvertObjectToObject<LMT05500DBParameter>(loTemp);

                    _agreementViewModel.poParamTabDeposit = loParam;
                    await _gridDepositUnitRef!.R_RefreshGrid(null);
                }
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region DepositUnit
        private async Task R_ServiceDepositUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementViewModel.GetAllDepositUnitList();
                eventArgs.ListEntityResult = _agreementViewModel.DepositUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_DisplayUnit(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMT05500UnitDTO)eventArgs.Data;

                _agreementViewModel.poParamTabDeposit.CFLOOR_ID = loParam.CFLOOR_ID;
                _agreementViewModel.poParamTabDeposit.CUNIT_ID = loParam.CUNIT_ID;
            }
        }

        #endregion
        #region Filtering
        private async Task FilteringTranscode_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _agreementViewModel.cFilterByTransacodeFront = poParam;
                switch (poParam)
                {
                    case "802030":
                        //CR 03Okt2024
                        _agreementViewModel.cFilterByTransacodeBack = "802030,802061,802020";
                        break;
                    case "802032":
                        //CR 03Okt2024
                        _agreementViewModel.cFilterByTransacodeBack = "802032,802062";
                        break;
                    case "802033":
                        //CR 03Okt2024
                        _agreementViewModel.cFilterByTransacodeBack = "802033,802063";
                        break;
                }
                await _gridAgreementRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task FilteringActiveDoc_OnChange(bool poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _agreementViewModel.lIncldActiveDoc = poParam;               
                await _gridAgreementRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
        #region CHANGE TAB
        //CHANGE TAB
        private void Before_Open_Deposit(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(PMT05500Deposit);
            //you can use  _agreementViewModel._currentAgreement ==null, to assign on parameter;

            if (_agreementViewModel.AgreementList.Count > 0)
            {
                // var currentAgreement = _gridAgreementRef.GetCurrentData();
                var poParam = _agreementViewModel.poParamTabDeposit;

                eventArgs.Parameter = poParam;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }
        private void TabChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            _agreementViewModel._dropdownProperty = true;
            if (eventArgs.TabStripTab.Id != "TabAgreement")
            {
                _agreementViewModel._dropdownProperty = false;
            }
        }
        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pageOnCRUDmode = (bool)poValue;
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
