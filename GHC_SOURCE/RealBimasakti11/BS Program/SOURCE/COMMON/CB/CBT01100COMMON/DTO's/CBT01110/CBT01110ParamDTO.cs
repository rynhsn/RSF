using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01100COMMON.DTO_s.CBT01110
{
    public class CBT01110ParamDTO : CBT01110JournalDetailDTO
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
