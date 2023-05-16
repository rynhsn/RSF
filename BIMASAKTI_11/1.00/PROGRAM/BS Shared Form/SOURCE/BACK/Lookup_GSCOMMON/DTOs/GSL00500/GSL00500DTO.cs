using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL00500DTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public char CBSIS { get; set; }
        public string CBSIS_DESCR { get; set; }
        public char CDBCR { get; set; }
        public string CDBCR_DESCR { get; set; }
        public bool LACTIVE { get; set; }
        public bool LCENTER_RESTR { get; set; }
        public bool LUSER_RESTR { get; set; }


    }
}
