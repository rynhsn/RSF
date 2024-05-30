namespace GLR00100Common.DTOs.Print
{
    public class GLR00100ReportLabelDTO
    {
        public string DEPARTMENT { get; set; } = "Department";
        public string PERIOD { get; set; } = "Period";
        public string CURRENCY_TYPE { get; set; } = "Currency Type";

        public string DATE { get; set; } = "Date";
        public string TRANS_CODE { get; set; } = "Trans. Code";
        public string TRANS_NAME { get; set; } = "Trans. Name";
        public string DEPT { get; set; } = "Dept.";
        public string REF_NO { get; set; } = "Reference No.";
        public string ACTIVITY_DESC { get; set; } = "Activity Description";
        public string CUSTOMER_SUPPLIER { get; set; } = "Customer/Supplier";
        public string BY { get; set; } = "By";
        public string MODULE { get; set; } = "Module";
        public string ACCOUNT_NO { get; set; } = "Account No.";
        public string ACCOUNT_NAME { get; set; } = "Account Name";
        public string CENTER { get; set; } = "Center";
        public string VOUCHER_NO { get; set; } = "Voucher No.";
        public string JOURNAL_DESC { get; set; } = "Journal Description";
        public string VOUCHER_DATE { get; set; } = "Voucher Date";
        public string CURR { get; set; } = "Curr.";
        public string DEBIT { get; set; } = "Debit";
        public string CREDIT { get; set; } = "Credit";

        public string SUMMARY_IN { get; set; } = "Summary in";
        public string TOTAL_DEBIT { get; set; } = "Total Debit";
        public string TOTAL_CREDIT { get; set; } = "Total Credit";

        public string TOTAL { get; set; } = "Total";
        public string GRAND_TOTAL { get; set; } = "Grand Total";
    }
}