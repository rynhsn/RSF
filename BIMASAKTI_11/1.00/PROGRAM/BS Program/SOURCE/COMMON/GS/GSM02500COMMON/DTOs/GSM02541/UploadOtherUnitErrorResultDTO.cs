using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class UploadOtherUnitErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadOtherUnitErrorDTO> Data { get; set; }
    }
}
