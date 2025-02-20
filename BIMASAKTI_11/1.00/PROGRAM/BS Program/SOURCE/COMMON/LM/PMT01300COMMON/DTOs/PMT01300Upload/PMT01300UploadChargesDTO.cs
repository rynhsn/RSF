using R_CommonFrontBackAPI;
using System;

namespace PMT01300COMMON
{
    public class PMT01300UploadChargesDTO
    {
        public int NO { get; set; } = 0;
        public string CDOC_NO { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CCHARGES_ID { get; set; } = "";
        public string CTAX_ID { get; set; } = "";
        public int IYEARS { get; set; } = 0;
        public int IMONTHS { get; set; } = 0;
        public int IDAYS { get; set; } = 0;
        public bool LBASED_OPEN_DATE { get; set; } = false;
        public string CSTART_DATE { get; set; } = "";
        public string CEND_DATE { get; set; } = "";
        public string CBILLING_MODE { get; set; } = "";
        public string CCURENCY_CODE { get; set; } = "";
        public string CFEE_METHOD { get; set; } = "";
        public decimal NFEE_AMT { get; set; } = 0;
        public string CPERIOD_MODE { get; set; } = "";
        public bool LPRORATE { get; set; } = false;
        public string CDESCRIPTION { get; set; } = "";
        public int ISEQ_NO_ERROR { get; set; } = 0;
        public bool LERROR { get; set; }
    }
}
