using System.Collections.Generic;

namespace ICR00100Common.DTOs.Print
{
    public class ICR00100ReportResultDTO
    {
        public string Title { get; set; }

        public ICR00100ReportLabelDTO Label { get; set; }

        public ICR00100ReportHeaderDTO Header { get; set; }
        public List<ICR00100DataResultDTO> Data { get; set; }
    }
}