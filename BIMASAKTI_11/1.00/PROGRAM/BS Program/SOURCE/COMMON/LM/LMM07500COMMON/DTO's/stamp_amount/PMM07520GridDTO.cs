using System;
using System.Collections.Generic;
using System.Text;

namespace PMM07500COMMON.DTO_s.stamp_amount
{
    public class PMM07520GridDTO : PMM07520SaveDTO
    {
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CLANGUAGE_ID { get; set; }
    }
}