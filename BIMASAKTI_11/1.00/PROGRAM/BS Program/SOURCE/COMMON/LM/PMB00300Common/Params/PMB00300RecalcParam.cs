namespace PMB00300Common.Params
{
    public class PMB00300RecalcParam
    {
        public string CPROPERTY_ID { get; set; } = "";
    }

    public class PMB00300RecalcChargesParam
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
    }
    
    public class PMB00300RecalcRuleParam
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
    }
}