using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class GSM02503ImageParameterDTO
    {
        public GSM02503ImageDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CUNIT_TYPE_ID { get; set; } = "";
        public byte[] OIMAGE { get; set; }
        public string CFILE_NAME { get; set; } = "";
        public string CFILE_EXTENSION { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
