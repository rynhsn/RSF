using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100Email
{
    public class PMT02100ResponseEmailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CEMAIL_FROM { get; set; }
        public string CTENANT_EMAIL { get; set; }
        public string CEMAIL_SUBJECT { get; set; }
        public string CEMAIL_BODY { get; set; }
        public string CSMTP_ID { get; set; }
        public string CUSER_ID { get; set; }
    }
}
