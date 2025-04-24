using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.Print.Distribute;
using System;
using System.Text;

namespace PMB04000COMMON.DTO.Utilities
{
    public class PMB04000ParamDTO : PMB04000BaseDTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public string? CDEPT_NAME { get; set; }
        public string CTENANT_ID { get; set; } = "";
        public string? CTENANT_NAME { get; set; }
        public bool LALL_TENANT { get; set; } = true;
        public string? CINVOICE_TYPE { get; set; }
        public string? CPERIOD { get; set; }
        public string CTRANS_CODE { get; set; } = "";
        public int IPERIOD_YEAR { get; set; } = DateTime.Now.Year;
        public string? CPERIOD_MONTH { get; set; } = DateTime.Now.Month.ToString("D2");
        public bool LALL_PERIOD { get; set; }
        public string? CTYPE_PROCESS { get; set; }
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }

    }
}
