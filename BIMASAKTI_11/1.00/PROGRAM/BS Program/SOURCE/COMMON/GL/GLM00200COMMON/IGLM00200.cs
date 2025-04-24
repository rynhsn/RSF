using GLM00200COMMON;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GLM00200COMMON
{
    public interface IGLM00200 
    {
        RecordResultDTO<AllInitRecordDTO> GetInitData();
        RecordResultDTO<JournalDTO> GetJournalData(JournalDTO poEntity);
        RecordResultDTO<JournalDTO> SaveJournalData(ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO> poEntity);
        RecordResultDTO<GLM00200UpdateStatusDTO> UpdateStatusJournalData(GLM00200UpdateStatusDTO poEntity);
        RecordResultDTO<CurrencyRateResultDTO> GetLastCurrency(CurrencyRateResultDTO poEntity);

        IAsyncEnumerable<JournalDTO> GetAllRecurringList();
        IAsyncEnumerable<JournalDetailGridDTO> GetAllJournalDetailList();
        IAsyncEnumerable<JournalDetailActualGridDTO> GetAllActualJournalDetailList();

        UploadByte DownloadTemplate();
        //IAsyncEnumerable<JournalGridDTO> GetFilteredRecurringList();
    }
}
