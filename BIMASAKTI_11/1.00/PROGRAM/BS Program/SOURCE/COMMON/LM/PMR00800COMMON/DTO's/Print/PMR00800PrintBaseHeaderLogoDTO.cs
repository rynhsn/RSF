using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00800COMMON.DTO_s.Print
{
    public class PMR00800PrintBaseHeaderLogoDTO
    {
        public string? CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] BLOGO { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
    }
}
