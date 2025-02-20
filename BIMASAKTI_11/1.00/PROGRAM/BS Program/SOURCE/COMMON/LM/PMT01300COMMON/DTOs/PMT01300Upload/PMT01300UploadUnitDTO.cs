using R_CommonFrontBackAPI;
using System;

namespace PMT01300COMMON
{
    public class PMT01300UploadUnitDTO
    {
        public int NO { get; set; } = 0;
        public string CDOC_NO { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public int ISEQ_NO_ERROR { get; set; } = 0;
        public bool LERROR { get; set; }
    }
}
