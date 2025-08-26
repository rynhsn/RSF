using GSM02500COMMON.DTOs;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace GSM02500SERVICE
{
    public class GSM02500PrintLogKeyDTO
    {
        public GSM02500PrintParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
