using PMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01003ViewModel : R_ViewModel<PMM01003DTO>
    {
        private PMM01000Model _PMM01000Model = new PMM01000Model();

        public PMM01003DTO ChargesType = new PMM01003DTO();

        public async Task CopyNewCharges(PMM01003DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMM01000Model.PMM01000CopyNewChargesAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }



    }
}
