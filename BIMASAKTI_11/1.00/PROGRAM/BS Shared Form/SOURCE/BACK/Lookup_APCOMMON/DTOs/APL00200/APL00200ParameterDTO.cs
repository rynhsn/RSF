using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_APCOMMON.DTOs.APL00200
{
    public class APL00200ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CTAXABLE_TYPE { get; set; }  ="";
        public string CACTIVE_TYPE { get; set; } ="";
        public string CLANGUAGE_ID { get; set; } ="";
        public string CTAX_DATE { get; set; } ="";
        public string CCATEGORY_ID { get; set; } = "";
        public string CSEARCH_CODE { get; set; } = "";
    }
}
