using System;
using System.Collections.Generic;
using System.Text;
using R_ReportFastReportBack;


namespace PMM00500Common.DTOs
{
    public class PrintParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGES_TYPE_FROM { get; set; }
        public string CCHARGES_TYPE_TO { get; set; }
        public string CSHORT_BY { get; set; }
        public bool LPRINT_DRAFT { get; set; }
        public bool LPRINT_INACTIVE { get; set; }
        public bool LPRINT_DETAIL_ACC { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
        public string CPRINT_EXT_TYPE { get; set; }

        public string CFILENAME { get; set; }
    }
}
