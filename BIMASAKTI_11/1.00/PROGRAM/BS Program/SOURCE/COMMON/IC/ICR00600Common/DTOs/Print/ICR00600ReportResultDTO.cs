using System.Collections.Generic;

namespace ICR00600Common.DTOs.Print
{
    public class ICR00600ReportResultDTO
    {
        public string Title { get; set; }

        public ICR00600ReportLabelDTO Label { get; set; }

        public ICR00600ReportHeaderDTO Header { get; set; }
        public List<ICR00600DataResultDTO> Data { get; set; }
    }
}