using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00210COMMON.Print_DTO.Detail.SubDetail
{
    public class TransactionDTO
    {
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_NAME { get; set; }

        public List<SalesmanDTO>? TransactionDetail { get; set; }
    }
}
