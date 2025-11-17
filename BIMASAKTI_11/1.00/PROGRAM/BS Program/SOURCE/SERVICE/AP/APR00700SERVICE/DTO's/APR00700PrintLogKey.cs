using APR00700COMMON;
using APR00700OMMON;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APR00700SERVICE.DTO_s
{
    public class APR00700PrintLogKey
    {
        public APR00700ParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
