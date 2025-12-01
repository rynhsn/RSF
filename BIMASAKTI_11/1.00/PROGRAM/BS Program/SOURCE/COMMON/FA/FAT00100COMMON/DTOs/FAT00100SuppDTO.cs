using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Supplier DTO for FAT00100 - Supplier Information
    /// </summary>
    public class FAT00100SuppDTO
    {
        // Standard properties (ALWAYS at top)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        // Supplier properties
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CINFO_SEQNO { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CADDRESS { get; set; } = string.Empty;
        public string CPOSTAL_CODE { get; set; } = string.Empty;
        public string CCITY { get; set; } = string.Empty;
        public string CCOUNTRY_CODE { get; set; } = string.Empty;
        public string CSTATE_CODE { get; set; } = string.Empty;
        public string CPHONE_1 { get; set; } = string.Empty;
        public string CPHONE_2 { get; set; } = string.Empty;
        public string CPHONE_3 { get; set; } = string.Empty;
        public string CFAX_NO1 { get; set; } = string.Empty;
        public string CFAX_NO2 { get; set; } = string.Empty;
        public string CFAX_NO3 { get; set; } = string.Empty;
        public string CEMAIL_1 { get; set; } = string.Empty;
        public string CEMAIL_2 { get; set; } = string.Empty;
        public string CEMAIL_3 { get; set; } = string.Empty;
        public string CTAX_REG_TP { get; set; } = string.Empty;
        public string CTAX_NAME { get; set; } = string.Empty;
        public string CTAX_REGISTER_ID { get; set; } = string.Empty;
        public DateTime DTAX_REGISTER_DATE { get; set; }
        public string CTAX_BUSINESS_TYPE { get; set; } = string.Empty;
        public string CTAX_BUSINESS_NAME { get; set; } = string.Empty;
        public string CNPWP { get; set; } = string.Empty;
        public string CNPKP { get; set; } = string.Empty;
        public string CNOTES { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }
}

