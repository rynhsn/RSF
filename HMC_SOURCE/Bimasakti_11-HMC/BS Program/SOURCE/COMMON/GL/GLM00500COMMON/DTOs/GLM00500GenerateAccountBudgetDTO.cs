using R_APICommonDTO;

namespace GLM00500Common.DTOs
{
    public class GLM00500GenerateAccountBudgetDTO : R_APIResultBaseDTO
    {
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CBUDGET_NO { get; set; }
        public string CBUDGET_NAME { get; set; }
        public string CBUDGET_ID { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CGLACCOUNT_TYPE { get; set; }
        public string CFROM_GLACCOUNT_NO { get; set; }
        public string CFROM_GLACCOUNT_NAME { get; set; }
        public string CTO_GLACCOUNT_NO { get; set; }
        public string CTO_GLACCOUNT_NAME { get; set; }
        public string CFROM_CENTER_CODE { get; set; }
        public string CFROM_CENTER_NAME { get; set; }
        public string CTO_CENTER_CODE { get; set; }
        public string CTO_CENTER_NAME { get; set; }
        public string CBASED_ON { get; set; }
        public string CYEAR { get; set; }
        public string CSOURCE_BUDGET_NO { get; set; }
        public string CFROM_PERIOD_NO { get; set; }
        public string CTO_PERIOD_NO { get; set; }
        public string CBY { get; set; }
        public decimal NBY_PCT { get; set; }
        public decimal NBY_AMOUNT { get; set; }
        public string CUPDATE_METHOD { get; set; }
    }
}