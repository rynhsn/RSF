using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO.Detail.SubDetail
{
    public class SalesmanDTO
    {
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public List<LoiNoDTO>? SalesmanDetail { get; set; }
    }
}
