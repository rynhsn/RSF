using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;


namespace PMM00500Common.DTOs
{
    public class PMM00510ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class PMM00510GridDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGE_TYPE_ID { get; set; }
        public string CUSER_ID { get; set; }

        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSTATUS_DESCRIPTION { get; set; }
        public string CUPDATE_BY { get; set; }
        public bool LACTIVE { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CSTATUS { get; set; }
    }
}
