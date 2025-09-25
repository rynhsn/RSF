using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170PropertyDTO
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CPROPERTY_NAME { get; set; }
    }
    public class ListPropertyDTO : R_APIResultBaseDTO
    {
        public List<PMR00170PropertyDTO>? Data { get; set; }
    }
}
