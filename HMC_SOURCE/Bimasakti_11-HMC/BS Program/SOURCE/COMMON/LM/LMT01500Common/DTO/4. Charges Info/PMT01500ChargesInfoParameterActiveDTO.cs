using System;

namespace PMT01500Common.DTO._4._Charges_Info
{
    public class PMT01500ChargesInfoParameterActiveDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CSEQ_NO { get; set; }
        public Boolean LACTIVE { get; set; }
        public string? CUSER_ID { get; set; }
    }
}
