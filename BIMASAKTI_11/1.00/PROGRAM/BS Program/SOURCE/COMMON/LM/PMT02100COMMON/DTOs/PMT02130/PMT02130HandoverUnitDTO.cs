using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02130
{
    public class PMT02130HandoverUnitDTO
    {
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CUNIT_ID { get; set; }

        //Tab Confirmed => Handover Process
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
    }
}
