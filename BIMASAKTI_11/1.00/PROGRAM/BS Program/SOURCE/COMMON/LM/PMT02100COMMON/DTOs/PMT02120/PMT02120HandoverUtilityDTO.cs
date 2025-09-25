using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120
{
    public class PMT02120HandoverUtilityDTO
    {
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }

        //Handover Process
        public string CUTILITY_TYPE { get; set; }
        public string CMETER_NO { get; set; }
        public decimal NCAPACITY { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_DISPLAY { get; set; }
        public string CSEQ_NO { get; set; }
        public int ISTART_INV_PRD_YEAR { get; set; }
        public int ISTART_INV_PRD_MONTH { get; set; }
        public decimal NMETER_START { get; set; }
        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
        public string CSTART_INV_PRD { get; set; }
    }
}
