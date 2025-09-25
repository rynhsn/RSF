using System;
using System.Collections.Generic;

namespace PMT04100COMMON
{
    public interface IPMT04100InitialProcess
    {
        #region Universal
        PMT04100SingleResult<PMT04100GSCompanyInfoDTO> GetGSCompanyInfo();
        PMT04100SingleResult<PMT04100GLSystemParamDTO> GetGLSystemParam();
        PMT04100SingleResult<PMT04100CBSystemParamDTO> GetCBSystemParam();
        PMT04100SingleResult<PMT04100GSTransInfoDTO> GetGSTransCodeInfo();
        PMT04100SingleResult<PMT04100GSPeriodYearRangeDTO> GetGSPeriodYearRange();
        PMT04100SingleResult<PMT04100PMSystemParamDTO> GetPMSystemParam(PMTInitialParamDTO poEntity);
        PMT04100SingleResult<PMT04100TodayDateDTO> GetTodayDate();
        PMT04100SingleResult<PMT04100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(PMT04100ParamGSPeriodDTInfoDTO poEntity);
        PMT04100SingleResult<PMT04100LastCurrencyRateDTO> GetLastCurrencyRate(PMT04100LastCurrencyRateDTO poEntity);
        IAsyncEnumerable<PMT04100GSGSBCodeDTO> GetGSBCodeList();
        IAsyncEnumerable<PMT04100PropertyDTO> GetPropertyList();
        #endregion

        PMT04100SingleResult<PMT04100JournalListInitialProcessDTO> GetTabJournalListInitialProcess();
        PMT04100SingleResult<PMT04110JournalEntryInitialProcessDTO> GetTabJournalEntryInitialProcess(PMTInitialParamDTO poEntity);
    }
}
