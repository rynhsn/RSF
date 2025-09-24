using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170DataDetailDbDTO : PMR00170DataSummaryDbDTO
    {
        public string? CUNIT_DETAIL_ID { get; set; }
        public string? CUNIT_DETAIL_NAME { get; set; }
        public decimal NUNIT_DETAIL_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_COMMON_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_PRICE { get; set; }
        public decimal NUNIT_TOTAL_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_TOTAL_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_TOTAL_COMMON_AREA_SIZE { get; set; }
        public decimal NUNIT_TOTAL_PRICE { get; set; }
        public string? CCHARGE_DETAIL_TYPE_NAME { get; set; }
        public string? CCHARGE_DETAIL_UNIT_NAME { get; set; }
        public string? CCHARGE_DETAIL_CHARGE_NAME { get; set; }
        public string? CCHARGE_DETAIL_TAX_NAME { get; set; }
        public string? CCHARGE_DETAIL_START_DATE { get; set; }
        public string? CCHARGE_DETAIL_END_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_START_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_END_DATE { get; set; }
        public string? CCHARGE_DETAIL_TENURE { get; set; }
        public string? CCHARGE_DETAIL_FEE_METHOD { get; set; }
        public decimal NCHARGE_DETAIL_FEE_AMOUNT { get; set; }
        public decimal NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT { get; set; }
        public decimal NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT { get; set; }
        public string? CDEPOSIT_DETAIL_ID { get; set; }
        public string? CDEPOSIT_DETAIL_DATE { get; set; }
        public DateTime? DDEPOSIT_DETAIL_DATE { get; set; }
        public decimal NDEPOSIT_DETAIL_AMOUNT { get; set; }
        public string? CDEPOSIT_DETAIL_DESCRIPTION { get; set; }
    }
}
