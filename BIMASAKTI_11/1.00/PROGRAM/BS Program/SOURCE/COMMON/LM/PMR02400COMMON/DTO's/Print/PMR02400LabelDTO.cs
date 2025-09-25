using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02400COMMON.DTO_s.Print
{
    public class PMR02400LabelDTO
    {
        public string PROPERTY_LABEL { get; set; } = "Property";
        public string PERIOD_LABEL { get; set; } = "Period";
        public string CUTOFF_DATE_LABEL { get; set; } = "Cut Off Date";
        public string REPORT_TYPE_LABEL { get; set; } = "Report Type";
        public string REPORT_OPTION { get; set; } = "Report Option";
        public string REPORT_CURRENCY_TYPE{ get; set; } = "Currency Type";
        public string CUSTOMER_LABEL { get; set; } = "Customer";
        public string COLUMN_CUSTOMER_LABEL { get; set; } = "Customer";
        public string COLUMN_AGREEMENT_LABEL { get; set; } = "Agreement No.";
        public string COLUMN_UNIT_LABEL { get; set; } = "Unit Name";
        public string COLUMN_BUILDING_LABEL { get; set; } = "Building";
        public string COLUMN_CCURRENCY_LABEL { get; set; } = "Curr.";
        public string COLUMN_INVOICE_LABEL { get; set; } = "Invoice";
        public string COLUMN_REDEEMED_LABEL { get; set; } = "Redeemed";
        public string COLUMN_PAID_LABEL { get; set; } = "Paid";
        public string COLUMN_OUTSTANDING_LABEL { get; set; } = "Outstanding";
        public string COLUMN_AMOUNT_LABEL { get; set; } = "Amount";
        public string COLUMN_NO_LABEL { get; set; } = "No.";
        public string COLUMN_DESC_LABEL { get; set; } = "Description";
        public string COLUMN_DUE_DATE_LABEL { get; set; } = "Due Date";
        public string COLUMN_LATE_DAYS_LABEL { get; set; } = "Late Days";
        public string SUBTOTAL_LABEL { get; set; } = "Sub Total based on";
        public string GRANDTOTAL_LABEL { get; set; } = "Grand Total";
    }
}
