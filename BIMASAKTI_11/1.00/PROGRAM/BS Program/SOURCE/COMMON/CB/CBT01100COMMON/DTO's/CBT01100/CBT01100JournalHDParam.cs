using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01100COMMON.DTO_s.CBT01100
{
    public class CBT01100JournalHDParam : CBT01100TransHDRecordDTO
    {
        public string CUSER_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CACTION { get; set; }
        public string CINPUT_TYPE { get; set; }
        public string CNEW_STATUS { get; set; }
        public bool LAUTO_COMMIT { get; set; }
        public bool LUNDO_COMMIT { get; set; }
        public string CPERIOD { get; set; }

        public string CSEARCH_TEXT { get; set; } = "";
    }
}
