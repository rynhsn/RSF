using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class UploadOtherUnitResultDTO : R_APIResultBaseDTO
    {
        public List<UploadOtherUnitDTO> Data { get; set; }
    }
}
