using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GSM02530DTO
    {
        public string CUNIT_ID {  get; set; }
        public string CUNIT_NAME { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public string CUNIT_VIEW_NAME { get; set; }
        public string CUNIT_CATEGORY_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
