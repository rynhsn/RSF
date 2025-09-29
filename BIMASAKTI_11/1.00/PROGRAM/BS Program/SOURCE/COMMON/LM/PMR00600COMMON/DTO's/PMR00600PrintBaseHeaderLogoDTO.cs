using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.DTO_s
{
    public class PMR00600PrintBaseHeaderLogoDTO
    {
        public string? CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] BLOGO { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
    }
}
