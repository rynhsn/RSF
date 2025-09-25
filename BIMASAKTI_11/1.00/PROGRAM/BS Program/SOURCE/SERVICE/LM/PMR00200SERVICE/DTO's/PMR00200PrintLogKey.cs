using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using PMR00200COMMON;

namespace PMR00200SERVICE
{
    public class PMR00200PrintLogKey
    {
        public PMR00200ParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }

    }
}
