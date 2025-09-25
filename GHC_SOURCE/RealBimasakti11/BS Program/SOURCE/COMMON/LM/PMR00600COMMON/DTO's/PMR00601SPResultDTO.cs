using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.DTO_s
{
    public class PMR00601SPResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CINVOICE_NO { get; set; }
        public string CINVOICE_DATE { get; set; }
        public DateTime? DINVOICE_DATE { get; set; }
        public decimal NINVOICE_AMOUNT { get; set; }
        public decimal NINVOICE_TAX_AMOUNT { get; set; }
        public decimal NINVOICE_TOTAL_AMOUNT { get; set; }
        public string CAGREEMENT_NO { get; set; }
        public string CAGREEMENT_DATE { get; set; }
        public DateTime? DAGREEMENT_DATE { get; set; }
        public string CCHARGE_ID { get; set; }
        public string CCHARGE_NAME { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string COVERTIME_DATE { get; set; }
        public DateTime? DOVERTIME_DATE { get; set; }
        public string CTIME_IN { get; set; }
        public string CTIME_OUT { get; set; }
        public string CTENURE { get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public decimal NUNIT_ACTUAL_AMOUNT { get; set; }
        public string CNOTE { get; set; }
        public string CTRANS_STATUS_CODE { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
    }
}
