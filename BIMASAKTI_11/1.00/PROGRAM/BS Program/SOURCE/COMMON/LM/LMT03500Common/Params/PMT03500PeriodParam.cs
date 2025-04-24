namespace PMT03500Common.Params
{
    public class PMT03500PeriodParam
    {
        public string CYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
    }
    
    public class PMT03500RateParam
    {
        public string CPROPERTY_ID { get; set; }
        public string CCHARGE_TYPE_ID { get; set; }
        public string CCHARGES_ID { get; set; }
    }

    public class PMT03500PeriodRangeParam
    {
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
    }
}