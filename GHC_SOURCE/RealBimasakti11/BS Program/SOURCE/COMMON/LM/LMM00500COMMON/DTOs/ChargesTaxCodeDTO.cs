using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;


namespace PMM00500Common.DTOs
{
    public class ChargesTaxCodeDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }

    public class ChargesTaxCodeListDTO : R_APIResultBaseDTO
    {
        public List<ChargesTaxCodeDTO> Data { get; set; }

    }
}
