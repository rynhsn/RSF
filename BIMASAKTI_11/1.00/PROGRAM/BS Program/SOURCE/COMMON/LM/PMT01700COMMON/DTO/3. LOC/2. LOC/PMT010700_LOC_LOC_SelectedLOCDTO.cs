using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._3._LOC._2._LOC
{
    public class PMT010700_LOC_LOC_SelectedLOCDTO
    {
        //empty string but used for SP
        public string? CCURRENCY_CODE { get; set; } = "";
        public string? CLEASE_MODE { get; set; } = "";
        public string? CCHARGE_MODE { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string? CDOC_DATE { get; set; } = "";
        //For Parameter R_Display
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CUSER_ID { get; set; }
        //Real DTOs
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_ID { get; set; } = "";
        public string? CBUILDING_NAME { get; set; }
        public string? CEVENT_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CREF_NO { get; set; } = "";
        public string? CORIGINAL_REF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CFOLLOW_UP_DATE { get; set; } = "";
        public DateTime? DFOLLOW_UP_DATE { get; set; }
        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
        public int IHOURS { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }

        //Perlu Confirmasi
        public string? CSTART_TIME { get; set; } = "";
        public DateTime? DSTART_TIME { get; set; }
        public string? CEND_TIME { get; set; } = "";
        public DateTime? DEND_TIME { get; set; }

        public string? CUNIT_DESCRIPTION { get; set; } = "";
        public string? CNOTES { get; set; } = "";
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CAGREEMENT_STATUS_DESCR { get; set; }
        public string? CBILLING_RULE_CODE { get; set; }
        public string? CBILLING_RULE_NAME { get; set; }        
        public decimal NBOOKING_FEE { get; set; }
        public string? CTC_CODE { get; set; }

        //CR12/07/2024
        public string? CLINK_TRANS_CODE { get; set; }
        public string? CLINK_REF_NO { get; set; }

        //DTO for enable disable button
        public string? CTRANS_STATUS { get; set; }

    }
}
