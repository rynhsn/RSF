using System;

namespace PMT01300COMMON
{
    public class PMT01300AgreementChargeCalUnitDTO
    {
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public decimal NTOTAL_AREA { get; set; }
        public decimal NFEE_PER_AREA { get; set; }
    }

    public class PMT01300ParameterAgreementChargeCalUnitDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CSEQ_NO { get; set; }
    }
}
