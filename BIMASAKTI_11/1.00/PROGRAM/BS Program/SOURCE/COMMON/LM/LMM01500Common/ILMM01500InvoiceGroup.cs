using System.Collections.Generic;
using LMM01500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMM01500Common
{
    public interface ILMM01500InvoiceGroup : R_IServiceCRUDBase<LMM01500InvGrpDTO>
    {
        IAsyncEnumerable<LMM01500InvGrpGridDTO> LMM01500GetInvoiceGroupListStream();
    }
}