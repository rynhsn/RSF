using System.Numerics;

namespace PMF00200ReportCommon
{
    public class PMF00200ColumnPrintDTO
    {
        public string AccountNo { get; set; } = "Account No.";
        public string Allocation { get; set; } = "Allocation";
        public string Amount { get; set; } = "Amount";
        public string CashBank { get; set; } = "Cash / Bank";
        public string CATitleName { get; set; } = "CASH – RECEIPT FROM CUSTOMER";
        public string Center { get; set; } = "Center";
        public string ChequeGiroNo { get; set; } = "Cheque / Giro No.";
        public string CQTitleName { get; set; } = "CHEQUE – RECEIPT FROM CUSTOMER";
        public string Credit { get; set; } = "Credit";
        public string Currency { get; set; } = "Currency";
        public string Customer { get; set; } = "Customer";
        public string CustomerReceipt { get; set; } = "Customer Receipt";
        public string Date { get; set; } = "Date";
        public string Debit { get; set; } = "Debit";
        public string Department { get; set; } = "Department";
        public string Description { get; set; } = "Description";
        public string DocumentNo { get; set; } = "Document No.";
        public string Email { get; set; } = "Email";
        public string InvoiceNo { get; set; } = "Invoice No.";
        public string Journal { get; set; } = "Journal";
        public string ReferenceNo { get; set; } = "Reference No.";
        public string WTTitleName { get; set; } = "WIRE TRANSFER – RECEIPT FROM CUSTOMER";
        public string BaseCurrency { get; set; } = "Base Currency";
        public string LocalCurrency { get; set; } = "Local Currency";
        public string TotalAmount { get; set; } = "Total Amount";
    }
}
