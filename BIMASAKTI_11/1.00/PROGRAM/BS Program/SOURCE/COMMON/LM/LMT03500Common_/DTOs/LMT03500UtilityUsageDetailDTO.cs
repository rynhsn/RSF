using System;

namespace LMT03500Common.DTOs
{
    public class LMT03500UtilityUsageDetailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCRS { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSEQ_NO { get; set; }
        public string CINV_PRD { get; set; }
        public string CINV_YEAR { get; set; }
        public string CINV_MONTH { get; set; }
        public string CUTILITY_PRD { get; set; }
        public string CUTILITY_YEAR { get; set; }
        public string CUTILITY_MONTH { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public string CMETER_NO { get; set; }
        public int IBLOCK1_START { get; set; }
        public int IBLOCK1_END { get; set; }
        public int IBLOCK1_USAGE { get; set; }
        public int IBLOCK2_START { get; set; }
        public int IBLOCK2_END { get; set; }
        public int IBLOCK2_USAGE { get; set; }
        public int IBEBAN_BERSAMA { get; set; }
        public int IAVG_BLOCK1_USAGE { get; set; }
        public int IAVG_BLOCK2_USAGE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        
        public decimal NSTANDING_CHARGE { get; set; }
        public decimal NLWBP_CHARGE { get; set; }
        public decimal NWBP_CHARGE { get; set; }
        public decimal NTRANSFORMATOR_FEE { get; set; }
        public decimal NRETRIBUTION_PCT { get; set; } 
        public decimal NTTLB_PCT { get; set; }
        public decimal CADMIN_FEE { get; set; }
    }
}