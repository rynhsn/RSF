using PMR00150COMMON.DTO_Report_Detail;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150ColumnSummaryDTO
    {
        public string Col_CTRANS_NAME { get; set; } = "Transaction Name";
        public string Col_CSALESMAN { get; set; } = "Salesman";
        public string Col_CREF_NO { get; set; } = "LOC No.";
        public string Col_CREF_DATE { get; set; } = "LOC Date";
        public string Col_CTENURE { get; set; } = "Tenure";
        public string Col_STATUS { get; set; } = "Status";
        public string Col_CAGREEMENT_STATUS_NAME { get; set; } = "Agreement";
        public string Col_CTRANS_STATUS_NAME { get; set; } = "Transaction";
        public string Col_NTOTAL_PRIZE { get; set; } = "Total Rent Prize";
        public string Col_CTAX { get; set; } = "Tax";
        public string Col_CTENANT { get; set; } = "Tenant";    
        public string Col_NTOTAL_GROSS_AREA_SIZE { get; set; } = "Gross";
        public string Col_NTOTAL_NET_AREA_SIZE { get; set; } = "Net";
        public string Col_NTOTAL_COMMON_AREA_SIZE { get; set; } = "Common";
        public string Col_CTC_MESSAGE { get; set; } = "Term & Condition";
        public string Col_GRAND_TOTAL_AREA { get; set; } = "Grand Total Area";

    }
}
