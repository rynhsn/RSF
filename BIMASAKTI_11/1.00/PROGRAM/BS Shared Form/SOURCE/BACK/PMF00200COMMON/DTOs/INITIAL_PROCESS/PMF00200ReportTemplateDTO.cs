using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00200COMMON
{
    public class PMF00200ReportTemplateDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROGRAM_ID { get; set; }
        public string CTEMPLATE_ID { get; set; }
        public string CTEMPLATE_NAME { get; set; }
        public string CFILE_NAME { get; set; }
        public bool LDEFAULT { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
