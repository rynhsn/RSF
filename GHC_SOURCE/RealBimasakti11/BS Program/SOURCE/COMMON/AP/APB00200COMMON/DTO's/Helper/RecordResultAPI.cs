using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APB00200COMMON.DTO_s.Helper
{
    public class RecordResultAPI<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }

    }
}
