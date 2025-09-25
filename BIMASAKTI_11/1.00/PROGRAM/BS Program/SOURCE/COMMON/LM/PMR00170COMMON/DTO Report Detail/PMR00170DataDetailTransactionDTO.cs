using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170DataDetailTransactionDTO
    {
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_NAME { get; set; }
        public List<PMR00170DataDetailSalesmanDTO>? TransactionDetail { get; set; }
    }
}
