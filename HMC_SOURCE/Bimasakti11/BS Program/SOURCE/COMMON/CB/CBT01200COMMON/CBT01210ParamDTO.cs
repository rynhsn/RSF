using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{ 
    public class CBT01210ParamDTO : CBT01210JournalDetailDTO
    {
        
        public string CPARENT_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }
        public string CDETAIL_DESC { get; set; }
        public DateTime? DDOCUMENT_DATE { get; set; }
        public bool LSUSPENSE_ACCOUNT { get; set; }
    }
}
