using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00700Common.DTOs
{
    public class FAT00700SubmitProcessParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CNEW_STATUS { get; set; } = string.Empty;

        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
    }
}
