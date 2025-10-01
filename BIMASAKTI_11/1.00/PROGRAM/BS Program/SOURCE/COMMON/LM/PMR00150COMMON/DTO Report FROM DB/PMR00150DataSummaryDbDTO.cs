using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150DataSummaryDbDTO
    {
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_NAME { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CTENURE { get; set; }
        public decimal NTOTAL_GROSS_AREA_SIZE { get; set; }
        public decimal NTOTAL_NET_AREA_SIZE { get; set; }
        public decimal NTOTAL_COMMON_AREA_SIZE { get; set; }
        public string? CAGREEMENT_STATUS_NAME { get; set; }
        public string? CTRANS_STATUS_NAME { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
        public string? CTAX { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTC_MESSAGE { get; set; }

        //ToGet Logo
        public byte[]? CLOGO { get; set; }
        public string? CCOMPANY_NAME { get; set; }
        public string? CDATETIME_NOW { get; set; }
        public string? CCOMPANY_ID { get; set; }

        public string? CSTORAGE_ID { get; set; } = "";
    }
}
