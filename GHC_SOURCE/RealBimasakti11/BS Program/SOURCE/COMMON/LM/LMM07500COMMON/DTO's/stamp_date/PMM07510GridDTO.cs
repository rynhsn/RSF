using System;
using System.Collections.Generic;
using System.Text;

namespace PMM07500COMMON.DTO_s.stamp_date
{
    public class PMM07510GridDTO : PMM07510SaveDTO
    {
        public DateTime DDATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CLANGUAGE_ID { get; set; }
    }
}
