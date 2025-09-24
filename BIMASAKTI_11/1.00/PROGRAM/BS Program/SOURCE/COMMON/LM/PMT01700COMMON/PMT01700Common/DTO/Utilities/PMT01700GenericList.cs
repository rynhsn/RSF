using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO.Utilities
{
    public class PMT01700GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }

    }
}
