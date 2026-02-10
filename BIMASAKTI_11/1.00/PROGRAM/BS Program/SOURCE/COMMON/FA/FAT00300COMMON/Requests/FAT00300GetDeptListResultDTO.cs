using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00300Common.Requests
{
    public class FAT00300GetDeptListResultDTO
    {
        public string CACTIVE_BY { get; set; } = string.Empty;
        public string CCENTER_CODE { get; set; } = string.Empty;
        public string CCENTER_NAME { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CINACTIVE_BY { get; set; } = string.Empty;
        public string CMANAGER_NAME { get; set; } = string.Empty;
        public string CUPDATE_BY { get; set; } = string.Empty;

        public DateTime? DACTIVE_DATE { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public DateTime? DINACTIVE_DATE { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        public bool LACTIVE { get; set; }
        public bool LEVERYONE { get; set; }
    }
}
