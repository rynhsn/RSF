using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public interface ILMM01500 : R_IServiceCRUDBase<LMM01500DTO>
    {
        IAsyncEnumerable<LMM01500DTOPropety> GetProperty();

        IAsyncEnumerable<LMM01501DTO> GetInvoiceGrpList();

        LMM01500DTO LMM01500ActiveInactive(LMM01500DTO poParam);

        IAsyncEnumerable<LMM01502DTO> LMM01500LookupBank();
    }

}
