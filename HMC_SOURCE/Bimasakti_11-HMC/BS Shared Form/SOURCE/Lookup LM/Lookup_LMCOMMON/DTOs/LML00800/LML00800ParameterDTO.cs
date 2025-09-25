using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00800ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CAGGR_STTS { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
        //CR12 --26/06/24
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CTRANS_STATUS { get; set; } = "";

        public string CSEARCH_TEXT { get; set; } = "";
    }
}
