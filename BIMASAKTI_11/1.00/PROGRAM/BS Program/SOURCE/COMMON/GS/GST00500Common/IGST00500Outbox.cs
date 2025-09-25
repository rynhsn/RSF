using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GST00500Common
{
    public interface IGST00500Outbox : R_IServiceCRUDBase<GST00500DTO>
    {
        IAsyncEnumerable<GST00500DTO> ApprovalOutboxListStream();
        List<GST00500ApprovalStatusDTO> GetApprovalStatusList();
    }
}
