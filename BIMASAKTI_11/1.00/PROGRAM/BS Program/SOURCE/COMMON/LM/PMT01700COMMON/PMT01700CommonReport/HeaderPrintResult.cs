using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700CommonReport
{
    public class HeaderPrintResult
    {
        public string CDATETIME_NOW { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public byte[] CLOGO { get; set; }
        public string? CSTORAGE_ID { get; set; }
    }
}
