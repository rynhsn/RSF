using System.Collections.Generic;
using R_APICommonDTO;

namespace APR00300Common.DTOs
{
    //DTO ini digunakan untuk menampung data list
    public class APR00300ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}