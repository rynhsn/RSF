using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02500
{
    public class SelectedPropertyParameterDTO
    {
        public SelectedPropertyDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
    }
}
