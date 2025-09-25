using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Upload.Agreement
{
    public class AgreementUploadDTO
    {
        public int NO { get; set; } = 0;
        public string CCOMPANY_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CLOI_REF_NO { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CHO_ACTUAL_DATE { get; set; }
        public int ISEQ_NO_ERROR { get; set; } = 0;
    }
}
