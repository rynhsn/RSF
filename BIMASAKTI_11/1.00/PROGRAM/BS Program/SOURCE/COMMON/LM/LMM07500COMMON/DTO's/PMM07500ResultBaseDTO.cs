using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM07500COMMON.DTO_s
{
    public class PMM07500ResultBaseDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
