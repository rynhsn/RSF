using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Front;

public partial class LMT03500UpdateMeter
{
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        
        try
        {
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
}