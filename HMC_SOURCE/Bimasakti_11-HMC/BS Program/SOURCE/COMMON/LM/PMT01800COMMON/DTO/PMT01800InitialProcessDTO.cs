using R_APICommonDTO;

namespace PMT01800COMMON.DTO
{
    public class PMT01800InitialProcessDTO : R_APIResultBaseDTO
    {
        
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }
        
        public int CMONTH_DEFAULT { get; set; }
        public int CYEAR_DEFAULT { get; set; }
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}