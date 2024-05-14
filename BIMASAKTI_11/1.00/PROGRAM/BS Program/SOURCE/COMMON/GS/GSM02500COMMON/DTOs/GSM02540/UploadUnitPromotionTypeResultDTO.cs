using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02540
{
    public class UploadUnitPromotionTypeResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitPromotionTypeDTO> Data { get; set; }
    }
}
