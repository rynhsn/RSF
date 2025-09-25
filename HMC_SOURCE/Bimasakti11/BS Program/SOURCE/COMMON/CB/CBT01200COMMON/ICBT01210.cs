using CBT01200Common.DTOs.CBT01210;
using CBT01200Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using R_CommonFrontBackAPI;

namespace CBT01200Common
{
    public interface ICBT01210  : R_IServiceCRUDBase<CBT01210ParamDTO>
    {
        IAsyncEnumerable<CBT01201DTO> GetJournalDetailList();
        
    }
}
