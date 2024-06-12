using System;

namespace PMI00500Common.DTOs
{
    public class PMI00500DTInvoiceDTO
    {
        public string CINVOICE_NO { get; set; } = "";
        public string CINVOICE_DESC { get; set; } = "";
        public DateTime DDUE_DATE { get; set; }
        public int ILATE_DAYS { get; set; }
        public decimal NPENALTY_AMOUNT { get; set; }
    }
}