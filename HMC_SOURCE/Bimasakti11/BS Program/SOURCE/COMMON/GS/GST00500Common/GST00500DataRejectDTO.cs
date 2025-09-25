using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GST00500Common
{
    public class GST00500DataRejectDTO
    {
        public bool LSELECTED { get; set; } = false;
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CAPPROVAL_STATUS { get; set; }
        public string CREASON_CODE { get; set; }
        public string TNOTES { get; set; }

    }
}
