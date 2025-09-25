using System.Runtime.ConstrainedExecution;

namespace PMM01000COMMON
{
    public class PMM01050ColumnPrintDTO
    {
        public string Utility_Charges { get; set; } = "Utility Charges";
        public string Currency { get; set; } = "Currency";
        public string Split_Admin_Invoice { get; set; } = "Split Admin Invoice";
        public string Administration_Fee_P { get; set; } = "Administration Fee (per month)";
        public string Taxable { get; set; } = "Taxable";
        public string Unit_Area_Validation { get; set; } = "Unit Area Validation (sqm)";
        public string To { get; set; } = "To";
        public string Cut_Off_Over_Weekday { get; set; } = "Cut Off Over Weekday";
        public string Holiday { get; set; } = "Holiday";
        public string Saturday { get; set; } = "Saturday";
        public string Sunday { get; set; } = "Sunday";
        public string Weekday_Overtime_Rate { get; set; } = "Weekday Overtime Rate";
        public string Level { get; set; } = "Level";
        public string Description { get; set; } = "Description";
        public string From { get; set; } = "From";
        public string Rate_Per_Hour { get; set; } = "Rate per Hour";
        public string Weekend_Overtime_Rate { get; set; } = "Weekend Overtime Rate";
        public string Charges_Date { get; set; } = "Charges Date";
    }
}
