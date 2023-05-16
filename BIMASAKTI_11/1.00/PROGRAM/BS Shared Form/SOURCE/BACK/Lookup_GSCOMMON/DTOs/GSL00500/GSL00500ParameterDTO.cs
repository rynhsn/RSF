using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL00500ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } 
        public string CPROPERTY_ID { get; set; }
        public string CPROGRAM_CODE { get; set; }
        public string CBSIS { get; set; }
        public string CDBCR { get; set; }
        public bool LCENTER_RESTR { get; set; }
        public bool LUSER_RESTR { get; set; }
        public string CUSER_ID { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CUSER_LANGUAGE { get; set; }
    }

}
