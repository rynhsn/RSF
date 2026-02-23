using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM06500Common
{
    public class GSM06500ListDTO : R_APIResultBaseDTO
    {
        public List<GSM06500DTO> ListData { get; set; }
    }

}
