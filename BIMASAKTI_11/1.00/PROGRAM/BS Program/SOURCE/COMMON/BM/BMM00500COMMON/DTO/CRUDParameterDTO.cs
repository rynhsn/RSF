using System;
using System.Collections.Generic;
using System.Text;

namespace BMM00500COMMON.DTO
{
    public class CRUDParameterDTO<T> where T : class, new()
    {
        public T Data { get; set; } = new T();
    }
}
