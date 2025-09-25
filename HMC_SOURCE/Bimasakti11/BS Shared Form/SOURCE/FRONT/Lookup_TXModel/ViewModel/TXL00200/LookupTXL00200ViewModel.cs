using Lookup_TXCOMMON.DTOs.TXL00200;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_TXModel.ViewModel.TXL00200
{
    public class LookupTXL00200ViewModel
    {
        private PublicLookupTXModel _model = new PublicLookupTXModel();
        private PublicLookupGetRecordTXModel _modelGetRecord = new PublicLookupGetRecordTXModel();
        public ObservableCollection<TXL00200DTO> loList = new ObservableCollection<TXL00200DTO>();
        public bool lHasData = false;
        public async Task TXL00200TaxNoLookUpAsync(TXL00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.TXL00200TaxNoLookUpAsync(poParameter);
                loList = new ObservableCollection<TXL00200DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<TXL00200DTO> TXL00200TaxNoLookUpAsync(TXL00200ParameterGetRecordDTO poParam)
        {
            var loEx = new R_Exception();
            TXL00200DTO? loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.TXL00200TaxNoLookUpAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
    }
}
