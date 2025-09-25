using APT00200COMMON.DTOs.APT00211;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00221
{
    public class TabItemEntryParameterDTO
    {
        public string CREC_ID { get; set; } = "";
        public APT00211HeaderDTO Data { get; set; }
    }
}
