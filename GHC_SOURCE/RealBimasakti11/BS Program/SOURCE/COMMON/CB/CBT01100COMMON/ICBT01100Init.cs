using System;
using System.Collections.Generic;

namespace CBT01100COMMON
{
    public interface ICBT01100Init
    {
        #region Universal
        CBT01100RecordResult<CBT01100GSCompanyInfoDTO> GetGSCompanyInfo();

        CBT01100RecordResult<CBT01100GLSystemParamDTO> GetGLSystemParam();
        CBT01100RecordResult<CBT01100CBSystemParamDTO> GetCBSystemParam();
        CBT01100RecordResult<CBT01100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT01100ParamGSPeriodDTInfoDTO poEntity);
        CBT01100RecordResult<CBT01100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo();
        CBT01100RecordResult<CBT01100GSTransInfoDTO> GetGSTransCodeInfo();
        CBT01100RecordResult<CBT01100GSPeriodYearRangeDTO> GetGSPeriodYearRange();
        CBT01100RecordResult<CBT01100TodayDateDTO> GetTodayDate();
        IAsyncEnumerable<CBT01100GSGSBCodeDTO> GetGSBCodeList();
        IAsyncEnumerable<CBT01100GSCurrencyDTO> GetCurrencyList();
        IAsyncEnumerable<CBT01100GSCenterDTO> GetCenterList();
        #endregion

        CBT01100RecordResult<CBT01100InitDTO> GetTabJournalListInitVar();
        CBT01100RecordResult<CBT01110InitDTO> GetTabJournalEntryInitVar();
    }
}
