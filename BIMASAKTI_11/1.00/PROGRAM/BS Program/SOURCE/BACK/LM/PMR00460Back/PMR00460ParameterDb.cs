namespace PMR00460Back;

public class PMR00460ParameterDb
{
    public string CCOMPANY_ID { get; set; } = "";
    public string CUSER_ID { get; set; } = "";
    public string CPROPERTY_ID { get; set; } = "";
    public string CFROM_BUILDING_ID { get; set; } = "";
    public string CTO_BUILDING_ID { get; set; } = "";
    public string CFROM_PERIOD { get; set; } = "";
    public string CTO_PERIOD { get; set; } = "";
    public string CREPORT_TYPE { get; set; } = "";
    public string CTYPE { get; set; } = "S";
    public bool LOPEN { get; set; }
    public bool LSCHEDULED { get; set; }
    public bool LCONFIRMED { get; set; }
    public bool LCLOSED { get; set; }
    public string CLANG_ID { get; set; } = "";
}