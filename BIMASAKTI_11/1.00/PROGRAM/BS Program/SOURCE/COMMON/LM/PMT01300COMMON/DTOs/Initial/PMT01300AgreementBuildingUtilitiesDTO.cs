using System;

namespace PMT01300COMMON
{
    public class PMT01300AgreementBuildingUtilitiesDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CSEQUENCE { get; set; }
        public string CMETER_NO { get; set; }
        public string CALIAS_METER_NO { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public decimal NCAPACITY { get; set; }
        public int IMETER_MAX_RESET { get; set; }
        public bool LACTIVE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }

    public class PMT01300ParameterAgreementBuildingUtilitiesDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
    }
}
