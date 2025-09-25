using System;

namespace APR00300Common.DTOs.Print
{
    public class APR00300PrintBaseHeaderLogoDTO
    {
        public string? CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] BLOGO { get; set; }
    }
}