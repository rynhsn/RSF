using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;

namespace PMT01300FRONT
{
    public partial class PMT01300 : R_Page
    {
        private string? _CTEXT_DATA_DUMMY = "BELOM DI INIT";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _CTEXT_DATA_DUMMY = "SUDAH DI INIT";
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

    }
}
