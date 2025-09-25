using PMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01531ViewModel
    {
        private PMM01530Model _PMM01530Model = new PMM01530Model();

        public ObservableCollection<PMM01531DTO> LookupOtherCharges { get; set; } = new ObservableCollection<PMM01531DTO>();

        public async Task GetLookupOtherCharges(PMM01531DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01530Model.GetOtherChargesLookupAsync(poParam);

                LookupOtherCharges = new ObservableCollection<PMM01531DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMM01531DTO> GetLookupOtherChargesRecord(PMM01531DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01531DTO loRtn = null;
            try
            {
                var loResult = await _PMM01530Model.GetOtherChargesRecordLookupAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

    }

}
