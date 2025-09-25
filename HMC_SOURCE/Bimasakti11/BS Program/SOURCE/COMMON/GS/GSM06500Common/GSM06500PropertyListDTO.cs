using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM06500Common
{
    public class GSM06500PropertyListDTO : R_APIResultBaseDTO
    {
        public List<GSM06500PropertyDTO> Data { get; set; }
    }

}
