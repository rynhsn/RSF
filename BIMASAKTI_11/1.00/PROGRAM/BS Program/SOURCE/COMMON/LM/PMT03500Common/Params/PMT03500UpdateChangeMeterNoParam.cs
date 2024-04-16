namespace PMT03500Common.Params
{
    public class PMT03500UpdateChangeMeterNoParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CUTILITY_TYPE { get; set; } = "";
        public string CMETER_NO { get; set; } = "";
        public int IFROM_METER_NO { get; set; }
        public int IMETER_END { get; set; }
        public int ITO_METER_NO { get; set; }
        public int IMETER_START { get; set; }
        public string CSTART_INV_PRD { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public string CUSER_LOGIN_ID { get; set; }
    }
}