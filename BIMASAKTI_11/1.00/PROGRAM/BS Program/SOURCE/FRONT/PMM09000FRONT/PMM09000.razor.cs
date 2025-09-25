using BlazorClientHelper;
using Global_PMCOMMON.DTOs.Response.Property;
using Microsoft.AspNetCore.Components;
using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using PMM09000FrontResources;
using PMM09000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000FRONT
{
    public partial class PMM09000 : R_Page
    {
        private PMM9000RecordViewModel _RecordDataViewModel = new();
        private PMM09000AmortizationViewModel _ListDataViewModel = new();

        private R_Grid<PMM09000BuildingDTO>? _gridBuilding;
        private R_Grid<PMM09000AmortizationDTO>? _gridAmortization;
        private R_Grid<PMM09000AmortizationSheduleDetailDTO>? _gridAmortizationSchedule;

        private R_ConductorGrid? _conGridBuilding;
        private R_ConductorGrid? _conGridAmortization;
        private R_ConductorGrid? _conGridAmortizationShedule;

        private R_TabStrip? _tabStrip;
        private R_TabPage? _tabPageAmortizationEntry;

        private int _pageSizeBuilding = 15;
        private int _pageSizeAmortization = 10;
        private int _pageSizeAmortizationShedule = 10;
        public bool _pageOnCRUDmode;
        public bool _lGridBuildingEnabled = true;
        private bool _lDataExist;
        public bool lControlButtonClose;
        public bool lControlButtonReopen;
        private PMM09000EntryHeaderDTO _AmortizationDetail = new();

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

        #region Filter
        private async Task Filtering_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            string lsUnitOption = poParam;
            try
            {
                _ListDataViewModel._UnitOptionValue = lsUnitOption;
                _lGridBuildingEnabled = true;
                switch (lsUnitOption)
                {
                    case "U":
                        await _gridBuilding!.R_RefreshGrid(null);
                        break;
                    case "O":
                        _ListDataViewModel._BuildingList.Clear();
                        _lGridBuildingEnabled = false;
                        _ListDataViewModel._BuildingValue.CBUILDING_ID = "";
                        await _gridAmortization.R_RefreshGrid(null);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region PropertyID
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _ListDataViewModel.GetPropertyList();
                if (_ListDataViewModel._PropertyList.Any())
                {
                    await _gridBuilding.R_RefreshGrid(null);
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
                PropertyDTO PropertyTemp = _ListDataViewModel._PropertyList
                    .FirstOrDefault(data => data.CPROPERTY_ID == lsProperty)!;

                _ListDataViewModel._UnitOptionValue = "U";
                _ListDataViewModel._PropertyValue = PropertyTemp;
                await _gridBuilding.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region BuildingList
        private async Task R_ServiceBuildingListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _ListDataViewModel.GetBuildingList();
                eventArgs.ListEntityResult = _ListDataViewModel._BuildingList;

                if (!_ListDataViewModel._BuildingList.Any())
                {
                    //  await _gridAmortization.R_RefreshGrid(null);
                    _ListDataViewModel._AmortizationList.Clear();
                    _AmortizationDetail.CUNIT_DESCRIPTION = "";
                    _ListDataViewModel._AmortizationScheduleList.Clear();
                }
                //else
                //{
                //    _ListDataViewModel.poParamTabDeposit = null;
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_DisplayBuilding(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loData = (PMM09000BuildingDTO)eventArgs.Data;
                    //  var loParam = R_FrontUtility.ConvertObjectToObject<LMT05500DBParameter>(loData);

                    _ListDataViewModel._BuildingValue = loData;
                    await _gridAmortization.R_RefreshGrid(null);
                }
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region AmortizationList
        private async Task R_ServiceAmortizationListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _ListDataViewModel.GetAmortizationList();
                eventArgs.ListEntityResult = _ListDataViewModel._AmortizationList;

                if (_ListDataViewModel._AmortizationList.Any())
                {
                    _lDataExist = true;
                }
                else
                {
                    _AmortizationDetail.CUNIT_DESCRIPTION = "";
                    _ListDataViewModel._AmortizationScheduleList.Clear();
                    _lDataExist = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_DisplayAmortizaton(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loData = (PMM09000AmortizationDTO)eventArgs.Data;
                    //  var loParam = R_FrontUtility.ConvertObjectToObject<LMT05500DBParameter>(loData);

                    _ListDataViewModel._AmortizationValue = loData;
                    lControlButtonClose = loData.CSTATUS == "10" ? true : false;
                    lControlButtonReopen = loData.CSTATUS == "80" ? true : false;
                    if (_ListDataViewModel._AmortizationValue.CTRANS_TYPE != null)
                    {
                        await AmortizationDetail();
                        await _gridAmortizationSchedule.R_RefreshGrid(null);

                    }
                }
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region AmortizationList
        private async Task AmortizationDetail()
        {
            var loEx = new R_Exception();
            try
            {
                PMM09000DbParameterDTO loParameter = new PMM09000DbParameterDTO
                {
                    CPROPERTY_ID = _ListDataViewModel._PropertyValue.CPROPERTY_ID!,
                    CUNIT_OPTION = _ListDataViewModel._UnitOptionValue,
                    CBUILDING_ID = _ListDataViewModel._BuildingValue.CBUILDING_ID,
                    CTRANS_TYPE = _ListDataViewModel._AmortizationValue.CTRANS_TYPE, //ERROR
                    CREF_NO = _ListDataViewModel._AmortizationValue.CREF_NO.Trim(),
                };
                _AmortizationDetail = await _RecordDataViewModel.GetAmortizationDetail(loParameter);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region AmortizationScheduleList
        private async Task R_ServiceAmortizationScheduleListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMM09000DbParameterDTO loParameter = new PMM09000DbParameterDTO
                {
                    CPROPERTY_ID = _ListDataViewModel._PropertyValue.CPROPERTY_ID!,
                    CUNIT_OPTION = _ListDataViewModel._UnitOptionValue,
                    CBUILDING_ID = _ListDataViewModel._BuildingValue.CBUILDING_ID,
                    CTRANS_TYPE = _ListDataViewModel._AmortizationValue.CTRANS_TYPE,
                    CREF_NO = _ListDataViewModel._AmortizationValue.CREF_NO.Trim()
                };
                await _ListDataViewModel.GetAmortizationScheduleList(loParameter);
                eventArgs.ListEntityResult = _ListDataViewModel._AmortizationScheduleList;

                if (!_ListDataViewModel._AmortizationScheduleList.Any())
                {

                    _ListDataViewModel._AmortizationScheduleList.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Button
        private async Task Btn_CloseAsync()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Close_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {

                    PMM09000DbParameterDTO poParam = new PMM09000DbParameterDTO
                    {
                        CPROPERTY_ID = _ListDataViewModel._PropertyValue.CPROPERTY_ID!,
                        CUNIT_OPTION = _ListDataViewModel._UnitOptionValue,
                        CBUILDING_ID = _ListDataViewModel._UnitOptionValue == "O" ? "" : _ListDataViewModel._BuildingValue.CBUILDING_ID,
                        CTRANS_TYPE = _ListDataViewModel._AmortizationValue.CTRANS_TYPE,
                        CREF_NO = _ListDataViewModel._AmortizationValue.CREF_NO.Trim(),
                        CACTION = "Close"
                    };
                    var loReturn = await _ListDataViewModel.ProcessUpdateAmortization(poParam);

                    if (loReturn.LSTATUS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_SuccessMessageClose"));
                        await _gridAmortization.R_RefreshGrid(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_FailedUpdate"));
                    }
                }
                else
                {
                    return;

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
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_ReOpenAsync()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["ReOpen_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    PMM09000DbParameterDTO poParam = new PMM09000DbParameterDTO
                    {
                        CPROPERTY_ID = _ListDataViewModel._PropertyValue.CPROPERTY_ID!,
                        CUNIT_OPTION = _ListDataViewModel._UnitOptionValue,
                        CBUILDING_ID = _ListDataViewModel._UnitOptionValue == "O" ? "" : _ListDataViewModel._BuildingValue.CBUILDING_ID,
                        CTRANS_TYPE = _ListDataViewModel._AmortizationValue.CTRANS_TYPE,
                        CREF_NO = _ListDataViewModel._AmortizationValue.CREF_NO.Trim(),
                        CACTION = "Open"
                    };
                    var loReturn = await _ListDataViewModel.ProcessUpdateAmortization(poParam);

                    if (loReturn.LSTATUS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_SuccessMessageReOpen"));
                        await _gridAmortization.R_RefreshGrid(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_FailedUpdate"));
                    }
                }
                else
                {
                    return;

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
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region CHANGE TAB
        //CHANGE TAB
        private void Before_Open_AmotizationEntry(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(PMM09000AmortizatonEntry);

            if (_ListDataViewModel._PropertyList.Any())
            {
                PMM09000DbParameterDTO poParam = new PMM09000DbParameterDTO
                {
                    CPROPERTY_ID = _ListDataViewModel._PropertyValue.CPROPERTY_ID!,
                    CUNIT_OPTION = _ListDataViewModel._UnitOptionValue,
                    CBUILDING_ID = _ListDataViewModel._BuildingValue.CBUILDING_ID!,
                    CTRANS_TYPE = _ListDataViewModel._AmortizationValue.CTRANS_TYPE!,
                    CREF_NO = _ListDataViewModel._AmortizationValue.CREF_NO!,
                    CGENERATE_MODE  = _ListDataViewModel._AmortizationValue.CGENERATE_MODE!,
                };
                eventArgs.Parameter = poParam;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }
        private void TabChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            _ListDataViewModel._dropdownProperty = true;
            if (eventArgs.TabStripTab.Id == "TabAmortizationEntry")
            {
                   _ListDataViewModel._dropdownProperty = false;
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

        #region Locking
        [Inject] IClientHelper clientHelper { get; set; }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData =  _AmortizationDetail;
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
                if (param) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM09000",
                        Table_Name = "PMM_AMORTIZATION_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM09000",
                        Table_Name = "PMM_AMORTIZATION_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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
