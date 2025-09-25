using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class UploadUnitErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitErrorDTO> Data { get; set; }
    }
}
