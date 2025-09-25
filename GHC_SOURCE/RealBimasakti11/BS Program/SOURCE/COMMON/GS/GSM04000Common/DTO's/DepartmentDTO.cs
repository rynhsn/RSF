using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common
{
    public class DepartmentDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CMANAGER_CODE { get; set; }
        public string CMANAGER_NAME { get; set; }
        public string CBRANCH_CODE { get; set; }
        public string CBRANCH_NAME { get; set; }
        public bool LEVERYONE { get; set; }
        public bool LACTIVE { get; set; }
        public string CEMAIL1 { get; set; }
        public string CEMAIL2 { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUSER_ID { get;set; }
    }
}
