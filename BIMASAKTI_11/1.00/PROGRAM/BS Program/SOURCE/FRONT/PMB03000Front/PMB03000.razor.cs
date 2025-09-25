using PMB03000Common.DTOs;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;

namespace PMB03000Front;

public partial class PMB03000 : R_Page
{
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMB03000PageParameterDTO)poParameter;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}