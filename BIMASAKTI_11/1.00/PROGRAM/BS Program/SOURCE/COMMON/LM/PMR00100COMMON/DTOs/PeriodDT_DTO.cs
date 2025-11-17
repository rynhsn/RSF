using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.DTOs
{
    public class PeriodDT_DTO
    {
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
    }

    public class PeriodDT_DataDTO : R_APIResultBaseDTO
    {
        public List<PeriodDT_DTO> Data { get; set; }
    }
}
