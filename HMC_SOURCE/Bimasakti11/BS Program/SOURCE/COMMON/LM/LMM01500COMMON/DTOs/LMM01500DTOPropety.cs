using System;

namespace LMM01500COMMON
{
    public class LMM01500DTOPropety
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class LMM01500PropertyParameterDTO
    {
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
    }

}
