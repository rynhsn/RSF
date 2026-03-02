using System;

namespace FAT00700Common.Print
{
    public class FAT00700HeaderPrintDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPAYMENT_TYPE { get; set; }
        public string CPAYMENT_TYPE_DESC { get; set; }
        public string CSCHEDPAYMENT_DATE { get; set; }
        public DateTime? DSCHEDPAYMENT_DATE { get; set; }
    }
}
