using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00300DTO
    {
        //private string dEPTCODE_AND_NAME;

        public string? CSUPERVISOR { get; set; }
        public string? CSUPERVISOR_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string DEPTCODE_AND_NAME
        {
            get
            {
                var name = string.IsNullOrWhiteSpace(CDEPT_NAME) ? "-" : CDEPT_NAME;
                var code = string.IsNullOrWhiteSpace(CDEPT_CODE) ? null : CDEPT_CODE;

                return code == null ? name : $"{name} ({code})";
            }
        }
        public string? CTYPE { get; set; }
        public string? CTYPE_DESCR { get; set; }
        public string CTYPE_AND_DESC

        {
            get
            {
                var descr = string.IsNullOrWhiteSpace(CTYPE_DESCR) ? "-" : CTYPE_DESCR;
                var type = string.IsNullOrWhiteSpace(CTYPE) ? null : CTYPE;

                return type == null ? descr : $"{descr} ({type})";
            }
        }


    }
}
