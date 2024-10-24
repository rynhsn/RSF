namespace APR00300Back;

public class APR00300ParameterDb
{
    public string CCOMPANY_ID { get; set; } = "";
    public string CUSER_ID { get; set; } = "";
    public string CLANG_ID { get; set; } = "";
    public string CPROPERTY_ID { get; set; } = "";
    public string CFROM_SUPPLIER_ID { get; set; } = "";
    public string CTO_SUPPLIER_ID { get; set; } = "";
    public string CCUT_OFF_DATE { get; set; } = "";
    public string CFROM_PERIOD { get; set; } = "";
    public string CTO_PERIOD { get; set; } = "";
    public bool LINCLUDE_ZERO_BALANCE { get; set; }
    public bool LSHOW_AGE_TOTAL { get; set; }
}