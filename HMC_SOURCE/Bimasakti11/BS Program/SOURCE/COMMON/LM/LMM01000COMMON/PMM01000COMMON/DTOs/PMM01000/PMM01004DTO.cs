using R_APICommonDTO;

namespace PMM01000COMMON
{
    public class PMM01004DTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE_FROM { get; set; }
        public string CCHARGES_TYPE_TO { get; set; }

        // result

        public string CSHORT_BY { get; set; }
        public bool LPRINT_INACTIVE { get; set; }
        public bool LPRINT_DETAIL_ACC { get; set; }
    }
}
