using System;
using System.Collections.Generic;

namespace GLT00100COMMON
{
    public interface IGLT00100Universal
    {
        #region Universal
        GLT00100RecordResult<GLT00100GSCompanyInfoDTO> GetGSCompanyInfo();
        GLT00100RecordResult<GLT00100GLSystemParamDTO> GetGLSystemParam();
        GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(GLT00100ParamGSPeriodDTInfoDTO poEntity);
        GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo();
        GLT00100RecordResult<GLT00100GSTransInfoDTO> GetGSTransCodeInfo();
        GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO> GetGSPeriodYearRange();
        GLT00100RecordResult<GLT00100TodayDateDTO> GetTodayDate();
        IAsyncEnumerable<GLT00100GSGSBCodeDTO> GetGSBCodeList();
        IAsyncEnumerable<GLT00100GSCurrencyDTO> GetCurrencyList();
        IAsyncEnumerable<GLT00100GSCenterDTO> GetCenterList();
        #endregion

        GLT00100RecordResult<GLT00100UniversalDTO> GetTabJournalListUniversalVar();
        GLT00100RecordResult<GLT00110UniversalDTO> GetTabJournalEntryUniversalVar();
    }
}
