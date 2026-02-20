using System;

namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT01100GetDeptLookupList method (RSP_GS_GET_DEPT_LOOKUP_LIST) - list
    /// </summary>
    public class FAT01100GetDeptLookupListResultDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCENTER_CODE { get; set; } = string.Empty;
        public string CCENTER_NAME { get; set; } = string.Empty;
        public string CMANAGER_NAME { get; set; } = string.Empty;
        public int LEVERYONE { get; set; }
        public int LACTIVE { get; set; }
        public string CACTIVE_BY { get; set; } = string.Empty;
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; } = string.Empty;
        public DateTime DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }
}
