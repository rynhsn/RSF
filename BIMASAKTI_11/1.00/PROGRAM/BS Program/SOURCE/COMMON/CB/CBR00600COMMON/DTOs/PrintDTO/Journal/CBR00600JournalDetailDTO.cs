using System;

namespace CBR00600COMMON
{
    public class CBR00600JournalDetailDTO
    {
        public int INO { get; set; }
        public string CCB_REF_NO { get; set; }

        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CREF_NO { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
    }
}
