using BlazorClientHelper;
using Global_PMCOMMON.DTOs.Response.Property;
using GSM02500COMMON.DTOs.GSM02503;
using Microsoft.AspNetCore.Components;
using PMT00100COMMON.Booking;
using PMT00100COMMON.UnitList;
using PMT00100FrontResources;
using PMT00100MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMT00100FRONT
{
    public partial class PMT00100 : R_Page
    {
        private PMT0100UnitListViewModel _viewModel = new();
        private PMT00100BookingViewModel _viewModelBooking = new();
        private R_ConductorGrid? _conductorBuilding;
        private R_Grid<PMT00100BuildingDTO>? _gridBuilding;
        private R_ConductorGrid? _conductorBuildingUnit;
        private R_Grid<PMT00100DTO>? _gridBuildingUnit;
        private R_ConductorGrid? _conductorAgreementByUnit;
        private R_Grid<PMT00100AgreementByUnitDTO>? _gridAgreementByUnit;

        private bool _gridBuildingEnabled = true;

        private int _pageSize = 7;
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

        #region PropertyId
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyList();
                await _gridBuilding.R_RefreshGrid(null);
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
                PropertyDTO PropertyTemp = _viewModel.PropertyList
                    .FirstOrDefault(data => data.CPROPERTY_ID == lsProperty)!;
                _viewModel.PropertyValue = PropertyTemp;
                _viewModel.Parameter.CPROPERTY_ID = PropertyTemp.CPROPERTY_ID!;
                await _gridBuilding.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region List
        private async Task GetListBuilding(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetBuildingList();
                eventArgs.ListEntityResult = _viewModel.BuildingList;
                //if (_viewModel.BuildingList.Count < 0)
                //{
                //    _viewModel.BuildingUnitList = new();
                //    _viewModel.AgreementByUnitList = new();
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_DisplayBuildingAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00100BuildingDTO)eventArgs.Data;
                if (loData != null)
                {
                    _viewModel.Parameter.CBUILDING_ID = loData.CBUILDING_ID;
                    _viewModel.Parameter.CBUILDING_NAME = loData.CBUILDING_NAME;
                    await _gridBuildingUnit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task GetListBuildingUnit(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetBuildingUnitList();
                eventArgs.ListEntityResult = _viewModel.BuildingUnitList;
                if (_viewModel.BuildingUnitList.Count > 0)
                {
                    var loGroupDescriptor = new List<R_GridGroupDescriptor>
                {
                    new() { FieldName = "TYPE" }
                };
                    await _gridAgreementByUnit.R_GroupBy(loGroupDescriptor);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_DisplayBuildingUnit(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00100DTO)eventArgs.Data;
                if (loData != null)
                {
                    _viewModel.btnSold_Booking = loData.CSTRATA_STATUS == "01" || loData.CSTRATA_STATUS == "02";
                    _viewModel.Parameter.CFLOOR_ID = loData.CFLOOR_ID;
                    _viewModel.Parameter.CFLOOR_NAME = loData.CFLOOR_NAME;
                    _viewModel.Parameter.CUNIT_ID = loData.CUNIT_ID;
                    _viewModel.Parameter.CUNIT_NAME = loData.CUNIT_NAME;
                    _viewModel.Parameter.CUNIT_TYPE_ID = loData.CUNIT_TYPE_ID;
                    _viewModel.Parameter.CUNIT_TYPE_NAME = loData.CUNIT_TYPE_NAME;
                    _viewModel.Parameter.NGROSS_AREA_SIZE = loData.NGROSS_AREA_SIZE;
                    _viewModel.Parameter.NNET_AREA_SIZE = loData.NNET_AREA_SIZE;
                    _viewModel.Parameter.CUNIT_CATEGORY_ID = loData.CUNIT_CATEGORY_ID;
                    _viewModel.Parameter.CUNIT_CATEGORY_NAME = loData.CUNIT_CATEGORY_ID;
                    _viewModel.Parameter.CTRANS_CODE = "802011";
                    await _gridAgreementByUnit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task GetListAgreementByUnit(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAgreementByUnitList();
                eventArgs.ListEntityResult = _viewModel.AgreementByUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_DisplayAgreementByUnit(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00100AgreementByUnitDTO)eventArgs.Data;
                if (loData != null)
                {
                    _viewModel.btnChangeUnitAndRealease = loData.CBOOK_STATUS == "01" || loData.CBOOK_STATUS == "02";

                    _viewModel.Parameter.CDEPT_CODE = loData.CDEPT_CODE;
                    _viewModel.Parameter.CTRANS_CODE = loData.CTRANS_CODE;
                    _viewModel.Parameter.CREF_NO = loData.CREF_NO;
                    _viewModel.Parameter.CTENANT_ID = loData.CTENANT_ID;
                    _viewModel.Parameter.CTENANT_NAME = loData.CTENANT_NAME;
                    _viewModel.Parameter.CTRANS_STATUS = loData.CTRANS_STATUS;

                }
                switch (loData.CTRANS_STATUS)
                {
                    case "00":
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = true;
                        //_viewModel.lControlButtonChangeUnit = true;
                        break;
                    case "10":
                        _viewModel.lControlButtonSubmit = false;
                        _viewModel.lControlButtonRedraft = true;
                        //_viewModel.lControlButtonChangeUnit = true;
                        break;
                    case "30":
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = false;
                        //_viewModel.lControlButtonChangeUnit = false;
                        break;
                    case "80":
                        //_viewModel.lControlButtonChangeUnit = false;
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = false;
                        break;
                    case "98":
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = false;
                        break;
                    default:
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = false;
                        break;
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region Button Control

        private void BeforeOpenPopupBooking(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                eventArgs.Parameter = _viewModel.Parameter;
                _viewModel.Parameter.CCALLER_ACTION = "BUTTON_BOOKING";
                eventArgs.TargetPageType = typeof(PMT00100Booking);

                //PMT00100BookingDTO vartemp = new PMT00100BookingDTO()
                //{
                //    CCOMPANY_ID = "RCD",
                //    CDEPT_CODE = "FIN",
                //    CPROPERTY_ID = "ASHMD",
                //    CTRANS_CODE = "802011",
                //    CREF_NO = "LOOFIN-20250100004-",
                //    CFLOOR_ID = "Floor1",
                //    CUNIT_ID = "UNT09",
                //    CUNIT_NAME = "Unit 9",
                //    CUNIT_TYPE_ID = "2BRoom",
                //    CUNIT_TYPE_NAME = "Two Bath Room",
                //    CCALLER_ACTION = "VIEW"
                //};
                //eventArgs.Parameter = vartemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task AfterOpenPopupBooking(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    await _gridAgreementByUnit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private void BeforeOpenViewImage(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new ImageTabParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = _viewModel.Parameter.CPROPERTY_ID,
                    CSELECTED_UNIT_TYPE_ID = _viewModel.Parameter.CUNIT_TYPE_ID
                };
                eventArgs.Parameter = loParam;
                eventArgs.PageNamespace = "GSM02500FRONT.GSM02503Image";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task AfterOpenViewImage(R_AfterOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private void BeforeOpenSold(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Parameter.CCALLER_ACTION = "ADD";
                eventArgs.Parameter = _viewModel.Parameter;
                eventArgs.PageNamespace = "PMT00300Front.PMT00300LOIInfo_UnitInfo";
                //var res = await R_MessageBox.Show("", "Feature under development",
                //   R_eMessageBoxButtonType.OK);
                //    eventArgs.TargetPageType = typeof(PMT00100Booking);

                //   await Close(false, false);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task AfterOpenSold(R_AfterOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private void BeforeOpenPopupChangeUnit(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = _viewModel.Parameter;
                eventArgs.TargetPageType = typeof(PMT00100ChangeUnit);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task AfterOpenPopupChangeUnitAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    await _gridAgreementByUnit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private void BeforeOpenPopupView(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                _viewModel.Parameter.CCALLER_ACTION = "BUTTON_VIEW";
                eventArgs.Parameter = _viewModel.Parameter;
                eventArgs.TargetPageType = typeof(PMT00100Booking);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private async Task AfterOpenPopupView(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    await _gridAgreementByUnit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private async Task SubmitBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //SUBMIT CODE == "10"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT00100_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModelBooking.loEntityAgreementByUnit = R_FrontUtility.ConvertObjectToObject<PMT00100AgreementByUnitDTO>(_conductorAgreementByUnit.R_GetCurrentData());

                    var loReturn = await _viewModelBooking.ProcessUpdateAgreement(lcNewStatus: "10");
                    if (loReturn.IS_PROCESS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT00100_Class), "_SuccessMessageOfferSubmit"));
                        await _gridAgreementByUnit.R_RefreshGrid(null);
                        // await _conductorAgreementByUnit.Re.Aull);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT00100_Class), "_FailedUpdate"));
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        private async Task RedraftBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                  R_FrontUtility.R_GetMessage(typeof(Resources_PMT00100_Class), "_ConfirmationRedraft"),
                  R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModelBooking.loEntityAgreementByUnit = R_FrontUtility.ConvertObjectToObject<PMT00100AgreementByUnitDTO>(_conductorAgreementByUnit.R_GetCurrentData());
                    var loReturn = await _viewModelBooking.ProcessUpdateAgreement(lcNewStatus: "00");
                    if (loReturn.IS_PROCESS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT00100_Class), "_SuccessMessageOfferRedraft"));
                        await _gridAgreementByUnit.R_RefreshGrid(null);
                        // await _conductorAgreementByUnit.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT00100_Class), "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        #endregion
        #region Locking
        [Inject] IClientHelper? _clientHelper { get; set; }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                //_viewModelBooking.loEntityAgreementByUnit = R_FrontUtility.ConvertObjectToObject<PMT00100AgreementByUnitDTO>(_conductorAgreementByUnit.R_GetCurrentData());

                var loData = R_FrontUtility.ConvertObjectToObject<PMT00100AgreementByUnitDTO>(_conductorAgreementByUnit.R_GetCurrentData());
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
                if (param) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT00100",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT00100",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
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
