using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100MODEL.FrontDTOs.PMT02100
{
    public class ScheduleProcessHeaderDTO
    {
        public string CSCHEDULED_DATE { get; set; }
        public DateTime? DSCHEDULED_DATE { get; set; } = DateTime.Today.AddDays(1);
        public string CSCHEDULED_TIME { get; set; }
        //public string CSCHEDULED_TIME_HOURS { get; set; }
        //public string CSCHEDULED_TIME_HOURS { get; set; }
        //public int ISCHEDULED_TIME_HOURS { get; set; } = 9;
        //public string CSCHEDULED_TIME_MINUTES { get; set; }
        //public int ISCHEDULED_TIME_MINUTES { get; set; } = 0;
    }
}
