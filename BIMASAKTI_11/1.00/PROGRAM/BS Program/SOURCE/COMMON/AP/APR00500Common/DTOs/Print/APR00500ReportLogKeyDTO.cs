using APR00500Common.Params;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace APR00500Common.DTOs.Print
{
    public class APR00500ReportLogKeyDTO
    {
        public APR00500ReportParam poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poGlobalVar { get; set; }
    }
}