using R_APICommonDTO;

namespace PMM01500COMMON
{
    public class PMM01531DTO
    {
        // parameter
        public string CSEARCH_TEXT { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }

        // + result
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; } = "";
        public string UNIT_UTILITY_CHARGE { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";
        public string CCHARGES_TYPE_DESCR { get; set; } = "";
    }
}
