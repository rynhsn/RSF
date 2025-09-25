using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global_PMCOMMON.DTOs.Generic__DTO
{
    public class GenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
