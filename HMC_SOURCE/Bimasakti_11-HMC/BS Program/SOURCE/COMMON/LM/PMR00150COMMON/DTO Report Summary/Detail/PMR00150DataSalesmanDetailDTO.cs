﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150DataSalesmanDetailDTO
    {
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; } = DateTime.Now.ToString("D");
        public DateTime? DREF_DATE { get; set; }
        public string? CTENURE { get; set; }
        public decimal NTOTAL_GROSS_AREA_SIZE { get; set; }
        public decimal NTOTAL_NET_AREA_SIZE { get; set; }
        public decimal NTOTAL_COMMON_AREA_SIZE { get; set; }
        public string? CAGREEMENT_STATUS_NAME { get; set; }
        public string? CTRANS_STATUS_NAME { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
        public string? CTAX { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTC_MESSAGE { get; set; }
    }
}
