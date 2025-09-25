using PMT04200Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common

{
    public interface IPMT04200Init
    {
        #region Universal
        PMT04200RecordResult<PMT04200GSCompanyInfoDTO> GetGSCompanyInfo();
        PMT04200RecordResult<PMT04200GLSystemParamDTO> GetGLSystemParam();
        PMT04200RecordResult<PMT04200CBSystemParamDTO> GetCBSystemParam();
        PMT04200RecordResult<PMT04200PMSystemParamDTO> GetPMSystemParam();
        PMT04200RecordResult<PMT04200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(PMT04200ParamGSPeriodDTInfoDTO poEntity);
        PMT04200RecordResult<PMT04200GSTransInfoDTO> GetGSTransCodeInfo();
        PMT04200RecordResult<PMT04200GSPeriodYearRangeDTO> GetGSPeriodYearRange();
        PMT04200RecordResult<PMT04200TodayDateDTO> GetTodayDate();
        IAsyncEnumerable<PropertyListDTO> GetPropertyList();
        IAsyncEnumerable<PMT04200GSCurrencyDTO> GetCurrencyList();
        IAsyncEnumerable<PMT04200GSGSBCodeDTO> GetGSBCodeList();
        PMT04200RecordResult<PMT04200LastCurrencyRateDTO> GetLastCurrencyRate(PMT04200LastCurrencyRateDTO poEntity);

        #endregion

        PMT04200RecordResult<PMT04200InitDTO> GetTabTransactionListInitVar();
        
        PMT04200RecordResult<PMT04200JournalListInitialProcessDTO> GetTabJournalListInitialProcess();
        PMT04200RecordResult<PMT04210JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess();
    }
}
