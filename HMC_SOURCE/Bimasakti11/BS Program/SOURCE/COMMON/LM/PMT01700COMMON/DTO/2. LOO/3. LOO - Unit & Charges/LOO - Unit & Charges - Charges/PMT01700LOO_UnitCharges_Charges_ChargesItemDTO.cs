using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges
{
    public class PMT01700LOO_UnitCharges_Charges_ChargesItemDTO
    {
        //Update Version For Parameter
        //public string? CPROPERTY_ID { get; set; }
        //public string? CDEPT_CODE { get; set; }
        //public string? CTRANS_CODE { get; set; }
        //public string? CREF_NO { get; set; }
        //public string? CSEQ_NO { get; set; }
        //public string? CUSER_ID { get; set; }

        //DATA
        public int ISEQ { get; set; }
        public string? CITEM_NAME { get; set; }
        public int IQTY { get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public decimal NDISCOUNT { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
    }
}
