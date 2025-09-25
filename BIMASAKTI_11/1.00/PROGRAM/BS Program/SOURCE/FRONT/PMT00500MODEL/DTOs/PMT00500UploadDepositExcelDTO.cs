using System;

namespace PMT00500MODEL
{
    public class PMT00500UploadDepositExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string FlagContractor { get; set; }
        public bool FlagContractorDisplay { get; set; }
        public string ContractorId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public string DepositId { get; set; } = "";
        public string DepositDate { get; set; } = "";
        public DateTime? DepositDateDisplay { get; set; } 
        public string Currency { get; set; } = "";
        public decimal DepositAmount { get; set; } = 0;
        public string FlagPaid { get; set; } 
        public bool FlagPaidDisplay { get; set; }
        public string Description { get; set; } = "";
        public string DocNo { get; set; } = "";
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
    public class PMT00500UploadDepositSaveExcelDTO
    {
        public string DocNo { get; set; } = "";
        public string FlagContractor { get; set; } 
        public string ContractorId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public string DepositId { get; set; } = "";
        public string DepositDate { get; set; } = "";
        public string Currency { get; set; } = "";
        public decimal DepositAmount { get; set; } = 0;
        public string FlagPaid { get; set; }
        public string Description { get; set; } = "";
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
}
