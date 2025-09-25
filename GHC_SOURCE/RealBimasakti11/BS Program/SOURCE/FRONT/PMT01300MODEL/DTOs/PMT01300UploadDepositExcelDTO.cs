using System;

namespace PMT01300MODEL
{
    public class PMT01300UploadDepositExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string FlagContractor { get; set; }
        public bool FlagContractorDisplay { get; set; }
        public string ContractorId { get; set; } = "";
        public string DepositId { get; set; } = "";
        public string DepositDate { get; set; } = "";
        public DateTime? DepositDateDisplay { get; set; } 
        public string Currency { get; set; } = "";
        public decimal DepositAmount { get; set; } = 0;
        public string FlagPaid { get; set; } 
        public bool FlagPaidDisplay { get; set; }
        public string Description { get; set; } = "";
        public string DocumentNo { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
    public class PMT01300UploadDepositSaveExcelDTO
    {
        public string DocumentNo { get; set; } = "";
        public string FlagContractor { get; set; } 
        public string ContractorId { get; set; } = "";
        public string DepositId { get; set; } = "";
        public string DepositDate { get; set; } = "";
        public string Currency { get; set; } = "";
        public decimal DepositAmount { get; set; } = 0;
        public string FlagPaid { get; set; }
        public string Description { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
