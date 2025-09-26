using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00220COMMON.Print_DTO
{
    public class PrintLogoResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] CLOGO { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
    }
}
