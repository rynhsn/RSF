using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03300COMMON.DTOs.Print
{
    public class PMR03300PrintBaseHeaderLogoDTO
    {
        public string? CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] BLOGO { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
    }
}
