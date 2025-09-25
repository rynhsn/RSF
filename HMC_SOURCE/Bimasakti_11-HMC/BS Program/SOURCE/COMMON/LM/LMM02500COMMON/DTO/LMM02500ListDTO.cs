using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM02500Common.DTO
{
    public class LMM02500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }

    public class LMM02500ComboBoxType
    {
        public string? CCODE { get; set; }
        public string? CDESCRIPTION { get; set; }
    }

    /*
    public class LMM02500GridDTO
    {
        public string CTENANT_GROUP_ID { get; set; }
        public string CTENANT_GROUP_NAME { get; set; }
        public string CPHONE1 { get; set; }
        public string CEMAIL { get; set; }
        public string CPIC_NAME { get; set; }
        public string CPIC_MOBILE_PHONE1 { get; set; }
        public string CPIC_EMAIL { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
    */

    public class LMM02500ParameterDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CPROPERTY_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}