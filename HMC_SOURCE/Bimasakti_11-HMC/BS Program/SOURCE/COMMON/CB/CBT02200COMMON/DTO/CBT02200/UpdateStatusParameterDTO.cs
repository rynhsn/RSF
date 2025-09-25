using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class UpdateStatusParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CREC_ID_LIST { get; set; } = "";
        public string CNEW_STATUS { get; set; } = "";
    }
}
