using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170DataTransactionDetailDTO
    {
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public List<PMR00170DataSalesmanDetailDTO>? SalesmanDetail  { get; set; }
    }
}
