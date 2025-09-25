using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{ 
    public class PMR00170DataDetailLocNoDTO
    {
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; } = DateTime.Now.ToString("D");
        public DateTime? DREF_DATE { get; set; }
        public string? CTENURE { get; set; }
        public string? CAGREEMENT_STATUS_NAME { get; set; }
        public string? CTRANS_STATUS_NAME { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
        public string? CTAX { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTC_MESSAGE { get; set; }
        public List<PMR00170DataDetailUnitDTO>? UnitDetail { get; set; }
        public List<PMR00170DataDetailChargeDTO>? ChargeDetail { get; set; }
        public List<PMR00170DataDetailDepositDTO>? DepositDetail { get; set; }
    }
}