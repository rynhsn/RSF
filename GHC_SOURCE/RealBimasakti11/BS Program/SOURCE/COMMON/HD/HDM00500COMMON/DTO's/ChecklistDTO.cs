using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00500COMMON.DTO_s
{
    public class ChecklistDTO : TaskchecklistDTO
    {

        public string CCHECKLIST_CODE { get; set; } = "";
        public string CCHECKLIST_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
    }
}
