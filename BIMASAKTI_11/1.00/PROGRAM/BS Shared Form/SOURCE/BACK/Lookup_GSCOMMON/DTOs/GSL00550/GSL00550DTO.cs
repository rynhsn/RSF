using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL00550DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CGOA_CODE { get; set; }
        public string CGOA_NAME { get; set; }
        public char CBSIS { get; set; }
        public string CBSIS_DESCR { get; set; }
        public char CDBCR { get; set; }
        public string CDBCR_DESCR { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
