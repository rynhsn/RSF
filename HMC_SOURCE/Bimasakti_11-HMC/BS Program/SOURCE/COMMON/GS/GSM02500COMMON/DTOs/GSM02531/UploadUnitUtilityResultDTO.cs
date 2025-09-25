using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02531
{
    public class UploadUnitUtilityResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitUtilityDTO> Data { get; set; }
    }
}
