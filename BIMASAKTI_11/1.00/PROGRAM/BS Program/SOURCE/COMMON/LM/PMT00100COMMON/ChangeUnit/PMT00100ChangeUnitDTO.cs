using PMT00100COMMON.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT00100COMMON.ChangeUnit
{
    public class PMT00100ChangeUnitDTO : BaseDTO
    {
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NACTUAL_PRICE { get; set; }
        public decimal NSELL_PRICE { get; set; }
        public decimal NBOOKING_FEE { get; set; }
        public string? CBILLING_RULE_CODE { get; set; }
        public string? CBILLING_RULE_TYPE { get; set; }
        public string? CBILLING_RULE_NAME { get; set; }       

    }
}
