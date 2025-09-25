using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLR00300Service.DTOLogs
{
    public class GLR00300ReportLogKeyDTO <T>
    {
        public T poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO? poReportGlobalDTO { get; set; }
    }
}
