using Lookup_TXCOMMON.DTOs.TXL00100;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Lookup_TXModel.ViewModel.TXL00100
{
    public class LookupTXL00100ViewModel
    {
        private PublicLookupTXModel _model = new PublicLookupTXModel();
        private PublicLookupGetRecordTXModel _modelGetRecord = new PublicLookupGetRecordTXModel();
        public ObservableCollection<TXL00100DTO> loList = new ObservableCollection<TXL00100DTO>();
        public bool lHasData = false;
        public async Task TXL00100BranchLookUp()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.TXL00100BranchLookUpAsync();
                loList = new ObservableCollection<TXL00100DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<TXL00100DTO> TXL00100BranchLookUp(TXL00100ParameterGetRecordDTO poParam)
        {
            var loEx = new R_Exception();
            TXL00100DTO? loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.TXL00100BranchLookUpAsync(poParam);
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
