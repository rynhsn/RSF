using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02510ViewModel : R_ViewModel<GSL02510DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL02510DTO> CashBankGrid = new ObservableCollection<GSL02510DTO>();

        public async Task GetCashBankList(GSL02510ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02510GetCashBankListAsync(poParameter);

                CashBankGrid = new ObservableCollection<GSL02510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02510DTO> GetCashBank(GSL02510ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL02510DTO loRtn = null; 
            try
            {
                var loResult = await _modelRecord.GSL02510GetCashBankAsync(poParameter);
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
