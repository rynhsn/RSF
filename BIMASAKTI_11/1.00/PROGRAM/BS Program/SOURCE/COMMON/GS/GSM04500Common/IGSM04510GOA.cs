using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04500Common
{
    public interface IGSM04510GOA : R_IServiceCRUDBase<GSM04510GOADTO>
    {
        IAsyncEnumerable<GSM04510GOADTO> JOURNAL_GRP_GOA_LIST();
    }

}
