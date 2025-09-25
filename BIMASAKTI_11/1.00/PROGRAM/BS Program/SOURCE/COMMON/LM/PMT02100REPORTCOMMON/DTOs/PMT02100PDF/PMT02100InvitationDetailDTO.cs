using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100PDF
{
    public class PMT02100InvitationDetailDTO
    {
        public string CCHARGES_TYPE_DESCRIPTION { get; set; }
        public string CDESCRIPTION { get; set; }
        public decimal NFEE { get; set; }
        public string CCURRENCY_SYMBOL { get; set; }
        public bool LTAXABLE { get; set; }
        public decimal NTOTAL { get; set; }
    }
}
