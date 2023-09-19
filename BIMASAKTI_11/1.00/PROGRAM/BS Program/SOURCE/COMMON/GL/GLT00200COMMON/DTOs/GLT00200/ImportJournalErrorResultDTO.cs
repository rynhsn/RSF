using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON.DTOs.GLT00200
{
    public class ImportJournalErrorResultDTO : R_APIResultBaseDTO
    {
        public List<ImportJournalErrorDTO> Data { get; set; }
    }
}
