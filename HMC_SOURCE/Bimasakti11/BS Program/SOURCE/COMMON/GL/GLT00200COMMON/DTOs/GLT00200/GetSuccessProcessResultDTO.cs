using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON.DTOs.GLT00200
{
    public class GetSuccessProcessResultDTO : R_APIResultBaseDTO
    {
        public List<GetImportJournalResult> Data { get; set; }
    }
}
