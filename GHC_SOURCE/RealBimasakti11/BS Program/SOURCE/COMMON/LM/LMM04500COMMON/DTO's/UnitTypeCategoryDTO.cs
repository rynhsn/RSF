using System;
using System.Collections.Generic;
using System.Text;

namespace PMM04500COMMON.DTO_s
{
    public class UnitTypeCategoryDTO
    {
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
