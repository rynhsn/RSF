using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace GLB00200Common
{
    public class GLB00200JournalDetailDTO
    {
        public string CREC_ID { get; set; }
        public Int64 INO { get; set; }
        public int NO_Convert { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CDBCR { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
        public string CDETAIL_DESC { get; set; }
    }

    public class GLB00200JournalDetailListDTO : R_APIResultBaseDTO
    {
        public List<GLB00200JournalDetailDTO> Data { get; set; }

    }
}