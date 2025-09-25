using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GST00500Common
{
    public class GST00500DTO : R_APIResultBaseDTO
    {
        public bool LSELECTED { get; set; } = false;
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_NAME { get; set; }
        public string? CTRANS_STATUS_DESC { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CAPPROVAL_STATUS { get; set; }
        public string? CPROGRAM_ID { get; set; }

        public string? CDEPT_CODE_NAME
        {
            get => string.IsNullOrEmpty(CDEPT_NAME) || string.IsNullOrEmpty(CDEPT_CODE)
                    ? null
                    : $"{CDEPT_NAME} ({CDEPT_CODE})";
        }

        /*
        public string? CDEPT_CODE_NAME
        {
            get => _CDEPT_CODE_NAME;
            set => _CDEPT_CODE_NAME = CDEPT_NAME + " (" + CDEPT_CODE + ")";
        }
        private string? _CDEPT_CODE_NAME;
        */
    }
    
}
