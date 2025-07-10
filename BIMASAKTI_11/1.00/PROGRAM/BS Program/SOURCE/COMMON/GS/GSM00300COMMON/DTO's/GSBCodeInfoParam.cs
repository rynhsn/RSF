using GSM00300COMMON.DTO_s.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00300COMMON.DTO_s
{
    public class GSBCodeInfoParam : GeneralParamDTO
    {
        public string CLASS_APPLICATION { get; set; }
        public string CLASS_ID { get; set; }
        public string REC_ID_LIST { get; set; }
    }
}
