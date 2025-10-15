using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.Upload
{
    public class CallTypeDownloadWithErrorDTO
    {
        public string? CallTypeId { get; set; }
        public string? CallTypeName { get; set; }
        public string? Category { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public bool LinkToPriorityApps { get; set; }
        public string? Valid { get; set; } = "Y";
        public string? Notes { get; set; }
    }
}
