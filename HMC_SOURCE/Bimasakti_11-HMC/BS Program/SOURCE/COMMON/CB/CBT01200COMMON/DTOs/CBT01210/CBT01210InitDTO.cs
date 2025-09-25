using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01210InitDTO
    {
        public CBT01200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public CBT01200TodayDateDTO VAR_TODAY { get; set; }
        public CBT01200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
        public CBT01200GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; }
        public CBT01200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public List<CBT01200GSCurrencyDTO> VAR_CURRENCY_LIST { get; set; }
        public List<CBT01200GSCenterDTO> VAR_CENTER_LIST { get; set; }
        public List<CBT01200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
    }
}
