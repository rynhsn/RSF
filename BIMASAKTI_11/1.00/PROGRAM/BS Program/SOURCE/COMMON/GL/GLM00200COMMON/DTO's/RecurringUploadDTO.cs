namespace GLM00200COMMON.DTO_s
{
    public class RecurringUploadDTO
    {
        
        public string? Department { get; set; }="";
        public string? Recurring_No { get; set; }="";
        public string? Recurring_Name { get; set; }="";
        public string? Document_No { get; set; }="";
        public string? Currency_Code { get; set; }="";
        public int? Fixed_Rate { get; set; }=0;
        public decimal? Local_Currency_Base { get; set; } = 0;
        public decimal? Local_Currency_Rate { get; set; } = 0;
        public decimal? Base_Currency_Base_Rate { get; set; } = 0;
        public decimal? Base_Currency_Rate { get; set; } = 0;
        public int? Frequency { get; set; } = 0;
        public int? Interval { get; set; } = 0;
        public string? Start_Date { get; set; }="";
        public string? Account_No { get; set; }="";
        public string? Center_Code { get; set; }="";
        public decimal? Debit_Amount { get; set; } = 0;
        public decimal? Credit_Amount { get; set; } = 0;
        public string? Description { get; set; }="";
        
    }
}
