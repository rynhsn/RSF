using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using PMT03500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Front;

public partial class PMT03500ChangeMeterPopup : R_Page
{
    private PMT03500UpdateMeterViewModel _viewModel = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (PMT03500UtilityMeterDetailDTO)poParameter;
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT03500UtilityMeterDTO>(loData);
            await _viewModel.Init(loParam.CPROPERTY_ID);
            await _viewModel.GetRecord(loParam);
            await _viewModel.GetMeterNoList(loParam);
            await _viewModel.GetPeriodRangeList();
            
            _viewModel.Entity.CBUILDING_ID = loData.CBUILDING_ID;
            _viewModel.Entity.CUNIT_NAME = loData.CUNIT_NAME;
            _viewModel.Entity.CTENANT_ID = loData.CTENANT_ID;
            _viewModel.Entity.CTENANT_NAME = loData.CTENANT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickCancel()
    {
        await this.Close(false, true);
    }

    private async Task OnClickSave()
    {
        var loEx = new R_Exception();
        try
        {
            if (_viewModel.UtilityType == EPMT03500UtilityUsageType.EC)
            {

                if (_viewModel.Entity.IMETER_MAX_RESET > 0)
                {
                    if (_viewModel.Entity.NTO_BLOCK1_START > _viewModel.Entity.IMETER_MAX_RESET)
                    {
                        loEx.Add("Error", $"Block 1 Start cannot be greater than Meter Max Reset: {_viewModel.Entity.IMETER_MAX_RESET}");
                    }

                    if (_viewModel.Entity.NTO_BLOCK2_START > _viewModel.Entity.IMETER_MAX_RESET)
                    {
                        loEx.Add("Error", $"Block 2 Start cannot be greater than Meter Max Reset: {_viewModel.Entity.IMETER_MAX_RESET}");
                    }
                    
                    // return;
                }

                
                if (_viewModel.Entity.NBLOCK1_END < 0)
                    loEx.Add("Error", "Please fill in the Block 1 End");
                if (_viewModel.Entity.NBLOCK2_END < 0)
                    loEx.Add("Error", "Please fill in the Block 2 End");
                if (_viewModel.Entity.NTO_BLOCK1_START < 0)
                    loEx.Add("Error", "Please fill in the Block 1 Start");
                if (_viewModel.Entity.NTO_BLOCK2_START < 0)
                    loEx.Add("Error", "Please fill in the Block 2 Start");
            }
            else if (_viewModel.UtilityType == EPMT03500UtilityUsageType.WG)
            {
                if (_viewModel.Entity.IMETER_MAX_RESET > 0)
                {
                    if (_viewModel.Entity.NTO_METER_START > _viewModel.Entity.IMETER_MAX_RESET)
                    {
                        loEx.Add("Error", $"Meter Start cannot be greater than Meter Max Reset: {_viewModel.Entity.IMETER_MAX_RESET}");
                    }
                    
                    // return;
                }
                
                if (_viewModel.Entity.NMETER_END < 0)
                    loEx.Add("Error", "Please fill in the Meter End");
                if (_viewModel.Entity.NTO_METER_START < 0)
                    loEx.Add("Error", "Please fill in the Meter Start");
            }
            
            if (_viewModel.Entity.CTO_METER_NO == null) 
                loEx.Add("Error", "Please fill in the To Meter No");
            
            if (_viewModel.Entity.DSTART_DATE_CHANGE == null)
                loEx.Add("Error", "Please fill in the Start Date");
            
            if (loEx.HasError)
                goto EndBlock;
            
            
            _viewModel.Entity.CSTART_INV_PRD = _viewModel.CSTART_INV_PRD_YEAR + _viewModel.CSTART_INV_PRD_MONTH;
            _viewModel.Entity.CTENANT_ID ??= "";
            
            await _viewModel.ChangeMeterNo(_viewModel.Entity);
            var loResult = await R_MessageBox.Show("Success", "Data has been changed", R_eMessageBoxButtonType.OK);

            if (loResult == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            
        }
            
        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
}