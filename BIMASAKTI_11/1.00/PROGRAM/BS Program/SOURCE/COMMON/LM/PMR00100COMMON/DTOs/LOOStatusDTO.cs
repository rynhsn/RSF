using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.DTOs
{
    public class LOOStatusDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }

    public class LOOStatusDataDTO : R_APIResultBaseDTO
    {
        public List<LOOStatusDTO> Data { get; set; }

    }
}
