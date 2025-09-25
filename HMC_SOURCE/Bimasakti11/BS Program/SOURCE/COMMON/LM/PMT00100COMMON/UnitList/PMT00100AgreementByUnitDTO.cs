using PMT00100COMMON.UtilityDTO;
using System;

namespace PMT00100COMMON.UnitList
{
    public class PMT00100AgreementByUnitDTO : BaseDTO
    {
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public decimal NACTUAL_PRICE { get; set; }
        public decimal NBOOKING_FEE { get; set; }
        public bool LPAID { get; set; }
        public string? CBOOK_STATUS { get; set; }
        public string? CBOOK_STATUS_DESCR { get; set; }
        public string? CTRANS_STATUS { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CBILLING_RULE_TYPE { get; set; }
        public string? CBILLING_RULE_CODE { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? TYPE { get => CTRANS_NAME; }
    }
}
