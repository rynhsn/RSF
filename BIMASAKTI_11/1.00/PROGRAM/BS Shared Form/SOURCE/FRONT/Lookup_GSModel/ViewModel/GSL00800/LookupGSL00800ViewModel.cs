using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00800ViewModel : R_ViewModel<GSL00800DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL00800DTO> CurrencyRateTypeGrid = new ObservableCollection<GSL00800DTO>();

        public async Task GetCurrencyRateTypeList(GSL00800ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00800GetCurrencyTypeListAsync();

                CurrencyRateTypeGrid = new ObservableCollection<GSL00800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
