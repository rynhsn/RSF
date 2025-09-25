using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04100COMMON
{
    public class PMT04110JournalEntryInitialProcessDTO
    {
        public PMT04100CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; }
        public PMT04100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public PMT04100TodayDateDTO VAR_TODAY { get; set; }
        public PMT04100GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
        public PMT04100PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; }
        public PMT04100GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; }
        public List<PMT04100PropertyDTO> VAR_PROPERTY_LIST { get; set; }
    }
}
