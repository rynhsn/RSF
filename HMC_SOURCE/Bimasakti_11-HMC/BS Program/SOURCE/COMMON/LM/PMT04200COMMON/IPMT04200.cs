using PMT04200Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using R_CommonFrontBackAPI;

namespace PMT04200Common
{
    public interface IPMT04200 
    {
        IAsyncEnumerable<PMT04200DTO> GetJournalList();
        IAsyncEnumerable<PMT04200AllocationGridDTO> GetAllocationList();
        PMT04200RecordResult<PMT04200DTO>  GetJournalRecord(PMT04200DTO poEntity);

        PMT04200RecordResult<PMT04200DTO> SaveJournalRecord(PMT04200SaveParamDTO poEntity);
        PMT04200RecordResult<PMT04200UpdateStatusDTO> UpdateJournalStatus(PMT04200UpdateStatusDTO poEntity);
        PMT04200RecordResult<PMT04200UpdateStatusDTO> SubmitCashReceipt(PMT04200UpdateStatusDTO poEntity);
    }
}
