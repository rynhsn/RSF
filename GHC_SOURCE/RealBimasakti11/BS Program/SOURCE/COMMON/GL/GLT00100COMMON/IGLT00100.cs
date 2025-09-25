using System;
using System.Collections.Generic;

namespace GLT00100COMMON
{
    public interface IGLT00100
    {
        IAsyncEnumerable<GLT00100DTO> GetJournalList();
        IAsyncEnumerable<GLT00101DTO> GetJournalDetailList();
        GLT00100RecordResult<GLT00100UpdateStatusDTO> UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity);
        GLT00100RecordResult<GLT00100RapidApprovalValidationDTO> ValidationRapidApproval(GLT00100RapidApprovalValidationDTO poEntity);
    }
}
