using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01000HeaderPrintDTO
    {
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public bool LACCRUAL { get; set; }
        public string CUTILITY_JRNGRP_CODE { get; set; }
        public string CUTILITY_JRNGRP_NAME { get; set; }

        public decimal NTAX_EXEMPTION_PCT{ get; set; }
        public bool LTAX_EXEMPTION { get; set; }
        public bool LOTHER_TAX { get; set; }
        public bool LWITHHOLDING_TAX { get; set; }
        public string CTAX_EXEMPTION_CODE { get; set; }
        public string COTHER_TAX_ID { get; set; }
        public string CWITHHOLDING_TAX_TYPE { get; set; }
        public string CWITHHOLDING_TAX_ID { get; set; }


        public List<PMM01000DetailPrintDTO> DetailCharges { get; set; }
    }
}
