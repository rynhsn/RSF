using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs
{
    public class GSM02500ActiveInactiveParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CUNIT_TYPE_CATEGORY_ID { get; set; } = "";
        public string CUNIT_TYPE_ID { get; set; } = "";
        public bool LACTIVE { get; set; } = false;
        public string CUSER_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CUTILITIES_TYPE { get; set; } = "";
        public string CSEQUENCE { get; set; } = "";
        public string CUNIT_PROMOTION_TYPE_ID { get; set; } = "";
        public string CUNIT_PROMOTION_ID { get; set; } = "";
    }
}
