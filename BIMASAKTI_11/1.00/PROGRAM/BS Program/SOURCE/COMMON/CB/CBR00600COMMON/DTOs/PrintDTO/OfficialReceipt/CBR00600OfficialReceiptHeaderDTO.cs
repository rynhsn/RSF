using System;

namespace CBR00600COMMON
{
    public class CBR00600OfficialReceiptHeaderDTO
    {
        public string CREPORT_TITLE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CCUSTOMER_ID_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string CTRANS_AMOUNT { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CMESSAGE_DESC { get; set; }
        public string CMESSAGE_DESC_RTF { get; set; }
        public string CADDITIONAL_INFO { get; set; }
        public string CADDITIONAL_INFO_RTF { get; set; }
    }
}
