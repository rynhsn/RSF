using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02500
{
    public class SelectedBuildingResultDTO : R_APIResultBaseDTO
    {
        public SelectedBuildingDTO Data { get; set; }
    }
}
