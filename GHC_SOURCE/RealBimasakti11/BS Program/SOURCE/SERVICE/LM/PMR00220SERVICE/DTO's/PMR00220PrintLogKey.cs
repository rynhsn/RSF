using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Text;
using PMR00220COMMON;
using R_BackEnd;

namespace PMR00220SERVICE
{
    public class PMR00220PrintLogKey
    {
        public PMR00220ParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }

    }
}
