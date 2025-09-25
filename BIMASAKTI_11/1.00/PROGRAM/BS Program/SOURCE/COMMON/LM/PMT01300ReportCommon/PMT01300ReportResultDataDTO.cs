using System;

namespace PMT01300ReportCommon
{
    public class PMT01300ReportResultDataDTO
    {
        public string? Title { get; set; }
        public PMT01300ReportLabelDTO? LabelReport { get; set; }
        public PMT01300ReportDataDTO? Data { get; set; }
    }
}