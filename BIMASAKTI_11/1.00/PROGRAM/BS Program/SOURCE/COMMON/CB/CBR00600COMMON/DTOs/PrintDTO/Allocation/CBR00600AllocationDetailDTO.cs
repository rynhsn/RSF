using System;

namespace CBR00600COMMON    
{
    public class CBR00600AllocationDetailDTO
    {
        public string CCB_REF_NO { get; set; }

        public int INO { get; set; }
        public string CALLOC_NO { get; set; }
        public string CALLOC_DATE { get; set; }
        public DateTime? DALLOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CINVOICE_NO { get; set; }
        public string CINVOICE_DESC { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
    }
}
