using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00200COMMON.DTO_s
{
    public class ActiveInactiveParam : PMM00200DTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
        public Int32 IUSER_LEVEL { get; set; }
        public string CVALUE { get; set; } = "";
        public bool LACTIVE { get; set; }
    }
}
