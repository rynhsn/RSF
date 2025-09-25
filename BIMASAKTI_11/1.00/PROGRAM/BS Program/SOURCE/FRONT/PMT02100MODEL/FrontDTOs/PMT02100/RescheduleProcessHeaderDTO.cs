using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100MODEL.FrontDTOs.PMT02100
{
    public class RescheduleProcessHeaderDTO
    {
        public string CRESCHEDULED_DATE { get; set; }
        public DateTime? DRESCHEDULED_DATE { get; set; }
        public string CRESCHEDULED_TIME_HOURS { get; set; }
        public string CRESCHEDULED_TIME_MINUTES { get; set; }
        public int IRESCHEDULED_TIME_HOURS { get; set; }
        public int IRESCHEDULED_TIME_MINUTES { get; set; }
    }
}
