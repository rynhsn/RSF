using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00500COMMON.DTO.General
{
    public class GeneralAPIResultBaseDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
