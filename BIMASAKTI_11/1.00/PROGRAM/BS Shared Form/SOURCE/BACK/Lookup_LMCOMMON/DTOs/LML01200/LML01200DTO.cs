using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML01200DTO
    {
        public string? CINVGRP_CODE { get; set; }
        public string? CINVGRP_NAME { get; set; }
        public string? CINVOICE_DUE_MODE { get; set; }
        public string? CINVOICE_DUE_MODE_NAME { get; set; }
        public string? CINVOICE_GROUP_MODE { get; set; }
        public string? CINVOICE_GROUP_MODE_NAME { get; set; }
        public bool LACTIVE { get; set; }
    }
}
