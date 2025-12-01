using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Contact Person DTO for FAT00100 - Supplier Contact Information
    /// </summary>
    public class FAT00100CPDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Contact person properties
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CINFO_SEQNO { get; set; } = string.Empty;
        public string CCONTACT_SEQNO { get; set; } = string.Empty;
        public string CFIRST_NAME { get; set; } = string.Empty;
        public string CLAST_NAME { get; set; } = string.Empty;
        public string CTITLE { get; set; } = string.Empty;
        public string COCCUP_CODE { get; set; } = string.Empty;
        public bool LDEFAULT { get; set; }
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }
}

