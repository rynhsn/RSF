using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01300ViewModel : R_ViewModel<GSL01300DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL01300DTO> BankAccountGrid = new ObservableCollection<GSL01300DTO>();

        public async Task GetBankAccountList(GSL01300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01300GetBankAccountListAsync(poParameter);

                BankAccountGrid = new ObservableCollection<GSL01300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL01300DTO> GetBankAccount(GSL01300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL01300DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL01300GetBankAccountAsync(poParameter);
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
