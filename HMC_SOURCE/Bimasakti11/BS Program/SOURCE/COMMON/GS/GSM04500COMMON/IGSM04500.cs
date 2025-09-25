using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04500Common
{
    public interface IGSM04500 : R_IServiceCRUDBase<GSM04500DTO>
    {
        IAsyncEnumerable<GSM04500DTO> GET_JOURNAL_GRP_LIST_STREAM();
        GSM04500JournalGroupTypeListDTO GetAllJournalGroupTypeList();
    }

}
