using System.Collections.Generic;

namespace APR00100COMMON.DTO_s.Print
{
    public class APR00100DataResultDTO
    {
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public List<APR00100SummaryBySupp1DTO> Detail1 { get; set; }
    }
}