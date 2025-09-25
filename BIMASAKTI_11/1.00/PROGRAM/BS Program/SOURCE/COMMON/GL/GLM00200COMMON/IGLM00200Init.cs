using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200COMMON
{
    public interface IGLM00200Init
    {
        IAsyncEnumerable<CurrencyDTO> GetListCurrency();
        
        IAsyncEnumerable<StatusDTO> GetListStatus();

        RecordResultDTO<AllInitRecordDTO> GetAllInitRecord();

    
        RecordResultDTO<CurrencyRateResultDTO> GetCurrencyRateRecord(CurrencyRateParamDTO poParam);
    }
}
