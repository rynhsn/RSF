using System;

namespace PMB00300Common.DTOs
{
    public class PMB00300RecalcDTO
    {
        // public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";

        public string CREF_NO { get; set; } = "";

        // public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";

        // public string CTRANS_STATUS { get; set; } = "";
        // public string CSTATUS_NAME { get; set; } = "";
        public int IYEARS { get; set; }
        public int IMONTHS { get; set; }
        public int IDAYS { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }

        public decimal NACTUAL_AREA_SIZE { get; set; }

        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";

        public string CFLOOR_ID { get; set; } = "";
        public string CFLOOR_NAME { get; set; } = "";

        public string CUNIT_ID { get; set; } = "";
        public string CUNIT_NAME { get; set; } = "";
        public string CHO_PLAN_DATE { get; set; } = "";

        public string CHO_ACTUAL_DATE { get; set; } = "";

        // public string CSTART_DATE { get; set; } = "";
        // public string CEND_DATE { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }
}