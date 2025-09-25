using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00110
{
    public class GetTransactionTypeParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CLANGUAGE_ID { get; set; }
    }
}
