using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100COMMON
{
    public class GLT00100UniversalDTO
    {
        public GLT00100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public List<GLT00100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
        public GLT00100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
        public GLT00100GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; }
        public GLT00100GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; }
    }
}
