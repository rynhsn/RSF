using HDM00500COMMON.DTO.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00500COMMON.DTO_s
{
    public class TaskchecklistDTO : GeneralParamDTO
    {

        public string CTASK_CHECKLIST_ID { get; set; } = "";
        public string CTASK_CHECKLIST_NAME { get; set; }= "";
        public bool? LACTIVE { get; set; }
        public string CACTIVE_BY { get; set; }= "";
        public string CACTION { get; set; }= "";
        public string CACTIVE_DATE { get; set; } = "";
        public DateTime? DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }= "";
        public string CINACTIVE_DATE { get; set; } = "";
        public DateTime? DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }= "";
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }= "";
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
