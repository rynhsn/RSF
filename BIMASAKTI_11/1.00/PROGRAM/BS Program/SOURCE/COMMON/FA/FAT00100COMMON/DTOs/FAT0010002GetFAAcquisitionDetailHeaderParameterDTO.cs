using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetFAAcquisitionDetailHeader method
    /// </summary>
    public class FAT0010002GetFAAcquisitionDetailHeaderParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CFOREIGN_LANGUAGE { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CSTATUS { get; set; } = string.Empty;
        public DateTime? DUPDATE_DATE { get; set; }
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQNO { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
    }
}

