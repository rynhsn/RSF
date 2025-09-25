using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs.PMT50610;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON
{
    public interface IPMT50610 : R_IServiceCRUDBase<PMT50610ParameterDTO>
    {
        IAsyncEnumerable<GetPaymentTermListDTO> GetPaymentTermList();
        IAsyncEnumerable<GetCurrencyListDTO> GetCurrencyList();
        GetCurrencyOrTaxRateResultDTO GetCurrencyOrTaxRate(GetCurrencyOrTaxRateParameterDTO poParam);
        SubmitJournalResultDTO SubmitJournalProcess(SubmitJournalParameterDTO poParam);
        RedraftJournalResultDTO RedraftJournalProcess(RedraftJournalParameterDTO poParam);
    }
}
