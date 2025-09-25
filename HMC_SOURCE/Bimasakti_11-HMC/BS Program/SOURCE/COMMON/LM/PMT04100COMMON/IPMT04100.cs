using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT04100COMMON
{
    public interface IPMT04100
    {
        IAsyncEnumerable<PMT04100DTO> GetJournalList();
        PMT04100SingleResult<PMT04100DTO> GetJournalRecord(PMT04100DTO poEntity);
        PMT04100SingleResult<PMT04100DTO> SaveJournalRecord(PMT04100SaveParamDTO poEntity);
        PMT04100SingleResult<PMT04100UpdateStatusDTO> UpdateJournalStatus(PMT04100UpdateStatusDTO poEntity);
        PMT04100SingleResult<PMT04100UpdateStatusDTO> SubmitCashReceipt(PMT04100UpdateStatusDTO poEntity);
    }
}
