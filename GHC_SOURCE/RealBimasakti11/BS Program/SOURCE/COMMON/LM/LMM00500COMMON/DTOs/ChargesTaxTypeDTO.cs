using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00500Common.DTOs
{
    public class ChargesTaxTypeDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
    public class ChargesTaxTypeListDTO : R_APIResultBaseDTO
    {
        public List<ChargesTaxTypeDTO> Data { get; set; }

    }
}
