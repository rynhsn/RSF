using R_APICommonDTO;
using System;

namespace GLM00400COMMON
{
    public class GLM00400InitialDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CYEAR { get; set; } = "";
        public string CMODE { get; set; } = "";
        public string CUSER_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public DateTime DTODAY { get; set; }
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }


}
