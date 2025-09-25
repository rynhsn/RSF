using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02300COMMON
{
    public interface ICBT02300BankInCheque : R_IServiceCRUDBase<CBT02300ChequeInfoDTO>
    {
        IAsyncEnumerable<CBT02300BankInChequeDTO> BankInChequeListStream();
        CBT02300ChequeInfoDTO BankInChequeInfo(CBT02300DBParamDetailDTO paramDetailDTO);
    }
}
