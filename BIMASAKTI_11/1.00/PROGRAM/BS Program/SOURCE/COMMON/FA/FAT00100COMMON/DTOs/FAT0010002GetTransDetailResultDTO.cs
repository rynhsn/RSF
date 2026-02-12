using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetTransDetail method
    /// </summary>
    public class FAT0010002GetTransDetailResultDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CGL_REF_NO { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public string CDOCUMENT_NO { get; set; } = string.Empty;
        public string CDOCUMENT_DATE { get; set; } = string.Empty;
        public string CSOURCE_MODULE { get; set; } = string.Empty;
        public string CFR_DEPT_CODE { get; set; } = string.Empty;
        public string CFR_DEPT_NAME { get; set; } = string.Empty;
        public string CFR_TRANS_CODE { get; set; } = string.Empty;
        public string CFR_TRANS_NAME { get; set; } = string.Empty;
        public string CFR_REF_NO { get; set; } = string.Empty;
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CSUPPLIER_SEQ_NO { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public decimal NLTOTAL_AMOUNT { get; set; }
        public decimal NBTOTAL_AMOUNT { get; set; }
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime? DUPDATE_DATE { get; set; }
        public string CREC_ID { get; set; } = string.Empty;
    }
}

