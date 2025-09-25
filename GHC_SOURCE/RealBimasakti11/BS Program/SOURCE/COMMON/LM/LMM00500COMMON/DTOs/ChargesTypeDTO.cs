using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;


namespace PMM00500Common.DTOs
{
    public class ChargesTypeDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CCHARGE_TYPE_ID { get; set; }
        public string CCHARGE_ID { get; set; }
    }

    public class ChargesTypeListDTO : R_APIResultBaseDTO
    {
        public List<ChargesTypeDTO> Data { get; set; }
    }
}
