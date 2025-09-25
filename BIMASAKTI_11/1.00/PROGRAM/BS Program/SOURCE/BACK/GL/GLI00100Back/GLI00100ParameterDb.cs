namespace GLI00100Back;

public class GLI00100ParameterDb
{
    public string CCOMPANY_ID { get; set; }
    public string CLANGUAGE_ID { get; set; }
}

public class GLI00100AccountParamDb
{
    public string CGLACCOUNT_NO { get; set; }
}

public class GLI00100AccountAnalysisParamDb
{
    public string CGLACCOUNT_NO { get; set; }
    public string CCURRENCY_TYPE { get; set; }
    public string CYEAR { get; set; }
    public string CCENTER_CODE { get; set; }
    public string CBUDGET_NO { get; set; }
}

public class GLI00100BudgetParamDb
{
    public string CYEAR { get; set; }
    public string CCURRENCY_TYPE { get; set; }
}

public class GLI00100AccountAnalysisDetailTransactionParamDb
{
    public string CGLACCOUNT_NO { get; set; }
    public string CPERIOD { get; set; }
    public string CCURRENCY_TYPE { get; set; }
    public string CCENTER_CODE { get; set; }
}

public class GLI00100JournalParamDb
{
    public string CUSER_ID { get; set; }
    public string CREC_ID { get; set; }
}