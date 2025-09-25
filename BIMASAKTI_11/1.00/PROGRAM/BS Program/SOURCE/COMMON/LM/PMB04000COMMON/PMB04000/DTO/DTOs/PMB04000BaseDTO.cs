using System;
using System.Collections.Generic;
using System.Text;

namespace PMB04000COMMON.DTO.DTOs
{
    public abstract class PMB04000BaseDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CLANG_ID { get; set; }
    }
}
