using APR00300Common.DTOs.Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace APR00300Service;

public class APR00300ReportLogKeyDTO
{
    public APR00300ReportParam poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
    public R_ReportGlobalDTO poGlobalVar { get; set; }
}