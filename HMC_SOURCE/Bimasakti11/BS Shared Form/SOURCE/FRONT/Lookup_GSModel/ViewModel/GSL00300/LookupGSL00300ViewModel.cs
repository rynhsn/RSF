using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00300ViewModel : R_ViewModel<GSL00300DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00300DTO> CurrencyGrid = new ObservableCollection<GSL00300DTO>();

        public async Task GetCurrencyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00300GetCurrencyListAsync();

                CurrencyGrid = new ObservableCollection<GSL00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL00300DTO> GetCurrency(GSL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00300DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00300GetCurrencyAsync(poEntity);
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
