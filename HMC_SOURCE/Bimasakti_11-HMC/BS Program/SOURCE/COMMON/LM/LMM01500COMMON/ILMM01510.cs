using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public interface ILMM01510 : R_IServiceCRUDBase<LMM01511DTO>
    {
        IAsyncEnumerable<LMM01510DTO> LMM01510TemplateAndBankAccountList();
    }

}
