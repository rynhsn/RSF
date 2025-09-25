using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02540
{
    public class UploadOtherUnitTypeResultDTO : R_APIResultBaseDTO
    {
        public List<UploadOtherUnitTypeDTO> Data { get; set; }
    }
}
