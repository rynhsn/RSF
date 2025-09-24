using System.Collections.Generic;

namespace PMT01700CommonReport
{
    public class PMTResultDataReportDTO
    {
        public string? Title { get; set; }
        public PMTLabelReportDTO? LabelReport { get; set; }
        public PMTDataReportDTO? Data { get; set; }
        public List<PMTDataChargesReportDTO>? DataCharges { get; set; } = new List<PMTDataChargesReportDTO>();
        public PMTFormatReportDTO? Format { get; set; }
        public PMTDigitalSignDTO? DigitalSign { get; set; }
    }
}
