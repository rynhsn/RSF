using System;

namespace PMT03500Common.DTOs
{
    public class PMT03500UtilityMeterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CUTILITY_TYPE_NAME { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        private string _CCHARGES;

        public string CCHARGES
        {
            get => _CCHARGES;
            set => _CCHARGES = $"{CCHARGES_NAME} ({CCHARGES_ID})";
        }

        public string CMETER_NO { get; set; }
        public string CSTATUS { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}