using System;
using System.Text;

namespace PMB04000COMMONPrintBatch
{
    public class PMB04000DataReportDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CLANG_ID { get; set; }
        public string? CREF_NO { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CNOMINAL { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CINVOICE_NO { get; set; }
        public string? CINVOICE_DATE { get; set; }
        public DateTime? DINVOICE_DATE { get; set; }
        public string? CTRANS_DESC { get; set; }
        public decimal NINV_AMOUNT { get; set; }
        public string? CCURRENCY_SYMBOL { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string? CCITY { get; set; }
        public string? CTODAY_DATE { get; set; }
        public DateTime? DTODAY_DATE { get; set; }
        public string? CPERSON { get; set; }
        public string? CPOSITION { get; set; }
        public string? CSIGN_ID01 { get; set; } = "";
        public string? CSIGN_ID02 { get; set; } = "";
        public string? CSIGN_ID03 { get; set; } = "";
        public string? CSIGN_ID04 { get; set; } = "";
        public string? CSIGN_ID05 { get; set; } = "";
        public string? CSIGN_ID06 { get; set; } = "";
        public string? CNAME01 { get; set; }
        public string? CNAME02 { get; set; }
        public string? CNAME03 { get; set; }
        public string? CNAME04 { get; set; }
        public string? CNAME05 { get; set; }
        public string? CNAME06 { get; set; }
        public string? CPOSITION01 { get; set; }
        public string? CPOSITION02 { get; set; }
        public string? CPOSITION03 { get; set; }
        public string? CPOSITION04 { get; set; }
        public string? CPOSITION05 { get; set; }
        public string? CPOSITION06 { get; set; }
        public byte[]? OSIGN01 { get; set; }
        public byte[]? OSIGN02 { get; set; }
        public byte[]? OSIGN03 { get; set; }
        public byte[]? OSIGN04 { get; set; }
        public byte[]? OSIGN05 { get; set; }
        public byte[]? OSIGN06 { get; set; }
    }
}
