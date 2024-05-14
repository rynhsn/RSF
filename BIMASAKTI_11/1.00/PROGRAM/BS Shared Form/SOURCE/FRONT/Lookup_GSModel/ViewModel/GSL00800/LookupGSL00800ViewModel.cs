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
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00800DTO> CurrencyRateTypeGrid = new ObservableCollection<GSL00800DTO>();

        public async Task GetCurrencyRateTypeList()
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
        public async Task<GSL00800DTO> GetCurrencyRateType(GSL00800ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00800DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00800GetCurrencyTypeAsync(poEntity);
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
