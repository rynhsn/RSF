using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_APCOMMON.DTOs.APL00400
{
    public class APL00400DTO
    {
        public string CALLOC_ID { get; set; }
        public string CALLOC_NAME { get; set; }
        public string CDEPT_MODE_NAME { get; set; }
        public string CDEPT_MODE { get; set; }
        public bool LACTIVE { get; set; }
        public string CREC_ID { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }

        // private string _CTRANSACTION;
        public string GLACCOUNT { get => CGLACCOUNT_NO + " - " + CGLACCOUNT_NAME; }
    }

}

