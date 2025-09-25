using System;

namespace PMT00100COMMON.UnitList
{
    public class PMT00100AgreementDetailDTO : PMT00100AgreementByUnitDTO
    {
        public string? CBUILDING_NAME { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CSTRATA_TAX_ID { get; set; }
        public string? CSTRATA_TAX_NAME { get; set; }
        public string? CURRENCY_CODE { get; set; }
        public string? CBILLING_RULE_NAME { get; set; }
        public decimal NSELL_PRICE { get; set; }
        public string? CHO_PLAN_DATE { get; set; }
        public DateTime? DHO_PLAN_DATE { get; set; }
        public bool LWITH_FO { get; set; }
        public string? CNOTES { get; set; }
    }
}
