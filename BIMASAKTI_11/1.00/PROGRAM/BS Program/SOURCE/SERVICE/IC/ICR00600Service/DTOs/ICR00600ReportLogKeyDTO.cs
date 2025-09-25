using ICR00600Common.DTOs.Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace ICR00600Service.DTOs;

public class ICR00600ReportLogKeyDTO
{
    public ICR00600ReportParam poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
    public R_ReportGlobalDTO poGlobalVar { get; set; }
}