using System;

namespace PMI00500Common.DTOs
{
    public class PMI00500DTReminderDTO
    {
        public string CREMINDER_NO { get; set; } = "";
        public DateTime? DREMINDER_DATE { get; set; }
        public string CREMINDER_LEVEL { get; set; } = "";
        public string CSTATUS { get; set; } = "";
    }
}