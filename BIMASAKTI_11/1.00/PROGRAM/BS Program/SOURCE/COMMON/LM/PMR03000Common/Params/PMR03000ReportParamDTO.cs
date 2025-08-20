using PMR03000Common.DTOs;
using PMR03000Common.DTOs.Print;

public class PMR03000ReportParamDTO
{
    public string CCOMPANY_ID { get; set; }
    public string CPROPERTY_ID { get; set; }
    public string CPROPERTY_NAME { get; set; } = "";
    public string CPERIOD { get; set; }
    public bool LPRINT { get; set; }
    public string CREPORT_TYPE { get; set; }
    public string CSERVICE_TYPE { get; set; }
    public string CUSER_ID { get; set; }
    
    public string CFROM_TENANT { get; set; } = "";
    public string CFROM_TENANT_NAME { get; set; } = "";
    public string CTO_TENANT { get; set; } = "";
    public string CTO_TENANT_NAME { get; set; } = "";
    public string CINVOICE_MSG { get; set; } = "";
    public int IPERIOD_YEAR { get; set; }
    public string CPERIOD_MONTH { get; set; } = "";
    
    public string CFILE_NAME { get; set; } = "";
    public string CREPORT_FILETYPE { get; set; } = "";
    public string CREPORT_FILENAME { get; set; } = "";
    public bool LIS_PRINT { get; set; } = true;
    public string CMESSAGE_NO {get; set;}
    public string CMESSAGE_NAME {get; set;}
    public string TMESSAGE_DESCR_RTF { get; set; }
    public string TADDITIONAL_DESCR_RTF { get; set; }
    
    public PMR03000ReportTemplateDTO ReportTemplate = new PMR03000ReportTemplateDTO();
    public PMR03000ReportParamRateDb RateParameter { get; set; } = new PMR03000ReportParamRateDb();
}

public class PMR03000ReportParamRateDb
{
    public string CCOMPANY_ID { get; set; } = "";
    public string CPROPERTY_ID { get; set; } = "";
    public string CCHARGES_TYPE { get; set; } = "";
    public string CCHARGES_ID { get; set; } = "";
    public string CUSER_ID { get; set; } = "";
    public string CSTART_DATE { get; set; } = "";
}