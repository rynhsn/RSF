using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON.DTOs
{
    public class ImportAdjustmentJournalErrorResultDTO : R_APIResultBaseDTO
    {
        public List<ImportAdjustmentJournalErrorDTO> Data { get; set; }
    }
}
