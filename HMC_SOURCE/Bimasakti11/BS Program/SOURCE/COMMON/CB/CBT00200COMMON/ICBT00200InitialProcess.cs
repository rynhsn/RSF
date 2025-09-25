using System;
using System.Collections.Generic;

namespace CBT00200COMMON
{
    public interface ICBT00200InitialProcess
    {
        #region Universal
        CBT00200SingleResult<CBT00200GSCompanyInfoDTO> GetGSCompanyInfo();
        CBT00200SingleResult<CBT00200GLSystemParamDTO> GetGLSystemParam();
        CBT00200SingleResult<CBT00200CBSystemParamDTO> GetCBSystemParam();
        CBT00200SingleResult<CBT00200GSTransInfoDTO> GetGSTransCodeInfo();
        CBT00200SingleResult<CBT00200GSPeriodYearRangeDTO> GetGSPeriodYearRange();
        CBT00200SingleResult<CBT00200TodayDateDTO> GetTodayDate();
        CBT00200SingleResult<CBT00200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT00200ParamGSPeriodDTInfoDTO poEntity);
        CBT00200SingleResult<CBT00200LastCurrencyRateDTO> GetLastCurrencyRate(CBT00200LastCurrencyRateDTO poEntity);
        IAsyncEnumerable<CBT00200GSGSBCodeDTO> GetGSBCodeList();
        IAsyncEnumerable<CBT00200GSCenterDTO> GetCenterList();
        #endregion

        CBT00200SingleResult<CBT00200JournalListInitialProcessDTO> GetTabJournalListInitialProcess();
        CBT00200SingleResult<CBT00210JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess();
    }
}
