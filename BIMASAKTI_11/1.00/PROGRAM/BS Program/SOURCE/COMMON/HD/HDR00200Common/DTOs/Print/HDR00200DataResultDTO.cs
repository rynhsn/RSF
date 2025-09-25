using System;
using System.Collections.Generic;

namespace HDR00200Common.DTOs.Print
{
    public class HDR00200DataResultDTO
    {
        public bool LMAINTENANCE { get; set; }
        public bool LPRIVATE { get; set; }
        public string? CBUILDING_ID { get; set; } = "";
        public string? CBUILDING_NAME { get; set; } = "";
        public string? CFLOOR_ID { get; set; } = "";
        public string? CFLOOR_NAME { get; set; } = "";
        public string? CUNIT_LOCATION_ID { get; set; } = "";
        public string? CUNIT_LOCATION_NAME { get; set; } = "";
        public string? CASSET_CODE { get; set; } = "";
        public string? CCARE_TICKET_NO { get; set; } = "";
        public DateTime? DCARE_TICKET_DATE { get; set; }
        public string? CCARE_DESCRIPTION { get; set; } = "";
        public string? CSTATUS { get; set; } = "";
        public string? CSTATUS_DESCRIPTION { get; set; } = "";
        public string? CSUBSTATUS { get; set; } = "";
        public string? CSUBSTATUS_DESCRIPTION { get; set; } = "";
        public string? CSUBSTATUS_DESCRIPTION_DISPLAY => string.IsNullOrEmpty(CSUBSTATUS) ? "" : $"({CSUBSTATUS_DESCRIPTION})";
        public string? CTENANT_OWNER_ID { get; set; } = "";
        public string? CTENANT_OWNER_NAME { get; set; } = "";
        public string? CCALLER_NAME { get; set; } = "";
        public string? CCALLER_PHONE { get; set; } = "";
        public string? CCALLER_CATEGORY { get; set; } = "";
        public string? CCALLER_CATEGORY_DESCRIPTION { get; set; } = "";
        public string? CTENANT_OR_OWNER { get; set; } = "";
        public string? CTENANT_OR_OWNER_DISPLAY => string.IsNullOrEmpty(CTENANT_OR_OWNER) ? "" : $"({CTENANT_OR_OWNER})";
        public string? CSOLUTION { get; set; } = "";
        public string? CINVOICE_REF_NO { get; set; } = "";
        public string? CCURRENCY_SYMBOL { get; set; } = "";
        public decimal NTOTAL_AMOUNT { get; set; }
        public bool LESCALATED { get; set; }
        public DateTime? DCLOSED_DATE { get; set; }


        public string? CTASK_CTG_CODE { get; set; } = "";
        public string? CTASK_CTG_NAME { get; set; } = "";
        public string? CTASK_CODE { get; set; } = "";
        public string? CTASK_NAME { get; set; } = "";
        public int IDURATION_TIME { get; set; }
        public string? CDURATION_TYPE { get; set; } = "";
        public string? CDURATION_TYPE_DESCRIPTION { get; set; } = "";
        public string? CCHECKLIST_STATUS { get; set; } = "";
        public string? CCHECKLIST_NOTES { get; set; } = "";
        public DateTime? DPLAN_START_DATE { get; set; }
        public DateTime? DPLAN_END_DATE { get; set; }
        public DateTime? DACTUAL_START_DATE { get; set; }
        public DateTime? DACTUAL_END_DATE { get; set; }
    }

    public class HDR00200ReportLabelDTO
    {
        public string Property { get; set; } = "Property";
        public string Period { get; set; } = "Period";
        public string Area { get; set; } = "Area";
        public string Building { get; set; } = "Building";
        public string Category { get; set; } = "Category";
        public string Department { get; set; } = "Department";
        public string Status { get; set; } = "Status";
        public string Handover { get; set; } = "Handover";
        public string Complaint { get; set; } = "Complaint";
        public string Request { get; set; } = "Request";
        public string Inquiry { get; set; } = "Inquiry";
        public string Open { get; set; } = "Open";
        public string Submitted { get; set; } = "Submitted";
        public string Assigned { get; set; } = "Assigned";
        public string OnProgress { get; set; } = "On Progress";
        public string Solved { get; set; } = "Solved";
        public string Completed { get; set; } = "Completed";
        public string Confirmed { get; set; } = "Confirmed";
        public string Closed { get; set; } = "Closed";
        public string Cancelled { get; set; } = "Cancelled";
        public string Terminated { get; set; } = "Terminated";
        public string Floor { get; set; } = "Floor";
        public string Unit { get; set; } = "Unit";
        public string TaskCategory { get; set; } = "Task Category";
        public string Description { get; set; } = "Description";
        public string TenantOwner { get; set; } = "Tenant/Owner";
        public string Caller { get; set; } = "Caller";
        public string Solution { get; set; } = "Solution";
        public string Invoice { get; set; } = "Invoice";
        public string Escalated { get; set; } = "Escalated";
        public string Location { get; set; } = "Location";
        public string CallerCategory { get; set; } = "Caller Category";
        public string Task { get; set; } = "Task";

