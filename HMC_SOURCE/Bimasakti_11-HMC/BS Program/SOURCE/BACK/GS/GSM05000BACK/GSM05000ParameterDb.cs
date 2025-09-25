using GSM05000Common;

namespace GSM05000Back;

public class GSM05000ParameterDb
{
    public string CCOMPANY_ID { get; set; }
    public string CUSER_ID { get; set; }
    public string CLANGUAGE_ID { get; set; }
    public string CTRANS_CODE { get; set; }
    public string CDEPT_CODE { get; set; }
    public string CDEPT_CODE_TO { get; set; }
    public string CDEPT_CODE_FROM { get; set; }
    public string CUSER_LOGIN_ID { get; set; }
    public GSM05000eTabName ETAB_NAME { get; set; }
}