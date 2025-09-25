using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300GetPropertyListDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CPROPERTY_NAME { get; set; } = string.Empty;
        public bool LACTIVE { get; set; } = false;
        public string CCURRENCY { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }
}
