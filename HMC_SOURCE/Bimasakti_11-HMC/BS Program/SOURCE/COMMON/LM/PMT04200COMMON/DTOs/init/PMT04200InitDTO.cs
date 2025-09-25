using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common.DTOs
{
    public class PMT04200InitDTO
    {
        public PMT04200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
        public PMT04200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public PMT04200PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; }
        public PMT04200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; }
        public PMT04200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public PMT04200GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; }
        public PMT04200TodayDateDTO VAR_TODAY { get; set; }
        
        public List<PropertyListDTO> VAR_GS_PROPERTY_LIST { get; set; }
        
        public List<PMT04200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
    }
    
}

