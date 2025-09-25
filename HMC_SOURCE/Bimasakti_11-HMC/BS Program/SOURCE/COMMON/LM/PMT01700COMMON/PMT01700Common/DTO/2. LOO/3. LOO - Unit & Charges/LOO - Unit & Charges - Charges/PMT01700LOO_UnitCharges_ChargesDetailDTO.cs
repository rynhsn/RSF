using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges
{
    public class PMT01700LOO_UnitCharges_ChargesDetailDTO : PMT01700LOO_UnitCharges_ChargesListDTO  
    {
        public string? CCHARGES_DESCR { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }

        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
        public string? CBILLING_MODE { get; set; }
        public string? CFEE_METHOD { get; set; }
        public string? CCHARGE_MODE { get; set; }

        public string? CINV_AMT { get; set; }
        public string? CINVGRP_CODE { get; set; }
        public string? CINVGRP_NAME { get; set; }
        public string CDESCRIPTION { get; set; } = "";

        public bool LTAXABLE { get; set; }
        public string? CTAX_ID { get; set; }
        public string? CTAX_NAME { get; set; }
        public bool LCAL_UNIT { get; set; }
        public string? CINVOICE_PERIOD { get; set; }
        public List<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>? ChargeItemList { get; set; }

        //CR 10-07-2024
        public string? CCURRENCY_CODE { get; set; }
        public bool LPRORATE { get; set; }
        public bool LITEMS { get; set; }
        //CR 22/10/2024
        public bool LTOTAL_PRICE { get; set; }
    }
}
