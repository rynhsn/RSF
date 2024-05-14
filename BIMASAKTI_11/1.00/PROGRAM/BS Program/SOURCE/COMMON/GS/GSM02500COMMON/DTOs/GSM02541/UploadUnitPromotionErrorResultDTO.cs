using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class UploadUnitPromotionErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadUnitPromotionErrorDTO> Data { get; set; }
    }
}
