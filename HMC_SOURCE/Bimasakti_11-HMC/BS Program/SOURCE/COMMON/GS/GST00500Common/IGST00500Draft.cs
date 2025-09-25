using System;
using System.Collections.Generic;
using System.Text;
using R_CommonFrontBackAPI;

namespace GST00500Common
{
    public interface IGST00500Draft: R_IServiceCRUDBase<GST00500DTO>
    {
        IAsyncEnumerable<GST00500DTO> ApprovalDraftListStream();
    }
}
