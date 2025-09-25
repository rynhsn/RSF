using System.Collections.Generic;
using R_APICommonDTO;

namespace PMT01800COMMON.DTO
{
    public class PMT01800PropertyDTO : R_APIResultBaseDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CLANGGUAGE_ID { get; set; }
    }

    public class PMT01800PropertyListDTO : R_APIResultBaseDTO
    {
        public List<PMT01800PropertyDTO> Data { get; set; }
    }
}