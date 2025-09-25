using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common
{
    public class UserDepartmentDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; } 
        public string CDEPT_NAME { get; set; }
        public string CUSER_ID { get; set; }
        public string CUSER_NAME { get; set; }
        public string CACTION { get; set; } 
        public string CUSER_LOGIN_ID { get; set; }
        public string CUPDATE_BY { get; set; } 
        public DateTime DUPDATE_DATE { get; set; }
    }
}
