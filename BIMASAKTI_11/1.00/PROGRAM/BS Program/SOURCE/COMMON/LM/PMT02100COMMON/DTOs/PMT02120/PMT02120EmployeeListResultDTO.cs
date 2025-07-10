using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120
{
    public class PMT02120EmployeeListResultDTO : R_APIResultBaseDTO
    {
        public List<PMT02120EmployeeListDTO> Data { get; set; }
    }
}
