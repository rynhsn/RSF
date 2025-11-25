using System.Collections.Generic;
using PMT05500COMMON.DTO;
using R_CommonFrontBackAPI;

namespace PMT05500COMMON.Interface
{
    public interface ILMT05500Deposit : R_IServiceCRUDBase<LMT05500DepositInfoDTO>
    {
        LMT05500DepositHeaderDTO DepositHeader(LMT05500DBParameter poParam);
        IAsyncEnumerable<LMT05500DepositListDTO> DepositListStream();
        IAsyncEnumerable<LMT05500DepositDetailListDTO> DepositDetailListStream();
    }
}