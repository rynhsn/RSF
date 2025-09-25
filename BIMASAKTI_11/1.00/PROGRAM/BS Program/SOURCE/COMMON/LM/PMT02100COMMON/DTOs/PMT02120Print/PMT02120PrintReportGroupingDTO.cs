using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120Print
{
    public class PMT02120PrintReportGroupingDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }

        public string CCONFIRMED_DATE { get; set; }
        public DateTime? DCONFIRMED_DATE { get; set; }
        public DateTime? DCONFIRMED_DATE_ONLY { get; set; }
        public string CCONFIRMED_BY { get; set; }
        public string CSCHEDULED_DATE { get; set; }
        public DateTime? DSCHEDULED_DATE { get; set; }
        public DateTime? DSCHEDULED_DATE_ONLY { get; set; }
        public int IRESCHEDULE_COUNT { get; set; }
        public int IPRINT_COUNT { get; set; }

        public string CPROPERTY_NAME { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }

        public string CTENANT_NAME { get; set; }
        public string CTENANT_PHONE_NO { get; set; }
        public string CTENANT_EMAIL { get; set; }


        public List<PMT02120PrintReportEmployeeDTO> EmployeeData { get; set; }
        public List<PMT02120PrintReportChecklistDTO> ChecklistData { get; set; }
        public PMT02120PrintReportFooterDTO FooterData { get; set; }
    }
}
