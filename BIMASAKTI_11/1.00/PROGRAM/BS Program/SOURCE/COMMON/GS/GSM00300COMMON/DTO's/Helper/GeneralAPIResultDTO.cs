using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00300COMMON.DTO_s.Helper
{
    public class GeneralAPIResultDTO<T> : R_APIResultBaseDTO
    {
        public T data { get; set; }
    }
}
