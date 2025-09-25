using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s
{
    public class GeneralResultDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
