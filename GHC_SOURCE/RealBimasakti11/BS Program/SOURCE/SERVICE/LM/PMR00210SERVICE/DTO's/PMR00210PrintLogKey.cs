using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using PMR00210COMMON;

namespace PMR00210SERVICE
{
    public class PMR00210PrintLogKey
    {
        public PMR00210ParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }

    }
}
