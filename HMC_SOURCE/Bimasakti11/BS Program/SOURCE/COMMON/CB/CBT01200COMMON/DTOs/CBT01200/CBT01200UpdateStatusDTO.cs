using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01200UpdateStatusDTO
    {
        public string CREC_ID { get; set; }
        public string CNEW_STATUS { get; set; }
        public bool LAUTO_COMMIT { get; set; }
        public bool LUNDO_COMMIT { get; set; }
    }
}
