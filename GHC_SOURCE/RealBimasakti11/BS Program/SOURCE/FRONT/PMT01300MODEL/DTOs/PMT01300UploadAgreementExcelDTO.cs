using System;

namespace PMT01300MODEL
{
    public class PMT01300UploadAgreementExcelDTO
    {
        public int NO { get; set; } = 0;
        public string Building { get; set; } = "";
        public string Department { get; set; } = "";
        public string AgreementNo { get; set; } = "";
        public string AgreementDate { get; set; } = "";
        public DateTime? AgreementDateDisplay { get; set; }
        public string DocumentNo { get; set; } = "";
        public string DocumentDate { get; set; } = "";
        public DateTime? DocumentDateDisplay { get; set; }
        public string PlanHODate { get; set; } = "";
        public DateTime? PlanHODateDisplay { get; set; }
        public string StartDate { get; set; } = "";
        public DateTime? StartDateDisplay { get; set; } 
        public string EndDate { get; set; } = "";
        public DateTime? EndDateDisplay { get; set; } 
        public string Year { get; set; } = "";
        public string Month { get; set; } = "";
        public string Day { get; set; } = "";
        public string Currency { get; set; } = "";
        public string LeaseMode { get; set; } = "";
        public string ChargeMode { get; set; } = "";
        public string Salesman { get; set; } = "";
        public string Tenant { get; set; } = "";
        public string UnitDescription { get; set; } = "";
        public string Notes { get; set; } = "";
        public string BillingRule { get; set; } = "";
        public decimal BookingFee { get; set; } = 0;
        public string TCCode { get; set; } = "";
        public string Valid { get; set; } = "";
        public string NotesError { get; set; } = "";
    }
    public class PMT01300UploadAgreementSaveExcelDTO
    {
        public string Building { get; set; } = "";
        public string Department { get; set; } = "";
        public string AgreementNo { get; set; } = "";
        public string AgreementDate { get; set; } = "";
        public string DocumentNo { get; set; } = "";
        public string DocumentDate { get; set; } = "";
        public string PlanHODate { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
        public string Year { get; set; } = "";
        public string Month { get; set; } = "";
        public string Day { get; set; } = "";
        public string Currency { get; set; } = "";
        public string LeaseMode { get; set; } = "";
        public string ChargeMode { get; set; } = "";
        public string Salesman { get; set; } = "";
        public string Tenant { get; set; } = "";
        public string UnitDescription { get; set; } = "";
        public string Notes { get; set; } = "";
        public string BillingRule { get; set; } = "";
        public decimal BookingFee { get; set; } = 0;
        public string TCCode { get; set; } = "";
        public string Valid { get; set; } = "";
        public string NotesError { get; set; } = "";
    }
}
