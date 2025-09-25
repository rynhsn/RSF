using System;

namespace PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges
{
    public class PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO
    {
        public string? CSEQ_NO { get; set; }
        public string? CCHARGES_TYPE_DESCR { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CCHARGES_ID_AND_NAME { get; set; }
        public bool LBASED_OPEN_DATE { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public int IINTERVAL { get; set; }
        public string? CPERIOD_MODE { get; set; }
        public decimal NBASE_RENT { get; set; }
        public decimal NINVOICE_AMT { get; set; }
        public decimal NTOTAL_AMT { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}
