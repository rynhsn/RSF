using System;
using System.Collections.Generic;
using System.Text;

namespace CBT00200COMMON
{
    public class CBT00200JournalListInitialProcessDTO
    {
        public CBT00200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public CBT00200GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; }
        public CBT00200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; }
        public CBT00200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
    }
}
