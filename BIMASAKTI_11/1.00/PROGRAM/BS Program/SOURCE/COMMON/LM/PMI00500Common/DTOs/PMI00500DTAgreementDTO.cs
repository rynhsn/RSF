using System;

namespace PMI00500Common.DTOs
{
    public class PMI00500DTAgreementDTO
    {
        public string CAGREEMENT_NO { get; set; } = "";
        public DateTime DSTART_DATE { get; set; }
        public DateTime DEND_DATE { get; set; }
        public string CUNIT { get; set; } = "";
        public string CBUILDING { get; set; } = "";
        public string CAGREEMENT_STATUS { get; set; } = "";
    }
}