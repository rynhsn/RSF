using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges
{
    //TAB LO, SUB TAB Unit & CHARGE, SUB TAB UNIT & Utilities
    public class PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO
    {
        public string? CCHARGES_TYPE_DESCR { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CDESCRIPTION { get; set; }
        public string? CMETER_NO { get; set; }
        public decimal NCAPACITY { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CSTATUS_DESCR { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string? CCHARGES_ID_NAME
        {
            get => _CCHARGES_ID_NAME;
            set => _CCHARGES_ID_NAME = CCHARGES_NAME + " (" + CCHARGES_ID + ")";
        }
        private string? _CCHARGES_ID_NAME;

        //CR 09072024 origanal grid to navigator
        public string? CTAX_ID { get; set; } = "";
        public string? CTAX_NAME { get; set; } = "";
        public int IMETER_START { get; set; } = 0;
        public string? CSTART_INV_PRD { get; set; } = "";

        //For DB Parameter : R_Display
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CUSER_ID { get; set; }
        //RealDto : For Front
        public string? CUTILITY_TYPE_DESCR { get; set; }
        public bool LTAXABLE { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }

    }
}
