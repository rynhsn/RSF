using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PMT05000COMMON.DTO_s
{
    public class GSPeriodDT_DTO
    {
        public string CYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
    }

    public class GSTPeriodDT_Display : GSPeriodDT_DTO
    {
        public DateTime DSTART_DATE { get; set; }= DateTime.Now;
        public DateTime DEND_DATE { get; set; } = DateTime.Now;
    }

    public class GSTPeriodDT_Param : GSPeriodDT_DTO
    {
        public string CCOMPANY_ID { get; set; }
    }
}
