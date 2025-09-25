using HDM00600COMMON.DTO.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00600COMMON.DTO
{
    public class ActiveInactivePricelistParam : GeneralParamDTO
    {
        public string CPRICELIST_ID { get; set; }
        public string CVALID_ID { get; set; }
    }
}
