using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02100ViewModel : R_ViewModel<GSL02100DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL02100DTO> PaymentTermGrid = new ObservableCollection<GSL02100DTO>();

        public async Task GetPaymentTermList(GSL02100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02100GetPaymentTermListAsync(poParameter);

                PaymentTermGrid = new ObservableCollection<GSL02100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02100DTO> GetPaymentTerm(GSL02100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL02100DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL02100GetPaymentTermAsync(poParameter);
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
