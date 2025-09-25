using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01200InitDTO
    {
        public CBT01200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public List<CBT01200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
        public CBT01200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public CBT01200GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; }
        public CBT01200GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; }
    }
}
