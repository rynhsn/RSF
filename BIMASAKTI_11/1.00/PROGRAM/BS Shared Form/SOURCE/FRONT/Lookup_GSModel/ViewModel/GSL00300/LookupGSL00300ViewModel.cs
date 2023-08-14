using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00300ViewModel : R_ViewModel<GSL00300DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL00300DTO> CurrencyGrid = new ObservableCollection<GSL00300DTO>();

        public async Task GetCurrencyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00300GetCurrencyListAsync();

                CurrencyGrid = new ObservableCollection<GSL00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
