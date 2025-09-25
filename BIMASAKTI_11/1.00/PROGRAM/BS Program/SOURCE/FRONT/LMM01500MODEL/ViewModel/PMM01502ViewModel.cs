using PMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01502ViewModel : R_ViewModel<PMM01500DTO>
    {
        private PMM01500Model _PMM01500Model = new PMM01500Model();

        public ObservableCollection<PMM01502DTO> LookupBank { get; set; } = new ObservableCollection<PMM01502DTO>();

        public async Task GetLookupBankGrid()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01500Model.PMM01500LookupBankAsync();

                LookupBank = new ObservableCollection<PMM01502DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }

}
