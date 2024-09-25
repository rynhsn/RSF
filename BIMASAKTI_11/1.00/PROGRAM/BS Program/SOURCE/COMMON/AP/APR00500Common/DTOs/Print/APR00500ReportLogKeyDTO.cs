using APR00500Common.Params;
using R_CommonFrontBackAPI.Log;

namespace APR00500Common.DTOs
{
    public class APR00500ReportLogKeyDTO
    {
        public APR00500ReportParam poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}