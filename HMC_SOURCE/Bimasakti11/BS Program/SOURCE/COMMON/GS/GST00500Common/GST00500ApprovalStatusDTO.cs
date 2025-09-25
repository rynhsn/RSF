using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GST00500Common
{
    public class GST00500ApprovalStatusDTO : R_APIResultBaseDTO
    {
        public string CUSER_ID { get; set; }
        public string CUSER_NAME { get; set; }
        public string CPOSITION { get; set; }
        public string CAPPROVAL_STATUS_DESC { get; set; }
    }
}
