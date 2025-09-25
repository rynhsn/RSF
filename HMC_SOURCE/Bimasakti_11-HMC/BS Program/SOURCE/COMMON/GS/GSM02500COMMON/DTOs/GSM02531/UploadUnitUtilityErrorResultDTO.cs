using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02531
{
    public class UploadUnitUtilityErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitUtilityErrorDTO> Data { get; set; }
    }
}
