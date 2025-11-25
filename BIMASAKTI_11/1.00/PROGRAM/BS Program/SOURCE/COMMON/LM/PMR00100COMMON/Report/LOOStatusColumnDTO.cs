using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.Report
{
    public class LOOStatusColumnDTO
    {
        public string COL_PROPERTY { get; set; } = "Property";
        public string COL_DEPT { get; set; } = "Department";
        public string COL_SALESMAN { get; set; } = "Salesman";
        public string COL_PERIOD { get; set; } = "Period";
        public string COL_REPORT_TYPENAME { get; set; } = "Report Type";

        public string COL_CTRANS_NAME { get; set; } = "Transaction Name";
        public string COL_CSALESMAN_NAME { get; set; } = "Salesman";
        public string COL_CREF_NO { get; set; } = "LOO No.";
        public string COL_CREF_DATE { get; set; } = "LOO Date";
        public string COL_CTENURE { get; set; } = "Tenure";
        public string COL_STATUS_NAME { get; set; } = "Status";
        public string COL_CAGREEMENT_STATUS_NAME { get; set; } = "Agreement";
        public string COL_CTRANS_STATUS_NAME { get; set; } = "Transaction";
        public string COL_NREVISION_COUNT { get; set; } = "Revision Count";
        public string COL_NTOTAL_PRICE { get; set; } = "Total Rent Price";
        public string COL_CTAX { get; set; } = "Tax";
        public string COL_CTENANT_NAME { get; set; } = "Tenant";
        public string COL_CTC_MESSAGE { get; set; } = "Term & Condition";
        public string COL_CUNIT_DETAIL_NAME { get; set; } = "Unit";

        public string COL_NUNIT_DETAIL_AREA { get; set; } = "Area";
        public string COL_NUNIT_DETAIL_GROSS_AREA_SIZE { get; set; } = "Gross";
        public string COL_NUNIT_DETAIL_NET_AREA_SIZ { get; set; } = "Net";
        public string COL_NUNIT_DETAIL_COMMON_AREA_SIZE { get; set; } = "Common";
        public string COL_NUNIT_DETAIL_PRICE { get; set; } = "Price";
        public string COL_NUNIT_TOTAL { get; set; } = "Total";

        public string COL_CCHARGE_DETAIL_TYPE_NAME { get; set; } = "Charge Type";
        public string COL_CCHARGE_DETAIL_UNIT_NAME { get; set; } = "Unit";
        public string COL_CCHARGE_DETAIL_CHARGE_NAME { get; set; } = "Charge";
        public string COL_CCHARGE_DETAIL_TAX_NAME { get; set; } = "Tax";
        public string COL_CCHARGE_DETAIL_START_DATE { get; set; } = "Start Date";
        public string COL_CCHARGE_DETAIL_END_DATE { get; set; } = "End Date";
        public string COL_CCHARGE_DETAIL_TENURE { get; set; } = "Tenure";
        public string COL_CCHARGE_DETAIL_FEE_METHOD { get; set; } = "Fee Method";
        public string COL_NCHARGE_DETAIL_FEE_AMOUNT { get; set; } = "Fee Amount";
        public string COL_LFOR_TOTAL_AREA { get; set; } = "For Total Area";
        public string COL_NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT { get; set; } = "Total Amount";
        public string COL_NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT { get; set; } = "Total";

        public string COL_CDEPOSIT_DETAIL_DEPOSIT { get; set; } = "Deposit";
        public string COL_CDEPOSIT_DETAIL_DATE { get; set; } = "Date";
        public string COL_NDEPOSIT_DETAIL_AMOUNT { get; set; } = "Amount";
        public string COL_CDEPOSIT_DETAIL_DESCRIPTION { get; set; } = "Description";

        public string COL_TOTAL_OF_AGGREMENT_BASED_ON_SM { get; set; } = "Total of Agreement Based On";
        public string COL_GRAND_TOTAL_OF_AGGREMENT { get; set; } = "Grand Total Of Agreements";

    }
}
