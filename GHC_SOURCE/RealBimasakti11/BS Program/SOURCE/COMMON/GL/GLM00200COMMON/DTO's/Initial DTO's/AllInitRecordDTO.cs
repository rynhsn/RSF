using GLM00200COMMON;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200COMMON
{
    public class AllInitRecordDTO 
    {
        public DateTime DTODAY { get; set; } 
        
        public CompanyDTO COMPANY_INFO { get; set; }
        
        public GLSysParamDTO GL_SYSTEM_PARAM { get; set; }

        public PeriodDTInfoDTO CURRENT_PERIOD_START_DATE { get; set; }
        
        public PeriodDTInfoDTO SOFT_PERIOD_START_DATE { get; set; }

        public IUndoCommitJrnDTO IUNDO_COMMIT_JRN { get; set; }
        
        public TransCodeDTO GSM_TRANSACTION_CODE { get; set; }
        
        public PeriodDTO PERIOD_YEAR { get; set; }
    }
}
