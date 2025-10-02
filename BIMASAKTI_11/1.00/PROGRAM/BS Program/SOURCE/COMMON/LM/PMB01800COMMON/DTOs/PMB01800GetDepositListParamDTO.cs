using System;
using System.Collections.Generic;
using System.Text;

namespace PMB01800COMMON.DTOs
{
    public class PMB01800GetDepositListParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CTRANS_TYPE { get; set; }
        public string CPAR_TRANS_CODE { get; set; }
        public string CPAR_DEPT_CODE { get; set; }
        public string CPAR_DEPT_NAME { get; set; }
        public string CUSER_ID { get; set; }
    }
}
