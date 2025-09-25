using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace PMM00500Common.DTOs
{
    public class PropertyListDataChargeDTO : R_APIResultBaseDTO
    {
        public List<PropertyListStreamChargeDTO> Data { get; set; }
    }
    public class PropertyListStreamChargeDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
    }
}
