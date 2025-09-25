using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace PMM00500Common.DTOs
{
    public class AccurualDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }

    public class AccrualListDTO : R_APIResultBaseDTO
    {
        public List<AccurualDTO> Data { get; set; }
    }
}
