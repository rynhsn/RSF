using System.Collections.Generic;

namespace GSM04000Common.DTO_s.Print
{
    public class GSM04000PrintBaseHeaderLogoDTO
    {
        public string? CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] BLOGO { get; set; }
    }

    public class GSM04000ReportDataDTO
    {
        public DepartmentDTO Department { get; set; }
        public List<UserDepartmentDTO> Users { get; set; }
    }
    
    public class GSM04000ReportResultDTO
    {
        public string Title { get; set; }
        public GSM04000ReportLabelDTO Label { get; set; }
        public List<GSM04000ReportDataDTO> Data { get; set; }
    }
    
    public class GSM04000ReportLabelDTO
    {
        public string DEPARTMENT { get; set; } = "Department";
        public string CENTER { get; set; } = "Center";
        public string BRANCH { get; set; } = "Branch";
        public string MANAGER { get; set; } = "Manager";
        public string EVERYONE { get; set; } = "Everyone";
        public string ACTIVE { get; set; } = "Active";
        public string EMAIL_1 { get; set; } = "Email 1";
        public string EMAIL_2 { get; set; } = "Email 2";
        public string ASSIGNED_USER { get; set; } = "Assigned User";
    }
    
    public class GSM04000ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GSM04000ReportResultDTO Data { get; set; }
    }
}