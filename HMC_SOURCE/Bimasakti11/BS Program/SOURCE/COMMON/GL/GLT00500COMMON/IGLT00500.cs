using GLT00500COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON
{
    public interface IGLT00500
    {
        IAsyncEnumerable<ImportAdjustmentJournalErrorDTO> GetImportAdjustmentJournalErrorList();
        IAsyncEnumerable<GetImportAdjustmentJournalResult> GetSuccessProcessList();
        InitialProcessDTO InitialProcess();
        TemplateImportAdjustmentJournalDTO DownloadTemplateImportAdjustmentJournal();
        //ImportAdjustmentJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter);
    }
}