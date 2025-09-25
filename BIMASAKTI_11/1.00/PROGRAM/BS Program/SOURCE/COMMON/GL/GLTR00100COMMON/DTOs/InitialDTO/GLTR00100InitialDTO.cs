using R_APICommonDTO;
using System;

namespace GLTR00100COMMON
{
    public class GLTR00100InitialDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public DateTime DTODAY { get; set; }
    }


}
