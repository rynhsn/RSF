using System;

namespace GLI00100Common.DTOs
{
    public class GLI00100BudgetDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CBUDGET_NO { get; set; }
        public string CREC_ID { get; set; }
        public string CYEAR { get; set; }
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