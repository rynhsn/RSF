namespace PMT03500Back;

public class PMT03500ParameterDb
{
    public EPMT03500UtilityUsageTypeDb EUTYLITY_TYPE { get; set; }
    public string CCOMPANY_ID { get; set; } = "";
    public string CPROPERTY_ID { get; set; } = "";
    public string CTRANS_CODE { get; set; } = "";
    public string CLANGUAGE_ID { get; set; } = "";
    public string CUSER_ID { get; set; } = "";
    
    public string CDEPT_CODE { get; set; } = "";

    public string CYEAR { get; set; } = "";
    public string CPERIOD_NO { get; set; } = "";
    public string CBUILDING_ID { get; set; } = "";
    public string CUTILITY_TYPE { get; set; } = "";
    public string CFROM_METER_NO { get; set; } = "";
    public string CTO_METER_NO { get; set; } = "";
    public string CFLOOR_LIST { get; set; } = "";
    public bool LALL_FLOOR { get; set; }
    public string CINVOICE_PRD { get; set; } = "";
    public bool LINVOICED { get; set; }
    public string CUTILITY_PRD { get; set; } = "";
    public string CUTILITY_PRD_FROM_DATE { get; set; } = "";
    public string CUTILITY_PRD_TO_DATE { get; set; } = "";
    public string CREF_NO { get; set; } = "";
    public string CTENANT_ID { get; set; } = "";
    public string CFLOOR_ID { get; set; } = "";
    public string CUNIT_ID { get; set; } = "";
    public string CCHARGES_TYPE { get; set; } = "";
    public string CCHARGES_ID { get; set; } = "";
    public string CMETER_NO { get; set; } = "";
    public int IMETER_START { get; set; }
    public int IBLOCK1_START { get; set; }
    public int IBLOCK2_START { get; set; }
    public int IBLOCK1_END { get; set; }
    public int IBLOCK2_END { get; set; }
    public string CSTART_INV_PRD { get; set; } = "";
    public string CSTART_DATE { get; set; } = "";
    public int IFROM_METER_NO { get; set; }
    public int IMETER_END { get; set; }
    public int ITO_METER_NO { get; set; }
    public string CSEQ_NO { get; set; } = "";
    public string CINV_PRD { get; set; } = "";
    public bool LOTHER_UNIT { get; set; }
}