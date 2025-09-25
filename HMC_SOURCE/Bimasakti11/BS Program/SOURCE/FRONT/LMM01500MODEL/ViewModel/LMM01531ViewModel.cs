using LMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01531ViewModel
    {
        private LMM01530Model _LMM01530Model = new LMM01530Model();

        public ObservableCollection<LMM01531DTO> LookupOtherCharges { get; set; } = new ObservableCollection<LMM01531DTO>();

        public async Task GetLookupOtherCharges(LMM01531DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01530Model.GetOtherChargesLookupAsync(poParam);

                LookupOtherCharges = new ObservableCollection<LMM01531DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }

}
