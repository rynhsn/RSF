using System;
using System.Collections.Generic;
using System.Text;

namespace APR00700COMMON.Print_DTO
{
    public class APR00700LabelDTO
    {
        // === Header Labels ===
        public string LABEL_TITLE { get; set; } = "Tenant Statement";
        public string LABEL_CUSTOMER { get; set; } = "Customer";
        public string LABEL_PERIOD { get; set; } = "Period";
        public string LABEL_CURRENCY { get; set; } = "Currency";
        public string LABEL_BEG_BALANCE { get; set; } = "Beginning balance";

        // === Grouping / Detail Header Labels ===
        public string LABEL_TENANT { get; set; } = "Tenant";

        // === Column Headers ===
        public string COLUMN_REF_DATE { get; set; } = "Ref. Date";
        public string COLUMN_REFERENCE_NO { get; set; } = "Reference No.";
        public string COLUMN_TRX_TYPE { get; set; } = "Trx. Type";
        public string COLUMN_DESCRIPTION { get; set; } = "Description";
        public string COLUMN_DEBIT_AMOUNT { get; set; } = "Debit Amount";
        public string COLUMN_CREDIT_AMOUNT { get; set; } = "Credit Amount";
        public string COLUMN_BALANCE { get; set; } = "Balance";

        // === Footer / Summary Labels ===
        public string LABEL_TOTAL_DEBIT_AMOUNT { get; set; } = "Total Debit Amount";
        public string LABEL_TOTAL_CREDIT_AMOUNT { get; set; } = "Total Credit Amount";
        public string LABEL_TOTAL_END_BALANCE { get; set; } = "Total End Balance";
    }
}