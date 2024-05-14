using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class UploadUnitPromotionResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitPromotionDTO> Data { get; set; }
    }
}
