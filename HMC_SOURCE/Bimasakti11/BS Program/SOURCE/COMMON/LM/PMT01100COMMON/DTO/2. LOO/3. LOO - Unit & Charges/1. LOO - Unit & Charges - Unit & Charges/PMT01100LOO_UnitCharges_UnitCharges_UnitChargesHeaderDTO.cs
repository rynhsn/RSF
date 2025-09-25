using R_APICommonDTO;

namespace PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges
{
    public class PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public decimal NTOTAL_NET_AREA { get; set; }
        public string? CYEAR { get; set; }
        public string? CMONTH { get; set; }
        public string? CDAY { get; set; }
        public int IYEAR { get; set; }
        public int IMONTH { get; set; }
        public int IDAY { get; set; }
    }
}
