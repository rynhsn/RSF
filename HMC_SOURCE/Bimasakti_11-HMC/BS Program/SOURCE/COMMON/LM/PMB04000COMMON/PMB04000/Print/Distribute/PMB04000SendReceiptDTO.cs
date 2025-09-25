using PMB04000COMMON.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMB04000COMMON.Print.Distribute
{
    public class PMB04000SendReceiptDTO : PMB04000BaseDTO
    {
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CEMAIL_ID { get; set; }
        public string? CEMAIL_FROM { get; set; }
        public string? CEMAIL_TO { get; set; }
        public string? CEMAIL_SUBJECT { get; set; }
        public string? CEMAIL_BODY { get; set; }
        public bool LFLAG_PRIORITY { get; set; }
        public string? CVALID_UNTIL { get; set; }
        public bool LFLAG_HTML { get; set; }
        public string? CPROGRAM_ID { get; set; }
        public string? CSMTP_ID { get; set; }
        public string? CSTATUS { get; set; }
        public string CFILE_ID { get; set; } = "";
        public string CFILE_NAME { get; set; } = "";
        public string CSTORAGE_ID{ get; set; } = "";
    }
}
