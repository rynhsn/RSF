using System;

namespace PQM00100COMMON.DTO_s
{
    public class ServiceGridDTO : ServiceBaseDTO
    {
        public string CUPDATE_BY { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}