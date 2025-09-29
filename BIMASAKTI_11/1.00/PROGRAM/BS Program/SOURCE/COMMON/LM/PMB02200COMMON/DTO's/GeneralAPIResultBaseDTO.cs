using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMB02200COMMON.DTO_s
{
    public class GeneralAPIResultBaseDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
}
}
