using System;

namespace PMT01300COMMON
{
    public class PMT01300HeaderPrintDTO
    {
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public DateTime? DDOC_DATE { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CADDRESS { get; set; }
        public string CINVGRP_NAME { get; set; }
        public string CLINK_REF_NO { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CPAY_TERM_NAME { get; set; }
        public string CPHONE1 { get; set; }
        public string CEMAIL { get; set; }
    }
}
