using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PQM00500COMMON.DTO_s
{
    public class MenuDTO : OriginalGridBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CMENU_ID { get; set; }
        public string CMENU_NAME { get; set; }
        
    }
}
