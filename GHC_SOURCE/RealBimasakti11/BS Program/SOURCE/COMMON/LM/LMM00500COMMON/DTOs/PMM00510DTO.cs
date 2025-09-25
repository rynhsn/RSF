using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00500Common.DTOs
{
    public class PMM00510DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGE_TYPE_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCR { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CACCRUAL_METHOD { get; set; }
        public bool LACCRUAL { get; set; }
        public bool LTAXABLE { get; set; }
        public bool LTAX_EXEMPTION { get; set; }
        public string CTAX_EXEMPTION_CODE { get; set; } = "";
        public decimal NTAX_EXEMPTION_PCT { get; set; }
        public bool LOTHER_TAX { get; set; }
        public string COTHER_TAX_ID { get; set; } = "";
        public bool LWITHHOLDING_TAX { get; set; }
        public string CWITHHOLDING_TAX_TYPE { get; set; } = "";
        public string CWITHHOLDING_TAX_ID { get; set; } = "";
        public string CSERVICE_JRNGRP_CODE { get; set; } = "";
        public string CSERVICE_JRNGRP_NAME { get; set; }
        public string CACTION { get; set; }
        public string CCULTURE { get; set; }

        public string CWITHHOLDING_TAX_NAME { get; set; }
        public decimal NWITHHOLDING_TAX_PCT { get; set; }

        public string COTHER_TAX_NAME { get; set; }
        public decimal NOTHER_TAX_PCT { get; set; }

        public string CNEW_CHARGES_ID { get; set; }
        public string CNEW_CHARGES_NAME { get; set; }
        public string CCURRENT_CHARGES_TYPE { get; set; }
        public string CCURRENT_CHARGE_ID { get; set; }

        public string CGOA_CODE { get; set; }
        public string CGOA_NAME { get; set; }
        public bool LDEPARTMENT_MODE { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCHARGES_TYPE_FROM { get; set; }
        public bool LPRINT_DETAIL_ACC { get; set; }
        public bool LPRINT_INACTIVE { get; set; }
        public bool LPRINT_DRAFT { get; set; }
        public string CSHORT_BY { get; set; }
        public string CCHARGES_TYPE_TO { get; set; }
    }

    public class LMM00510ListDTO : R_APIResultBaseDTO
    {
        public List<PMM00510DTO> Data { get; set; }
    }
}
