using PMT01700COMMON.DTO.Utilities.ParamDb;

namespace PMT01700COMMON.DTO.Utilities.Print
{
    public class ParamaterGetReportTemplateListDTO : PMT01700BaseParameterDTO
    {
        public string? CPROGRAM_ID { get; set; }
        public string? CTEMPLATE_ID { get; set; }

    }
}
