using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class UploadUnitTypeErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitTypeErrorDTO> Data { get; set; }
    }
}
