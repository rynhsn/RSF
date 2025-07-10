using PMR03000Common.DTOs;

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
    public PMR03000ReportTemplateDTO ReportTemplate = new PMR03000ReportTemplateDTO();
}