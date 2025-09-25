using GLM00100Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;

namespace GLM00100Front;

public partial class GLM00100SystemParameterPopUp : R_Page
{
    private readonly GLM00100ViewModel _glm00100ViewModel = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _glm00100ViewModel.GLM00100GetDataValueForCreateSystemParameter();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }


    private void OnChangedMonth(string pcParam)
    {
        _glm00100ViewModel.loCreateDataGLM00100.CSTARTING_MM = pcParam;
    }


    private async Task OnClickProcessButton()
    {
        bool loRes = await _glm00100ViewModel.GLM00100CreateSystemParameter();
        if (loRes)
            await Close(true, true);
    }

    private async Task OnClickCancelButton()
    {
        await CloseProgram();
    }
}