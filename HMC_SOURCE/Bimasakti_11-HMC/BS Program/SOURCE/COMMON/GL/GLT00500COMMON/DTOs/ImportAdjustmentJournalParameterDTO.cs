using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON.DTOs
{
    public class ImportAdjustmentJournalParameterDTO
    {
        public GLT00500HeaderDTO HeaderData { get; set; } = new GLT00500HeaderDTO();
        public List<GLT00500DetailDTO> DetailData { get; set; } = new List<GLT00500DetailDTO>();
        public string CPROCESS_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
    }
}
