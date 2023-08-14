using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL00100DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public decimal NTAX_PERCENTAGE { get; set; }
    }
}
