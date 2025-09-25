using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00800DTO
    {
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CAGREEMENT_STATUS { get; set; }
        public string? CAGREEMENT_STATUS_NAME { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CFLOW_ID { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }


        //CR12  --26/06/24
        public string? CREC_ID { get; set; }
        public string? CTRANSACTION_NAME { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CLOI_REF_NO { get; set; }
    }
}