        public string Every { get; set; } = "Every";

        // Start
        public string Start { get; set; } = "Start";

        // End
        public string End { get; set; } = "End";
        public string Asset { get; set; } = "Asset";
        public string TicketNoDate { get; set; } = "Ticket No. & Date";
        public string ChecklistStatus { get; set; } = "Checklist Status";
        public string ChecklistNotes { get; set; } = "Checklist Notes";
        public string PlanDate { get; set; } = "Plan Date";
        public string ActualDate { get; set; } = "Actual Date";
        public string IsEscalated { get; set; } = "Is Escalated";
        public string ClosedDate { get; set; } = "Closed Date";
        public string TotalCareIn { get; set; } = "Total CARE in";
        public string TotalCareFor { get; set; } = "Total CARE for";
        public string To { get; set; } = "To";
        public string HasEscalated { get; set; } = "has escalated";
        public string All { get; set; } = "all";
        public string WithInvoice { get; set; } = "with invoice";
        public string GrandTotalCareMaintenance { get; set; } = "Grand Total CARE Maintenance";
        public string GrandTotalCareInPrivateArea { get; set; } = "Grand Total CARE in Private Area";
        public string GrandTotalCareInPublicArea { get; set; } = "Grand Total CARE in Public Area";
    }

    public class HDR00200ReportHeaderDTO
    {
        public string PropertyDisplay { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public string? PeriodDisplay { get; set; }
        public string AreaDisplay { get; set; }
        public string BuildingDisplay { get; set; }
        public string CategoryDisplay { get; set; }
        public string DepartmentDisplay { get; set; }
        public bool CheckHandover { get; set; }
        public bool CheckComplaint { get; set; }
        public bool CheckRequest { get; set; }
        public bool CheckInquiry { get; set; }
        public bool CheckOpen { get; set; }
        public bool CheckSubmitted { get; set; }
        public bool CheckAssigned { get; set; }
        public bool CheckOnProgress { get; set; }
        public bool CheckSolved { get; set; }
        public bool CheckCompleted { get; set; }
        public bool CheckConfirmed { get; set; }
        public bool CheckClosed { get; set; }
        public bool CheckCancelled { get; set; }
        public bool CheckTerminated { get; set; }
    }

    #region Grouping for Maintenance

    public class HDR00200GroupAssetMaintenanceDTO
    {
        public string? CASSET_CODE { get; set; } = "";
        public List<HDR00200DataResultDTO> Data { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }

    public class HDR00200GroupTaskMaintenanceDTO
    {
        public string? CTASK_CODE { get; set; } = "";
        public string? CTASK_NAME { get; set; } = "";
        public int IDURATION_TIME { get; set; }
        public string? CDURATION_TYPE_DESCRIPTION { get; set; } = "";
        public List<HDR00200GroupAssetMaintenanceDTO> Asset { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }

    public class HDR00200GroupCareMaintenanceDTO
    {
        public string? CTASK_CTG_CODE { get; set; } = "";
        public string? CTASK_CTG_NAME { get; set; } = "";
        public List<HDR00200GroupTaskMaintenanceDTO> Task { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }

    #endregion
    
    #region Grouping for Private

    public class HDR00200GroupUnitPrivateDTO
    {
        public string CUNIT_LOCATION_ID { get; set; } = "";
        public List<HDR00200DataResultDTO> Data { get; set; }
        public int SUM_WITH_INVOICE { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }
    
    public class HDR00200GroupFloorPrivateDTO
    {
        public string CFLOOR_ID { get; set; } = "";
        public string CFLOOR_NAME { get; set; } = "";
        public List<HDR00200GroupUnitPrivateDTO> Unit { get; set; }
        public int SUM_WITH_INVOICE { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }
    
    public class HDR00200GroupCarePrivateDTO
    {
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public List<HDR00200GroupFloorPrivateDTO> Floor { get; set; }
        public int SUM_WITH_INVOICE { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }
    
    #endregion
    
    #region Grouping for Public

    public class HDR00200GroupCarePublicDTO
    {
        public string CUNIT_LOCATION_ID { get; set; } = "";
        public string CUNIT_LOCATION_NAME { get; set; } = "";
        public string CBUILDING_FLOOR { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public List<HDR00200DataResultDTO> Data { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }
    
    #endregion

    public class HDR00200ReportResultDTO
    {
        public string Title { get; set; }

        public HDR00200ReportLabelDTO Label { get; set; }

        public HDR00200ReportHeaderDTO Header { get; set; }
        public List<HDR00200GroupCareMaintenanceDTO> DataMaintenance { get; set; }
        public List<HDR00200GroupCarePublicDTO> DataPublic { get; set; }
        public List<HDR00200GroupCarePrivateDTO> DataPrivate { get; set; }
        public int SUM_WITH_INVOICE { get; set; }
        public int SUM_ESCALATED { get; set; }
        public int SUM_ALL { get; set; }
    }

    public class HDR00200ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public HDR00200ReportResultDTO Data { get; set; }
    }
}