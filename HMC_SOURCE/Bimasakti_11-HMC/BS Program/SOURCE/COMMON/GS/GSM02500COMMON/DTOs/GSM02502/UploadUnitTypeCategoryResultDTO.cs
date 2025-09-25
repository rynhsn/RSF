using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class UploadUnitTypeCategoryResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitTypeCategoryDTO> Data { get; set; }
    }
}
