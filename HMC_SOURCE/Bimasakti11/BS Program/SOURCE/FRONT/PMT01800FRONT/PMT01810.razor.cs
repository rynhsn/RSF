using PMT01800COMMON.DTO;
using PMT01810Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT01800Front;

public partial class PMT01810 : R_Page, R_ITabPage
{
    public PMT01810ViewModel _viewModelPMT01810 = new();
    private PMT01810ViewModel _viewModelPMT01810Detail = new();
    private R_Conductor _conductorGridPMT01800;
    private R_ConductorGrid _conductorGridPMT01810;
    private R_Grid<PMT01810DTO> _gridRefHead;
    private R_Grid<PMT01810DTO> _gridRefChild;
    private bool hasShownWarning = false;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMT01800DTO)poParameter;
            _viewModelPMT01810.propertyValue = loParam.CPROPERTY_ID;

            await _gridRefHead.R_RefreshGrid(loParam);

            // enableProperty = true;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task GridServiceGetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT01800DTO>(eventArgs.Parameter);
            _viewModelPMT01810Detail.propertyValue = loParam.CPROPERTY_ID;
            await _viewModelPMT01810.GetGridListHeader(loParam);
            eventArgs.ListEntityResult = _viewModelPMT01810.loGridList;
            if (_viewModelPMT01810.loGridList.Count == 0)
            {
                await R_MessageBox.Show("", _localizer["DATANOTFOUND"], R_eMessageBoxButtonType.OK);
                await _gridRefChild.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    // private async Task R_Display(R_DisplayEventArgs eventArgs)
    // {
    //     var loEx = new R_Exception();
    //     try
    //     {
    //         if (eventArgs.ConductorMode == R_eConductorMode.Normal)
    //         {
    //             var loParam = R_FrontUtility.ConvertObjectToObject<PMT01800DTO>(eventArgs.Data);
    //
    //             _viewModelPMT01810.TransCode = loParam.CTRANS_CODE;
    //             _viewModelPMT01810.DeptCode = loParam.CDEPT_CODE;
    //             _viewModelPMT01810.RefNo = loParam.CREF_NO;
    //             await _gridRefChild.R_RefreshGrid(null);
    //
    //             // await  _gridRef10510.R_RefreshGrid(null);
    //             // await _viewModelCBI00100.GetAgeingDTList();
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    // }

    private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT01810DTO>(eventArgs.Data);

            _viewModelPMT01810.TransCode = loParam.CTRANS_CODE;
            _viewModelPMT01810.DeptCode = loParam.CDEPT_CODE;
            _viewModelPMT01810.RefNo = loParam.CREF_NO;
            eventArgs.Result = loParam;

            // await _gridRefChild.R_RefreshGrid(null);


            // await  _gridRef10510.R_RefreshGrid(null);
            // await _viewModelCBI00100.GetAgeingDTList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task R_Display_Head(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01810DTO>(eventArgs.Data);

                _viewModelPMT01810.TransCode = loParam.CTRANS_CODE;
                _viewModelPMT01810.DeptCode = loParam.CDEPT_CODE;
                _viewModelPMT01810.RefNo = loParam.CREF_NO;

                await _gridRefChild.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
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

    private async Task GridServiceGetListDetail(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelPMT01810.GetGridListDetail();
            eventArgs.ListEntityResult = _viewModelPMT01810.LoGridListDetail;
            //await _gridRef.AutoFitAllColumnsAsync();
            if (_gridRefChild.DataSource.Count == 0 && _gridRefHead.DataSource.Count != 0)
            {
                await R_MessageBox.Show("", _localizer["DATANOTFOUND"], R_eMessageBoxButtonType.OK);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task Conductor_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMT01810DTO)eventArgs.Data;
            await _viewModelPMT01810.Delete(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task RefreshTabPageAsync(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMT01800DTO)poParam;

            await _gridRefHead.R_RefreshGrid(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}