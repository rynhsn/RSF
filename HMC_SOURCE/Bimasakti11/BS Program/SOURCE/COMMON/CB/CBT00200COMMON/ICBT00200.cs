using System;
using System.Collections.Generic;

namespace CBT00200COMMON
{
    public interface ICBT00200
    {
        IAsyncEnumerable<CBT00200DTO> GetJournalList();
        CBT00200SingleResult<CBT00200DTO> GetJournalRecord(CBT00200DTO poEntity);
        CBT00200SingleResult<CBT00200DTO> SaveJournalRecord(CBT00200SaveParamDTO poEntity);
        CBT00200SingleResult<CBT00200UpdateStatusDTO> UpdateJournalStatus(CBT00200UpdateStatusDTO poEntity);
    }
}
