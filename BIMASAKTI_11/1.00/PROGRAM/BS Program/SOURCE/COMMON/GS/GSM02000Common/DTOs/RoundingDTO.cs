using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM02000Common.DTOs
{
    public class RoundingListDTO : R_APIResultBaseDTO
    {
        public List<RoundingDTO> Data { get; set; }
    }
    
    public class RoundingDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
}