using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Facility
{
    public class GSM02502FacilityTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02502FacilityTypeDTO> Data { get; set; }
    }
}
