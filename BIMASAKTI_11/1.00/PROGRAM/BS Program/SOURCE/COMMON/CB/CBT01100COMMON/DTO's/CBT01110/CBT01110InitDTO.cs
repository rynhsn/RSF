using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01100COMMON
{
    public class CBT01110InitDTO
    {
        public CBT01100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public CBT01100TodayDateDTO VAR_TODAY { get; set; }
        public CBT01100GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
        public CBT01100GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; }
        public CBT01100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public List<CBT01100GSCurrencyDTO> VAR_CURRENCY_LIST { get; set; }
        public List<CBT01100GSCenterDTO> VAR_CENTER_LIST { get; set; }  
        public List<CBT01100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
    }
}
