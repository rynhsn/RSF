namespace GLM00200COMMON.DTO_s
{
    public class RecurringUploadDTO
    {
        public int? SEQ_NO { get; set; }
        public string? Department { get; set; }="";
        public string? Recurring_No { get; set; }="";
        public string? Recurring_Name { get; set; }="";
        public string? Document_No { get; set; }="";
        public string? Currency_Code { get; set; }="";
        public string? Fixed_Rate { get; set; }="";
        public decimal? Local_Currency_Base { get; set; }
        public decimal? Local_Currency_Rate { get; set; }
        public decimal? Base_Currency_Base_Rate { get; set; }
        public decimal? Base_Currency_Rate { get; set; }
        public int? Frequency { get; set; }
        public int? Interval { get; set; }
        public string? Start_Date { get; set; }="";
        public string? Account_No { get; set; }="";
        public string? Center_Code { get; set; }="";
        public decimal? Debit_Amount { get; set; }
        public decimal? Credit_Amount { get; set; }
        public string? Description { get; set; }="";
        
    }
}
