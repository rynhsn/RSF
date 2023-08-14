using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL00300DTO
    {
        private string _currency_name_code;
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CCURRENCY_SYMBOL { get; set; }
        public string CCURRENCY_NAME_CODE { get => _currency_name_code; set => _currency_name_code = $"{CCURRENCY_NAME} ({CCURRENCY_CODE})"; }
    }
}
