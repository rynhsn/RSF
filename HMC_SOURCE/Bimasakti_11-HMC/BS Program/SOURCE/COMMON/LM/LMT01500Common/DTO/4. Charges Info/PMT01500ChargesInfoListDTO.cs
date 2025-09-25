using System;

namespace PMT01500Common.DTO._4._Charges_Info
{
    public class PMT01500ChargesInfoListDTO
    {
        public string? CSEQ_NO { get; set; }
        public string? CCHARGES_TYPE_DESCR { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public decimal NFEE_AMT { get; set; }
        public string? CINVOICE_PERIOD { get; set; }
        public Boolean LACTIVE { get; set; }
        public decimal NINVOICE_AMT { get; set; }
        public decimal NTOTAL_AMT { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
