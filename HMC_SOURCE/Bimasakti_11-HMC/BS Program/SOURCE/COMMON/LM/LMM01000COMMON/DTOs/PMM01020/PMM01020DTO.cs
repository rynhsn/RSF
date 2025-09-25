using System;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01020DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCR { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_DATE { get; set; }
        public DateTime? DCHARGES_DATE { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // result
        public string CCHARGES_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NPIPE_SIZE { get; set; }
        public decimal NBUY_STANDING_CHARGE { get; set; }
        public string CUSAGE_RATE_MODE { get; set; }
        public string CUSAGE_RATE_MODE_DESCR { get; set; }
        public decimal NSTANDING_CHARGE { get; set; }
        public decimal NBUY_USAGE_CHARGE_RATE { get; set; }
        public decimal NUSAGE_CHARGE_RATE { get; set; }
        public bool LUSAGE_MIN_CHARGE { get; set; }
        public bool LSPLIT_ADMIN { get; set; }
        public decimal NUSAGE_MIN_CHARGE_AMT { get; set; }
        public decimal NMAINTENANCE_FEE { get; set; }
        public string CADMIN_FEE { get; set; }
        public string CADMIN_CHARGE_ID { get; set; }
        public string CADMIN_CHARGE_ID_DESCR { get; set; }
        public string CADMIN_FEE_DESCR { get; set; }
        public decimal NADMIN_FEE_PCT { get; set; }
        public decimal NADMIN_FEE_AMT { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }
        public bool LADMIN_FEE_SC { get; set; }
        public bool LADMIN_FEE_USAGE { get; set; }
        public bool LADMIN_FEE_MAINTENANCE { get; set; }
        public List<PMM01021DTO> CRATE_WG_LIST { get; set; }

        public byte[] CLOGO { get; set; }
    }
}
