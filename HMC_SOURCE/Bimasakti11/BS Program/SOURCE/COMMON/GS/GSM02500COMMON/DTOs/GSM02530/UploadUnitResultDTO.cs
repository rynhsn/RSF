using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class UploadUnitResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitDTO> Data { get; set; }
    }
}
