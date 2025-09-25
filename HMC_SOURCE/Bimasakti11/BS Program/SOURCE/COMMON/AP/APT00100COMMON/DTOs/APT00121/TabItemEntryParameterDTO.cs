using APT00100COMMON.DTOs.APT00111;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00121
{
    public class TabItemEntryParameterDTO
    {
        public string CREC_ID { get; set; } = "";
        public APT00111HeaderDTO Data { get; set; }
    }
}
