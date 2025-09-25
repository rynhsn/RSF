using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Facility
{
    public class GSM02502FacilityDTO
    {
        public string CFACILITY_TYPE { get; set; }
        public string CFACILITY_TYPE_DESCR { get; set; }
        public int IQTY { get; set; }
        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
        public bool LACTIVE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}
