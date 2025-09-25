using PMR02600Common.Params;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace PMR02600Service.DTOs;

public class PMR02600ReportLogKeyDTO
{
        public PMR02600ReportParam poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poGlobalVar { get; set; }
}