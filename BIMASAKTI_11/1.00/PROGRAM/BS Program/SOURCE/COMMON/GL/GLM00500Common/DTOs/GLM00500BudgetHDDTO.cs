using System;

namespace GLM00500Common.DTOs
{
    public class GLM00500BudgetHDDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CYEAR { get; set; }
        
        public string CREC_ID { get; set; }
        public string CBUDGET_NO { get; set; }
        public string CBUDGET_NAME { get; set; }
        public string CBUDGET_NAME_DISPLAY { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CCURRENCY_TYPE_NAME { get; set; }
        public bool LFINAL { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        
    }
}