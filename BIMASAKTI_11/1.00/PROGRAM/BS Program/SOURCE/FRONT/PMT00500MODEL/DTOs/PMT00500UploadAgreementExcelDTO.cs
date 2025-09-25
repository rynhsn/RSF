using System;

namespace PMT00500MODEL
{
    public class PMT00500UploadAgreementExcelDTO
    {
        public int NO { get; set; } = 0;
        public string Building { get; set; } = "";
        public string Department { get; set; } = "";
        public string AgreementNo { get; set; } = "";
        public string AgreementDate { get; set; } = "";
        public DateTime? AgreementDateDisplay { get; set; }
        public string DocNo { get; set; } = "";
        public string DocDate { get; set; } = "";
        public DateTime? DocDateDisplay { get; set; }
        public string PlanHODate { get; set; } = "";
        public DateTime? PlanHODateDisplay { get; set; }
        public string StartDate { get; set; } = "";
        public DateTime? StartDateDisplay { get; set; } 
        public string Currency { get; set; } = "";
        public string ChargeMode { get; set; } = "";
        public string Salesman { get; set; } = "";
        public string Tenant { get; set; } = "";
        public string UnitDescription { get; set; } = "";
        public string Notes { get; set; } = "";
        public string BillingRule { get; set; } = "";
        public decimal BookingFee { get; set; } = 0;
        public string TCCode { get; set; } = "";
        public string ActualHODate { get; set; } = "";
        public DateTime? ActualHODateDisplay { get; set; } 
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
    public class PMT00500UploadAgreementSaveExcelDTO
    {
        public string Building { get; set; } = "";
        public string Department { get; set; } = "";
        public string AgreementNo { get; set; } = "";
        public string AgreementDate { get; set; } = "";
        public string DocNo { get; set; } = "";
        public string DocDate { get; set; } = "";
        public string PlanHODate { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string Currency { get; set; } = "";
        public string ChargeMode { get; set; } = "";
        public string Salesman { get; set; } = "";
        public string Tenant { get; set; } = "";
        public string UnitDescription { get; set; } = "";
        public string Notes { get; set; } = "";
        public string BillingRule { get; set; } = "";
        public decimal BookingFee { get; set; } = 0;
        public string TCCode { get; set; } = "";
        public string ActualHODate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
}
