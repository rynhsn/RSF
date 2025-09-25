using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00200COMMON
{
    public class RecurringJournalListParamDTO
    {
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get;  set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPERIOD_YYYYMM { get; set; }
        public int CPERIOD_YYYY { get; set; }
        public string CPERIOD_MM { get; set; }
        public string CSTATUS { get; set; } = "";
        public string CSEARCH_TEXT { get; set; }
        public bool LSHOW_ALL { get; set; }
        public string CREC_ID { get; set; }
    }
}
