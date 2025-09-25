using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120
{
    public class PMT02120RescheduleListResultDTO : R_APIResultBaseDTO
    {
        public List<PMT02120RescheduleListDTO> Data { get; set; }
    }
}
