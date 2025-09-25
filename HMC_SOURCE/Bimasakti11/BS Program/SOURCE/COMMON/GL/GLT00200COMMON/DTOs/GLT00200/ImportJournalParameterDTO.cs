using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON.DTOs.GLT00200
{
    public class ImportJournalParameterDTO
    {
        public GLT00200HeaderDTO HeaderData { get; set; } = new GLT00200HeaderDTO();
        public List<GLT00200DetailDTO> DetailData { get; set; } = new List<GLT00200DetailDTO>();
        public string CPROCESS_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
    }
}
