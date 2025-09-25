using System;

namespace PMM01000COMMON
{
    public class PMM01000DTOPropety
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }

    public class PMM01000PropertyParameterDTO
    {
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
    }

}
