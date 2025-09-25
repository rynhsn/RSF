using PMR00800COMMON.DTO_s;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace PMR00800SERVICE
{
    public class PMR00800PrintLogKey
    {
        public PMR00800ParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }

    }
}
