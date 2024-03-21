using LMT03500Common.DTOs;
using LMT03500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMT03500Front;

public partial class LMT03500ChangeMeterPopup : R_Page
{
    private LMT03500UpdateMeterViewModel _viewModel = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var lo = (LMT03500UtilityMeterDetailDTO)poParameter;
            var loParam = R_FrontUtility.ConvertObjectToObject<LMT03500UtilityMeterDTO>(lo);
            await _viewModel.GetRecord(loParam);
            _viewModel.Entity.CUNIT_NAME = lo.CUNIT_NAME;
            _viewModel.Entity.CTENANT_ID = lo.CTENANT_ID;
            _viewModel.Entity.CTENANT_NAME = lo.CTENANT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickCancel()
    {
        await this.Close(true, false);
    }

    private void OnClickSave()
    {
        throw new NotImplementedException();
    }
}