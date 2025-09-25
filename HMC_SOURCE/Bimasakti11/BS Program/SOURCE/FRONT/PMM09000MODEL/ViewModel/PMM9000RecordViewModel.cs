using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.UtiliyDTO;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000MODEL.ViewModel
{
    public class PMM9000RecordViewModel
    {
        private PMM09000RecordModel _GetRecordModel = new PMM09000RecordModel();


        public async Task<PMM09000EntryHeaderDTO> GetAmortizationDetail(PMM09000DbParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            PMM09000EntryHeaderDTO ReturnAmortizationDetail = new PMM09000EntryHeaderDTO();
            try
            {
                ReturnAmortizationDetail = await _GetRecordModel.GetAmortizationDetailAsyncModelAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return ReturnAmortizationDetail;
        }
    }
}
