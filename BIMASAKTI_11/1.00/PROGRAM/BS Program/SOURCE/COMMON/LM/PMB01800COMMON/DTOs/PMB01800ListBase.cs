using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMB01800COMMON.DTOs
{
    public class PMB01800ListBase<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
