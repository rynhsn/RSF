namespace PMT04200Common.DTOs;

public class PMT04200ParamDepositDTO
{
    public string PARAM_CALLER_ID { get; set; }
    public string PARAM_CALLER_TRANS_CODE { get; set; }
    public string PARAM_CALLER_REF_NO { get; set; }
    public string PARAM_CALLER_ACTION { get; set; }
    public string PARAM_DEPT_CODE { get; set; } 
    public string PARAM_REF_NO { get; set; } 
    public string PARAM_DOC_NO { get; set; } 
    public string PARAM_DOC_DATE { get; set; } 
    public string PARAM_DESCRIPTION { get; set; } 
    public string PARAM_GLACCOUNT_NO { get; set; }
    public string PARAM_CENTER_CODE { get; set; }
    public string PARAM_CASH_FLOW_GROUP_CODE { get; set; }
    public string PARAM_CASH_FLOW_CODE { get; set; }
    public decimal PARAM_AMOUNT { get; set; }
}

public enum ePARAM_CALLER 
{ 
    TRANSACTION,
    DEPOSIT
}