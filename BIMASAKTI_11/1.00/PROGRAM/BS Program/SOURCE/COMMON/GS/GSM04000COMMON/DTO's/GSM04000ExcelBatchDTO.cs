using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common
{

    public class GSM04000ExcelBatchDTO
    {
        public int No { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string CenterCode { get; set; }
        public string ManagerName { get; set; }
        public string BranchCode { get; set; }
        public bool Everyone { get; set; }
        public bool Active { get; set; }
        public string NonActiveDate { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
}
