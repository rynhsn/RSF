using System.Collections.ObjectModel;
using Global_PMCOMMON.DTOs.User_Param_Detail;
using Global_PMModel.ViewModel;
using Lookup_PMCOMMON.DTOs.GET_USER_PARAM_DETAIL;
using Lookup_PMModel.ViewModel.GetUserParamDetail;
using PMT01700FRONT;
using PMT01800COMMON.DTO;
using PMT01800Model;
using PMT01810Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT01800Front;

public partial class PMT01800 : R_Page
{
    private PMT01800ViewModel _viewModelPMT01800 = new();
    private PMT01810ViewModel _viewModelPMT01810 = new();
    private GlobalFunctionViewModel _viewModelGetUserParamDetail = new();
    private R_Conductor _conductorRefPMT01800;
    private R_ConductorGrid _conductorGridPMT01800;
    private R_ConductorGrid _conductorGridPMT01810;
    private R_Grid<PMT01800DTO> _gridRefHead;
    private R_Grid<PMT01800DTO> _gridRefChild;

    private R_TabStrip _tabStripRef;
    private R_TabPage _tabPageLOCRef;
    private string _lastProperty;
    public bool enableDisableProperty = true;
    private bool hasShownWarning = false;


    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loReturn = await _viewModelGetUserParamDetail.GetUserParamDetail(new GetUserParamDetailParameterDTO()
            {
                CCODE = "153",
            });

            int loValueInt = 0;
            bool checkSuccessConvert = int.TryParse(loReturn.CVALUE, out loValueInt);

            _viewModelPMT01800.OfferDate = checkSuccessConvert == true? DateTime.Now.AddDays(-loValueInt): DateTime.Now;

            await _viewModelPMT01800.GetProperty();
            await _viewModelPMT01800.GetSpinnerYear();
            await OnChangedProperty(_viewModelPMT01800.propertyValue);
        }

        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task OnChangedProperty(object poParam)
    {
        var loEx = new R_Exception();
        string lsProperty = (string)poParam;
        try
        {
            
            _viewModelPMT01800.propertyValue = string.IsNullOrEmpty(lsProperty) ? "" : lsProperty;
            DateTime ldTimeNow = _viewModelPMT01800.OfferDate != null
                ? _viewModelPMT01800.OfferDate.Value
                : DateTime.Now;

            if (_tabStripRef.ActiveTab.Id == nameof(PMT01810))
            {
                if (_conductorGridPMT01800.R_ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = new PMT01800DTO();
                    loParam.CPROPERTY_ID = _viewModelPMT01800.propertyValue;

                    await _tabPageLOCRef.InvokeRefreshTabPageAsync(loParam);
                }
            }
            else
            {
                _viewModelPMT01800.OfferDate = ldTimeNow;
                await OnchangeDate();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


        R_DisplayException(loEx);
    }


    private async Task ServiceGetRecord(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01800DTO>(eventArgs.Data);

                _viewModelPMT01800.TransCode = loParam.CTRANS_CODE;
                _viewModelPMT01800.DeptCode = loParam.CDEPT_CODE;
                _viewModelPMT01800.RefNo = loParam.CREF_NO;
                _viewModelPMT01800.buildingId = loParam.CBUILDING_ID;
                _viewModelPMT01800.BuildingName = loParam.CBUILDING_NAME;


                await _gridRefChild.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private string BuildName = "";

    private async Task R_DIsplayChild(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01800DTO>(eventArgs.Data);

                BuildName = loParam.CBUILDING_NAME;


                // await  _gridRef10510.R_RefreshGrid(null);
                // await _viewModelCBI00100.GetAgeingDTList();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GridServiceGetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelPMT01800.GetGridListHeader();
            if (_viewModelPMT01800.loGridList.Count == 0)
            {
                hasShownWarning = false;
            }
            else
            {
                hasShownWarning = true;
            }

            if (_viewModelPMT01800.loGridList.Count == 0 && !hasShownWarning)
            {
                hasShownWarning = true;
            }

            eventArgs.ListEntityResult = _viewModelPMT01800.loGridList;
            await _gridRefChild.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task GridServiceGetListDetail(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelPMT01800.GetGridListDetail();
            eventArgs.ListEntityResult = _viewModelPMT01800.LoGridListDetail;

            if (_viewModelPMT01800.LoGridListDetail.Count == 0 &&
                _viewModelPMT01800.loGridList.Count != 0 && !hasShownWarning)
            {
                await R_MessageBox.Show("", _localizer["DATANOTFOUND"], R_eMessageBoxButtonType.OK);
                hasShownWarning = true;
            }

            if (_viewModelPMT01800.loGridList.Count == 0)
            {
                BuildName = "";
                _viewModelPMT01800.LoGridListDetail.Clear();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task OnchangeDate()
    {
        var loEx = new R_Exception();

        try

        {
            _viewModelPMT01800.OfferDateString = _viewModelPMT01800.OfferDate?.ToString("yyyyMMdd");

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


        R_DisplayException(loEx);
    }

    private void BeforeOpenTabPage_Undo(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        var loParam = new PMT01800DTO();
        loParam.CPROPERTY_ID = _viewModelPMT01800.propertyValue;
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(PMT01810);
    }


    // private void LOOListActive(R_TabStripTab eventArgs)
    // {
    //     // _tabStripRef.ActiveTab.Id
    //     enableDisableProperty = _tabStripRef.ActiveTab.Id == nameof(PMT01800);
    // }


    private void BeforeOpenPMT01700(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            PMT01800DTO loParam = new PMT01800DTO()
            {
                CPROPERTY_ID = _viewModelPMT01800.propertyValue,
                CDEPT_CODE = _viewModelPMT01800.DeptCode,
                CTRANS_CODE = _viewModelPMT01800.TransCode,
                CREF_NO = _viewModelPMT01800.RefNo,
                CBUILDING_ID = _viewModelPMT01800.buildingId,
                CBUILDING_NAME = _viewModelPMT01800.BuildingName,
                CALLER_ACTION = "ADD",
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PMT01700LOO_Offer);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task R_After_ServiceOpenOthersProgram(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.Result != null)
            {
                await _tabStripRef.SetActiveTabAsync(nameof(PMT01810));
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ActiveIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        if (eventArgs.TabStripTab.Id == nameof(PMT01800))
        {
            if (_lastProperty != _viewModelPMT01800.propertyValue)
            {
            }
        }
        else
        {
            _lastProperty = _viewModelPMT01800.propertyValue;
        }
    }
    
    public async Task Refresh_Button()
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelPMT01800.GetGridListHeader();
            await _gridRefHead.R_RefreshGrid(null);
            if (_viewModelPMT01800.loGridList.Count == 0)
            {
                await R_MessageBox.Show("", _localizer["DATANOTFOUND"], R_eMessageBoxButtonType.OK);
                return;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


    }
}