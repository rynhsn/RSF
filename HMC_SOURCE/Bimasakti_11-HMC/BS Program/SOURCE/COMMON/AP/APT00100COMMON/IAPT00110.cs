using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs.APT00110;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON
{
    public interface IAPT00110 : R_IServiceCRUDBase<APT00110ParameterDTO>
    {
        IAsyncEnumerable<GetPaymentTermListDTO> GetPaymentTermList();
        IAsyncEnumerable<GetCurrencyListDTO> GetCurrencyList();
        GetCurrencyOrTaxRateResultDTO GetCurrencyOrTaxRate(GetCurrencyOrTaxRateParameterDTO poParam);
        SubmitJournalResultDTO SubmitJournalProcess(SubmitJournalParameterDTO poParam);
        RedraftJournalResultDTO RedraftJournalProcess(RedraftJournalParameterDTO poParam);
    }
}
