using System;

namespace HDM00400Common.DTOs
{
    public class HDM00400PublicLocationDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPUBLIC_LOC_ID { get; set; } = "";
        public string CPUBLIC_LOC_NAME { get; set; } = "";
        public bool LACTIVE { get; set; }
        public string CLOCATION_DESCR { get; set; } = "";
        
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CFLOOR_NAME { get; set; } = "";
        
        public string CACTIVE_BY { get; set; } = "";
        public DateTime? DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; } = "";
        public DateTime? DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        
        public string CUSER_ID { get; set; } = "";
    }
}