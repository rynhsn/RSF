using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00300DTO
    {
        private string dEPTCODE_AND_NAME;

        public string? CSUPERVISOR { get; set; }
        public string? CSUPERVISOR_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string DEPTCODE_AND_NAME { 
            get => dEPTCODE_AND_NAME; 
            set => dEPTCODE_AND_NAME = CDEPT_NAME + " (" + CDEPT_CODE + ")"; }
    }
}
