using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM01500Common.DTOs
{
    public class LMM01500PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CCURRENCY { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class LMM01500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class LMM01500SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}