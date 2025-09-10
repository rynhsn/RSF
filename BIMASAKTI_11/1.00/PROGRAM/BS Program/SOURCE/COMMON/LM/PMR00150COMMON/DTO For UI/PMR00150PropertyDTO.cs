using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150PropertyDTO
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CPROPERTY_NAME { get; set; }
    }
    public class ListPropertyDTO : R_APIResultBaseDTO
    {
        public List<PMR00150PropertyDTO>? Data { get; set; }
    }
}
