using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170DataDetailSalesmanDTO
    {
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public List<PMR00170DataDetailLocNoDTO>? SalesmanDetail { get; set; }
    }
}
