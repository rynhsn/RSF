using System;
using System.Collections.Generic;
using System.Text;

namespace PMB02200COMMON.DTO_s
{
    public class UtilityChargesParamDTO : GeneralParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public bool LALL_BUILDING { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CUTILITY_TYPE { get; set; }
    }
}
