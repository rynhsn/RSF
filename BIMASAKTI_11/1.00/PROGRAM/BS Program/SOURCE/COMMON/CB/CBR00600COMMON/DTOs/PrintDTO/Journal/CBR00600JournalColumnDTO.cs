namespace CBR00600COMMON
{
    public class CBR00600JournalColumnDTO
    {
        //Header
        public string Reference_No { get; set; } = "Reference No.";
        public string Reference_Date { get; set; } = "Reference Date";
        public string Customer { get; set; } = "Customer";
        public string Cash_Or_Bank { get; set; } = "Cash / Bank";
        public string Department { get; set; } = "Department";
        public string Document_No { get; set; } = "Document No.";
        public string Document_Date { get; set; } = "Document Date";
        public string Cheque_Or_Giro_No { get; set; } = "Cheque / Giro No.";
        public string LowerAmount { get; set; } = "Amount";
        public string Local_Currency { get; set; } = "Local Currency";
        public string Base_Currency { get; set; } = "Base Currency";
        public string Description { get; set; } = "Description";
        public string Journal { get; set; } = "Journal";

        // Detail
        public string Date { get; set; } = "Date";
        public string Invoice_No { get; set; } = "Invoice No.";
        public string Account_No { get; set; } = "Account No.";
        public string Center { get; set; } = "Center";
        public string Debit { get; set; } = "Debit";
        public string Credit { get; set; } = "Credit";
    }
}
