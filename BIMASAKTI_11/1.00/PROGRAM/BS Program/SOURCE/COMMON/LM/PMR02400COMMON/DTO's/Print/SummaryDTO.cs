using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02400COMMON.DTO_s.Print
{
    public class SummaryDTO
    {
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public List<AgreementDTO> Agreements { get; set; }
        public List<SubtotalCurrencyDTO> SubtotalCurrencies { get; set; }
    }
}
