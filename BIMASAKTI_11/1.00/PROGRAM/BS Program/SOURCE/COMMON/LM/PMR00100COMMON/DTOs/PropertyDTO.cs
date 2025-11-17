using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.DTOs
{
    public class PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
    }
    public class PropertyDataDTO : R_APIResultBaseDTO
    {
        public List<PropertyDTO> Data { get; set; }
    }
}
