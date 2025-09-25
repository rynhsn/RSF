using R_APICommonDTO;
using System;

namespace PMM01000COMMON
{
    public class PMM01000DTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCR { get; set; }

        // result

        public string CCHARGES_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public bool LACTIVE { get; set; } = true;
        public bool LACCRUAL { get; set; }
        public bool LTAXABLE { get; set; }
        public bool LTAX_EXEMPTION { get; set; }
        public string CTAX_EXEMPTION_CODE { get; set; } = "";
        public decimal NTAX_EXEMPTION_PCT{ get; set; }
        public bool LOTHER_TAX { get; set; }
        public string COTHER_TAX_ID { get; set; } = "";
        public string CTAX_OTHER_NAME { get; set; } = "";
        public decimal NTAX_PERCENTAGE_OTHER { get; set; }
        public bool LWITHHOLDING_TAX { get; set; }
        public string CWITHHOLDING_TAX_NAME { get; set; } = "";
        public string CWITHHOLDING_TAX_TYPE { get; set; } = "";
        public string CWITHHOLDING_TAX_ID { get; set; } = "";
        public decimal NTAX_PERCENTAGE_WITHHOLDING { get; set; }
        public string CUTILITY_JRNGRP_CODE { get; set; } = "";
        public string CUTILITY_JRNGRP_NAME { get; set; } = "";
        public string CACCRUAL_METHOD { get; set; } = "";
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
