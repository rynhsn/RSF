using PMR03100COMMON.DTO_s.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03100COMMON.DTO_s.Print
{
    public class ParamDTO : SPParamDTO
    {
        public string CPROPERTY_NAME { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILEEXT { get; set; } = "";
    }
}
