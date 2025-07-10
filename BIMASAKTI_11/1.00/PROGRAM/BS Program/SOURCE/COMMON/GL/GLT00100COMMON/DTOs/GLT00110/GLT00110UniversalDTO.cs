using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100COMMON
{
    public class GLT00110UniversalDTO
    {
        public GLT00100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public GLT00100TodayDateDTO VAR_TODAY { get; set; }
        public GLT00100GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
        public GLT00100GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; }
        public GLT00100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public List<GLT00100GSCurrencyDTO> VAR_CURRENCY_LIST { get; set; }
        public List<GLT00100GSCenterDTO> VAR_CENTER_LIST { get; set; }  
        public List<GLT00100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
    }
}
