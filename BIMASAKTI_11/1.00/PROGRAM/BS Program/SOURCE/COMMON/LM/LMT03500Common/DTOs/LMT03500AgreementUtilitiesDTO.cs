using System;
using R_APICommonDTO;

namespace LMT03500Common.DTOs
{
    public class LMT03500AgreementUtilitiesDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CUTILITY_TYPE_DESCR { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSEQ_NO { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public string CMETER_NO { get; set; }
        public int IMETER_START { get; set; }
        public string CSTART_INV_PRD { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_DESCR { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}