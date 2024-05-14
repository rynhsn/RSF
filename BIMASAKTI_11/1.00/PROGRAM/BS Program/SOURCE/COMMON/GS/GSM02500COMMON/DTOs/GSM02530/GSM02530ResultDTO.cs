using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GSM02530ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02530DTO> Data { get; set; }
    }
}
