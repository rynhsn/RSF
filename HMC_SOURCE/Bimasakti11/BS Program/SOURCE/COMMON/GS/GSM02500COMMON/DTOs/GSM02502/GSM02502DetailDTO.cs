using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class GSM02502DetailDTO
    {
        public string CUNIT_TYPE_CATEGORY_ID { get; set; } = "";
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public bool LACTIVE { get; set; } = true;
        public string CPROPERTY_TYPE { get; set; } = "";
        public bool LSINGLE_UNIT { get; set; } = false;
        public int IINVITATION_INVOICE_PERIOD { get; set; } = 0;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
