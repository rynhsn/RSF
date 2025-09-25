using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;


namespace PMM00500Common.DTOs
{
    public class ChargesCStatusDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }

    public class ChargesCStatusListDTO : R_APIResultBaseDTO
    {
        public List<ChargesCStatusDTO> Data { get; set; }

    }
}
