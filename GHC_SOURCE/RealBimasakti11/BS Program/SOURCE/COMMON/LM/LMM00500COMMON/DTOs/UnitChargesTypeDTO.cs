using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00500Common.DTOs
{
    public class UnitChargesTypeDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
    public class UnitChargesTypeListDTO : R_APIResultBaseDTO
    {
        public List<UnitChargesTypeDTO> Data { get; set; }
    }
}
