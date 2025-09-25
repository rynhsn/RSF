using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print.Grouping
{
    public class DeptDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public List<CustomerDTO> Customers { get; set; }
        public List<SubtotalCurrenciesDTO> DeptSubtotalCurrencies { get; set; }
    }
}
