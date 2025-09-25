using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM05000Common.DTOs
{
    public class GSM05000ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    
    public class GSM05000SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

    public class GSM05000SearchTextDTO
    {
        public string CSEARCH_TEXT { get; set; } = "";
    }
}