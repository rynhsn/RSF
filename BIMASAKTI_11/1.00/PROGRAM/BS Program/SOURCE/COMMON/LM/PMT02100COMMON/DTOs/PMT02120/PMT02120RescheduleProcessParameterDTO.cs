using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120
{
    public class PMT02120RescheduleProcessParameterDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CSCHEDULED_HO_DATE { get; set; }
        public string CSCHEDULED_HO_TIME { get; set; }
        public string CREASON { get; set; }
        public string CEMPLOYEE_ID { get; set; }
    }
}
