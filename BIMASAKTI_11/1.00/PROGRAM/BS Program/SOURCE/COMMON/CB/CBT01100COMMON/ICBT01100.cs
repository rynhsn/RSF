using CBT01100COMMON.DTO_s.CBT01100;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace CBT01100COMMON
{
    public interface ICBT01100 : R_IServiceCRUDBase<CBT01100JournalHDParam>
    {
        IAsyncEnumerable<CBT01100DTO> GetJournalList();
        CBT01100RecordResult<CBT01100UpdateStatusDTO> UpdateJournalStatus(CBT01100UpdateStatusDTO poEntity);
        CBT01100RecordResult<CBT01110LastCurrencyRateDTO> GetLastCurrency(CBT01110LastCurrencyRateDTO poEntity);
    }
}
