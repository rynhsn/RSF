using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04500Common
{
    public interface IGSM04510GOADept : R_IServiceCRUDBase<GSM04510GOADeptDTO>
    {
        IAsyncEnumerable<GSM04510GOADeptDTO> JOURNAL_GRP_GOA_DEPT_LIST();
    }

}
