using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120Print
{
    public class PMT02120PrintReportColumnDTO
    {
        public string CAGREEMENT_INFORMATION { get; set; } = "Agreement Information";
        public string CDEPARTMENT { get; set; } = "Department";
        public string CREF_NO { get; set; } = "Reference No.";
        public string CREF_DATE { get; set; } = "Reference Date";

        public string CHANDOVER_INFORMATION { get; set; } = "Handover Information";
        public string CCONFIRMED_DATE { get; set; } = "Confirmed Date";
        public string CCONFIRMED_BY { get; set; } = "Confirmed By";
        public string CSCHEDULED_DATE { get; set; } = "Scheduled Date";

        public string CUNIT_INFORMATION { get; set; } = "Unit Information";
        public string CPROPERTY_ID { get; set; } = "Property";
        public string CBUILDING_ID { get; set; } = "Building";
        public string CFLOOR_ID { get; set; } = "Floor";
        public string CUNIT_ID { get; set; } = "Unit";
        public string CUNIT_TYPE_CATEGORY { get; set; } = "Unit Type Category";

        public string CTENANT_INFORMATION { get; set; } = "Tenant Information";
        public string CTENANT { get; set; } = "Tenant";
        public string CPHONE_NO { get; set; } = "Phone No.";
        public string CEMAIL { get; set; } = "Email";

        public string CASSIGNED_EMPLOYEE { get; set; } = "Assigned Employee";
        public string CEMPLOYEE { get; set; } = "Employee";
        public string CTYPE { get; set; } = "Type";

        public string CCHECKLIST { get; set; } = "Checklist";
        public string CCATEGORY { get; set; } = "Category";
        public string CITEM { get; set; } = "Item";
        public string CSTATUS { get; set; } = "Status";
        public string CNOTES { get; set; } = "Notes";
        public string CQUANTITY { get; set; } = "Quantity";
        public string CACTUAL_QUANTITY { get; set; } = "Actual Quantity";
        public string CTOTAL_QUANTITY { get; set; } = "Total Quantity";

        public string CDUTIES_AND_RESPONSIBILITIES { get; set; } = "Duties and Responsibilities";
        public string CMESSAGE_DUTIES_1 { get; set; } = "The employees listed above are assigned to carry out the handover procedure according to the prepared checklist. Ensure all items are thoroughly inspected and their status recorded.";
        public string CMESSAGE_DUTIES_2 { get; set; } = "The employees listed above are assigned to carry out the handover procedure accordingly. Ensure everything is thoroughly inspected and their status recorded.";

        public string CCONFIRMATION { get; set; } = "Confirmation";
        public string CMESSAGE_CONFIRMATION { get; set; } = "Both parties confirm that the handover has been carried out in accordance with the checklist.";

        public string CDATE { get; set; } = "Date";
        public string COK_NOTOK { get; set; } = "OK / Not OK";

        //public string CTEANNT { get; set; }

        public string CFIRST_PRINT { get; set; } = "First Print";
        public string CREPRINT_NO { get; set; } = "Re-Print No";
        public string CRESCHEDULED_COUNT { get; set; } = "Rescheduled Count";
    }
}
