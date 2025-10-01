using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges
{
    public class PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public decimal NTOTAL_NET_AREA { get; set; }
        public decimal NTOTAL_GROSS_AREA { get; set; }
        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
        public int IHOURS { get; set; }
        //ForParam
        public string? CCHARGE_MODE { get; set; }
        //FOR PRAM TAB CHARGES --11/07/2024
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string? CTRANS_STATUS { get; set; }
        public string? CCURRENCY_CODE { get; set; }

        public string? CTAXABLE_TYPE { get; set; }

    }
}
