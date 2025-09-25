using System.Collections.Generic;

namespace PMR02400COMMON.DTO_s.Print
{
    public class DetailDTO
    {
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public List<AgreementDtDTO> Agreements { get; set; }
        public List<SubtotalCurrencyDTO> SubtotalCurrencies { get; set; }
    }
}
