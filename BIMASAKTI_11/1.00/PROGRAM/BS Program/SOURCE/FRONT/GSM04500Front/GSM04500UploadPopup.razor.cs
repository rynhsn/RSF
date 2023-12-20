using R_BlazorFrontEnd.Exceptions;

namespace GSM04500Front;

public partial class GSM04500Upload
{
    protected override async Task R_Init_From_Master(object poParam)
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