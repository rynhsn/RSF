using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150DataDetailSalesmanDTO
    {
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public List<PMR00150DataDetailLocNoDTO>? SalesmanDetail { get; set; }
    }
}
