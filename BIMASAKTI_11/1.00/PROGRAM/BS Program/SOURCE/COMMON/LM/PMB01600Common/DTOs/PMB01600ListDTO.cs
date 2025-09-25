using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMB01600Common.DTOs
{
    public class PMB01600ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
