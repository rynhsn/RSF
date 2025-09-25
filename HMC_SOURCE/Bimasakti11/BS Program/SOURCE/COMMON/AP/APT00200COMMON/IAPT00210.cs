using APT00200COMMON.DTOs.APT00200;
using APT00200COMMON.DTOs.APT00210;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON
{
    public interface IAPT00210 : R_IServiceCRUDBase<APT00210ParameterDTO>
    {
        IAsyncEnumerable<GetPaymentTermListDTO> GetPaymentTermList();
        IAsyncEnumerable<GetCurrencyListDTO> GetCurrencyList();
        GetCurrencyOrTaxRateResultDTO GetCurrencyOrTaxRate(GetCurrencyOrTaxRateParameterDTO poParam);
        SubmitJournalResultDTO SubmitJournalProcess(SubmitJournalParameterDTO poParam);
        RedraftJournalResultDTO RedraftJournalProcess(RedraftJournalParameterDTO poParam);
    }
}
