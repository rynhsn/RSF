using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300GetAgreementFormListResultDTO : R_APIResultBaseDTO
    {
        public List<PMI00300GetAgreementFormListDTO> Data { get; set; }
    }
}
