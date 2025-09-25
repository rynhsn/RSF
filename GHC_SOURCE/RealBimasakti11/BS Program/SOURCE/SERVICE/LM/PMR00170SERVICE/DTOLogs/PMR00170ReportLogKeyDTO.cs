using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR00170SERVICE.DTOLogs
{
    public class PMR00170ReportLogKeyDTO <T>
    {
        public T? poParam { get; set; }
        public R_NetCoreLogKeyDTO? poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
