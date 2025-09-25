using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public class PMT01330ActiveInactiveDTO
    {
        //Parameter
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CSEQ_NO { get; set; }
        public bool LACTIVE { get; set; }
    }
}
