using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50600
{
    public class PMT50600DTO
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPARTMENT_CODE { get; set; } = "";
        public string CDEPARTMENT_NAME { get; set; } = "";
        public string CTENANT_OPTIONS { get; set; } = "A";
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public bool LONETIME { get; set; } = false;
        public int IPERIOD_FROM_YEAR { get; set; } = DateTime.Now.Year;
        public string CPERIOD_FROM_MONTH { get; set; } = DateTime.Now.ToString("MM");
        public int IPERIOD_TO_YEAR { get;set; } = DateTime.Now.Year;
        public string CPERIOD_TO_MONTH { get; set; } = DateTime.Now.ToString("MM");
    }
}
