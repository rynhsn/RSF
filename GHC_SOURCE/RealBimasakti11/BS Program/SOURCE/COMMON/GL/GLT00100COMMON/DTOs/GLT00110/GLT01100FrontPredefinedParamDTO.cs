namespace GLT00100COMMON
{
    public class GLT01100FrontPredefinedParamDTO
    {
        //param from tab listjournal
        public GLT00110DTO? PARAM_FROM_INTERNAL_JOURNAL_LIST { get; set; }
        


        //external param
        public string PARAM_REC_ID { get; set; }

        public string PARAM_CALLER_ID { get; set; }
        public string PARAM_CALLER_TRANS_CODE { get; set; }
        public string PARAM_CALLER_REF_NO { get; set; }
        public string PARAM_CALLER_ACTION { get; set; }
        public string PARAM_DEPT_CODE { get; set; }
        public string PARAM_DEPT_NAME { get; set; }
        public string PARAM_REF_NO { get; set; }
        public string PARAM_DOC_NO { get; set; }
        public string PARAM_DOC_DATE { get; set; }
        public string PARAM_DESCRIPTION { get; set; }
        public string PARAM_SEQ_NO { get; set; }

        //currencies
        public string PARAM_CURRENCY_CODE { get; set; }
        public decimal PARAM_LC_BASE_RATE { get; set; }
        public decimal PARAM_LC_RATE { get; set; }
        public decimal PARAM_BC_BASE_RATE { get; set; }
        public decimal PARAM_BC_RATE { get; set; }
        
        //detail
        public string PARAM_GLACCOUNT_NO { get; set; }
        public string PARAM_CENTER_CODE { get; set; }
        public string PARAM_CENTER_NAME { get; set; }
        public string PARAM_BSIS { get; set; }
        public string PARAM_DBCR { get; set; }
        public decimal PARAM_AMOUNT { get; set; }
        public string PARAM_GLACCOUNT_NAME { get; set; }
    }
}
