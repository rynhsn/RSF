using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common.DTO_s
{
    public class ActiveInactiveParam : GeneralParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public bool LNEW_ACTIVE_STATUS { get; set; }
    }
}
