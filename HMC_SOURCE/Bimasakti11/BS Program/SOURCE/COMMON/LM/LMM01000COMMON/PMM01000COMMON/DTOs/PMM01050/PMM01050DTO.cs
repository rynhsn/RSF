using System;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01050DTO
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
        public string CADMIN_FEE { get; set; }
        public string CADMIN_CHARGE_ID { get; set; }
        public string CADMIN_CHARGE_ID_DESCR { get; set; }
        public string CADMIN_FEE_DESCR { get; set; }
        public decimal NADMIN_FEE_PCT { get; set; }
        public decimal NADMIN_FEE_AMT { get; set; }
        public decimal NUNIT_AREA_VALID_FROM { get; set; }
        public decimal NUNIT_AREA_VALID_TO { get; set; }
        public bool LSPLIT_ADMIN { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }
        public bool LCUT_OFF_WEEKDAY { get; set; }
        public bool LHOLIDAY { get; set; }
        public bool LSATURDAY { get; set; }
        public bool LSUNDAY { get; set; }
        public List<PMM01051DTO> CRATE_OT_LIST { get; set; }
        public byte[] CLOGO { get; set; }
    }
}
