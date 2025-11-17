using APR00700COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace APR00700OMMON
{
    public class APR00700ParamDTO : APR00700SPParamDTO
    {
        public string CREPORT_CULTURE { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CPROPERTY_NAME { get; set; } = string.Empty;
        public string CPERIOD_DISPLAY { get; set; } = string.Empty;

        public string CREPORT_FILEEXT { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
    }
}