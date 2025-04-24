using System;
using System.Collections.Generic;

namespace PMR00460Common.DTOs.Print
{
    public class PMR00460DataResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CSCHEDULE_DATE { get; set; }
        public DateTime? DSCHEDULE_DATE { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY { get; set; }
        public string CREF_NO { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_EMAIL { get; set; }
        public string CHANDOVER_STATUS { get; set; }
        public bool LCHECKLIST { get; set; }
        public string CSTAFF_ID { get; set; }
        public string CSTAFF_NAME { get; set; }
        public string CCHECKLIST_DESCRIPTION { get; set; }
        public string CNOTES { get; set; }
        public bool LCHECKLIST_STATUS { get; set; }
        public string CCARE_REF_NO { get; set; }
        public int IDEFAULT_QUANTITY { get; set; }
        public int IACTUAL_QUANTITY { get; set; }
        public string CUNIT { get; set; }
    }
    
    public class PMR00460ReportDataDTO
    {
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public List<PMR00460GroupBuildingDTO> LOI { get; set; }
    }
    
    public class PMR00460GroupBuildingDTO
    {
        public string CREF_NO { get; set; }
        public List<PMR00460GroupByLoiSummaryDTO> Summary { get; set; }
        public List<PMR00460GroupByLoiDetailDTO> Detail { get; set; }
    }

    public class PMR00460GroupByLoiSummaryDTO
    {
        public string CREF_NO { get; set; }
        public bool LCHECKLIST { get; set; }
        
        public string CSCHEDULE_DATE { get; set; }
        public DateTime? DSCHEDULE_DATE { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_EMAIL { get; set; }
        public string CSTAFF_ID { get; set; }
        public string CSTAFF_NAME { get; set; }
        public string CHANDOVER_STATUS { get; set; }
    }
    
    public class PMR00460GroupByLoiDetailDTO
    {
        public string CCHECKLIST_DESCRIPTION { get; set; }
        public string CNOTES { get; set; }
        public string CCHECKLIST_STATUS { get; set; }
        public string CCARE_REF_NO { get; set; }
        public int IDEFAULT_QUANTITY { get; set; }
        public int IACTUAL_QUANTITY { get; set; }
        public string CQUANTITY_DISPLAY { get; set; }
        public string CUNIT { get; set; }
    }
    
    public class PMR00460ReportHeaderDTO
    {
        public string CPROPERTY_NAME { get; set; } = "";
        public string CBUILDING { get; set; } = "";
        public string CPERIOD { get; set; } = "";
        public string CREPORT_TYPE { get; set; } = "";
        public string CREPORT_TYPE_NAME { get; set; } = "";
        public string CTYPE { get; set; } = "";
        public string CTYPE_NAME { get; set; } = "";
        
        public bool LOPEN { get; set; }
        public bool LSCHEDULED { get; set; }
        public bool LCONFIRMED { get; set; }
        public bool LCLOSED { get; set; }
        
    }
    
    public class PMR00460ReportLabelDTO
    {
        public string Property { get; set; } = "Property";
        public string Building { get; set; } = "Building";
        public string Period { get; set; } = "Period";
        public string ReportType { get; set; } = "Report Type";
        public string AgreementType { get; set; } = "Agreement Type";
        public string Status { get; set; } = "Status";
        public string Open { get; set; } = "Open";
        public string Scheduled { get; set; } = "Scheduled";
        public string Confirmed { get; set; } = "Confirmed";
        public string Closed { get; set; } = "Closed";
        public string Date { get; set; } = "Date";
        public string UnitID { get; set; } = "Unit ID";
        public string Category { get; set; } = "Category";
        public string LOINo { get; set; } = "LOI No";
        public string Tenant { get; set; } = "Tenant";
        public string TenantEmail { get; set; } = "Tenant Email";
        public string HandoverBy { get; set; } = "Handover By";
        public string ChecklistDescription { get; set; } = "Checklist Description";
        public string Notes { get; set; } = "Notes";
        public string CareNo { get; set; } = "CARE No.";
        public string Quantity { get; set; } = "Quantity";
        public string ActualQuantity { get; set; } = "Actual Quantity";
        public string TotalQuantity { get; set; } = "Total Quantity";
    }
    
    public class PMR00460ReportResultDTO
    {
        public string Title { get; set; }

        public PMR00460ReportLabelDTO Label { get; set; }

        public PMR00460ReportHeaderDTO Header { get; set; }
        public List<PMR00460ReportDataDTO> Data { get; set; }
    }
    
    
    public class PMR00460ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMR00460ReportResultDTO Data { get; set; }
    }
}