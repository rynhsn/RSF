using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMM00500COMMON.DTO
{
    public class BMM00500ListResultDTO<E> : R_APIResultBaseDTO
    {
        public List<E>? Data { get; set; }
    }
}
