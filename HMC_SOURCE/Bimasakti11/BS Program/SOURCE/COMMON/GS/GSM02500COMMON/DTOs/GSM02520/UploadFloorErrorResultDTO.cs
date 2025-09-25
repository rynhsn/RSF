using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class UploadFloorErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadFloorErrorDTO> Data { get; set; }
    }
}
