using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM04500Common
{
    public class GSM04500PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; }

    }
    public class GSM04500PropertyListDTO : R_APIResultBaseDTO
    {
        public List<GSM04500PropertyDTO> Data { get; set; }
    }


}
