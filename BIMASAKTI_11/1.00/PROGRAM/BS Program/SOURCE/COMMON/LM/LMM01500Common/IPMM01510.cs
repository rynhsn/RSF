using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01500COMMON
{
    public interface IPMM01510 : R_IServiceCRUDBase<PMM01511DTO>
    {
        IAsyncEnumerable<PMM01510DTO> PMM01510TemplateAndBankAccountList();
    }

}
