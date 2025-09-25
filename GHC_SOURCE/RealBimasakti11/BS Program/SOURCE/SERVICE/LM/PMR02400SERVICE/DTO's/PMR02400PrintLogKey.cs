using PMR02400COMMON.DTO_s;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace PMR02400SERVICE
{
    public class PMR02400PrintLogKey
    {
        public PMR02400ParamDTO poParamSummary { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }

    }
}
