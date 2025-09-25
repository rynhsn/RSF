using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global_PMCOMMON.DTOs.Generic__DTO
{
    public class GenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
