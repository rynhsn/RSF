using PMT50600COMMON.DTOs.PMT50611;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50621
{
    public class TabItemEntryParameterDTO
    {
        public string CREC_ID { get; set; } = "";
        public bool LIS_NEW { get; set; } = false;
        public PMT50611HeaderDTO Data { get; set; }
    }
}
