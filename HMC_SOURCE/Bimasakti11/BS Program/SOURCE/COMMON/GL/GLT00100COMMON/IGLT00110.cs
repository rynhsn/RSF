using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GLT00100COMMON
{
    public interface IGLT00110 
    {
        GLT00100RecordResult<GLT00110LastCurrencyRateDTO> GetLastCurrency(GLT00110LastCurrencyRateDTO poEntity);
        GLT00100RecordResult<GLT00110DTO> GetJournalRecord(GLT00110DTO poEntity);
        GLT00100RecordResult<GLT00110DTO> SaveJournal(GLT00110HeaderDetailDTO poEntity);
    }
}
