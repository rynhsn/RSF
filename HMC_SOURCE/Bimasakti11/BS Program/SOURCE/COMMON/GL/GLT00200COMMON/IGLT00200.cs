using GLT00200COMMON.DTOs.GLT00200;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON
{
    public interface IGLT00200
    {
        IAsyncEnumerable<ImportJournalErrorDTO> GetImportJournalErrorList();
        IAsyncEnumerable<GetImportJournalResult> GetSuccessProcessList();
        InitialProcessDTO InitialProcess();
        TemplateImportJournalDTO DownloadTemplateImportJournal();
        //ImportJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter);
    }
}
