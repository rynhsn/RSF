using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class UploadUnitTypeCategoryErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitTypeCategoryErrorDTO> Data { get; set; }
    }
}
