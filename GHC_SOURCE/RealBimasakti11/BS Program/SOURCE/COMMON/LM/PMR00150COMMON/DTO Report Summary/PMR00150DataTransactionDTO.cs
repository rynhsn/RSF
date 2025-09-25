using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150DataTransactionDTO
    {
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_NAME { get; set; }
        public List<PMR00150DataTransactionDetailDTO>? TransactionDetail { get; set; }

    
    }
}
