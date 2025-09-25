using HDR00200Common.DTOs.Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace HDR00200Service.DTOs;

public class HDR00200ReportLogKeyDTO
{
        public HDR00200ReportParam poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poGlobalVar { get; set; }
}