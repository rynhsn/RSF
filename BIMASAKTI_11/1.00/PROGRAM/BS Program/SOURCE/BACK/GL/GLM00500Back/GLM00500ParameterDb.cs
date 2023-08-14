namespace GLM00500Back;

public class GLM00500ParameterDb
{
    public string CREC_ID { get; set; }
    public string CYEAR { get; set; }
    public string CCOMPANY_ID { get; set; }
    public string CUSER_ID { get; set; }
    public string CLANGUAGE_ID { get; set; }
    public string CGLACCOUNT_TYPE { get; set; }
    public string CBUDGET_ID { get; set; }

    public int NPERIOD_COUNT { get; set; }
    public string CCURRENCY_TYPE { get; set; }
    public decimal NBUDGET { get; set; }
    public string CROUNDING_METHOD { get; set; }
    public string CDIST_METHOD { get; set; }
    public string CBW_CODE { get; set; }
}

public class GLM00500ParameterUploadDb
{
    public string CCOMPANY_ID { get; set; }
    public string CUSER_ID { get; set; }
    public string CPROCESS_ID { get; set; }
    public string CREC_ID { get; set; }
}
