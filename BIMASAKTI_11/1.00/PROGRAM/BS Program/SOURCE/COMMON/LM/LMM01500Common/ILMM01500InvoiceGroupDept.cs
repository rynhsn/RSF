using System.Collections.Generic;
using LMM01500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMM01500Common
{
    public interface ILMM01500InvoiceGroupDept : R_IServiceCRUDBase<LMM01500InvGrpDeptDTO>
    {
        IAsyncEnumerable<LMM01500InvGrpDeptGridDTO> LMM01500GetInvoiceGroupDeptListStream();
    }
}