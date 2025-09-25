using PMM10000COMMON.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.SLA_Call_Type
{
    public class PMM10000SLACallTypeDTO : BaseDTO
    {
        public string? CCALL_TYPE_ID { get; set; }
        public string? CCALL_TYPE_NAME { get; set; }
        public string? CCATEGORY_ID { get; set; }
        public string? CCATEGORY_NAME { get; set; }
        public int IDAYS { get; set; }
        public int IHOURS { get; set; }
        public int IMINUTES { get; set; }
        public bool LPRIORITY { get; set; }
    }
}
