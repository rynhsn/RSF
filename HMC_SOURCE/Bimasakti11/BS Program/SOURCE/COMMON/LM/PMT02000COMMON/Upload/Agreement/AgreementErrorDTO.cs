using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Upload.Agreement
{
    public  class AgreementErrorDTO
    {
        public int NO { get; set; } = 0;
        public string? Property { get; set; }
        public string? Transaction { get; set; }
        public string? Department { get; set; }
        public string? LOI_AgrmntNo { get; set; }
        public string? Building { get; set; }
        public string? HORefNo { get; set; }
        public string? HORefDate { get; set; }
        public string? HOActualDate { get; set; }
        public string? Valid { get; set; }
        public string? Notes { get; set; }
        public string? NotesError { get; set; }
    }
}
