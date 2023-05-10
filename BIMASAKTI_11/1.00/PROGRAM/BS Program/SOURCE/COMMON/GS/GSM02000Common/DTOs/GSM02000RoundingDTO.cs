using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM02000Common.DTOs
{
    public class GSM02000RoundingListDTO : R_APIResultBaseDTO
    {
        public List<GSM02000RoundingDTO> Data { get; set; }
    }
    
    public class GSM02000RoundingDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
}