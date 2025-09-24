using R_APICommonDTO;
using System.Collections.Generic;

namespace CBR00600COMMON
{
    public class CBR00600DTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CFILTER_BY { get; set; }
        public string CPERIOD { get; set; }
        public string CFROM_VALUE { get; set; }
        public string CTO_VALUE { get; set; }
        public string CMESSAGE_TYPE { get; set; }
        public string CMESSAGE_NO { get; set; }
        public bool LRECEIPT { get; set; }
        public bool LALLOCATION { get; set; }
        public bool LJOURNAL { get; set; }

        public string CREPORT_TYPE { get; set; }

        public string CLANGUAGE_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CREPORT_FILETYPE { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public bool LIS_PRINT { get; set; }
        public string CREPORT_CULTURE { get; set; }
    }

}
