using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace PMM06000COMMON
{
    public class LMM06000PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; }
    }
    public class LMM06000PropertyListDTO : R_APIResultBaseDTO
    {
        public List<LMM06000PropertyDTO> Data { get; set; }
    }
}
