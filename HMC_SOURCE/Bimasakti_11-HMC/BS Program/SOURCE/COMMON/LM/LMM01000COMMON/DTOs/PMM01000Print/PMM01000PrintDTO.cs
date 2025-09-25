using R_APICommonDTO;

namespace PMM01000COMMON
{
    public class PMM01000PrintDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCR { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public bool LACCRUAL { get; set; }
        public string CUTILITY_JRNGRP_CODE { get; set; }
        public string CUTILITY_JRNGRP_NAME { get; set; }
        public bool LTAX_EXEMPTION { get; set; }
        public string CTAX_EXEMPTION_CODE { get; set; }
        public decimal NTAX_EXEMPTION_PCT{ get; set; }
        public bool LOTHER_TAX { get; set; }
        public string COTHER_TAX_ID { get; set; }
        public bool LWITHHOLDING_TAX { get; set; }
        public string CWITHHOLDING_TAX_TYPE { get; set; }
        public string CWITHHOLDING_TAX_ID { get; set; }
        public string CGOA_CODE { get; set; }
        public string CGOA_NAME { get; set; }
        public bool LDEPARTMENT_MODE { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }

        public byte[] CLOGO { get; set; }
    }
}
