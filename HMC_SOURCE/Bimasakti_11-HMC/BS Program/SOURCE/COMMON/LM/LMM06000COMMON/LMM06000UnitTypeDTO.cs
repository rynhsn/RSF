using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace PMM06000COMMON
{
    public class LMM06000UnitTypeDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
    }

    public class LMM06000UnitTypeListDTO : R_APIResultBaseDTO
    {
        public List<LMM06000UnitTypeDTO> Data { get; set; }
    }
}
