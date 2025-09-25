using BaseHeaderReportCOMMON;
using System.Collections.Generic;

namespace PMA00400Common.DTO
{
    public class PMA00400ResultDataDTO
    {
        public PMA00400LabelDTO? Label { get; set; }
        public PMA00400HeaderDTO? HeaderData { get; set; }
        public List<PMA00400EmployeeDTO>? EmployeeData { get; set; }
        public List<PMA00400HandoverChecklistDTO>? HandoverChecklistData { get; set; }
        public List<PMA00400UtilityDTO>? UtilityData { get; set; }
        public List<PMA00400GetImageDTO>? ChecklistImageData { get; set; }
        public List<PMA00400GetImageDTO>? UtilityImageData { get; set; }
    }

    public class PMA00400ResultWithHeaderDTO : BaseHeaderResult
    {
        public PMA00400ResultDataDTO? PMA00400ResulDataFormatDTO { get; set; }
    }
}
