using R_CommonFrontBackAPI;
using System;

namespace PMT01300COMMON
{
    public class PMT01300UploadUtilitiesDTO
    {
        public int NO { get; set; } = 0;
        public string CDOC_NO { get; set; } = "";
        public string CUTILITY_TYPE { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CMETER_NO { get; set; } = "";
        public string CCHARGES_ID { get; set; } = "";
        public string CTAX_ID { get; set; } = "";
        public decimal NMETER_START { get; set; }
        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
        public int ISEQ_NO_ERROR { get; set; } = 0;
        public bool LERROR { get; set; }
    }
}
