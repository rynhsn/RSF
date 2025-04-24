using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common.DTO_s
{
    public class GeneralAPIResultDTO<T> : R_APIResultBaseDTO
    {
        public T data { get; set; }
    }
}
