using System;
using System.Collections.Generic;
using System.Text;

namespace CBT00200COMMON
{
    public class CBT00210JournalEntryInitialProcessDTO
    {
        public CBT00200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; }
        public CBT00200TodayDateDTO VAR_TODAY { get; set; }
        public CBT00200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
        public List<CBT00200GSCenterDTO> VAR_CENTER_LIST { get; set; }
        public CBT00200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public CBT00200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public CBT00200GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; }
    }
}
