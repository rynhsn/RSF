using System;
using System.Collections.Generic;
using System.Text;

namespace Global_PMCOMMON.DTOs
{
    public abstract class BaseDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
    }
}
