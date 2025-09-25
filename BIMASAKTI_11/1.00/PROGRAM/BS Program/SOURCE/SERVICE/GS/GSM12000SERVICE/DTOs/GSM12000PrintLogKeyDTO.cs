using GSM12000COMMON;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace GSM12000SERVICE
{
    public class GSM12000PrintLogKeyDTO
    {
        public GSM12000PrintParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
