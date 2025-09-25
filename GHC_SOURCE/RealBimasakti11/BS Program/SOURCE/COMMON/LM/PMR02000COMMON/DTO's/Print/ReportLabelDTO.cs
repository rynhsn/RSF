using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print
{
    public class ReportLabelDTO
    {
        public string LABEL_PARAM_PROPERTY { get; set; } = "Property";
        public string LABEL_PARAM_DATA_BASED_ON { get; set; } = "Data Based On";
        public string LABEL_PARAM_REMAINING_BASED_ON { get; set; } = "Remaining Based On";
        public string LABEL_PARAM_CUTT_OFF_DATE { get; set; } = "Cut Off Date";
        public string LABEL_PARAM_PERIOD { get; set; }="Period";
        public string LABEL_PARAM_REPORT_TYPE { get; set; } = "Report Type";
        public string LABEL_PARAM_CURRENCY { get; set; } = "Currency";
        public string LABEL_PARAM_TRANS_CURR { get; set; } = "Transaction Currency";
        public string LABEL_PARAM_LOCAL_CURR { get; set; } = "Local Currency";
        public string LABEL_PARAM_BASE_CURR { get; set; } = "Base Currency";
        public string LABEL_PARAM_FILTER { get; set; } = "Filter";
        public string LABEL_PARAM_FILTER_DEPT { get; set; } = "Departent";
        public string LABEL_PARAM_FILTER_TRANSTYPE { get; set; } = "Transaction Type";
        public string LABEL_PARAM_FILTER_CUSTCTG { get; set; } = "Customer Category";
        public string LABEL_DEPARTMENT { get; set; } = "Department";
        public string LABEL_CUSTOMER { get; set; } = "Customer";
        public string LABEL_CUSTOMER_TYPE { get; set; } = "Customer Type";
        public string LABEL_TRANS_TYPE { get; set; } = "Trx Type";
        public string LABEL_REFNO { get; set; } = "Refference No.";
        public string LABEL_REFDATE { get; set; } = "Ref. Date";
        public string LABEL_LOI_AGRMT_NO { get; set; } = "LOI/Agrmnt. No.";
        public string LABEL_CURRENCY { get; set; } = "Curr.";
        public string LABEL_AMOUNT { get; set; } = "Amount";
        public string LABEL_BEGINNING_APPLY { get; set; } = "Beginning Apply";
        public string LABEL_REMAINING { get; set; } = "Remaining";
        public string LABEL_TAX { get; set; } = "Tax";
        public string LABEL_GAIN_LOSS { get; set; } = "Gain / Loss";
        public string LABEL_ALLOCATION { get; set; } = "Allocation";
        public string LABEL_SUBTOTAL { get; set; } = "Sub Total based on";
        public string LABEL_GRANDTOTAL { get; set; } = "Grand Total";

        public string LABEL_REFERENCE { get; set; } = "Reference";
        public string lABEL_NO { get; set; } = "No.";
        public string LABEL_DATE { get; set; } = "Date";
        public string LABEL_PRIDUCTCHARGE { get; set; } = "Product/Charge";
        public string LABEL_PRODUCTDEPT { get; set; } = "Product Departmnet";
        public string LABEL_PRODUCTQTY { get; set; } = "Quantity";
        public string LABEL_PRODUCTMEASUREMENT { get; set; } ="Measurement";
        public string LABEL_PRODUCTUNITPRICE { get; set; } = "Unit Price";
        public string LABEL_PRODUCTTOTPRICE { get; set; }="Total Price";
        public string LABEL_PRODUCTLINEDISC { get; set; }="Line Discount";
        public string LABEL_PRODUCTOTHERTAX { get; set; }="Other Tax";
        public string LABEL_PRODUCTTOTALAMOUNT { get; set; }="Line Amount";

    }
}
