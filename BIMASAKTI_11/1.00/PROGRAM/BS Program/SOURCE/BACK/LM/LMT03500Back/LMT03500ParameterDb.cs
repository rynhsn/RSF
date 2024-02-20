namespace LMT03500Back;

public class LMT03500ParameterDb
{
    public string CCOMPANY_ID { get; set; }
    public string CPROPERTY_ID { get; set; }
    public string CTRANS_CODE { get; set; } = "";
    public string CLANGUAGE_ID { get; set; }
    public string CUSER_ID { get; set; }

    public string CYEAR { get; set; }
    public string CBUILDING_ID { get; set; }
    public string CUTILITY_TYPE { get; set; }
    public string CFLOOR_LIST { get; set; }
    public bool LALL_FLOOR { get; set; }
    public string CINVOICE_PRD { get; set; }
    public bool LINVOICED { get; set; }
    public string CUTILITY_PRD { get; set; }
    public string CUTILITY_PRD_FROM_DATE { get; set; }
    public string CUTILITY_PRD_TO_DATE { get; set; }
    public string CREF_NO { get; set; }
}