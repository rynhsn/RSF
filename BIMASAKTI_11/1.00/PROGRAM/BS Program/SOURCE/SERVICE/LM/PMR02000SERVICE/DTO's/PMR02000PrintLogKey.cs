using PMR02000COMMON.DTO_s.Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace PMR02000SERVICE
{
    public class PMR02000PrintLogKey
    {
        public ReportParamDTO poParamSummary { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
