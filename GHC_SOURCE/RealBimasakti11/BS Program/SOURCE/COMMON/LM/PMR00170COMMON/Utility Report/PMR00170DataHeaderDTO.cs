using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170DataHeaderDTO
    {
        public string? PROPERTY { get; set; } 
        public string? FROM_DEPARTMENT { get; set; }
        public string? TO_DEPARTMENT { get; set; }
        public string? FROM_SALESMEN { get; set; }
        public string? TO_SALESMEN { get; set; }
        public string? FROM_PERIOD { get; set; }
        public string? TO_PERIOD { get; set; }
        public string? CREPORT_NAME { get; set; }
        public string CDEPT_REPORT_DISPLAY { get; set; }
        public string CSALESMAN_REPORT_DISPLAY{ get; set; }
        public string CPERIOD_DISPLAY { get; set; }
    }
}
