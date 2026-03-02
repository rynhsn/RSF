using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00700Common.DTOs
{
    public class FAT00700CompanyInfoResultDTO
    {
        // C = string (default string.Empty)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCOMPANY_NAME { get; set; } = string.Empty;
        public string CCOGS_METHOD { get; set; } = string.Empty;
        public string CPRIMARY_CO_ID { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_NAME { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_NAME { get; set; } = string.Empty;

        // L = bool
        public bool LENABLE_CENTER_IS { get; set; }
        public bool LENABLE_CENTER_BS { get; set; }
        public bool LCASH_FLOW1 { get; set; }

        // I = int
        public int ITIMEZONE { get; set; }
        public int IROW_COUNT { get; set; }

        // D = DateTime? (nullable)
        public string CDATETIME_NOW { get; set; } = string.Empty;
    }
}
