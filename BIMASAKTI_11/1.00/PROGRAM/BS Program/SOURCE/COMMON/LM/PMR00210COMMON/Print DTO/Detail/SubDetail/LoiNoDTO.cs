using System;
using System.Collections.Generic;

namespace PMR00210COMMON.Print_DTO.Detail.SubDetail
{
    public class LoiNoDTO
    {
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CTENURE { get; set; }
        public string? CTRANS_STATUS_NAME { get; set; }
        public decimal? NTOTAL_PRICE { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTC_MESSAGE { get; set; }

        public List<UnitDTO>? UnitDetail { get; set; }
        public List<UtilityDTO>? UtilityDetail { get; set; }
        public List<ChargeDTO>? ChargeDetail { get; set; }
        public List<DepositDTO>? DepositDetail { get; set; }
        public List<DocumentDTO>? DocumentDetail { get; set; }

    }
}