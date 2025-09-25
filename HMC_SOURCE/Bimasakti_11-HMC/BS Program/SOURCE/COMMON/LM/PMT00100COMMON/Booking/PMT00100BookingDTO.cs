using PMT00100COMMON.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT00100COMMON.Booking
{
    public class PMT00100BookingDTO : BaseDTO
    {
        public string? CTRANS_CODE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public string? CUNIT_CATEGORY_LIST { get; set; }
        public string? CUNIT_CATEGORY_ID { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CBILLING_RULE_CODE { get; set; } 
        public string? CBILLING_RULE_NAME { get; set; } 
        public string? CSTRATA_TAX_ID { get; set; }
        public string? CSTRATA_TAX_NAME { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public decimal NACTUAL_PRICE { get; set; }
        public decimal NSELL_PRICE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; }
        public decimal NBOOKING_FEE { get; set; }
        public string? CHO_PLAN_DATE { get; set; }
        public DateTime? DHO_PLAN_DATE { get; set; }
        public bool LWITH_FO { get; set; }
        public string? CNOTES { get; set; }
        //get from another process and just send to sp
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CLEASE_MODE { get; set; }
        public string? CCHARGE_MODE { get; set; }
        public string? CFOLLOW_UP_DATE { get; set; }
        public string? CTC_CODE { get; set; }
        public string? CEXPIRED_DATE { get; set; }
        public string? CLINK_TRANS_CODE { get; set; }
        public string? CLINK_REF_NO { get; set; }
        public string? CSTART_TIME { get; set; }
        public string? CLINKCEND_TIME_REF_NO { get; set; }
        public int IDAYS { get; set; }
        public int IMONTHS { get; set; }
        public int IYEARS { get; set; }
        public int IHOURS { get; set; }
        public string? CEND_TIME { get; set; }
        public string? CCALLER_ACTION { get; set; }
        public string? CBILLING_RULE_TYPE { get; set; }
        public string? CTRANS_STATUS { get; set; }
        public string? CMODE_CRUD { get; set; }
    }
}
