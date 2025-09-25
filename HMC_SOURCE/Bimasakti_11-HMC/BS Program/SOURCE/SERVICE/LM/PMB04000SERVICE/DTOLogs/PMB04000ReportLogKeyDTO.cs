using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB04000SERVICE.DTOLogs
{
    public class PMB04000ReportLogKeyDTO <T>
    {
        public T? poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobalDTO { get; set; }
    }
}
