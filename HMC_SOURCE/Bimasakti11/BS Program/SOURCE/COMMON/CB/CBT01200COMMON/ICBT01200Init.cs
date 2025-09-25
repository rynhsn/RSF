using CBT01200Common.DTOs;
using CBT01200Common.DTOs.CBT01210;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common

{
    public interface ICBT01200Init
    {
        #region Universal
        CBT01200RecordResult<CBT01200GSCompanyInfoDTO> GetGSCompanyInfo();
        CBT01200RecordResult<CBT01200GLSystemParamDTO> GetGLSystemParam();
        CBT01200RecordResult<CBT01200CBSystemParamDTO> GetCBSystemParam();
        CBT01200RecordResult<CBT01200GSPeriodDTInfoDTO> GetGSPeriodDTInfo(CBT01200ParamGSPeriodDTInfoDTO poEntity);
        CBT01200RecordResult<CBT01200GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo();
        CBT01200RecordResult<CBT01200GSTransInfoDTO> GetGSTransCodeInfo();
        CBT01200RecordResult<CBT01200GSPeriodYearRangeDTO> GetGSPeriodYearRange();
        CBT01200RecordResult<CBT01200TodayDateDTO> GetTodayDate();
        IAsyncEnumerable<CBT01200GSGSBCodeDTO> GetGSBCodeList();
        IAsyncEnumerable<CBT01200GSCurrencyDTO> GetCurrencyList();
        IAsyncEnumerable<CBT01200GSCenterDTO> GetCenterList();
        #endregion

        CBT01200RecordResult<CBT01200InitDTO> GetTabJournalListInitVar();
        CBT01200RecordResult<CBT01210InitDTO> GetTabJournalEntryInitVar();
    }
}
