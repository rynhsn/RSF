using System.Collections.Generic;
using GSM04500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM04500Common
{
    public interface IGSM04500JournalGroup : R_IServiceCRUDBase<GSM04500JournalGroupDTO>
    {
        IAsyncEnumerable<GSM04500JournalGroupDTO> GSM04500GetAllJournalGroupListStream();
        
        GSM04500JournalGroupExcelDTO GSM04500DownloadTemplateFile();
        
        
    }
}