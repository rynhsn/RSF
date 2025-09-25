using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_APCOMMON.DTOs.APL00200
{
    public class APL00200DTO
    {
        public string CEXPENDITURE_ID { get; set; }
        public string CCOMPANY_ID {get; set; }
        public string CCATEGORY_TYPE {get; set; }
        public string CUSER_ID { get; set; }
        public string CEXPENDITURE_NAME { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CCATEGORY_ID { get; set; } = "";
        public bool LTAXABLE { get; set; }
        public string CWITHHOLDING_TAX_NAME { get; set; }
        public string COTHER_TAX_ID { get; set; }
        public string COTHER_TAX_NAME { get; set; }
        public decimal NOTHER_TAX_PCT { get; set; }
        public string COTHER_TAX_PCT { get; set; }
        public string CUNIT { get; set; }

        public string RadioButton { get; set; } = "A";
        public string Code { get; set; }
        public string Desc { get; set; }

    }
}
